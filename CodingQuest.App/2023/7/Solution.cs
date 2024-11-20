using System.Numerics;

namespace CQ_2023_7;

[Day(2023, 7, id:19, "Navigation sensor")]
sealed partial class Solution([Field(Type = typeof(ushort[]), AssignFormat = "Helpers.ParseToArray<ushort>({0})")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    int Run1()
    => (int)Math.Round(_input.Where(static i => BitOperations.PopCount(i) % 2 == 0)
        .Select(static i => (ushort)(i << 1) >> 1)
        .Average(), MidpointRounding.ToEven);
}
