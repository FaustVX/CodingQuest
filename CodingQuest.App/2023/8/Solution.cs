namespace CQ_2023_8;

[Day(2023, 8, id:20, "Tic tac toe")]
sealed partial class Solution([Field(Type = typeof(int[,]), AssignFormat = """Helpers.ParseTo2DArray<int>({0}, width: 9, separator: " ")""")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    int Run1()
    {
        var winnings = (stackalloc int[4]);
        foreach (var moves in _input.AsSpan2D().EnumerateRows())
            winnings[PlayGame(moves)]++;
        return winnings[0] * winnings[1] * winnings[2];
    }

    static int PlayGame(ReadOnlySpan<int> moves)
    {
        var board = new Span2D<byte>(stackalloc byte[3*3], 3);
        var currentPlayer = 1;
        var rounds = 1;
        foreach (var move in moves)
        {
            board.TryGetSpan()[move - 1] = (byte)currentPlayer;
            if (CheckGame(board, (byte)currentPlayer))
                return currentPlayer;
            currentPlayer = (currentPlayer % 2) + 1;
            rounds++;
        }
        return 0;

        static bool CheckGame(ReadOnlySpan2D<byte> board, byte player)
        {
            ReadOnlySpan<Func<int, int, ReadOnlySpan2D<byte>, byte, bool>> conditions =
            [
                CheckLine,
                CheckColumn,
                CheckDiagonal1,
                CheckDiagonal2,
            ];

            for (int x = 0; x < board.Width; x++)
                for (int y = 0; y < board.Height; y++)
                    if (player == board[x, y] && conditions.Any(board, (condition, board) => condition(x, y, board, player)))
                        return true;
            return false;

            static bool CheckLine(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (x > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[x + i, y] != player)
                        return false;
                return true;
            }

            static bool CheckColumn(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (y > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[x, y + i] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal1(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (x > 0 || y > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[x + i, y + i] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal2(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (x < 2 || y > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[x - i, y + i] != player)
                        return false;
                return true;
            }
        }
    }
}
