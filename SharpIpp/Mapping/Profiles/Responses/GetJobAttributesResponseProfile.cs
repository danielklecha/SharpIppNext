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
            DateTimeAtCompleted = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtCompleted),
            DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtCreation),
            DateTimeAtProcessing = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtProcessing),
            JobId = map.MapFromDicNullable<int?>(src, JobAttribute.JobId),
            JobUri = map.MapFromDicNullable<string?>(src, JobAttribute.JobUri),
            JobImpressionsCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressionsCompleted),
            JobMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheetsCompleted),
            JobOriginatingUserName = map.MapFromDicNullable<string?>(src, JobAttribute.JobOriginatingUserName),
            JobPrinterUpTime = map.MapFromDicNullable<int?>(src, JobAttribute.JobPrinterUpTime),
            JobPrinterUri = map.MapFromDicNullable<string?>(src, JobAttribute.JobPrinterUri),
            JobState = map.MapFromDicNullable<JobState?>(src, JobAttribute.JobState),
            JobStateMessage = map.MapFromDicNullable<string?>(src, JobAttribute.JobStateMessage),
            JobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, JobAttribute.JobStateReasons),
            JobResourceIds = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobResourceIds),
            TimeAtCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCompleted),
            TimeAtCreation = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCreation),
            TimeAtProcessing = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtProcessing),
            JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName),
            JobKOctetsProcessed = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctetsProcessed),
            JobImpressions = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressions),
            JobImpressionsCol = src.ContainsKey(JobAttribute.JobImpressionsCol)
                ? map.Map<JobCounter>(src[JobAttribute.JobImpressionsCol].FromBegCollection().ToIppDictionary())
                : null,
            JobMediaSheets = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheets),
            JobMediaSheetsCol = src.ContainsKey(JobAttribute.JobMediaSheetsCol)
                ? map.Map<JobCounter>(src[JobAttribute.JobMediaSheetsCol].FromBegCollection().ToIppDictionary())
                : null,
            JobMoreInfo = map.MapFromDicNullable<string?>(src, JobAttribute.JobMoreInfo),
            JobChargeInfo = map.MapFromDicNullable<string?>(src, JobAttribute.JobChargeInfo),
            DocumentFormatDetails = src.ContainsKey(JobAttribute.DocumentFormatDetails)
                ? map.Map<DocumentFormatDetails>(src[JobAttribute.DocumentFormatDetails].FromBegCollection().ToIppDictionary())
                : null,
            DocumentFormatDetailsDetected = src.ContainsKey(JobAttribute.DocumentFormatDetailsDetected)
                ? map.Map<DocumentFormatDetails>(src[JobAttribute.DocumentFormatDetailsDetected].FromBegCollection().ToIppDictionary())
                : null,
            NumberOfDocuments = map.MapFromDicNullable<int?>(src, JobAttribute.NumberOfDocuments),
            NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, JobAttribute.NumberOfInterveningJobs),
            OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, JobAttribute.OutputDeviceAssigned),
            JobKOctets = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctets),
            JobDetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobDetailedStatusMessages),
            JobDocumentAccessErrors = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobDocumentAccessErrors),
            JobMessageFromOperator = map.MapFromDicNullable<string?>(src, JobAttribute.JobMessageFromOperator),
            JobPages = map.MapFromDicNullable<int?>(src, JobAttribute.JobPages),
            JobPagesCol = src.ContainsKey(JobAttribute.JobPagesCol)
                ? map.Map<JobCounter>(src[JobAttribute.JobPagesCol].FromBegCollection().ToIppDictionary())
                : null,
            JobPagesCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobPagesCompleted),
            JobImpressionsCompletedCol = src.ContainsKey(JobAttribute.JobImpressionsCompletedCol)
                ? map.Map<JobCounter>(src[JobAttribute.JobImpressionsCompletedCol].FromBegCollection().ToIppDictionary())
                : null,
            JobMediaSheetsCompletedCol = src.ContainsKey(JobAttribute.JobMediaSheetsCompletedCol)
                ? map.Map<JobCounter>(src[JobAttribute.JobMediaSheetsCompletedCol].FromBegCollection().ToIppDictionary())
                : null,
            JobPagesCompletedCol = src.ContainsKey(JobAttribute.JobPagesCompletedCol)
                ? map.Map<JobCounter>(src[JobAttribute.JobPagesCompletedCol].FromBegCollection().ToIppDictionary())
                : null,
            JobProcessingTime = map.MapFromDicNullable<int?>(src, JobAttribute.JobProcessingTime),
            ClientInfo = null,
            JobSheetsCol = src.ContainsKey(JobAttribute.JobSheetsCol)
                ? map.Map<JobSheetsCol>(src[JobAttribute.JobSheetsCol].FromBegCollection().ToIppDictionary())
                : null,
            ErrorsCount = map.MapFromDicNullable<int?>(src, JobAttribute.ErrorsCount),
            WarningsCount = map.MapFromDicNullable<int?>(src, JobAttribute.WarningsCount),
            PrintContentOptimizeActual = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, JobAttribute.PrintContentOptimizeActual),
            CopiesActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.CopiesActual),
            FinishingsActual = map.MapFromDicSetNullable<Finishings[]?>(src, JobAttribute.FinishingsActual),
            CoverBackActual = null,
            CoverFrontActual = null,
            JobHoldUntilActual = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, JobAttribute.JobHoldUntilActual),
            JobPriorityActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobPriorityActual),
            JobSheetsActual = map.MapFromDicSetNullable<JobSheets[]?>(src, JobAttribute.JobSheetsActual),
            MediaActual = map.MapFromDicSetNullable<string, Media>(src, JobAttribute.MediaActual, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
            ImpositionTemplateActual = map.MapFromDicSetNullable<string, ImpositionTemplate>(src, JobAttribute.ImpositionTemplateActual, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
            InsertSheetActual = null,
            JobAccountIdActual = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobAccountIdActual),
            JobAccountingSheetsActual = null,
            JobAccountingUserIdActual = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobAccountingUserIdActual),
            JobErrorSheetActual = null,
            JobMessageToOperatorActual = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobMessageToOperatorActual),
            JobSheetMessageActual = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobSheetMessageActual),
            MediaColActual = null,
            MediaInputTrayCheckActual = map.MapFromDicSetNullable<MediaInputTrayCheck[]?>(src, JobAttribute.MediaInputTrayCheckActual),
            MultipleDocumentHandlingActual = map.MapFromDicSetNullable<MultipleDocumentHandling[]?>(src, JobAttribute.MultipleDocumentHandlingActual),
            NumberUpActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.NumberUpActual),
            OrientationRequestedActual = map.MapFromDicSetNullable<Orientation[]?>(src, JobAttribute.OrientationRequestedActual),
            OutputBinActual = map.MapFromDicSetNullable<string, OutputBin>(src, JobAttribute.OutputBinActual, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword)),
            PageDeliveryActual = map.MapFromDicSetNullable<PageDelivery[]?>(src, JobAttribute.PageDeliveryActual),
            PageOrderReceivedActual = map.MapFromDicSetNullable<PageOrderReceived[]?>(src, JobAttribute.PageOrderReceivedActual),
            PageRangesActual = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, JobAttribute.PageRangesActual),
            PresentationDirectionNumberUpActual = map.MapFromDicSetNullable<PresentationDirectionNumberUp[]?>(src, JobAttribute.PresentationDirectionNumberUpActual),
            PrintQualityActual = map.MapFromDicSetNullable<PrintQuality[]?>(src, JobAttribute.PrintQualityActual),
            PrinterResolutionActual = map.MapFromDicSetNullable<Resolution[]?>(src, JobAttribute.PrinterResolutionActual),
            SidesActual = map.MapFromDicSetNullable<Sides[]?>(src, JobAttribute.SidesActual),
            XImagePositionActual = map.MapFromDicSetNullable<XImagePosition[]?>(src, JobAttribute.XImagePositionActual),
            XImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.XImageShiftActual),
            XSide1ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.XSide1ImageShiftActual),
            XSide2ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.XSide2ImageShiftActual),
            YImagePositionActual = map.MapFromDicSetNullable<YImagePosition[]?>(src, JobAttribute.YImagePositionActual),
            YImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.YImageShiftActual),
            YSide1ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.YSide1ImageShiftActual),
            YSide2ImageShiftActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.YSide2ImageShiftActual),
            OverridesActual = null,
            FinishingsColActual = null,
            DateTimeAtCompletedEstimated = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtCompletedEstimated),
            DateTimeAtProcessingEstimated = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtProcessingEstimated),
            TimeAtCompletedEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCompletedEstimated),
            TimeAtProcessingEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtProcessingEstimated),
            OutputDeviceJobState = map.MapFromDicNullable<JobState?>(src, JobAttribute.OutputDeviceJobState),
            MaterialsColActual = null,
            };

            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.CoverBackActual, out var coverBackActual))
                dst.CoverBackActual = coverBackActual.GroupBegCollection().Select(x => map.Map<Cover>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.CoverFrontActual, out var coverFrontActual))
                dst.CoverFrontActual = coverFrontActual.GroupBegCollection().Select(x => map.Map<Cover>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.InsertSheetActual, out var insertSheetActual))
                dst.InsertSheetActual = insertSheetActual.GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.JobAccountingSheetsActual, out var jobAccountingSheetsActual))
                dst.JobAccountingSheetsActual = jobAccountingSheetsActual.GroupBegCollection().Select(x => map.Map<JobAccountingSheets>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.JobErrorSheetActual, out var jobErrorSheetActual))
                dst.JobErrorSheetActual = jobErrorSheetActual.GroupBegCollection().Select(x => map.Map<JobErrorSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.MediaColActual, out var mediaColActual))
                dst.MediaColActual = mediaColActual.GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.OverridesActual, out var overridesActual))
                dst.OverridesActual = overridesActual.GroupBegCollection().Select(x => map.Map<OverrideInstruction>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.FinishingsColActual, out var finishingsColActual))
                dst.FinishingsColActual = finishingsColActual.GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.MaterialsColActual, out var materialsColActual))
                dst.MaterialsColActual = materialsColActual.GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();

            if (src.TryGetValue(JobAttribute.SeparatorSheetsActual, out var separatorSheetsActual))
                dst.SeparatorSheetsActual = separatorSheetsActual.GroupBegCollection().Select(x => map.Map<SeparatorSheets>(x.FromBegCollection().ToIppDictionary())).ToArray();

            return dst;
        });

        mapper.CreateMap<JobDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.DateTimeAtCompleted != null)
                dic.Add(JobAttribute.DateTimeAtCompleted, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCompleted, src.DateTimeAtCompleted.Value)]);
            if (src.DateTimeAtCreation != null)
                dic.Add(JobAttribute.DateTimeAtCreation, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtProcessing != null)
                dic.Add(JobAttribute.DateTimeAtProcessing, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtProcessing, src.DateTimeAtProcessing.Value)]);
            if (src.JobId != null)
                dic.Add(JobAttribute.JobId, [new IppAttribute(Tag.Integer, JobAttribute.JobId, src.JobId.Value)]);
            if (src.JobUri != null)
                dic.Add(JobAttribute.JobUri, new IppAttribute[] { new IppAttribute(Tag.Uri, JobAttribute.JobUri, src.JobUri) });
            if (src.JobImpressionsCompleted != null)
                dic.Add(JobAttribute.JobImpressionsCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobImpressionsCompleted, src.JobImpressionsCompleted.Value)]);
            if (src.JobMediaSheetsCompleted != null)
                dic.Add(JobAttribute.JobMediaSheetsCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheetsCompleted, src.JobMediaSheetsCompleted.Value)]);
            if (src.JobOriginatingUserName != null)
                dic.Add(JobAttribute.JobOriginatingUserName, [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobOriginatingUserName, src.JobOriginatingUserName)]);
            if (src.JobPrinterUpTime != null)
                dic.Add(JobAttribute.JobPrinterUpTime, [new IppAttribute(Tag.Integer, JobAttribute.JobPrinterUpTime, src.JobPrinterUpTime.Value)]);
            if (src.JobPrinterUri != null)
                dic.Add(JobAttribute.JobPrinterUri, [new IppAttribute(Tag.Uri, JobAttribute.JobPrinterUri, src.JobPrinterUri)]);
            if (src.JobState != null)
                dic.Add(JobAttribute.JobState, [new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)src.JobState.Value)]);
            if (src.JobStateMessage != null)
                dic.Add(JobAttribute.JobStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, src.JobStateMessage)]);
            if (src.JobStateReasons != null)
                dic.Add(JobAttribute.JobStateReasons, src.JobStateReasons.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobStateReasons, map.Map<string>(x))).ToArray());
            if (src.JobResourceIds != null)
                dic.Add(JobAttribute.JobResourceIds, src.JobResourceIds.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobResourceIds, x)).ToArray());
            if (src.TimeAtCompleted != null)
                dic.Add(JobAttribute.TimeAtCompleted, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCompleted, src.TimeAtCompleted.Value)]);
            if (src.TimeAtCreation != null)
                dic.Add(JobAttribute.TimeAtCreation, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtProcessing != null)
                dic.Add(JobAttribute.TimeAtProcessing, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtProcessing, src.TimeAtProcessing.Value)]);
            if (src.JobName != null)
                dic.Add(JobAttribute.JobName, [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, src.JobName)]);
            if (src.JobKOctetsProcessed != null)
                dic.Add(JobAttribute.JobKOctetsProcessed, [new IppAttribute(Tag.Integer, JobAttribute.JobKOctetsProcessed, src.JobKOctetsProcessed.Value)]);
            if (src.JobImpressions != null)
                dic.Add(JobAttribute.JobImpressions, [new IppAttribute(Tag.Integer, JobAttribute.JobImpressions, src.JobImpressions.Value)]);
            if (src.JobImpressionsCol != null)
                dic.Add(JobAttribute.JobImpressionsCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCol).ToBegCollection(JobAttribute.JobImpressionsCol).ToArray());
            if (src.JobMediaSheets != null)
                dic.Add(JobAttribute.JobMediaSheets, [new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheets, src.JobMediaSheets.Value)]);
            if (src.JobMediaSheetsCol != null)
                dic.Add(JobAttribute.JobMediaSheetsCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCol).ToBegCollection(JobAttribute.JobMediaSheetsCol).ToArray());
            if (src.JobMoreInfo != null)
                dic.Add(JobAttribute.JobMoreInfo, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobMoreInfo, src.JobMoreInfo) });
            if (src.JobChargeInfo != null)
                dic.Add(JobAttribute.JobChargeInfo, [new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobChargeInfo, src.JobChargeInfo)]);
            if (src.DocumentFormatDetails != null)
                dic.Add(JobAttribute.DocumentFormatDetails, map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(JobAttribute.DocumentFormatDetails).ToArray());
            if (src.DocumentFormatDetailsDetected != null)
                dic.Add(JobAttribute.DocumentFormatDetailsDetected, map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetailsDetected).ToBegCollection(JobAttribute.DocumentFormatDetailsDetected).ToArray());
            if (src.NumberOfDocuments != null)
                dic.Add(JobAttribute.NumberOfDocuments, [new IppAttribute(Tag.Integer, JobAttribute.NumberOfDocuments, src.NumberOfDocuments.Value)]);
            if (src.NumberOfInterveningJobs != null)
                dic.Add(JobAttribute.NumberOfInterveningJobs, [new IppAttribute(Tag.Integer, JobAttribute.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value)]);
            if (src.OutputDeviceAssigned != null)
                dic.Add(JobAttribute.OutputDeviceAssigned, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputDeviceAssigned, src.OutputDeviceAssigned) });
            if (src.JobKOctets != null)
                dic.Add(JobAttribute.JobKOctets, [new IppAttribute(Tag.Integer, JobAttribute.JobKOctets, src.JobKOctets.Value)]);
            if (src.JobDetailedStatusMessages != null)
                dic.Add(JobAttribute.JobDetailedStatusMessages, src.JobDetailedStatusMessages.Select(x => new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobDetailedStatusMessages, x)).ToArray());
            if (src.JobDocumentAccessErrors != null)
                dic.Add(JobAttribute.JobDocumentAccessErrors, src.JobDocumentAccessErrors.Select(x => new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobDocumentAccessErrors, x)).ToArray());
            if (src.JobMessageFromOperator != null)
                dic.Add(JobAttribute.JobMessageFromOperator, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobMessageFromOperator, src.JobMessageFromOperator) });
            if (src.JobPages != null)
                dic.Add(JobAttribute.JobPages, [new IppAttribute(Tag.Integer, JobAttribute.JobPages, src.JobPages.Value)]);
            if (src.JobPagesCol != null)
                dic.Add(JobAttribute.JobPagesCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCol).ToBegCollection(JobAttribute.JobPagesCol).ToArray());
            if (src.JobPagesCompleted != null)
                dic.Add(JobAttribute.JobPagesCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobImpressionsCompletedCol != null)
                dic.Add(JobAttribute.JobImpressionsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCompletedCol).ToBegCollection(JobAttribute.JobImpressionsCompletedCol).ToArray());
            if (src.JobMediaSheetsCompletedCol != null)
                dic.Add(JobAttribute.JobMediaSheetsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCompletedCol).ToBegCollection(JobAttribute.JobMediaSheetsCompletedCol).ToArray());
            if (src.JobPagesCompletedCol != null)
                dic.Add(JobAttribute.JobPagesCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCompletedCol).ToBegCollection(JobAttribute.JobPagesCompletedCol).ToArray());
            if (src.JobProcessingTime != null)
                dic.Add(JobAttribute.JobProcessingTime, [new IppAttribute(Tag.Integer, JobAttribute.JobProcessingTime, src.JobProcessingTime.Value)]);
            if (src.ClientInfo != null)
                dic.Add(JobAttribute.ClientInfo, src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.ClientInfo)).ToArray());
            if (src.JobSheetsCol != null)
                dic.Add(JobAttribute.JobSheetsCol, map.Map<IEnumerable<IppAttribute>>(src.JobSheetsCol).ToBegCollection(JobAttribute.JobSheetsCol).ToArray());
            if (src.ErrorsCount != null)
                dic.Add(JobAttribute.ErrorsCount, [new IppAttribute(Tag.Integer, JobAttribute.ErrorsCount, src.ErrorsCount.Value)]);
            if (src.WarningsCount != null)
                dic.Add(JobAttribute.WarningsCount, [new IppAttribute(Tag.Integer, JobAttribute.WarningsCount, src.WarningsCount.Value)]);
            if (src.PrintContentOptimizeActual != null)
                dic.Add(JobAttribute.PrintContentOptimizeActual, src.PrintContentOptimizeActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.PrintContentOptimizeActual, map.Map<string>(x))).ToArray());
            if (src.CopiesActual != null)
                dic.Add(JobAttribute.CopiesActual, src.CopiesActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.CopiesActual, x)).ToArray());
            if (src.FinishingsActual != null)
                dic.Add(JobAttribute.FinishingsActual, src.FinishingsActual.Select(x => new IppAttribute(Tag.Enum, JobAttribute.FinishingsActual, (int)x)).ToArray());
            if (src.CoverBackActual != null)
                dic.Add(JobAttribute.CoverBackActual, src.CoverBackActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.CoverBackActual)).ToArray());
            if (src.CoverFrontActual != null)
                dic.Add(JobAttribute.CoverFrontActual, src.CoverFrontActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.CoverFrontActual)).ToArray());
            if (src.JobHoldUntilActual != null)
                dic.Add(JobAttribute.JobHoldUntilActual, src.JobHoldUntilActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobHoldUntilActual, map.Map<string>(x))).ToArray());
            if (src.JobPriorityActual != null)
                dic.Add(JobAttribute.JobPriorityActual, src.JobPriorityActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobPriorityActual, x)).ToArray());
            if (src.JobSheetsActual != null)
                dic.Add(JobAttribute.JobSheetsActual, src.JobSheetsActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobSheetsActual, map.Map<string>(x))).ToArray());
            if (src.MediaActual != null)
                dic.Add(JobAttribute.MediaActual, src.MediaActual.Select(x =>
                {
                    var mediaTag = x.ToIppTag();
                    return new IppAttribute(mediaTag, JobAttribute.MediaActual, x);
                }).ToArray());
            if (src.ImpositionTemplateActual != null)
                dic.Add(JobAttribute.ImpositionTemplateActual, src.ImpositionTemplateActual.Select(x =>
                {
                    var impositionTemplate = map.Map<string>(x);
                    var impositionTemplateTag = x.ToIppTag();
                    return new IppAttribute(impositionTemplateTag, JobAttribute.ImpositionTemplateActual, impositionTemplate);
                }).ToArray());
            if (src.InsertSheetActual != null)
                dic.Add(JobAttribute.InsertSheetActual, src.InsertSheetActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.InsertSheetActual)).ToArray());
            if (src.JobAccountIdActual != null)
                dic.Add(JobAttribute.JobAccountIdActual, src.JobAccountIdActual.Select(x => new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobAccountIdActual, x)).ToArray());
            if (src.JobAccountingSheetsActual != null)
                dic.Add(JobAttribute.JobAccountingSheetsActual, src.JobAccountingSheetsActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.JobAccountingSheetsActual)).ToArray());
            if (src.JobAccountingUserIdActual != null)
                dic.Add(JobAttribute.JobAccountingUserIdActual, src.JobAccountingUserIdActual.Select(x => new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobAccountingUserIdActual, x)).ToArray());
            if (src.JobErrorSheetActual != null)
                dic.Add(JobAttribute.JobErrorSheetActual, src.JobErrorSheetActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.JobErrorSheetActual)).ToArray());
            if (src.JobMessageToOperatorActual != null)
                dic.Add(JobAttribute.JobMessageToOperatorActual, src.JobMessageToOperatorActual.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobMessageToOperatorActual, x)).ToArray());
            if (src.JobSheetMessageActual != null)
                dic.Add(JobAttribute.JobSheetMessageActual, src.JobSheetMessageActual.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobSheetMessageActual, x)).ToArray());
            if (src.MediaColActual != null)
                dic.Add(JobAttribute.MediaColActual, src.MediaColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.MediaColActual)).ToArray());
            if (src.MediaInputTrayCheckActual != null)
                dic.Add(JobAttribute.MediaInputTrayCheckActual, src.MediaInputTrayCheckActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.MediaInputTrayCheckActual, map.Map<string>(x))).ToArray());
            if (src.MultipleDocumentHandlingActual != null)
                dic.Add(JobAttribute.MultipleDocumentHandlingActual, src.MultipleDocumentHandlingActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.MultipleDocumentHandlingActual, map.Map<string>(x))).ToArray());
            if (src.NumberUpActual != null)
                dic.Add(JobAttribute.NumberUpActual, src.NumberUpActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.NumberUpActual, x)).ToArray());
            if (src.OrientationRequestedActual != null)
                dic.Add(JobAttribute.OrientationRequestedActual, src.OrientationRequestedActual.Select(x => new IppAttribute(Tag.Enum, JobAttribute.OrientationRequestedActual, (int)x)).ToArray());
            if (src.OutputBinActual != null)
                dic.Add(JobAttribute.OutputBinActual, src.OutputBinActual.Select(x =>
                {
                    var outputBin = map.Map<string>(x);
                    var outputBinTag = x.ToIppTag();
                    return new IppAttribute(outputBinTag, JobAttribute.OutputBinActual, outputBin);
                }).ToArray());
            if (src.PageDeliveryActual != null)
                dic.Add(JobAttribute.PageDeliveryActual, src.PageDeliveryActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.PageDeliveryActual, map.Map<string>(x))).ToArray());
            if (src.PageOrderReceivedActual != null)
                dic.Add(JobAttribute.PageOrderReceivedActual, src.PageOrderReceivedActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.PageOrderReceivedActual, map.Map<string>(x))).ToArray());
            if (src.PageRangesActual != null)
                dic.Add(JobAttribute.PageRangesActual, src.PageRangesActual.Select(x => new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRangesActual, x)).ToArray());
            if (src.PresentationDirectionNumberUpActual != null)
                dic.Add(JobAttribute.PresentationDirectionNumberUpActual, src.PresentationDirectionNumberUpActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.PresentationDirectionNumberUpActual, map.Map<string>(x))).ToArray());
            if (src.PrintQualityActual != null)
                dic.Add(JobAttribute.PrintQualityActual, src.PrintQualityActual.Select(x => new IppAttribute(Tag.Enum, JobAttribute.PrintQualityActual, (int)x)).ToArray());
            if (src.PrinterResolutionActual != null)
                dic.Add(JobAttribute.PrinterResolutionActual, src.PrinterResolutionActual.Select(x => new IppAttribute(Tag.Resolution, JobAttribute.PrinterResolutionActual, x)).ToArray());
            if (src.SidesActual != null)
                dic.Add(JobAttribute.SidesActual, src.SidesActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.SidesActual, map.Map<string>(x))).ToArray());
            if (src.SeparatorSheetsActual != null)
                dic.Add(JobAttribute.SeparatorSheetsActual, src.SeparatorSheetsActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.SeparatorSheetsActual)).ToArray());
            if (src.XImagePositionActual != null)
                dic.Add(JobAttribute.XImagePositionActual, src.XImagePositionActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.XImagePositionActual, map.Map<string>(x))).ToArray());
            if (src.XImageShiftActual != null)
                dic.Add(JobAttribute.XImageShiftActual, src.XImageShiftActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.XImageShiftActual, x)).ToArray());
            if (src.XSide1ImageShiftActual != null)
                dic.Add(JobAttribute.XSide1ImageShiftActual, src.XSide1ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.XSide1ImageShiftActual, x)).ToArray());
            if (src.XSide2ImageShiftActual != null)
                dic.Add(JobAttribute.XSide2ImageShiftActual, src.XSide2ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.XSide2ImageShiftActual, x)).ToArray());
            if (src.YImagePositionActual != null)
                dic.Add(JobAttribute.YImagePositionActual, src.YImagePositionActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.YImagePositionActual, map.Map<string>(x))).ToArray());
            if (src.YImageShiftActual != null)
                dic.Add(JobAttribute.YImageShiftActual, src.YImageShiftActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.YImageShiftActual, x)).ToArray());
            if (src.YSide1ImageShiftActual != null)
                dic.Add(JobAttribute.YSide1ImageShiftActual, src.YSide1ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.YSide1ImageShiftActual, x)).ToArray());
            if (src.YSide2ImageShiftActual != null)
                dic.Add(JobAttribute.YSide2ImageShiftActual, src.YSide2ImageShiftActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.YSide2ImageShiftActual, x)).ToArray());
            if (src.OverridesActual != null)
                dic.Add(JobAttribute.OverridesActual, src.OverridesActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.OverridesActual)).ToArray());
            if (src.FinishingsColActual != null)
                dic.Add(JobAttribute.FinishingsColActual, src.FinishingsColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.FinishingsColActual)).ToArray());
            if (src.DateTimeAtCompletedEstimated != null)
                dic.Add(JobAttribute.DateTimeAtCompletedEstimated, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCompletedEstimated, src.DateTimeAtCompletedEstimated.Value)]);
            if (src.DateTimeAtProcessingEstimated != null)
                dic.Add(JobAttribute.DateTimeAtProcessingEstimated, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtProcessingEstimated, src.DateTimeAtProcessingEstimated.Value)]);
            if (src.TimeAtCompletedEstimated != null)
                dic.Add(JobAttribute.TimeAtCompletedEstimated, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCompletedEstimated, src.TimeAtCompletedEstimated.Value)]);
            if (src.TimeAtProcessingEstimated != null)
                dic.Add(JobAttribute.TimeAtProcessingEstimated, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtProcessingEstimated, src.TimeAtProcessingEstimated.Value)]);
            if (src.OutputDeviceJobState != null)
                dic.Add(JobAttribute.OutputDeviceJobState, [new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobState, (int)src.OutputDeviceJobState.Value)]);
            if (src.MaterialsColActual != null)
                dic.Add(JobAttribute.MaterialsColActual, src.MaterialsColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.MaterialsColActual)).ToArray());
            return dic;
        });
    }
}
