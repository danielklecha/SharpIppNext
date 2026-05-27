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
                JobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobId),
                JobUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobUri),
                JobPrinterUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobPrinterUri),
                JobName = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobName),
                JobOriginatingUserName = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobOriginatingUserName),
                JobState = map.MapFromDicNullable<JobState?>(src, IppAttributeNames.JobState),
                JobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, IppAttributeNames.JobStateReasons),
                JobStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobStateMessage),
                JobImpressionsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobImpressionsCompleted),
                JobMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobMediaSheetsCompleted),
                JobKOctetsProcessed = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobKOctetsProcessed),
                JobPagesCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPagesCompleted),
                JobPagesCompletedCol = src.ContainsKey(IppAttributeNames.JobPagesCompletedCol)
                    ? map.Map<JobCounter>(src[IppAttributeNames.JobPagesCompletedCol].FromBegCollection().ToIppDictionary())
                    : null,
                JobImpressionsCompletedCol = src.ContainsKey(IppAttributeNames.JobImpressionsCompletedCol)
                    ? map.Map<JobCounter>(src[IppAttributeNames.JobImpressionsCompletedCol].FromBegCollection().ToIppDictionary())
                    : null,
                JobMediaSheetsCompletedCol = src.ContainsKey(IppAttributeNames.JobMediaSheetsCompletedCol)
                    ? map.Map<JobCounter>(src[IppAttributeNames.JobMediaSheetsCompletedCol].FromBegCollection().ToIppDictionary())
                    : null,
                JobProcessingTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobProcessingTime),
                ErrorsCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.ErrorsCount),
                WarningsCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.WarningsCount),
                NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberOfInterveningJobs),
                OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, IppAttributeNames.OutputDeviceAssigned),
                JobPrinterUpTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPrinterUpTime),
                TimeAtCreation = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCreation),
                TimeAtProcessing = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtProcessing),
                TimeAtCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCompleted),
                DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCreation),
                DateTimeAtProcessing = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtProcessing),
                DateTimeAtCompleted = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCompleted),
            };
            return dst;
        });

        mapper.CreateMap<JobStatusAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.JobId != null)
                dic.Add(IppAttributeNames.JobId, [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId.Value)]);
            if (src.JobUri != null)
                dic.Add(IppAttributeNames.JobUri, [new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, src.JobUri.ToString())]);
            if (src.JobPrinterUri != null)
                dic.Add(IppAttributeNames.JobPrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.JobPrinterUri, src.JobPrinterUri.ToString())]);
            if (src.JobName != null)
                dic.Add(IppAttributeNames.JobName, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobName, src.JobName)]);
            if (src.JobOriginatingUserName != null)
                dic.Add(IppAttributeNames.JobOriginatingUserName, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobOriginatingUserName, src.JobOriginatingUserName)]);
            if (src.JobState != null)
                dic.Add(IppAttributeNames.JobState, [new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)src.JobState.Value)]);
            if (src.JobStateReasons != null)
                dic.Add(IppAttributeNames.JobStateReasons, src.JobStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, map.Map<string>(x))).ToArray());
            if (src.JobStateMessage != null)
                dic.Add(IppAttributeNames.JobStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobStateMessage, src.JobStateMessage)]);
            if (src.JobImpressionsCompleted != null)
                dic.Add(IppAttributeNames.JobImpressionsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobImpressionsCompleted, src.JobImpressionsCompleted.Value)]);
            if (src.JobMediaSheetsCompleted != null)
                dic.Add(IppAttributeNames.JobMediaSheetsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobMediaSheetsCompleted, src.JobMediaSheetsCompleted.Value)]);
            if (src.JobKOctetsProcessed != null)
                dic.Add(IppAttributeNames.JobKOctetsProcessed, [new IppAttribute(Tag.Integer, IppAttributeNames.JobKOctetsProcessed, src.JobKOctetsProcessed.Value)]);
            if (src.JobPagesCompleted != null)
                dic.Add(IppAttributeNames.JobPagesCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobPagesCompletedCol != null)
                dic.Add(IppAttributeNames.JobPagesCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCompletedCol).ToBegCollection(IppAttributeNames.JobPagesCompletedCol).ToArray());
            if (src.JobImpressionsCompletedCol != null)
                dic.Add(IppAttributeNames.JobImpressionsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCompletedCol).ToBegCollection(IppAttributeNames.JobImpressionsCompletedCol).ToArray());
            if (src.JobMediaSheetsCompletedCol != null)
                dic.Add(IppAttributeNames.JobMediaSheetsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCompletedCol).ToBegCollection(IppAttributeNames.JobMediaSheetsCompletedCol).ToArray());
            if (src.JobProcessingTime != null)
                dic.Add(IppAttributeNames.JobProcessingTime, [new IppAttribute(Tag.Integer, IppAttributeNames.JobProcessingTime, src.JobProcessingTime.Value)]);
            if (src.ErrorsCount != null)
                dic.Add(IppAttributeNames.ErrorsCount, [new IppAttribute(Tag.Integer, IppAttributeNames.ErrorsCount, src.ErrorsCount.Value)]);
            if (src.WarningsCount != null)
                dic.Add(IppAttributeNames.WarningsCount, [new IppAttribute(Tag.Integer, IppAttributeNames.WarningsCount, src.WarningsCount.Value)]);
            if (src.NumberOfInterveningJobs != null)
                dic.Add(IppAttributeNames.NumberOfInterveningJobs, [new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value)]);
            if (src.OutputDeviceAssigned != null)
                dic.Add(IppAttributeNames.OutputDeviceAssigned, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputDeviceAssigned, src.OutputDeviceAssigned)]);
            if (src.JobPrinterUpTime != null)
                dic.Add(IppAttributeNames.JobPrinterUpTime, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPrinterUpTime, src.JobPrinterUpTime.Value)]);
            if (src.TimeAtCreation != null)
                dic.Add(IppAttributeNames.TimeAtCreation, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtProcessing != null)
                dic.Add(IppAttributeNames.TimeAtProcessing, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtProcessing, src.TimeAtProcessing.Value)]);
            if (src.TimeAtCompleted != null)
                dic.Add(IppAttributeNames.TimeAtCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCompleted, src.TimeAtCompleted.Value)]);
            if (src.DateTimeAtCreation != null)
                dic.Add(IppAttributeNames.DateTimeAtCreation, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtProcessing != null)
                dic.Add(IppAttributeNames.DateTimeAtProcessing, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtProcessing, src.DateTimeAtProcessing.Value)]);
            if (src.DateTimeAtCompleted != null)
                dic.Add(IppAttributeNames.DateTimeAtCompleted, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCompleted, src.DateTimeAtCompleted.Value)]);
            return dic;
        });
    }
}
