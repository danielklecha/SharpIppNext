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

    public static NoValue Instance = new();

    public static bool IsNoValue(object value, Tag tag = Tag.Unknown)
    {
        return value switch
        {
            int integer when integer == int.MinValue => true,
            Enum enumValue when Enum.GetUnderlyingType(enumValue.GetType()) == typeof(short) && Convert.ToInt16(enumValue) == short.MinValue => true,
            Enum enumValue when Enum.GetUnderlyingType(enumValue.GetType()) != typeof(short) && Convert.ToInt32(enumValue) == int.MinValue => true,
            DateTimeOffset dateTimeOffset when dateTimeOffset == default => true,
            DateTime dateTime when dateTime == default => true,
            Range range when range.Equals(default) => true,
            Resolution resolution when resolution.Equals(default) => true,
            StringWithLanguage stringWithLanguage when stringWithLanguage.Equals(default) => true,
            string stringValue when stringValue == string.Empty && tag == Tag.Keyword => true,
            string stringValue when stringValue == NoValueString => true,
            _ => false
        };
    }

    public static T GetNoValue<T>(Tag tag = Tag.Unknown)
    {
        return (T)GetNoValue(typeof(T), tag);
    }

    public static object GetNoValue(Type type, Tag tag = Tag.Unknown)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        if (underlyingType == typeof(int))
            return int.MinValue;

        if (underlyingType.IsEnum)
        {
            var enumUnderlyingType = Enum.GetUnderlyingType(underlyingType);
            if (enumUnderlyingType == typeof(short))
                return Enum.ToObject(underlyingType, short.MinValue);

            return Enum.ToObject(underlyingType, int.MinValue);
        }

        if (underlyingType == typeof(string))
            return (tag == Tag.Keyword ? string.Empty : NoValueString);

        if (underlyingType == typeof(DateTimeOffset))
            return DateTimeOffset.MinValue;

        if (underlyingType == typeof(DateTime))
            return DateTime.MinValue;

        if (underlyingType == typeof(bool))
            return false;

        if (underlyingType == typeof(Range))
            return new Range();

        if (underlyingType == typeof(Resolution))
            return new Resolution();

        if (underlyingType == typeof(StringWithLanguage))
            return new StringWithLanguage();

        throw new ArgumentException($"Type {type} is not supported for NoValue mapping and has no non-null default value");
    }
}
