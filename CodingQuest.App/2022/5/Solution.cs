using System.Diagnostics;

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
        var board = (stackalloc byte[7*7]);
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

        [DebuggerStepThrough]
        static int From2Dto1D(int x, int y)
        => y * 7 + x;

        static void DropToken(int column, Span<byte> board, int player)
        {
            ref var pos = ref board[0];
            for (var i = 0; i < 7; i++)
                if ((pos = ref board[From2Dto1D(column, i)]) == 0)
                {
                    pos = (byte)player;
                    return;
                }
        }

        static bool CheckGame(ReadOnlySpan<byte> board, byte player)
        {
            for (int x = 0; x < 7; x++)
                for (int y = 0; y < 7; y++)
                    if (player == board[From2Dto1D(x, y)] && (CheckLine(x, y, board, player) || CheckColumn(x, y, board, player) || CheckDiagonal1(x, y, board, player) || CheckDiagonal2(x, y, board, player)))
                        return true;
            return false;

            static bool CheckLine(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (x > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[From2Dto1D(x + i, y)] != player)
                        return false;
                return true;
            }

            static bool CheckColumn(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (y > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[From2Dto1D(x, y + i)] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal1(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (x > 3 || y > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[From2Dto1D(x + i, y + i)] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal2(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (x < 3 || y > 3)
                    return false;
                for (int i = 0; i < 4; i++)
                    if (board[From2Dto1D(x - i, y + i)] != player)
                        return false;
                return true;
            }
        }
    }
}
