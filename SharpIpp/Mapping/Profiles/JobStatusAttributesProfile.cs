using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class JobStatusAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobStatusAttributes>((src, map) =>
        {
            var dst = new JobStatusAttributes
            {
                JobId = map.MapFromDicNullable<int?>(src, JobAttribute.JobId),
                JobUri = map.MapFromDicNullable<string?>(src, JobAttribute.JobUri),
                JobPrinterUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.JobPrinterUri),
                JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName),
                JobOriginatingUserName = map.MapFromDicNullable<string?>(src, JobAttribute.JobOriginatingUserName),
                JobState = map.MapFromDicNullable<JobState?>(src, JobAttribute.JobState),
                JobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, JobAttribute.JobStateReasons),
                JobStateMessage = map.MapFromDicNullable<string?>(src, JobAttribute.JobStateMessage),
                JobImpressionsCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressionsCompleted),
                JobMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheetsCompleted),
                JobKOctetsProcessed = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctetsProcessed),
                JobPagesCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobPagesCompleted),
                JobPagesCompletedCol = src.ContainsKey(JobAttribute.JobPagesCompletedCol)
                    ? map.Map<JobCounter>(src[JobAttribute.JobPagesCompletedCol].FromBegCollection().ToIppDictionary())
                    : null,
                JobImpressionsCompletedCol = src.ContainsKey(JobAttribute.JobImpressionsCompletedCol)
                    ? map.Map<JobCounter>(src[JobAttribute.JobImpressionsCompletedCol].FromBegCollection().ToIppDictionary())
                    : null,
                JobMediaSheetsCompletedCol = src.ContainsKey(JobAttribute.JobMediaSheetsCompletedCol)
                    ? map.Map<JobCounter>(src[JobAttribute.JobMediaSheetsCompletedCol].FromBegCollection().ToIppDictionary())
                    : null,
                JobProcessingTime = map.MapFromDicNullable<int?>(src, JobAttribute.JobProcessingTime),
                ErrorsCount = map.MapFromDicNullable<int?>(src, JobAttribute.ErrorsCount),
                WarningsCount = map.MapFromDicNullable<int?>(src, JobAttribute.WarningsCount),
                NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, JobAttribute.NumberOfInterveningJobs),
                OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, JobAttribute.OutputDeviceAssigned),
                JobPrinterUpTime = map.MapFromDicNullable<int?>(src, JobAttribute.JobPrinterUpTime),
                TimeAtCreation = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCreation),
                TimeAtProcessing = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtProcessing),
                TimeAtCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.TimeAtCompleted),
                DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtCreation),
                DateTimeAtProcessing = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtProcessing),
                DateTimeAtCompleted = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.DateTimeAtCompleted),
            };
            return dst;
        });

        mapper.CreateMap<JobStatusAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.JobId != null)
                dic.Add(JobAttribute.JobId, [new IppAttribute(Tag.Integer, JobAttribute.JobId, src.JobId.Value)]);
            if (src.JobUri != null)
                dic.Add(JobAttribute.JobUri, [new IppAttribute(Tag.Uri, JobAttribute.JobUri, src.JobUri)]);
            if (src.JobPrinterUri != null)
                dic.Add(JobAttribute.JobPrinterUri, [new IppAttribute(Tag.Uri, JobAttribute.JobPrinterUri, src.JobPrinterUri.ToString())]);
            if (src.JobName != null)
                dic.Add(JobAttribute.JobName, [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, src.JobName)]);
            if (src.JobOriginatingUserName != null)
                dic.Add(JobAttribute.JobOriginatingUserName, [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobOriginatingUserName, src.JobOriginatingUserName)]);
            if (src.JobState != null)
                dic.Add(JobAttribute.JobState, [new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)src.JobState.Value)]);
            if (src.JobStateReasons != null)
                dic.Add(JobAttribute.JobStateReasons, src.JobStateReasons.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobStateReasons, map.Map<string>(x))).ToArray());
            if (src.JobStateMessage != null)
                dic.Add(JobAttribute.JobStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, src.JobStateMessage)]);
            if (src.JobImpressionsCompleted != null)
                dic.Add(JobAttribute.JobImpressionsCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobImpressionsCompleted, src.JobImpressionsCompleted.Value)]);
            if (src.JobMediaSheetsCompleted != null)
                dic.Add(JobAttribute.JobMediaSheetsCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheetsCompleted, src.JobMediaSheetsCompleted.Value)]);
            if (src.JobKOctetsProcessed != null)
                dic.Add(JobAttribute.JobKOctetsProcessed, [new IppAttribute(Tag.Integer, JobAttribute.JobKOctetsProcessed, src.JobKOctetsProcessed.Value)]);
            if (src.JobPagesCompleted != null)
                dic.Add(JobAttribute.JobPagesCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobPagesCompletedCol != null)
                dic.Add(JobAttribute.JobPagesCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCompletedCol).ToBegCollection(JobAttribute.JobPagesCompletedCol).ToArray());
            if (src.JobImpressionsCompletedCol != null)
                dic.Add(JobAttribute.JobImpressionsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCompletedCol).ToBegCollection(JobAttribute.JobImpressionsCompletedCol).ToArray());
            if (src.JobMediaSheetsCompletedCol != null)
                dic.Add(JobAttribute.JobMediaSheetsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCompletedCol).ToBegCollection(JobAttribute.JobMediaSheetsCompletedCol).ToArray());
            if (src.JobProcessingTime != null)
                dic.Add(JobAttribute.JobProcessingTime, [new IppAttribute(Tag.Integer, JobAttribute.JobProcessingTime, src.JobProcessingTime.Value)]);
            if (src.ErrorsCount != null)
                dic.Add(JobAttribute.ErrorsCount, [new IppAttribute(Tag.Integer, JobAttribute.ErrorsCount, src.ErrorsCount.Value)]);
            if (src.WarningsCount != null)
                dic.Add(JobAttribute.WarningsCount, [new IppAttribute(Tag.Integer, JobAttribute.WarningsCount, src.WarningsCount.Value)]);
            if (src.NumberOfInterveningJobs != null)
                dic.Add(JobAttribute.NumberOfInterveningJobs, [new IppAttribute(Tag.Integer, JobAttribute.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value)]);
            if (src.OutputDeviceAssigned != null)
                dic.Add(JobAttribute.OutputDeviceAssigned, [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputDeviceAssigned, src.OutputDeviceAssigned)]);
            if (src.JobPrinterUpTime != null)
                dic.Add(JobAttribute.JobPrinterUpTime, [new IppAttribute(Tag.Integer, JobAttribute.JobPrinterUpTime, src.JobPrinterUpTime.Value)]);
            if (src.TimeAtCreation != null)
                dic.Add(JobAttribute.TimeAtCreation, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtProcessing != null)
                dic.Add(JobAttribute.TimeAtProcessing, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtProcessing, src.TimeAtProcessing.Value)]);
            if (src.TimeAtCompleted != null)
                dic.Add(JobAttribute.TimeAtCompleted, [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCompleted, src.TimeAtCompleted.Value)]);
            if (src.DateTimeAtCreation != null)
                dic.Add(JobAttribute.DateTimeAtCreation, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtProcessing != null)
                dic.Add(JobAttribute.DateTimeAtProcessing, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtProcessing, src.DateTimeAtProcessing.Value)]);
            if (src.DateTimeAtCompleted != null)
                dic.Add(JobAttribute.DateTimeAtCompleted, [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCompleted, src.DateTimeAtCompleted.Value)]);
            return dic;
        });
    }
}
