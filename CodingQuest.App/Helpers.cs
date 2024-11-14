// See https://aka.ms/new-console-template for more information
namespace CodingQuest;

public static class Helpers
{
    public static int[] ParseToIntArray(string input)
    => input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();
}
