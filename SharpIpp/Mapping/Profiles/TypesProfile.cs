using System;
using System.Linq;
using System.Reflection;
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
        mapper.CreateIppMap<byte[]>();

        mapper.CreateIppMap<byte[], string>((src, map) => System.Text.Encoding.UTF8.GetString(src));
        mapper.CreateIppMap<string, byte[]>((src, map) => System.Text.Encoding.UTF8.GetBytes(src));

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
        mapper.CreateIppMap<int, DocumentState>((src, map) => (DocumentState)src);
        mapper.CreateIppMap<int, ClientType>((src, map) => (ClientType)src);
        mapper.CreateIppMap<int, Orientation>((src, map) => (Orientation)src);
        mapper.CreateIppMap<int, PrinterState>((src, map) => (PrinterState)src);
        mapper.CreateIppMap<int, ResourceState>((src, map) => (ResourceState)src);
        mapper.CreateIppMap<int, PrintQuality>((src, map) => (PrintQuality)src);
        mapper.CreateIppMap<int, ResolutionUnit>((src, map) => (ResolutionUnit)src);
        mapper.CreateIppMap<int, PrinterType>((src, map) => (PrinterType)src);
        mapper.CreateIppMap<int, Protocol.Models.Range>((src, map) => new Protocol.Models.Range(src, src));
        mapper.CreateIppMap<string, IppVersion>((src, map) => new IppVersion(src));
        mapper.CreateIppMap<NoValue, int>((src, map) => NoValue.GetNoValue<int>());
        mapper.CreateIppMap<NoValue, bool>((src, map) => NoValue.GetNoValue<bool>());
        mapper.CreateIppMap<NoValue, JobState>((src, map) => NoValue.GetNoValue<JobState>());
        mapper.CreateIppMap<NoValue, DocumentState>((src, map) => NoValue.GetNoValue<DocumentState>());
        mapper.CreateIppMap<NoValue, PrinterState>((src, map) => NoValue.GetNoValue<PrinterState>());
        mapper.CreateIppMap<NoValue, Finishings>((src, map) => NoValue.GetNoValue<Finishings>());
        mapper.CreateIppMap<NoValue, IppStatusCode>((src, map) => NoValue.GetNoValue<IppStatusCode>());
        mapper.CreateIppMap<NoValue, Orientation>((src, map) => NoValue.GetNoValue<Orientation>());
        mapper.CreateIppMap<NoValue, PrintQuality>((src, map) => NoValue.GetNoValue<PrintQuality>());
        mapper.CreateIppMap<NoValue, ResolutionUnit>((src, map) => NoValue.GetNoValue<ResolutionUnit>());
        mapper.CreateIppMap<NoValue, PrinterType>((src, map) => NoValue.GetNoValue<PrinterType>());
        mapper.CreateIppMap<NoValue, IppOperation>((src, map) => NoValue.GetNoValue<IppOperation>());
        mapper.CreateIppMap<NoValue, DateTime>((src, map) => NoValue.GetNoValue<DateTime>());
        mapper.CreateIppMap<NoValue, DateTimeOffset>((src, map) => NoValue.GetNoValue<DateTimeOffset>());
        mapper.CreateIppMap<NoValue, Protocol.Models.Range>((src, map) => NoValue.GetNoValue<Protocol.Models.Range>());
        mapper.CreateIppMap<NoValue, Resolution>((src, map) => NoValue.GetNoValue<Resolution>());
        mapper.CreateIppMap<NoValue, StringWithLanguage>((src, map) => NoValue.GetNoValue<StringWithLanguage>());
        mapper.CreateIppMap<NoValue, string>(
            (src, map) => NoValue.GetNoValue<string>());

        //All name parameters can come as StringWithLanguage or string
        //mappers for string\language mapping 
        mapper.CreateIppMap<StringWithLanguage, string>((src, map) => src.Value);
        mapper.CreateIppMap<string, StringWithLanguage?>((src, map) => new StringWithLanguage("en", src));

        ConfigureSmartEnums(mapper);
    }

    private static void ConfigureSmartEnums(IMapperConstructor map)
    {
        var method = typeof(TypesProfile).GetMethod(nameof(ConfigureSmartEnum), BindingFlags.NonPublic | BindingFlags.Static);
        if (method != null)
        {
            var assembly = typeof(ISmartEnum).Assembly;
            var smartEnumTypes = assembly.GetTypes()
                .Where(type => typeof(ISmartEnum).IsAssignableFrom(type) && type is { IsAbstract: false, IsInterface: false, IsValueType: true })
                .OrderBy(type => type.FullName);

            foreach (var recordType in smartEnumTypes)
                method.MakeGenericMethod(recordType).Invoke(null, new object[] { map });
        }
    }

    private static void ConfigureSmartEnum<T>(IMapperConstructor map) where T : struct
    {
        var smartEnumType = typeof(T);
        if (typeof(IKeywordSmartEnum).IsAssignableFrom(smartEnumType))
        {
            var threeArgumentConstructor = smartEnumType.GetConstructor([typeof(string), typeof(bool), typeof(bool)])
                ?? throw new MissingMethodException($"No (string, bool, bool) constructor found for keyword smart enum type '{smartEnumType.FullName}'.");

            map.CreateIppMap<string, T>((src, ctx) => (T)threeArgumentConstructor.Invoke([src, true, true]));
        }
        else
        {
            var twoArgumentConstructor = smartEnumType.GetConstructor([typeof(string), typeof(bool)])
                ?? throw new MissingMethodException($"No (string, bool) constructor found for smart enum type '{smartEnumType.FullName}'.");

            map.CreateIppMap<string, T>((src, ctx) => (T)twoArgumentConstructor.Invoke([src, true]));
        }

        map.CreateIppMap<T, string>((src, ctx) => src.ToString()!);
        map.CreateIppMap<NoValue, T>((src, ctx) => NoValue.GetNoValue<T>());
    }
}
