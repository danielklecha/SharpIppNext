using System;

namespace SharpIpp.Protocol.Models;

public struct Range : IEquatable<Range>
{
    public int Lower { get; }

    public int Upper { get; }

    public Range(int lower, int upper)
    {
        Lower = lower;
        Upper = upper;
    }

    public override string ToString()
    {
        return $"{Lower} - {Upper}";
    }

    public bool Equals(Range other)
    {
        return Lower == other.Lower && Upper == other.Upper;
    }

    public override bool Equals(object obj)
    {
        return obj is Range other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Lower * 397) ^ Upper;
        }
    }
}
