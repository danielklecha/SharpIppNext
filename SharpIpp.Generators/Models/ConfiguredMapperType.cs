using Microsoft.CodeAnalysis;

namespace SharpIpp.Generators.Models;

internal readonly struct ConfiguredMapperType
{
    public INamedTypeSymbol TypeSymbol { get; }
    public int Order { get; }

    public ConfiguredMapperType(INamedTypeSymbol typeSymbol, int order)
    {
        TypeSymbol = typeSymbol;
        Order = order;
    }
}