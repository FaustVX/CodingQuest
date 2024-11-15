// See https://aka.ms/new-console-template for more information
namespace CodingQuest;

public static class Helpers
{
    public static int[] ParseToIntArray(string input)
    => input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();
    public static int[,] ParseToInt2DArray(string input, int width, string separator)
    {
        var split = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var array = new int[split.Length, width];
        for (int y = 0; y < split.Length; y++)
        {
            var line = split[y].Split(separator);
            for (int x = 0; x < width; x++)
                array[y, x] = int.Parse(line[x]);
        }
        return array;
    }
}
