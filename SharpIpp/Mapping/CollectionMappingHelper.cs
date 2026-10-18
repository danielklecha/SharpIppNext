using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

internal static class CollectionMappingHelper
{
    public static List<T> MapArrayToList<T>(T[] src, IMapperApplier _) => new List<T>(src);
    public static T[] MapListToArray<T>(List<T> src, IMapperApplier _) => src.ToArray();
    public static T[] MapItemToArray<T>(T src, IMapperApplier _) => new[] { src };
    public static List<T> MapEnumerableToList<T>(IEnumerable<T> src, IMapperApplier _) => new List<T>(src);
    public static T[] MapEnumerableToArray<T>(IEnumerable<T> src, IMapperApplier _) => src.ToArray();

    public static T[] MapObjectArrayToArray<T>(object[] src, IMapperApplier map)
    {
        var arr = new T[src.Length];
        for (int i = 0; i < src.Length; i++)
        {
            var item = src[i];
            arr[i] = item != null ? map.Map<T>(item) : default!;
        }
        return arr;
    }

    public static List<T> MapObjectArrayToList<T>(object[] src, IMapperApplier map)
    {
        var list = new List<T>(src.Length);
        for (int i = 0; i < src.Length; i++)
        {
            var item = src[i];
            list.Add(item != null ? map.Map<T>(item) : default!);
        }
        return list;
    }

    public static TEnum[] MapStringArrayToEnumArray<TEnum>(string[] src, IMapperApplier map)
    {
        var arr = new TEnum[src.Length];
        for (int i = 0; i < src.Length; i++) arr[i] = map.Map<TEnum>(src[i]);
        return arr;
    }

    public static string[] MapEnumArrayToStringArray<TEnum>(TEnum[] src, IMapperApplier _)
    {
        var arr = new string[src.Length];
        for (int i = 0; i < src.Length; i++) arr[i] = src[i]!.ToString()!;
        return arr;
    }

    public static List<TEnum> MapStringArrayToEnumList<TEnum>(string[] src, IMapperApplier map)
    {
        var list = new List<TEnum>(src.Length);
        for (int i = 0; i < src.Length; i++) list.Add(map.Map<TEnum>(src[i]));
        return list;
    }

    public static string[] MapEnumListToStringArray<TEnum>(List<TEnum> src, IMapperApplier _)
    {
        var arr = new string[src.Count];
        for (int i = 0; i < src.Count; i++) arr[i] = src[i]!.ToString()!;
        return arr;
    }

    public static IEnumerable<TEnum> MapStringArrayToEnumEnumerable<TEnum>(string[] src, IMapperApplier map) => map.Map<TEnum[]>(src);

    public static IReadOnlyCollection<TEnum> MapStringArrayToEnumReadOnlyCollection<TEnum>(string[] src, IMapperApplier map) => map.Map<TEnum[]>(src);

    public static T[] MapObjectArrayToFilteredArray<T>(object[] src, IMapperApplier map) where T : class
    {
        var list = new List<T>(src.Length);
        for (int i = 0; i < src.Length; i++)
        {
            var item = src[i];
            if (item == null || item is global::SharpIpp.Protocol.Models.NoValue) continue;
            var mapped = map.MapNullable<T>(item);
            if (mapped != null) list.Add(mapped);
        }
        return list.ToArray();
    }

    public static List<T> MapObjectArrayToFilteredList<T>(object[] src, IMapperApplier map) where T : class
    {
        var list = new List<T>(src.Length);
        for (int i = 0; i < src.Length; i++)
        {
            var item = src[i];
            if (item == null || item is global::SharpIpp.Protocol.Models.NoValue) continue;
            var mapped = map.MapNullable<T>(item);
            if (mapped != null) list.Add(mapped);
        }
        return list;
    }

    public static IEnumerable<IppAttribute> MapModelToIppAttributeEnumerable<T>(T src, IMapperApplier map)
    {
        return map.Map<List<IppAttribute>>(src);
    }

    public static IDictionary<string, IppAttribute[]> MapModelToIppDictionary<T>(T src, IMapperApplier map)
    {
        return map.Map<List<IppAttribute>>(src).ToIppDictionary();
    }

    public static Dictionary<string, IppAttribute[]> MapModelToConcreteIppDictionary<T>(T src, IMapperApplier map)
    {
        return (Dictionary<string, IppAttribute[]>)map.Map<List<IppAttribute>>(src).ToIppDictionary();
    }

    public static T[] MapAttributeGroupListToModelArray<T>(List<List<IppAttribute>> src, IMapperApplier map)
    {
        var arr = new T[src.Count];
        for (int i = 0; i < src.Count; i++)
            arr[i] = map.Map<T>(src[i].ToIppDictionary());
        return arr;
    }

    public static List<List<IppAttribute>> MapModelArrayToAttributeGroupList<T>(T[] src, IMapperApplier map)
    {
        var list = new List<List<IppAttribute>>(src.Length);
        for (int i = 0; i < src.Length; i++)
            list.Add(map.Map<List<IppAttribute>>(src[i]));
        return list;
    }

    public static IppValue<T> MapDictionaryToIppValue<T>(IDictionary<string, IppAttribute[]> src, IMapperApplier map)
    {
        if (src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand())
            return default;
        return new IppValue<T>(map.Map<T>(src));
    }

    public static IppValue<T> MapConcreteDictionaryToIppValue<T>(Dictionary<string, IppAttribute[]> src, IMapperApplier map)
    {
        if (src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand())
            return default;
        return new IppValue<T>(map.Map<T>(src));
    }

    public static IEnumerable<IppAttribute> MapIppValueModelToIppAttributeEnumerable<T>(IppValue<T> src, string attributeName, IMapperApplier map)
    {
        if (!src.IsValue)
            return new IppAttribute[] { new IppAttribute(Tag.NoValue, attributeName, NoValue.Instance) };
        return map.Map<IEnumerable<IppAttribute>>(src.Value);
    }

    public static List<IppAttribute> MapIppValueModelToIppAttributeList<T>(IppValue<T> src, string attributeName, IMapperApplier map)
    {
        if (!src.IsValue)
            return new List<IppAttribute> { new IppAttribute(Tag.NoValue, attributeName, NoValue.Instance) };
        return map.Map<List<IppAttribute>>(src.Value);
    }
}
