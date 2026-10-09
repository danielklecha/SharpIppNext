using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Extensions;

public static class MapperApplierExtensions
{
    public static TDestination MapFromDicSet<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key) where TDestination : IEnumerable?
    {
        if (!src.TryGetValue(key, out IppAttribute[]? attributes) || attributes.Length == 0)
            return mapper.Map<TDestination>(null);
        var values = attributes.Select(x => x.Value).ToArray();
        return mapper.Map<TDestination>(values);
    }

    public static TDestination? MapFromDicSetNullable<TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key) where TDestination : IEnumerable?
    {
        if (!src.TryGetValue(key, out IppAttribute[]? attributes) || attributes.Length == 0)
            return mapper.MapNullable<TDestination>(null);
        if (attributes.Length == 1 && attributes[0].IsOutOfBandOrNoValue())
            return mapper.MapNullable<TDestination>(null);
        var values = attributes.Select(x => x.Value).ToArray();
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
        if (values.Length == 1 && values[0].IsOutOfBandOrNoValue())
            return mapper.MapNullable<TDestination>(values[0].Value);

        var targetType = Nullable.GetUnderlyingType(typeof(TDestination)) ?? typeof(TDestination);
        if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IppValue<>))
        {
            var innerType = targetType.GetGenericArguments()[0];
            if (innerType.IsArray || typeof(IEnumerable).IsAssignableFrom(innerType) && innerType != typeof(string))
            {
                var rawValues = values.Select(x => x.Value).ToArray();
                return mapper.MapNullable<TDestination>(rawValues);
            }
        }

        return mapper.MapNullable<TDestination>(values[0].Value);
    }

    public static TDestination? MapFromDicNullable<TPartial, TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key,
        Func<IppAttribute, TPartial, TDestination?> factory)
    {
        if (!src.TryGetValue(key, out IppAttribute[]? values))
            return mapper.MapNullable<TDestination>(null);
        if (values.Length == 0)
            return mapper.MapNullable<TDestination>(null);
        if (values.Length == 1 && values[0].IsOutOfBandOrNoValue())
            return mapper.MapNullable<TDestination>(null);

        var partial = mapper.MapNullable<TPartial>(values[0].Value);
        if (partial is null)
            return mapper.MapNullable<TDestination>(null);

        return factory(values[0], partial);
    }

    public static TDestination[]? MapFromDicSetNullable<TPartial, TDestination>(
        this IMapperApplier mapper,
        IDictionary<string, IppAttribute[]> src,
        string key,
        Func<IppAttribute, TPartial, TDestination?> factory)
    {
        if (!src.TryGetValue(key, out IppAttribute[]? values))
            return mapper.MapNullable<TDestination[]?>(null);
        if (values.Length == 0)
            return mapper.MapNullable<TDestination[]?>(null);
        if (values.Length == 1 && values[0].IsOutOfBandOrNoValue())
            return mapper.MapNullable<TDestination[]?>(null);

        var result = new List<TDestination>(values.Length);
        foreach (var attribute in values)
        {
            if (attribute.IsOutOfBandOrNoValue())
                continue;

            var partial = mapper.MapNullable<TPartial>(attribute.Value);
            if (partial is null)
                return mapper.MapNullable<TDestination[]?>(null);

            var destination = factory(attribute, partial);
            if (destination is null)
                return mapper.MapNullable<TDestination[]?>(null);

            result.Add(destination);
        }

        return result.ToArray();
    }
}
