namespace CQ_2022_2;

[Day(2022, 2, id:1, "Engine diagnostics")]
sealed partial class Solution([Field(Type = typeof(int[]), AssignFormat = "Helpers.ParseToArray<int>({0})")]string input) : ISolution
{
    public int RunCount => 1;

    public string Run(int index)
    => Run1().ToString();

    public int Run1()
    {
        var min = 1500 * 60;
        var max = 1600 * 60;
        var countOOB = 0;
        var sum = _input.AsSpan(0, 60).Sum();
        if (sum < min || sum > max)
            countOOB++;
        for (var i = 0; i < _input.Length - 60; i++)
        {
            sum = sum - _input[i] + _input[i + 60];
            if (sum < min || sum > max)
                countOOB++;
        }
        return countOOB;
    }
}
