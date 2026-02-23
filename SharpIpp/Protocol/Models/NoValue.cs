using System;

namespace SharpIpp.Protocol.Models;

public struct NoValue : IEquatable<NoValue>
{
    public override string ToString()
    {
        return "no value";
    }

    public bool Equals(NoValue other)
    {
        return true;
    }

    public override bool Equals(object obj)
    {
        return obj is NoValue other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public static NoValue Instance = new NoValue();
}
