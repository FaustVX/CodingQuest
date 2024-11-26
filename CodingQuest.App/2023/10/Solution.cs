using System.Diagnostics.CodeAnalysis;

namespace CQ_2023_10;

[Day(2023, 10, id:22, "Decoding pixels")]
sealed partial class Solution([Field(Type = typeof(Rectangle[]), AssignFormat = """Helpers.ParseToArray<Rectangle>({0})""")]string input) : ISolution
{
    public string Run()
    {
        var (w, h) = Globals.IsTest ? (8, 8) : (50, 10);
        return Run1(w, h);
    }

    string Run1(int width, int height)
    {
        var display = new Span2D<bool>(stackalloc bool[width * height], width);
        foreach (var (x, y) in _input.SelectMany(static r => r.GetPoints()))
            display[x, y] ^= true;
        DrawBoard(display);

        return Console.ReadLine()!; // can't use OCR because the font isn't monospace.
    }

    static void DrawBoard(ReadOnlySpan2D<bool> board)
    {
        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
                Console.Write(board[x, y] ? '#' : ' ');
            Console.WriteLine();
        }
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

    public IEnumerable<(int x, int y)> GetPoints()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                yield return (X + x, Y + y);
    }
}
