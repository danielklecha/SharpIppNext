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
        mapper.CreateIppMap<int, TransmissionStatus>((src, map) => (TransmissionStatus)src);
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
        mapper.CreateIppMap<NoValue, TransmissionStatus>((src, map) => NoValue.GetNoValue<TransmissionStatus>());
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
          ConfigureSmartEnum<AccuracyUnits>(map);
          ConfigureSmartEnum<BalingType>(map);
          ConfigureSmartEnum<BalingWhen>(map);
          ConfigureSmartEnum<BindingType>(map);
          ConfigureSmartEnum<CapacityUnit>(map);
          ConfigureSmartEnum<Charset>(map);
          ConfigureSmartEnum<ClientInfoMember>(map);
          ConfigureSmartEnum<CoatingSides>(map);
          ConfigureSmartEnum<CoatingType>(map);
          ConfigureSmartEnum<Compression>(map);
          ConfigureSmartEnum<CoveringName>(map);
          ConfigureSmartEnum<CoverMember>(map);
          ConfigureSmartEnum<CoverSheetInfoMember>(map);
          ConfigureSmartEnum<CoverType>(map);
          ConfigureSmartEnum<CurrentPageOrder>(map);
          ConfigureSmartEnum<DestinationAccessMember>(map);
          ConfigureSmartEnum<DestinationUrisMember>(map);
          ConfigureSmartEnum<DocumentAccessMember>(map);
          ConfigureSmartEnum<DocumentCreationAttribute>(map);
          ConfigureSmartEnum<DocumentDigitalSignature>(map);
          ConfigureSmartEnum<DocumentFormat>(map);
          ConfigureSmartEnum<DocumentFormatDetail>(map);
          ConfigureSmartEnum<DocumentStateReason>(map);
          ConfigureSmartEnum<FeedOrientation>(map);
          ConfigureSmartEnum<FetchDocumentAttribute>(map);
          ConfigureSmartEnum<FinisherSupplyClass>(map);
          ConfigureSmartEnum<FinisherSupplyType>(map);
          ConfigureSmartEnum<FinisherType>(map);
          ConfigureSmartEnum<FinishingReferenceEdge>(map);
          ConfigureSmartEnum<FinishingsColMember>(map);
          ConfigureSmartEnum<FinishingTemplate>(map);
          ConfigureSmartEnum<FoldingDirection>(map);
          ConfigureSmartEnum<IdentifyAction>(map);
          ConfigureSmartEnum<ImpositionTemplate>(map);
          ConfigureSmartEnum<InputAttributesMember>(map);
          ConfigureSmartEnum<InputColorMode>(map);
          ConfigureSmartEnum<InputContentType>(map);
          ConfigureSmartEnum<InputFilmScanMode>(map);
          ConfigureSmartEnum<InputSource>(map);
          ConfigureSmartEnum<InputTrayType>(map);
          ConfigureSmartEnum<InsertSheetMember>(map);
          ConfigureSmartEnum<IppFeature>(map);
          ConfigureSmartEnum<JobAccountingSheetsMember>(map);
          ConfigureSmartEnum<JobAccountingSheetsType>(map);
          ConfigureSmartEnum<JobAccountType>(map);
          ConfigureSmartEnum<JobCompleteBefore>(map);
          ConfigureSmartEnum<JobCreationAttribute>(map);
          ConfigureSmartEnum<JobErrorAction>(map);
          ConfigureSmartEnum<JobErrorSheetMember>(map);
          ConfigureSmartEnum<JobErrorSheetType>(map);
          ConfigureSmartEnum<JobErrorSheetWhen>(map);
          ConfigureSmartEnum<JobHistoryAttribute>(map);
          ConfigureSmartEnum<JobHoldUntil>(map);
          ConfigureSmartEnum<JobPasswordEncryption>(map);
          ConfigureSmartEnum<JobPhoneNumberScheme>(map);
          ConfigureSmartEnum<JobReleaseAction>(map);
          ConfigureSmartEnum<JobSheets>(map);
          ConfigureSmartEnum<JobSheetsColMember>(map);
          ConfigureSmartEnum<JobSheetsType>(map);
          ConfigureSmartEnum<JobSpooling>(map);
          ConfigureSmartEnum<JobStateReason>(map);
          ConfigureSmartEnum<JobStorageAccess>(map);
          ConfigureSmartEnum<JobStorageDisposition>(map);
          ConfigureSmartEnum<LaminatingType>(map);
          ConfigureSmartEnum<MarkerType>(map);
          ConfigureSmartEnum<MaterialAmountUnits>(map);
          ConfigureSmartEnum<MaterialColor>(map);
          ConfigureSmartEnum<MaterialKey>(map);
          ConfigureSmartEnum<MaterialPurpose>(map);
          ConfigureSmartEnum<MaterialRateUnits>(map);
          ConfigureSmartEnum<MaterialsColMember>(map);
          ConfigureSmartEnum<MaterialType>(map);
          ConfigureSmartEnum<Media>(map);
          ConfigureSmartEnum<MediaCoating>(map);
          ConfigureSmartEnum<MediaColMember>(map);
          ConfigureSmartEnum<MediaColor>(map);
          ConfigureSmartEnum<MediaGrain>(map);
          ConfigureSmartEnum<MediaInputTrayCheck>(map);
          ConfigureSmartEnum<MediaKey>(map);
          ConfigureSmartEnum<MediaPrePrinted>(map);
          ConfigureSmartEnum<MediaRecycled>(map);
          ConfigureSmartEnum<MediaSource>(map);
          ConfigureSmartEnum<MediaSourceFeedDirection>(map);
          ConfigureSmartEnum<MediaTooth>(map);
          ConfigureSmartEnum<MediaType>(map);
          ConfigureSmartEnum<MultipleDocumentHandling>(map);
          ConfigureSmartEnum<MultipleObjectHandling>(map);
          ConfigureSmartEnum<MultipleOperationTimeOutAction>(map);
          ConfigureSmartEnum<NaturalLanguage>(map);
          ConfigureSmartEnum<NotifyEvent>(map);
          ConfigureSmartEnum<NotifyPullMethod>(map);
          ConfigureSmartEnum<OutputAttributesMember>(map);
          ConfigureSmartEnum<OutputBin>(map);
          ConfigureSmartEnum<OutputDevice>(map);
          ConfigureSmartEnum<OutputTrayType>(map);
          ConfigureSmartEnum<OverrideSupported>(map);
          ConfigureSmartEnum<PageDelivery>(map);
          ConfigureSmartEnum<PageOrderReceived>(map);
          ConfigureSmartEnum<PdfFeature>(map);
          ConfigureSmartEnum<PdfVersion>(map);
          ConfigureSmartEnum<PdlOverride>(map);
          ConfigureSmartEnum<PlatformShape>(map);
          ConfigureSmartEnum<PwgRasterDocumentSheetBack>(map);
          ConfigureSmartEnum<Repertoire>(map);
          ConfigureSmartEnum<PowerState>(map);
          ConfigureSmartEnum<PresentationDirectionNumberUp>(map);
          ConfigureSmartEnum<PresentOnOff>(map);
          ConfigureSmartEnum<PrintBase>(map);
          ConfigureSmartEnum<PrintColorMode>(map);
          ConfigureSmartEnum<PrintContentOptimize>(map);
          ConfigureSmartEnum<PrinterCreationAttribute>(map);
          ConfigureSmartEnum<PrinterMandatoryJobAttribute>(map);
          ConfigureSmartEnum<PrinterMode>(map);
          ConfigureSmartEnum<PrinterRequestedJobAttribute>(map);
          ConfigureSmartEnum<PrinterServiceType>(map);
          ConfigureSmartEnum<PrinterStateReason>(map);
          ConfigureSmartEnum<PrinterSupplyType>(map);
          ConfigureSmartEnum<PrintObjectsMember>(map);
          ConfigureSmartEnum<PrintRenderingIntent>(map);
          ConfigureSmartEnum<PrintScaling>(map);
          ConfigureSmartEnum<PrintSupports>(map);
          ConfigureSmartEnum<ResourceFormat>(map);
          ConfigureSmartEnum<ResourceSettableAttribute>(map);
          ConfigureSmartEnum<ResourceStateReason>(map);
          ConfigureSmartEnum<ResourceType>(map);
          ConfigureSmartEnum<SaveDisposition>(map);
          ConfigureSmartEnum<SeparatorSheetsMember>(map);
          ConfigureSmartEnum<SeparatorSheetsType>(map);
          ConfigureSmartEnum<SheetCollate>(map);
          ConfigureSmartEnum<Sides>(map);
          ConfigureSmartEnum<StackingOrder>(map);
          ConfigureSmartEnum<StitchingMethod>(map);
          ConfigureSmartEnum<SystemMandatoryPrinterAttribute>(map);
          ConfigureSmartEnum<SystemMandatoryRegistrationAttribute>(map);
          ConfigureSmartEnum<SystemSettableAttribute>(map);
          ConfigureSmartEnum<SystemStateReason>(map);
          ConfigureSmartEnum<SystemTimeSourceConfigured>(map);
          ConfigureSmartEnum<TrimmingType>(map);
          ConfigureSmartEnum<TrimmingWhen>(map);
          ConfigureSmartEnum<UriAuthentication>(map);
          ConfigureSmartEnum<UriScheme>(map);
          ConfigureSmartEnum<UriSecurity>(map);
          ConfigureSmartEnum<WhichJobs>(map);
          ConfigureSmartEnum<WhichPrinters>(map);
          ConfigureSmartEnum<X509Type>(map);
          ConfigureSmartEnum<XImagePosition>(map);
          ConfigureSmartEnum<YImagePosition>(map);
    }

    private static void ConfigureSmartEnum<[System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IMapperConstructor map) where T : struct
    {
        var smartEnumType = typeof(T);
        if (typeof(IMarkedSmartEnum).IsAssignableFrom(smartEnumType))
        {
            var threeArgumentConstructor = smartEnumType.GetConstructor([typeof(string), typeof(bool), typeof(bool)])
                ?? throw new MissingMethodException($"No (string, bool, bool) constructor found for marked smart enum type '{smartEnumType.FullName}'.");

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
