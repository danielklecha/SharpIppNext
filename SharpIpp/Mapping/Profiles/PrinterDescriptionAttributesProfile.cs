using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles;

internal class PrinterDescriptionAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<List<List<IppAttribute>>, PrinterDescriptionAttributes[]>((src, map) =>
            src.Select(x => map.Map<PrinterDescriptionAttributes>(x.ToIppDictionary()))
                .ToArray());

        mapper.CreateMap<PrinterDescriptionAttributes[], List<List<IppAttribute>>>((src, map) =>
        {
            return src.Select(x =>
            {
                var attrs = new List<IppAttribute>();
                attrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(x).Values.SelectMany(v => v));
                return attrs;
            }).ToList();
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>((src, map) =>
        {
            var result = new PrinterDescriptionAttributes
            {
                CharsetConfigured = map.MapFromDicNullable<string?>(src, IppAttributeNames.CharsetConfigured),
                CharsetSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.CharsetSupported),
                ColorSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.ColorSupported),
                CompressionDefault = map.MapFromDicNullable<Compression?>(src, IppAttributeNames.CompressionDefault),
                CompressionSupported = map.MapFromDicSetNullable<Compression[]?>(src, IppAttributeNames.CompressionSupported),
                DocumentFormatDefault = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormatDefault),
                DocumentFormatSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DocumentFormatSupported),
                ClientInfoSupported = map.MapFromDicSetNullable<ClientInfoMember[]?>(src, IppAttributeNames.ClientInfoSupported),
                MaxClientInfoSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.MaxClientInfoSupported),
                DocumentCharsetDefault = map.MapFromDicNullable<Charset?>(src, IppAttributeNames.DocumentCharsetDefault),
                DocumentCharsetSupported = map.MapFromDicSetNullable<Charset[]?>(src, IppAttributeNames.DocumentCharsetSupported),
                DocumentFormatDetailsSupported = map.MapFromDicSetNullable<DocumentFormatDetail[]?>(src, IppAttributeNames.DocumentFormatDetailsSupported),
                DocumentNaturalLanguageDefault = map.MapFromDicNullable<NaturalLanguage?>(src, IppAttributeNames.DocumentNaturalLanguageDefault),
                DocumentNaturalLanguageSupported = map.MapFromDicSetNullable<NaturalLanguage[]?>(src, IppAttributeNames.DocumentNaturalLanguageSupported),
                JobIdsSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobIdsSupported),
                JobMandatoryAttributesSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobMandatoryAttributesSupported),
                JobSheetsColSupported = map.MapFromDicSetNullable<JobSheetsColMember[]?>(src, IppAttributeNames.JobSheetsColSupported),
                GeneratedNaturalLanguageSupported = map.MapFromDicSetNullable<NaturalLanguage[]?>(src, IppAttributeNames.GeneratedNaturalLanguageSupported),
                IppVersionsSupported = map.MapFromDicSetNullable<IppVersion[]?>(src, IppAttributeNames.IppVersionsSupported),
                JobImpressionsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobImpressionsSupported),
                JobKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobKOctetsSupported),
                JpegKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JpegKOctetsSupported),
                PdfKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.PdfKOctetsSupported),
                JobMediaSheetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobMediaSheetsSupported),
                JobSheetsDefault = map.MapFromDicNullable<JobSheets?>(src, IppAttributeNames.JobSheetsDefault),
                JobSheetsSupported = map.MapFromDicSetNullable<JobSheets[]?>(src, IppAttributeNames.JobSheetsSupported),
                NumberUpDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberUpDefault),
                NumberUpSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.NumberUpSupported),
                MultipleDocumentJobsSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.MultipleDocumentJobsSupported),
                MultipleDocumentHandlingDefault = map.MapFromDicNullable<MultipleDocumentHandling?>(src, IppAttributeNames.MultipleDocumentHandlingDefault),
                MultipleDocumentHandlingSupported = map.MapFromDicSetNullable<MultipleDocumentHandling[]?>(src, IppAttributeNames.MultipleDocumentHandlingSupported),
                MultipleOperationTimeOut = map.MapFromDicNullable<int?>(src, IppAttributeNames.MultipleOperationTimeOut),
                MultipleOperationTimeOutAction = map.MapFromDicNullable<MultipleOperationTimeOutAction?>(src, IppAttributeNames.MultipleOperationTimeOutAction),
                NaturalLanguageConfigured = map.MapFromDicNullable<NaturalLanguage?>(src, IppAttributeNames.NaturalLanguageConfigured),
                OperationsSupported = map.MapFromDicSetNullable<IppOperation[]?>(src, IppAttributeNames.OperationsSupported),
                PagesPerMinute = map.MapFromDicNullable<int?>(src, IppAttributeNames.PagesPerMinute),
                PdlOverrideSupported = map.MapFromDicNullable<PdlOverride?>(src, IppAttributeNames.PdlOverrideSupported),
                OverridesSupported = map.MapFromDicSetNullable<OverrideSupported[]?>(src, IppAttributeNames.OverridesSupported),
                PagesPerMinuteColor = map.MapFromDicNullable<int?>(src, IppAttributeNames.PagesPerMinuteColor),
                PrinterCurrentTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.PrinterCurrentTime),
                PrinterConfigChangeTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterConfigChangeTime),
                PrinterConfigChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.PrinterConfigChangeDateTime),
                PrinterConfigChanges = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterConfigChanges),
                PrinterGeoLocation = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterGeoLocation),
                PrinterIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.PrinterIds),
                PrinterImpressionsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterImpressionsCompleted),
                PrinterImpressionsCompletedCol = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterImpressionsCompletedCol),
                PrinterMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterMediaSheetsCompleted),
                PrinterMediaSheetsCompletedCol = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterMediaSheetsCompletedCol),
                PrinterPagesCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterPagesCompleted),
                PrinterPagesCompletedCol = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterPagesCompletedCol),
                PrinterDriverInstaller = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterDriverInstaller),
                PrinterInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, IppAttributeNames.PrinterIsAcceptingJobs),
                PrinterLocation = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterLocation),
                PrinterMakeAndModel = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterMakeAndModel),
                PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterMessageFromOperator),
                PrinterMoreInfo = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterMoreInfo),
                PrinterMoreInfoManufacturer = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterMoreInfoManufacturer),
                PrinterName = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterName),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, IppAttributeNames.PrinterState),
                PrinterStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterStateMessage),
                PrinterStateChangeTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterStateChangeTime),
                PrinterStateChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.PrinterStateChangeDateTime),
                PrinterDetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterDetailedStatusMessages),
                PrinterStateReasons = map.MapFromDicSetNullable<PrinterStateReason[]?>(src, IppAttributeNames.PrinterStateReasons),
                PrinterUpTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterUpTime),
                PrinterUriSupported = map.MapFromDicSetNullable<Uri[]?>(src, IppAttributeNames.PrinterUriSupported),
                PrintScalingDefault = map.MapFromDicNullable<PrintScaling?>(src, IppAttributeNames.PrintScalingDefault),
                PrintScalingSupported = map.MapFromDicSetNullable<PrintScaling[]?>(src, IppAttributeNames.PrintScalingSupported),
                QueuedJobCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.QueuedJobCount),
                ReferenceUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, IppAttributeNames.ReferenceUriSchemesSupported),
                UriAuthenticationSupported = map.MapFromDicSetNullable<UriAuthentication[]?>(src, IppAttributeNames.UriAuthenticationSupported),
                UriSecuritySupported = map.MapFromDicSetNullable<UriSecurity[]?>(src, IppAttributeNames.UriSecuritySupported),
                MediaDefault = map.MapFromDicNullable<string, Media?>(src, IppAttributeNames.MediaDefault, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                MediaSupported = map.MapFromDicSetNullable<string, Media>(src, IppAttributeNames.MediaSupported, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                MediaReady = map.MapFromDicSetNullable<string, Media>(src, IppAttributeNames.MediaReady, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                SidesDefault = map.MapFromDicNullable<Sides?>(src, IppAttributeNames.SidesDefault),
                SidesSupported = map.MapFromDicSetNullable<Sides[]?>(src, IppAttributeNames.SidesSupported),
                FinishingsDefault = map.MapFromDicNullable<Finishings?>(src, IppAttributeNames.FinishingsDefault),
                FinishingsSupported = map.MapFromDicSetNullable<Finishings[]?>(src, IppAttributeNames.FinishingsSupported),
                PrinterResolutionDefault = map.MapFromDicNullable<Resolution?>(src, IppAttributeNames.PrinterResolutionDefault),
                PrinterResolutionSupported = map.MapFromDicSetNullable<Resolution[]?>(src, IppAttributeNames.PrinterResolutionSupported),
                PrintQualityDefault = map.MapFromDicNullable<PrintQuality?>(src, IppAttributeNames.PrintQualityDefault),
                PrintQualitySupported = map.MapFromDicSetNullable<PrintQuality[]?>(src, IppAttributeNames.PrintQualitySupported),
                JobPriorityDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPriorityDefault),
                JobPrioritySupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPrioritySupported),
                CopiesDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.CopiesDefault),
                CopiesSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.CopiesSupported),
                OrientationRequestedDefault = map.MapFromDicNullable<Orientation?>(src, IppAttributeNames.OrientationRequestedDefault),
                OrientationRequestedSupported = map.MapFromDicSetNullable<Orientation[]?>(src, IppAttributeNames.OrientationRequestedSupported),
                PageRangesSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.PageRangesSupported),
                JobHoldUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, IppAttributeNames.JobHoldUntilDefault),
                JobHoldUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, IppAttributeNames.JobHoldUntilSupported),
                JobHoldUntilTimeSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobHoldUntilTimeSupported),
                JobDelayOutputUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, IppAttributeNames.JobDelayOutputUntilDefault),
                JobDelayOutputUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, IppAttributeNames.JobDelayOutputUntilSupported),
                JobDelayOutputUntilTimeSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobDelayOutputUntilTimeSupported),
                JobHistoryAttributesConfigured = map.MapFromDicSetNullable<JobHistoryAttribute[]?>(src, IppAttributeNames.JobHistoryAttributesConfigured),
                JobHistoryAttributesSupported = map.MapFromDicSetNullable<JobHistoryAttribute[]?>(src, IppAttributeNames.JobHistoryAttributesSupported),
                JobHistoryIntervalConfigured = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobHistoryIntervalConfigured),
                JobHistoryIntervalSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobHistoryIntervalSupported),
                JobRetainUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, IppAttributeNames.JobRetainUntilDefault),
                JobRetainUntilIntervalDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobRetainUntilIntervalDefault),
                JobRetainUntilIntervalSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobRetainUntilIntervalSupported),
                JobRetainUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, IppAttributeNames.JobRetainUntilSupported),
                JobRetainUntilTimeSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobRetainUntilTimeSupported),
                OutputBinDefault = map.MapFromDicNullable<string, OutputBin?>(src, IppAttributeNames.OutputBinDefault, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
                OutputBinSupported = map.MapFromDicSetNullable<string, OutputBin>(src, IppAttributeNames.OutputBinSupported, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
                MediaColSupported = map.MapFromDicSetNullable<MediaColMember[]?>(src, IppAttributeNames.MediaColSupported),
                MediaKeySupported = map.MapFromDicSetNullable<string, MediaKey>(src, IppAttributeNames.MediaKeySupported, (attribute, value) => new MediaKey(value, attribute.Tag == Tag.Keyword)),
                MediaSourceSupported = map.MapFromDicSetNullable<MediaSource[]?>(src, IppAttributeNames.MediaSourceSupported),
                MediaTypeSupported = map.MapFromDicSetNullable<MediaType[]?>(src, IppAttributeNames.MediaTypeSupported),
                MediaBackCoatingSupported = map.MapFromDicSetNullable<MediaCoating[]?>(src, IppAttributeNames.MediaBackCoatingSupported),
                MediaFrontCoatingSupported = map.MapFromDicSetNullable<MediaCoating[]?>(src, IppAttributeNames.MediaFrontCoatingSupported),
                MediaColorSupported = map.MapFromDicSetNullable<MediaColor[]?>(src, IppAttributeNames.MediaColorSupported),
                MediaGrainSupported = map.MapFromDicSetNullable<MediaGrain[]?>(src, IppAttributeNames.MediaGrainSupported),
                MediaToothSupported = map.MapFromDicSetNullable<MediaTooth[]?>(src, IppAttributeNames.MediaToothSupported),
                MediaPrePrintedSupported = map.MapFromDicSetNullable<MediaPrePrinted[]?>(src, IppAttributeNames.MediaPrePrintedSupported),
                MediaRecycledSupported = map.MapFromDicSetNullable<MediaRecycled[]?>(src, IppAttributeNames.MediaRecycledSupported),
                MediaHoleCountSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MediaHoleCountSupported),
                MediaOrderCountSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MediaOrderCountSupported),
                MediaThicknessSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MediaThicknessSupported),
                MediaWeightMetricSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MediaWeightMetricSupported),
                MediaBottomMarginSupported = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.MediaBottomMarginSupported),
                MediaLeftMarginSupported = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.MediaLeftMarginSupported),
                MediaRightMarginSupported = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.MediaRightMarginSupported),
                MediaTopMarginSupported = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.MediaTopMarginSupported),
                PrintColorModeDefault = map.MapFromDicNullable<PrintColorMode?>(src, IppAttributeNames.PrintColorModeDefault),
                PrintColorModeSupported = map.MapFromDicSetNullable<PrintColorMode[]?>(src, IppAttributeNames.PrintColorModeSupported),
                WhichJobsSupported = map.MapFromDicSetNullable<WhichJobs[]?>(src, IppAttributeNames.WhichJobsSupported),
                PrinterUUID = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterUUID),
                PdfVersionsSupported = map.MapFromDicSetNullable<PdfVersion[]?>(src, IppAttributeNames.PdfVersionsSupported),
                IppFeaturesSupported = map.MapFromDicSetNullable<IppFeature[]?>(src, IppAttributeNames.IppFeaturesSupported),
                DocumentCreationAttributesSupported = map.MapFromDicSetNullable<DocumentCreationAttribute[]?>(src, IppAttributeNames.DocumentCreationAttributesSupported),
                JobAccountIdDefault = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobAccountIdDefault),
                JobAccountTypeDefault = map.MapFromDicNullable<JobAccountType?>(src, IppAttributeNames.JobAccountTypeDefault),
                JobAccountTypeSupported = map.MapFromDicSetNullable<JobAccountType[]?>(src, IppAttributeNames.JobAccountTypeSupported),
                JobAccountIdSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobAccountIdSupported),
                JobAccountingUserIdDefault = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobAccountingUserIdDefault),
                JobAccountingUserIdSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobAccountingUserIdSupported),
                JobPasswordEncryptionSupported = map.MapFromDicSetNullable<JobPasswordEncryption[]?>(src, IppAttributeNames.JobPasswordEncryptionSupported),
                JobAuthorizationUriSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobAuthorizationUriSupported),
                PrinterChargeInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterChargeInfo),
                PrinterChargeInfoUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterChargeInfoUri),
                PrinterMandatoryJobAttributes = map.MapFromDicSetNullable<PrinterMandatoryJobAttribute[]?>(src, IppAttributeNames.PrinterMandatoryJobAttributes),
                PrinterRequestedJobAttributes = map.MapFromDicSetNullable<PrinterRequestedJobAttribute[]?>(src, IppAttributeNames.PrinterRequestedJobAttributes),
                PrinterAlert = map.MapFromDicSetNullable<PrinterAlert[]?>(src, IppAttributeNames.PrinterAlert),
                PrinterAlertDescription = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterAlertDescription),
                PrinterSupplyDescription = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterSupplyDescription),
                JobCancelAfterDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobCancelAfterDefault),
                JobCancelAfterSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobCancelAfterSupported),
                JobSpoolingSupported = map.MapFromDicNullable<JobSpooling?>(src, IppAttributeNames.JobSpoolingSupported),
                MaxPageRangesSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.MaxPageRangesSupported),
                PrintContentOptimizeDefault = map.MapFromDicNullable<PrintContentOptimize?>(src, IppAttributeNames.PrintContentOptimizeDefault),
                PrintContentOptimizeSupported = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, IppAttributeNames.PrintContentOptimizeSupported),
                OutputDeviceSupported = map.MapFromDicSetNullable<OutputDevice[]?>(src, IppAttributeNames.OutputDeviceSupported),
                OutputDeviceUuidSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.OutputDeviceUuidSupported),
                DocumentAccessSupported = map.MapFromDicSetNullable<DocumentAccessMember[]?>(src, IppAttributeNames.DocumentAccessSupported),
                FetchDocumentAttributesSupported = map.MapFromDicSetNullable<FetchDocumentAttribute[]?>(src, IppAttributeNames.FetchDocumentAttributesSupported),
                PrinterModeConfigured = map.MapFromDicNullable<PrinterMode?>(src, IppAttributeNames.PrinterModeConfigured),
                PrinterModeSupported = map.MapFromDicSetNullable<PrinterMode[]?>(src, IppAttributeNames.PrinterModeSupported),
                PrinterStaticResourceDirectoryUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterStaticResourceDirectoryUri),
                PrinterStaticResourceKOctetsSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterStaticResourceKOctetsSupported),
                PrinterStaticResourceKOctetsFree = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterStaticResourceKOctetsFree),
                AccuracyUnitsSupported = map.MapFromDicSetNullable<AccuracyUnits[]?>(src, IppAttributeNames.AccuracyUnitsSupported),
                ChamberHumidityDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberHumidityDefault),
                ChamberHumiditySupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.ChamberHumiditySupported),
                ChamberTemperatureDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberTemperatureDefault),
                ChamberTemperatureSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.ChamberTemperatureSupported),
                MaterialAmountUnitsSupported = map.MapFromDicSetNullable<MaterialAmountUnits[]?>(src, IppAttributeNames.MaterialAmountUnitsSupported),
                MaterialDiameterSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MaterialDiameterSupported),
                MaterialNozzleDiameterSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MaterialNozzleDiameterSupported),
                MaterialPurposeSupported = map.MapFromDicSetNullable<MaterialPurpose[]?>(src, IppAttributeNames.MaterialPurposeSupported),
                MaterialRateSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MaterialRateSupported),
                MaterialRateUnitsSupported = map.MapFromDicSetNullable<MaterialRateUnits[]?>(src, IppAttributeNames.MaterialRateUnitsSupported),
                MaterialShellThicknessSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MaterialShellThicknessSupported),
                MaterialTemperatureSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.MaterialTemperatureSupported),
                MaterialTypeSupported = map.MapFromDicSetNullable<MaterialType[]?>(src, IppAttributeNames.MaterialTypeSupported),
                MaterialsColSupported = map.MapFromDicSetNullable<MaterialsColMember[]?>(src, IppAttributeNames.MaterialsColSupported),
                MaxMaterialsColSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.MaxMaterialsColSupported),
                MultipleObjectHandlingDefault = map.MapFromDicNullable<MultipleObjectHandling?>(src, IppAttributeNames.MultipleObjectHandlingDefault),
                MultipleObjectHandlingSupported = map.MapFromDicSetNullable<MultipleObjectHandling[]?>(src, IppAttributeNames.MultipleObjectHandlingSupported),
                PdfFeaturesSupported = map.MapFromDicSetNullable<PdfFeature[]?>(src, IppAttributeNames.PdfFeaturesSupported),
                PlatformShape = map.MapFromDicNullable<PlatformShape?>(src, IppAttributeNames.PlatformShape),
                PlatformTemperatureDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.PlatformTemperatureDefault),
                PlatformTemperatureSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.PlatformTemperatureSupported),
                PrintBaseDefault = map.MapFromDicNullable<PrintBase?>(src, IppAttributeNames.PrintBaseDefault),
                PrintBaseSupported = map.MapFromDicSetNullable<PrintBase[]?>(src, IppAttributeNames.PrintBaseSupported),
                PrintObjectsSupported = map.MapFromDicSetNullable<PrintObjectsMember[]?>(src, IppAttributeNames.PrintObjectsSupported),
                PrintSupportsDefault = map.MapFromDicNullable<PrintSupports?>(src, IppAttributeNames.PrintSupportsDefault),
                PrintSupportsSupported = map.MapFromDicSetNullable<PrintSupports[]?>(src, IppAttributeNames.PrintSupportsSupported),
                ChamberHumidityCurrent = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberHumidityCurrent),
                ChamberTemperatureCurrent = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberTemperatureCurrent),
                PrinterCameraImageUri = map.MapFromDicSetNullable<Uri[]?>(src, IppAttributeNames.PrinterCameraImageUri),
                PrinterResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.PrinterResourceIds),
                JobCreationAttributesSupported = map.MapFromDicSetNullable<JobCreationAttribute[]?>(src, IppAttributeNames.JobCreationAttributesSupported),
                PrinterRequestedClientType = map.MapFromDicSetNullable<ClientType[]?>(src, IppAttributeNames.PrinterRequestedClientType),
                PrinterServiceType = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, IppAttributeNames.PrinterServiceType),
                FinishingTemplateSupported = map.MapFromDicSetNullable<string, FinishingTemplate>(src, IppAttributeNames.FinishingTemplateSupported, (attribute, value) => new FinishingTemplate(value, attribute.Tag == Tag.Keyword)),
                FinishingsColSupported = map.MapFromDicSetNullable<FinishingsColMember[]?>(src, IppAttributeNames.FinishingsColSupported),
                JobPagesPerSetSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobPagesPerSetSupported),
                PunchingHoleDiameterConfigured = map.MapFromDicNullable<int?>(src, IppAttributeNames.PunchingHoleDiameterConfigured),
                PrinterFinisher = map.MapFromDicSetNullable<PrinterFinisher[]?>(src, IppAttributeNames.PrinterFinisher),
                PrinterFinisherDescription = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterFinisherDescription),
                PrinterFinisherSupplies = map.MapFromDicSetNullable<PrinterFinisherSupply[]?>(src, IppAttributeNames.PrinterFinisherSupplies),
                PrinterFinisherSuppliesDescription = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterFinisherSuppliesDescription),
                BalingTypeSupported = map.MapFromDicSetNullable<string, BalingType>(src, IppAttributeNames.BalingTypeSupported, (attribute, value) => new BalingType(value, attribute.Tag == Tag.Keyword)),
                BalingWhenSupported = map.MapFromDicSetNullable<BalingWhen[]?>(src, IppAttributeNames.BalingWhenSupported),
                BindingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, IppAttributeNames.BindingReferenceEdgeSupported),
                BindingTypeSupported = map.MapFromDicSetNullable<string, BindingType>(src, IppAttributeNames.BindingTypeSupported, (attribute, value) => new BindingType(value, attribute.Tag == Tag.Keyword)),
                CoatingSidesSupported = map.MapFromDicSetNullable<CoatingSides[]?>(src, IppAttributeNames.CoatingSidesSupported),
                CoatingTypeSupported = map.MapFromDicSetNullable<string, CoatingType>(src, IppAttributeNames.CoatingTypeSupported, (attribute, value) => new CoatingType(value, attribute.Tag == Tag.Keyword)),
                CoveringNameSupported = map.MapFromDicSetNullable<string, CoveringName>(src, IppAttributeNames.CoveringNameSupported, (attribute, value) => new CoveringName(value, attribute.Tag == Tag.Keyword)),
                FoldingDirectionSupported = map.MapFromDicSetNullable<FoldingDirection[]?>(src, IppAttributeNames.FoldingDirectionSupported),
                FoldingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.FoldingOffsetSupported),
                FoldingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, IppAttributeNames.FoldingReferenceEdgeSupported),
                LaminatingSidesSupported = map.MapFromDicSetNullable<CoatingSides[]?>(src, IppAttributeNames.LaminatingSidesSupported),
                LaminatingTypeSupported = map.MapFromDicSetNullable<string, LaminatingType>(src, IppAttributeNames.LaminatingTypeSupported, (attribute, value) => new LaminatingType(value, attribute.Tag == Tag.Keyword)),
                PunchingLocationsSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.PunchingLocationsSupported),
                PunchingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.PunchingOffsetSupported),
                PunchingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, IppAttributeNames.PunchingReferenceEdgeSupported),
                StitchingAngleSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.StitchingAngleSupported),
                StitchingLocationsSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.StitchingLocationsSupported),
                StitchingMethodSupported = map.MapFromDicSetNullable<StitchingMethod[]?>(src, IppAttributeNames.StitchingMethodSupported),
                StitchingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.StitchingOffsetSupported),
                StitchingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, IppAttributeNames.StitchingReferenceEdgeSupported),
                TrimmingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.TrimmingOffsetSupported),
                TrimmingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, IppAttributeNames.TrimmingReferenceEdgeSupported),
                TrimmingTypeSupported = map.MapFromDicSetNullable<string, TrimmingType>(src, IppAttributeNames.TrimmingTypeSupported, (attribute, value) => new TrimmingType(value, attribute.Tag == Tag.Keyword)),
                TrimmingWhenSupported = map.MapFromDicSetNullable<TrimmingWhen[]?>(src, IppAttributeNames.TrimmingWhenSupported),
                CoverBackSupported = map.MapFromDicSetNullable<CoverMember[]?>(src, IppAttributeNames.CoverBackSupported),
                CoverFrontSupported = map.MapFromDicSetNullable<CoverMember[]?>(src, IppAttributeNames.CoverFrontSupported),
                CoverTypeSupported = map.MapFromDicSetNullable<CoverType[]?>(src, IppAttributeNames.CoverTypeSupported),
                ForceFrontSideSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.ForceFrontSideSupported),
                ImageOrientationDefault = map.MapFromDicNullable<Orientation?>(src, IppAttributeNames.ImageOrientationDefault),
                ImageOrientationSupported = map.MapFromDicSetNullable<Orientation[]?>(src, IppAttributeNames.ImageOrientationSupported),
                ImpositionTemplateDefault = map.MapFromDicNullable<string, ImpositionTemplate?>(src, IppAttributeNames.ImpositionTemplateDefault, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
                ImpositionTemplateSupported = map.MapFromDicSetNullable<string, ImpositionTemplate>(src, IppAttributeNames.ImpositionTemplateSupported, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
                InsertCountSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.InsertCountSupported),
                InsertSheetSupported = map.MapFromDicSetNullable<InsertSheetMember[]?>(src, IppAttributeNames.InsertSheetSupported),
                JobAccountingOutputBinSupported = map.MapFromDicSetNullable<string, OutputBin>(src, IppAttributeNames.JobAccountingOutputBinSupported, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
                JobAccountingSheetsSupported = map.MapFromDicSetNullable<JobAccountingSheetsMember[]?>(src, IppAttributeNames.JobAccountingSheetsSupported),
                JobAccountingSheetsTypeSupported = map.MapFromDicSetNullable<JobAccountingSheetsType[]?>(src, IppAttributeNames.JobAccountingSheetsTypeSupported),
                JobCompleteBeforeSupported = map.MapFromDicSetNullable<JobCompleteBefore[]?>(src, IppAttributeNames.JobCompleteBeforeSupported),
                JobCompleteBeforeTimeSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobCompleteBeforeTimeSupported),
                JobErrorSheetSupported = map.MapFromDicSetNullable<JobErrorSheetMember[]?>(src, IppAttributeNames.JobErrorSheetSupported),
                JobErrorSheetTypeSupported = map.MapFromDicSetNullable<JobErrorSheetType[]?>(src, IppAttributeNames.JobErrorSheetTypeSupported),
                JobErrorSheetWhenSupported = map.MapFromDicSetNullable<JobErrorSheetWhen[]?>(src, IppAttributeNames.JobErrorSheetWhenSupported),
                JobMessageToOperatorSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobMessageToOperatorSupported),
                JobPhoneNumberDefault = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobPhoneNumberDefault),
                JobPhoneNumberSchemeSupported = map.MapFromDicSetNullable<JobPhoneNumberScheme[]?>(src, IppAttributeNames.JobPhoneNumberSchemeSupported),
                JobPhoneNumberSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobPhoneNumberSupported),
                JobRecipientNameSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobRecipientNameSupported),
                JobSheetMessageSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.JobSheetMessageSupported),
                PageDeliveryDefault = map.MapFromDicNullable<PageDelivery?>(src, IppAttributeNames.PageDeliveryDefault),
                PageDeliverySupported = map.MapFromDicSetNullable<PageDelivery[]?>(src, IppAttributeNames.PageDeliverySupported),
                PresentationDirectionNumberUpDefault = map.MapFromDicNullable<PresentationDirectionNumberUp?>(src, IppAttributeNames.PresentationDirectionNumberUpDefault),
                PresentationDirectionNumberUpSupported = map.MapFromDicSetNullable<PresentationDirectionNumberUp[]?>(src, IppAttributeNames.PresentationDirectionNumberUpSupported),
                SeparatorSheetsSupported = map.MapFromDicSetNullable<SeparatorSheetsMember[]?>(src, IppAttributeNames.SeparatorSheetsSupported),
                SeparatorSheetsTypeSupported = map.MapFromDicSetNullable<SeparatorSheetsType[]?>(src, IppAttributeNames.SeparatorSheetsTypeSupported),
                XImagePositionDefault = map.MapFromDicNullable<XImagePosition?>(src, IppAttributeNames.XImagePositionDefault),
                XImagePositionSupported = map.MapFromDicSetNullable<XImagePosition[]?>(src, IppAttributeNames.XImagePositionSupported),
                XImageShiftDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.XImageShiftDefault),
                XImageShiftSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.XImageShiftSupported),
                XSide1ImageShiftDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.XSide1ImageShiftDefault),
                XSide2ImageShiftDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.XSide2ImageShiftDefault),
                YImagePositionDefault = map.MapFromDicNullable<YImagePosition?>(src, IppAttributeNames.YImagePositionDefault),
                YImagePositionSupported = map.MapFromDicSetNullable<YImagePosition[]?>(src, IppAttributeNames.YImagePositionSupported),
                YImageShiftDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.YImageShiftDefault),
                YImageShiftSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.YImageShiftSupported),
                YSide1ImageShiftDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.YSide1ImageShiftDefault),
                YSide2ImageShiftDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.YSide2ImageShiftDefault),
                ConfirmationSheetPrintDefault = map.MapFromDicNullable<bool?>(src, IppAttributeNames.ConfirmationSheetPrintDefault),
                CoverSheetInfoSupported = map.MapFromDicSetNullable<CoverSheetInfoMember[]?>(src, IppAttributeNames.CoverSheetInfoSupported),
                DestinationAccessesSupported = map.MapFromDicSetNullable<DestinationAccessMember[]?>(src, IppAttributeNames.DestinationAccessesSupported),
                DestinationUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, IppAttributeNames.DestinationUriSchemesSupported),
                DestinationUrisSupported = map.MapFromDicSetNullable<DestinationUrisMember[]?>(src, IppAttributeNames.DestinationUrisSupported),
                FromNameSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.FromNameSupported),
                InputAttributesSupported = map.MapFromDicSetNullable<InputAttributesMember[]?>(src, IppAttributeNames.InputAttributesSupported),
                InputColorModeSupported = map.MapFromDicSetNullable<InputColorMode[]?>(src, IppAttributeNames.InputColorModeSupported),
                InputContentTypeSupported = map.MapFromDicSetNullable<InputContentType[]?>(src, IppAttributeNames.InputContentTypeSupported),
                InputFilmScanModeSupported = map.MapFromDicSetNullable<InputFilmScanMode[]?>(src, IppAttributeNames.InputFilmScanModeSupported),
                InputMediaSupported = map.MapFromDicSetNullable<string, Media>(src, IppAttributeNames.InputMediaSupported, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                InputOrientationRequestedSupported = map.MapFromDicSetNullable<Orientation[]?>(src, IppAttributeNames.InputOrientationRequestedSupported),
                InputQualitySupported = map.MapFromDicSetNullable<PrintQuality[]?>(src, IppAttributeNames.InputQualitySupported),
                InputResolutionSupported = map.MapFromDicSetNullable<Resolution[]?>(src, IppAttributeNames.InputResolutionSupported),
                InputSidesSupported = map.MapFromDicSetNullable<Sides[]?>(src, IppAttributeNames.InputSidesSupported),
                InputSourceSupported = map.MapFromDicSetNullable<InputSource[]?>(src, IppAttributeNames.InputSourceSupported),
                LogoUriFormatsSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.LogoUriFormatsSupported),
                LogoUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, IppAttributeNames.LogoUriSchemesSupported),
                MessageSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.MessageSupported),
                MultipleDestinationUrisSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.MultipleDestinationUrisSupported),
                NumberOfRetriesDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberOfRetriesDefault),
                NumberOfRetriesSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.NumberOfRetriesSupported),
                OrganizationNameSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.OrganizationNameSupported),
                JobDestinationSpoolingSupported = map.MapFromDicNullable<JobSpooling?>(src, IppAttributeNames.JobDestinationSpoolingSupported),
                OutputAttributesSupported = map.MapFromDicSetNullable<OutputAttributesMember[]?>(src, IppAttributeNames.OutputAttributesSupported),
                PrinterFaxLogUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterFaxLogUri),
                PrinterFaxModemInfo = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterFaxModemInfo),
                PrinterFaxModemName = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PrinterFaxModemName),
                PrinterFaxModemNumber = map.MapFromDicSetNullable<Uri[]?>(src, IppAttributeNames.PrinterFaxModemNumber),
                RetryIntervalDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.RetryIntervalDefault),
                RetryIntervalSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.RetryIntervalSupported),
                RetryTimeOutDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.RetryTimeOutDefault),
                RetryTimeOutSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.RetryTimeOutSupported),
                SubjectSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.SubjectSupported),
                ToNameSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.ToNameSupported),
                JpegXDimensionSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JpegXDimensionSupported),
                JpegYDimensionSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JpegYDimensionSupported),
                JobPasswordSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPasswordSupported),
                JobPasswordLengthSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.JobPasswordLengthSupported),
                DocumentPasswordSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentPasswordSupported),
                XSide1ImageOffsetSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.XSide1ImageOffsetSupported),
                XSide2ImageOffsetSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.XSide2ImageOffsetSupported),
                YSide1ImageOffsetSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.YSide1ImageOffsetSupported),
                YSide2ImageOffsetSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, IppAttributeNames.YSide2ImageOffsetSupported),
                UserDefinedValuesSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.UserDefinedValuesSupported),
                PdlInitFileSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.PdlInitFileSupported),
                JobSaveDispositionSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobSaveDispositionSupported),
                SaveDispositionSupported = map.MapFromDicSetNullable<SaveDisposition[]?>(src, IppAttributeNames.SaveDispositionSupported),
                SaveInfoSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SaveInfoSupported),
                SaveLocationSupported = map.MapFromDicSetNullable<Uri[]?>(src, IppAttributeNames.SaveLocationSupported),
            };

            if (src.TryGetValue(IppAttributeNames.JobSheetsColDefault, out var jobSheetsColDefault))
            {
                result.JobSheetsColDefault = map.Map<JobSheetsCol>(jobSheetsColDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.PrinterContactCol, out var printerContactCol))
            {
                result.PrinterContactCol = printerContactCol.GroupBegCollection().Select(x => map.Map<SystemContact>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.MediaColDefault, out var mediaColDefault))
            {
                result.MediaColDefault = map.Map<MediaCol>(mediaColDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.MediaColDatabase, out var mediaColDatabase))
            {
                result.MediaColDatabase = mediaColDatabase.GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.MediaColReady, out var mediaColReady))
            {
                result.MediaColReady = mediaColReady.GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.MediaSizeSupported, out var mediaSizeSupported))
            {
                result.MediaSizeSupported = mediaSizeSupported.GroupBegCollection().Select(x => map.Map<MediaSizeSupported>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.PrinterSupply, out var printerSupply))
            {
                result.PrinterSupply = printerSupply.GroupBegCollection().Select(x => map.Map<PrinterSupply>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.PrinterInputTray, out var printerInputTray))
            {
                result.PrinterInputTray = printerInputTray.GroupBegCollection().Select(x => map.Map<PrinterInputTray>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.PrinterOutputTray, out var printerOutputTray))
            {
                result.PrinterOutputTray = printerOutputTray.GroupBegCollection().Select(x => map.Map<PrinterOutputTray>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.JobConstraintsSupported, out var jobConstraintsSupported))
            {
                result.JobConstraintsSupported = jobConstraintsSupported.GroupBegCollection().Select(x => map.Map<JobConstraintsSupported>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.JobPresetsSupported, out var jobPresetsSupported))
            {
                result.JobPresetsSupported = jobPresetsSupported.GroupBegCollection().Select(x => map.Map<JobPresetsSupported>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.JobResolversSupported, out var jobResolversSupported))
            {
                result.JobResolversSupported = jobResolversSupported.GroupBegCollection().Select(x => map.Map<JobResolversSupported>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.JobTriggersSupported, out var jobTriggersSupported))
            {
                result.JobTriggersSupported = jobTriggersSupported.GroupBegCollection().Select(x => map.Map<JobTriggersSupported>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.PrintColorModeIccProfiles, out var printColorModeIccProfiles))
            {
                result.PrintColorModeIccProfile = printColorModeIccProfiles.GroupBegCollection().Select(x => map.Map<PrintColorModeIccProfile>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.PrinterIccProfiles, out var printerIccProfiles))
            {
                result.PrinterIccProfile = printerIccProfiles.GroupBegCollection().Select(x => map.Map<PrinterIccProfile>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.MaterialsColDatabase, out var materialsColDatabase))
            {
                result.MaterialsColDatabase = materialsColDatabase.GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.MaterialsColDefault, out var materialsColDefault))
            {
                result.MaterialsColDefault = materialsColDefault.GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.MaterialsColReady, out var materialsColReady))
            {
                result.MaterialsColReady = materialsColReady.GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.PrintAccuracyDefault, out var printAccuracyDefault))
            {
                result.PrintAccuracyDefault = map.Map<PrintAccuracy>(printAccuracyDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.PrintAccuracySupported, out var printAccuracySupported))
            {
                result.PrintAccuracySupported = map.Map<PrintAccuracy>(printAccuracySupported.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.PrinterVolumeSupported, out var printerVolumeSupported))
            {
                result.PrinterVolumeSupported = map.Map<PrinterVolumeSupported>(printerVolumeSupported.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.FinishingsColDefault, out var finishingsColDefault))
            {
                result.FinishingsColDefault = finishingsColDefault.GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.FinishingsColReady, out var finishingsColReady))
            {
                result.FinishingsColReady = finishingsColReady.GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.FinishingsColDatabase, out var finishingsColDatabase))
            {
                result.FinishingsColDatabase = finishingsColDatabase.GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.CoverBackDefault, out var coverBackDefault))
            {
                result.CoverBackDefault = map.Map<Cover>(coverBackDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.CoverFrontDefault, out var coverFrontDefault))
            {
                result.CoverFrontDefault = map.Map<Cover>(coverFrontDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.InsertSheetDefault, out var insertsheetdefault) && insertsheetdefault.GroupBegCollection().Any())
            {
                result.InsertSheetDefault = insertsheetdefault.GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.JobAccountingSheetsDefault, out var jobAccountingSheetsDefault))
            {
                result.JobAccountingSheetsDefault = map.Map<JobAccountingSheets>(jobAccountingSheetsDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.JobErrorSheetDefault, out var jobErrorSheetDefault))
            {
                result.JobErrorSheetDefault = map.Map<JobErrorSheet>(jobErrorSheetDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.SeparatorSheetsDefault, out var separatorSheetsDefault))
            {
                result.SeparatorSheetsDefault = map.Map<SeparatorSheets>(separatorSheetsDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.CoverSheetInfoDefault, out var coverSheetInfoDefault))
            {
                result.CoverSheetInfoDefault = map.Map<CoverSheetInfo>(coverSheetInfoDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.DestinationUriReady, out var destinationUriReady))
            {
                result.DestinationUriReady = destinationUriReady.GroupBegCollection().Select(x => map.Map<DestinationUriReady>(x.FromBegCollection().ToIppDictionary())).ToArray();
            }

            if (src.TryGetValue(IppAttributeNames.InputAttributesDefault, out var inputAttributesDefault))
            {
                result.InputAttributesDefault = map.Map<DocumentTemplateAttributes>(inputAttributesDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.OutputAttributesDefault, out var outputAttributesDefault))
            {
                result.OutputAttributesDefault = map.Map<OutputAttributes>(outputAttributesDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.PdlInitFileDefault, out var pdlInitFileDefault))
            {
                result.PdlInitFileDefault = map.Map<PdlInitFile>(pdlInitFileDefault.FromBegCollection().ToIppDictionary());
            }

            if (src.TryGetValue(IppAttributeNames.JobSaveDispositionDefault, out var jobSaveDispositionDefault))
            {
                result.JobSaveDispositionDefault = map.Map<JobSaveDisposition>(jobSaveDispositionDefault.FromBegCollection().ToIppDictionary());
            }

            return result;
        });

        mapper.CreateMap<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();

            if (src.CharsetConfigured != null)
                dic.Add(IppAttributeNames.CharsetConfigured, new IppAttribute[] { new IppAttribute(Tag.Charset, IppAttributeNames.CharsetConfigured, src.CharsetConfigured) });
            if (src.CharsetSupported != null)
                dic.Add(IppAttributeNames.CharsetSupported, src.CharsetSupported.Select(x => new IppAttribute(Tag.Charset, IppAttributeNames.CharsetSupported, x)).ToArray());
            if (src.ColorSupported != null)
                dic.Add(IppAttributeNames.ColorSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, IppAttributeNames.ColorSupported, src.ColorSupported.Value) });
            if (src.CompressionDefault != null)
                dic.Add(IppAttributeNames.CompressionDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.CompressionDefault, map.Map<string>(src.CompressionDefault.Value)) });
            if (src.CompressionSupported != null)
                dic.Add(IppAttributeNames.CompressionSupported, src.CompressionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CompressionSupported, map.Map<string>(x))).ToArray());
            if (src.DocumentFormatDefault != null)
                dic.Add(IppAttributeNames.DocumentFormatDefault, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatDefault, src.DocumentFormatDefault.Value) });
            if (src.DocumentFormatSupported != null)
                dic.Add(IppAttributeNames.DocumentFormatSupported, src.DocumentFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatSupported, x)).ToArray());
            if (src.ClientInfoSupported != null)
                dic.Add(IppAttributeNames.ClientInfoSupported, src.ClientInfoSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.ClientInfoSupported, map.Map<string>(x))).ToArray());
            if (src.MaxClientInfoSupported != null)
                dic.Add(IppAttributeNames.MaxClientInfoSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.MaxClientInfoSupported, src.MaxClientInfoSupported.Value)]);
            if (src.DocumentCharsetDefault != null)
                dic.Add(IppAttributeNames.DocumentCharsetDefault, [new IppAttribute(Tag.Charset, IppAttributeNames.DocumentCharsetDefault, src.DocumentCharsetDefault.Value)]);
            if (src.DocumentCharsetSupported != null)
                dic.Add(IppAttributeNames.DocumentCharsetSupported, src.DocumentCharsetSupported.Select(x => new IppAttribute(Tag.Charset, IppAttributeNames.DocumentCharsetSupported, x)).ToArray());
            if (src.DocumentFormatDetailsSupported != null)
                dic.Add(IppAttributeNames.DocumentFormatDetailsSupported, src.DocumentFormatDetailsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.DocumentFormatDetailsSupported, map.Map<string>(x))).ToArray());
            if (src.DocumentNaturalLanguageDefault != null)
                dic.Add(IppAttributeNames.DocumentNaturalLanguageDefault, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.DocumentNaturalLanguageDefault, src.DocumentNaturalLanguageDefault.Value)]);
            if (src.DocumentNaturalLanguageSupported != null)
                dic.Add(IppAttributeNames.DocumentNaturalLanguageSupported, src.DocumentNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.DocumentNaturalLanguageSupported, x)).ToArray());
            if (src.JobIdsSupported != null)
                dic.Add(IppAttributeNames.JobIdsSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobIdsSupported, src.JobIdsSupported.Value)]);
            if (src.JobMandatoryAttributesSupported != null)
                dic.Add(IppAttributeNames.JobMandatoryAttributesSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobMandatoryAttributesSupported, src.JobMandatoryAttributesSupported.Value)]);
            if (src.JobSheetsColDefault != null)
                dic.Add(IppAttributeNames.JobSheetsColDefault, map.Map<IEnumerable<IppAttribute>>(src.JobSheetsColDefault).ToBegCollection(IppAttributeNames.JobSheetsColDefault).ToArray());
            if (src.JobSheetsColSupported != null)
                dic.Add(IppAttributeNames.JobSheetsColSupported, src.JobSheetsColSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobSheetsColSupported, map.Map<string>(x))).ToArray());
            if (src.GeneratedNaturalLanguageSupported != null)
                dic.Add(IppAttributeNames.GeneratedNaturalLanguageSupported, src.GeneratedNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.GeneratedNaturalLanguageSupported, x)).ToArray());
            if (src.IppVersionsSupported != null)
                dic.Add(IppAttributeNames.IppVersionsSupported, src.IppVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.IppVersionsSupported, x.ToString())).ToArray());
            if (src.JobImpressionsSupported != null)
                dic.Add(IppAttributeNames.JobImpressionsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobImpressionsSupported, src.JobImpressionsSupported.Value) });
            if (src.JobKOctetsSupported != null)
                dic.Add(IppAttributeNames.JobKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobKOctetsSupported, src.JobKOctetsSupported.Value) });
            if (src.JpegKOctetsSupported != null)
                dic.Add(IppAttributeNames.JpegKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JpegKOctetsSupported, src.JpegKOctetsSupported.Value) });
            if (src.PdfKOctetsSupported != null)
                dic.Add(IppAttributeNames.PdfKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PdfKOctetsSupported, src.PdfKOctetsSupported.Value) });
            if (src.JobMediaSheetsSupported != null)
                dic.Add(IppAttributeNames.JobMediaSheetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobMediaSheetsSupported, src.JobMediaSheetsSupported.Value) });
            if (src.JobSheetsDefault != null)
                dic.Add(IppAttributeNames.JobSheetsDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobSheetsDefault, map.Map<string>(src.JobSheetsDefault.Value))]);
            if (src.JobSheetsSupported != null)
                dic.Add(IppAttributeNames.JobSheetsSupported, src.JobSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobSheetsSupported, map.Map<string>(x))).ToArray());
            if (src.NumberUpDefault != null)
                dic.Add(IppAttributeNames.NumberUpDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.NumberUpDefault, src.NumberUpDefault.Value)]);
            if (src.NumberUpSupported != null)
                dic.Add(IppAttributeNames.NumberUpSupported, src.NumberUpSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.NumberUpSupported, x)).ToArray());
            if (src.MultipleDocumentJobsSupported != null)
                dic.Add(IppAttributeNames.MultipleDocumentJobsSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, IppAttributeNames.MultipleDocumentJobsSupported, src.MultipleDocumentJobsSupported.Value) });
            if (src.MultipleDocumentHandlingDefault != null)
                dic.Add(IppAttributeNames.MultipleDocumentHandlingDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleDocumentHandlingDefault, map.Map<string>(src.MultipleDocumentHandlingDefault.Value))]);
            if (src.MultipleDocumentHandlingSupported != null)
                dic.Add(IppAttributeNames.MultipleDocumentHandlingSupported, src.MultipleDocumentHandlingSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleDocumentHandlingSupported, map.Map<string>(x))).ToArray());
            if (src.MultipleOperationTimeOut != null)
                dic.Add(IppAttributeNames.MultipleOperationTimeOut, new IppAttribute[] { new IppAttribute(Tag.Integer, IppAttributeNames.MultipleOperationTimeOut, src.MultipleOperationTimeOut.Value) });
            if (src.MultipleOperationTimeOutAction != null)
                dic.Add(IppAttributeNames.MultipleOperationTimeOutAction, [new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleOperationTimeOutAction, map.Map<string>(src.MultipleOperationTimeOutAction.Value))]);
            if (src.NaturalLanguageConfigured != null)
                dic.Add(IppAttributeNames.NaturalLanguageConfigured, new IppAttribute[] { new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.NaturalLanguageConfigured, src.NaturalLanguageConfigured.Value) });
            if (src.OperationsSupported != null)
                dic.Add(IppAttributeNames.OperationsSupported, src.OperationsSupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.OperationsSupported, (int)x)).ToArray());
            if (src.PagesPerMinute != null)
                dic.Add(IppAttributeNames.PagesPerMinute, new IppAttribute[] { new IppAttribute(Tag.Integer, IppAttributeNames.PagesPerMinute, src.PagesPerMinute.Value) });
            if (src.PdlOverrideSupported != null)
                dic.Add(IppAttributeNames.PdlOverrideSupported, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.PdlOverrideSupported, src.PdlOverrideSupported.Value) });
            if (src.OverridesSupported != null)
                dic.Add(IppAttributeNames.OverridesSupported, src.OverridesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.OverridesSupported, x.Value)).ToArray());
            if (src.PagesPerMinuteColor != null)
                dic.Add(IppAttributeNames.PagesPerMinuteColor, [new IppAttribute(Tag.Integer, IppAttributeNames.PagesPerMinuteColor, src.PagesPerMinuteColor.Value)]);
            if (src.PrinterCurrentTime != null)
                dic.Add(IppAttributeNames.PrinterCurrentTime, new IppAttribute[] { new IppAttribute(Tag.DateTime, IppAttributeNames.PrinterCurrentTime, src.PrinterCurrentTime.Value) });
            if (src.PrinterConfigChangeTime != null)
                dic.Add(IppAttributeNames.PrinterConfigChangeTime, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterConfigChangeTime, src.PrinterConfigChangeTime.Value)]);
            if (src.PrinterConfigChangeDateTime != null)
                dic.Add(IppAttributeNames.PrinterConfigChangeDateTime, [new IppAttribute(Tag.DateTime, IppAttributeNames.PrinterConfigChangeDateTime, src.PrinterConfigChangeDateTime.Value)]);
            if (src.PrinterConfigChanges != null)
                dic.Add(IppAttributeNames.PrinterConfigChanges, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterConfigChanges, src.PrinterConfigChanges.Value)]);
            if (src.PrinterContactCol != null)
                dic.Add(IppAttributeNames.PrinterContactCol, src.PrinterContactCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterContactCol)).ToArray());
            if (src.PrinterGeoLocation != null)
                dic.Add(IppAttributeNames.PrinterGeoLocation, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterGeoLocation, src.PrinterGeoLocation.ToString()) });
            if (src.PrinterIds != null)
                dic.Add(IppAttributeNames.PrinterIds, src.PrinterIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.PrinterIds, x)).ToArray());
            if (src.PrinterImpressionsCompleted != null)
                dic.Add(IppAttributeNames.PrinterImpressionsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterImpressionsCompleted, src.PrinterImpressionsCompleted.Value)]);
            if (src.PrinterImpressionsCompletedCol != null)
                dic.Add(IppAttributeNames.PrinterImpressionsCompletedCol, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterImpressionsCompletedCol, src.PrinterImpressionsCompletedCol.Value)]);
            if (src.PrinterMediaSheetsCompleted != null)
                dic.Add(IppAttributeNames.PrinterMediaSheetsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterMediaSheetsCompleted, src.PrinterMediaSheetsCompleted.Value)]);
            if (src.PrinterMediaSheetsCompletedCol != null)
                dic.Add(IppAttributeNames.PrinterMediaSheetsCompletedCol, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterMediaSheetsCompletedCol, src.PrinterMediaSheetsCompletedCol.Value)]);
            if (src.PrinterPagesCompleted != null)
                dic.Add(IppAttributeNames.PrinterPagesCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterPagesCompleted, src.PrinterPagesCompleted.Value)]);
            if (src.PrinterPagesCompletedCol != null)
                dic.Add(IppAttributeNames.PrinterPagesCompletedCol, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterPagesCompletedCol, src.PrinterPagesCompletedCol.Value)]);
            if (src.PrinterDriverInstaller != null)
                dic.Add(IppAttributeNames.PrinterDriverInstaller, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterDriverInstaller, src.PrinterDriverInstaller.ToString()) });
            if (src.PrinterInfo != null)
                dic.Add(IppAttributeNames.PrinterInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterInfo, src.PrinterInfo) });
            if (src.PrinterIsAcceptingJobs != null)
                dic.Add(IppAttributeNames.PrinterIsAcceptingJobs, new IppAttribute[] { new IppAttribute(Tag.Boolean, IppAttributeNames.PrinterIsAcceptingJobs, src.PrinterIsAcceptingJobs.Value) });
            if (src.PrinterLocation != null)
                dic.Add(IppAttributeNames.PrinterLocation, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterLocation, src.PrinterLocation) });
            if (src.PrinterMakeAndModel != null)
                dic.Add(IppAttributeNames.PrinterMakeAndModel, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterMakeAndModel, src.PrinterMakeAndModel) });
            if (src.PrinterMessageFromOperator != null)
                dic.Add(IppAttributeNames.PrinterMessageFromOperator, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterMessageFromOperator, src.PrinterMessageFromOperator) });
            if (src.PrinterMoreInfo != null)
                dic.Add(IppAttributeNames.PrinterMoreInfo, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterMoreInfo, src.PrinterMoreInfo.ToString()) });
            if (src.PrinterMoreInfoManufacturer != null)
                dic.Add(IppAttributeNames.PrinterMoreInfoManufacturer, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterMoreInfoManufacturer, src.PrinterMoreInfoManufacturer.ToString()) });
            if (src.PrinterName != null)
                dic.Add(IppAttributeNames.PrinterName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.PrinterName, src.PrinterName) });
            if (src.PrinterState != null)
                dic.Add(IppAttributeNames.PrinterState, new IppAttribute[] { new IppAttribute(Tag.Enum, IppAttributeNames.PrinterState, (int)src.PrinterState.Value) });
            if (src.PrinterStateMessage != null)
                dic.Add(IppAttributeNames.PrinterStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterStateMessage, src.PrinterStateMessage) });
            if (src.PrinterDetailedStatusMessages != null)
                dic.Add(IppAttributeNames.PrinterDetailedStatusMessages, src.PrinterDetailedStatusMessages.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterDetailedStatusMessages, x)).ToArray());
            if (src.PrinterStateChangeTime != null)
                dic.Add(IppAttributeNames.PrinterStateChangeTime, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterStateChangeTime, src.PrinterStateChangeTime.Value)]);
            if (src.PrinterStateChangeDateTime != null)
                dic.Add(IppAttributeNames.PrinterStateChangeDateTime, [new IppAttribute(Tag.DateTime, IppAttributeNames.PrinterStateChangeDateTime, src.PrinterStateChangeDateTime.Value)]);
            if (src.PrinterStateReasons != null)
                dic.Add(IppAttributeNames.PrinterStateReasons, src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterStateReasons, x)).ToArray());
            if (src.PrinterUpTime != null)
                dic.Add(IppAttributeNames.PrinterUpTime, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterUpTime, src.PrinterUpTime.Value)]);
            if (src.PrinterUriSupported != null)
                dic.Add(IppAttributeNames.PrinterUriSupported, src.PrinterUriSupported.Select(x => new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUriSupported, x.ToString())).ToArray());
            if (src.PrintScalingDefault != null)
                dic.Add(IppAttributeNames.PrintScalingDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.PrintScalingDefault, map.Map<string>(src.PrintScalingDefault)) });
            if (src.PrintScalingSupported != null)
                dic.Add(IppAttributeNames.PrintScalingSupported, src.PrintScalingSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintScalingSupported, map.Map<string>(x))).ToArray());
            if (src.QueuedJobCount != null)
                dic.Add(IppAttributeNames.QueuedJobCount, [new IppAttribute(Tag.Integer, IppAttributeNames.QueuedJobCount, src.QueuedJobCount.Value)]);
            if (src.ReferenceUriSchemesSupported != null)
                dic.Add(IppAttributeNames.ReferenceUriSchemesSupported, src.ReferenceUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, IppAttributeNames.ReferenceUriSchemesSupported, map.Map<string>(x))).ToArray());
            if (src.UriAuthenticationSupported != null)
                dic.Add(IppAttributeNames.UriAuthenticationSupported, src.UriAuthenticationSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.UriAuthenticationSupported, map.Map<string>(x))).ToArray());
            if (src.UriSecuritySupported != null)
                dic.Add(IppAttributeNames.UriSecuritySupported, src.UriSecuritySupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.UriSecuritySupported, map.Map<string>(x))).ToArray());
            if (src.MediaDefault != null)
            {
                var mediaDefaultTag = src.MediaDefault.Value.ToIppTag();
                dic.Add(IppAttributeNames.MediaDefault, [new IppAttribute(mediaDefaultTag, IppAttributeNames.MediaDefault, map.Map<string>(src.MediaDefault.Value))]);
            }
            if (src.MediaSupported != null)
                dic.Add(IppAttributeNames.MediaSupported, src.MediaSupported.Select(x =>
                {
                    var mediaTag = x.ToIppTag();
                    return new IppAttribute(mediaTag, IppAttributeNames.MediaSupported, map.Map<string>(x));
                }).ToArray());
            if (src.MediaReady != null)
                dic.Add(IppAttributeNames.MediaReady, src.MediaReady.Select(x =>
                {
                    var mediaTag = x.ToIppTag();
                    return new IppAttribute(mediaTag, IppAttributeNames.MediaReady, map.Map<string>(x));
                }).ToArray());
            if (src.SidesDefault != null)
                dic.Add(IppAttributeNames.SidesDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.SidesDefault, map.Map<string>(src.SidesDefault)) });
            if (src.SidesSupported != null)
                dic.Add(IppAttributeNames.SidesSupported, src.SidesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SidesSupported, map.Map<string>(x))).ToArray());
            if (src.FinishingsDefault != null)
                dic.Add(IppAttributeNames.FinishingsDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, IppAttributeNames.FinishingsDefault, (int)src.FinishingsDefault.Value) });
            if (src.FinishingsSupported != null)
                dic.Add(IppAttributeNames.FinishingsSupported, src.FinishingsSupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.FinishingsSupported, (int)x)).ToArray());
            if (src.PrinterResolutionDefault != null)
                dic.Add(IppAttributeNames.PrinterResolutionDefault, new IppAttribute[] { new IppAttribute(Tag.Resolution, IppAttributeNames.PrinterResolutionDefault, src.PrinterResolutionDefault.Value) });
            if (src.PrinterResolutionSupported != null)
                dic.Add(IppAttributeNames.PrinterResolutionSupported, src.PrinterResolutionSupported.Select(x => new IppAttribute(Tag.Resolution, IppAttributeNames.PrinterResolutionSupported, x)).ToArray());
            if (src.PrintQualityDefault != null)
                dic.Add(IppAttributeNames.PrintQualityDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, IppAttributeNames.PrintQualityDefault, (int)src.PrintQualityDefault.Value) });
            if (src.PrintQualitySupported != null)
                dic.Add(IppAttributeNames.PrintQualitySupported, src.PrintQualitySupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.PrintQualitySupported, (int)x)).ToArray());
            if (src.JobPriorityDefault != null)
                dic.Add(IppAttributeNames.JobPriorityDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPriorityDefault, src.JobPriorityDefault.Value)]);
            if (src.JobPrioritySupported != null)
                dic.Add(IppAttributeNames.JobPrioritySupported, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPrioritySupported, src.JobPrioritySupported.Value)]);
            if (src.CopiesDefault != null)
                dic.Add(IppAttributeNames.CopiesDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.CopiesDefault, src.CopiesDefault.Value)]);
            if (src.CopiesSupported != null)
                dic.Add(IppAttributeNames.CopiesSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.CopiesSupported, src.CopiesSupported.Value) });
            if (src.OrientationRequestedDefault != null)
                dic.Add(IppAttributeNames.OrientationRequestedDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequestedDefault, (int)src.OrientationRequestedDefault.Value) });
            if (src.OrientationRequestedSupported != null)
                dic.Add(IppAttributeNames.OrientationRequestedSupported, src.OrientationRequestedSupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequestedSupported, (int)x)).ToArray());
            if (src.PageRangesSupported != null)
                dic.Add(IppAttributeNames.PageRangesSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, IppAttributeNames.PageRangesSupported, src.PageRangesSupported.Value) });
            if (src.JobHoldUntilDefault != null)
                dic.Add(IppAttributeNames.JobHoldUntilDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.JobHoldUntilDefault, map.Map<string>(src.JobHoldUntilDefault.Value)) });
            if (src.JobHoldUntilSupported != null)
                dic.Add(IppAttributeNames.JobHoldUntilSupported, src.JobHoldUntilSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobHoldUntilSupported, map.Map<string>(x))).ToArray());
            if (src.JobHoldUntilTimeSupported != null)
                dic.Add(IppAttributeNames.JobHoldUntilTimeSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobHoldUntilTimeSupported, src.JobHoldUntilTimeSupported.Value)]);
            if (src.JobDelayOutputUntilDefault != null)
                dic.Add(IppAttributeNames.JobDelayOutputUntilDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobDelayOutputUntilDefault, map.Map<string>(src.JobDelayOutputUntilDefault.Value))]);
            if (src.JobDelayOutputUntilSupported != null)
                dic.Add(IppAttributeNames.JobDelayOutputUntilSupported, src.JobDelayOutputUntilSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobDelayOutputUntilSupported, map.Map<string>(x))).ToArray());
            if (src.JobDelayOutputUntilTimeSupported != null)
                dic.Add(IppAttributeNames.JobDelayOutputUntilTimeSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobDelayOutputUntilTimeSupported, src.JobDelayOutputUntilTimeSupported.Value)]);
            if (src.JobHistoryAttributesConfigured != null)
                dic.Add(IppAttributeNames.JobHistoryAttributesConfigured, src.JobHistoryAttributesConfigured.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobHistoryAttributesConfigured, map.Map<string>(x))).ToArray());
            if (src.JobHistoryAttributesSupported != null)
                dic.Add(IppAttributeNames.JobHistoryAttributesSupported, src.JobHistoryAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobHistoryAttributesSupported, map.Map<string>(x))).ToArray());
            if (src.JobHistoryIntervalConfigured != null)
                dic.Add(IppAttributeNames.JobHistoryIntervalConfigured, [new IppAttribute(Tag.Integer, IppAttributeNames.JobHistoryIntervalConfigured, src.JobHistoryIntervalConfigured.Value)]);
            if (src.JobHistoryIntervalSupported != null)
                dic.Add(IppAttributeNames.JobHistoryIntervalSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobHistoryIntervalSupported, src.JobHistoryIntervalSupported.Value)]);
            if (src.JobRetainUntilDefault != null)
                dic.Add(IppAttributeNames.JobRetainUntilDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobRetainUntilDefault, map.Map<string>(src.JobRetainUntilDefault.Value))]);
            if (src.JobRetainUntilIntervalDefault != null)
                dic.Add(IppAttributeNames.JobRetainUntilIntervalDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.JobRetainUntilIntervalDefault, src.JobRetainUntilIntervalDefault.Value)]);
            if (src.JobRetainUntilIntervalSupported != null)
                dic.Add(IppAttributeNames.JobRetainUntilIntervalSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobRetainUntilIntervalSupported, src.JobRetainUntilIntervalSupported.Value)]);
            if (src.JobRetainUntilSupported != null)
                dic.Add(IppAttributeNames.JobRetainUntilSupported, src.JobRetainUntilSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobRetainUntilSupported, map.Map<string>(x))).ToArray());
            if (src.JobRetainUntilTimeSupported != null)
                dic.Add(IppAttributeNames.JobRetainUntilTimeSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobRetainUntilTimeSupported, src.JobRetainUntilTimeSupported.Value)]);
            if (src.OutputBinDefault != null)
            {
                var outputBinDefaultValue = src.OutputBinDefault.Value;
                var outputBinDefault = map.Map<string>(outputBinDefaultValue);
                var outputBinDefaultTag = outputBinDefaultValue.ToIppTag();
                dic.Add(IppAttributeNames.OutputBinDefault, [new IppAttribute(outputBinDefaultTag, IppAttributeNames.OutputBinDefault, outputBinDefault)]);
            }
            if (src.OutputBinSupported != null)
                dic.Add(IppAttributeNames.OutputBinSupported, src.OutputBinSupported.Select(x =>
                {
                    var outputBin = map.Map<string>(x);
                    var outputBinTag = x.ToIppTag();
                    return new IppAttribute(outputBinTag, IppAttributeNames.OutputBinSupported, outputBin);
                }).ToArray());
            if (src.MediaColDefault != null)
                dic.Add(IppAttributeNames.MediaColDefault, map.Map<IEnumerable<IppAttribute>>(src.MediaColDefault).ToBegCollection(IppAttributeNames.MediaColDefault).ToArray());
            if (src.MediaColDatabase != null)
                dic.Add(IppAttributeNames.MediaColDatabase, src.MediaColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MediaColDatabase)).ToArray());
            if (src.MediaColReady != null)
                dic.Add(IppAttributeNames.MediaColReady, src.MediaColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MediaColReady)).ToArray());
            if (src.MediaColSupported != null)
                dic.Add(IppAttributeNames.MediaColSupported, src.MediaColSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaColSupported, map.Map<string>(x))).ToArray());
            if (src.MediaSizeSupported != null)
                dic.Add(IppAttributeNames.MediaSizeSupported, src.MediaSizeSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MediaSizeSupported)).ToArray());
            if (src.MediaKeySupported != null)
                dic.Add(IppAttributeNames.MediaKeySupported, src.MediaKeySupported.Select(x =>
                {
                    var mediaKeyTag = x.ToIppTag();
                    return new IppAttribute(mediaKeyTag, IppAttributeNames.MediaKeySupported, x.ToString());
                }).ToArray());
            if (src.MediaSourceSupported != null)
                dic.Add(IppAttributeNames.MediaSourceSupported, src.MediaSourceSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaSourceSupported, x.ToString())).ToArray());
            if (src.MediaTypeSupported != null)
                dic.Add(IppAttributeNames.MediaTypeSupported, src.MediaTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaTypeSupported, x.ToString())).ToArray());
            if (src.MediaBackCoatingSupported != null)
                dic.Add(IppAttributeNames.MediaBackCoatingSupported, src.MediaBackCoatingSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaBackCoatingSupported, x.ToString())).ToArray());
            if (src.MediaFrontCoatingSupported != null)
                dic.Add(IppAttributeNames.MediaFrontCoatingSupported, src.MediaFrontCoatingSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaFrontCoatingSupported, x.ToString())).ToArray());
            if (src.MediaColorSupported != null)
                dic.Add(IppAttributeNames.MediaColorSupported, src.MediaColorSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaColorSupported, x.ToString())).ToArray());
            if (src.MediaGrainSupported != null)
                dic.Add(IppAttributeNames.MediaGrainSupported, src.MediaGrainSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaGrainSupported, x.ToString())).ToArray());
            if (src.MediaToothSupported != null)
                dic.Add(IppAttributeNames.MediaToothSupported, src.MediaToothSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaToothSupported, x.ToString())).ToArray());
            if (src.MediaPrePrintedSupported != null)
                dic.Add(IppAttributeNames.MediaPrePrintedSupported, src.MediaPrePrintedSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaPrePrintedSupported, x.ToString())).ToArray());
            if (src.MediaRecycledSupported != null)
                dic.Add(IppAttributeNames.MediaRecycledSupported, src.MediaRecycledSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaRecycledSupported, x.ToString())).ToArray());
            if (src.MediaHoleCountSupported != null)
                dic.Add(IppAttributeNames.MediaHoleCountSupported, src.MediaHoleCountSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MediaHoleCountSupported, x)).ToArray());
            if (src.MediaOrderCountSupported != null)
                dic.Add(IppAttributeNames.MediaOrderCountSupported, src.MediaOrderCountSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MediaOrderCountSupported, x)).ToArray());
            if (src.MediaThicknessSupported != null)
                dic.Add(IppAttributeNames.MediaThicknessSupported, src.MediaThicknessSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MediaThicknessSupported, x)).ToArray());
            if (src.MediaWeightMetricSupported != null)
                dic.Add(IppAttributeNames.MediaWeightMetricSupported, src.MediaWeightMetricSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MediaWeightMetricSupported, x)).ToArray());
            if (src.MediaBottomMarginSupported != null)
                dic.Add(IppAttributeNames.MediaBottomMarginSupported, src.MediaBottomMarginSupported.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.MediaBottomMarginSupported, x)).ToArray());
            if (src.MediaLeftMarginSupported != null)
                dic.Add(IppAttributeNames.MediaLeftMarginSupported, src.MediaLeftMarginSupported.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.MediaLeftMarginSupported, x)).ToArray());
            if (src.MediaRightMarginSupported != null)
                dic.Add(IppAttributeNames.MediaRightMarginSupported, src.MediaRightMarginSupported.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.MediaRightMarginSupported, x)).ToArray());
            if (src.MediaTopMarginSupported != null)
                dic.Add(IppAttributeNames.MediaTopMarginSupported, src.MediaTopMarginSupported.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.MediaTopMarginSupported, x)).ToArray());
            if (src.PrintColorModeDefault != null)
                dic.Add(IppAttributeNames.PrintColorModeDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.PrintColorModeDefault, map.Map<string>(src.PrintColorModeDefault.Value)) });
            if (src.PrintColorModeSupported != null)
                dic.Add(IppAttributeNames.PrintColorModeSupported, src.PrintColorModeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintColorModeSupported, map.Map<string>(x))).ToArray());
            if (src.WhichJobsSupported != null)
                dic.Add(IppAttributeNames.WhichJobsSupported, src.WhichJobsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.WhichJobsSupported, map.Map<string>(x))).ToArray());
            if (src.PrinterUUID != null)
                dic.Add(IppAttributeNames.PrinterUUID, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterUUID, src.PrinterUUID) });
            if (src.PdfVersionsSupported != null)
                dic.Add(IppAttributeNames.PdfVersionsSupported, src.PdfVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PdfVersionsSupported, map.Map<string>(x))).ToArray());
            if (src.IppFeaturesSupported != null)
                dic.Add(IppAttributeNames.IppFeaturesSupported, src.IppFeaturesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.IppFeaturesSupported, x)).ToArray());
            if (src.DocumentCreationAttributesSupported != null)
                dic.Add(IppAttributeNames.DocumentCreationAttributesSupported, src.DocumentCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.DocumentCreationAttributesSupported, x)).ToArray());
            if (src.JobAccountIdDefault != null)
                dic.Add(IppAttributeNames.JobAccountIdDefault, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobAccountIdDefault, src.JobAccountIdDefault)]);
            if (src.JobAccountTypeDefault != null)
                dic.Add(IppAttributeNames.JobAccountTypeDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobAccountTypeDefault, map.Map<string>(src.JobAccountTypeDefault.Value))]);
            if (src.JobAccountTypeSupported != null)
                dic.Add(IppAttributeNames.JobAccountTypeSupported, src.JobAccountTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobAccountTypeSupported, map.Map<string>(x))).ToArray());
            if (src.JobAccountIdSupported != null)
                dic.Add(IppAttributeNames.JobAccountIdSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobAccountIdSupported, src.JobAccountIdSupported.Value)]);
            if (src.JobAccountingUserIdDefault != null)
                dic.Add(IppAttributeNames.JobAccountingUserIdDefault, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobAccountingUserIdDefault, src.JobAccountingUserIdDefault)]);
            if (src.JobAccountingUserIdSupported != null)
                dic.Add(IppAttributeNames.JobAccountingUserIdSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobAccountingUserIdSupported, src.JobAccountingUserIdSupported.Value)]);
            if (src.JobPasswordEncryptionSupported != null)
                dic.Add(IppAttributeNames.JobPasswordEncryptionSupported, src.JobPasswordEncryptionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobPasswordEncryptionSupported, map.Map<string>(x))).ToArray());
            if (src.JobAuthorizationUriSupported != null)
                dic.Add(IppAttributeNames.JobAuthorizationUriSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobAuthorizationUriSupported, src.JobAuthorizationUriSupported.Value)]);
            if (src.PrinterChargeInfo != null)
                dic.Add(IppAttributeNames.PrinterChargeInfo, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterChargeInfo, src.PrinterChargeInfo)]);
            if (src.PrinterChargeInfoUri != null)
                dic.Add(IppAttributeNames.PrinterChargeInfoUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterChargeInfoUri, src.PrinterChargeInfoUri.ToString())]);
            if (src.PrinterMandatoryJobAttributes != null)
                dic.Add(IppAttributeNames.PrinterMandatoryJobAttributes, src.PrinterMandatoryJobAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterMandatoryJobAttributes, map.Map<string>(x))).ToArray());
            if (src.PrinterRequestedJobAttributes != null)
                dic.Add(IppAttributeNames.PrinterRequestedJobAttributes, src.PrinterRequestedJobAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterRequestedJobAttributes, map.Map<string>(x))).ToArray());
            if (src.PrinterAlert != null)
                dic.Add(IppAttributeNames.PrinterAlert, src.PrinterAlert.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.PrinterAlert, map.Map<string>(x))).ToArray());
            if (src.PrinterAlertDescription != null)
                dic.Add(IppAttributeNames.PrinterAlertDescription, src.PrinterAlertDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterAlertDescription, x)).ToArray());
            if (src.PrinterSupply != null)
                dic.Add(IppAttributeNames.PrinterSupply, src.PrinterSupply.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterSupply)).ToArray());
            if (src.PrinterInputTray != null)
                dic.Add(IppAttributeNames.PrinterInputTray, src.PrinterInputTray.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterInputTray)).ToArray());
            if (src.PrinterOutputTray != null)
                dic.Add(IppAttributeNames.PrinterOutputTray, src.PrinterOutputTray.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterOutputTray)).ToArray());
            if (src.JobConstraintsSupported != null)
                dic.Add(IppAttributeNames.JobConstraintsSupported, src.JobConstraintsSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobConstraintsSupported)).ToArray());
            if (src.JobPresetsSupported != null)
                dic.Add(IppAttributeNames.JobPresetsSupported, src.JobPresetsSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobPresetsSupported)).ToArray());
            if (src.JobResolversSupported != null)
                dic.Add(IppAttributeNames.JobResolversSupported, src.JobResolversSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobResolversSupported)).ToArray());
            if (src.JobTriggersSupported != null)
                dic.Add(IppAttributeNames.JobTriggersSupported, src.JobTriggersSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobTriggersSupported)).ToArray());
            if (src.PrintColorModeIccProfile != null)
                dic.Add(IppAttributeNames.PrintColorModeIccProfiles, src.PrintColorModeIccProfile.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrintColorModeIccProfiles)).ToArray());
            if (src.PrinterIccProfile != null)
                dic.Add(IppAttributeNames.PrinterIccProfiles, src.PrinterIccProfile.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterIccProfiles)).ToArray());
            if (src.PrinterSupplyDescription != null)
                dic.Add(IppAttributeNames.PrinterSupplyDescription, src.PrinterSupplyDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterSupplyDescription, x)).ToArray());
            if (src.JobCancelAfterDefault != null)
                dic.Add(IppAttributeNames.JobCancelAfterDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.JobCancelAfterDefault, src.JobCancelAfterDefault.Value)]);
            if (src.JobCancelAfterSupported != null)
                dic.Add(IppAttributeNames.JobCancelAfterSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobCancelAfterSupported, src.JobCancelAfterSupported.Value)]);
            if (src.JobSpoolingSupported != null)
                dic.Add(IppAttributeNames.JobSpoolingSupported, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobSpoolingSupported, src.JobSpoolingSupported.Value)]);
            if (src.MaxPageRangesSupported != null)
                dic.Add(IppAttributeNames.MaxPageRangesSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.MaxPageRangesSupported, src.MaxPageRangesSupported.Value)]);
            if (src.PrintContentOptimizeDefault != null)
                dic.Add(IppAttributeNames.PrintContentOptimizeDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrintContentOptimizeDefault, src.PrintContentOptimizeDefault.Value)]);
            if (src.PrintContentOptimizeSupported != null)
                dic.Add(IppAttributeNames.PrintContentOptimizeSupported, src.PrintContentOptimizeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintContentOptimizeSupported, x)).ToArray());
            if (src.OutputDeviceSupported != null)
                dic.Add(IppAttributeNames.OutputDeviceSupported, src.OutputDeviceSupported.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputDeviceSupported, x)).ToArray());
            if (src.OutputDeviceUuidSupported != null)
                dic.Add(IppAttributeNames.OutputDeviceUuidSupported, src.OutputDeviceUuidSupported.Select(x => new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuidSupported, x)).ToArray());
            if (src.DocumentAccessSupported != null)
                dic.Add(IppAttributeNames.DocumentAccessSupported, src.DocumentAccessSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.DocumentAccessSupported, map.Map<string>(x))).ToArray());
            if (src.FetchDocumentAttributesSupported != null)
                dic.Add(IppAttributeNames.FetchDocumentAttributesSupported, src.FetchDocumentAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.FetchDocumentAttributesSupported, map.Map<string>(x))).ToArray());
            if (src.PrinterModeConfigured != null)
                dic.Add(IppAttributeNames.PrinterModeConfigured, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterModeConfigured, src.PrinterModeConfigured.Value.Value)]);
            if (src.PrinterModeSupported != null)
                dic.Add(IppAttributeNames.PrinterModeSupported, src.PrinterModeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterModeSupported, x.Value)).ToArray());
            if (src.PrinterStaticResourceDirectoryUri != null)
                dic.Add(IppAttributeNames.PrinterStaticResourceDirectoryUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterStaticResourceDirectoryUri, src.PrinterStaticResourceDirectoryUri.ToString())]);
            if (src.PrinterStaticResourceKOctetsSupported != null)
                dic.Add(IppAttributeNames.PrinterStaticResourceKOctetsSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterStaticResourceKOctetsSupported, src.PrinterStaticResourceKOctetsSupported.Value)]);
            if (src.PrinterStaticResourceKOctetsFree != null)
                dic.Add(IppAttributeNames.PrinterStaticResourceKOctetsFree, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterStaticResourceKOctetsFree, src.PrinterStaticResourceKOctetsFree.Value)]);
            if (src.AccuracyUnitsSupported != null)
                dic.Add(IppAttributeNames.AccuracyUnitsSupported, src.AccuracyUnitsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.AccuracyUnitsSupported, x.Value)).ToArray());
            if (src.ChamberHumidityDefault != null)
                dic.Add(IppAttributeNames.ChamberHumidityDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidityDefault, src.ChamberHumidityDefault.Value)]);
            if (src.ChamberHumiditySupported != null)
                dic.Add(IppAttributeNames.ChamberHumiditySupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.ChamberHumiditySupported, src.ChamberHumiditySupported.Value)]);
            if (src.ChamberTemperatureDefault != null)
                dic.Add(IppAttributeNames.ChamberTemperatureDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperatureDefault, src.ChamberTemperatureDefault.Value)]);
            if (src.ChamberTemperatureSupported != null)
                dic.Add(IppAttributeNames.ChamberTemperatureSupported, src.ChamberTemperatureSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.ChamberTemperatureSupported, x)).ToArray());
            if (src.MaterialAmountUnitsSupported != null)
                dic.Add(IppAttributeNames.MaterialAmountUnitsSupported, src.MaterialAmountUnitsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MaterialAmountUnitsSupported, x.Value)).ToArray());
            if (src.MaterialDiameterSupported != null)
                dic.Add(IppAttributeNames.MaterialDiameterSupported, src.MaterialDiameterSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MaterialDiameterSupported, x)).ToArray());
            if (src.MaterialNozzleDiameterSupported != null)
                dic.Add(IppAttributeNames.MaterialNozzleDiameterSupported, src.MaterialNozzleDiameterSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MaterialNozzleDiameterSupported, x)).ToArray());
            if (src.MaterialPurposeSupported != null)
                dic.Add(IppAttributeNames.MaterialPurposeSupported, src.MaterialPurposeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MaterialPurposeSupported, x.Value)).ToArray());
            if (src.MaterialRateSupported != null)
                dic.Add(IppAttributeNames.MaterialRateSupported, src.MaterialRateSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MaterialRateSupported, x)).ToArray());
            if (src.MaterialRateUnitsSupported != null)
                dic.Add(IppAttributeNames.MaterialRateUnitsSupported, src.MaterialRateUnitsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MaterialRateUnitsSupported, x.Value)).ToArray());
            if (src.MaterialShellThicknessSupported != null)
                dic.Add(IppAttributeNames.MaterialShellThicknessSupported, src.MaterialShellThicknessSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MaterialShellThicknessSupported, x)).ToArray());
            if (src.MaterialTemperatureSupported != null)
                dic.Add(IppAttributeNames.MaterialTemperatureSupported, src.MaterialTemperatureSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.MaterialTemperatureSupported, x)).ToArray());
            if (src.MaterialTypeSupported != null)
                dic.Add(IppAttributeNames.MaterialTypeSupported, src.MaterialTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MaterialTypeSupported, x.Value)).ToArray());
            if (src.MaterialsColDatabase != null)
                dic.Add(IppAttributeNames.MaterialsColDatabase, src.MaterialsColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MaterialsColDatabase)).ToArray());
            if (src.MaterialsColDefault != null)
                dic.Add(IppAttributeNames.MaterialsColDefault, src.MaterialsColDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MaterialsColDefault)).ToArray());
            if (src.MaterialsColReady != null)
                dic.Add(IppAttributeNames.MaterialsColReady, src.MaterialsColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MaterialsColReady)).ToArray());
            if (src.MaterialsColSupported != null)
                dic.Add(IppAttributeNames.MaterialsColSupported, src.MaterialsColSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MaterialsColSupported, map.Map<string>(x))).ToArray());
            if (src.MaxMaterialsColSupported != null)
                dic.Add(IppAttributeNames.MaxMaterialsColSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.MaxMaterialsColSupported, src.MaxMaterialsColSupported.Value)]);
            if (src.MultipleObjectHandlingDefault != null)
                dic.Add(IppAttributeNames.MultipleObjectHandlingDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandlingDefault, src.MultipleObjectHandlingDefault.Value.Value)]);
            if (src.MultipleObjectHandlingSupported != null)
                dic.Add(IppAttributeNames.MultipleObjectHandlingSupported, src.MultipleObjectHandlingSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandlingSupported, x.Value)).ToArray());
            if (src.PdfFeaturesSupported != null)
                dic.Add(IppAttributeNames.PdfFeaturesSupported, src.PdfFeaturesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PdfFeaturesSupported, map.Map<string>(x))).ToArray());
            if (src.PlatformShape != null)
                dic.Add(IppAttributeNames.PlatformShape, [new IppAttribute(Tag.Keyword, IppAttributeNames.PlatformShape, src.PlatformShape.Value.Value)]);
            if (src.PlatformTemperatureDefault != null)
                dic.Add(IppAttributeNames.PlatformTemperatureDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperatureDefault, src.PlatformTemperatureDefault.Value)]);
            if (src.PlatformTemperatureSupported != null)
                dic.Add(IppAttributeNames.PlatformTemperatureSupported, src.PlatformTemperatureSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PlatformTemperatureSupported, x)).ToArray());
            if (src.PrintAccuracyDefault != null)
                dic.Add(IppAttributeNames.PrintAccuracyDefault, map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracyDefault).ToBegCollection(IppAttributeNames.PrintAccuracyDefault).ToArray());
            if (src.PrintAccuracySupported != null)
                dic.Add(IppAttributeNames.PrintAccuracySupported, map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracySupported).ToBegCollection(IppAttributeNames.PrintAccuracySupported).ToArray());
            if (src.PrintBaseDefault != null)
                dic.Add(IppAttributeNames.PrintBaseDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBaseDefault, src.PrintBaseDefault.Value.Value)]);
            if (src.PrintBaseSupported != null)
                dic.Add(IppAttributeNames.PrintBaseSupported, src.PrintBaseSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBaseSupported, x.Value)).ToArray());
            if (src.PrintObjectsSupported != null)
                dic.Add(IppAttributeNames.PrintObjectsSupported, src.PrintObjectsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintObjectsSupported, map.Map<string>(x))).ToArray());
            if (src.PrintSupportsDefault != null)
                dic.Add(IppAttributeNames.PrintSupportsDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupportsDefault, src.PrintSupportsDefault.Value.Value)]);
            if (src.PrintSupportsSupported != null)
                dic.Add(IppAttributeNames.PrintSupportsSupported, src.PrintSupportsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupportsSupported, x.Value)).ToArray());
            if (src.PrinterVolumeSupported != null)
                dic.Add(IppAttributeNames.PrinterVolumeSupported, map.Map<IEnumerable<IppAttribute>>(src.PrinterVolumeSupported).ToBegCollection(IppAttributeNames.PrinterVolumeSupported).ToArray());
            if (src.ChamberHumidityCurrent != null)
                dic.Add(IppAttributeNames.ChamberHumidityCurrent, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidityCurrent, src.ChamberHumidityCurrent.Value)]);
            if (src.ChamberTemperatureCurrent != null)
                dic.Add(IppAttributeNames.ChamberTemperatureCurrent, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperatureCurrent, src.ChamberTemperatureCurrent.Value)]);
            if (src.PrinterCameraImageUri != null)
                dic.Add(IppAttributeNames.PrinterCameraImageUri, src.PrinterCameraImageUri.Select(x => new IppAttribute(Tag.Uri, IppAttributeNames.PrinterCameraImageUri, x.ToString())).ToArray());
            if (src.PrinterResourceIds != null)
                dic.Add(IppAttributeNames.PrinterResourceIds, src.PrinterResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.PrinterResourceIds, x)).ToArray());
            if (src.JobCreationAttributesSupported != null)
                dic.Add(IppAttributeNames.JobCreationAttributesSupported, src.JobCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobCreationAttributesSupported, map.Map<string>(x))).ToArray());
            if (src.PrinterRequestedClientType != null)
                dic.Add(IppAttributeNames.PrinterRequestedClientType, src.PrinterRequestedClientType.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.PrinterRequestedClientType, (int)x)).ToArray());
            if (src.PrinterServiceType != null)
                dic.Add(IppAttributeNames.PrinterServiceType, src.PrinterServiceType.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceType, map.Map<string>(x))).ToArray());
            if (src.FinishingTemplateSupported != null)
                dic.Add(IppAttributeNames.FinishingTemplateSupported, src.FinishingTemplateSupported.Select(x =>
                {
                    var finishingTemplateTag = x.ToIppTag();
                    return new IppAttribute(finishingTemplateTag, IppAttributeNames.FinishingTemplateSupported, x);
                }).ToArray());
            if (src.FinishingsColSupported != null)
                dic.Add(IppAttributeNames.FinishingsColSupported, src.FinishingsColSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.FinishingsColSupported, x)).ToArray());
            if (src.JobPagesPerSetSupported != null)
                dic.Add(IppAttributeNames.JobPagesPerSetSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobPagesPerSetSupported, src.JobPagesPerSetSupported.Value)]);
            if (src.PunchingHoleDiameterConfigured != null)
                dic.Add(IppAttributeNames.PunchingHoleDiameterConfigured, [new IppAttribute(Tag.Integer, IppAttributeNames.PunchingHoleDiameterConfigured, src.PunchingHoleDiameterConfigured.Value)]);
            if (src.PrinterFinisher != null)
                dic.Add(IppAttributeNames.PrinterFinisher, src.PrinterFinisher.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.PrinterFinisher, map.Map<string>(x))).ToArray());
            if (src.PrinterFinisherDescription != null)
                dic.Add(IppAttributeNames.PrinterFinisherDescription, src.PrinterFinisherDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterFinisherDescription, x)).ToArray());
            if (src.PrinterFinisherSupplies != null)
                dic.Add(IppAttributeNames.PrinterFinisherSupplies, src.PrinterFinisherSupplies.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.PrinterFinisherSupplies, map.Map<string>(x))).ToArray());
            if (src.PrinterFinisherSuppliesDescription != null)
                dic.Add(IppAttributeNames.PrinterFinisherSuppliesDescription, src.PrinterFinisherSuppliesDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterFinisherSuppliesDescription, x)).ToArray());
            if (src.FinishingsColDefault != null)
                dic.Add(
                    IppAttributeNames.FinishingsColDefault,
                    src.FinishingsColDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.FinishingsColDefault)
                    ).ToArray());
            if (src.FinishingsColReady != null)
                dic.Add(IppAttributeNames.FinishingsColReady, src.FinishingsColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.FinishingsColReady)).ToArray());
            if (src.BalingTypeSupported != null)
                dic.Add(IppAttributeNames.BalingTypeSupported, src.BalingTypeSupported.Select(x =>
                {
                    var balingTypeTag = x.ToIppTag();
                    return new IppAttribute(balingTypeTag, IppAttributeNames.BalingTypeSupported, x);
                }).ToArray());
            if (src.BalingWhenSupported != null)
                dic.Add(IppAttributeNames.BalingWhenSupported, src.BalingWhenSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.BalingWhenSupported, map.Map<string>(x))).ToArray());
            if (src.BindingReferenceEdgeSupported != null)
                dic.Add(IppAttributeNames.BindingReferenceEdgeSupported, src.BindingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.BindingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.BindingTypeSupported != null)
                dic.Add(IppAttributeNames.BindingTypeSupported, src.BindingTypeSupported.Select(x =>
                {
                    var bindingTypeTag = x.ToIppTag();
                    return new IppAttribute(bindingTypeTag, IppAttributeNames.BindingTypeSupported, x);
                }).ToArray());
            if (src.CoatingSidesSupported != null)
                dic.Add(IppAttributeNames.CoatingSidesSupported, src.CoatingSidesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CoatingSidesSupported, map.Map<string>(x))).ToArray());
            if (src.CoatingTypeSupported != null)
                dic.Add(IppAttributeNames.CoatingTypeSupported, src.CoatingTypeSupported.Select(x =>
                {
                    var coatingTypeTag = x.ToIppTag();
                    return new IppAttribute(coatingTypeTag, IppAttributeNames.CoatingTypeSupported, x);
                }).ToArray());
            if (src.CoveringNameSupported != null)
                dic.Add(IppAttributeNames.CoveringNameSupported, src.CoveringNameSupported.Select(x =>
                {
                    var coveringNameTag = x.ToIppTag();
                    return new IppAttribute(coveringNameTag, IppAttributeNames.CoveringNameSupported, x);
                }).ToArray());
            if (src.FinishingsColDatabase != null)
                dic.Add(
                    IppAttributeNames.FinishingsColDatabase,
                    src.FinishingsColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.FinishingsColDatabase)
                    ).ToArray());
            if (src.FoldingDirectionSupported != null)
                dic.Add(IppAttributeNames.FoldingDirectionSupported, src.FoldingDirectionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.FoldingDirectionSupported, map.Map<string>(x))).ToArray());
            if (src.FoldingOffsetSupported != null)
                dic.Add(IppAttributeNames.FoldingOffsetSupported, src.FoldingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.FoldingOffsetSupported, x)).ToArray());
            if (src.FoldingReferenceEdgeSupported != null)
                dic.Add(IppAttributeNames.FoldingReferenceEdgeSupported, src.FoldingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.FoldingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.LaminatingSidesSupported != null)
                dic.Add(IppAttributeNames.LaminatingSidesSupported, src.LaminatingSidesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.LaminatingSidesSupported, map.Map<string>(x))).ToArray());
            if (src.LaminatingTypeSupported != null)
                dic.Add(IppAttributeNames.LaminatingTypeSupported, src.LaminatingTypeSupported.Select(x =>
                {
                    var laminatingTypeTag = x.ToIppTag();
                    return new IppAttribute(laminatingTypeTag, IppAttributeNames.LaminatingTypeSupported, x);
                }).ToArray());
            if (src.PunchingLocationsSupported != null)
                dic.Add(IppAttributeNames.PunchingLocationsSupported, src.PunchingLocationsSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PunchingLocationsSupported, x)).ToArray());
            if (src.PunchingOffsetSupported != null)
                dic.Add(IppAttributeNames.PunchingOffsetSupported, src.PunchingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PunchingOffsetSupported, x)).ToArray());
            if (src.PunchingReferenceEdgeSupported != null)
                dic.Add(IppAttributeNames.PunchingReferenceEdgeSupported, src.PunchingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PunchingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.StitchingAngleSupported != null)
                dic.Add(IppAttributeNames.StitchingAngleSupported, src.StitchingAngleSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.StitchingAngleSupported, x)).ToArray());
            if (src.StitchingLocationsSupported != null)
                dic.Add(IppAttributeNames.StitchingLocationsSupported, src.StitchingLocationsSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.StitchingLocationsSupported, x)).ToArray());
            if (src.StitchingMethodSupported != null)
                dic.Add(IppAttributeNames.StitchingMethodSupported, src.StitchingMethodSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.StitchingMethodSupported, map.Map<string>(x))).ToArray());
            if (src.StitchingOffsetSupported != null)
                dic.Add(IppAttributeNames.StitchingOffsetSupported, src.StitchingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.StitchingOffsetSupported, x)).ToArray());
            if (src.StitchingReferenceEdgeSupported != null)
                dic.Add(IppAttributeNames.StitchingReferenceEdgeSupported, src.StitchingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.StitchingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.TrimmingOffsetSupported != null)
                dic.Add(IppAttributeNames.TrimmingOffsetSupported, src.TrimmingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.TrimmingOffsetSupported, x)).ToArray());
            if (src.TrimmingReferenceEdgeSupported != null)
                dic.Add(IppAttributeNames.TrimmingReferenceEdgeSupported, src.TrimmingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.TrimmingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.TrimmingTypeSupported != null)
                dic.Add(IppAttributeNames.TrimmingTypeSupported, src.TrimmingTypeSupported.Select(x =>
                {
                    var trimmingTypeTag = x.ToIppTag();
                    return new IppAttribute(trimmingTypeTag, IppAttributeNames.TrimmingTypeSupported, map.Map<string>(x));
                }).ToArray());
            if (src.TrimmingWhenSupported != null)
                dic.Add(IppAttributeNames.TrimmingWhenSupported, src.TrimmingWhenSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.TrimmingWhenSupported, map.Map<string>(x))).ToArray());
            if (src.CoverBackDefault != null)
                dic.Add(IppAttributeNames.CoverBackDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverBackDefault).ToBegCollection(IppAttributeNames.CoverBackDefault).ToArray());
            if (src.CoverBackSupported != null)
                dic.Add(IppAttributeNames.CoverBackSupported, src.CoverBackSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CoverBackSupported, map.Map<string>(x))).ToArray());
            if (src.CoverFrontDefault != null)
                dic.Add(IppAttributeNames.CoverFrontDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverFrontDefault).ToBegCollection(IppAttributeNames.CoverFrontDefault).ToArray());
            if (src.CoverFrontSupported != null)
                dic.Add(IppAttributeNames.CoverFrontSupported, src.CoverFrontSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CoverFrontSupported, map.Map<string>(x))).ToArray());
            if (src.CoverTypeSupported != null)
                dic.Add(IppAttributeNames.CoverTypeSupported, src.CoverTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CoverTypeSupported, map.Map<string>(x))).ToArray());
            if (src.ForceFrontSideSupported != null)
                dic.Add(IppAttributeNames.ForceFrontSideSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.ForceFrontSideSupported, src.ForceFrontSideSupported.Value)]);
            if (src.ImageOrientationDefault != null)
                dic.Add(IppAttributeNames.ImageOrientationDefault, [new IppAttribute(Tag.Enum, IppAttributeNames.ImageOrientationDefault, (int)src.ImageOrientationDefault.Value)]);
            if (src.ImageOrientationSupported != null)
                dic.Add(IppAttributeNames.ImageOrientationSupported, src.ImageOrientationSupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.ImageOrientationSupported, (int)x)).ToArray());
            if (src.ImpositionTemplateDefault != null)
            {
                var impositionTemplateDefaultTag = src.ImpositionTemplateDefault.Value.ToIppTag();
                dic.Add(IppAttributeNames.ImpositionTemplateDefault, [new IppAttribute(impositionTemplateDefaultTag, IppAttributeNames.ImpositionTemplateDefault, map.Map<string>(src.ImpositionTemplateDefault.Value))]);
            }
            if (src.ImpositionTemplateSupported != null)
                dic.Add(IppAttributeNames.ImpositionTemplateSupported, src.ImpositionTemplateSupported.Select(x =>
                {
                    var impositionTemplateTag = x.ToIppTag();
                    return new IppAttribute(impositionTemplateTag, IppAttributeNames.ImpositionTemplateSupported, map.Map<string>(x));
                }).ToArray());
            if (src.InsertCountSupported != null)
                dic.Add(IppAttributeNames.InsertCountSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.InsertCountSupported, src.InsertCountSupported.Value)]);
            if (src.InsertSheetDefault != null)
                dic.Add(IppAttributeNames.InsertSheetDefault, src.InsertSheetDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.InsertSheetDefault)).ToArray());
            if (src.InsertSheetSupported != null)
                dic.Add(IppAttributeNames.InsertSheetSupported, src.InsertSheetSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InsertSheetSupported, map.Map<string>(x))).ToArray());
            if (src.JobAccountingOutputBinSupported != null)
                dic.Add(IppAttributeNames.JobAccountingOutputBinSupported, src.JobAccountingOutputBinSupported.Select(x =>
                {
                    var outputBinTag = x.ToIppTag();
                    return new IppAttribute(outputBinTag, IppAttributeNames.JobAccountingOutputBinSupported, x.ToString());
                }).ToArray());
            if (src.JobAccountingSheetsDefault != null)
                dic.Add(IppAttributeNames.JobAccountingSheetsDefault, map.Map<IEnumerable<IppAttribute>>(src.JobAccountingSheetsDefault).ToBegCollection(IppAttributeNames.JobAccountingSheetsDefault).ToArray());
            if (src.JobAccountingSheetsSupported != null)
                dic.Add(IppAttributeNames.JobAccountingSheetsSupported, src.JobAccountingSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobAccountingSheetsSupported, map.Map<string>(x))).ToArray());
            if (src.JobAccountingSheetsTypeSupported != null)
                dic.Add(IppAttributeNames.JobAccountingSheetsTypeSupported, src.JobAccountingSheetsTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobAccountingSheetsTypeSupported, x)).ToArray());
            if (src.JobCompleteBeforeSupported != null)
                dic.Add(IppAttributeNames.JobCompleteBeforeSupported, src.JobCompleteBeforeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobCompleteBeforeSupported, x)).ToArray());
            if (src.JobCompleteBeforeTimeSupported != null)
                dic.Add(IppAttributeNames.JobCompleteBeforeTimeSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobCompleteBeforeTimeSupported, src.JobCompleteBeforeTimeSupported.Value)]);
            if (src.JobErrorSheetDefault != null)
                dic.Add(IppAttributeNames.JobErrorSheetDefault, map.Map<IEnumerable<IppAttribute>>(src.JobErrorSheetDefault).ToBegCollection(IppAttributeNames.JobErrorSheetDefault).ToArray());
            if (src.JobErrorSheetSupported != null)
                dic.Add(IppAttributeNames.JobErrorSheetSupported, src.JobErrorSheetSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobErrorSheetSupported, map.Map<string>(x))).ToArray());
            if (src.JobErrorSheetTypeSupported != null)
                dic.Add(IppAttributeNames.JobErrorSheetTypeSupported, src.JobErrorSheetTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobErrorSheetTypeSupported, x)).ToArray());
            if (src.JobErrorSheetWhenSupported != null)
                dic.Add(IppAttributeNames.JobErrorSheetWhenSupported, src.JobErrorSheetWhenSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobErrorSheetWhenSupported, x)).ToArray());
            if (src.JobMessageToOperatorSupported != null)
                dic.Add(IppAttributeNames.JobMessageToOperatorSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobMessageToOperatorSupported, src.JobMessageToOperatorSupported.Value)]);
            if (src.JobPhoneNumberDefault != null)
                dic.Add(IppAttributeNames.JobPhoneNumberDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobPhoneNumberDefault, src.JobPhoneNumberDefault)]);
            if (src.JobPhoneNumberSchemeSupported != null)
                dic.Add(IppAttributeNames.JobPhoneNumberSchemeSupported, src.JobPhoneNumberSchemeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobPhoneNumberSchemeSupported, map.Map<string>(x))).ToArray());
            if (src.JobPhoneNumberSupported != null)
                dic.Add(IppAttributeNames.JobPhoneNumberSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobPhoneNumberSupported, src.JobPhoneNumberSupported.Value)]);
            if (src.JobRecipientNameSupported != null)
                dic.Add(IppAttributeNames.JobRecipientNameSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobRecipientNameSupported, src.JobRecipientNameSupported.Value)]);
            if (src.JobSheetMessageSupported != null)
                dic.Add(IppAttributeNames.JobSheetMessageSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.JobSheetMessageSupported, src.JobSheetMessageSupported.Value)]);
            if (src.PageDeliveryDefault != null)
                dic.Add(IppAttributeNames.PageDeliveryDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.PageDeliveryDefault, map.Map<string>(src.PageDeliveryDefault.Value))]);
            if (src.PageDeliverySupported != null)
                dic.Add(IppAttributeNames.PageDeliverySupported, src.PageDeliverySupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PageDeliverySupported, map.Map<string>(x))).ToArray());
            if (src.PresentationDirectionNumberUpDefault != null)
                dic.Add(IppAttributeNames.PresentationDirectionNumberUpDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.PresentationDirectionNumberUpDefault, map.Map<string>(src.PresentationDirectionNumberUpDefault.Value))]);
            if (src.PresentationDirectionNumberUpSupported != null)
                dic.Add(IppAttributeNames.PresentationDirectionNumberUpSupported, src.PresentationDirectionNumberUpSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PresentationDirectionNumberUpSupported, map.Map<string>(x))).ToArray());
            if (src.SeparatorSheetsDefault != null)
                dic.Add(IppAttributeNames.SeparatorSheetsDefault, map.Map<IEnumerable<IppAttribute>>(src.SeparatorSheetsDefault).ToBegCollection(IppAttributeNames.SeparatorSheetsDefault).ToArray());
            if (src.SeparatorSheetsSupported != null)
                dic.Add(IppAttributeNames.SeparatorSheetsSupported, src.SeparatorSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SeparatorSheetsSupported, map.Map<string>(x))).ToArray());
            if (src.SeparatorSheetsTypeSupported != null)
                dic.Add(IppAttributeNames.SeparatorSheetsTypeSupported, src.SeparatorSheetsTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SeparatorSheetsTypeSupported, x)).ToArray());
            if (src.XImagePositionDefault != null)
                dic.Add(IppAttributeNames.XImagePositionDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.XImagePositionDefault, map.Map<string>(src.XImagePositionDefault.Value))]);
            if (src.XImagePositionSupported != null)
                dic.Add(IppAttributeNames.XImagePositionSupported, src.XImagePositionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.XImagePositionSupported, map.Map<string>(x))).ToArray());
            if (src.XImageShiftDefault != null)
                dic.Add(IppAttributeNames.XImageShiftDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.XImageShiftDefault, src.XImageShiftDefault.Value)]);
            if (src.XImageShiftSupported != null)
                dic.Add(IppAttributeNames.XImageShiftSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.XImageShiftSupported, src.XImageShiftSupported.Value)]);
            if (src.XSide1ImageShiftDefault != null)
                dic.Add(IppAttributeNames.XSide1ImageShiftDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.XSide1ImageShiftDefault, src.XSide1ImageShiftDefault.Value)]);
            if (src.XSide2ImageShiftDefault != null)
                dic.Add(IppAttributeNames.XSide2ImageShiftDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.XSide2ImageShiftDefault, src.XSide2ImageShiftDefault.Value)]);
            if (src.YImagePositionDefault != null)
                dic.Add(IppAttributeNames.YImagePositionDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.YImagePositionDefault, map.Map<string>(src.YImagePositionDefault.Value))]);
            if (src.YImagePositionSupported != null)
                dic.Add(IppAttributeNames.YImagePositionSupported, src.YImagePositionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.YImagePositionSupported, map.Map<string>(x))).ToArray());
            if (src.YImageShiftDefault != null)
                dic.Add(IppAttributeNames.YImageShiftDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.YImageShiftDefault, src.YImageShiftDefault.Value)]);
            if (src.YImageShiftSupported != null)
                dic.Add(IppAttributeNames.YImageShiftSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.YImageShiftSupported, src.YImageShiftSupported.Value)]);
            if (src.YSide1ImageShiftDefault != null)
                dic.Add(IppAttributeNames.YSide1ImageShiftDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.YSide1ImageShiftDefault, src.YSide1ImageShiftDefault.Value)]);
            if (src.YSide2ImageShiftDefault != null)
                dic.Add(IppAttributeNames.YSide2ImageShiftDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.YSide2ImageShiftDefault, src.YSide2ImageShiftDefault.Value)]);
            if (src.ConfirmationSheetPrintDefault.HasValue)
                dic.Add(IppAttributeNames.ConfirmationSheetPrintDefault, [new IppAttribute(Tag.Boolean, IppAttributeNames.ConfirmationSheetPrintDefault, src.ConfirmationSheetPrintDefault.Value)]);
            if (src.CoverSheetInfoDefault != null)
                dic.Add(IppAttributeNames.CoverSheetInfoDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverSheetInfoDefault).ToBegCollection(IppAttributeNames.CoverSheetInfoDefault).ToArray());
            if (src.CoverSheetInfoSupported != null)
                dic.Add(IppAttributeNames.CoverSheetInfoSupported, src.CoverSheetInfoSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CoverSheetInfoSupported, map.Map<string>(x))).ToArray());
            if (src.DestinationAccessesSupported != null)
                dic.Add(IppAttributeNames.DestinationAccessesSupported, src.DestinationAccessesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.DestinationAccessesSupported, map.Map<string>(x))).ToArray());
            if (src.DestinationUriReady != null)
                dic.Add(IppAttributeNames.DestinationUriReady, src.DestinationUriReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.DestinationUriReady)).ToArray());
            if (src.DestinationUriSchemesSupported != null)
                dic.Add(IppAttributeNames.DestinationUriSchemesSupported, src.DestinationUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, IppAttributeNames.DestinationUriSchemesSupported, map.Map<string>(x))).ToArray());
            if (src.DestinationUrisSupported != null)
                dic.Add(IppAttributeNames.DestinationUrisSupported, src.DestinationUrisSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.DestinationUrisSupported, x.ToString())).ToArray());
            if (src.FromNameSupported.HasValue)
                dic.Add(IppAttributeNames.FromNameSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.FromNameSupported, src.FromNameSupported.Value)]);
            if (src.InputAttributesDefault != null)
                dic.Add(IppAttributeNames.InputAttributesDefault, map.Map<IEnumerable<IppAttribute>>(src.InputAttributesDefault).ToBegCollection(IppAttributeNames.InputAttributesDefault).ToArray());
            if (src.InputAttributesSupported != null)
                dic.Add(IppAttributeNames.InputAttributesSupported, src.InputAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InputAttributesSupported, map.Map<string>(x))).ToArray());
            if (src.InputColorModeSupported != null)
                dic.Add(IppAttributeNames.InputColorModeSupported, src.InputColorModeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InputColorModeSupported, x.Value)).ToArray());
            if (src.InputContentTypeSupported != null)
                dic.Add(IppAttributeNames.InputContentTypeSupported, src.InputContentTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InputContentTypeSupported, x.Value)).ToArray());
            if (src.InputFilmScanModeSupported != null)
                dic.Add(IppAttributeNames.InputFilmScanModeSupported, src.InputFilmScanModeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InputFilmScanModeSupported, x.Value)).ToArray());
            if (src.InputMediaSupported != null)
                dic.Add(IppAttributeNames.InputMediaSupported, src.InputMediaSupported.Select(x => new IppAttribute(x.ToIppTag(), IppAttributeNames.InputMediaSupported, map.Map<string>(x))).ToArray());
            if (src.InputOrientationRequestedSupported != null)
                dic.Add(IppAttributeNames.InputOrientationRequestedSupported, src.InputOrientationRequestedSupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.InputOrientationRequestedSupported, (int)x)).ToArray());
            if (src.InputQualitySupported != null)
                dic.Add(IppAttributeNames.InputQualitySupported, src.InputQualitySupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.InputQualitySupported, (int)x)).ToArray());
            if (src.InputResolutionSupported != null)
                dic.Add(IppAttributeNames.InputResolutionSupported, src.InputResolutionSupported.Select(x => new IppAttribute(Tag.Resolution, IppAttributeNames.InputResolutionSupported, x)).ToArray());
            if (src.InputSidesSupported != null)
                dic.Add(IppAttributeNames.InputSidesSupported, src.InputSidesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InputSidesSupported, map.Map<string>(x))).ToArray());
            if (src.InputSourceSupported != null)
                dic.Add(IppAttributeNames.InputSourceSupported, src.InputSourceSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.InputSourceSupported, x.Value)).ToArray());
            if (src.LogoUriFormatsSupported != null)
                dic.Add(IppAttributeNames.LogoUriFormatsSupported, src.LogoUriFormatsSupported.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.LogoUriFormatsSupported, x)).ToArray());
            if (src.LogoUriSchemesSupported != null)
                dic.Add(IppAttributeNames.LogoUriSchemesSupported, src.LogoUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, IppAttributeNames.LogoUriSchemesSupported, map.Map<string>(x))).ToArray());
            if (src.MessageSupported.HasValue)
                dic.Add(IppAttributeNames.MessageSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.MessageSupported, src.MessageSupported.Value)]);
            if (src.MultipleDestinationUrisSupported.HasValue)
                dic.Add(IppAttributeNames.MultipleDestinationUrisSupported, [new IppAttribute(Tag.Boolean, IppAttributeNames.MultipleDestinationUrisSupported, src.MultipleDestinationUrisSupported.Value)]);
            if (src.NumberOfRetriesDefault.HasValue)
                dic.Add(IppAttributeNames.NumberOfRetriesDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfRetriesDefault, src.NumberOfRetriesDefault.Value)]);
            if (src.NumberOfRetriesSupported != null)
                dic.Add(IppAttributeNames.NumberOfRetriesSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.NumberOfRetriesSupported, src.NumberOfRetriesSupported.Value)]);
            if (src.OrganizationNameSupported.HasValue)
                dic.Add(IppAttributeNames.OrganizationNameSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.OrganizationNameSupported, src.OrganizationNameSupported.Value)]);
            if (src.JobDestinationSpoolingSupported != null)
                dic.Add(IppAttributeNames.JobDestinationSpoolingSupported, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobDestinationSpoolingSupported, map.Map<string>(src.JobDestinationSpoolingSupported.Value))]);
            if (src.OutputAttributesDefault != null)
                dic.Add(IppAttributeNames.OutputAttributesDefault, map.Map<IEnumerable<IppAttribute>>(src.OutputAttributesDefault).ToBegCollection(IppAttributeNames.OutputAttributesDefault).ToArray());
            if (src.OutputAttributesSupported != null)
                dic.Add(IppAttributeNames.OutputAttributesSupported, src.OutputAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.OutputAttributesSupported, map.Map<string>(x))).ToArray());
            if (src.PrinterFaxLogUri != null)
                dic.Add(IppAttributeNames.PrinterFaxLogUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterFaxLogUri, src.PrinterFaxLogUri.ToString())]);
            if (src.PrinterFaxModemInfo != null)
                dic.Add(IppAttributeNames.PrinterFaxModemInfo, src.PrinterFaxModemInfo.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterFaxModemInfo, x)).ToArray());
            if (src.PrinterFaxModemName != null)
                dic.Add(IppAttributeNames.PrinterFaxModemName, src.PrinterFaxModemName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.PrinterFaxModemName, x)).ToArray());
            if (src.PrinterFaxModemNumber != null)
                dic.Add(IppAttributeNames.PrinterFaxModemNumber, src.PrinterFaxModemNumber.Select(x => new IppAttribute(Tag.Uri, IppAttributeNames.PrinterFaxModemNumber, x.ToString())).ToArray());
            if (src.RetryIntervalDefault.HasValue)
                dic.Add(IppAttributeNames.RetryIntervalDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.RetryIntervalDefault, src.RetryIntervalDefault.Value)]);
            if (src.RetryIntervalSupported != null)
                dic.Add(IppAttributeNames.RetryIntervalSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.RetryIntervalSupported, src.RetryIntervalSupported.Value)]);
            if (src.RetryTimeOutDefault.HasValue)
                dic.Add(IppAttributeNames.RetryTimeOutDefault, [new IppAttribute(Tag.Integer, IppAttributeNames.RetryTimeOutDefault, src.RetryTimeOutDefault.Value)]);
            if (src.RetryTimeOutSupported != null)
                dic.Add(IppAttributeNames.RetryTimeOutSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.RetryTimeOutSupported, src.RetryTimeOutSupported.Value)]);
            if (src.SubjectSupported.HasValue)
                dic.Add(IppAttributeNames.SubjectSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.SubjectSupported, src.SubjectSupported.Value)]);
            if (src.ToNameSupported.HasValue)
                dic.Add(IppAttributeNames.ToNameSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.ToNameSupported, src.ToNameSupported.Value)]);
            if (src.JpegXDimensionSupported != null)
                dic.Add(IppAttributeNames.JpegXDimensionSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JpegXDimensionSupported, src.JpegXDimensionSupported.Value)]);
            if (src.JpegYDimensionSupported != null)
                dic.Add(IppAttributeNames.JpegYDimensionSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JpegYDimensionSupported, src.JpegYDimensionSupported.Value)]);
            if (src.JobPasswordSupported.HasValue)
                dic.Add(IppAttributeNames.JobPasswordSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPasswordSupported, src.JobPasswordSupported.Value)]);
            if (src.JobPasswordLengthSupported != null)
                dic.Add(IppAttributeNames.JobPasswordLengthSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.JobPasswordLengthSupported, src.JobPasswordLengthSupported.Value)]);
            if (src.DocumentPasswordSupported.HasValue)
                dic.Add(IppAttributeNames.DocumentPasswordSupported, [new IppAttribute(Tag.Integer, IppAttributeNames.DocumentPasswordSupported, src.DocumentPasswordSupported.Value)]);
            if (src.XSide1ImageOffsetSupported != null)
                dic.Add(IppAttributeNames.XSide1ImageOffsetSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.XSide1ImageOffsetSupported, src.XSide1ImageOffsetSupported.Value)]);
            if (src.XSide2ImageOffsetSupported != null)
                dic.Add(IppAttributeNames.XSide2ImageOffsetSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.XSide2ImageOffsetSupported, src.XSide2ImageOffsetSupported.Value)]);
            if (src.YSide1ImageOffsetSupported != null)
                dic.Add(IppAttributeNames.YSide1ImageOffsetSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.YSide1ImageOffsetSupported, src.YSide1ImageOffsetSupported.Value)]);
            if (src.YSide2ImageOffsetSupported != null)
                dic.Add(IppAttributeNames.YSide2ImageOffsetSupported, [new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.YSide2ImageOffsetSupported, src.YSide2ImageOffsetSupported.Value)]);
            if (src.UserDefinedValuesSupported != null)
                dic.Add(IppAttributeNames.UserDefinedValuesSupported, src.UserDefinedValuesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.UserDefinedValuesSupported, x)).ToArray());
            if (src.PdlInitFileSupported != null)
                dic.Add(IppAttributeNames.PdlInitFileSupported, src.PdlInitFileSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PdlInitFileSupported, x)).ToArray());
            if (src.PdlInitFileDefault != null)
                dic.Add(IppAttributeNames.PdlInitFileDefault, map.Map<IEnumerable<IppAttribute>>(src.PdlInitFileDefault).ToBegCollection(IppAttributeNames.PdlInitFileDefault).ToArray());
            if (src.JobSaveDispositionSupported != null)
                dic.Add(IppAttributeNames.JobSaveDispositionSupported, src.JobSaveDispositionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobSaveDispositionSupported, x)).ToArray());
            if (src.JobSaveDispositionDefault != null)
                dic.Add(IppAttributeNames.JobSaveDispositionDefault, map.Map<IEnumerable<IppAttribute>>(src.JobSaveDispositionDefault).ToBegCollection(IppAttributeNames.JobSaveDispositionDefault).ToArray());
            if (src.SaveDispositionSupported != null)
                dic.Add(IppAttributeNames.SaveDispositionSupported, src.SaveDispositionSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SaveDispositionSupported, x.Value)).ToArray());
            if (src.SaveInfoSupported != null)
                dic.Add(IppAttributeNames.SaveInfoSupported, src.SaveInfoSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SaveInfoSupported, x)).ToArray());
            if (src.SaveLocationSupported != null)
                dic.Add(IppAttributeNames.SaveLocationSupported, src.SaveLocationSupported.Select(x => new IppAttribute(Tag.Uri, IppAttributeNames.SaveLocationSupported, x.ToString())).ToArray());
            return dic;
        });
    }
}
