using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CQ_2023_13;

[Day(2023, 13, id:23, "Avoid the asteroids")]
sealed partial class Solution([Field(Type = typeof(Asteroid[]), AssignFormat = """Helpers.ParseToArray<Asteroid>({0})""")]string input) : ISolution
{
    public string Run()
    => Run1();

    string Run1()
    {
        var space = new Span2D<bool>(stackalloc bool[100 * 100], 100);
        MoveStep(_input, space, 3600);

        for (int i = 0; i < 60; i++)
            MoveStep(_input, space);

        var passage = FindPassage(space);
        return $"{passage.x}:{passage.y}";

        static void MoveStep(Span<Asteroid> asteroids, Span2D<bool> space, int steps = 1)
        {
            foreach (ref var a in asteroids)
                if ((a = a with { X = checked((short)(a.X + steps * a.HSpeed)), Y = checked((short)(a.Y + steps * a.VSpeed)) }).IsValid)
                    space[a.X, a.Y] = true;
        }

        static (int x, int y) FindPassage(ReadOnlySpan2D<bool> space)
        {
            for (int y = 0; y < space.Height; y++)
                for (int x = 0; x < space.Width; x++)
                    if (!space[x, y])
                        return (x, y);
            throw new UnreachableException();
        }
    }
}

readonly partial record struct Asteroid(short X, short Y, short HSpeed, short VSpeed) : IParsable<Asteroid>
{
    public static Asteroid Parse(string s, IFormatProvider? provider)
    {
        var values = (stackalloc Range[4]);
        var span = s.AsSpan();
        span.Split(values, ' ');

        return new(short.Parse(span[values[0]]), short.Parse(span[values[1]]), short.Parse(span[values[2]]), short.Parse(span[values[3]]));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Asteroid result)
    => throw new NotImplementedException();

    public readonly bool IsValid
    => X >= 0 && X < 100 && Y >= 0 && Y < 100;
}
