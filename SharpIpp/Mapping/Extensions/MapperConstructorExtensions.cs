using System;
using System.Collections.Generic;
using System.Linq;

using SharpIpp.Mapping;
using SharpIpp.Protocol.Extensions;
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
        mapper.CreateIppMap<T, T>(CollectionMappingHelper.Identity<T>);
    }

    public static void CreateIppMap<TSource, TDestination>(
        this IMapperConstructor mapper,
        Func<TSource, IMapperApplier, TDestination> mapFunc) where TSource : notnull
    {
        mapper.CreateMap(mapFunc);
    }

    public static void CreateCollectionMap<T>(this IMapperConstructor mapper)
    {
        mapper.CreateMap<T[], List<T>>(CollectionMappingHelper.MapArrayToList<T>);
        mapper.CreateMap<List<T>, T[]>(CollectionMappingHelper.MapListToArray<T>);
        mapper.CreateMap<T, T[]>(CollectionMappingHelper.MapItemToArray<T>);
        mapper.CreateMap<IEnumerable<T>, List<T>>(CollectionMappingHelper.MapEnumerableToList<T>);
        mapper.CreateMap<IEnumerable<T>, T[]>(CollectionMappingHelper.MapEnumerableToArray<T>);
        mapper.CreateMap<object[], T[]>(CollectionMappingHelper.MapObjectArrayToArray<T>);
        mapper.CreateMap<object[], List<T>>(CollectionMappingHelper.MapObjectArrayToList<T>);
        mapper.CreateMap<object[], IEnumerable<T>>(CollectionMappingHelper.MapObjectArrayToList<T>);
        mapper.CreateMap<object[], IReadOnlyCollection<T>>(CollectionMappingHelper.MapObjectArrayToList<T>);
        mapper.CreateMap<object[], IReadOnlyList<T>>(CollectionMappingHelper.MapObjectArrayToList<T>);
        mapper.CreateMap<object[], IList<T>>(CollectionMappingHelper.MapObjectArrayToList<T>);
    }
}
