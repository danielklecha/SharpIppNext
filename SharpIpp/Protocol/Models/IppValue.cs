using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// A wrapper struct for IPP attributes that supports values and out-of-band Tag.NoValue (0x13).
/// </summary>
/// <typeparam name="T">The underlying attribute type (scalar or array).</typeparam>
public readonly struct IppValue<T> : IEquatable<IppValue<T>>, IIppValue
{
    private readonly T? _value;

    public object? ValueAsObject => IsValue ? _value : null;

    /// <summary>
    /// Indicates whether this instance represents a concrete value.
    /// Returns false when the attribute represents IPP Tag.NoValue.
    /// </summary>
    public bool IsValue { get; }

    /// <summary>
    /// Gets a value indicating whether this instance represents a concrete value.
    /// Alias for <see cref="IsValue"/> for familiarity with Nullable&lt;T&gt;.
    /// </summary>
    public bool HasValue => IsValue;

    /// <summary>
    /// Gets the underlying value if present.
    /// Throws <see cref="InvalidOperationException"/> if the attribute is in the NoValue state.
    /// </summary>
    public T Value => IsValue ? _value! : throw new InvalidOperationException("Attribute is NoValue");

    /// <summary>
    /// Gets an <see cref="IppValue{T}"/> representing the IPP Tag.NoValue state.
    /// </summary>
    public static IppValue<T> NoValue => default;

    /// <summary>
    /// Initializes a new instance of the <see cref="IppValue{T}"/> struct with the specified value.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    public IppValue(T? value)
    {
        _value = value;
        IsValue = true;
    }

    /// <summary>
    /// Gets the underlying value or default(T) if in the NoValue state.
    /// </summary>
    public T? GetValueOrDefault() => _value;

    /// <summary>
    /// Gets the underlying value or the specified default value if in the NoValue state.
    /// </summary>
    /// <param name="defaultValue">The fallback value.</param>
    public T GetValueOrDefault(T defaultValue) => IsValue ? _value! : defaultValue;

    /// <summary>
    /// Attempts to get the underlying value.
    /// </summary>
    /// <param name="value">When this method returns, contains the value if present, or default.</param>
    /// <returns>true if this instance represents a value; otherwise, false.</returns>
    public bool TryGetValue([NotNullWhen(true)] out T? value)
    {
        if (IsValue && _value != null)
        {
            value = _value;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Deconstructs the instance into its value flag and value.
    /// </summary>
    public void Deconstruct(out bool isValue, out T? value)
    {
        isValue = IsValue;
        value = _value;
    }

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to an <see cref="IppValue{T}"/>.
    /// </summary>
    public static implicit operator IppValue<T>(T? value) => new(value);

    /// <summary>
    /// Implicitly converts <see cref="NoValue"/> to an <see cref="IppValue{T}"/> representing the NoValue state.
    /// </summary>
    public static implicit operator IppValue<T>(NoValue _) => default;

    /// <summary>
    /// Implicitly converts an <see cref="IppValue{T}"/> to the underlying <typeparamref name="T"/>?, returning default if NoValue.
    /// </summary>
    public static implicit operator T?(IppValue<T> value) => value.IsValue ? value._value : default;

    /// <summary>
    /// Determines whether an <see cref="IppValue{T}"/> is in the NoValue state.
    /// </summary>
    public static bool operator ==(IppValue<T> left, NoValue right) => !left.IsValue;

    /// <summary>
    /// Determines whether an <see cref="IppValue{T}"/> represents a concrete value rather than NoValue.
    /// </summary>
    public static bool operator !=(IppValue<T> left, NoValue right) => left.IsValue;

    /// <summary>
    /// Determines whether an <see cref="IppValue{T}"/> is in the NoValue state.
    /// </summary>
    public static bool operator ==(NoValue left, IppValue<T> right) => !right.IsValue;

    /// <summary>
    /// Determines whether an <see cref="IppValue{T}"/> represents a concrete value rather than NoValue.
    /// </summary>
    public static bool operator !=(NoValue left, IppValue<T> right) => right.IsValue;

    /// <summary>
    /// Compares two <see cref="IppValue{T}"/> instances for equality.
    /// </summary>
    public static bool operator ==(IppValue<T> left, IppValue<T> right)
    {
        if (left.IsValue != right.IsValue) return false;
        if (!left.IsValue) return true;
        if (left._value is Array leftArr && right._value is Array rightArr)
        {
            if (leftArr.Length != rightArr.Length) return false;
            for (int i = 0; i < leftArr.Length; i++)
            {
                if (!Equals(leftArr.GetValue(i), rightArr.GetValue(i)))
                    return false;
            }
            return true;
        }
        return EqualityComparer<T>.Default.Equals(left._value!, right._value!);
    }

    /// <summary>
    /// Compares two <see cref="IppValue{T}"/> instances for inequality.
    /// </summary>
    public static bool operator !=(IppValue<T> left, IppValue<T> right) => !(left == right);

    public bool Equals(IppValue<T> other) => this == other;

    public override bool Equals(object? obj)
    {
        if (obj is IppValue<T> other)
            return Equals(other);
        if (obj is NoValue)
            return !IsValue;
        if (obj is T otherValue)
            return this == new IppValue<T>(otherValue);
        return false;
    }

    public override int GetHashCode()
    {
        if (!IsValue || _value == null) return 0;
        if (_value is Array arr)
        {
            int hash = 17;
            foreach (var item in arr)
            {
                hash = hash * 31 + (item?.GetHashCode() ?? 0);
            }
            return hash;
        }
        return _value.GetHashCode();
    }

    public override string ToString() => IsValue ? _value?.ToString() ?? string.Empty : "no value";
}