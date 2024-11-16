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
}
