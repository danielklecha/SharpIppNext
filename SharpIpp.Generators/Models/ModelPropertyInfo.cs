using Microsoft.CodeAnalysis;

namespace SharpIpp.Generators.Models;

internal class ModelPropertyInfo
{
    public IPropertySymbol Property { get; set; } = null!;
    public ITypeSymbol UnwrappedType { get; set; } = null!;
    public ITypeSymbol? ElementType { get; set; }
    public string AttributeName { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public int Order { get; set; }
    public int SourceIndex { get; set; }
    public string? DefaultValue { get; set; }
    public bool IsNullable { get; set; }
    public bool IsArray { get; set; }
    public bool IsIppDictArray { get; set; }
    public bool IsCollection { get; set; }
    public bool IsCollectionArray { get; set; }
    public bool IsStructuredString { get; set; }
    public bool IsStructuredStringArray { get; set; }
    public bool IsMarkedSmartEnum { get; set; }
    public bool IsMarkedSmartEnumArray { get; set; }
    public bool IsSmartEnum { get; set; }
    public bool IsSmartEnumArray { get; set; }
    public bool IsEnum { get; set; }
    public bool IsEnumArray { get; set; }
    public bool IsString { get; set; }
    public bool IsStringArray { get; set; }
    public bool IsInt { get; set; }
    public bool IsIntArray { get; set; }
    public bool IsBool { get; set; }
    public bool IsBoolArray { get; set; }
    public bool IsDateTimeOffset { get; set; }
    public bool IsDateTimeOffsetArray { get; set; }
    public bool IsUri { get; set; }
    public bool IsUriArray { get; set; }
    public bool IsRange { get; set; }
    public bool IsRangeArray { get; set; }
    public bool IsResolution { get; set; }
    public bool IsResolutionArray { get; set; }
    public bool IsOctetString { get; set; }
    public bool IsOctetStringArray { get; set; }
    public bool IsStringWithLanguage { get; set; }
    public bool IsStringWithLanguageArray { get; set; }
    public bool IsIppValue { get; set; }
    public bool IsIppValueArray { get; set; }
    public ITypeSymbol? IppValueInnerType { get; set; }
    public ITypeSymbol? IppValueElementType { get; set; }
    public bool HasExplicitTag { get; set; }
}