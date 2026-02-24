using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping;

public class SimpleMapper : IMapper
{
    private readonly ConcurrentDictionary<(Type src, Type dst), Func<object, object?, SimpleMapper, object?>> _dictionary = new();

    private readonly ConcurrentDictionary<(Type src, Type dst), List<((Type src, Type dst) map, MapType type)>> _pairsCache = new();

    public void CreateMap<TSource, TDest>(Func<TSource, IMapperApplier, TDest?> mapFunc)
    {
        CreateMap(typeof(TSource), typeof(TDest), (src, mapper) => mapFunc((TSource)src, mapper));
    }

    public void CreateMap<TSource, TDest>(Func<TSource, TDest?, IMapperApplier, TDest?> mapFunc)
    {
        CreateMap(typeof(TSource),
            typeof(TDest),
            (src, dst, mapper) => mapFunc((TSource)src, (TDest?)dst, mapper));
    }

    public void CreateMap(Type sourceType, Type destType, Func<object, IMapperApplier, object?> mapFunc)
    {
        CreateMap(sourceType, destType, (src, dst, mapper) => mapFunc(src, mapper));
    }

    public void CreateMap(Type sourceType, Type destType, Func<object, object?, IMapperApplier, object?> mapFunc)
    {
        var key = (sourceType, destType);
        _dictionary[key] = (src, dst, mapper) => mapFunc(src, dst, mapper);
    }

    public TDest Map<TDest>(object? source)
    {
        var res = MapNullable<TDest>(source) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
        return res;
    }

    public TDest Map<TDest>(object? source, TDest? dest)
    {
        var res = MapNullable(source, dest) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
        return res;
    }

    public TDest Map<TSource, TDest>(TSource? source)
    {
        var res = MapNullable<TSource, TDest>(source) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
        return res;
    }

    public TDest Map<TSource, TDest>(TSource? source, TDest? dest)
    {
        var res = MapNullable(source, dest) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
        return res;
    }

    public TDest? MapNullable<TDest>(object? source)
    {
        return MapNullable<TDest>(source, default);
    }

    public TDest? MapNullable<TDest>(object? source, TDest? dest)
    {
        if (source == null)
        {
            return dest;
        }

        return MapNullable(source, source.GetType(), dest);
    }

    public TDest? MapNullable<TSource, TDest>(TSource? source)
    {
        return MapNullable<TSource, TDest>(source, default);
    }

    public TDest? MapNullable<TSource, TDest>(TSource? source, TDest? dest)
    {
        if (source == null)
        {
            return dest;
        }

        return MapNullable(source, typeof(TSource), dest);
    }

    public TDest? MapNullable<TDest>(object? source, Type sourceType, TDest? dest)
    {
        return (TDest?)MapNullable(source, sourceType, typeof(TDest), dest);
    }

    public object Map(object? source, Type destType)
    {
        return MapNullable(source, destType) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
    }

    public object Map(object? source, Type sourceType, Type destType, object? dest)
    {
        return MapNullable(source, sourceType, destType, dest) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
    }

    public object? MapNullable(object? source, Type destType)
    {
        if (source == null)
        {
            return null;
        }

        return MapNullable(source, source.GetType(), destType, null);
    }

    public object? MapNullable(object? source, Type sourceType, Type destType, object? dest)
    {
        if (source == null)
        {
            return dest;
        }

        if (sourceType == destType)
        {
            return source;
        }

        var pairs = _pairsCache.GetOrAdd((sourceType, destType), key => PossiblePairs(key.src, key.dst).ToList());

        foreach (var (map, type) in pairs)
        {
            switch (type)
            {
                case MapType.Cast: return source;
                case MapType.Simple:
                    if (_dictionary.TryGetValue(map, out var mapFunc))
                    {
                        return mapFunc(source, dest, this);
                    }
                    break;
            }
        }

        throw new ArgumentException($"No mapping found for types {sourceType.FullName} -> {destType.FullName}. Source: {source}");
    }

    private static IEnumerable<((Type src, Type dst) map, MapType type)> PossiblePairs(Type sourceType, Type destType)
    {
        var underlying = Nullable.GetUnderlyingType(destType);

        if (underlying != null && underlying == sourceType)
        {
            yield return ((sourceType, destType), MapType.Cast);
        }

        yield return ((sourceType, destType), MapType.Simple);

        foreach (var ifc in sourceType.GetInterfaces())
        {
            yield return ((ifc, destType), MapType.Simple);
        }
    }

    private enum MapType
    {
        Simple,
        Cast,
    }
}
