using System;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents an IPP octetString, which is a sequence of 8-bit octets.
/// See: RFC 8011 Section 5.1.10
/// </summary>
public readonly struct OctetString : IEquatable<OctetString>, INoValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OctetString"/> struct from a byte array.
    /// </summary>
    /// <param name="value">The byte array value.</param>
    public OctetString(byte[] value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OctetString"/> struct from a string, using UTF-8 encoding.
    /// </summary>
    /// <param name="value">The string value.</param>
    public OctetString(string value)
    {
        Value = Encoding.UTF8.GetBytes(value);
    }

    /// <summary>
    /// Gets the raw byte array value.
    /// </summary>
    public byte[] Value { get; }

    /// <summary>
    /// Gets a value indicating whether this instance represents a real value.
    /// </summary>
    public bool IsValue => Value != null;

    /// <summary>
    /// Implicitly converts a byte array to an <see cref="OctetString"/>.
    /// </summary>
    public static implicit operator OctetString(byte[] value) => new(value);

    /// <summary>
    /// Implicitly converts a string to an <see cref="OctetString"/> using UTF-8 encoding.
    /// </summary>
    public static implicit operator OctetString(string value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="OctetString"/> to a byte array.
    /// </summary>
    public static implicit operator byte[]?(OctetString octetString) => octetString.Value;

    /// <summary>
    /// Converts the octetString to a string using UTF-8 encoding.
    /// </summary>
    public override string ToString()
    {
        return Value == null ? string.Empty : Encoding.UTF8.GetString(Value);
    }

    /// <inheritdoc />
    public bool Equals(OctetString other)
    {
        if (Value == null || other.Value == null)
        {
            return Value == other.Value;
        }

        return Value.SequenceEqual(other.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is OctetString other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        if (Value == null)
        {
            return 0;
        }

        // Use a simple hash for the byte array
        var hash = 17;
        foreach (var b in Value.Take(Math.Min(Value.Length, 32)))
        {
            hash = hash * 31 + b;
        }

        return hash;
    }

    /// <summary>
    /// Compares two <see cref="OctetString"/> instances for equality.
    /// </summary>
    public static bool operator ==(OctetString left, OctetString right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="OctetString"/> instances for inequality.
    /// </summary>
    public static bool operator !=(OctetString left, OctetString right)
    {
        return !left.Equals(right);
    }
}
