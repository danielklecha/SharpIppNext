using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

/// <summary>
/// Configures mappings for standard BCL / .NET core types (e.g., DateTime, Uri, byte[], string).
/// </summary>
[MapperConfiguration(0)]
internal static class CoreTypeMappers
{
    public static void Configure(IMapperConstructor mapper)
    {
        ConfigureTypes(mapper);
        ConfigureUri(mapper);
    }

    private static void ConfigureTypes(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<byte[], string>(CoreTypeMappingHelper.Utf8BytesToString);
        mapper.CreateIppMap<string, byte[]>(CoreTypeMappingHelper.StringToUtf8Bytes);
        mapper.CreateIppMap<int, DateTime>(CoreTypeMappingHelper.UnixSecondsToDateTime);
        mapper.CreateIppMap<DateTime, int>(CoreTypeMappingHelper.DateTimeToUnixSeconds);
    }

    private static void ConfigureUri(IMapperConstructor mapper)
    {
        mapper.CreateMap<string, Uri?>(CoreTypeMappingHelper.StringToUri);
        mapper.CreateMap<NoValue, Uri?>(CoreTypeMappingHelper.NoValueToUri);
        mapper.CreateMap<Uri, string>(CoreTypeMappingHelper.UriToString);
    }
}