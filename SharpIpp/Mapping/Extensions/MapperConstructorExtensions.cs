using System;
using System.Collections.Generic;
using System.Linq;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Extensions;

public static class MapperConstructorExtensions
{
    public static void RegisterGeneratedProfiles(this IMapperConstructor mapper)
    {
        GeneratedMapperRegistry.RegisterAll(mapper);
    }

    public static void CreateIppMap<T>(this IMapperConstructor mapper) where T : notnull
    {
        mapper.CreateIppMap<T, T>((i, _) => i);
    }

    public static void CreateIppMap<TSource, TDestination>(
        this IMapperConstructor mapper,
        Func<TSource, IMapperApplier, TDestination> mapFunc) where TSource : notnull
    {
        mapper.CreateMap(mapFunc);
    }

    public static void CreateCollectionMap<T>(this IMapperConstructor mapper)
    {
        mapper.CreateMap<T[], List<T>>((src, _) => new List<T>(src));
        mapper.CreateMap<List<T>, T[]>((src, _) => src.ToArray());
        mapper.CreateMap<T, T[]>((src, _) => new[] { src });
        mapper.CreateMap<IEnumerable<T>, List<T>>((src, _) => new List<T>(src));
        mapper.CreateMap<IEnumerable<T>, T[]>((src, _) => src.ToArray());
        mapper.CreateMap<T[], IEnumerable<T>>((src, _) => src);
        mapper.CreateMap<List<T>, IEnumerable<T>>((src, _) => src);
        mapper.CreateMap<T[], IReadOnlyCollection<T>>((src, _) => src);
        mapper.CreateMap<T[], IReadOnlyList<T>>((src, _) => src);
        mapper.CreateMap<T[], IList<T>>((src, _) => src);
        mapper.CreateMap<List<T>, IReadOnlyCollection<T>>((src, _) => src);
        mapper.CreateMap<List<T>, IReadOnlyList<T>>((src, _) => src);
        mapper.CreateMap<List<T>, IList<T>>((src, _) => src);
        mapper.CreateMap<object[], T[]>((src, map) =>
        {
            var arr = new T[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                var item = src[i];
                arr[i] = item != null ? map.Map<T>(item) : default!;
            }
            return arr;
        });
        mapper.CreateMap<object[], List<T>>((src, map) =>
        {
            var list = new List<T>(src.Length);
            for (int i = 0; i < src.Length; i++)
            {
                var item = src[i];
                list.Add(item != null ? map.Map<T>(item) : default!);
            }
            return list;
        });
        mapper.CreateMap<object[], IEnumerable<T>>((src, map) => map.Map<List<T>>(src));
        mapper.CreateMap<object[], IReadOnlyCollection<T>>((src, map) => map.Map<List<T>>(src));
        mapper.CreateMap<object[], IReadOnlyList<T>>((src, map) => map.Map<List<T>>(src));
        mapper.CreateMap<object[], IList<T>>((src, map) => map.Map<List<T>>(src));
    }
}
