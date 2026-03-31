using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Protocol.Models;

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

    public object Map(object? source, Type sourceType, Type destType)
    {
        return Map(source, sourceType, destType, null);
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

        var pairs = _pairsCache.GetOrAdd((sourceType, destType), key => PossiblePairs(key.src, key.dst).ToList());

        foreach (var (map, type) in pairs)
        {
            switch (type)
            {
                case MapType.Cast: return source;
                case MapType.Simple:
                    if (_dictionary.TryGetValue(map, out var mapFunc))
                    {
                        var result = mapFunc(source, dest, this);

                        return result;
                    }
                    break;
            }
        }

        if (TryMapEnumerableToCollection(source, destType, out var collectionResult))
        {
            return collectionResult;
        }

        throw new ArgumentException($"No mapping found for types {sourceType.FullName} -> {destType.FullName}. Source: {source}");
    }

    private bool TryMapEnumerableToCollection(object source, Type destType, out object? result)
    {
        result = null;

        if (source is string || source is not IEnumerable sourceEnumerable)
        {
            return false;
        }

        if (!TryGetCollectionElementType(destType, out var destElementType, out var isArray))
        {
            return false;
        }

        var mappedItems = new List<object?>();
        foreach (var item in sourceEnumerable)
        {
            var sourceItemType = item?.GetType() ?? destElementType;
            mappedItems.Add(MapNullable(item, sourceItemType, destElementType, null));
        }

        if (isArray)
        {
            var array = Array.CreateInstance(destElementType, mappedItems.Count);
            for (var i = 0; i < mappedItems.Count; i++)
            {
                var value = mappedItems[i];
                if (value == null && destElementType.IsValueType)
                {
                    value = Activator.CreateInstance(destElementType);
                }

                array.SetValue(value, i);
            }

            result = array;
            return true;
        }

        var listType = typeof(List<>).MakeGenericType(destElementType);
        var list = (IList)Activator.CreateInstance(listType)!;
        foreach (var item in mappedItems)
        {
            if (item == null && destElementType.IsValueType)
            {
                list.Add(Activator.CreateInstance(destElementType));
            }
            else
            {
                list.Add(item);
            }
        }

        if (destType.IsAssignableFrom(listType))
        {
            result = list;
            return true;
        }

        if (!destType.IsInterface && !destType.IsAbstract)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(destElementType);
            var enumerableCtor = destType.GetConstructor(new[] { enumerableType });
            if (enumerableCtor != null)
            {
                result = enumerableCtor.Invoke(new object[] { list });
                return true;
            }

            if (typeof(IList).IsAssignableFrom(destType) && destType.GetConstructor(Type.EmptyTypes) != null)
            {
                var destinationList = (IList)Activator.CreateInstance(destType)!;
                foreach (var item in list)
                {
                    destinationList.Add(item);
                }

                result = destinationList;
                return true;
            }
        }

        return false;
    }

    private static bool TryGetCollectionElementType(Type type, out Type elementType, out bool isArray)
    {
        isArray = false;

        if (type.IsArray)
        {
            elementType = type.GetElementType()!;
            isArray = true;
            return true;
        }

        if (TryGetCollectionElementTypeFromGenericType(type, out elementType))
        {
            return true;
        }

        foreach (var interfaceType in type.GetInterfaces())
        {
            if (TryGetCollectionElementTypeFromGenericType(interfaceType, out elementType))
            {
                return true;
            }
        }

        elementType = typeof(object);
        return false;
    }

    private static bool TryGetCollectionElementTypeFromGenericType(Type type, out Type elementType)
    {
        if (!type.IsGenericType)
        {
            elementType = typeof(object);
            return false;
        }

        var genericDefinition = type.GetGenericTypeDefinition();
        if (genericDefinition != typeof(List<>)
            && genericDefinition != typeof(IEnumerable<>)
            && genericDefinition != typeof(ICollection<>)
            && genericDefinition != typeof(IList<>)
            && genericDefinition != typeof(IReadOnlyCollection<>)
            && genericDefinition != typeof(IReadOnlyList<>)
            && genericDefinition != typeof(ISet<>))
        {
            elementType = typeof(object);
            return false;
        }

        elementType = type.GetGenericArguments()[0];
        return true;
    }

    private static IEnumerable<((Type src, Type dst) map, MapType type)> PossiblePairs(Type sourceType, Type destType)
    {
        var visited = new HashSet<(Type src, Type dst)>();

        static IEnumerable<Type> SourceCandidates(Type src)
        {
            yield return src;
            foreach (var ifc in src.GetInterfaces())
            {
                yield return ifc;
            }
        }

        static IEnumerable<Type> DestinationCandidates(Type dst)
        {
            yield return dst;

            var underlying = Nullable.GetUnderlyingType(dst);
            if (underlying != null)
            {
                yield return underlying;
            }

            if (!dst.IsValueType)
            {
                for (var baseType = dst.BaseType; baseType != null; baseType = baseType.BaseType)
                {
                    yield return baseType;
                }

                foreach (var ifc in dst.GetInterfaces())
                {
                    yield return ifc;
                }
            }
        }

        static bool TryAdd(HashSet<(Type src, Type dst)> set, Type src, Type dst)
        {
            return set.Add((src, dst));
        }

        var underlying = Nullable.GetUnderlyingType(destType);

        if (underlying != null && underlying == sourceType)
        {
            yield return ((sourceType, destType), MapType.Cast);
        }

        foreach (var src in SourceCandidates(sourceType))
        {
            foreach (var dst in DestinationCandidates(destType))
            {
                if (TryAdd(visited, src, dst))
                {
                    yield return ((src, dst), MapType.Simple);
                }
            }
        }
    }

    private enum MapType
    {
        Simple,
        Cast,
    }
}
