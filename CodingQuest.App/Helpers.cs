using System.Runtime.CompilerServices;

namespace CodingQuest;

public static class Helpers
{
    public static T[] ParseToArray<T>(string input)
    where T : IParsable<T>
    => input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
        .Select(static i => T.Parse(i, null))
        .ToArray();

    public static T[,] ParseTo2DArray<T>(string input, int width, string separator)
    where T : IParsable<T>
    {
        var split = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var array = new T[split.Length, width];
        for (int y = 0; y < split.Length; y++)
        {
            var line = split[y].Split(separator);
            for (int x = 0; x < width; x++)
                array[y, x] = T.Parse(line[x], null);
        }
        return array;
    }

    public static T[,] ParseTo2DArrayClass<T>(string input, int width, Action<ReadOnlySpan<char>, Span<T>> parser)
    where T : class
    {
        var p = (stackalloc nint[width]);
        var parsed = Unsafe.As<Span<nint>, Span<T>>(ref p);
        return ParseTo2DArray(input, width, parsed, parser);
    }

    public static T[,] ParseTo2DArrayStruct<T>(string input, int width, Action<ReadOnlySpan<char>, Span<T>> parser)
    where T : unmanaged
    => ParseTo2DArray(input, width, (stackalloc T[width]), parser);

    private static T[,] ParseTo2DArray<T>(string input, int width, Span<T> parsed, Action<ReadOnlySpan<char>, Span<T>> parser)
    {
        var split = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var array = new T[width, split.Length];
        for (int y = 0; y < split.Length; y++)
        {
            parsed.Clear();
            parser(split[y], parsed);
            for (int x = 0; x < parsed.Length; x++)
                array[x, y] = parsed[x];
        }
        return array;
    }
}
