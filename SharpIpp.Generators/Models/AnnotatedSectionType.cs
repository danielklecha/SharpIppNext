using Microsoft.CodeAnalysis;

namespace SharpIpp.Generators.Models;

internal readonly struct AnnotatedSectionType
{
    public INamedTypeSymbol TypeSymbol { get; }
    public byte SectionTag { get; }

    public AnnotatedSectionType(INamedTypeSymbol typeSymbol, byte sectionTag)
    {
        TypeSymbol = typeSymbol;
        SectionTag = sectionTag;
    }
}