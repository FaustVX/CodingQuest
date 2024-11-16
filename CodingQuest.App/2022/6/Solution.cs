using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace CQ_2022_6;

[Day(2022, 6, id:5, "Spot the forgery")]
sealed partial class Solution([Field(Type = typeof(Record[]), AssignFormat = """Helpers.ParseToArray<Record>({0})""")]string input) : ISolution
{
    public int RunCount => 1;

    public string Run(int index, bool isTest)
    => Run1().ToString("x64");

    public BigInteger Run1()
    {
        var modified = false;
        foreach (ref var record in _input.AsSpan())
        {
            if (modified || record.ComputeHash() != record.ComputedHash)
            {
                modified = true;
                record = record with { PreviousHash = Unsafe.Subtract(ref record, 1).ComputedHash };
                for (int n = 0; n < int.MaxValue - 1; n++)
                {
                    record = record with { Number = n };
                    var hash = record.ComputeHash();
                    if (Record.IsAcceptableHash(hash))
                    {
                        record = record with { ComputedHash = hash };
                        break;
                    }
                }
            }
        }
        return _input[^1].ComputedHash;
    }
}

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
readonly partial struct Record([Property] string description, [Property] int number, [Property] BigInteger previousHash, [Property] BigInteger computedHash) : IParsable<Record>
{
    private static readonly BigInteger MaxHash = BigInteger.Parse("000000" + new string('f', 64 - 6), NumberStyles.HexNumber);
    public static Record Parse(string s, IFormatProvider? provider)
    {
        var span = s.AsSpan();
        var pipe = s.IndexOf('|');

        return new(s[0..pipe++], int.Parse(span[pipe++..s.NextIndexOf('|', ref pipe)]), BigInteger.Parse(span[++pipe..s.NextIndexOf('|', ref pipe)], NumberStyles.HexNumber), BigInteger.Parse(span[++pipe..], NumberStyles.HexNumber));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Record result)
    => throw new NotImplementedException();

    public static bool IsAcceptableHash(BigInteger hash)
    => hash <= MaxHash;

    public readonly string DataToHash
    => new StringBuilder()
        .Append(Description)
        .Append('|')
        .Append(Number)
        .Append('|')
        .Append(PreviousHash.ToString("x64"))
        .ToString();

    public readonly BigInteger ComputeHash()
    {
        var hash = (stackalloc byte[32]);
        SHA256.HashData(Encoding.ASCII.GetBytes(DataToHash), hash);
        return new(hash, isUnsigned: true, isBigEndian: true);
    }
}
