using System;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class TypesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<int>();
        mapper.CreateIppMap<bool>();
        mapper.CreateIppMap<string>();
        mapper.CreateIppMap<DateTimeOffset>();
        mapper.CreateIppMap<Protocol.Models.Range>();
        mapper.CreateIppMap<Resolution>();
        mapper.CreateIppMap<StringWithLanguage>();

        var unixStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);
        mapper.CreateIppMap<int, DateTime>((src, map) => unixStartTime.AddSeconds(src));
        mapper.CreateIppMap<DateTime, int>((src, map) => (src - unixStartTime).Seconds);
        mapper.CreateIppMap<int, IppOperation>((src, map) => (IppOperation)(short)src);
        mapper.CreateIppMap<int, Finishings>((src, map) => (Finishings)src);
        mapper.CreateIppMap<int, IppStatusCode>((src, map) => (IppStatusCode)src);
        mapper.CreateIppMap<int, JobState>((src, map) => (JobState)src);
        mapper.CreateIppMap<int, Orientation>((src, map) => (Orientation)src);
        mapper.CreateIppMap<int, PrinterState>((src, map) => (PrinterState)src);
        mapper.CreateIppMap<int, PrintQuality>((src, map) => (PrintQuality)src);
        mapper.CreateIppMap<int, ResolutionUnit>((src, map) => (ResolutionUnit)src);
        mapper.CreateIppMap<int, PrinterType>((src, map) => (PrinterType)src);
        mapper.CreateIppMap<string, IppVersion>((src, map) => new IppVersion(src));
        mapper.CreateIppMap<NoValue, int>((src, map) => NoValue.GetNoValue<int>());
        mapper.CreateIppMap<NoValue, int?>((src, map) => NoValue.GetNoValue<int?>());
        mapper.CreateIppMap<NoValue, JobState>((src, map) => NoValue.GetNoValue<JobState>());
        mapper.CreateIppMap<NoValue, PrinterState>((src, map) => NoValue.GetNoValue<PrinterState>());
        mapper.CreateIppMap<NoValue, Finishings>((src, map) => NoValue.GetNoValue<Finishings>());
        mapper.CreateIppMap<NoValue, IppStatusCode>((src, map) => NoValue.GetNoValue<IppStatusCode>());
        mapper.CreateIppMap<NoValue, Orientation>((src, map) => NoValue.GetNoValue<Orientation>());
        mapper.CreateIppMap<NoValue, PrintQuality>((src, map) => NoValue.GetNoValue<PrintQuality>());
        mapper.CreateIppMap<NoValue, ResolutionUnit>((src, map) => NoValue.GetNoValue<ResolutionUnit>());
        mapper.CreateIppMap<NoValue, PrinterType>((src, map) => NoValue.GetNoValue<PrinterType>());
        mapper.CreateIppMap<NoValue, IppOperation>((src, map) => NoValue.GetNoValue<IppOperation>());
        mapper.CreateIppMap<NoValue, DateTime>((src, map) => NoValue.GetNoValue<DateTime>());
        mapper.CreateIppMap<NoValue, DateTime?>((src, map) => NoValue.GetNoValue<DateTime?>());
        mapper.CreateIppMap<NoValue, DateTimeOffset>((src, map) => NoValue.GetNoValue<DateTimeOffset>());
        mapper.CreateIppMap<NoValue, DateTimeOffset?>((src, map) => NoValue.GetNoValue<DateTimeOffset?>());
        mapper.CreateIppMap<NoValue, Protocol.Models.Range>((src, map) => NoValue.GetNoValue<Protocol.Models.Range>());
        mapper.CreateIppMap<NoValue, Protocol.Models.Range?>((src, map) => NoValue.GetNoValue<Protocol.Models.Range?>());
        mapper.CreateIppMap<NoValue, Resolution>((src, map) => NoValue.GetNoValue<Resolution>());
        mapper.CreateIppMap<NoValue, Resolution?>((src, map) => NoValue.GetNoValue<Resolution?>());
        mapper.CreateIppMap<NoValue, StringWithLanguage>((src, map) => NoValue.GetNoValue<StringWithLanguage>());
        mapper.CreateIppMap<NoValue, StringWithLanguage?>((src, map) => NoValue.GetNoValue<StringWithLanguage?>());
        mapper.CreateIppMap<NoValue, string>((src, map) => NoValue.GetNoValue<string>());
        mapper.CreateIppMap<NoValue, string?>((src, map) => NoValue.GetNoValue<string?>());

        //All name parameters can come as StringWithLanguage or string
        //mappers for string\language mapping 
        mapper.CreateIppMap<StringWithLanguage, string>((src, map) => src.Value);
        mapper.CreateIppMap<string, StringWithLanguage?>((src, map) => new StringWithLanguage("en", src));

        ConfigureKeyword<JobHoldUntil>(mapper);
        ConfigureKeyword<MultipleDocumentHandling>(mapper);
        ConfigureKeyword<Sides>(mapper);
        ConfigureKeyword<JobSheets>(mapper);
        ConfigureKeyword<Compression>(mapper);
        ConfigureKeyword<PrintScaling>(mapper);
        ConfigureKeyword<WhichJobs>(mapper);
        ConfigureKeyword<JobStateReason>(mapper);
        ConfigureKeyword<UriScheme>(mapper);
        ConfigureKeyword<UriAuthentication>(mapper);
        ConfigureKeyword<UriSecurity>(mapper);
        ConfigureKeyword<MediaSource>(mapper);
        ConfigureKeyword<MediaSourceFeedDirection>(mapper);
        ConfigureKeyword<MediaCoating>(mapper);
        ConfigureKeyword<MediaGrain>(mapper);
        ConfigureKeyword<MediaPrePrinted>(mapper);
        ConfigureKeyword<MediaRecycled>(mapper);
        ConfigureKeyword<MediaTooth>(mapper);
        ConfigureKeyword<PrintColorMode>(mapper);
    }

    private static void ConfigureKeyword<T>(IMapperConstructor map) where T : struct, Enum
    {
        map.CreateIppMap<string, T>((src, ctx) => Enum.TryParse(src.ConvertKebabCaseToCamelCase(), false, out T value) ? value : NoValue.GetNoValue<T>());
        map.CreateIppMap<T, string>((src, ctx) => src.ToString().ConvertCamelCaseToKebabCase());
        map.CreateIppMap<NoValue, T>((src, ctx) => NoValue.GetNoValue<T>());
    }
}
