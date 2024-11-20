using System.Diagnostics.CodeAnalysis;

namespace CQ_2022_10;

[Day(2022, 10, id:7, "Check the heat shields")]
sealed partial class Solution([Field(Type = typeof(Rectangle[]), AssignFormat = """Helpers.ParseToArray<Rectangle>({0})""")]string input) : ISolution
{
    public string Run()
    {
        var (width, height) = Globals.InputFile[^7] switch
        {
            '1' => (10, 10),
            '2' => (100, 100),
            _ => (20_000, 100_000),
        };
        return Run1(width, height).ToString();
    }

    int Run1(int width, int height)
    {
        var total = width * height;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (_input.Any(r => r.IsPointIn(x, y)))
                    total--;
        return total;
    }
}

readonly partial record struct Rectangle(int X, int Y, int Width, int Height) : IParsable<Rectangle>
{
    public static Rectangle Parse(string s, IFormatProvider? provider)
    {
        var values = (stackalloc Range[4]);
        var span = s.AsSpan();
        span.Split(values, ' ');

        return new(int.Parse(span[values[0]]), int.Parse(span[values[1]]), int.Parse(span[values[2]]), int.Parse(span[values[3]]));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Rectangle result)
    => throw new NotImplementedException();

    public bool IsPointIn(int x, int y)
    => x >= X && x - X < Width && y >= Y && y - Y < Height;
}
