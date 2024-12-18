using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CQ_2023_14;

[Day(2023, 14, id:24, "Snakes on a spaceship")]
sealed partial class Solution([Field(Type = typeof(Input), AssignFormat = """Input.Parse({0}, null)""")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    int Run1()
    {
        var snake = new LinkedList<Point>();
        ref var fruit = ref _input.Fruits[0];
        var points = 0;
        snake.AddFirst(new Point(0, 0));
        foreach (var move in _input.Moves)
        {
            if (!AddNewPos(snake, snake.First!.Value.Move(move)))
                break;
            points++;
            if (snake.First!.Value == fruit)
            {
                points += 100;
                fruit = ref Unsafe.Add(ref fruit, 1);
            }
            else
                snake.RemoveLast();
        }
        return points;

        static bool AddNewPos(LinkedList<Point> snake, Point head)
        {
            if ((uint)head.X >= (Globals.IsTest ? 8 : 20) || (uint)head.Y >= (Globals.IsTest ? 8 : 20))
                return false;
            foreach (var body in snake)
                if (body == head)
                    return false;
            snake.AddFirst(head);
            return true;
        }
    }
}

readonly partial record struct Input(Point[] Fruits, Move[] Moves) : IParsable<Input>
{
    public static Input Parse(string s, IFormatProvider? provider)
    {
        var values = (stackalloc Range[4]);
        var span = s.AsSpan();
        span.Split(values, Environment.NewLine, (StringSplitOptions)3);

        var moves = new Move[span[values[3]].Length];
        for (int i = 0; i < moves.Length; i++)
            moves[i] = span[values[3]][i] switch
            {
                'U' => Move.Up,
                'D' => Move.Down,
                'L' => Move.Left,
                'R' => Move.Right,
                _ => throw new UnreachableException(),
            };

        return new(Helpers.ParseToArray<Point>(s[values[1]], ' '), moves);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Input result)
    => throw new NotImplementedException();
}

readonly record struct Point(int X, int Y) : IParsable<Point>
{
    public static Point Parse(string s, IFormatProvider? provider)
    {
        var values = (stackalloc Range[2]);
        var span = s.AsSpan();
        span.Split(values, ',');

        return new(int.Parse(span[values[0]]), int.Parse(span[values[1]]));
    }

    public readonly Point Move(Move move)
    {
        var x = X + (move switch
        {
            CQ_2023_14.Move.Left => -1,
            CQ_2023_14.Move.Right => 1,
            _ => 0,
        });
        var y = Y + (move switch
        {
            CQ_2023_14.Move.Up => -1,
            CQ_2023_14.Move.Down => 1,
            _ => 0,
        });

        return new(x, y);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Point result)
    => throw new NotImplementedException();
}

enum Move
{
    Up,
    Down,
    Left,
    Right,
}
