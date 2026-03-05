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
            IIppCollection collection when collection.IsNoValue => true,
            NoValue => true,
            _ => false
        };
    }

    public static T GetCollectionNoValue<T>() where T : IIppCollection, new()
    {
        return new T { IsNoValue = true };
    }

    public static T GetNoValue<T>(Tag tag = Tag.Unknown)
    {
        return (T)GetNoValue(typeof(T), tag);
    }

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

        return underlyingType switch
        {
            _ when underlyingType == typeof(int) => int.MinValue,
            _ when underlyingType == typeof(string) => tag == Tag.Keyword ? string.Empty : NoValueString,
            _ when underlyingType == typeof(DateTimeOffset) => DateTimeOffset.MinValue,
            _ when underlyingType == typeof(DateTime) => DateTime.MinValue,
            _ when underlyingType == typeof(bool) => false,
            _ when underlyingType == typeof(Range) => new Range(),
            _ when underlyingType == typeof(Resolution) => new Resolution(),
            _ when underlyingType == typeof(StringWithLanguage) => new StringWithLanguage(),
            _ when underlyingType == typeof(MediaCol) => GetCollectionNoValue<MediaCol>(),
            _ when underlyingType == typeof(MediaSize) => GetCollectionNoValue<MediaSize>(),
            _ when underlyingType == typeof(MediaSourceProperties) => GetCollectionNoValue<MediaSourceProperties>(),
            _ when underlyingType == typeof(FinishingsCol) => GetCollectionNoValue<FinishingsCol>(),
            _ when underlyingType == typeof(Baling) => GetCollectionNoValue<Baling>(),
            _ when underlyingType == typeof(Binding) => GetCollectionNoValue<Binding>(),
            _ when underlyingType == typeof(Coating) => GetCollectionNoValue<Coating>(),
            _ when underlyingType == typeof(Covering) => GetCollectionNoValue<Covering>(),
            _ when underlyingType == typeof(Folding) => GetCollectionNoValue<Folding>(),
            _ when underlyingType == typeof(Laminating) => GetCollectionNoValue<Laminating>(),
            _ when underlyingType == typeof(Punching) => GetCollectionNoValue<Punching>(),
            _ when underlyingType == typeof(Stitching) => GetCollectionNoValue<Stitching>(),
            _ when underlyingType == typeof(Trimming) => GetCollectionNoValue<Trimming>(),
            _ => throw new ArgumentException($"Type {type} is not supported for NoValue mapping and has no non-null default value")
        };
    }
}

