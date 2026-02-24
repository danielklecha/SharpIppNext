using System;
using System.Linq;
using System.Reflection;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Extensions;

public static class MapperConstructorExtensions
{
    public static void FillFromAssembly(this IMapperConstructor mapper, Assembly assembly)
    {
        var profiles = assembly.GetTypes()
            .Where(x => typeof(IProfile).IsAssignableFrom(x) && x.IsClass)
            .Select(x => (IProfile)Activator.CreateInstance(x)!);

        foreach (IProfile profile in profiles)
        {
            profile.CreateMaps(mapper);
        }
    }
    public static void CreateIppMap<T>(this IMapperConstructor mapper) where T : notnull
    {
        mapper.CreateIppMap<T, T>((i, _) => i);
    }

    public static void CreateIppMap<TSource, TDestination>(
        this IMapperConstructor mapper,
        Func<TSource, IMapperApplier, TDestination> mapFunc) where TSource : notnull
    {
        var destType = typeof(TDestination);
        var srcType = typeof(TSource);
        mapper.CreateMap(mapFunc);
        mapper.CreateMap<NoValue, TDestination[]?>((_, __) => null);

        mapper.CreateMap<object, TDestination>((src, map) =>
        {
            if (src == null)
            {
                throw new ArgumentException($"Mapping null to non nullable type {typeof(object)} -> {destType}");
            }

            return src is TSource source
                ? map.Map<TDestination>(source)
                : throw new ArgumentException($"Mapping not supported {srcType} -> {destType}");
        });

        if (Nullable.GetUnderlyingType(destType) == null)
        {
            var destIsClass = destType.IsClass;
            var destNullable = destIsClass ? destType : typeof(Nullable<>).MakeGenericType(destType);

            var destNull = destIsClass
                ? Convert.ChangeType(null, destNullable)
                : Activator.CreateInstance(destNullable);
            mapper.CreateMap(typeof(NoValue), destNullable, (_, _2, _3) => destNull);

            if (!destIsClass)
            {
                mapper.CreateMap(srcType,
                    destNullable,
                    (src, _2, map) => src == null ? destNull : map.Map<TDestination>(src));
            }
        }

        mapper.CreateMap<TSource, TDestination[]>((src, map) =>
            src != null
                ? map.Map<TDestination[]>(new[] { src })
                : throw new ArgumentException($"Mapping null to non nullable type {srcType} -> {destType}"));

        mapper.CreateMap<TSource, TDestination[]?>((src, map) =>
            src == null ? null : map.Map<TDestination[]?>(new[] { src }));

        mapper.CreateMap<TSource[], TDestination[]>((src, map) =>
            src.Select(x => map.Map<TDestination>(x)).ToArray());

        mapper.CreateMap<TSource[], TDestination[]?>((src, map) =>
            src.Select(x => map.Map<TDestination>(x)).ToArray());

        mapper.CreateMap<TSource[]?, TDestination[]?>((src, map) =>
            src?.Select(x => map.Map<TDestination>(x)).ToArray());

        mapper.CreateMap<object[], TDestination[]>((src, map) => src.Select(map.Map<TDestination>).ToArray());
        mapper.CreateMap<object[], TDestination[]?>((src, map) => src.Select(map.Map<TDestination>).ToArray());
        mapper.CreateMap<object[]?, TDestination[]?>((src, map) => src?.Select(map.Map<TDestination>).ToArray());
    }
}
