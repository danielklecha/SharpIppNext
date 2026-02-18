using System;
using System.Numerics;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace SharpIpp.Protocol.Models;

public class IppAttribute : IEquatable<IppAttribute>
{
    internal IppAttribute(Tag tag, string name, object value)
    {
        Tag = tag;
        Name = name;
        Value = value;
    }

    public IppAttribute(Tag tag, string name, int value) : this(tag, name, (object)value)
    {
    }

    public static IppAttribute CreateInt(string name, int value) => value == int.MinValue ? CreateNoValue(name) : new (Tag.Integer, name, value);

    public static IppAttribute CreateEnum(string name, int value) => value == int.MinValue ? CreateNoValue(name) : new(Tag.Enum, name, value);

    public IppAttribute(Tag tag, string name, bool value) : this(tag, name, (object)value)
    {
    }

    public static IppAttribute CreateBoolean(string name, bool value) => new(Tag.Boolean, name, value);

    public IppAttribute(Tag tag, string name, string value) : this(tag, name, (object)value)
    {
    }

    public IppAttribute(Tag tag, string name, DateTimeOffset value) : this(tag, name, (object)value)
    {
    }

    public static IppAttribute CreateDateTime(string name, DateTimeOffset value) => value == DateTimeOffset.MinValue ? CreateNoValue(name) : new (Tag.DateTime, name, value);

    public IppAttribute(Tag tag, string name, NoValue value) : this(tag, name, (object)value)
    {
    }

    public static IppAttribute CreateNoValue(string name) => new(Tag.NoValue, name, NoValue.Instance);

    public IppAttribute(Tag tag, string name, Range value) : this(tag, name, (object)value)
    {
    }

    public IppAttribute(Tag tag, string name, Resolution value) : this(tag, name, (object)value)
    {
    }

    public IppAttribute(Tag tag, string name, StringWithLanguage value) : this(tag, name, (object)value)
    {
    }

    public Tag Tag { get; }

    public string Name { get; }

    /// <summary>
    ///     Possible values:
    ///     <see cref="int"/>
    ///     <see cref="bool"/>
    ///     <see cref="string" />
    ///     <see cref="DateTimeOffset" />
    ///     <see cref="NoValue" />
    ///     <see cref="Range" />
    ///     <see cref="Resolution" />
    ///     <see cref="StringWithLanguage" />
    /// </summary>
    public object Value { get; }

    public bool Equals(IppAttribute other)
    {
        if (null == other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Tag == other.Tag && Name == other.Name && Equals(Value, other.Value);
    }

    public override string ToString()
    {
        return $"({Tag}) {Name}: {Value}";
    }

    public override bool Equals(object obj)
    {
        if (null == obj)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((IppAttribute)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (int)Tag;
            hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            return hashCode;
        }
    }
}
