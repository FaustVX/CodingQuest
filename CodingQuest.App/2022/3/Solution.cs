namespace CQ_2022_3;

[Day(2022, 3, id:2, "Lottery tickets")]
sealed partial class Solution([Field(Type = typeof(int[,]), AssignFormat = """Helpers.ParseTo2DArray<int>({0}, width:6, separator:" ")""")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    int Run1()
    {
        var winning = (stackalloc int[] {12, 48, 30, 95, 15, 55, 97});
        var gain = (stackalloc int[6 + 1] {0, 0, 0, 1, 10, 100, 1000});
        var length = _input.GetLength(0);
        var sum = 0;
        for (int y = 0; y < length; y++)
        {
            var winningNumbers = 0;
            for (int x = 0; x < 6; x++)
                if (winning.Contains(_input[y, x]))
                    winningNumbers++;
            sum += gain[winningNumbers];
        }
        return sum;
    }
}
