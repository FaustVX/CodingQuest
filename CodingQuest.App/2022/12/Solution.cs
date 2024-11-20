using System.Collections.Immutable;
using System.Diagnostics;

namespace CQ_2022_12;

[Day(2022, 12, id:9, "Lost in an alien market!")]
sealed partial class Solution([Field(Type = typeof(bool[,]), AssignFormat = """"Helpers.ParseTo2DArrayStruct<bool>({0}, width:Globals.IsTest ? 21 : 401, parser: Parser)"""")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    int Run1()
    => Traverse(FindEntrance(_input, 0), FindEntrance(_input, ^1), []).Length;

    ImmutableArray<(int x, int y)> Traverse((int x,int y) pos, (int x, int y) end, ImmutableArray<(int x, int y)> path)
    {
        if (pos == end)
            return path.Add(pos);
        if (!pos.IsContainedIn(_input) || !_input[pos.x, pos.y])
            return [];
        if (!path.Contains(pos.Down()) && (Traverse(pos.Down(), end, path.Add(pos)) is not [] and var down))
            return down;
        if (!path.Contains(pos.Left()) && (Traverse(pos.Left(), end, path.Add(pos)) is not [] and var left))
            return left;
        if (!path.Contains(pos.Right()) && (Traverse(pos.Right(), end, path.Add(pos)) is not [] and var right))
            return right;
        if (!path.Contains(pos.Up()) && (Traverse(pos.Up(), end, path.Add(pos)) is not [] and var up))
            return up;
        return [];
    }

    static (int, int) FindEntrance(bool[,] array, Index line)
    {
        var y = line.GetOffset(array.GetLength(1));
        for (int x = 0; x < array.GetLength(0); x++)
            if (array[x, y])
                return (x, y);
        throw new UnreachableException();
    }

    static void Parser(ReadOnlySpan<char> input, Span<bool> output)
    {
        for (int x = 0; x < output.Length; x++)
            output[x] = input[x] == ' ';
    }
}
