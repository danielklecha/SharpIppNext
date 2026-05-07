using System.Collections.Generic;
using System.Linq;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Extensions;

internal static class IppDictionaryExtensions
{
    public static bool IsOutOfBandNoValue(this IDictionary<string, IppAttribute[]> src)
    {
        return src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand();
    }
}
