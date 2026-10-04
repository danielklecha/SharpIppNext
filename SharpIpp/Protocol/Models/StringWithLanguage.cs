using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents an IPP text or name value that may optionally carry a language tag.
/// Implements <see cref="INoValue"/> so that <c>default(StringWithLanguage)</c> represents the IPP NoValue state.
/// </summary>
/// <remarks>
/// <para>Three-state semantics when used as <c>StringWithLanguage?</c>:</para>
/// <list type="bullet">
///   <item><c>null</c> — attribute not present in the IPP message</item>
///   <item><c>default(StringWithLanguage)</c> (<see cref="IsValue"/> = false) — attribute present with Tag.NoValue</item>
///   <item>Constructed instance (<see cref="IsValue"/> = true) — attribute has a concrete value</item>
/// </list>
/// </remarks>
public readonly struct StringWithLanguage(string? language, string value) : IEquatable<StringWithLanguage>, IEquatable<string>, IEquatable<NoValue>, INoValue
{
    public string? Language { get; } = language;

    public string Value { get; } = value;

    public bool HasLanguage => !string.IsNullOrEmpty(Language);

    /// <summary>
    /// Indicates whether this instance represents a concrete value.
    /// Returns false only for <c>default(StringWithLanguage)</c>, which represents IPP Tag.NoValue.
    /// </summary>
    public bool IsValue { get; } = true;

    public StringWithLanguage(string value) : this(null, value)
    {
    }

    public override string ToString()
    {
        if (!IsValue) return "no value";
        return HasLanguage ? $"{Value} ({Language})" : Value;
    }

    public bool Equals(StringWithLanguage other)
    {
        if (IsValue != other.IsValue) return false;
        if (!IsValue) return true;
        return Language == other.Language && Value == other.Value;
    }

    public bool Equals(string? other)
    {
        if (!IsValue) return other == null;
        return Value == other;
    }

    public bool Equals(NoValue other)
    {
        return !IsValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is StringWithLanguage other) return Equals(other);
        if (obj is string str) return Equals(str);
        if (obj is INoValue noVal) return !noVal.IsValue && !IsValue;
        return false;
    }

    public override int GetHashCode()
    {
        if (!IsValue) return 0;
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


    /// <summary>
    /// Implicitly converts a <see cref="StringWithLanguage"/> to <see cref="string"/>.
    /// Returns <c>null</c> if the instance is in the NoValue state.
    /// </summary>
    public static implicit operator string?(StringWithLanguage swl) => swl.IsValue ? swl.Value : null;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="StringWithLanguage"/> without a language tag.
    /// A <c>null</c> string produces <c>default(StringWithLanguage)</c> (NoValue state).
    /// </summary>
    public static implicit operator StringWithLanguage(string? value) =>
        value == null ? default : new(null, value);

    /// <summary>
    /// Implicitly converts <see cref="NoValue"/> to a <see cref="StringWithLanguage"/> in the NoValue state.
    /// Enables <c>request.JobName = NoValue.Instance</c>.
    /// </summary>
    public static implicit operator StringWithLanguage(NoValue _) => default;
}
