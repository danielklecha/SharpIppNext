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
        mapper.CreateIppMap<int, DocumentState>((src, map) => (DocumentState)src);
        mapper.CreateIppMap<int, Orientation>((src, map) => (Orientation)src);
        mapper.CreateIppMap<int, PrinterState>((src, map) => (PrinterState)src);
        mapper.CreateIppMap<int, PrintQuality>((src, map) => (PrintQuality)src);
        mapper.CreateIppMap<int, ResolutionUnit>((src, map) => (ResolutionUnit)src);
        mapper.CreateIppMap<int, PrinterType>((src, map) => (PrinterType)src);
        mapper.CreateIppMap<int, Protocol.Models.Range>((src, map) => new Protocol.Models.Range(src, src));
        mapper.CreateIppMap<string, IppVersion>((src, map) => new IppVersion(src));
        mapper.CreateIppMap<NoValue, int>((src, map) => NoValue.GetNoValue<int>());
        mapper.CreateIppMap<NoValue, int?>((src, map) => NoValue.GetNoValue<int?>());
        mapper.CreateIppMap<NoValue, bool>((src, map) => NoValue.GetNoValue<bool>());
        mapper.CreateIppMap<NoValue, bool?>((src, map) => NoValue.GetNoValue<bool?>());
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

        ConfigureSmartEnum<JobHoldUntil>(mapper);
        ConfigureSmartEnum<MultipleDocumentHandling>(mapper);
        ConfigureSmartEnum<Sides>(mapper);
        ConfigureSmartEnum<JobSheets>(mapper);
        ConfigureSmartEnum<Compression>(mapper);
        ConfigureSmartEnum<PrintScaling>(mapper);
        ConfigureSmartEnum<WhichJobs>(mapper);
        ConfigureSmartEnum<JobStateReason>(mapper);
        ConfigureSmartEnum<UriScheme>(mapper);
        ConfigureSmartEnum<UriAuthentication>(mapper);
        ConfigureSmartEnum<UriSecurity>(mapper);
        ConfigureSmartEnum<MediaSource>(mapper);
        ConfigureSmartEnum<MediaSourceFeedDirection>(mapper);
        ConfigureSmartEnum<MediaCoating>(mapper);
        ConfigureSmartEnum<MediaGrain>(mapper);
        ConfigureSmartEnum<MediaPrePrinted>(mapper);
        ConfigureSmartEnum<MediaRecycled>(mapper);
        ConfigureSmartEnum<MediaTooth>(mapper);
        ConfigureSmartEnum<PrintColorMode>(mapper);
        ConfigureSmartEnum<DocumentStateReason>(mapper);
        ConfigureSmartEnum<FinishingReferenceEdge>(mapper);
        ConfigureSmartEnum<BalingWhen>(mapper);
        ConfigureSmartEnum<CoatingSides>(mapper);
        ConfigureSmartEnum<FoldingDirection>(mapper);
        ConfigureSmartEnum<StitchingMethod>(mapper);
        ConfigureSmartEnum<TrimmingType>(mapper);
        ConfigureSmartEnum<TrimmingWhen>(mapper);
        ConfigureSmartEnum<JobErrorSheetWhen>(mapper);
        ConfigureSmartEnum<PageDelivery>(mapper);
        ConfigureSmartEnum<PageOrderReceived>(mapper);
        ConfigureSmartEnum<PresentationDirectionNumberUp>(mapper);
        ConfigureSmartEnum<XImagePosition>(mapper);
        ConfigureSmartEnum<YImagePosition>(mapper);
        ConfigureSmartEnum<CoverType>(mapper);
        ConfigureSmartEnum<PrinterStateReason>(mapper);
        ConfigureSmartEnum<PdlOverride>(mapper);
        ConfigureSmartEnum<JobSpooling>(mapper);
        ConfigureSmartEnum<PrintContentOptimize>(mapper);
        ConfigureSmartEnum<BalingType>(mapper);
        ConfigureSmartEnum<BindingType>(mapper);
        ConfigureSmartEnum<CoatingType>(mapper);
        ConfigureSmartEnum<CoveringName>(mapper);
        ConfigureSmartEnum<LaminatingType>(mapper);
        ConfigureSmartEnum<MediaInputTrayCheck>(mapper);
        ConfigureSmartEnum<JobSheetsType>(mapper);
        ConfigureSmartEnum<SeparatorSheetsType>(mapper);
        ConfigureSmartEnum<CurrentPageOrder>(mapper);

        ConfigureSmartEnum<OutputBin>(mapper);
        ConfigureSmartEnum<ImpositionTemplate>(mapper);
        ConfigureSmartEnum<JobPhoneNumberScheme>(mapper);
        ConfigureSmartEnum<FinishingTemplate>(mapper);
        ConfigureSmartEnum<Media>(mapper);
        ConfigureSmartEnum<MediaColor>(mapper);
        ConfigureSmartEnum<MediaType>(mapper);
    }

    private static void ConfigureSmartEnum<T>(IMapperConstructor map) where T : struct
    {
        map.CreateIppMap<string, T>((src, ctx) => (T)Activator.CreateInstance(typeof(T), src)!);
        map.CreateIppMap<T, string>((src, ctx) => src.ToString()!);
        map.CreateIppMap<NoValue, T>((src, ctx) => NoValue.GetNoValue<T>());
        map.CreateIppMap<object[], T[]>((src, ctx) => src.Select(x => ctx.Map<T>(x)).ToArray());
        map.CreateIppMap<string[], T[]>((src, ctx) => src.Select(x => ctx.Map<T>(x)).ToArray());
    }
}
