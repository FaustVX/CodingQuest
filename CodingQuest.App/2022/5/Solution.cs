namespace CQ_2022_5;

[Day(2022, 5, id:4, "Obsessing over Connect 4")]
sealed partial class Solution([Field(Type = typeof(string[]), AssignFormat = """Helpers.ParseToArray<string>({0})""")]string input) : ISolution
{
    public string Run()
    => (Globals.IsTest ? Run1_test() : Run1()).ToString();

    int Run1_test()
    => PlayGame(_input[0]);

    int Run1()
    {
        var winnings = (stackalloc int[4]);
        foreach (var moves in _input)
            winnings[PlayGame(moves)]++;
        return winnings[1] * winnings[2] * winnings[3];
    }

    static int PlayGame(ReadOnlySpan<char> moves)
    {
        var board = new Span2D<byte>(stackalloc byte[7*7], 7);
        var currentPlayer = 1;
        var rounds = 1;
        foreach (var move in moves)
        {
            DropToken(move - '1', board, currentPlayer);
            if (CheckGame(board, (byte)currentPlayer))
                return currentPlayer;
            currentPlayer = (currentPlayer % 3) + 1;
            rounds++;
        }
        return 0;

        static void DropToken(int column, Span2D<byte> board, int player)
        {
            ref var pos = ref board[0, 0];
            for (var i = 0; i < 7; i++)
                if ((pos = ref board[column, i]) == 0)
                {
                    pos = (byte)player;
                    return;
                }
        }

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
                if (x > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[x + i, y] != player)
                        return false;
                return true;
            }

            static bool CheckColumn(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (y > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[x, y + i] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal1(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (x > 3 || y > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[x + i, y + i] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal2(int x, int y, ReadOnlySpan2D<byte> board, byte player)
            {
                if (x < 3 || y > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[x - i, y + i] != player)
                        return false;
                return true;
            }
        }
    }
}
