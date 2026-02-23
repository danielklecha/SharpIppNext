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
            return mapper.Map<TDestination>(NoValue.Instance);
        var values = src[key].Select(x => x.Value).ToArray();
        if (values.Length == 0)
            return mapper.Map<TDestination>(NoValue.Instance);
        return mapper.Map<TDestination>(values);
    }



    public static TDestination MapFromDic<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key)
    {
        if (!src.ContainsKey(key))
            return mapper.Map<TDestination>(NoValue.Instance);
        var values = src[key];
        if (values.Length == 0)
            return mapper.Map<TDestination>(NoValue.Instance);
        return mapper.Map<TDestination>(values[0].Value);
    }
}
