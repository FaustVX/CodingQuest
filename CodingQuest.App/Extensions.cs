// See https://aka.ms/new-console-template for more information
using System.Numerics;

namespace CodingQuest;

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
}
