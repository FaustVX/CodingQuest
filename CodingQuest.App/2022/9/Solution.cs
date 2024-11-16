using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CQ_2022_9;

[Day(2022, 9, id:6, "Debugging")]
sealed partial class Solution([Field(Type = typeof(OpCode[]), AssignFormat = """Helpers.ParseToArray<OpCode>({0})""")]string input) : ISolution
{
    public int RunCount => 1;

    public string Run(int index, bool isTest)
    => Run1();

    public string Run1()
    {
        var cpu = new CPU(_input);
        cpu.Run();
        return cpu.Out;
    }
}

sealed partial class CPU([Field(Name = "_rom")]OpCode[] opCodes)
{
    public long PC { get; set; } = 0;
    public long[] Registers { get; } = new long['L' - 'A' + 1];
    public bool LastComp { get; set; }
    public bool IsHalted { get; set; }
    public string Out => _sb.ToString();
    private readonly StringBuilder _sb = new();

    public void Output(long value)
    {
        if (_sb.Length != 0)
            _sb.Append(',');
        _sb.Append(value);
    }

    public void Run()
    {
        while (!IsHalted)
            _rom[PC++].Execute(this);
    }
}

abstract class Location
{
    public static Location Parse(ReadOnlySpan<char> location, out ReadOnlySpan<char> loc)
    {
        if (location[0] is >= 'A' and <= 'L' and var m)
        {
            loc = location[1..];
            return new Memory(m - 'A');
        }
        var space = location.IndexOf(' ');
        if (space < 0)
        {
            loc = [];
            space = location.Length;
        }
        else
            loc = location[space..];
        return new Immediate(long.Parse(location[..space]));
    }

    public abstract long GetValue(CPU cpu);

    public override abstract string ToString();
}

sealed partial class Memory([Property] int location) : Location
{
    public override long GetValue(CPU cpu)
    => cpu.Registers[Location];

    public override string ToString()
    => ((char)(Location + 'A')).ToString();
}

sealed partial class Immediate([Property] long value) : Location
{
    public override long GetValue(CPU cpu)
    => Value;

    public override string ToString()
    => Value.ToString();
}

abstract class OpCode : IParsable<OpCode>
{
    public static OpCode Parse(string s, IFormatProvider? provider)
    => s.AsSpan().Count(' ') switch
    {
        2 => BinaryOpCode.Parse(s),
        1 => SourceOpCode.Parse(s),
        0 => End.Parse(s),
        _ => throw new UnreachableException(),
    };

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out OpCode result)
    => throw new NotImplementedException();
    public abstract void Execute(CPU cpu);

    public override abstract string ToString();
}

abstract partial class BinaryOpCode : OpCode
{
    public static BinaryOpCode Parse(ReadOnlySpan<char> line)
    {
        if (line[0] is 'C')
            return SourceSourceOpCode.Parse(line);
        return TargetSourceOpCode.Parse(line);
    }
}

abstract partial class TargetSourceOpCode([Property]Memory target, [Property]Location source) : BinaryOpCode
{
    public static new TargetSourceOpCode Parse(ReadOnlySpan<char> line)
    => line[..3] switch
    {
        "ADD" => new Add((Memory)Location.Parse(line[4..], out line), Location.Parse(line[1..], out _)),
        "MOD" => new Mod((Memory)Location.Parse(line[4..], out line), Location.Parse(line[1..], out _)),
        "DIV" => new Div((Memory)Location.Parse(line[4..], out line), Location.Parse(line[1..], out _)),
        "MOV" => new Mov((Memory)Location.Parse(line[4..], out line), Location.Parse(line[1..], out _)),
        _ => throw new UnreachableException(),
    };

    public override string ToString()
    => $"{GetType().Name.ToUpperInvariant()} {Target} {Source}";
}

abstract partial class SourceOpCode([Property]Location source) : OpCode
{
    public static SourceOpCode Parse(ReadOnlySpan<char> line)
    => line[..3] switch
    {
        "JMP" => new Jmp(Location.Parse(line[4..], out _)),
        "JIF" => new Jif(Location.Parse(line[4..], out _)),
        "OUT" => new Out(Location.Parse(line[4..], out _)),
        _ => throw new UnreachableException(),
    };

    public override string ToString()
    => $"{GetType().Name.ToUpperInvariant()} {Source}";
}

abstract partial class SourceSourceOpCode([Property]Location source1, [Property]Location source2) : BinaryOpCode
{
    public static new SourceSourceOpCode Parse(ReadOnlySpan<char> line)
    => line[..3] switch
    {
        "CEQ" => new Ceq(Location.Parse(line[4..], out line), Location.Parse(line[1..], out _)),
        "CGE" => new Cge(Location.Parse(line[4..], out line), Location.Parse(line[1..], out _)),
        _ => throw new UnreachableException(),
    };

    public override string ToString()
    => $"{GetType().Name.ToUpperInvariant()} {Source1} {Source2}";
}

sealed class End : OpCode
{
    public static End Parse(ReadOnlySpan<char> line)
    => new();

    public override void Execute(CPU cpu)
    => cpu.IsHalted = true;

    public override string ToString()
    => "END";
}

sealed class Jmp(Location source) : SourceOpCode(source)
{
    public override void Execute(CPU cpu)
    => cpu.PC += Source.GetValue(cpu) - 1; // -1 because PC has already been incremented
}

sealed class Jif(Location source) : SourceOpCode(source)
{
    public override void Execute(CPU cpu)
    => cpu.PC += cpu.LastComp ? Source.GetValue(cpu) - 1 : 0; // -1 because PC has already been incremented
}

sealed class Out(Location source) : SourceOpCode(source)
{
    public override void Execute(CPU cpu)
    => cpu.Output(Source.GetValue(cpu));
}

sealed class Add(Memory target, Location source) : TargetSourceOpCode(target, source)
{
    public override void Execute(CPU cpu)
    => cpu.Registers[Target.Location] += Source.GetValue(cpu);
}

sealed class Mod(Memory target, Location source) : TargetSourceOpCode(target, source)
{
    public override void Execute(CPU cpu)
    => cpu.Registers[Target.Location] %= Source.GetValue(cpu);
}

sealed class Div(Memory target, Location source) : TargetSourceOpCode(target, source)
{
    public override void Execute(CPU cpu)
    => cpu.Registers[Target.Location] /= Source.GetValue(cpu);
}

sealed class Mov(Memory target, Location source) : TargetSourceOpCode(target, source)
{
    public override void Execute(CPU cpu)
    => cpu.Registers[Target.Location] = Source.GetValue(cpu);
}

sealed class Ceq(Location source1, Location source2) : SourceSourceOpCode(source1, source2)
{
    public override void Execute(CPU cpu)
    => cpu.LastComp = Source1.GetValue(cpu) == Source2.GetValue(cpu);
}

sealed class Cge(Location source1, Location source2) : SourceSourceOpCode(source1, source2)
{
    public override void Execute(CPU cpu)
    => cpu.LastComp = Source1.GetValue(cpu) >= Source2.GetValue(cpu);
}
