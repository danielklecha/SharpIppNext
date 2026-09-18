using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

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
        mapper.CreateIppMap<byte[], string>((src, map) => System.Text.Encoding.UTF8.GetString(src));
        mapper.CreateIppMap<string, byte[]>((src, map) => System.Text.Encoding.UTF8.GetBytes(src));

        var unixStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);
        mapper.CreateIppMap<int, DateTime>((src, map) => unixStartTime.AddSeconds(src));
        mapper.CreateIppMap<DateTime, int>((src, map) => (src - unixStartTime).Seconds);

        mapper.CreateIppMap<NoValue, bool>((src, map) => NoValue.GetNoValue<bool>());
    }

    private static void ConfigureUri(IMapperConstructor mapper)
    {
        mapper.CreateMap<string, Uri?>((src, dst, map) =>
        {
            if (Uri.TryCreate(src, UriKind.RelativeOrAbsolute, out var uri))
            {
                return uri;
            }
            return null;
        });

        mapper.CreateMap<Uri, string>((src, dst, map) => src.ToString());
        mapper.CreateMap<NoValue, Uri?>((src, dst, map) => null);
    }
}