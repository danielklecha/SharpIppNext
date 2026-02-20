using System;
using System.Numerics;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace SharpIpp.Protocol.Models;

public readonly struct IppAttribute : IEquatable<IppAttribute>
{
    public IppAttribute()
    {
        Tag = Tag.NoValue;
        Name = string.Empty;
        Value = NoValue.Instance;
    }

    internal IppAttribute(Tag tag, string name, object value)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        switch (value)
        {
            case int integer when integer == int.MinValue:
            case DateTimeOffset dateTime when dateTime == default:
            case Range range when range.Equals(default):
            case Resolution resolution when resolution.Equals(default):
            case StringWithLanguage stringWithLanguage when stringWithLanguage.Equals(default):
            case string stringValue when stringValue == string.Empty && tag == Tag.Keyword:
                tag = Tag.NoValue;
                value = NoValue.Instance;
                break;
        }
        Tag = tag;
        Name = name;
        Value = value;
    }

    public IppAttribute(Tag tag, string name, int value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, bool value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, string value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, DateTimeOffset value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, NoValue value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, Range value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, Resolution value) : this(tag, name, value as object)
    {
    }

    public IppAttribute(Tag tag, string name, StringWithLanguage value) : this(tag, name, value as object)
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
        return Tag == other.Tag && Name == other.Name && Equals(Value, other.Value);
    }

    public override string ToString()
    {
        return $"({Tag}) {Name}: {Value}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not IppAttribute other)
        {
            return false;
        }

        return Equals(other);
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
