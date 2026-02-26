using System.Collections;
using System.Collections.Generic;
using System.Linq;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Extensions;

public static class MapperApplierExtensions
{
    public static TDestination MapFromDicSet<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key) where TDestination : IEnumerable?
    {
        if (!src.ContainsKey(key))
            return mapper.Map<TDestination>(null);
        var values = src[key].Select(x => x.Value).ToArray();
        if (values.Length == 0)
            return mapper.Map<TDestination>(null);
        return mapper.Map<TDestination>(values);
    }

    public static TDestination? MapFromDicSetNullable<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key) where TDestination : IEnumerable?
    {
        if (!src.ContainsKey(key))
            return mapper.MapNullable<TDestination>(null);
        var values = src[key].Select(x => x.Value).ToArray();
        if (values.Length == 0)
            return mapper.MapNullable<TDestination>(null);
        return mapper.MapNullable<TDestination>(values);
    }

    public static TDestination MapFromDic<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key)
    {
        if (!src.TryGetValue(key, out IppAttribute[]? values))
            return mapper.Map<TDestination>(null);
        if (values.Length == 0)
            return mapper.Map<TDestination>(null);
        return mapper.Map<TDestination>(values[0].Value);
    }

    public static TDestination? MapFromDicNullable<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key)
    {
        if (!src.TryGetValue(key, out IppAttribute[]? values))
            return mapper.MapNullable<TDestination>(null);
        if (values.Length == 0)
            return mapper.MapNullable<TDestination>(null);
        return mapper.MapNullable<TDestination>(values[0].Value);
    }
}
