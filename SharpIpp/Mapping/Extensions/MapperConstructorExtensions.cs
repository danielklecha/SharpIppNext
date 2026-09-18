using System;
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
}
