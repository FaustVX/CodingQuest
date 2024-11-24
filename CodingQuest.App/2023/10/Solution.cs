using System.Diagnostics.CodeAnalysis;

namespace CQ_2023_10;

[Day(2023, 10, id:22, "Decoding pixels")]
sealed partial class Solution([Field(Type = typeof(Rectangle[]), AssignFormat = """Helpers.ParseToArray<Rectangle>({0})""")]string input) : ISolution
{
    static readonly Helpers.From2DTo1DHandler From2Dto1D = Helpers.From2DTo1D(50);

    public string Run()
    => Globals.IsTest ? RunTest() : Run1();

    string Run1()
    {
        var from2Dto1D = Helpers.From2DTo1D(50);
        var display = (stackalloc bool[50 * 10]);
        foreach (var (x, y) in _input.SelectMany(static r => r.GetPoints()))
            display[from2Dto1D(x, y)] ^= true;
        DrawBoard(display, 50);

        return Console.ReadLine()!; // can't use OCR because the font isn't monospace.
    }

    string RunTest()
    {
        var from2Dto1D = Helpers.From2DTo1D(8);
        var display = (stackalloc bool[8 * 8]);
        foreach (var (x, y) in _input.SelectMany(static r => r.GetPoints()))
            display[from2Dto1D(x, y)] ^= true;
        DrawBoard(display, 8);

        return Console.ReadLine()!; // can't use OCR because the font isn't monospace.
    }

    static void DrawBoard(ReadOnlySpan<bool> board, int width)
    {
        var from2Dto1D = Helpers.From2DTo1D(width);
        var height = board.Length / width;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
                Console.Write(board[from2Dto1D(x, y)] ? '#' : ' ');
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
