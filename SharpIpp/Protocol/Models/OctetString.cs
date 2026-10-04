using System;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents an IPP octetString, which is a sequence of 8-bit octets.
/// See: RFC 8011 Section 5.1.10
/// Implements <see cref="INoValue"/> so that <c>default(OctetString)</c> represents the IPP NoValue state.
/// </summary>
/// <remarks>
/// <para>Three-state semantics when used as <c>OctetString?</c>:</para>
/// <list type="bullet">
///   <item><c>null</c> — attribute not present in the IPP message</item>
///   <item><c>default(OctetString)</c> (<see cref="IsValue"/> = false) — attribute present with Tag.NoValue</item>
///   <item>Constructed instance (<see cref="IsValue"/> = true) — attribute has a concrete value</item>
/// </list>
/// </remarks>
public readonly struct OctetString(byte[] value) : IEquatable<OctetString>, IEquatable<byte[]>, IEquatable<string>, IEquatable<NoValue>, INoValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OctetString"/> struct from a string, using UTF-8 encoding.
    /// </summary>
    /// <param name="value">The string value.</param>
    public OctetString(string value) : this(Encoding.UTF8.GetBytes(value))
    {
    }

    /// <summary>
    /// Gets the raw byte array value, or null if in the NoValue state or initialized with null.
    /// </summary>
    public byte[]? Value { get; } = value;

    /// <summary>
    /// Indicates whether this instance represents a concrete value.
    /// Returns false only for <c>default(OctetString)</c>, which represents IPP Tag.NoValue.
    /// </summary>
    public bool IsValue { get; } = true;

    /// <summary>
    /// Implicitly converts a byte array to an <see cref="OctetString"/>.
    /// A <c>null</c> byte array produces <c>default(OctetString)</c> (NoValue state).
    /// </summary>
    public static implicit operator OctetString(byte[]? value) =>
        value == null ? default : new(value);

    /// <summary>
    /// Implicitly converts a string to an <see cref="OctetString"/> using UTF-8 encoding.
    /// A <c>null</c> string produces <c>default(OctetString)</c> (NoValue state).
    /// </summary>
    public static implicit operator OctetString(string? value) =>
        value == null ? default : new(value);

    /// <summary>
    /// Implicitly converts <see cref="NoValue"/> to an <see cref="OctetString"/> in the NoValue state.
    /// </summary>
    public static implicit operator OctetString(NoValue _) => default;

    /// <summary>
    /// Implicitly converts an <see cref="OctetString"/> to a byte array.
    /// Returns <c>null</c> if the instance is in the NoValue state.
    /// </summary>
    public static implicit operator byte[]?(OctetString octetString) =>
        octetString.IsValue ? octetString.Value : null;

    /// <summary>
    /// Explicitly converts an <see cref="OctetString"/> to a string using UTF-8 encoding.
    /// Returns <c>null</c> if the instance is in the NoValue state.
    /// </summary>
    public static explicit operator string?(OctetString octetString) =>
        octetString.IsValue ? octetString.ToString() : null;

    /// <summary>
    /// Converts the octetString to a string using UTF-8 encoding.
    /// Returns "no value" if in the NoValue state.
    /// </summary>
    public override string ToString()
    {
        if (!IsValue) return "no value";
        return Value == null ? string.Empty : Encoding.UTF8.GetString(Value);
    }

    public bool Equals(OctetString other)
    {
        if (IsValue != other.IsValue) return false;
        if (!IsValue) return true;
        if (Value == null || other.Value == null)
            return Value == other.Value;
        return Value.SequenceEqual(other.Value);
    }

    public bool Equals(byte[]? other)
    {
        if (!IsValue) return other == null;
        if (Value == null || other == null) return Value == other;
        return Value.SequenceEqual(other);
    }

    public bool Equals(string? other)
    {
        if (!IsValue) return other == null;
        if (other == null) return false;
        return ToString() == other;
    }

    public bool Equals(NoValue other) => !IsValue;

    public override bool Equals(object? obj)
    {
        if (obj is OctetString other) return Equals(other);
        if (obj is byte[] bytes) return Equals(bytes);
        if (obj is string str) return Equals(str);
        if (obj is INoValue noVal) return !noVal.IsValue && !IsValue;
        return false;
    }

    public override int GetHashCode()
    {
        if (!IsValue || Value == null) return 0;
        var hash = 17;
        foreach (var b in Value.Take(Math.Min(Value.Length, 32)))
        {
            hash = hash * 31 + b;
        }
        return hash;
    }

    public static bool operator ==(OctetString left, OctetString right) => left.Equals(right);
    public static bool operator !=(OctetString left, OctetString right) => !left.Equals(right);
    public static bool operator ==(OctetString left, NoValue right) => !left.IsValue;
    public static bool operator !=(OctetString left, NoValue right) => left.IsValue;
    public static bool operator ==(NoValue left, OctetString right) => !right.IsValue;
    public static bool operator !=(NoValue left, OctetString right) => right.IsValue;
}
