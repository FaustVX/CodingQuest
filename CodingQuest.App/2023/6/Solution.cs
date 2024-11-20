using System.Diagnostics.CodeAnalysis;

namespace CQ_2023_6;

[Day(2023, 6, id:18, "Inventory check")]
sealed partial class Solution([Field(Type = typeof(Record[]), AssignFormat = "Helpers.ParseToArray<Record>({0})")]string input) : ISolution
{
    public string Run()
    => Run1().ToString();

    long Run1()
    => _input
        .AggregateBy(r => r.Category, 0, (acc, r) => acc + r.Quantity)
        .Aggregate(1L, (acc, g) => acc * (g.Value % 100));
}

record struct Record(Guid Guid, int Quantity, string Category) : IParsable<Record>
{
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Record result)
    => throw new NotImplementedException();

    static Record IParsable<Record>.Parse(string s, IFormatProvider? provider)
    {
        var ranges = (stackalloc Range[3]);
        s.AsSpan().Split(ranges, ' ');
        return new(Guid.Parse(s.AsSpan(ranges[0])), int.Parse(s.AsSpan(ranges[1])), string.Intern(s[ranges[2]]));
    }
}
