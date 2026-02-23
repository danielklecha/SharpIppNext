using System;

namespace SharpIpp.Protocol.Models;

public readonly struct StringWithLanguage : IEquatable<StringWithLanguage>
{
    public string Language { get; }

    public string Value { get; }

    public StringWithLanguage(string language, string value)
    {
        Language = language;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value} ({Language})";
    }

    public bool Equals(StringWithLanguage other)
    {
        return Language == other.Language && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is StringWithLanguage other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((Language != null ? Language.GetHashCode() : 0) * 397) ^
                   (Value != null ? Value.GetHashCode() : 0);
        }
    }

    public static bool operator ==(StringWithLanguage left, StringWithLanguage right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(StringWithLanguage left, StringWithLanguage right)
    {
        return !left.Equals(right);
    }
}
