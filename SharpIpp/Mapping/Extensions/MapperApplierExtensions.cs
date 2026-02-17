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
        string key) where TDestination : IEnumerable
    {
        var mapKey = !src.ContainsKey(key) ? (object)NoValue.Instance : src[key].Select(x => x.Value).ToArray();
        return mapper.Map<TDestination>(mapKey);
    }


    public static TDestination MapFromDicSetNull<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key) where TDestination : IEnumerable?
    {
        var mapKey = !src.ContainsKey(key) ? (object)NoValue.Instance : src[key].Select(x => x.Value).ToArray();
        return mapper.Map<TDestination>(mapKey);
    }

    public static TDestination MapFromDic<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key)
    {
        var mapKey = !src.ContainsKey(key) ? NoValue.Instance : src[key].First().Value;
        return mapper.Map<TDestination>(mapKey);
    }
}