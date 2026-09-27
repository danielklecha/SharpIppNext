using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

public class SimpleMapper : IMapper
{
    private readonly ConcurrentDictionary<(Type src, Type dst), Func<object, object?, SimpleMapper, object?>> _dictionary = new();

    private readonly ConcurrentDictionary<(Type src, Type dst), Func<object, object?, SimpleMapper, object?>?> _resolvedMapCache = new();

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
        _resolvedMapCache.Clear();
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
        return Map<TDest>(source);
    }

    public TDest Map<TSource, TDest>(TSource? source, TDest? dest)
    {
        return Map<TDest>(source, dest);
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

        var mapFunc = _resolvedMapCache.GetOrAdd((sourceType, destType), key => FindMap(key.src, key.dst));
        if (mapFunc != null)
        {
            return mapFunc(source, dest, this);
        }

        throw new ArgumentException($"No mapping found for types {sourceType.FullName} -> {destType.FullName}. Source: {source}");
    }

    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2070:UnsatisfiedInterfaces", Justification = "Interfaces of mapped models are preserved.")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "Array element types are known from model registrations.")]
    private Func<object, object?, SimpleMapper, object?>? FindMap(Type sourceType, Type destType)
    {
        if (_dictionary.TryGetValue((sourceType, destType), out var directMap))
        {
            return directMap;
        }

        var underlyingDest = Nullable.GetUnderlyingType(destType);
        if (underlyingDest != null && _dictionary.TryGetValue((sourceType, underlyingDest), out var underlyingMap))
        {
            return underlyingMap;
        }

        foreach (var iface in sourceType.GetInterfaces())
        {
            if (_dictionary.TryGetValue((iface, destType), out var ifaceMap))
            {
                return ifaceMap;
            }

            if (underlyingDest != null && _dictionary.TryGetValue((iface, underlyingDest), out ifaceMap))
            {
                return ifaceMap;
            }
        }

        for (var baseType = sourceType.BaseType; baseType != null && baseType != typeof(object); baseType = baseType.BaseType)
        {
            if (_dictionary.TryGetValue((baseType, destType), out var baseMap))
            {
                return baseMap;
            }

            if (underlyingDest != null && _dictionary.TryGetValue((baseType, underlyingDest), out baseMap))
            {
                return baseMap;
            }
        }

        for (var baseType = destType.BaseType; baseType != null && baseType != typeof(object); baseType = baseType.BaseType)
        {
            if (_dictionary.TryGetValue((sourceType, baseType), out var baseMap))
            {
                return baseMap;
            }
        }

        foreach (var iface in destType.GetInterfaces())
        {
            if (_dictionary.TryGetValue((sourceType, iface), out var ifaceMap))
            {
                return ifaceMap;
            }
        }

        if (destType.IsArray)
        {
            var destElemType = destType.GetElementType()!;

            if (sourceType.IsArray)
            {
                var srcElemType = sourceType.GetElementType()!;
                return (src, _, mapper) =>
                {
                    var srcArray = (Array)src;
                    var destArray = Array.CreateInstance(destElemType, srcArray.Length);
                    for (int i = 0; i < srcArray.Length; i++)
                    {
                        var item = srcArray.GetValue(i);
                        var mapped = mapper.MapNullable(item, item?.GetType() ?? srcElemType, destElemType);
                        if (mapped != null || !destElemType.IsValueType)
                        {
                            destArray.SetValue(mapped, i);
                        }
                    }
                    return destArray;
                };
            }

            if (sourceType == typeof(string) || !typeof(System.Collections.IEnumerable).IsAssignableFrom(sourceType))
            {
                return (src, _, mapper) =>
                {
                    var mapped = mapper.MapNullable(src, sourceType, destElemType);
                    var destArray = Array.CreateInstance(destElemType, 1);
                    if (mapped != null || !destElemType.IsValueType)
                    {
                        destArray.SetValue(mapped, 0);
                    }
                    return destArray;
                };
            }
        }

        return null;
    }
}
