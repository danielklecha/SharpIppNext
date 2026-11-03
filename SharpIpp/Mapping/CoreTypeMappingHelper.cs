using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

internal static class CoreTypeMappingHelper
{
    private static readonly DateTime UnixStartTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);

    public static string Utf8BytesToString(byte[] src, IMapperApplier _) => System.Text.Encoding.UTF8.GetString(src);
    public static byte[] StringToUtf8Bytes(string src, IMapperApplier _) => System.Text.Encoding.UTF8.GetBytes(src);
    public static DateTime UnixSecondsToDateTime(int src, IMapperApplier _) => UnixStartTime.AddSeconds(src);
    public static int DateTimeToUnixSeconds(DateTime src, IMapperApplier _) => (src - UnixStartTime).Seconds;

    public static Uri? StringToUri(string src, Uri? _, IMapperApplier __)
    {
        return Uri.TryCreate(src, UriKind.RelativeOrAbsolute, out var uri) ? uri : null;
    }

    public static Uri? NoValueToUri(NoValue _, Uri? __, IMapperApplier ___) => null;
    public static string UriToString(Uri src, IMapperApplier _) => src.ToString();
}

