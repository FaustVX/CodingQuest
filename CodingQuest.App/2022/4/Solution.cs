using System.Diagnostics.CodeAnalysis;

namespace CQ_2022_4;

[Day(2022, 4, id:3, "Tour the stars")]
sealed partial class Solution([Field(Type = typeof(Point3D[]), AssignFormat = """Helpers.ParseToArray<Point3D>({0})""")]string input) : ISolution
{
    public int RunCount => 1;

    public string Run(int index, bool isTest)
    => Run1().ToString();

    public long Run1()
    {
        var distance = 0L;
        var previous = default(Point3D);
        for (int i = 1; i < _input.Length; i++)
        {
            var point = _input[i];
            (point, previous) = (point - previous, point);
            point *= point;
            distance += (long)Math.Sqrt(point.X + point.Y + point.Z);
        }
        return distance;
    }
}

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
readonly partial struct Point3D([Property]long x, [Property]long y, [Property]long z) : IParsable<Point3D>
{
    public static Point3D Parse(string s, IFormatProvider? provider)
    {
        var values = (stackalloc Range[3]);
        var span = s.AsSpan();
        span.Split(values, ' ');

        return new(long.Parse(span[values[0]]), long.Parse(span[values[1]]), long.Parse(span[values[2]]));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Point3D result)
    => throw new NotImplementedException();

    public static Point3D operator -(Point3D a, Point3D b)
    => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Point3D operator *(Point3D a, Point3D b)
    => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
}
