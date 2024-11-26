using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CodingQuest;

[StructLayout(LayoutKind.Auto)]
public ref partial struct RowEnumerator<T>([Field] ReadOnlySpan2D<T> span)
{
    public int _row;
    public readonly ReadOnlySpan<T> Current => _span.GetRow(_row);
    public bool MoveNext()
    => ++_row < _span.Height;
    public readonly RowEnumerator<T> GetEnumerator() => this with { _row = -1 };
}

[StructLayout(LayoutKind.Auto)]
public readonly ref partial struct Span2D<T>([Field]Span<T> span, [Property(Setter = "")]int width)
{
    private readonly Helpers.From2DTo1DHandler _from2DTo1D = Cache.GetOrCreate(width);
    public readonly int Height { get; } = span.Length / width;
    public readonly ref T this[Index x, Index y]
    => ref _span[_from2DTo1D(x.GetOffset(Width), y.GetOffset(Height))];
    public static implicit operator Span2D<T>(T[,] span)
    => new(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in span[0, 0]), span.Length), span.GetLength(1));
    public static implicit operator ReadOnlySpan2D<T>(Span2D<T> span)
    => new(span._span, span.Width);
    public readonly Span<T> GetRow(Index row) => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this[0, row]), Width);
    public readonly RowEnumerator<T> EnumerateRows() => new(this);
    public readonly Span<T> TryGetSpan() => _span;
}

[StructLayout(LayoutKind.Auto)]
public readonly ref partial struct ReadOnlySpan2D<T>([Field]ReadOnlySpan<T> span, [Property(Setter = "")]int width)
{
    private readonly Helpers.From2DTo1DHandler _from2DTo1D = Cache.GetOrCreate(width);
    public readonly int Height { get; } = span.Length / width;
    public readonly ref readonly T this[Index x, Index y]
    => ref _span[_from2DTo1D(x.GetOffset(Width), y.GetOffset(Height))];
    public static implicit operator ReadOnlySpan2D<T>(T[,] span)
    => new(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in span[0, 0]), span.Length), span.GetLength(1));
    public readonly ReadOnlySpan<T> GetRow(Index row) => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this[0, row]), Width);
    public readonly RowEnumerator<T> EnumerateRows() => new(this);
    public readonly ReadOnlySpan<T> TryGetSpan() => _span;
}

file static class Cache
{
    private static readonly Dictionary<int, Helpers.From2DTo1DHandler> From2DTo1D = [];
    public static Helpers.From2DTo1DHandler GetOrCreate(int width)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(From2DTo1D, width, out var exists);
        if (!exists)
            value = Helpers.From2DTo1D(width);
        return value!;
    }
}
