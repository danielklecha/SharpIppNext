using System;
using Microsoft.CodeAnalysis;

namespace SharpIpp.Generators.Models;

internal readonly struct ConversionMapping : IEquatable<ConversionMapping>
{
    public ITypeSymbol SourceType { get; }
    public ITypeSymbol DestType { get; }

    public ConversionMapping(ITypeSymbol sourceType, ITypeSymbol destType)
    {
        SourceType = sourceType;
        DestType = destType;
    }

    public bool Equals(ConversionMapping other) =>
        SymbolEqualityComparer.Default.Equals(SourceType, other.SourceType) &&
        SymbolEqualityComparer.Default.Equals(DestType, other.DestType);

    public override bool Equals(object? obj) => obj is ConversionMapping other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            return (SymbolEqualityComparer.Default.GetHashCode(SourceType) * 397) ^
                   SymbolEqualityComparer.Default.GetHashCode(DestType);
        }
    }
}