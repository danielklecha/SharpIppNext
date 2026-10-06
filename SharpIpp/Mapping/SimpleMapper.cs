using SharpIpp.Mapping.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

public class SimpleMapper : IMapper
{
    private static readonly Lazy<SimpleMapper> _instance = new(CreateDefaultInstance);

    /// <summary>
    /// Gets the shared default <see cref="SimpleMapper"/> instance configured with registered profiles.
    /// </summary>
    public static SimpleMapper Instance => _instance.Value;

    private static SimpleMapper CreateDefaultInstance()
    {
        var mapper = new SimpleMapper();
        mapper.RegisterGeneratedProfiles();
        return mapper;
    }

    private readonly ConcurrentDictionary<(Type src, Type dst), Func<object, object?, SimpleMapper, object?>> _dictionary = new();

    public void CreateMap<TSource, TDest>(Func<TSource, IMapperApplier, TDest?> mapFunc)
    {
        CreateMap(typeof(TSource), typeof(TDest), (src, mapper) => mapFunc((TSource)src, mapper));
    }

    public void CreateMap<TSource, TDest>(Func<TSource, TDest?, IMapperApplier, TDest?> mapFunc)
    {
        CreateMap(typeof(TSource),
            typeof(TDest),
            (src, dst, mapper) => mapFunc((TSource)src, dst == null ? default : (TDest)dst, mapper));
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
        return Map<TSource, TDest>(source, default);
    }

    public TDest Map<TSource, TDest>(TSource? source, TDest? dest)
    {
        var res = MapNullable<TSource, TDest>(source, dest) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
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

        return (TDest?)MapNullable(source, source.GetType(), typeof(TDest), dest);
    }

    public TDest? MapNullable<TSource, TDest>(TSource? source)
    {
        return MapNullable<TSource, TDest>(source, default);
    }

    public TDest? MapNullable<TSource, TDest>(TSource? source, TDest? dest)
    {
        return (TDest?)MapNullable(source, typeof(TSource), typeof(TDest), dest);
    }

    public TDest? MapNullable<TDest>(object? source, Type sourceType, TDest? dest)
    {
        if (source == null)
        {
            return dest;
        }

        return (TDest?)MapNullable(source, sourceType, typeof(TDest), dest);
    }

    public object Map(object? source, Type destType)
    {
        return MapNullable(source, destType) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
    }

    public object Map(object? source, Type sourceType, Type destType)
    {
        var res = MapNullable(source, sourceType, destType) ?? throw new ArgumentException("Cannot map null source to non-nullable destination without a default destination.");
        return res;
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

    public object? MapNullable(object? source, Type sourceType, Type destType)
    {
        return MapNullable(source, sourceType, destType, null);
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

        var underlyingDest = Nullable.GetUnderlyingType(destType);
        if (underlyingDest != null && underlyingDest == sourceType)
        {
            return source;
        }

        if (source is NoValue)
        {
            if (_dictionary.TryGetValue((sourceType, destType), out var noValMap))
            {
                return noValMap(source, dest, this);
            }
            if (underlyingDest != null && _dictionary.TryGetValue((sourceType, underlyingDest), out var underlyingNoValMap))
            {
                return underlyingNoValMap(source, dest, this);
            }
            if (!destType.IsValueType || underlyingDest != null)
            {
                return null;
            }
        }

        if (source is IIppValue ippVal)
        {
            if (_dictionary.TryGetValue((sourceType, destType), out var ippValMap))
            {
                return ippValMap(source, dest, this);
            }
            if (underlyingDest != null && _dictionary.TryGetValue((sourceType, underlyingDest), out var underlyingIppValMap))
            {
                return underlyingIppValMap(source, dest, this);
            }
            if (!ippVal.IsValue)
            {
                if (destType == typeof(NoValue))
                {
                    return NoValue.Instance;
                }
                if (!destType.IsValueType || underlyingDest != null)
                {
                    return null;
                }
            }
            else
            {
                var val = ippVal.ValueAsObject;
                if (val == null)
                {
                    return null;
                }
                if (destType.IsAssignableFrom(val.GetType()))
                {
                    return val;
                }
                return MapNullable(val, val.GetType(), destType, dest);
            }
        }

        if (_dictionary.TryGetValue((sourceType, destType), out var mapFunc))
        {
            return mapFunc(source, dest, this);
        }

        if (underlyingDest != null && _dictionary.TryGetValue((sourceType, underlyingDest), out var underlyingMap))
        {
            return underlyingMap(source, dest, this);
        }

        throw new ArgumentException($"No mapping found for types {sourceType.FullName} -> {destType.FullName}. Source: {source}");
    }
}
