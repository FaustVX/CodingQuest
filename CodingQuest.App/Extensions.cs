using System.Diagnostics;
using System.Numerics;

namespace CodingQuest;

[DebuggerStepThrough]
public static class Extensions
{
    public static T Sum<T>(this ReadOnlySpan<T> values)
    where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        T sum = T.AdditiveIdentity;
        foreach (var item in values)
            sum += item;
        return sum;
    }

    public static T Sum<T>(this Span<T> values)
    where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    => Sum((ReadOnlySpan<T>)values);

    public static int NextIndexOf(this string span, char separator, ref int index)
    => index = span.IndexOf(separator, index + 1);

    public static (int x, int y) Up(this (int x, int y) pos)
    => (pos.x, pos.y - 1);

    public static (int x, int y) Down(this (int x, int y) pos)
    => (pos.x, pos.y + 1);

    public static (int x, int y) Left(this (int x, int y) pos)
    => (pos.x - 1, pos.y);

    public static (int x, int y) Right(this (int x, int y) pos)
    => (pos.x + 1, pos.y);

    public static bool IsContainedIn<T>(this (int x, int y) pos, T[,] array)
    => pos.x >= 0 && pos.y >= 0 && pos.x < array.GetLength(0) && pos.y < array.GetLength(1);

    public static bool Any<TSource, TObject>(this ReadOnlySpan<TSource> source, TObject @object, Func<TSource, TObject, bool> predicate)
    where TObject : allows ref struct
    {
        if (predicate is null)
            ArgumentNullException.ThrowIfNull(predicate);

        foreach (TSource element in source)
            if (predicate(element, @object))
                return true;

        return false;
    }

    public static Span2D<T> AsSpan2D<T>(this T[,] array) => array;
}
