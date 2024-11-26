using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace CQ_2023_9;

[Day(2023, 9, id:21, "Mayday!")]
sealed partial class Solution([Field(Type = typeof(Message[]), AssignFormat = """Helpers.ParseToArray<Message>({0})""")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    string Run1()
    => _input
            .Where(static m => m.IsChecksumValid)
            .OrderBy(static m => m.Sequence)
            .Aggregate(new StringBuilder(), (sb, m) => sb.Append(m.String))
            .ToString().TrimEnd();
}

readonly record struct Message(short Header, int Sender, byte Sequence, byte Checksum, byte[] Data) : IParsable<Message>
{
    public static Message Parse(string s, IFormatProvider? provider)
    {
        var span = s.AsSpan();
        var offset = 0;
        return new(
            short.Parse(span[offset .. (offset += 4)], System.Globalization.NumberStyles.HexNumber),
            int.Parse(span[offset .. (offset += 8)], System.Globalization.NumberStyles.HexNumber),
            byte.Parse(span[offset .. (offset += 2)], System.Globalization.NumberStyles.HexNumber),
            byte.Parse(span[offset .. (offset += 2)], System.Globalization.NumberStyles.HexNumber),
            BigInteger.Parse(span[offset ..], System.Globalization.NumberStyles.HexNumber).ToByteArray(isBigEndian: true));
    }

    public bool IsChecksumValid
    {
        get
        {
            byte sum = 0;
            foreach (var item in Data)
                sum += item;
            return sum == Checksum;
        }
    }

    public string String => Encoding.ASCII.GetString(Data);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Message result)
    => throw new NotImplementedException();
}
