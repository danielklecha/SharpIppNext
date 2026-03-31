using System;

namespace SharpIpp.Protocol.Models;

public readonly struct Range(int lower, int upper, bool isValue = true) : IEquatable<Range>, INoValue
{
    public int Lower { get; } = lower;
    public int Upper { get; } = upper;
    public bool IsValue { get; } = isValue;

    public override string ToString() => $"{Lower} - {Upper}";

    public void Deconstruct(out int lower, out int upper)
    {
        lower = Lower;
        upper = Upper;
    }

    public bool Equals(Range other) => Lower == other.Lower && Upper == other.Upper;

    public override bool Equals(object? obj) => obj is Range other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            return (Lower * 397) ^ Upper;
        }
    }

    public static bool operator ==(Range left, Range right) => left.Equals(right);

    public static bool operator !=(Range left, Range right) => !left.Equals(right);
}
