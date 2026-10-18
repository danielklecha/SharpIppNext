using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Protocol.Models;

internal static class IppValueHelper
{
    internal static bool ArrayEquals(Array left, Array right)
    {
        if (left.Length != right.Length) return false;
        for (int i = 0; i < left.Length; i++)
        {
            if (!Equals(left.GetValue(i), right.GetValue(i)))
                return false;
        }
        return true;
    }

    internal static int ArrayGetHashCode(Array arr)
    {
        int hash = 17;
        foreach (var item in arr)
        {
            hash = hash * 31 + (item?.GetHashCode() ?? 0);
        }
        return hash;
    }

    internal static IppValue<T> FromValue<T>(T src, SharpIpp.Mapping.IMapperApplier _) => new(src);
    internal static IppValue<T> FromNoValue<T>(NoValue _, SharpIpp.Mapping.IMapperApplier __) => default;
    internal static IppValue<T> FromString<T>(string src, SharpIpp.Mapping.IMapperApplier map) => new(map.Map<T>(src));
}
