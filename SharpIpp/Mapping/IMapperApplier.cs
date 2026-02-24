using System;

namespace SharpIpp.Mapping;

public interface IMapperApplier
{
    TDest Map<TDest>(object? source);

    TDest Map<TDest>(object? source, TDest? dest);

    TDest Map<TSource, TDest>(TSource? source);

    TDest Map<TSource, TDest>(TSource? source, TDest? dest);

    TDest? MapNullable<TDest>(object? source);

    TDest? MapNullable<TDest>(object? source, TDest? dest);

    TDest? MapNullable<TSource, TDest>(TSource? source);

    TDest? MapNullable<TSource, TDest>(TSource? source, TDest? dest);

    TDest? MapNullable<TDest>(object? source, Type sourceType, TDest? dest);
    
    object Map(object? source, Type destType);

    object Map(object? source, Type sourceType, Type destType, object? dest);

    object? MapNullable(object? source, Type destType);

    object? MapNullable(object? source, Type sourceType, Type destType, object? dest);
}
