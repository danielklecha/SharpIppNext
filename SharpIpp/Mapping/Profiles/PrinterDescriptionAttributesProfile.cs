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
            return new PrinterDescriptionAttributes
            {
                CharsetConfigured = map.MapFromDicNullable<string?>(src, PrinterAttribute.CharsetConfigured),
                CharsetSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.CharsetSupported),
                ColorSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.ColorSupported),
                CompressionDefault = map.MapFromDicNullable<Compression?>(src, PrinterAttribute.CompressionDefault),
                CompressionSupported = map.MapFromDicSetNullable<Compression[]?>(src, PrinterAttribute.CompressionSupported),
                DocumentFormatDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.DocumentFormatDefault),
                DocumentFormatSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentFormatSupported),
                ClientInfoSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.ClientInfoSupported),
                MaxClientInfoSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.MaxClientInfoSupported),
                DocumentCharsetDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.DocumentCharsetDefault),
                DocumentCharsetSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentCharsetSupported),
                DocumentFormatDetailsSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentFormatDetailsSupported),
                DocumentNaturalLanguageDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.DocumentNaturalLanguageDefault),
                DocumentNaturalLanguageSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentNaturalLanguageSupported),
                JobIdsSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobIdsSupported),
                JobMandatoryAttributesSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobMandatoryAttributesSupported),
                JobSheetsColDefault = src.ContainsKey(PrinterAttribute.JobSheetsColDefault)
                    ? map.Map<JobSheetsCol>(src[PrinterAttribute.JobSheetsColDefault].FromBegCollection().ToIppDictionary())
                    : null,
                JobSheetsColSupported = map.MapFromDicSetNullable<JobSheetsColMember[]?>(src, PrinterAttribute.JobSheetsColSupported),
                GeneratedNaturalLanguageSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.GeneratedNaturalLanguageSupported),
                IppVersionsSupported = map.MapFromDicSetNullable<IppVersion[]?>(src, PrinterAttribute.IppVersionsSupported),
                JobImpressionsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobImpressionsSupported),
                JobKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobKOctetsSupported),
                JpegKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JpegKOctetsSupported),
                PdfKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.PdfKOctetsSupported),
                JobMediaSheetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobMediaSheetsSupported),
                JobSheetsDefault = map.MapFromDicNullable<JobSheets?>(src, PrinterAttribute.JobSheetsDefault),
                JobSheetsSupported = map.MapFromDicSetNullable<JobSheets[]?>(src, PrinterAttribute.JobSheetsSupported),
                NumberUpDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.NumberUpDefault),
                NumberUpSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.NumberUpSupported),
                MultipleDocumentJobsSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.MultipleDocumentJobsSupported),
                MultipleDocumentHandlingDefault = map.MapFromDicNullable<MultipleDocumentHandling?>(src, PrinterAttribute.MultipleDocumentHandlingDefault),
                MultipleDocumentHandlingSupported = map.MapFromDicSetNullable<MultipleDocumentHandling[]?>(src, PrinterAttribute.MultipleDocumentHandlingSupported),
                MultipleOperationTimeOut = map.MapFromDicNullable<int?>(src, PrinterAttribute.MultipleOperationTimeOut),
                NaturalLanguageConfigured = map.MapFromDicNullable<string?>(src, PrinterAttribute.NaturalLanguageConfigured),
                OperationsSupported = map.MapFromDicSetNullable<IppOperation[]?>(src, PrinterAttribute.OperationsSupported),
                PagesPerMinute = map.MapFromDicNullable<int?>(src, PrinterAttribute.PagesPerMinute),
                PdlOverrideSupported = map.MapFromDicNullable<PdlOverride?>(src, PrinterAttribute.PdlOverrideSupported),
                OverridesSupported = map.MapFromDicSetNullable<OverrideSupported[]?>(src, PrinterAttribute.OverridesSupported),
                PagesPerMinuteColor = map.MapFromDicNullable<int?>(src, PrinterAttribute.PagesPerMinuteColor),
                PrinterCurrentTime = map.MapFromDicNullable<DateTimeOffset?>(src, PrinterAttribute.PrinterCurrentTime),
                PrinterConfigChangeTime = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterConfigChangeTime),
                PrinterConfigChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, PrinterAttribute.PrinterConfigChangeDateTime),
                PrinterConfigChanges = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterConfigChanges),
                PrinterContactCol = src.ContainsKey(PrinterAttribute.PrinterContactCol)
                    ? src[PrinterAttribute.PrinterContactCol].GroupBegCollection().Select(x => map.Map<SystemContact>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                PrinterGeoLocation = map.MapFromDicNullable<Uri?>(src, PrinterAttribute.PrinterGeoLocation),
                PrinterIds = map.MapFromDicSetNullable<int[]?>(src, PrinterAttribute.PrinterIds),
                PrinterImpressionsCompleted = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterImpressionsCompleted),
                PrinterImpressionsCompletedCol = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterImpressionsCompletedCol),
                PrinterMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterMediaSheetsCompleted),
                PrinterMediaSheetsCompletedCol = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterMediaSheetsCompletedCol),
                PrinterPagesCompleted = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterPagesCompleted),
                PrinterPagesCompletedCol = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterPagesCompletedCol),
                PrinterDriverInstaller = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterDriverInstaller),
                PrinterInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PrinterIsAcceptingJobs),
                PrinterLocation = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterLocation),
                PrinterMakeAndModel = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMakeAndModel),
                PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMessageFromOperator),
                PrinterMoreInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMoreInfo),
                PrinterMoreInfoManufacturer = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMoreInfoManufacturer),
                PrinterName = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterName),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, PrinterAttribute.PrinterState),
                PrinterStateMessage = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterStateMessage),
                PrinterStateChangeTime = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterStateChangeTime),
                PrinterStateChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, PrinterAttribute.PrinterStateChangeDateTime),
                PrinterDetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterDetailedStatusMessages),
                PrinterStateReasons = map.MapFromDicSetNullable<PrinterStateReason[]?>(src, PrinterAttribute.PrinterStateReasons),
                PrinterUpTime = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterUpTime),
                PrinterUriSupported = map.MapFromDicSetNullable<Uri[]?>(src, PrinterAttribute.PrinterUriSupported),
                PrintScalingDefault = map.MapFromDicNullable<PrintScaling?>(src, PrinterAttribute.PrintScalingDefault),
                PrintScalingSupported = map.MapFromDicSetNullable<PrintScaling[]?>(src, PrinterAttribute.PrintScalingSupported),
                QueuedJobCount = map.MapFromDicNullable<int?>(src, PrinterAttribute.QueuedJobCount),
                ReferenceUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, PrinterAttribute.ReferenceUriSchemesSupported),
                UriAuthenticationSupported = map.MapFromDicSetNullable<UriAuthentication[]?>(src, PrinterAttribute.UriAuthenticationSupported),
                UriSecuritySupported = map.MapFromDicSetNullable<UriSecurity[]?>(src, PrinterAttribute.UriSecuritySupported),
                MediaDefault = map.MapFromDicNullable<string, Media?>(src, PrinterAttribute.MediaDefault, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                MediaSupported = map.MapFromDicSetNullable<string, Media>(src, PrinterAttribute.MediaSupported, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                MediaReady = map.MapFromDicSetNullable<string, Media>(src, PrinterAttribute.MediaReady, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                SidesDefault = map.MapFromDicNullable<Sides?>(src, PrinterAttribute.SidesDefault),
                SidesSupported = map.MapFromDicSetNullable<Sides[]?>(src, PrinterAttribute.SidesSupported),
                FinishingsDefault = map.MapFromDicNullable<Finishings?>(src, PrinterAttribute.FinishingsDefault),
                FinishingsSupported = map.MapFromDicSetNullable<Finishings[]?>(src, PrinterAttribute.FinishingsSupported),
                PrinterResolutionDefault = map.MapFromDicNullable<Resolution?>(src, PrinterAttribute.PrinterResolutionDefault),
                PrinterResolutionSupported = map.MapFromDicSetNullable<Resolution[]?>(src, PrinterAttribute.PrinterResolutionSupported),
                PrintQualityDefault = map.MapFromDicNullable<PrintQuality?>(src, PrinterAttribute.PrintQualityDefault),
                PrintQualitySupported = map.MapFromDicSetNullable<PrintQuality[]?>(src, PrinterAttribute.PrintQualitySupported),
                JobPriorityDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobPriorityDefault),
                JobPrioritySupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobPrioritySupported),
                CopiesDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.CopiesDefault),
                CopiesSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.CopiesSupported),
                OrientationRequestedDefault = map.MapFromDicNullable<Orientation?>(src, PrinterAttribute.OrientationRequestedDefault),
                OrientationRequestedSupported = map.MapFromDicSetNullable<Orientation[]?>(src, PrinterAttribute.OrientationRequestedSupported),
                PageRangesSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PageRangesSupported),
                JobHoldUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, PrinterAttribute.JobHoldUntilDefault),
                JobHoldUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobHoldUntilSupported),
                JobHoldUntilTimeSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobHoldUntilTimeSupported),
                JobDelayOutputUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, PrinterAttribute.JobDelayOutputUntilDefault),
                JobDelayOutputUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobDelayOutputUntilSupported),
                JobDelayOutputUntilTimeSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobDelayOutputUntilTimeSupported),
                JobHistoryAttributesConfigured = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobHistoryAttributesConfigured),
                JobHistoryAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobHistoryAttributesSupported),
                JobHistoryIntervalConfigured = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobHistoryIntervalConfigured),
                JobHistoryIntervalSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobHistoryIntervalSupported),
                JobRetainUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, PrinterAttribute.JobRetainUntilDefault),
                JobRetainUntilIntervalDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobRetainUntilIntervalDefault),
                JobRetainUntilIntervalSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobRetainUntilIntervalSupported),
                JobRetainUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobRetainUntilSupported),
                JobRetainUntilTimeSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobRetainUntilTimeSupported),
                OutputBinDefault = map.MapFromDicNullable<string, OutputBin?>(src, PrinterAttribute.OutputBinDefault, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
                OutputBinSupported = map.MapFromDicSetNullable<string, OutputBin>(src, PrinterAttribute.OutputBinSupported, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
                MediaColDefault = src.ContainsKey(PrinterAttribute.MediaColDefault) ? map.Map<MediaCol>(src[PrinterAttribute.MediaColDefault].FromBegCollection().ToIppDictionary()) : null,
                MediaColDatabase = src.ContainsKey(PrinterAttribute.MediaColDatabase)
                    ? src[PrinterAttribute.MediaColDatabase].GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                MediaColReady = src.ContainsKey(PrinterAttribute.MediaColReady)
                    ? src[PrinterAttribute.MediaColReady].GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                MediaColSupported = map.MapFromDicSetNullable<MediaColMember[]?>(src, PrinterAttribute.MediaColSupported),
                MediaSizeSupported = src.ContainsKey(PrinterAttribute.MediaSizeSupported)
                    ? src[PrinterAttribute.MediaSizeSupported].GroupBegCollection().Select(x => map.Map<MediaSizeSupported>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                MediaKeySupported = map.MapFromDicSetNullable<string, MediaKey>(src, PrinterAttribute.MediaKeySupported, (attribute, value) => new MediaKey(value, attribute.Tag == Tag.Keyword)),
                MediaSourceSupported = map.MapFromDicSetNullable<MediaSource[]?>(src, PrinterAttribute.MediaSourceSupported),
                MediaTypeSupported = map.MapFromDicSetNullable<MediaType[]?>(src, PrinterAttribute.MediaTypeSupported),
                MediaBackCoatingSupported = map.MapFromDicSetNullable<MediaCoating[]?>(src, PrinterAttribute.MediaBackCoatingSupported),
                MediaFrontCoatingSupported = map.MapFromDicSetNullable<MediaCoating[]?>(src, PrinterAttribute.MediaFrontCoatingSupported),
                MediaColorSupported = map.MapFromDicSetNullable<MediaColor[]?>(src, PrinterAttribute.MediaColorSupported),
                MediaGrainSupported = map.MapFromDicSetNullable<MediaGrain[]?>(src, PrinterAttribute.MediaGrainSupported),
                MediaToothSupported = map.MapFromDicSetNullable<MediaTooth[]?>(src, PrinterAttribute.MediaToothSupported),
                MediaPrePrintedSupported = map.MapFromDicSetNullable<MediaPrePrinted[]?>(src, PrinterAttribute.MediaPrePrintedSupported),
                MediaRecycledSupported = map.MapFromDicSetNullable<MediaRecycled[]?>(src, PrinterAttribute.MediaRecycledSupported),
                MediaHoleCountSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MediaHoleCountSupported),
                MediaOrderCountSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MediaOrderCountSupported),
                MediaThicknessSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MediaThicknessSupported),
                MediaWeightMetricSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MediaWeightMetricSupported),
                MediaBottomMarginSupported = map.MapFromDicSetNullable<int[]?>(src, PrinterAttribute.MediaBottomMarginSupported),
                MediaLeftMarginSupported = map.MapFromDicSetNullable<int[]?>(src, PrinterAttribute.MediaLeftMarginSupported),
                MediaRightMarginSupported = map.MapFromDicSetNullable<int[]?>(src, PrinterAttribute.MediaRightMarginSupported),
                MediaTopMarginSupported = map.MapFromDicSetNullable<int[]?>(src, PrinterAttribute.MediaTopMarginSupported),
                PrintColorModeDefault = map.MapFromDicNullable<PrintColorMode?>(src, PrinterAttribute.PrintColorModeDefault),
                PrintColorModeSupported = map.MapFromDicSetNullable<PrintColorMode[]?>(src, PrinterAttribute.PrintColorModeSupported),
                WhichJobsSupported = map.MapFromDicSetNullable<WhichJobs[]?>(src, PrinterAttribute.WhichJobsSupported),
                PrinterUUID = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterUUID),
                PdfVersionsSupported = map.MapFromDicSetNullable<PdfVersion[]?>(src, PrinterAttribute.PdfVersionsSupported),
                IppFeaturesSupported = map.MapFromDicSetNullable<IppFeature[]?>(src, PrinterAttribute.IppFeaturesSupported),
                DocumentCreationAttributesSupported = map.MapFromDicSetNullable<DocumentCreationAttribute[]?>(src, PrinterAttribute.DocumentCreationAttributesSupported),
                JobAccountIdDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.JobAccountIdDefault),
                JobAccountTypeDefault = map.MapFromDicNullable<JobAccountType?>(src, PrinterAttribute.JobAccountTypeDefault),
                JobAccountTypeSupported = map.MapFromDicSetNullable<JobAccountType[]?>(src, PrinterAttribute.JobAccountTypeSupported),
                JobAccountIdSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobAccountIdSupported),
                JobAccountingUserIdDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.JobAccountingUserIdDefault),
                JobAccountingUserIdSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobAccountingUserIdSupported),
                JobPasswordEncryptionSupported = map.MapFromDicSetNullable<JobPasswordEncryption[]?>(src, PrinterAttribute.JobPasswordEncryptionSupported),
                JobAuthorizationUriSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobAuthorizationUriSupported),
                PrinterChargeInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterChargeInfo),
                PrinterChargeInfoUri = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterChargeInfoUri),
                PrinterMandatoryJobAttributes = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterMandatoryJobAttributes),
                PrinterRequestedJobAttributes = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterRequestedJobAttributes),
                PrinterAlert = map.MapFromDicSetNullable<PrinterAlert[]?>(src, PrinterAttribute.PrinterAlert),
                PrinterAlertDescription = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterAlertDescription),
                PrinterSupply = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterSupply),
                PrinterSupplyDescription = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterSupplyDescription),
                JobCancelAfterDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobCancelAfterDefault),
                JobCancelAfterSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobCancelAfterSupported),
                JobSpoolingSupported = map.MapFromDicNullable<JobSpooling?>(src, PrinterAttribute.JobSpoolingSupported),
                MaxPageRangesSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.MaxPageRangesSupported),
                PrintContentOptimizeDefault = map.MapFromDicNullable<PrintContentOptimize?>(src, PrinterAttribute.PrintContentOptimizeDefault),
                PrintContentOptimizeSupported = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, PrinterAttribute.PrintContentOptimizeSupported),
                OutputDeviceSupported = map.MapFromDicSetNullable<OutputDevice[]?>(src, PrinterAttribute.OutputDeviceSupported),
                OutputDeviceUuidSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.OutputDeviceUuidSupported),
                DocumentAccessSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentAccessSupported),
                FetchDocumentAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.FetchDocumentAttributesSupported),
                PrinterModeConfigured = map.MapFromDicNullable<PrinterMode?>(src, PrinterAttribute.PrinterModeConfigured),
                PrinterModeSupported = map.MapFromDicSetNullable<PrinterMode[]?>(src, PrinterAttribute.PrinterModeSupported),
                PrinterStaticResourceDirectoryUri = map.MapFromDicNullable<Uri?>(src, PrinterAttribute.PrinterStaticResourceDirectoryUri),
                PrinterStaticResourceKOctetsSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterStaticResourceKOctetsSupported),
                PrinterStaticResourceKOctetsFree = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterStaticResourceKOctetsFree),
                AccuracyUnitsSupported = map.MapFromDicSetNullable<AccuracyUnits[]?>(src, PrinterAttribute.AccuracyUnitsSupported),
                ChamberHumidityDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.ChamberHumidityDefault),
                ChamberHumiditySupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.ChamberHumiditySupported),
                ChamberTemperatureDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.ChamberTemperatureDefault),
                ChamberTemperatureSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.ChamberTemperatureSupported),
                MaterialAmountUnitsSupported = map.MapFromDicSetNullable<MaterialAmountUnits[]?>(src, PrinterAttribute.MaterialAmountUnitsSupported),
                MaterialDiameterSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MaterialDiameterSupported),
                MaterialNozzleDiameterSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MaterialNozzleDiameterSupported),
                MaterialPurposeSupported = map.MapFromDicSetNullable<MaterialPurpose[]?>(src, PrinterAttribute.MaterialPurposeSupported),
                MaterialRateSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MaterialRateSupported),
                MaterialRateUnitsSupported = map.MapFromDicSetNullable<MaterialRateUnits[]?>(src, PrinterAttribute.MaterialRateUnitsSupported),
                MaterialShellThicknessSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MaterialShellThicknessSupported),
                MaterialTemperatureSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.MaterialTemperatureSupported),
                MaterialTypeSupported = map.MapFromDicSetNullable<MaterialType[]?>(src, PrinterAttribute.MaterialTypeSupported),
                MaterialsColDatabase = src.ContainsKey(PrinterAttribute.MaterialsColDatabase)
                    ? src[PrinterAttribute.MaterialsColDatabase].GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                MaterialsColDefault = src.ContainsKey(PrinterAttribute.MaterialsColDefault)
                    ? src[PrinterAttribute.MaterialsColDefault].GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                MaterialsColReady = src.ContainsKey(PrinterAttribute.MaterialsColReady)
                    ? src[PrinterAttribute.MaterialsColReady].GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                MaterialsColSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.MaterialsColSupported),
                MaxMaterialsColSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.MaxMaterialsColSupported),
                MultipleObjectHandlingDefault = map.MapFromDicNullable<MultipleObjectHandling?>(src, PrinterAttribute.MultipleObjectHandlingDefault),
                MultipleObjectHandlingSupported = map.MapFromDicSetNullable<MultipleObjectHandling[]?>(src, PrinterAttribute.MultipleObjectHandlingSupported),
                PdfFeaturesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PdfFeaturesSupported),
                PlatformShape = map.MapFromDicNullable<PlatformShape?>(src, PrinterAttribute.PlatformShape),
                PlatformTemperatureDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.PlatformTemperatureDefault),
                PlatformTemperatureSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.PlatformTemperatureSupported),
                PrintAccuracyDefault = src.ContainsKey(PrinterAttribute.PrintAccuracyDefault)
                    ? map.Map<PrintAccuracy>(src[PrinterAttribute.PrintAccuracyDefault].FromBegCollection().ToIppDictionary())
                    : null,
                PrintAccuracySupported = src.ContainsKey(PrinterAttribute.PrintAccuracySupported)
                    ? map.Map<PrintAccuracy>(src[PrinterAttribute.PrintAccuracySupported].FromBegCollection().ToIppDictionary())
                    : null,
                PrintBaseDefault = map.MapFromDicNullable<PrintBase?>(src, PrinterAttribute.PrintBaseDefault),
                PrintBaseSupported = map.MapFromDicSetNullable<PrintBase[]?>(src, PrinterAttribute.PrintBaseSupported),
                PrintObjectsSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrintObjectsSupported),
                PrintSupportsDefault = map.MapFromDicNullable<PrintSupports?>(src, PrinterAttribute.PrintSupportsDefault),
                PrintSupportsSupported = map.MapFromDicSetNullable<PrintSupports[]?>(src, PrinterAttribute.PrintSupportsSupported),
                PrinterVolumeSupported = src.ContainsKey(PrinterAttribute.PrinterVolumeSupported)
                    ? map.Map<PrinterVolumeSupported>(src[PrinterAttribute.PrinterVolumeSupported].FromBegCollection().ToIppDictionary())
                    : null,
                ChamberHumidityCurrent = map.MapFromDicNullable<int?>(src, PrinterAttribute.ChamberHumidityCurrent),
                ChamberTemperatureCurrent = map.MapFromDicNullable<int?>(src, PrinterAttribute.ChamberTemperatureCurrent),
                PrinterCameraImageUri = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterCameraImageUri),
                PrinterResourceIds = map.MapFromDicSetNullable<int[]?>(src, PrinterAttribute.PrinterResourceIds),
                JobCreationAttributesSupported = map.MapFromDicSetNullable<JobCreationAttribute[]?>(src, PrinterAttribute.JobCreationAttributesSupported),
                PrinterRequestedClientType = map.MapFromDicSetNullable<ClientType[]?>(src, PrinterAttribute.PrinterRequestedClientType),
                PrinterServiceType = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, PrinterAttribute.PrinterServiceType),
                FinishingTemplateSupported = map.MapFromDicSetNullable<string, FinishingTemplate>(src, PrinterAttribute.FinishingTemplateSupported, (attribute, value) => new FinishingTemplate(value, attribute.Tag == Tag.Keyword)),
                FinishingsColSupported = map.MapFromDicSetNullable<FinishingsColMember[]?>(src, PrinterAttribute.FinishingsColSupported),
                JobPagesPerSetSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobPagesPerSetSupported),
                PunchingHoleDiameterConfigured = map.MapFromDicNullable<int?>(src, PrinterAttribute.PunchingHoleDiameterConfigured),
                PrinterFinisher = map.MapFromDicSetNullable<PrinterFinisher[]?>(src, PrinterAttribute.PrinterFinisher),
                PrinterFinisherDescription = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFinisherDescription),
                PrinterFinisherSupplies = map.MapFromDicSetNullable<PrinterFinisherSupply[]?>(src, PrinterAttribute.PrinterFinisherSupplies),
                PrinterFinisherSuppliesDescription = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFinisherSuppliesDescription),
                FinishingsColDefault = src.ContainsKey(PrinterAttribute.FinishingsColDefault) ? src[PrinterAttribute.FinishingsColDefault].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                FinishingsColReady = src.ContainsKey(PrinterAttribute.FinishingsColReady) ? src[PrinterAttribute.FinishingsColReady].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                BalingTypeSupported = map.MapFromDicSetNullable<string, BalingType>(src, PrinterAttribute.BalingTypeSupported, (attribute, value) => new BalingType(value, attribute.Tag == Tag.Keyword)),
                BalingWhenSupported = map.MapFromDicSetNullable<BalingWhen[]?>(src, PrinterAttribute.BalingWhenSupported),
                BindingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.BindingReferenceEdgeSupported),
                BindingTypeSupported = map.MapFromDicSetNullable<string, BindingType>(src, PrinterAttribute.BindingTypeSupported, (attribute, value) => new BindingType(value, attribute.Tag == Tag.Keyword)),
                CoatingSidesSupported = map.MapFromDicSetNullable<CoatingSides[]?>(src, PrinterAttribute.CoatingSidesSupported),
                CoatingTypeSupported = map.MapFromDicSetNullable<string, CoatingType>(src, PrinterAttribute.CoatingTypeSupported, (attribute, value) => new CoatingType(value, attribute.Tag == Tag.Keyword)),
                CoveringNameSupported = map.MapFromDicSetNullable<string, CoveringName>(src, PrinterAttribute.CoveringNameSupported, (attribute, value) => new CoveringName(value, attribute.Tag == Tag.Keyword)),
                FinishingsColDatabase = src.ContainsKey(PrinterAttribute.FinishingsColDatabase) ? src[PrinterAttribute.FinishingsColDatabase].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                FoldingDirectionSupported = map.MapFromDicSetNullable<FoldingDirection[]?>(src, PrinterAttribute.FoldingDirectionSupported),
                FoldingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.FoldingOffsetSupported),
                FoldingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.FoldingReferenceEdgeSupported),
                LaminatingSidesSupported = map.MapFromDicSetNullable<CoatingSides[]?>(src, PrinterAttribute.LaminatingSidesSupported),
                LaminatingTypeSupported = map.MapFromDicSetNullable<string, LaminatingType>(src, PrinterAttribute.LaminatingTypeSupported, (attribute, value) => new LaminatingType(value, attribute.Tag == Tag.Keyword)),
                PunchingLocationsSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.PunchingLocationsSupported),
                PunchingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.PunchingOffsetSupported),
                PunchingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.PunchingReferenceEdgeSupported),
                StitchingAngleSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.StitchingAngleSupported),
                StitchingLocationsSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.StitchingLocationsSupported),
                StitchingMethodSupported = map.MapFromDicSetNullable<StitchingMethod[]?>(src, PrinterAttribute.StitchingMethodSupported),
                StitchingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.StitchingOffsetSupported),
                StitchingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.StitchingReferenceEdgeSupported),
                TrimmingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.TrimmingOffsetSupported),
                TrimmingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.TrimmingReferenceEdgeSupported),
                TrimmingTypeSupported = map.MapFromDicSetNullable<string, TrimmingType>(src, PrinterAttribute.TrimmingTypeSupported, (attribute, value) => new TrimmingType(value, attribute.Tag == Tag.Keyword)),
                TrimmingWhenSupported = map.MapFromDicSetNullable<TrimmingWhen[]?>(src, PrinterAttribute.TrimmingWhenSupported),
                CoverBackDefault = src.ContainsKey(PrinterAttribute.CoverBackDefault) ? map.Map<Cover>(src[PrinterAttribute.CoverBackDefault].FromBegCollection().ToIppDictionary()) : null,
                CoverBackSupported = map.MapFromDicSetNullable<CoverMember[]?>(src, PrinterAttribute.CoverBackSupported),
                CoverFrontDefault = src.ContainsKey(PrinterAttribute.CoverFrontDefault) ? map.Map<Cover>(src[PrinterAttribute.CoverFrontDefault].FromBegCollection().ToIppDictionary()) : null,
                CoverFrontSupported = map.MapFromDicSetNullable<CoverMember[]?>(src, PrinterAttribute.CoverFrontSupported),
                CoverTypeSupported = map.MapFromDicSetNullable<CoverType[]?>(src, PrinterAttribute.CoverTypeSupported),
                ForceFrontSideSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.ForceFrontSideSupported),
                ImageOrientationDefault = map.MapFromDicNullable<Orientation?>(src, PrinterAttribute.ImageOrientationDefault),
                ImageOrientationSupported = map.MapFromDicSetNullable<Orientation[]?>(src, PrinterAttribute.ImageOrientationSupported),
                ImpositionTemplateDefault = map.MapFromDicNullable<string, ImpositionTemplate?>(src, PrinterAttribute.ImpositionTemplateDefault, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
                ImpositionTemplateSupported = map.MapFromDicSetNullable<string, ImpositionTemplate>(src, PrinterAttribute.ImpositionTemplateSupported, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
                InsertCountSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.InsertCountSupported),
                InsertSheetDefault = src.TryGetValue(PrinterAttribute.InsertSheetDefault, out var insertsheetdefault) && insertsheetdefault.GroupBegCollection().Any() ? insertsheetdefault.GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                InsertSheetSupported = map.MapFromDicSetNullable<InsertSheetMember[]?>(src, PrinterAttribute.InsertSheetSupported),
                JobAccountingOutputBinSupported = map.MapFromDicSetNullable<string, OutputBin>(src, PrinterAttribute.JobAccountingOutputBinSupported, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
                JobAccountingSheetsDefault = src.ContainsKey(PrinterAttribute.JobAccountingSheetsDefault) ? map.Map<JobAccountingSheets>(src[PrinterAttribute.JobAccountingSheetsDefault].FromBegCollection().ToIppDictionary()) : null,
                JobAccountingSheetsSupported = map.MapFromDicSetNullable<JobAccountingSheetsMember[]?>(src, PrinterAttribute.JobAccountingSheetsSupported),
                JobAccountingSheetsTypeSupported = map.MapFromDicSetNullable<JobSheetsType[]?>(src, PrinterAttribute.JobAccountingSheetsTypeSupported),
                JobCompleteBeforeSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobCompleteBeforeSupported),
                JobCompleteBeforeTimeSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobCompleteBeforeTimeSupported),
                JobErrorSheetDefault = src.ContainsKey(PrinterAttribute.JobErrorSheetDefault) ? map.Map<JobErrorSheet>(src[PrinterAttribute.JobErrorSheetDefault].FromBegCollection().ToIppDictionary()) : null,
                JobErrorSheetSupported = map.MapFromDicSetNullable<JobErrorSheetMember[]?>(src, PrinterAttribute.JobErrorSheetSupported),
                JobErrorSheetTypeSupported = map.MapFromDicSetNullable<JobSheetsType[]?>(src, PrinterAttribute.JobErrorSheetTypeSupported),
                JobErrorSheetWhenSupported = map.MapFromDicSetNullable<JobErrorSheetWhen[]?>(src, PrinterAttribute.JobErrorSheetWhenSupported),
                JobMessageToOperatorSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobMessageToOperatorSupported),
                JobPhoneNumberDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.JobPhoneNumberDefault),
                JobPhoneNumberSchemeSupported = map.MapFromDicSetNullable<JobPhoneNumberScheme[]?>(src, PrinterAttribute.JobPhoneNumberSchemeSupported),
                JobPhoneNumberSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobPhoneNumberSupported),
                JobRecipientNameSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobRecipientNameSupported),
                JobSheetMessageSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobSheetMessageSupported),
                PageDeliveryDefault = map.MapFromDicNullable<PageDelivery?>(src, PrinterAttribute.PageDeliveryDefault),
                PageDeliverySupported = map.MapFromDicSetNullable<PageDelivery[]?>(src, PrinterAttribute.PageDeliverySupported),
                PresentationDirectionNumberUpDefault = map.MapFromDicNullable<PresentationDirectionNumberUp?>(src, PrinterAttribute.PresentationDirectionNumberUpDefault),
                PresentationDirectionNumberUpSupported = map.MapFromDicSetNullable<PresentationDirectionNumberUp[]?>(src, PrinterAttribute.PresentationDirectionNumberUpSupported),
                SeparatorSheetsDefault = src.ContainsKey(PrinterAttribute.SeparatorSheetsDefault) ? map.Map<SeparatorSheets>(src[PrinterAttribute.SeparatorSheetsDefault].FromBegCollection().ToIppDictionary()) : null,
                SeparatorSheetsSupported = map.MapFromDicSetNullable<SeparatorSheetsMember[]?>(src, PrinterAttribute.SeparatorSheetsSupported),
                SeparatorSheetsTypeSupported = map.MapFromDicSetNullable<SeparatorSheetsType[]?>(src, PrinterAttribute.SeparatorSheetsTypeSupported),
                XImagePositionDefault = map.MapFromDicNullable<XImagePosition?>(src, PrinterAttribute.XImagePositionDefault),
                XImagePositionSupported = map.MapFromDicSetNullable<XImagePosition[]?>(src, PrinterAttribute.XImagePositionSupported),
                XImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.XImageShiftDefault),
                XImageShiftSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.XImageShiftSupported),
                XSide1ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.XSide1ImageShiftDefault),
                XSide2ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.XSide2ImageShiftDefault),
                YImagePositionDefault = map.MapFromDicNullable<YImagePosition?>(src, PrinterAttribute.YImagePositionDefault),
                YImagePositionSupported = map.MapFromDicSetNullable<YImagePosition[]?>(src, PrinterAttribute.YImagePositionSupported),
                YImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.YImageShiftDefault),
                YImageShiftSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.YImageShiftSupported),
                YSide1ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.YSide1ImageShiftDefault),
                YSide2ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.YSide2ImageShiftDefault),
                ConfirmationSheetPrintDefault = map.MapFromDicNullable<bool?>(src, PrinterAttribute.ConfirmationSheetPrintDefault),
                CoverSheetInfoDefault = src.ContainsKey(PrinterAttribute.CoverSheetInfoDefault) ? map.Map<CoverSheetInfo>(src[PrinterAttribute.CoverSheetInfoDefault].FromBegCollection().ToIppDictionary()) : null,
                CoverSheetInfoSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.CoverSheetInfoSupported),
                DestinationAccessesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DestinationAccessesSupported),
                DestinationUriReady = src.ContainsKey(PrinterAttribute.DestinationUriReady) ? src[PrinterAttribute.DestinationUriReady].GroupBegCollection().Select(x => map.Map<DestinationUriReady>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                DestinationUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, PrinterAttribute.DestinationUriSchemesSupported),
                DestinationUrisSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DestinationUrisSupported),
                FromNameSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.FromNameSupported),
                InputAttributesDefault = src.ContainsKey(PrinterAttribute.InputAttributesDefault) ? map.Map<DocumentTemplateAttributes>(src[PrinterAttribute.InputAttributesDefault].FromBegCollection().ToIppDictionary()) : null,
                InputAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.InputAttributesSupported),
                InputColorModeSupported = map.MapFromDicSetNullable<InputColorMode[]?>(src, PrinterAttribute.InputColorModeSupported),
                InputContentTypeSupported = map.MapFromDicSetNullable<InputContentType[]?>(src, PrinterAttribute.InputContentTypeSupported),
                InputFilmScanModeSupported = map.MapFromDicSetNullable<InputFilmScanMode[]?>(src, PrinterAttribute.InputFilmScanModeSupported),
                InputMediaSupported = map.MapFromDicSetNullable<string, Media>(src, PrinterAttribute.InputMediaSupported, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                InputOrientationRequestedSupported = map.MapFromDicSetNullable<Orientation[]?>(src, PrinterAttribute.InputOrientationRequestedSupported),
                InputQualitySupported = map.MapFromDicSetNullable<PrintQuality[]?>(src, PrinterAttribute.InputQualitySupported),
                InputResolutionSupported = map.MapFromDicSetNullable<Resolution[]?>(src, PrinterAttribute.InputResolutionSupported),
                InputSidesSupported = map.MapFromDicSetNullable<Sides[]?>(src, PrinterAttribute.InputSidesSupported),
                InputSourceSupported = map.MapFromDicSetNullable<InputSource[]?>(src, PrinterAttribute.InputSourceSupported),
                LogoUriFormatsSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.LogoUriFormatsSupported),
                LogoUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, PrinterAttribute.LogoUriSchemesSupported),
                MessageSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.MessageSupported),
                MultipleDestinationUrisSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.MultipleDestinationUrisSupported),
                NumberOfRetriesDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.NumberOfRetriesDefault),
                NumberOfRetriesSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.NumberOfRetriesSupported),
                OrganizationNameSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.OrganizationNameSupported),
                JobDestinationSpoolingSupported = map.MapFromDicNullable<JobSpooling?>(src, PrinterAttribute.JobDestinationSpoolingSupported),
                OutputAttributesDefault = src.ContainsKey(PrinterAttribute.OutputAttributesDefault) ? map.Map<OutputAttributes>(src[PrinterAttribute.OutputAttributesDefault].FromBegCollection().ToIppDictionary()) : null,
                OutputAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.OutputAttributesSupported),
                PrinterFaxLogUri = map.MapFromDicNullable<Uri?>(src, PrinterAttribute.PrinterFaxLogUri),
                PrinterFaxModemInfo = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFaxModemInfo),
                PrinterFaxModemName = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFaxModemName),
                PrinterFaxModemNumber = map.MapFromDicSetNullable<Uri[]?>(src, PrinterAttribute.PrinterFaxModemNumber),
                RetryIntervalDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.RetryIntervalDefault),
                RetryIntervalSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.RetryIntervalSupported),
                RetryTimeOutDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.RetryTimeOutDefault),
                RetryTimeOutSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.RetryTimeOutSupported),
                SubjectSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.SubjectSupported),
                ToNameSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.ToNameSupported),
            };
        });

        mapper.CreateMap<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();

            if (src.CharsetConfigured != null)
                dic.Add(PrinterAttribute.CharsetConfigured, new IppAttribute[] { new IppAttribute(Tag.Charset, PrinterAttribute.CharsetConfigured, src.CharsetConfigured) });
            if (src.CharsetSupported != null)
                dic.Add(PrinterAttribute.CharsetSupported, src.CharsetSupported.Select(x => new IppAttribute(Tag.Charset, PrinterAttribute.CharsetSupported, x)).ToArray());
            if (src.ColorSupported != null)
                dic.Add(PrinterAttribute.ColorSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.ColorSupported, src.ColorSupported.Value) });
            if (src.CompressionDefault != null)
                dic.Add(PrinterAttribute.CompressionDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.CompressionDefault, map.Map<string>(src.CompressionDefault.Value)) });
            if (src.CompressionSupported != null)
                dic.Add(PrinterAttribute.CompressionSupported, src.CompressionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CompressionSupported, map.Map<string>(x))).ToArray());
            if (src.DocumentFormatDefault != null)
                dic.Add(PrinterAttribute.DocumentFormatDefault, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, PrinterAttribute.DocumentFormatDefault, src.DocumentFormatDefault) });
            if (src.DocumentFormatSupported != null)
                dic.Add(PrinterAttribute.DocumentFormatSupported, src.DocumentFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, PrinterAttribute.DocumentFormatSupported, x)).ToArray());
            if (src.ClientInfoSupported != null)
                dic.Add(PrinterAttribute.ClientInfoSupported, src.ClientInfoSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.ClientInfoSupported, x)).ToArray());
            if (src.MaxClientInfoSupported != null)
                dic.Add(PrinterAttribute.MaxClientInfoSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.MaxClientInfoSupported, src.MaxClientInfoSupported.Value)]);
            if (src.DocumentCharsetDefault != null)
                dic.Add(PrinterAttribute.DocumentCharsetDefault, [new IppAttribute(Tag.Charset, PrinterAttribute.DocumentCharsetDefault, src.DocumentCharsetDefault)]);
            if (src.DocumentCharsetSupported != null)
                dic.Add(PrinterAttribute.DocumentCharsetSupported, src.DocumentCharsetSupported.Select(x => new IppAttribute(Tag.Charset, PrinterAttribute.DocumentCharsetSupported, x)).ToArray());
            if (src.DocumentFormatDetailsSupported != null)
                dic.Add(PrinterAttribute.DocumentFormatDetailsSupported, src.DocumentFormatDetailsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.DocumentFormatDetailsSupported, x)).ToArray());
            if (src.DocumentNaturalLanguageDefault != null)
                dic.Add(PrinterAttribute.DocumentNaturalLanguageDefault, [new IppAttribute(Tag.NaturalLanguage, PrinterAttribute.DocumentNaturalLanguageDefault, src.DocumentNaturalLanguageDefault)]);
            if (src.DocumentNaturalLanguageSupported != null)
                dic.Add(PrinterAttribute.DocumentNaturalLanguageSupported, src.DocumentNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, PrinterAttribute.DocumentNaturalLanguageSupported, x)).ToArray());
            if (src.JobIdsSupported != null)
                dic.Add(PrinterAttribute.JobIdsSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobIdsSupported, src.JobIdsSupported.Value)]);
            if (src.JobMandatoryAttributesSupported != null)
                dic.Add(PrinterAttribute.JobMandatoryAttributesSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobMandatoryAttributesSupported, src.JobMandatoryAttributesSupported.Value)]);
            if (src.JobSheetsColDefault != null)
                dic.Add(PrinterAttribute.JobSheetsColDefault, map.Map<IEnumerable<IppAttribute>>(src.JobSheetsColDefault).ToBegCollection(PrinterAttribute.JobSheetsColDefault).ToArray());
            if (src.JobSheetsColSupported != null)
                dic.Add(PrinterAttribute.JobSheetsColSupported, src.JobSheetsColSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobSheetsColSupported, map.Map<string>(x))).ToArray());
            if (src.GeneratedNaturalLanguageSupported != null)
                dic.Add(PrinterAttribute.GeneratedNaturalLanguageSupported, src.GeneratedNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, PrinterAttribute.GeneratedNaturalLanguageSupported, x)).ToArray());
            if (src.IppVersionsSupported != null)
                dic.Add(PrinterAttribute.IppVersionsSupported, src.IppVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.IppVersionsSupported, x.ToString())).ToArray());
            if (src.JobImpressionsSupported != null)
                dic.Add(PrinterAttribute.JobImpressionsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobImpressionsSupported, src.JobImpressionsSupported.Value) });
            if (src.JobKOctetsSupported != null)
                dic.Add(PrinterAttribute.JobKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobKOctetsSupported, src.JobKOctetsSupported.Value) });
            if (src.JpegKOctetsSupported != null)
                dic.Add(PrinterAttribute.JpegKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JpegKOctetsSupported, src.JpegKOctetsSupported.Value) });
            if (src.PdfKOctetsSupported != null)
                dic.Add(PrinterAttribute.PdfKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PdfKOctetsSupported, src.PdfKOctetsSupported.Value) });
            if (src.JobMediaSheetsSupported != null)
                dic.Add(PrinterAttribute.JobMediaSheetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobMediaSheetsSupported, src.JobMediaSheetsSupported.Value) });
            if (src.JobSheetsDefault != null)
                dic.Add(PrinterAttribute.JobSheetsDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobSheetsDefault, map.Map<string>(src.JobSheetsDefault.Value))]);
            if (src.JobSheetsSupported != null)
                dic.Add(PrinterAttribute.JobSheetsSupported, src.JobSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobSheetsSupported, map.Map<string>(x))).ToArray());
            if (src.NumberUpDefault != null)
                dic.Add(PrinterAttribute.NumberUpDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.NumberUpDefault, src.NumberUpDefault.Value)]);
            if (src.NumberUpSupported != null)
                dic.Add(PrinterAttribute.NumberUpSupported, src.NumberUpSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.NumberUpSupported, x)).ToArray());
            if (src.MultipleDocumentJobsSupported != null)
                dic.Add(PrinterAttribute.MultipleDocumentJobsSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.MultipleDocumentJobsSupported, src.MultipleDocumentJobsSupported.Value) });
            if (src.MultipleDocumentHandlingDefault != null)
                dic.Add(PrinterAttribute.MultipleDocumentHandlingDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.MultipleDocumentHandlingDefault, map.Map<string>(src.MultipleDocumentHandlingDefault.Value))]);
            if (src.MultipleDocumentHandlingSupported != null)
                dic.Add(PrinterAttribute.MultipleDocumentHandlingSupported, src.MultipleDocumentHandlingSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MultipleDocumentHandlingSupported, map.Map<string>(x))).ToArray());
            if (src.MultipleOperationTimeOut != null)
                dic.Add(PrinterAttribute.MultipleOperationTimeOut, new IppAttribute[] { new IppAttribute(Tag.Integer, PrinterAttribute.MultipleOperationTimeOut, src.MultipleOperationTimeOut.Value) });
            if (src.NaturalLanguageConfigured != null)
                dic.Add(PrinterAttribute.NaturalLanguageConfigured, new IppAttribute[] { new IppAttribute(Tag.NaturalLanguage, PrinterAttribute.NaturalLanguageConfigured, src.NaturalLanguageConfigured) });
            if (src.OperationsSupported != null)
                dic.Add(PrinterAttribute.OperationsSupported, src.OperationsSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.OperationsSupported, (int)x)).ToArray());
            if (src.PagesPerMinute != null)
                dic.Add(PrinterAttribute.PagesPerMinute, new IppAttribute[] { new IppAttribute(Tag.Integer, PrinterAttribute.PagesPerMinute, src.PagesPerMinute.Value) });
            if (src.PdlOverrideSupported != null)
                dic.Add(PrinterAttribute.PdlOverrideSupported, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PdlOverrideSupported, src.PdlOverrideSupported.Value) });
            if (src.OverridesSupported != null)
                dic.Add(PrinterAttribute.OverridesSupported, src.OverridesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.OverridesSupported, x.Value)).ToArray());
            if (src.PagesPerMinuteColor != null)
                dic.Add(PrinterAttribute.PagesPerMinuteColor, [new IppAttribute(Tag.Integer, PrinterAttribute.PagesPerMinuteColor, src.PagesPerMinuteColor.Value)]);
            if (src.PrinterCurrentTime != null)
                dic.Add(PrinterAttribute.PrinterCurrentTime, new IppAttribute[] { new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterCurrentTime, src.PrinterCurrentTime.Value) });
            if (src.PrinterConfigChangeTime != null)
                dic.Add(PrinterAttribute.PrinterConfigChangeTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterConfigChangeTime, src.PrinterConfigChangeTime.Value)]);
            if (src.PrinterConfigChangeDateTime != null)
                dic.Add(PrinterAttribute.PrinterConfigChangeDateTime, [new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterConfigChangeDateTime, src.PrinterConfigChangeDateTime.Value)]);
            if (src.PrinterConfigChanges != null)
                dic.Add(PrinterAttribute.PrinterConfigChanges, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterConfigChanges, src.PrinterConfigChanges.Value)]);
            if (src.PrinterContactCol != null)
                dic.Add(PrinterAttribute.PrinterContactCol, src.PrinterContactCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.PrinterContactCol)).ToArray());
            if (src.PrinterGeoLocation != null)
                dic.Add(PrinterAttribute.PrinterGeoLocation, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterGeoLocation, src.PrinterGeoLocation.ToString()) });
            if (src.PrinterIds != null)
                dic.Add(PrinterAttribute.PrinterIds, src.PrinterIds.Select(x => new IppAttribute(Tag.Integer, PrinterAttribute.PrinterIds, x)).ToArray());
            if (src.PrinterImpressionsCompleted != null)
                dic.Add(PrinterAttribute.PrinterImpressionsCompleted, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterImpressionsCompleted, src.PrinterImpressionsCompleted.Value)]);
            if (src.PrinterImpressionsCompletedCol != null)
                dic.Add(PrinterAttribute.PrinterImpressionsCompletedCol, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterImpressionsCompletedCol, src.PrinterImpressionsCompletedCol.Value)]);
            if (src.PrinterMediaSheetsCompleted != null)
                dic.Add(PrinterAttribute.PrinterMediaSheetsCompleted, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterMediaSheetsCompleted, src.PrinterMediaSheetsCompleted.Value)]);
            if (src.PrinterMediaSheetsCompletedCol != null)
                dic.Add(PrinterAttribute.PrinterMediaSheetsCompletedCol, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterMediaSheetsCompletedCol, src.PrinterMediaSheetsCompletedCol.Value)]);
            if (src.PrinterPagesCompleted != null)
                dic.Add(PrinterAttribute.PrinterPagesCompleted, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterPagesCompleted, src.PrinterPagesCompleted.Value)]);
            if (src.PrinterPagesCompletedCol != null)
                dic.Add(PrinterAttribute.PrinterPagesCompletedCol, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterPagesCompletedCol, src.PrinterPagesCompletedCol.Value)]);
            if (src.PrinterDriverInstaller != null)
                dic.Add(PrinterAttribute.PrinterDriverInstaller, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterDriverInstaller, src.PrinterDriverInstaller) });
            if (src.PrinterInfo != null)
                dic.Add(PrinterAttribute.PrinterInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterInfo, src.PrinterInfo) });
            if (src.PrinterIsAcceptingJobs != null)
                dic.Add(PrinterAttribute.PrinterIsAcceptingJobs, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.PrinterIsAcceptingJobs, src.PrinterIsAcceptingJobs.Value) });
            if (src.PrinterLocation != null)
                dic.Add(PrinterAttribute.PrinterLocation, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterLocation, src.PrinterLocation) });
            if (src.PrinterMakeAndModel != null)
                dic.Add(PrinterAttribute.PrinterMakeAndModel, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterMakeAndModel, src.PrinterMakeAndModel) });
            if (src.PrinterMessageFromOperator != null)
                dic.Add(PrinterAttribute.PrinterMessageFromOperator, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterMessageFromOperator, src.PrinterMessageFromOperator) });
            if (src.PrinterMoreInfo != null)
                dic.Add(PrinterAttribute.PrinterMoreInfo, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterMoreInfo, src.PrinterMoreInfo) });
            if (src.PrinterMoreInfoManufacturer != null)
                dic.Add(PrinterAttribute.PrinterMoreInfoManufacturer, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterMoreInfoManufacturer, src.PrinterMoreInfoManufacturer) });
            if (src.PrinterName != null)
                dic.Add(PrinterAttribute.PrinterName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.PrinterName, src.PrinterName) });
            if (src.PrinterState != null)
                dic.Add(PrinterAttribute.PrinterState, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.PrinterState, (int)src.PrinterState.Value) });
            if (src.PrinterStateMessage != null)
                dic.Add(PrinterAttribute.PrinterStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterStateMessage, src.PrinterStateMessage) });
            if (src.PrinterDetailedStatusMessages != null)
                dic.Add(PrinterAttribute.PrinterDetailedStatusMessages, src.PrinterDetailedStatusMessages.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterDetailedStatusMessages, x)).ToArray());
            if (src.PrinterStateChangeTime != null)
                dic.Add(PrinterAttribute.PrinterStateChangeTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterStateChangeTime, src.PrinterStateChangeTime.Value)]);
            if (src.PrinterStateChangeDateTime != null)
                dic.Add(PrinterAttribute.PrinterStateChangeDateTime, [new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterStateChangeDateTime, src.PrinterStateChangeDateTime.Value)]);
            if (src.PrinterStateReasons != null)
                dic.Add(PrinterAttribute.PrinterStateReasons, src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterStateReasons, x)).ToArray());
            if (src.PrinterUpTime != null)
                dic.Add(PrinterAttribute.PrinterUpTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterUpTime, src.PrinterUpTime.Value)]);
            if (src.PrinterUriSupported != null)
                dic.Add(PrinterAttribute.PrinterUriSupported, src.PrinterUriSupported.Select(x => new IppAttribute(Tag.Uri, PrinterAttribute.PrinterUriSupported, x.ToString())).ToArray());
            if (src.PrintScalingDefault != null)
                dic.Add(PrinterAttribute.PrintScalingDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrintScalingDefault, map.Map<string>(src.PrintScalingDefault)) });
            if (src.PrintScalingSupported != null)
                dic.Add(PrinterAttribute.PrintScalingSupported, src.PrintScalingSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintScalingSupported, map.Map<string>(x))).ToArray());
            if (src.QueuedJobCount != null)
                dic.Add(PrinterAttribute.QueuedJobCount, [new IppAttribute(Tag.Integer, PrinterAttribute.QueuedJobCount, src.QueuedJobCount.Value)]);
            if (src.ReferenceUriSchemesSupported != null)
                dic.Add(PrinterAttribute.ReferenceUriSchemesSupported, src.ReferenceUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, PrinterAttribute.ReferenceUriSchemesSupported, map.Map<string>(x))).ToArray());
            if (src.UriAuthenticationSupported != null)
                dic.Add(PrinterAttribute.UriAuthenticationSupported, src.UriAuthenticationSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.UriAuthenticationSupported, map.Map<string>(x))).ToArray());
            if (src.UriSecuritySupported != null)
                dic.Add(PrinterAttribute.UriSecuritySupported, src.UriSecuritySupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.UriSecuritySupported, map.Map<string>(x))).ToArray());
            if (src.MediaDefault != null)
            {
                var mediaDefaultTag = src.MediaDefault.Value.ToIppTag();
                dic.Add(PrinterAttribute.MediaDefault, [new IppAttribute(mediaDefaultTag, PrinterAttribute.MediaDefault, map.Map<string>(src.MediaDefault.Value))]);
            }
            if (src.MediaSupported != null)
                dic.Add(PrinterAttribute.MediaSupported, src.MediaSupported.Select(x =>
                {
                    var mediaTag = x.ToIppTag();
                    return new IppAttribute(mediaTag, PrinterAttribute.MediaSupported, map.Map<string>(x));
                }).ToArray());
            if (src.MediaReady != null)
                dic.Add(PrinterAttribute.MediaReady, src.MediaReady.Select(x =>
                {
                    var mediaTag = x.ToIppTag();
                    return new IppAttribute(mediaTag, PrinterAttribute.MediaReady, map.Map<string>(x));
                }).ToArray());
            if (src.SidesDefault != null)
                dic.Add(PrinterAttribute.SidesDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.SidesDefault, map.Map<string>(src.SidesDefault)) });
            if (src.SidesSupported != null)
                dic.Add(PrinterAttribute.SidesSupported, src.SidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.SidesSupported, map.Map<string>(x))).ToArray());
            if (src.FinishingsDefault != null)
                dic.Add(PrinterAttribute.FinishingsDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.FinishingsDefault, (int)src.FinishingsDefault.Value) });
            if (src.FinishingsSupported != null)
                dic.Add(PrinterAttribute.FinishingsSupported, src.FinishingsSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.FinishingsSupported, (int)x)).ToArray());
            if (src.PrinterResolutionDefault != null)
                dic.Add(PrinterAttribute.PrinterResolutionDefault, new IppAttribute[] { new IppAttribute(Tag.Resolution, PrinterAttribute.PrinterResolutionDefault, src.PrinterResolutionDefault.Value) });
            if (src.PrinterResolutionSupported != null)
                dic.Add(PrinterAttribute.PrinterResolutionSupported, src.PrinterResolutionSupported.Select(x => new IppAttribute(Tag.Resolution, PrinterAttribute.PrinterResolutionSupported, x)).ToArray());
            if (src.PrintQualityDefault != null)
                dic.Add(PrinterAttribute.PrintQualityDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.PrintQualityDefault, (int)src.PrintQualityDefault.Value) });
            if (src.PrintQualitySupported != null)
                dic.Add(PrinterAttribute.PrintQualitySupported, src.PrintQualitySupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.PrintQualitySupported, (int)x)).ToArray());
            if (src.JobPriorityDefault != null)
                dic.Add(PrinterAttribute.JobPriorityDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.JobPriorityDefault, src.JobPriorityDefault.Value)]);
            if (src.JobPrioritySupported != null)
                dic.Add(PrinterAttribute.JobPrioritySupported, [new IppAttribute(Tag.Integer, PrinterAttribute.JobPrioritySupported, src.JobPrioritySupported.Value)]);
            if (src.CopiesDefault != null)
                dic.Add(PrinterAttribute.CopiesDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.CopiesDefault, src.CopiesDefault.Value)]);
            if (src.CopiesSupported != null)
                dic.Add(PrinterAttribute.CopiesSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.CopiesSupported, src.CopiesSupported.Value) });
            if (src.OrientationRequestedDefault != null)
                dic.Add(PrinterAttribute.OrientationRequestedDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.OrientationRequestedDefault, (int)src.OrientationRequestedDefault.Value) });
            if (src.OrientationRequestedSupported != null)
                dic.Add(PrinterAttribute.OrientationRequestedSupported, src.OrientationRequestedSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.OrientationRequestedSupported, (int)x)).ToArray());
            if (src.PageRangesSupported != null)
                dic.Add(PrinterAttribute.PageRangesSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.PageRangesSupported, src.PageRangesSupported.Value) });
            if (src.JobHoldUntilDefault != null)
                dic.Add(PrinterAttribute.JobHoldUntilDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.JobHoldUntilDefault, map.Map<string>(src.JobHoldUntilDefault.Value)) });
            if (src.JobHoldUntilSupported != null)
                dic.Add(PrinterAttribute.JobHoldUntilSupported, src.JobHoldUntilSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobHoldUntilSupported, map.Map<string>(x))).ToArray());
            if (src.JobHoldUntilTimeSupported != null)
                dic.Add(PrinterAttribute.JobHoldUntilTimeSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobHoldUntilTimeSupported, src.JobHoldUntilTimeSupported.Value)]);
            if (src.JobDelayOutputUntilDefault != null)
                dic.Add(PrinterAttribute.JobDelayOutputUntilDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobDelayOutputUntilDefault, map.Map<string>(src.JobDelayOutputUntilDefault.Value))]);
            if (src.JobDelayOutputUntilSupported != null)
                dic.Add(PrinterAttribute.JobDelayOutputUntilSupported, src.JobDelayOutputUntilSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobDelayOutputUntilSupported, map.Map<string>(x))).ToArray());
            if (src.JobDelayOutputUntilTimeSupported != null)
                dic.Add(PrinterAttribute.JobDelayOutputUntilTimeSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobDelayOutputUntilTimeSupported, src.JobDelayOutputUntilTimeSupported.Value)]);
            if (src.JobHistoryAttributesConfigured != null)
                dic.Add(PrinterAttribute.JobHistoryAttributesConfigured, src.JobHistoryAttributesConfigured.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobHistoryAttributesConfigured, x)).ToArray());
            if (src.JobHistoryAttributesSupported != null)
                dic.Add(PrinterAttribute.JobHistoryAttributesSupported, src.JobHistoryAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobHistoryAttributesSupported, x)).ToArray());
            if (src.JobHistoryIntervalConfigured != null)
                dic.Add(PrinterAttribute.JobHistoryIntervalConfigured, [new IppAttribute(Tag.Integer, PrinterAttribute.JobHistoryIntervalConfigured, src.JobHistoryIntervalConfigured.Value)]);
            if (src.JobHistoryIntervalSupported != null)
                dic.Add(PrinterAttribute.JobHistoryIntervalSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobHistoryIntervalSupported, src.JobHistoryIntervalSupported.Value)]);
            if (src.JobRetainUntilDefault != null)
                dic.Add(PrinterAttribute.JobRetainUntilDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobRetainUntilDefault, map.Map<string>(src.JobRetainUntilDefault.Value))]);
            if (src.JobRetainUntilIntervalDefault != null)
                dic.Add(PrinterAttribute.JobRetainUntilIntervalDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.JobRetainUntilIntervalDefault, src.JobRetainUntilIntervalDefault.Value)]);
            if (src.JobRetainUntilIntervalSupported != null)
                dic.Add(PrinterAttribute.JobRetainUntilIntervalSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobRetainUntilIntervalSupported, src.JobRetainUntilIntervalSupported.Value)]);
            if (src.JobRetainUntilSupported != null)
                dic.Add(PrinterAttribute.JobRetainUntilSupported, src.JobRetainUntilSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobRetainUntilSupported, map.Map<string>(x))).ToArray());
            if (src.JobRetainUntilTimeSupported != null)
                dic.Add(PrinterAttribute.JobRetainUntilTimeSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobRetainUntilTimeSupported, src.JobRetainUntilTimeSupported.Value)]);
            if (src.OutputBinDefault != null)
            {
                var outputBinDefaultValue = src.OutputBinDefault.Value;
                var outputBinDefault = map.Map<string>(outputBinDefaultValue);
                var outputBinDefaultTag = outputBinDefaultValue.ToIppTag();
                dic.Add(PrinterAttribute.OutputBinDefault, [new IppAttribute(outputBinDefaultTag, PrinterAttribute.OutputBinDefault, outputBinDefault)]);
            }
            if (src.OutputBinSupported != null)
                dic.Add(PrinterAttribute.OutputBinSupported, src.OutputBinSupported.Select(x =>
                {
                    var outputBin = map.Map<string>(x);
                    var outputBinTag = x.ToIppTag();
                    return new IppAttribute(outputBinTag, PrinterAttribute.OutputBinSupported, outputBin);
                }).ToArray());
            if (src.MediaColDefault != null)
                dic.Add(PrinterAttribute.MediaColDefault, map.Map<IEnumerable<IppAttribute>>(src.MediaColDefault).ToBegCollection(PrinterAttribute.MediaColDefault).ToArray());
            if (src.MediaColDatabase != null)
                dic.Add(PrinterAttribute.MediaColDatabase, src.MediaColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.MediaColDatabase)).ToArray());
            if (src.MediaColReady != null)
                dic.Add(PrinterAttribute.MediaColReady, src.MediaColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.MediaColReady)).ToArray());
            if (src.MediaColSupported != null)
                dic.Add(PrinterAttribute.MediaColSupported, src.MediaColSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaColSupported, map.Map<string>(x))).ToArray());
            if (src.MediaSizeSupported != null)
                dic.Add(PrinterAttribute.MediaSizeSupported, src.MediaSizeSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.MediaSizeSupported)).ToArray());
            if (src.MediaKeySupported != null)
                dic.Add(PrinterAttribute.MediaKeySupported, src.MediaKeySupported.Select(x =>
                {
                    var mediaKeyTag = x.ToIppTag();
                    return new IppAttribute(mediaKeyTag, PrinterAttribute.MediaKeySupported, x.ToString());
                }).ToArray());
            if (src.MediaSourceSupported != null)
                dic.Add(PrinterAttribute.MediaSourceSupported, src.MediaSourceSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaSourceSupported, x.ToString())).ToArray());
            if (src.MediaTypeSupported != null)
                dic.Add(PrinterAttribute.MediaTypeSupported, src.MediaTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaTypeSupported, x.ToString())).ToArray());
            if (src.MediaBackCoatingSupported != null)
                dic.Add(PrinterAttribute.MediaBackCoatingSupported, src.MediaBackCoatingSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaBackCoatingSupported, x.ToString())).ToArray());
            if (src.MediaFrontCoatingSupported != null)
                dic.Add(PrinterAttribute.MediaFrontCoatingSupported, src.MediaFrontCoatingSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaFrontCoatingSupported, x.ToString())).ToArray());
            if (src.MediaColorSupported != null)
                dic.Add(PrinterAttribute.MediaColorSupported, src.MediaColorSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaColorSupported, x.ToString())).ToArray());
            if (src.MediaGrainSupported != null)
                dic.Add(PrinterAttribute.MediaGrainSupported, src.MediaGrainSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaGrainSupported, x.ToString())).ToArray());
            if (src.MediaToothSupported != null)
                dic.Add(PrinterAttribute.MediaToothSupported, src.MediaToothSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaToothSupported, x.ToString())).ToArray());
            if (src.MediaPrePrintedSupported != null)
                dic.Add(PrinterAttribute.MediaPrePrintedSupported, src.MediaPrePrintedSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaPrePrintedSupported, x.ToString())).ToArray());
            if (src.MediaRecycledSupported != null)
                dic.Add(PrinterAttribute.MediaRecycledSupported, src.MediaRecycledSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaRecycledSupported, x.ToString())).ToArray());
            if (src.MediaHoleCountSupported != null)
                dic.Add(PrinterAttribute.MediaHoleCountSupported, src.MediaHoleCountSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MediaHoleCountSupported, x)).ToArray());
            if (src.MediaOrderCountSupported != null)
                dic.Add(PrinterAttribute.MediaOrderCountSupported, src.MediaOrderCountSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MediaOrderCountSupported, x)).ToArray());
            if (src.MediaThicknessSupported != null)
                dic.Add(PrinterAttribute.MediaThicknessSupported, src.MediaThicknessSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MediaThicknessSupported, x)).ToArray());
            if (src.MediaWeightMetricSupported != null)
                dic.Add(PrinterAttribute.MediaWeightMetricSupported, src.MediaWeightMetricSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MediaWeightMetricSupported, x)).ToArray());
            if (src.MediaBottomMarginSupported != null)
                dic.Add(PrinterAttribute.MediaBottomMarginSupported, src.MediaBottomMarginSupported.Select(x => new IppAttribute(Tag.Integer, PrinterAttribute.MediaBottomMarginSupported, x)).ToArray());
            if (src.MediaLeftMarginSupported != null)
                dic.Add(PrinterAttribute.MediaLeftMarginSupported, src.MediaLeftMarginSupported.Select(x => new IppAttribute(Tag.Integer, PrinterAttribute.MediaLeftMarginSupported, x)).ToArray());
            if (src.MediaRightMarginSupported != null)
                dic.Add(PrinterAttribute.MediaRightMarginSupported, src.MediaRightMarginSupported.Select(x => new IppAttribute(Tag.Integer, PrinterAttribute.MediaRightMarginSupported, x)).ToArray());
            if (src.MediaTopMarginSupported != null)
                dic.Add(PrinterAttribute.MediaTopMarginSupported, src.MediaTopMarginSupported.Select(x => new IppAttribute(Tag.Integer, PrinterAttribute.MediaTopMarginSupported, x)).ToArray());
            if (src.PrintColorModeDefault != null)
                dic.Add(PrinterAttribute.PrintColorModeDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrintColorModeDefault, map.Map<string>(src.PrintColorModeDefault.Value)) });
            if (src.PrintColorModeSupported != null)
                dic.Add(PrinterAttribute.PrintColorModeSupported, src.PrintColorModeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintColorModeSupported, map.Map<string>(x))).ToArray());
            if (src.WhichJobsSupported != null)
                dic.Add(PrinterAttribute.WhichJobsSupported, src.WhichJobsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.WhichJobsSupported, map.Map<string>(x))).ToArray());
            if (src.PrinterUUID != null)
                dic.Add(PrinterAttribute.PrinterUUID, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterUUID, src.PrinterUUID) });
            if (src.PdfVersionsSupported != null)
                dic.Add(PrinterAttribute.PdfVersionsSupported, src.PdfVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, map.Map<string>(x))).ToArray());
            if (src.IppFeaturesSupported != null)
                dic.Add(PrinterAttribute.IppFeaturesSupported, src.IppFeaturesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, x)).ToArray());
            if (src.DocumentCreationAttributesSupported != null)
                dic.Add(PrinterAttribute.DocumentCreationAttributesSupported, src.DocumentCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.DocumentCreationAttributesSupported, x)).ToArray());
            if (src.JobAccountIdDefault != null)
                dic.Add(PrinterAttribute.JobAccountIdDefault, [new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.JobAccountIdDefault, src.JobAccountIdDefault)]);
            if (src.JobAccountTypeDefault != null)
                dic.Add(PrinterAttribute.JobAccountTypeDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountTypeDefault, map.Map<string>(src.JobAccountTypeDefault.Value))]);
            if (src.JobAccountTypeSupported != null)
                dic.Add(PrinterAttribute.JobAccountTypeSupported, src.JobAccountTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountTypeSupported, map.Map<string>(x))).ToArray());
            if (src.JobAccountIdSupported != null)
                dic.Add(PrinterAttribute.JobAccountIdSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobAccountIdSupported, src.JobAccountIdSupported.Value)]);
            if (src.JobAccountingUserIdDefault != null)
                dic.Add(PrinterAttribute.JobAccountingUserIdDefault, [new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.JobAccountingUserIdDefault, src.JobAccountingUserIdDefault)]);
            if (src.JobAccountingUserIdSupported != null)
                dic.Add(PrinterAttribute.JobAccountingUserIdSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobAccountingUserIdSupported, src.JobAccountingUserIdSupported.Value)]);
            if (src.JobPasswordEncryptionSupported != null)
                dic.Add(PrinterAttribute.JobPasswordEncryptionSupported, src.JobPasswordEncryptionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobPasswordEncryptionSupported, map.Map<string>(x))).ToArray());
            if (src.JobAuthorizationUriSupported != null)
                dic.Add(PrinterAttribute.JobAuthorizationUriSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobAuthorizationUriSupported, src.JobAuthorizationUriSupported.Value)]);
            if (src.PrinterChargeInfo != null)
                dic.Add(PrinterAttribute.PrinterChargeInfo, [new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterChargeInfo, src.PrinterChargeInfo)]);
            if (src.PrinterChargeInfoUri != null)
                dic.Add(PrinterAttribute.PrinterChargeInfoUri, [new IppAttribute(Tag.Uri, PrinterAttribute.PrinterChargeInfoUri, src.PrinterChargeInfoUri)]);
            if (src.PrinterMandatoryJobAttributes != null)
                dic.Add(PrinterAttribute.PrinterMandatoryJobAttributes, src.PrinterMandatoryJobAttributes.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterMandatoryJobAttributes, x)).ToArray());
            if (src.PrinterRequestedJobAttributes != null)
                dic.Add(PrinterAttribute.PrinterRequestedJobAttributes, src.PrinterRequestedJobAttributes.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterRequestedJobAttributes, x)).ToArray());
            if (src.PrinterAlert != null)
                dic.Add(PrinterAttribute.PrinterAlert, src.PrinterAlert.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterAlert, map.Map<string>(x))).ToArray());
            if (src.PrinterAlertDescription != null)
                dic.Add(PrinterAttribute.PrinterAlertDescription, src.PrinterAlertDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterAlertDescription, x)).ToArray());
            if (src.PrinterSupply != null)
                dic.Add(PrinterAttribute.PrinterSupply, src.PrinterSupply.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterSupply, x)).ToArray());
            if (src.PrinterSupplyDescription != null)
                dic.Add(PrinterAttribute.PrinterSupplyDescription, src.PrinterSupplyDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterSupplyDescription, x)).ToArray());
            if (src.JobCancelAfterDefault != null)
                dic.Add(PrinterAttribute.JobCancelAfterDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.JobCancelAfterDefault, src.JobCancelAfterDefault.Value)]);
            if (src.JobCancelAfterSupported != null)
                dic.Add(PrinterAttribute.JobCancelAfterSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobCancelAfterSupported, src.JobCancelAfterSupported.Value)]);
            if (src.JobSpoolingSupported != null)
                dic.Add(PrinterAttribute.JobSpoolingSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobSpoolingSupported, src.JobSpoolingSupported.Value)]);
            if (src.MaxPageRangesSupported != null)
                dic.Add(PrinterAttribute.MaxPageRangesSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.MaxPageRangesSupported, src.MaxPageRangesSupported.Value)]);
            if (src.PrintContentOptimizeDefault != null)
                dic.Add(PrinterAttribute.PrintContentOptimizeDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrintContentOptimizeDefault, src.PrintContentOptimizeDefault.Value)]);
            if (src.PrintContentOptimizeSupported != null)
                dic.Add(PrinterAttribute.PrintContentOptimizeSupported, src.PrintContentOptimizeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintContentOptimizeSupported, x)).ToArray());
            if (src.OutputDeviceSupported != null)
                dic.Add(PrinterAttribute.OutputDeviceSupported, src.OutputDeviceSupported.Select(x => new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.OutputDeviceSupported, x)).ToArray());
            if (src.OutputDeviceUuidSupported != null)
                dic.Add(PrinterAttribute.OutputDeviceUuidSupported, src.OutputDeviceUuidSupported.Select(x => new IppAttribute(Tag.Uri, PrinterAttribute.OutputDeviceUuidSupported, x)).ToArray());
            if (src.DocumentAccessSupported != null)
                dic.Add(PrinterAttribute.DocumentAccessSupported, src.DocumentAccessSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.DocumentAccessSupported, x)).ToArray());
            if (src.FetchDocumentAttributesSupported != null)
                dic.Add(PrinterAttribute.FetchDocumentAttributesSupported, src.FetchDocumentAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FetchDocumentAttributesSupported, x)).ToArray());
            if (src.PrinterModeConfigured != null)
                dic.Add(PrinterAttribute.PrinterModeConfigured, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterModeConfigured, src.PrinterModeConfigured.Value.Value)]);
            if (src.PrinterModeSupported != null)
                dic.Add(PrinterAttribute.PrinterModeSupported, src.PrinterModeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterModeSupported, x.Value)).ToArray());
            if (src.PrinterStaticResourceDirectoryUri != null)
                dic.Add(PrinterAttribute.PrinterStaticResourceDirectoryUri, [new IppAttribute(Tag.Uri, PrinterAttribute.PrinterStaticResourceDirectoryUri, src.PrinterStaticResourceDirectoryUri.ToString())]);
            if (src.PrinterStaticResourceKOctetsSupported != null)
                dic.Add(PrinterAttribute.PrinterStaticResourceKOctetsSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterStaticResourceKOctetsSupported, src.PrinterStaticResourceKOctetsSupported.Value)]);
            if (src.PrinterStaticResourceKOctetsFree != null)
                dic.Add(PrinterAttribute.PrinterStaticResourceKOctetsFree, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterStaticResourceKOctetsFree, src.PrinterStaticResourceKOctetsFree.Value)]);
            if (src.AccuracyUnitsSupported != null)
                dic.Add(PrinterAttribute.AccuracyUnitsSupported, src.AccuracyUnitsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.AccuracyUnitsSupported, x.Value)).ToArray());
            if (src.ChamberHumidityDefault != null)
                dic.Add(PrinterAttribute.ChamberHumidityDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.ChamberHumidityDefault, src.ChamberHumidityDefault.Value)]);
            if (src.ChamberHumiditySupported != null)
                dic.Add(PrinterAttribute.ChamberHumiditySupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.ChamberHumiditySupported, src.ChamberHumiditySupported.Value)]);
            if (src.ChamberTemperatureDefault != null)
                dic.Add(PrinterAttribute.ChamberTemperatureDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.ChamberTemperatureDefault, src.ChamberTemperatureDefault.Value)]);
            if (src.ChamberTemperatureSupported != null)
                dic.Add(PrinterAttribute.ChamberTemperatureSupported, src.ChamberTemperatureSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.ChamberTemperatureSupported, x)).ToArray());
            if (src.MaterialAmountUnitsSupported != null)
                dic.Add(PrinterAttribute.MaterialAmountUnitsSupported, src.MaterialAmountUnitsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MaterialAmountUnitsSupported, x.Value)).ToArray());
            if (src.MaterialDiameterSupported != null)
                dic.Add(PrinterAttribute.MaterialDiameterSupported, src.MaterialDiameterSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MaterialDiameterSupported, x)).ToArray());
            if (src.MaterialNozzleDiameterSupported != null)
                dic.Add(PrinterAttribute.MaterialNozzleDiameterSupported, src.MaterialNozzleDiameterSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MaterialNozzleDiameterSupported, x)).ToArray());
            if (src.MaterialPurposeSupported != null)
                dic.Add(PrinterAttribute.MaterialPurposeSupported, src.MaterialPurposeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MaterialPurposeSupported, x.Value)).ToArray());
            if (src.MaterialRateSupported != null)
                dic.Add(PrinterAttribute.MaterialRateSupported, src.MaterialRateSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MaterialRateSupported, x)).ToArray());
            if (src.MaterialRateUnitsSupported != null)
                dic.Add(PrinterAttribute.MaterialRateUnitsSupported, src.MaterialRateUnitsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MaterialRateUnitsSupported, x.Value)).ToArray());
            if (src.MaterialShellThicknessSupported != null)
                dic.Add(PrinterAttribute.MaterialShellThicknessSupported, src.MaterialShellThicknessSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MaterialShellThicknessSupported, x)).ToArray());
            if (src.MaterialTemperatureSupported != null)
                dic.Add(PrinterAttribute.MaterialTemperatureSupported, src.MaterialTemperatureSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.MaterialTemperatureSupported, x)).ToArray());
            if (src.MaterialTypeSupported != null)
                dic.Add(PrinterAttribute.MaterialTypeSupported, src.MaterialTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MaterialTypeSupported, x.Value)).ToArray());
            if (src.MaterialsColDatabase != null)
                dic.Add(PrinterAttribute.MaterialsColDatabase, src.MaterialsColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.MaterialsColDatabase)).ToArray());
            if (src.MaterialsColDefault != null)
                dic.Add(PrinterAttribute.MaterialsColDefault, src.MaterialsColDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.MaterialsColDefault)).ToArray());
            if (src.MaterialsColReady != null)
                dic.Add(PrinterAttribute.MaterialsColReady, src.MaterialsColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.MaterialsColReady)).ToArray());
            if (src.MaterialsColSupported != null)
                dic.Add(PrinterAttribute.MaterialsColSupported, src.MaterialsColSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MaterialsColSupported, x)).ToArray());
            if (src.MaxMaterialsColSupported != null)
                dic.Add(PrinterAttribute.MaxMaterialsColSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.MaxMaterialsColSupported, src.MaxMaterialsColSupported.Value)]);
            if (src.MultipleObjectHandlingDefault != null)
                dic.Add(PrinterAttribute.MultipleObjectHandlingDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.MultipleObjectHandlingDefault, src.MultipleObjectHandlingDefault.Value.Value)]);
            if (src.MultipleObjectHandlingSupported != null)
                dic.Add(PrinterAttribute.MultipleObjectHandlingSupported, src.MultipleObjectHandlingSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MultipleObjectHandlingSupported, x.Value)).ToArray());
            if (src.PdfFeaturesSupported != null)
                dic.Add(PrinterAttribute.PdfFeaturesSupported, src.PdfFeaturesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PdfFeaturesSupported, x)).ToArray());
            if (src.PlatformShape != null)
                dic.Add(PrinterAttribute.PlatformShape, [new IppAttribute(Tag.Keyword, PrinterAttribute.PlatformShape, src.PlatformShape.Value.Value)]);
            if (src.PlatformTemperatureDefault != null)
                dic.Add(PrinterAttribute.PlatformTemperatureDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.PlatformTemperatureDefault, src.PlatformTemperatureDefault.Value)]);
            if (src.PlatformTemperatureSupported != null)
                dic.Add(PrinterAttribute.PlatformTemperatureSupported, src.PlatformTemperatureSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PlatformTemperatureSupported, x)).ToArray());
            if (src.PrintAccuracyDefault != null)
                dic.Add(PrinterAttribute.PrintAccuracyDefault, map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracyDefault).ToBegCollection(PrinterAttribute.PrintAccuracyDefault).ToArray());
            if (src.PrintAccuracySupported != null)
                dic.Add(PrinterAttribute.PrintAccuracySupported, map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracySupported).ToBegCollection(PrinterAttribute.PrintAccuracySupported).ToArray());
            if (src.PrintBaseDefault != null)
                dic.Add(PrinterAttribute.PrintBaseDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrintBaseDefault, src.PrintBaseDefault.Value.Value)]);
            if (src.PrintBaseSupported != null)
                dic.Add(PrinterAttribute.PrintBaseSupported, src.PrintBaseSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintBaseSupported, x.Value)).ToArray());
            if (src.PrintObjectsSupported != null)
                dic.Add(PrinterAttribute.PrintObjectsSupported, src.PrintObjectsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintObjectsSupported, x)).ToArray());
            if (src.PrintSupportsDefault != null)
                dic.Add(PrinterAttribute.PrintSupportsDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrintSupportsDefault, src.PrintSupportsDefault.Value.Value)]);
            if (src.PrintSupportsSupported != null)
                dic.Add(PrinterAttribute.PrintSupportsSupported, src.PrintSupportsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintSupportsSupported, x.Value)).ToArray());
            if (src.PrinterVolumeSupported != null)
                dic.Add(PrinterAttribute.PrinterVolumeSupported, map.Map<IEnumerable<IppAttribute>>(src.PrinterVolumeSupported).ToBegCollection(PrinterAttribute.PrinterVolumeSupported).ToArray());
            if (src.ChamberHumidityCurrent != null)
                dic.Add(PrinterAttribute.ChamberHumidityCurrent, [new IppAttribute(Tag.Integer, PrinterAttribute.ChamberHumidityCurrent, src.ChamberHumidityCurrent.Value)]);
            if (src.ChamberTemperatureCurrent != null)
                dic.Add(PrinterAttribute.ChamberTemperatureCurrent, [new IppAttribute(Tag.Integer, PrinterAttribute.ChamberTemperatureCurrent, src.ChamberTemperatureCurrent.Value)]);
            if (src.PrinterCameraImageUri != null)
                dic.Add(PrinterAttribute.PrinterCameraImageUri, src.PrinterCameraImageUri.Select(x => new IppAttribute(Tag.Uri, PrinterAttribute.PrinterCameraImageUri, x)).ToArray());
            if (src.PrinterResourceIds != null)
                dic.Add(PrinterAttribute.PrinterResourceIds, src.PrinterResourceIds.Select(x => new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, x)).ToArray());
            if (src.JobCreationAttributesSupported != null)
                dic.Add(PrinterAttribute.JobCreationAttributesSupported, src.JobCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobCreationAttributesSupported, x)).ToArray());
            if (src.PrinterRequestedClientType != null)
                dic.Add(PrinterAttribute.PrinterRequestedClientType, src.PrinterRequestedClientType.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.PrinterRequestedClientType, (int)x)).ToArray());
            if (src.PrinterServiceType != null)
                dic.Add(PrinterAttribute.PrinterServiceType, src.PrinterServiceType.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, map.Map<string>(x))).ToArray());
            if (src.FinishingTemplateSupported != null)
                dic.Add(PrinterAttribute.FinishingTemplateSupported, src.FinishingTemplateSupported.Select(x =>
                {
                    var finishingTemplateTag = x.ToIppTag();
                    return new IppAttribute(finishingTemplateTag, PrinterAttribute.FinishingTemplateSupported, x);
                }).ToArray());
            if (src.FinishingsColSupported != null)
                dic.Add(PrinterAttribute.FinishingsColSupported, src.FinishingsColSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FinishingsColSupported, x)).ToArray());
            if (src.JobPagesPerSetSupported != null)
                dic.Add(PrinterAttribute.JobPagesPerSetSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobPagesPerSetSupported, src.JobPagesPerSetSupported.Value)]);
            if (src.PunchingHoleDiameterConfigured != null)
                dic.Add(PrinterAttribute.PunchingHoleDiameterConfigured, [new IppAttribute(Tag.Integer, PrinterAttribute.PunchingHoleDiameterConfigured, src.PunchingHoleDiameterConfigured.Value)]);
            if (src.PrinterFinisher != null)
                dic.Add(PrinterAttribute.PrinterFinisher, src.PrinterFinisher.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterFinisher, map.Map<string>(x))).ToArray());
            if (src.PrinterFinisherDescription != null)
                dic.Add(PrinterAttribute.PrinterFinisherDescription, src.PrinterFinisherDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterFinisherDescription, x)).ToArray());
            if (src.PrinterFinisherSupplies != null)
                dic.Add(PrinterAttribute.PrinterFinisherSupplies, src.PrinterFinisherSupplies.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterFinisherSupplies, map.Map<string>(x))).ToArray());
            if (src.PrinterFinisherSuppliesDescription != null)
                dic.Add(PrinterAttribute.PrinterFinisherSuppliesDescription, src.PrinterFinisherSuppliesDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterFinisherSuppliesDescription, x)).ToArray());
            if (src.FinishingsColDefault != null)
                dic.Add(
                    PrinterAttribute.FinishingsColDefault,
                    src.FinishingsColDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.FinishingsColDefault)
                    ).ToArray());
            if (src.FinishingsColReady != null)
                dic.Add(PrinterAttribute.FinishingsColReady, src.FinishingsColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.FinishingsColReady)).ToArray());
            if (src.BalingTypeSupported != null)
                dic.Add(PrinterAttribute.BalingTypeSupported, src.BalingTypeSupported.Select(x =>
                {
                    var balingTypeTag = x.ToIppTag();
                    return new IppAttribute(balingTypeTag, PrinterAttribute.BalingTypeSupported, x);
                }).ToArray());
            if (src.BalingWhenSupported != null)
                dic.Add(PrinterAttribute.BalingWhenSupported, src.BalingWhenSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.BalingWhenSupported, map.Map<string>(x))).ToArray());
            if (src.BindingReferenceEdgeSupported != null)
                dic.Add(PrinterAttribute.BindingReferenceEdgeSupported, src.BindingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.BindingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.BindingTypeSupported != null)
                dic.Add(PrinterAttribute.BindingTypeSupported, src.BindingTypeSupported.Select(x =>
                {
                    var bindingTypeTag = x.ToIppTag();
                    return new IppAttribute(bindingTypeTag, PrinterAttribute.BindingTypeSupported, x);
                }).ToArray());
            if (src.CoatingSidesSupported != null)
                dic.Add(PrinterAttribute.CoatingSidesSupported, src.CoatingSidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoatingSidesSupported, map.Map<string>(x))).ToArray());
            if (src.CoatingTypeSupported != null)
                dic.Add(PrinterAttribute.CoatingTypeSupported, src.CoatingTypeSupported.Select(x =>
                {
                    var coatingTypeTag = x.ToIppTag();
                    return new IppAttribute(coatingTypeTag, PrinterAttribute.CoatingTypeSupported, x);
                }).ToArray());
            if (src.CoveringNameSupported != null)
                dic.Add(PrinterAttribute.CoveringNameSupported, src.CoveringNameSupported.Select(x =>
                {
                    var coveringNameTag = x.ToIppTag();
                    return new IppAttribute(coveringNameTag, PrinterAttribute.CoveringNameSupported, x);
                }).ToArray());
            if (src.FinishingsColDatabase != null)
                dic.Add(
                    PrinterAttribute.FinishingsColDatabase,
                    src.FinishingsColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.FinishingsColDatabase)
                    ).ToArray());
            if (src.FoldingDirectionSupported != null)
                dic.Add(PrinterAttribute.FoldingDirectionSupported, src.FoldingDirectionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FoldingDirectionSupported, map.Map<string>(x))).ToArray());
            if (src.FoldingOffsetSupported != null)
                dic.Add(PrinterAttribute.FoldingOffsetSupported, src.FoldingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.FoldingOffsetSupported, x)).ToArray());
            if (src.FoldingReferenceEdgeSupported != null)
                dic.Add(PrinterAttribute.FoldingReferenceEdgeSupported, src.FoldingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FoldingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.LaminatingSidesSupported != null)
                dic.Add(PrinterAttribute.LaminatingSidesSupported, src.LaminatingSidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.LaminatingSidesSupported, map.Map<string>(x))).ToArray());
            if (src.LaminatingTypeSupported != null)
                dic.Add(PrinterAttribute.LaminatingTypeSupported, src.LaminatingTypeSupported.Select(x =>
                {
                    var laminatingTypeTag = x.ToIppTag();
                    return new IppAttribute(laminatingTypeTag, PrinterAttribute.LaminatingTypeSupported, x);
                }).ToArray());
            if (src.PunchingLocationsSupported != null)
                dic.Add(PrinterAttribute.PunchingLocationsSupported, src.PunchingLocationsSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PunchingLocationsSupported, x)).ToArray());
            if (src.PunchingOffsetSupported != null)
                dic.Add(PrinterAttribute.PunchingOffsetSupported, src.PunchingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PunchingOffsetSupported, x)).ToArray());
            if (src.PunchingReferenceEdgeSupported != null)
                dic.Add(PrinterAttribute.PunchingReferenceEdgeSupported, src.PunchingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PunchingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.StitchingAngleSupported != null)
                dic.Add(PrinterAttribute.StitchingAngleSupported, src.StitchingAngleSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.StitchingAngleSupported, x)).ToArray());
            if (src.StitchingLocationsSupported != null)
                dic.Add(PrinterAttribute.StitchingLocationsSupported, src.StitchingLocationsSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.StitchingLocationsSupported, x)).ToArray());
            if (src.StitchingMethodSupported != null)
                dic.Add(PrinterAttribute.StitchingMethodSupported, src.StitchingMethodSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.StitchingMethodSupported, map.Map<string>(x))).ToArray());
            if (src.StitchingOffsetSupported != null)
                dic.Add(PrinterAttribute.StitchingOffsetSupported, src.StitchingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.StitchingOffsetSupported, x)).ToArray());
            if (src.StitchingReferenceEdgeSupported != null)
                dic.Add(PrinterAttribute.StitchingReferenceEdgeSupported, src.StitchingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.StitchingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.TrimmingOffsetSupported != null)
                dic.Add(PrinterAttribute.TrimmingOffsetSupported, src.TrimmingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.TrimmingOffsetSupported, x)).ToArray());
            if (src.TrimmingReferenceEdgeSupported != null)
                dic.Add(PrinterAttribute.TrimmingReferenceEdgeSupported, src.TrimmingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.TrimmingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
            if (src.TrimmingTypeSupported != null)
                dic.Add(PrinterAttribute.TrimmingTypeSupported, src.TrimmingTypeSupported.Select(x =>
                {
                    var trimmingTypeTag = x.ToIppTag();
                    return new IppAttribute(trimmingTypeTag, PrinterAttribute.TrimmingTypeSupported, map.Map<string>(x));
                }).ToArray());
            if (src.TrimmingWhenSupported != null)
                dic.Add(PrinterAttribute.TrimmingWhenSupported, src.TrimmingWhenSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.TrimmingWhenSupported, map.Map<string>(x))).ToArray());
            if (src.CoverBackDefault != null)
                dic.Add(PrinterAttribute.CoverBackDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverBackDefault).ToBegCollection(PrinterAttribute.CoverBackDefault).ToArray());
            if (src.CoverBackSupported != null)
                dic.Add(PrinterAttribute.CoverBackSupported, src.CoverBackSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverBackSupported, map.Map<string>(x))).ToArray());
            if (src.CoverFrontDefault != null)
                dic.Add(PrinterAttribute.CoverFrontDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverFrontDefault).ToBegCollection(PrinterAttribute.CoverFrontDefault).ToArray());
            if (src.CoverFrontSupported != null)
                dic.Add(PrinterAttribute.CoverFrontSupported, src.CoverFrontSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverFrontSupported, map.Map<string>(x))).ToArray());
            if (src.CoverTypeSupported != null)
                dic.Add(PrinterAttribute.CoverTypeSupported, src.CoverTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverTypeSupported, map.Map<string>(x))).ToArray());
            if (src.ForceFrontSideSupported != null)
                dic.Add(PrinterAttribute.ForceFrontSideSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.ForceFrontSideSupported, src.ForceFrontSideSupported.Value)]);
            if (src.ImageOrientationDefault != null)
                dic.Add(PrinterAttribute.ImageOrientationDefault, [new IppAttribute(Tag.Enum, PrinterAttribute.ImageOrientationDefault, (int)src.ImageOrientationDefault.Value)]);
            if (src.ImageOrientationSupported != null)
                dic.Add(PrinterAttribute.ImageOrientationSupported, src.ImageOrientationSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.ImageOrientationSupported, (int)x)).ToArray());
            if (src.ImpositionTemplateDefault != null)
            {
                var impositionTemplateDefaultTag = src.ImpositionTemplateDefault.Value.ToIppTag();
                dic.Add(PrinterAttribute.ImpositionTemplateDefault, [new IppAttribute(impositionTemplateDefaultTag, PrinterAttribute.ImpositionTemplateDefault, map.Map<string>(src.ImpositionTemplateDefault.Value))]);
            }
            if (src.ImpositionTemplateSupported != null)
                dic.Add(PrinterAttribute.ImpositionTemplateSupported, src.ImpositionTemplateSupported.Select(x =>
                {
                    var impositionTemplateTag = x.ToIppTag();
                    return new IppAttribute(impositionTemplateTag, PrinterAttribute.ImpositionTemplateSupported, map.Map<string>(x));
                }).ToArray());
            if (src.InsertCountSupported != null)
                dic.Add(PrinterAttribute.InsertCountSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.InsertCountSupported, src.InsertCountSupported.Value)]);
            if (src.InsertSheetDefault != null)
                dic.Add(PrinterAttribute.InsertSheetDefault, src.InsertSheetDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.InsertSheetDefault)).ToArray());
            if (src.InsertSheetSupported != null)
                dic.Add(PrinterAttribute.InsertSheetSupported, src.InsertSheetSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InsertSheetSupported, map.Map<string>(x))).ToArray());
            if (src.JobAccountingOutputBinSupported != null)
                dic.Add(PrinterAttribute.JobAccountingOutputBinSupported, src.JobAccountingOutputBinSupported.Select(x =>
                {
                    var outputBinTag = x.ToIppTag();
                    return new IppAttribute(outputBinTag, PrinterAttribute.JobAccountingOutputBinSupported, x.ToString());
                }).ToArray());
            if (src.JobAccountingSheetsDefault != null)
                dic.Add(PrinterAttribute.JobAccountingSheetsDefault, map.Map<IEnumerable<IppAttribute>>(src.JobAccountingSheetsDefault).ToBegCollection(PrinterAttribute.JobAccountingSheetsDefault).ToArray());
            if (src.JobAccountingSheetsSupported != null)
                dic.Add(PrinterAttribute.JobAccountingSheetsSupported, src.JobAccountingSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountingSheetsSupported, map.Map<string>(x))).ToArray());
            if (src.JobAccountingSheetsTypeSupported != null)
                dic.Add(PrinterAttribute.JobAccountingSheetsTypeSupported, src.JobAccountingSheetsTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountingSheetsTypeSupported, x)).ToArray());
            if (src.JobCompleteBeforeSupported != null)
                dic.Add(PrinterAttribute.JobCompleteBeforeSupported, src.JobCompleteBeforeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobCompleteBeforeSupported, x)).ToArray());
            if (src.JobCompleteBeforeTimeSupported != null)
                dic.Add(PrinterAttribute.JobCompleteBeforeTimeSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobCompleteBeforeTimeSupported, src.JobCompleteBeforeTimeSupported.Value)]);
            if (src.JobErrorSheetDefault != null)
                dic.Add(PrinterAttribute.JobErrorSheetDefault, map.Map<IEnumerable<IppAttribute>>(src.JobErrorSheetDefault).ToBegCollection(PrinterAttribute.JobErrorSheetDefault).ToArray());
            if (src.JobErrorSheetSupported != null)
                dic.Add(PrinterAttribute.JobErrorSheetSupported, src.JobErrorSheetSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobErrorSheetSupported, map.Map<string>(x))).ToArray());
            if (src.JobErrorSheetTypeSupported != null)
                dic.Add(PrinterAttribute.JobErrorSheetTypeSupported, src.JobErrorSheetTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobErrorSheetTypeSupported, x)).ToArray());
            if (src.JobErrorSheetWhenSupported != null)
                dic.Add(PrinterAttribute.JobErrorSheetWhenSupported, src.JobErrorSheetWhenSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobErrorSheetWhenSupported, x)).ToArray());
            if (src.JobMessageToOperatorSupported != null)
                dic.Add(PrinterAttribute.JobMessageToOperatorSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobMessageToOperatorSupported, src.JobMessageToOperatorSupported.Value)]);
            if (src.JobPhoneNumberDefault != null)
                dic.Add(PrinterAttribute.JobPhoneNumberDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobPhoneNumberDefault, src.JobPhoneNumberDefault)]);
            if (src.JobPhoneNumberSchemeSupported != null)
                dic.Add(PrinterAttribute.JobPhoneNumberSchemeSupported, src.JobPhoneNumberSchemeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobPhoneNumberSchemeSupported, map.Map<string>(x))).ToArray());
            if (src.JobPhoneNumberSupported != null)
                dic.Add(PrinterAttribute.JobPhoneNumberSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobPhoneNumberSupported, src.JobPhoneNumberSupported.Value)]);
            if (src.JobRecipientNameSupported != null)
                dic.Add(PrinterAttribute.JobRecipientNameSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobRecipientNameSupported, src.JobRecipientNameSupported.Value)]);
            if (src.JobSheetMessageSupported != null)
                dic.Add(PrinterAttribute.JobSheetMessageSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobSheetMessageSupported, src.JobSheetMessageSupported.Value)]);
            if (src.PageDeliveryDefault != null)
                dic.Add(PrinterAttribute.PageDeliveryDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PageDeliveryDefault, map.Map<string>(src.PageDeliveryDefault.Value))]);
            if (src.PageDeliverySupported != null)
                dic.Add(PrinterAttribute.PageDeliverySupported, src.PageDeliverySupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PageDeliverySupported, map.Map<string>(x))).ToArray());
            if (src.PresentationDirectionNumberUpDefault != null)
                dic.Add(PrinterAttribute.PresentationDirectionNumberUpDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PresentationDirectionNumberUpDefault, map.Map<string>(src.PresentationDirectionNumberUpDefault.Value))]);
            if (src.PresentationDirectionNumberUpSupported != null)
                dic.Add(PrinterAttribute.PresentationDirectionNumberUpSupported, src.PresentationDirectionNumberUpSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PresentationDirectionNumberUpSupported, map.Map<string>(x))).ToArray());
            if (src.SeparatorSheetsDefault != null)
                dic.Add(PrinterAttribute.SeparatorSheetsDefault, map.Map<IEnumerable<IppAttribute>>(src.SeparatorSheetsDefault).ToBegCollection(PrinterAttribute.SeparatorSheetsDefault).ToArray());
            if (src.SeparatorSheetsSupported != null)
                dic.Add(PrinterAttribute.SeparatorSheetsSupported, src.SeparatorSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.SeparatorSheetsSupported, map.Map<string>(x))).ToArray());
            if (src.SeparatorSheetsTypeSupported != null)
                dic.Add(PrinterAttribute.SeparatorSheetsTypeSupported, src.SeparatorSheetsTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.SeparatorSheetsTypeSupported, x)).ToArray());
            if (src.XImagePositionDefault != null)
                dic.Add(PrinterAttribute.XImagePositionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.XImagePositionDefault, map.Map<string>(src.XImagePositionDefault.Value))]);
            if (src.XImagePositionSupported != null)
                dic.Add(PrinterAttribute.XImagePositionSupported, src.XImagePositionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.XImagePositionSupported, map.Map<string>(x))).ToArray());
            if (src.XImageShiftDefault != null)
                dic.Add(PrinterAttribute.XImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.XImageShiftDefault, src.XImageShiftDefault.Value)]);
            if (src.XImageShiftSupported != null)
                dic.Add(PrinterAttribute.XImageShiftSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.XImageShiftSupported, src.XImageShiftSupported.Value)]);
            if (src.XSide1ImageShiftDefault != null)
                dic.Add(PrinterAttribute.XSide1ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.XSide1ImageShiftDefault, src.XSide1ImageShiftDefault.Value)]);
            if (src.XSide2ImageShiftDefault != null)
                dic.Add(PrinterAttribute.XSide2ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.XSide2ImageShiftDefault, src.XSide2ImageShiftDefault.Value)]);
            if (src.YImagePositionDefault != null)
                dic.Add(PrinterAttribute.YImagePositionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.YImagePositionDefault, map.Map<string>(src.YImagePositionDefault.Value))]);
            if (src.YImagePositionSupported != null)
                dic.Add(PrinterAttribute.YImagePositionSupported, src.YImagePositionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.YImagePositionSupported, map.Map<string>(x))).ToArray());
            if (src.YImageShiftDefault != null)
                dic.Add(PrinterAttribute.YImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.YImageShiftDefault, src.YImageShiftDefault.Value)]);
            if (src.YImageShiftSupported != null)
                dic.Add(PrinterAttribute.YImageShiftSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.YImageShiftSupported, src.YImageShiftSupported.Value)]);
            if (src.YSide1ImageShiftDefault != null)
                dic.Add(PrinterAttribute.YSide1ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.YSide1ImageShiftDefault, src.YSide1ImageShiftDefault.Value)]);
            if (src.YSide2ImageShiftDefault != null)
                dic.Add(PrinterAttribute.YSide2ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.YSide2ImageShiftDefault, src.YSide2ImageShiftDefault.Value)]);
            if (src.ConfirmationSheetPrintDefault.HasValue)
                dic.Add(PrinterAttribute.ConfirmationSheetPrintDefault, [new IppAttribute(Tag.Boolean, PrinterAttribute.ConfirmationSheetPrintDefault, src.ConfirmationSheetPrintDefault.Value)]);
            if (src.CoverSheetInfoDefault != null)
                dic.Add(PrinterAttribute.CoverSheetInfoDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverSheetInfoDefault).ToBegCollection(PrinterAttribute.CoverSheetInfoDefault).ToArray());
            if (src.CoverSheetInfoSupported != null)
                dic.Add(PrinterAttribute.CoverSheetInfoSupported, src.CoverSheetInfoSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverSheetInfoSupported, x)).ToArray());
            if (src.DestinationAccessesSupported != null)
                dic.Add(PrinterAttribute.DestinationAccessesSupported, src.DestinationAccessesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.DestinationAccessesSupported, x)).ToArray());
            if (src.DestinationUriReady != null)
                dic.Add(PrinterAttribute.DestinationUriReady, src.DestinationUriReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.DestinationUriReady)).ToArray());
            if (src.DestinationUriSchemesSupported != null)
                dic.Add(PrinterAttribute.DestinationUriSchemesSupported, src.DestinationUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, PrinterAttribute.DestinationUriSchemesSupported, map.Map<string>(x))).ToArray());
            if (src.DestinationUrisSupported != null)
                dic.Add(PrinterAttribute.DestinationUrisSupported, src.DestinationUrisSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.DestinationUrisSupported, x)).ToArray());
            if (src.FromNameSupported.HasValue)
                dic.Add(PrinterAttribute.FromNameSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.FromNameSupported, src.FromNameSupported.Value)]);
            if (src.InputAttributesDefault != null)
                dic.Add(PrinterAttribute.InputAttributesDefault, map.Map<IEnumerable<IppAttribute>>(src.InputAttributesDefault).ToBegCollection(PrinterAttribute.InputAttributesDefault).ToArray());
            if (src.InputAttributesSupported != null)
                dic.Add(PrinterAttribute.InputAttributesSupported, src.InputAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InputAttributesSupported, x)).ToArray());
            if (src.InputColorModeSupported != null)
                dic.Add(PrinterAttribute.InputColorModeSupported, src.InputColorModeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InputColorModeSupported, x.Value)).ToArray());
            if (src.InputContentTypeSupported != null)
                dic.Add(PrinterAttribute.InputContentTypeSupported, src.InputContentTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InputContentTypeSupported, x.Value)).ToArray());
            if (src.InputFilmScanModeSupported != null)
                dic.Add(PrinterAttribute.InputFilmScanModeSupported, src.InputFilmScanModeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InputFilmScanModeSupported, x.Value)).ToArray());
            if (src.InputMediaSupported != null)
                dic.Add(PrinterAttribute.InputMediaSupported, src.InputMediaSupported.Select(x => new IppAttribute(x.ToIppTag(), PrinterAttribute.InputMediaSupported, map.Map<string>(x))).ToArray());
            if (src.InputOrientationRequestedSupported != null)
                dic.Add(PrinterAttribute.InputOrientationRequestedSupported, src.InputOrientationRequestedSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.InputOrientationRequestedSupported, (int)x)).ToArray());
            if (src.InputQualitySupported != null)
                dic.Add(PrinterAttribute.InputQualitySupported, src.InputQualitySupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.InputQualitySupported, (int)x)).ToArray());
            if (src.InputResolutionSupported != null)
                dic.Add(PrinterAttribute.InputResolutionSupported, src.InputResolutionSupported.Select(x => new IppAttribute(Tag.Resolution, PrinterAttribute.InputResolutionSupported, x)).ToArray());
            if (src.InputSidesSupported != null)
                dic.Add(PrinterAttribute.InputSidesSupported, src.InputSidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InputSidesSupported, map.Map<string>(x))).ToArray());
            if (src.InputSourceSupported != null)
                dic.Add(PrinterAttribute.InputSourceSupported, src.InputSourceSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InputSourceSupported, x.Value)).ToArray());
            if (src.LogoUriFormatsSupported != null)
                dic.Add(PrinterAttribute.LogoUriFormatsSupported, src.LogoUriFormatsSupported.Select(x => new IppAttribute(Tag.MimeMediaType, PrinterAttribute.LogoUriFormatsSupported, x)).ToArray());
            if (src.LogoUriSchemesSupported != null)
                dic.Add(PrinterAttribute.LogoUriSchemesSupported, src.LogoUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, PrinterAttribute.LogoUriSchemesSupported, map.Map<string>(x))).ToArray());
            if (src.MessageSupported.HasValue)
                dic.Add(PrinterAttribute.MessageSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.MessageSupported, src.MessageSupported.Value)]);
            if (src.MultipleDestinationUrisSupported.HasValue)
                dic.Add(PrinterAttribute.MultipleDestinationUrisSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.MultipleDestinationUrisSupported, src.MultipleDestinationUrisSupported.Value)]);
            if (src.NumberOfRetriesDefault.HasValue)
                dic.Add(PrinterAttribute.NumberOfRetriesDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.NumberOfRetriesDefault, src.NumberOfRetriesDefault.Value)]);
            if (src.NumberOfRetriesSupported != null)
                dic.Add(PrinterAttribute.NumberOfRetriesSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.NumberOfRetriesSupported, src.NumberOfRetriesSupported.Value)]);
            if (src.OrganizationNameSupported.HasValue)
                dic.Add(PrinterAttribute.OrganizationNameSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.OrganizationNameSupported, src.OrganizationNameSupported.Value)]);
            if (src.JobDestinationSpoolingSupported != null)
                dic.Add(PrinterAttribute.JobDestinationSpoolingSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobDestinationSpoolingSupported, map.Map<string>(src.JobDestinationSpoolingSupported.Value))]);
            if (src.OutputAttributesDefault != null)
                dic.Add(PrinterAttribute.OutputAttributesDefault, map.Map<IEnumerable<IppAttribute>>(src.OutputAttributesDefault).ToBegCollection(PrinterAttribute.OutputAttributesDefault).ToArray());
            if (src.OutputAttributesSupported != null)
                dic.Add(PrinterAttribute.OutputAttributesSupported, src.OutputAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.OutputAttributesSupported, x)).ToArray());
            if (src.PrinterFaxLogUri != null)
                dic.Add(PrinterAttribute.PrinterFaxLogUri, [new IppAttribute(Tag.Uri, PrinterAttribute.PrinterFaxLogUri, src.PrinterFaxLogUri.ToString())]);
            if (src.PrinterFaxModemInfo != null)
                dic.Add(PrinterAttribute.PrinterFaxModemInfo, src.PrinterFaxModemInfo.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterFaxModemInfo, x)).ToArray());
            if (src.PrinterFaxModemName != null)
                dic.Add(PrinterAttribute.PrinterFaxModemName, src.PrinterFaxModemName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.PrinterFaxModemName, x)).ToArray());
            if (src.PrinterFaxModemNumber != null)
                dic.Add(PrinterAttribute.PrinterFaxModemNumber, src.PrinterFaxModemNumber.Select(x => new IppAttribute(Tag.Uri, PrinterAttribute.PrinterFaxModemNumber, x.ToString())).ToArray());
            if (src.RetryIntervalDefault.HasValue)
                dic.Add(PrinterAttribute.RetryIntervalDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.RetryIntervalDefault, src.RetryIntervalDefault.Value)]);
            if (src.RetryIntervalSupported != null)
                dic.Add(PrinterAttribute.RetryIntervalSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.RetryIntervalSupported, src.RetryIntervalSupported.Value)]);
            if (src.RetryTimeOutDefault.HasValue)
                dic.Add(PrinterAttribute.RetryTimeOutDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.RetryTimeOutDefault, src.RetryTimeOutDefault.Value)]);
            if (src.RetryTimeOutSupported != null)
                dic.Add(PrinterAttribute.RetryTimeOutSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.RetryTimeOutSupported, src.RetryTimeOutSupported.Value)]);
            if (src.SubjectSupported.HasValue)
                dic.Add(PrinterAttribute.SubjectSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.SubjectSupported, src.SubjectSupported.Value)]);
            if (src.ToNameSupported.HasValue)
                dic.Add(PrinterAttribute.ToNameSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.ToNameSupported, src.ToNameSupported.Value)]);
            return dic;
        });
    }
}
