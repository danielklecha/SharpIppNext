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

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobDescriptionAttributes>((src, map) => new JobDescriptionAttributes
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
            TimeAtCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCompleted),
            TimeAtCreation = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCreation),
            TimeAtProcessing = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtProcessing),
            JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName),
            JobKOctetsProcessed = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctetsProcessed),
            JobImpressions = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressions),
            JobMediaSheets = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheets),
            JobMoreInfo = map.MapFromDicNullable<string?>(src, JobAttribute.JobMoreInfo),
            NumberOfDocuments = map.MapFromDicNullable<int?>(src, JobAttribute.NumberOfDocuments),
            NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, JobAttribute.NumberOfInterveningJobs),
            OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, JobAttribute.OutputDeviceAssigned),
            JobKOctets = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctets),
            JobDetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobDetailedStatusMessages),
            JobDocumentAccessErrors = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobDocumentAccessErrors),
            JobMessageFromOperator = map.MapFromDicNullable<string?>(src, JobAttribute.JobMessageFromOperator),
            JobPages = map.MapFromDicNullable<int?>(src, JobAttribute.JobPages),
            JobPagesCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobPagesCompleted),
            JobProcessingTime = map.MapFromDicNullable<int?>(src, JobAttribute.JobProcessingTime),
            ErrorsCount = map.MapFromDicNullable<int?>(src, JobAttribute.ErrorsCount),
            WarningsCount = map.MapFromDicNullable<int?>(src, JobAttribute.WarningsCount),
            PrintContentOptimizeActual = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, JobAttribute.PrintContentOptimizeActual),
            CopiesActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.CopiesActual),
            FinishingsActual = map.MapFromDicSetNullable<Finishings[]?>(src, JobAttribute.FinishingsActual),
            JobHoldUntilActual = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, JobAttribute.JobHoldUntilActual),
            JobPriorityActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobPriorityActual),
            JobSheetsActual = map.MapFromDicSetNullable<JobSheets[]?>(src, JobAttribute.JobSheetsActual),
            MediaActual = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.MediaActual),
            MediaColActual = src.TryGetValue(JobAttribute.MediaColActual, out var mediaColActual) && mediaColActual.GroupBegCollection().Any()
                ? mediaColActual.GroupBegCollection().Select(x => map.Map<MediaCol>(x.FromBegCollection().ToIppDictionary())).ToArray()
                : null,
            MultipleDocumentHandlingActual = map.MapFromDicSetNullable<MultipleDocumentHandling[]?>(src, JobAttribute.MultipleDocumentHandlingActual),
            NumberUpActual = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.NumberUpActual),
            OrientationRequestedActual = map.MapFromDicSetNullable<Orientation[]?>(src, JobAttribute.OrientationRequestedActual),
            OutputBinActual = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.OutputBinActual),
            PageRangesActual = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, JobAttribute.PageRangesActual),
            PrintQualityActual = map.MapFromDicSetNullable<PrintQuality[]?>(src, JobAttribute.PrintQualityActual),
            PrinterResolutionActual = map.MapFromDicSetNullable<Resolution[]?>(src, JobAttribute.PrinterResolutionActual),
            SidesActual = map.MapFromDicSetNullable<Sides[]?>(src, JobAttribute.SidesActual),
            FinishingsColActual = src.TryGetValue(JobAttribute.FinishingsColActual, out var finishingsColActual) && finishingsColActual.GroupBegCollection().Any()
                ? finishingsColActual.GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray()
                : null,
            DateTimeAtCompletedEstimated = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtCompletedEstimated),
            DateTimeAtProcessingEstimated = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtProcessingEstimated),
            TimeAtCompletedEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCompletedEstimated),
            TimeAtProcessingEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtProcessingEstimated),
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
            if (src.JobMediaSheets != null)
                dic.Add(JobAttribute.JobMediaSheets, [new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheets, src.JobMediaSheets.Value)]);
            if (src.JobMoreInfo != null)
                dic.Add(JobAttribute.JobMoreInfo, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobMoreInfo, src.JobMoreInfo) });
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
            if (src.JobPagesCompleted != null)
                dic.Add(JobAttribute.JobPagesCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobProcessingTime != null)
                dic.Add(JobAttribute.JobProcessingTime, [new IppAttribute(Tag.Integer, JobAttribute.JobProcessingTime, src.JobProcessingTime.Value)]);
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
            if (src.JobHoldUntilActual != null)
                dic.Add(JobAttribute.JobHoldUntilActual, src.JobHoldUntilActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobHoldUntilActual, map.Map<string>(x))).ToArray());
            if (src.JobPriorityActual != null)
                dic.Add(JobAttribute.JobPriorityActual, src.JobPriorityActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobPriorityActual, x)).ToArray());
            if (src.JobSheetsActual != null)
                dic.Add(JobAttribute.JobSheetsActual, src.JobSheetsActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobSheetsActual, map.Map<string>(x))).ToArray());
            if (src.MediaActual != null)
                dic.Add(JobAttribute.MediaActual, src.MediaActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.MediaActual, x)).ToArray());
            if (src.MediaColActual != null)
                dic.Add(JobAttribute.MediaColActual, src.MediaColActual.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.MediaColActual)).ToArray());
            if (src.MultipleDocumentHandlingActual != null)
                dic.Add(JobAttribute.MultipleDocumentHandlingActual, src.MultipleDocumentHandlingActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.MultipleDocumentHandlingActual, map.Map<string>(x))).ToArray());
            if (src.NumberUpActual != null)
                dic.Add(JobAttribute.NumberUpActual, src.NumberUpActual.Select(x => new IppAttribute(Tag.Integer, JobAttribute.NumberUpActual, x)).ToArray());
            if (src.OrientationRequestedActual != null)
                dic.Add(JobAttribute.OrientationRequestedActual, src.OrientationRequestedActual.Select(x => new IppAttribute(Tag.Enum, JobAttribute.OrientationRequestedActual, (int)x)).ToArray());
            if (src.OutputBinActual != null)
                dic.Add(JobAttribute.OutputBinActual, src.OutputBinActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.OutputBinActual, x)).ToArray());
            if (src.PageRangesActual != null)
                dic.Add(JobAttribute.PageRangesActual, src.PageRangesActual.Select(x => new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRangesActual, x)).ToArray());
            if (src.PrintQualityActual != null)
                dic.Add(JobAttribute.PrintQualityActual, src.PrintQualityActual.Select(x => new IppAttribute(Tag.Enum, JobAttribute.PrintQualityActual, (int)x)).ToArray());
            if (src.PrinterResolutionActual != null)
                dic.Add(JobAttribute.PrinterResolutionActual, src.PrinterResolutionActual.Select(x => new IppAttribute(Tag.Resolution, JobAttribute.PrinterResolutionActual, x)).ToArray());
            if (src.SidesActual != null)
                dic.Add(JobAttribute.SidesActual, src.SidesActual.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.SidesActual, map.Map<string>(x))).ToArray());
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
            return dic;
        });
    }
}
