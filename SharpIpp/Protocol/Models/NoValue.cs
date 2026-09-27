using System;

namespace SharpIpp.Protocol.Models;

public struct NoValue : IEquatable<NoValue>, INoValue
{
    public override string ToString() => "no value";

    public bool Equals(NoValue other) => true;

    public override bool Equals(object? obj) => obj is NoValue other && Equals(other);

    public override int GetHashCode() => 0;

    public static bool operator ==(NoValue left, NoValue right) => true;
    public static bool operator !=(NoValue left, NoValue right) => false;

    public static readonly NoValue Instance = new();

    bool INoValue.IsValue => false;
}
