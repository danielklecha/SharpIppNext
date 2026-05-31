using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class GetJobAttributesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        //https://tools.ietf.org/html/rfc2911#section-4.4
        mapper.CreateMap<IppResponseMessage, GetJobAttributesResponse>((src, map) =>
        {
            var dst = new GetJobAttributesResponse { JobAttributes = map.Map<JobDescriptionAttributes>(src.JobAttributes.SelectMany(x => x).ToIppDictionary()) };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetJobAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            var jobAttrs = new List<IppAttribute>();
            jobAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.JobAttributes).Values.SelectMany(x => x));
            dst.JobAttributes.Add(jobAttrs);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobDescriptionAttributes>((src, map) =>
        {
            var dst = new JobDescriptionAttributes
            {
            DateTimeAtCompleted = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCompleted),
            DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCreation),
            DateTimeAtProcessing = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtProcessing),
            JobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobId),
            JobUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobUri),
            JobImpressionsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobImpressionsCompleted),
            JobMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobMediaSheetsCompleted),
            JobOriginatingUserName = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobOriginatingUserName),
            JobPrinterUpTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPrinterUpTime),
            JobPrinterUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobPrinterUri),
            JobState = map.MapFromDicNullable<JobState?>(src, IppAttributeNames.JobState),
            JobStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobStateMessage),
            JobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, IppAttributeNames.JobStateReasons),
            JobResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.JobResourceIds),
            TimeAtCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCompleted),
            TimeAtCreation = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCreation),
            TimeAtProcessing = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtProcessing),
            JobName = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobName),
            JobKOctetsProcessed = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobKOctetsProcessed),
            JobImpressions = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobImpressions),
            JobImpressionsCol = src.ContainsKey(IppAttributeNames.JobImpressionsCol)
                ? map.Map<JobCounter>(src[IppAttributeNames.JobImpressionsCol].FromBegCollection().ToIppDictionary())
                : null,
            JobMediaSheets = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobMediaSheets),
            JobMediaSheetsCol = src.ContainsKey(IppAttributeNames.JobMediaSheetsCol)
                ? map.Map<JobCounter>(src[IppAttributeNames.JobMediaSheetsCol].FromBegCollection().ToIppDictionary())
                : null,
            JobMoreInfo = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobMoreInfo),
            JobChargeInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobChargeInfo),
            DocumentFormatDetails = src.ContainsKey(IppAttributeNames.DocumentFormatDetails)
                ? map.Map<DocumentFormatDetails>(src[IppAttributeNames.DocumentFormatDetails].FromBegCollection().ToIppDictionary())
                : null,
            DocumentFormatDetailsDetected = src.ContainsKey(IppAttributeNames.DocumentFormatDetailsDetected)
                ? map.Map<DocumentFormatDetails>(src[IppAttributeNames.DocumentFormatDetailsDetected].FromBegCollection().ToIppDictionary())
                : null,
            NumberOfDocuments = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberOfDocuments),
            NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberOfInterveningJobs),
            OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, IppAttributeNames.OutputDeviceAssigned),
            JobKOctets = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobKOctets),
            JobDetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobDetailedStatusMessages),
            JobDocumentAccessErrors = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobDocumentAccessErrors),
            JobMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobMessageFromOperator),
            JobPages = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPages),
            JobPagesCol = src.ContainsKey(IppAttributeNames.JobPagesCol)
                ? map.Map<JobCounter>(src[IppAttributeNames.JobPagesCol].FromBegCollection().ToIppDictionary())
                : null,
            JobPagesCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPagesCompleted),
            JobImpressionsCompletedCol = src.ContainsKey(IppAttributeNames.JobImpressionsCompletedCol)
                ? map.Map<JobCounter>(src[IppAttributeNames.JobImpressionsCompletedCol].FromBegCollection().ToIppDictionary())
                : null,
            JobMediaSheetsCompletedCol = src.ContainsKey(IppAttributeNames.JobMediaSheetsCompletedCol)
                ? map.Map<JobCounter>(src[IppAttributeNames.JobMediaSheetsCompletedCol].FromBegCollection().ToIppDictionary())
                : null,
            JobPagesCompletedCol = src.ContainsKey(IppAttributeNames.JobPagesCompletedCol)
                ? map.Map<JobCounter>(src[IppAttributeNames.JobPagesCompletedCol].FromBegCollection().ToIppDictionary())
                : null,
            JobProcessingTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobProcessingTime),
            JobPagesCompletedCurrentCopy = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPagesCompletedCurrentCopy),
            PagesCompletedCurrentCopy = map.MapFromDicNullable<int?>(src, IppAttributeNames.PagesCompletedCurrentCopy),
            ClientInfo = null,
            JobSheetsCol = src.ContainsKey(IppAttributeNames.JobSheetsCol)
                ? map.Map<JobSheetsCol>(src[IppAttributeNames.JobSheetsCol].FromBegCollection().ToIppDictionary())
                : null,
            ErrorsCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.ErrorsCount),
            WarningsCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.WarningsCount),
            PrintContentOptimizeActual = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, IppAttributeNames.PrintContentOptimizeActual),
            CopiesActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.CopiesActual),
            FinishingsActual = map.MapFromDicSetNullable<Finishings[]?>(src, IppAttributeNames.FinishingsActual),
            CoverBackActual = null,
            CoverFrontActual = null,
            JobHoldUntilActual = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, IppAttributeNames.JobHoldUntilActual),
            JobPriorityActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.JobPriorityActual),
            JobSheetsActual = map.MapFromDicSetNullable<JobSheets[]?>(src, IppAttributeNames.JobSheetsActual),
            MediaActual = map.MapFromDicSetNullable<string, Media>(src, IppAttributeNames.MediaActual, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
            ImpositionTemplateActual = map.MapFromDicSetNullable<string, ImpositionTemplate>(src, IppAttributeNames.ImpositionTemplateActual, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
            InsertSheetActual = null,
            JobAccountIdActual = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobAccountIdActual),
            JobAccountingSheetsActual = null,
            JobAccountingUserIdActual = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobAccountingUserIdActual),
            JobErrorSheetActual = null,
            JobMessageToOperatorActual = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobMessageToOperatorActual),
            JobSheetMessageActual = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobSheetMessageActual),
            MediaInputTrayCheckActual = map.MapFromDicSetNullable<MediaInputTrayCheck[]?>(src, IppAttributeNames.MediaInputTrayCheckActual),
            MultipleDocumentHandlingActual = map.MapFromDicSetNullable<MultipleDocumentHandling[]?>(src, IppAttributeNames.MultipleDocumentHandlingActual),
            NumberUpActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.NumberUpActual),
            OrientationRequestedActual = map.MapFromDicSetNullable<Orientation[]?>(src, IppAttributeNames.OrientationRequestedActual),
            OutputBinActual = map.MapFromDicSetNullable<string, OutputBin>(src, IppAttributeNames.OutputBinActual, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
            PageDeliveryActual = map.MapFromDicSetNullable<PageDelivery[]?>(src, IppAttributeNames.PageDeliveryActual),
            PageOrderReceivedActual = map.MapFromDicSetNullable<PageOrderReceived[]?>(src, IppAttributeNames.PageOrderReceivedActual),
            PageRangesActual = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, IppAttributeNames.PageRangesActual),
            PresentationDirectionNumberUpActual = map.MapFromDicSetNullable<PresentationDirectionNumberUp[]?>(src, IppAttributeNames.PresentationDirectionNumberUpActual),
            PrintQualityActual = map.MapFromDicSetNullable<PrintQuality[]?>(src, IppAttributeNames.PrintQualityActual),
            PrinterResolutionActual = map.MapFromDicSetNullable<Resolution[]?>(src, IppAttributeNames.PrinterResolutionActual),
            SidesActual = map.MapFromDicSetNullable<Sides[]?>(src, IppAttributeNames.SidesActual),
            XImagePositionActual = map.MapFromDicSetNullable<XImagePosition[]?>(src, IppAttributeNames.XImagePositionActual),
            XImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.XImageShiftActual),
            XSide1ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.XSide1ImageShiftActual),
            XSide2ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.XSide2ImageShiftActual),
            YImagePositionActual = map.MapFromDicSetNullable<YImagePosition[]?>(src, IppAttributeNames.YImagePositionActual),
            YImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.YImageShiftActual),
            YSide1ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.YSide1ImageShiftActual),
            YSide2ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.YSide2ImageShiftActual),
            DateTimeAtCompletedEstimated = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCompletedEstimated),
            DateTimeAtProcessingEstimated = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtProcessingEstimated),
            TimeAtCompletedEstimated = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCompletedEstimated),
            TimeAtProcessingEstimated = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtProcessingEstimated),
            DocumentFormatReady = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DocumentFormatReady),
            OutputDeviceJobState = map.MapFromDicNullable<JobState?>(src, IppAttributeNames.OutputDeviceJobState),
            OutputDeviceJobStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.OutputDeviceJobStateMessage),
            OutputDeviceJobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, IppAttributeNames.OutputDeviceJobStateReasons),
            OutputDeviceUuidAssigned = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuidAssigned),
            ChamberHumidityActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ChamberHumidityActual),
            ChamberTemperatureActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ChamberTemperatureActual),
            MultipleObjectHandlingActual3d = map.MapFromDicNullable<MultipleObjectHandling?>(src, IppAttributeNames.MultipleObjectHandlingActual3d),
            PlatformTemperatureActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.PlatformTemperatureActual),
            PrintBaseActual3d = map.MapFromDicSetNullable<PrintBase[]?>(src, IppAttributeNames.PrintBaseActual3d),
            PrintSupportsActual3d = map.MapFromDicSetNullable<PrintSupports[]?>(src, IppAttributeNames.PrintSupportsActual3d),
            PrintAccuracyActual3d = null,
            PrintObjectsActual3d = null,
            MaterialsColActual = null,
            DestinationStatuses = null,
            ChamberHumidityCurrent = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberHumidityCurrent),
            ChamberTemperatureCurrent = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberTemperatureCurrent),
            };

            if (src.TryGetValue(IppAttributeNames.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.CoverBackActual, out var coverBackActual))
                dst.CoverBackActual = coverBackActual.GroupBegCollection().Select(x => map.Map<Cover>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.CoverFrontActual, out var coverFrontActual))
                dst.CoverFrontActual = coverFrontActual.GroupBegCollection().Select(x => map.Map<Cover>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.InsertSheetActual, out var insertSheetActual))
                dst.InsertSheetActual = insertSheetActual.GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.JobAccountingSheetsActual, out var jobAccountingSheetsActual))
                dst.JobAccountingSheetsActual = jobAccountingSheetsActual.GroupBegCollection().Select(x => map.Map<JobAccountingSheets>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.JobErrorSheetActual, out var jobErrorSheetActual))
                dst.JobErrorSheetActual = jobErrorSheetActual.GroupBegCollection().Select(x => map.Map<JobErrorSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.MediaColActual, out var mediaColActual))
                dst.MediaColActual = mediaColActual.GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.OverridesActual, out var overridesActual))
                dst.OverridesActual = overridesActual.GroupBegCollection().Select(x => map.Map<OverrideInstruction>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.FinishingsColActual, out var finishingsColActual))
                dst.FinishingsColActual = finishingsColActual.GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.MaterialsColActual, out var materialsColActual))
                dst.MaterialsColActual = materialsColActual.GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.PrintAccuracyActual3d, out var printAccuracyActual3d))
                dst.PrintAccuracyActual3d = map.Map<PrintAccuracy>(printAccuracyActual3d.FromBegCollection().ToIppDictionary());

            if (src.TryGetValue(IppAttributeNames.PrintObjectsActual3d, out var printObjectsActual3d))
                dst.PrintObjectsActual3d = printObjectsActual3d.GroupBegCollection().Select(x => map.Map<PrintObject>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.SeparatorSheetsActual, out var separatorSheetsActual))
                dst.SeparatorSheetsActual = separatorSheetsActual.GroupBegCollection().Select(x => map.Map<SeparatorSheets>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(IppAttributeNames.DestinationStatuses, out var destinationStatuses))
                dst.DestinationStatuses = destinationStatuses.GroupBegCollection().Select(x => map.Map<DestinationStatus>(x.FromBegCollection().ToIppDictionary())).ToArray();

            dst.JobCopiesActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.JobCopiesActual);
            dst.JobKOctetsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobKOctetsCompleted);
            dst.JobPassword = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(src, IppAttributeNames.JobPasswordEncryption);
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobMandatoryAttributes);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.JobIds);
            dst.RequestingUserUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.RequestingUserUri);
            dst.JobChargeInfoUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobChargeInfoUri);

            return dst;
        });

        mapper.CreateMap<JobDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.DateTimeAtCompleted != null)
                dic.Add(IppAttributeNames.DateTimeAtCompleted, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCompleted, src.DateTimeAtCompleted.Value)]);
            if (src.DateTimeAtCreation != null)
                dic.Add(IppAttributeNames.DateTimeAtCreation, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtProcessing != null)
                dic.Add(IppAttributeNames.DateTimeAtProcessing, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtProcessing, src.DateTimeAtProcessing.Value)]);
            if (src.JobId != null)
                dic.Add(IppAttributeNames.JobId, [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId.Value)]);
            if (src.JobUri != null)
                dic.Add(IppAttributeNames.JobUri, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, src.JobUri.ToString()) });
            if (src.JobImpressionsCompleted != null)
                dic.Add(IppAttributeNames.JobImpressionsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobImpressionsCompleted, src.JobImpressionsCompleted.Value)]);
            if (src.JobMediaSheetsCompleted != null)
                dic.Add(IppAttributeNames.JobMediaSheetsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobMediaSheetsCompleted, src.JobMediaSheetsCompleted.Value)]);
            if (src.JobOriginatingUserName != null)
                dic.Add(IppAttributeNames.JobOriginatingUserName, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobOriginatingUserName, src.JobOriginatingUserName)]);
            if (src.JobPrinterUpTime != null)
                dic.Add(IppAttributeNames.JobPrinterUpTime, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPrinterUpTime, src.JobPrinterUpTime.Value)]);
            if (src.JobPrinterUri != null)
                dic.Add(IppAttributeNames.JobPrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.JobPrinterUri, src.JobPrinterUri.ToString())]);
            if (src.JobState != null)
                dic.Add(IppAttributeNames.JobState, [new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)src.JobState.Value)]);
            if (src.JobStateMessage != null)
                dic.Add(IppAttributeNames.JobStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobStateMessage, src.JobStateMessage)]);
            if (src.JobStateReasons != null)
                dic.Add(IppAttributeNames.JobStateReasons, src.JobStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, map.Map<string>(x))).ToArray());
            if (src.JobResourceIds != null)
                dic.Add(IppAttributeNames.JobResourceIds, src.JobResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobResourceIds, x)).ToArray());
            if (src.TimeAtCompleted != null)
                dic.Add(IppAttributeNames.TimeAtCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCompleted, src.TimeAtCompleted.Value)]);
            if (src.TimeAtCreation != null)
                dic.Add(IppAttributeNames.TimeAtCreation, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtProcessing != null)
                dic.Add(IppAttributeNames.TimeAtProcessing, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtProcessing, src.TimeAtProcessing.Value)]);
            if (src.JobName != null)
                dic.Add(IppAttributeNames.JobName, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobName, src.JobName)]);
            if (src.JobKOctetsProcessed != null)
                dic.Add(IppAttributeNames.JobKOctetsProcessed, [new IppAttribute(Tag.Integer, IppAttributeNames.JobKOctetsProcessed, src.JobKOctetsProcessed.Value)]);
            if (src.JobImpressions != null)
                dic.Add(IppAttributeNames.JobImpressions, [new IppAttribute(Tag.Integer, IppAttributeNames.JobImpressions, src.JobImpressions.Value)]);
            if (src.JobImpressionsCol != null)
                dic.Add(IppAttributeNames.JobImpressionsCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCol).ToBegCollection(IppAttributeNames.JobImpressionsCol).ToArray());
            if (src.JobMediaSheets != null)
                dic.Add(IppAttributeNames.JobMediaSheets, [new IppAttribute(Tag.Integer, IppAttributeNames.JobMediaSheets, src.JobMediaSheets.Value)]);
            if (src.JobMediaSheetsCol != null)
                dic.Add(IppAttributeNames.JobMediaSheetsCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCol).ToBegCollection(IppAttributeNames.JobMediaSheetsCol).ToArray());
            if (src.JobMoreInfo != null)
                dic.Add(IppAttributeNames.JobMoreInfo, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.JobMoreInfo, src.JobMoreInfo.ToString()) });
            if (src.JobChargeInfo != null)
                dic.Add(IppAttributeNames.JobChargeInfo, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobChargeInfo, src.JobChargeInfo)]);
            if (src.DocumentFormatDetails != null)
                dic.Add(IppAttributeNames.DocumentFormatDetails, map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(IppAttributeNames.DocumentFormatDetails).ToArray());
            if (src.DocumentFormatDetailsDetected != null)
                dic.Add(IppAttributeNames.DocumentFormatDetailsDetected, map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetailsDetected).ToBegCollection(IppAttributeNames.DocumentFormatDetailsDetected).ToArray());
            if (src.NumberOfDocuments != null)
                dic.Add(IppAttributeNames.NumberOfDocuments, [new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfDocuments, src.NumberOfDocuments.Value)]);
            if (src.NumberOfInterveningJobs != null)
                dic.Add(IppAttributeNames.NumberOfInterveningJobs, [new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value)]);
            if (src.OutputDeviceAssigned != null)
                dic.Add(IppAttributeNames.OutputDeviceAssigned, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputDeviceAssigned, src.OutputDeviceAssigned) });
            if (src.JobKOctets != null)
                dic.Add(IppAttributeNames.JobKOctets, [new IppAttribute(Tag.Integer, IppAttributeNames.JobKOctets, src.JobKOctets.Value)]);
            if (src.JobDetailedStatusMessages != null)
                dic.Add(IppAttributeNames.JobDetailedStatusMessages, src.JobDetailedStatusMessages.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobDetailedStatusMessages, x)).ToArray());
            if (src.JobDocumentAccessErrors != null)
                dic.Add(IppAttributeNames.JobDocumentAccessErrors, src.JobDocumentAccessErrors.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobDocumentAccessErrors, x)).ToArray());
            if (src.JobMessageFromOperator != null)
                dic.Add(IppAttributeNames.JobMessageFromOperator, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobMessageFromOperator, src.JobMessageFromOperator) });
            if (src.JobPages != null)
                dic.Add(IppAttributeNames.JobPages, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPages, src.JobPages.Value)]);
            if (src.JobPagesCol != null)
                dic.Add(IppAttributeNames.JobPagesCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCol).ToBegCollection(IppAttributeNames.JobPagesCol).ToArray());
            if (src.JobPagesCompleted != null)
                dic.Add(IppAttributeNames.JobPagesCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobImpressionsCompletedCol != null)
                dic.Add(IppAttributeNames.JobImpressionsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCompletedCol).ToBegCollection(IppAttributeNames.JobImpressionsCompletedCol).ToArray());
            if (src.JobMediaSheetsCompletedCol != null)
                dic.Add(IppAttributeNames.JobMediaSheetsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCompletedCol).ToBegCollection(IppAttributeNames.JobMediaSheetsCompletedCol).ToArray());
            if (src.JobPagesCompletedCol != null)
                dic.Add(IppAttributeNames.JobPagesCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCompletedCol).ToBegCollection(IppAttributeNames.JobPagesCompletedCol).ToArray());
            if (src.JobProcessingTime != null)
                dic.Add(IppAttributeNames.JobProcessingTime, [new IppAttribute(Tag.Integer, IppAttributeNames.JobProcessingTime, src.JobProcessingTime.Value)]);
            if (src.ClientInfo != null)
                dic.Add(IppAttributeNames.ClientInfo, src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.ClientInfo)).ToArray());
            if (src.JobSheetsCol != null)
                dic.Add(IppAttributeNames.JobSheetsCol, map.Map<IEnumerable<IppAttribute>>(src.JobSheetsCol).ToBegCollection(IppAttributeNames.JobSheetsCol).ToArray());
            if (src.ErrorsCount != null)
                dic.Add(IppAttributeNames.ErrorsCount, [new IppAttribute(Tag.Integer, IppAttributeNames.ErrorsCount, src.ErrorsCount.Value)]);
            if (src.WarningsCount != null)
                dic.Add(IppAttributeNames.WarningsCount, [new IppAttribute(Tag.Integer, IppAttributeNames.WarningsCount, src.WarningsCount.Value)]);
            if (src.PrintContentOptimizeActual != null)
                dic.Add(IppAttributeNames.PrintContentOptimizeActual, src.PrintContentOptimizeActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintContentOptimizeActual, map.Map<string>(x))).ToArray());
            if (src.CopiesActual != null)
                dic.Add(IppAttributeNames.CopiesActual, src.CopiesActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.CopiesActual, x)).ToArray());
            if (src.FinishingsActual != null)
                dic.Add(IppAttributeNames.FinishingsActual, src.FinishingsActual.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.FinishingsActual, (int)x)).ToArray());
            if (src.CoverBackActual != null)
                dic.Add(IppAttributeNames.CoverBackActual, src.CoverBackActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.CoverBackActual)).ToArray());
            if (src.CoverFrontActual != null)
                dic.Add(IppAttributeNames.CoverFrontActual, src.CoverFrontActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.CoverFrontActual)).ToArray());
            if (src.JobHoldUntilActual != null)
                dic.Add(IppAttributeNames.JobHoldUntilActual, src.JobHoldUntilActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobHoldUntilActual, map.Map<string>(x))).ToArray());
            if (src.JobPriorityActual != null)
                dic.Add(IppAttributeNames.JobPriorityActual, src.JobPriorityActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobPriorityActual, x)).ToArray());
            if (src.JobSheetsActual != null)
                dic.Add(IppAttributeNames.JobSheetsActual, src.JobSheetsActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobSheetsActual, map.Map<string>(x))).ToArray());
            if (src.MediaActual != null)
                dic.Add(IppAttributeNames.MediaActual, src.MediaActual.Select(x =>
                {
                    var mediaTag = x.ToIppTag();
                    return new IppAttribute(mediaTag, IppAttributeNames.MediaActual, x);
                }).ToArray());
            if (src.ImpositionTemplateActual != null)
                dic.Add(IppAttributeNames.ImpositionTemplateActual, src.ImpositionTemplateActual.Select(x =>
                {
                    var impositionTemplate = map.Map<string>(x);
                    var impositionTemplateTag = x.ToIppTag();
                    return new IppAttribute(impositionTemplateTag, IppAttributeNames.ImpositionTemplateActual, impositionTemplate);
                }).ToArray());
            if (src.InsertSheetActual != null)
                dic.Add(IppAttributeNames.InsertSheetActual, src.InsertSheetActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.InsertSheetActual)).ToArray());
            if (src.JobAccountIdActual != null)
                dic.Add(IppAttributeNames.JobAccountIdActual, src.JobAccountIdActual.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobAccountIdActual, x)).ToArray());
            if (src.JobAccountingSheetsActual != null)
                dic.Add(IppAttributeNames.JobAccountingSheetsActual, src.JobAccountingSheetsActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobAccountingSheetsActual)).ToArray());
            if (src.JobAccountingUserIdActual != null)
                dic.Add(IppAttributeNames.JobAccountingUserIdActual, src.JobAccountingUserIdActual.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobAccountingUserIdActual, x)).ToArray());
            if (src.JobErrorSheetActual != null)
                dic.Add(IppAttributeNames.JobErrorSheetActual, src.JobErrorSheetActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobErrorSheetActual)).ToArray());
            if (src.JobMessageToOperatorActual != null)
                dic.Add(IppAttributeNames.JobMessageToOperatorActual, src.JobMessageToOperatorActual.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobMessageToOperatorActual, x)).ToArray());
            if (src.JobSheetMessageActual != null)
                dic.Add(IppAttributeNames.JobSheetMessageActual, src.JobSheetMessageActual.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobSheetMessageActual, x)).ToArray());
            if (src.MediaColActual != null)
                dic.Add(IppAttributeNames.MediaColActual, src.MediaColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MediaColActual)).ToArray());
            if (src.MediaInputTrayCheckActual != null)
                dic.Add(IppAttributeNames.MediaInputTrayCheckActual, src.MediaInputTrayCheckActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MediaInputTrayCheckActual, map.Map<string>(x))).ToArray());
            if (src.MultipleDocumentHandlingActual != null)
                dic.Add(IppAttributeNames.MultipleDocumentHandlingActual, src.MultipleDocumentHandlingActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleDocumentHandlingActual, map.Map<string>(x))).ToArray());
            if (src.NumberUpActual != null)
                dic.Add(IppAttributeNames.NumberUpActual, src.NumberUpActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.NumberUpActual, x)).ToArray());
            if (src.OrientationRequestedActual != null)
                dic.Add(IppAttributeNames.OrientationRequestedActual, src.OrientationRequestedActual.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequestedActual, (int)x)).ToArray());
            if (src.OutputBinActual != null)
                dic.Add(IppAttributeNames.OutputBinActual, src.OutputBinActual.Select(x =>
                {
                    var outputBin = map.Map<string>(x);
                    var outputBinTag = x.ToIppTag();
                    return new IppAttribute(outputBinTag, IppAttributeNames.OutputBinActual, outputBin);
                }).ToArray());
            if (src.PageDeliveryActual != null)
                dic.Add(IppAttributeNames.PageDeliveryActual, src.PageDeliveryActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PageDeliveryActual, map.Map<string>(x))).ToArray());
            if (src.PageOrderReceivedActual != null)
                dic.Add(IppAttributeNames.PageOrderReceivedActual, src.PageOrderReceivedActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PageOrderReceivedActual, map.Map<string>(x))).ToArray());
            if (src.PageRangesActual != null)
                dic.Add(IppAttributeNames.PageRangesActual, src.PageRangesActual.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PageRangesActual, x)).ToArray());
            if (src.PresentationDirectionNumberUpActual != null)
                dic.Add(IppAttributeNames.PresentationDirectionNumberUpActual, src.PresentationDirectionNumberUpActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PresentationDirectionNumberUpActual, map.Map<string>(x))).ToArray());
            if (src.PrintQualityActual != null)
                dic.Add(IppAttributeNames.PrintQualityActual, src.PrintQualityActual.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.PrintQualityActual, (int)x)).ToArray());
            if (src.PrinterResolutionActual != null)
                dic.Add(IppAttributeNames.PrinterResolutionActual, src.PrinterResolutionActual.Select(x => new IppAttribute(Tag.Resolution, IppAttributeNames.PrinterResolutionActual, x)).ToArray());
            if (src.SidesActual != null)
                dic.Add(IppAttributeNames.SidesActual, src.SidesActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SidesActual, map.Map<string>(x))).ToArray());
            if (src.SeparatorSheetsActual != null)
                dic.Add(IppAttributeNames.SeparatorSheetsActual, src.SeparatorSheetsActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SeparatorSheetsActual)).ToArray());
            if (src.XImagePositionActual != null)
                dic.Add(IppAttributeNames.XImagePositionActual, src.XImagePositionActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.XImagePositionActual, map.Map<string>(x))).ToArray());
            if (src.XImageShiftActual != null)
                dic.Add(IppAttributeNames.XImageShiftActual, src.XImageShiftActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.XImageShiftActual, x)).ToArray());
            if (src.XSide1ImageShiftActual != null)
                dic.Add(IppAttributeNames.XSide1ImageShiftActual, src.XSide1ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.XSide1ImageShiftActual, x)).ToArray());
            if (src.XSide2ImageShiftActual != null)
                dic.Add(IppAttributeNames.XSide2ImageShiftActual, src.XSide2ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.XSide2ImageShiftActual, x)).ToArray());
            if (src.YImagePositionActual != null)
                dic.Add(IppAttributeNames.YImagePositionActual, src.YImagePositionActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.YImagePositionActual, map.Map<string>(x))).ToArray());
            if (src.YImageShiftActual != null)
                dic.Add(IppAttributeNames.YImageShiftActual, src.YImageShiftActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.YImageShiftActual, x)).ToArray());
            if (src.YSide1ImageShiftActual != null)
                dic.Add(IppAttributeNames.YSide1ImageShiftActual, src.YSide1ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.YSide1ImageShiftActual, x)).ToArray());
            if (src.YSide2ImageShiftActual != null)
                dic.Add(IppAttributeNames.YSide2ImageShiftActual, src.YSide2ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.YSide2ImageShiftActual, x)).ToArray());
            if (src.OverridesActual != null)
                dic.Add(IppAttributeNames.OverridesActual, src.OverridesActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.OverridesActual)).ToArray());
            if (src.FinishingsColActual != null)
                dic.Add(IppAttributeNames.FinishingsColActual, src.FinishingsColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.FinishingsColActual)).ToArray());
            if (src.DateTimeAtCompletedEstimated != null)
                dic.Add(IppAttributeNames.DateTimeAtCompletedEstimated, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCompletedEstimated, src.DateTimeAtCompletedEstimated.Value)]);
            if (src.DateTimeAtProcessingEstimated != null)
                dic.Add(IppAttributeNames.DateTimeAtProcessingEstimated, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtProcessingEstimated, src.DateTimeAtProcessingEstimated.Value)]);
            if (src.TimeAtCompletedEstimated != null)
                dic.Add(IppAttributeNames.TimeAtCompletedEstimated, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCompletedEstimated, src.TimeAtCompletedEstimated.Value)]);
            if (src.TimeAtProcessingEstimated != null)
                dic.Add(IppAttributeNames.TimeAtProcessingEstimated, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtProcessingEstimated, src.TimeAtProcessingEstimated.Value)]);
            if (src.DocumentFormatReady != null)
                dic.Add(IppAttributeNames.DocumentFormatReady, src.DocumentFormatReady.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatReady, x)).ToArray());
            if (src.OutputDeviceJobState != null)
                dic.Add(IppAttributeNames.OutputDeviceJobState, [new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceJobState, (int)src.OutputDeviceJobState.Value)]);
            if (src.OutputDeviceJobStateMessage != null)
                dic.Add(IppAttributeNames.OutputDeviceJobStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.OutputDeviceJobStateMessage, src.OutputDeviceJobStateMessage)]);
            if (src.OutputDeviceJobStateReasons != null)
                dic.Add(IppAttributeNames.OutputDeviceJobStateReasons, src.OutputDeviceJobStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.OutputDeviceJobStateReasons, map.Map<string>(x))).ToArray());
            if (src.OutputDeviceUuidAssigned != null)
                dic.Add(IppAttributeNames.OutputDeviceUuidAssigned, [new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuidAssigned, src.OutputDeviceUuidAssigned.ToString())]);
            if (src.ChamberHumidityActual != null)
                dic.Add(IppAttributeNames.ChamberHumidityActual, src.ChamberHumidityActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidityActual, x)).ToArray());
            if (src.ChamberTemperatureActual != null)
                dic.Add(IppAttributeNames.ChamberTemperatureActual, src.ChamberTemperatureActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperatureActual, x)).ToArray());
            if (src.MultipleObjectHandlingActual3d != null)
                dic.Add(IppAttributeNames.MultipleObjectHandlingActual3d, [new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandlingActual3d, src.MultipleObjectHandlingActual3d.Value.Value)]);
            if (src.PlatformTemperatureActual != null)
                dic.Add(IppAttributeNames.PlatformTemperatureActual, src.PlatformTemperatureActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperatureActual, x)).ToArray());
            if (src.PrintAccuracyActual3d != null)
                dic.Add(IppAttributeNames.PrintAccuracyActual3d, map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracyActual3d).ToBegCollection(IppAttributeNames.PrintAccuracyActual3d).ToArray());
            if (src.PrintBaseActual3d != null)
                dic.Add(IppAttributeNames.PrintBaseActual3d, src.PrintBaseActual3d.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBaseActual3d, map.Map<string>(x))).ToArray());
            if (src.PrintObjectsActual3d != null)
                dic.Add(IppAttributeNames.PrintObjectsActual3d, src.PrintObjectsActual3d.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrintObjectsActual3d)).ToArray());
            if (src.PrintSupportsActual3d != null)
                dic.Add(IppAttributeNames.PrintSupportsActual3d, src.PrintSupportsActual3d.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupportsActual3d, map.Map<string>(x))).ToArray());
            if (src.MaterialsColActual != null)
                dic.Add(IppAttributeNames.MaterialsColActual, src.MaterialsColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MaterialsColActual)).ToArray());
            if (src.DestinationStatuses != null)
                dic.Add(IppAttributeNames.DestinationStatuses, src.DestinationStatuses.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.DestinationStatuses)).ToArray());
            if (src.JobCopiesActual != null)
                dic.Add(IppAttributeNames.JobCopiesActual, src.JobCopiesActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobCopiesActual, x)).ToArray());
            if (src.JobKOctetsCompleted != null)
                dic.Add(IppAttributeNames.JobKOctetsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobKOctetsCompleted, src.JobKOctetsCompleted.Value)]);
            if (src.JobPassword != null)
                dic.Add(IppAttributeNames.JobPassword, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.JobPassword, src.JobPassword.Value)]);
            if (src.JobPasswordEncryption != null)
                dic.Add(IppAttributeNames.JobPasswordEncryption, [new IppAttribute(Tag.Keyword, IppAttributeNames.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value))]);
            if (src.JobMandatoryAttributes != null)
                dic.Add(IppAttributeNames.JobMandatoryAttributes, src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, x)).ToArray());
            if (src.JobIds != null)
                dic.Add(IppAttributeNames.JobIds, src.JobIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, x)).ToArray());
            if (src.RequestingUserUri != null)
                dic.Add(IppAttributeNames.RequestingUserUri, [new IppAttribute(Tag.Uri, IppAttributeNames.RequestingUserUri, src.RequestingUserUri.ToString())]);
            if (src.JobChargeInfoUri != null)
                dic.Add(IppAttributeNames.JobChargeInfoUri, [new IppAttribute(Tag.Uri, IppAttributeNames.JobChargeInfoUri, src.JobChargeInfoUri.ToString())]);
            if (src.JobPagesCompletedCurrentCopy != null)
                dic.Add(IppAttributeNames.JobPagesCompletedCurrentCopy, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPagesCompletedCurrentCopy, src.JobPagesCompletedCurrentCopy.Value)]);
            if (src.PagesCompletedCurrentCopy != null)
                dic.Add(IppAttributeNames.PagesCompletedCurrentCopy, [new IppAttribute(Tag.Integer, IppAttributeNames.PagesCompletedCurrentCopy, src.PagesCompletedCurrentCopy.Value)]);
            if (src.ChamberHumidityCurrent != null)
                dic.Add(IppAttributeNames.ChamberHumidityCurrent, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidityCurrent, src.ChamberHumidityCurrent.Value)]);
            if (src.ChamberTemperatureCurrent != null)
                dic.Add(IppAttributeNames.ChamberTemperatureCurrent, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperatureCurrent, src.ChamberTemperatureCurrent.Value)]);
            return dic;
        });
    }
}
