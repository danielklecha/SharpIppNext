using SharpIpp.Models.Responses;
using System;

namespace SharpIpp.Protocol.Models;

public struct NoValue : IEquatable<NoValue>
{
    public const string NoValueString = "###NOVALUE###";

    public override string ToString()
    {
        return "no value";
    }

    public bool Equals(NoValue other)
    {
        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is NoValue other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public static bool operator ==(NoValue left, NoValue right) => true;
    public static bool operator !=(NoValue left, NoValue right) => false;

    public static NoValue Instance = new();

    public static NoValue GetNoValue() => Instance;

    public static implicit operator int(NoValue _) => int.MinValue;
    public static implicit operator string(NoValue _) => NoValueString;
    public static implicit operator DateTimeOffset(NoValue _) => DateTimeOffset.MinValue;
    public static implicit operator DateTime(NoValue _) => DateTime.MinValue;
    public static implicit operator Range(NoValue _) => new();
    public static implicit operator Resolution(NoValue _) => new();
    public static implicit operator OctetString(NoValue _) => new();
    public static implicit operator StringWithLanguage(NoValue _) => new();
    public static implicit operator IppVersion(NoValue _) => default;

    public static bool operator ==(int left, NoValue right) => left == int.MinValue;
    public static bool operator !=(int left, NoValue right) => left != int.MinValue;
    public static bool operator ==(NoValue left, int right) => right == int.MinValue;
    public static bool operator !=(NoValue left, int right) => right != int.MinValue;

    public static bool operator ==(string? left, NoValue right) => left == NoValueString;
    public static bool operator !=(string? left, NoValue right) => left != NoValueString;
    public static bool operator ==(NoValue left, string? right) => right == NoValueString;
    public static bool operator !=(NoValue left, string? right) => right != NoValueString;

    public static bool operator ==(DateTime left, NoValue right) => left == DateTime.MinValue;
    public static bool operator !=(DateTime left, NoValue right) => left != DateTime.MinValue;
    public static bool operator ==(NoValue left, DateTime right) => right == DateTime.MinValue;
    public static bool operator !=(NoValue left, DateTime right) => right != DateTime.MinValue;

    public static bool operator ==(DateTimeOffset left, NoValue right) => left == DateTimeOffset.MinValue;
    public static bool operator !=(DateTimeOffset left, NoValue right) => left != DateTimeOffset.MinValue;
    public static bool operator ==(NoValue left, DateTimeOffset right) => right == DateTimeOffset.MinValue;
    public static bool operator !=(NoValue left, DateTimeOffset right) => right != DateTimeOffset.MinValue;

    public static bool operator ==(Range left, NoValue right) => !left.IsValue;
    public static bool operator !=(Range left, NoValue right) => left.IsValue;
    public static bool operator ==(NoValue left, Range right) => !right.IsValue;
    public static bool operator !=(NoValue left, Range right) => right.IsValue;

    public static bool operator ==(Resolution left, NoValue right) => !left.IsValue;
    public static bool operator !=(Resolution left, NoValue right) => left.IsValue;
    public static bool operator ==(NoValue left, Resolution right) => !right.IsValue;
    public static bool operator !=(NoValue left, Resolution right) => right.IsValue;

    public static bool operator ==(OctetString left, NoValue right) => !left.IsValue;
    public static bool operator !=(OctetString left, NoValue right) => left.IsValue;
    public static bool operator ==(NoValue left, OctetString right) => !right.IsValue;
    public static bool operator !=(NoValue left, OctetString right) => right.IsValue;

    public static bool operator ==(StringWithLanguage left, NoValue right) => !left.IsValue;
    public static bool operator !=(StringWithLanguage left, NoValue right) => left.IsValue;
    public static bool operator ==(NoValue left, StringWithLanguage right) => !right.IsValue;
    public static bool operator !=(NoValue left, StringWithLanguage right) => right.IsValue;

    public static bool operator ==(IppVersion left, NoValue right) => !left.IsValue;
    public static bool operator !=(IppVersion left, NoValue right) => left.IsValue;
    public static bool operator ==(NoValue left, IppVersion right) => !right.IsValue;
    public static bool operator !=(NoValue left, IppVersion right) => right.IsValue;

    public static bool IsNoValue(object value, Tag tag = Tag.Unknown)
    {
        return value switch
        {
            int integer when integer == int.MinValue => true,
            Enum enumValue when Enum.GetUnderlyingType(enumValue.GetType()) == typeof(short) && Convert.ToInt16(enumValue) == short.MinValue => true,
            Enum enumValue when Enum.GetUnderlyingType(enumValue.GetType()) != typeof(short) && Convert.ToInt32(enumValue) == int.MinValue => true,
            DateTimeOffset dateTimeOffset when dateTimeOffset == default => true,
            DateTime dateTime when dateTime == default => true,
            string stringValue when stringValue == string.Empty && tag == Tag.Keyword => true,
            string stringValue when stringValue == NoValueString => true,
            Array array when array.Length == 1 && array.GetValue(0) is NoValue => true,
            INoValue noValueModel when !noValueModel.IsValue => true,
            NoValue => true,
            _ => false
        };
    }

    public static T GetNoValue<T>(Tag tag = Tag.Unknown)
    {
        return (T)GetNoValue(typeof(T), tag);
    }

    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2067:UnsatisfiedPublicParameterlessConstructor", Justification = "ISmartEnum and IIppCollection types always have public parameterless constructors.")]
    public static object GetNoValue(Type type, Tag tag = Tag.Unknown)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        if (underlyingType.IsEnum)
        {
            var enumUnderlyingType = Enum.GetUnderlyingType(underlyingType);
            return enumUnderlyingType == typeof(short)
                ? Enum.ToObject(underlyingType, short.MinValue)
                : Enum.ToObject(underlyingType, int.MinValue);
        }

        if (underlyingType == typeof(int)) return int.MinValue;
        if (underlyingType == typeof(string)) return tag == Tag.Keyword ? string.Empty : NoValueString;
        if (underlyingType == typeof(DateTimeOffset)) return DateTimeOffset.MinValue;
        if (underlyingType == typeof(DateTime)) return DateTime.MinValue;
        if (underlyingType == typeof(bool)) return false;
        if (underlyingType == typeof(Range)) return new Range();
        if (underlyingType == typeof(Resolution)) return new Resolution();
        if (underlyingType == typeof(StringWithLanguage)) return new StringWithLanguage();
        if (underlyingType == typeof(OctetString)) return new OctetString();
        if (underlyingType == typeof(IppVersion)) return default(IppVersion);

        if (typeof(INoValueWritable).IsAssignableFrom(underlyingType) && Activator.CreateInstance(underlyingType) is INoValueWritable writable)
        {
            writable.IsValue = false;
            return writable;
        }

        if (typeof(ISmartEnum).IsAssignableFrom(underlyingType) && Activator.CreateInstance(underlyingType) is ISmartEnum smartEnum)
        {
            return smartEnum;
        }

        throw new ArgumentException($"Type {type} is not supported for NoValue mapping and has no non-null default value");
    }
}
