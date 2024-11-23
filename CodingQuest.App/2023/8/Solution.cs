using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CQ_2023_8;

[Day(2023, 8, id:20, "Tic tac toe")]
sealed partial class Solution([Field(Type = typeof(int[,]), AssignFormat = """Helpers.ParseTo2DArray<int>({0}, width: 9, separator: " ")""")]string input) : ISolution
{
    static readonly Helpers.From2DTo1DHandler From2Dto1D = Helpers.From2DTo1D(3);

    public string Run()
    => Run1().ToString();

    int Run1()
    {
        var winnings = (stackalloc int[4]);
        foreach (var moves in new RowEnumerator<int>(_input, 9))
            winnings[PlayGame(moves)]++;
        return winnings[0] * winnings[1] * winnings[2];
    }

    static int PlayGame(ReadOnlySpan<int> moves)
    {
        var board = (stackalloc byte[3*3]);
        var currentPlayer = 1;
        var rounds = 1;
        foreach (var move in moves)
        {
            board[move - 1] = (byte)currentPlayer;
            if (CheckGame(board, (byte)currentPlayer))
                return currentPlayer;
            currentPlayer = (currentPlayer % 2) + 1;
            rounds++;
        }
        return 0;

        static bool CheckGame(ReadOnlySpan<byte> board, byte player)
        {
            ReadOnlySpan<Func<int, int, ReadOnlySpan<byte>, byte, bool>> conditions =
            [
                CheckLine,
                CheckColumn,
                CheckDiagonal1,
                CheckDiagonal2,
            ];

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    if (player == board[From2Dto1D(x, y)] && conditions.Any(board, (condition, board) => condition(x, y, board, player)))
                        return true;
            return false;

            static bool CheckLine(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (x > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[From2Dto1D(x + i, y)] != player)
                        return false;
                return true;
            }

            static bool CheckColumn(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (y > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[From2Dto1D(x, y + i)] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal1(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (x > 0 || y > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[From2Dto1D(x + i, y + i)] != player)
                        return false;
                return true;
            }

            static bool CheckDiagonal2(int x, int y, ReadOnlySpan<byte> board, byte player)
            {
                if (x < 2 || y > 0)
                    return false;
                for (int i = 0; i < 3; i++)
                    if (board[From2Dto1D(x - i, y + i)] != player)
                        return false;
                return true;
            }
        }
    }
}

[StructLayout(LayoutKind.Auto)]
ref partial struct RowEnumerator<T>([Field] T[,] span, [Field] int width)
{
    public int _row;
    private readonly int _height = span.Length / width;
    public readonly ReadOnlySpan<T> Current => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in _span[_row, 0]), _width);
    public bool MoveNext()
    => ++_row < _height;
    public readonly RowEnumerator<T> GetEnumerator() => this with { _row = -1 };
}
