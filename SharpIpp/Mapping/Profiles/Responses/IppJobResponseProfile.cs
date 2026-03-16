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
internal class IppJobResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, IIppJobResponse>((src, dst, map) =>
        {
            dst = dst ?? throw new ArgumentNullException(nameof(dst));
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            var jobAttrs = new JobAttributes();
            map.Map(src.JobAttributes.SelectMany(x => x).ToIppDictionary(), jobAttrs);
            dst.JobAttributes = jobAttrs;
            return dst;
        });

        mapper.CreateMap<IIppJobResponse, IppResponseMessage>((src, dst, map) =>
        {
            dst ??= new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.JobAttributes != null)
            {
                var jobAttrs = new List<IppAttribute>();
                jobAttrs.AddRange(map.Map<JobAttributes, IDictionary<string, IppAttribute[]>>(src.JobAttributes).Values.SelectMany(x => x));
                dst.JobAttributes.Add(jobAttrs);
            }
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobAttributes>((
            src,
            dst,
            map) =>
        {
            dst ??= new JobAttributes();
            dst.JobUri = map.MapFromDicNullable<string?>(src, JobAttribute.JobUri);
            dst.JobId = map.MapFromDic<int>(src, JobAttribute.JobId);
            dst.JobState = map.MapFromDic<JobState>(src, JobAttribute.JobState);
            dst.JobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, JobAttribute.JobStateReasons);
            dst.JobStateMessage = map.MapFromDicNullable<string?>(src, JobAttribute.JobStateMessage);
            dst.NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, JobAttribute.NumberOfInterveningJobs);
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(JobAttribute.JobImpressionsCompletedCol))
                dst.JobImpressionsCompletedCol = map.Map<JobCounter>(src[JobAttribute.JobImpressionsCompletedCol].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(JobAttribute.JobMediaSheetsCompletedCol))
                dst.JobMediaSheetsCompletedCol = map.Map<JobCounter>(src[JobAttribute.JobMediaSheetsCompletedCol].FromBegCollection().ToIppDictionary());
            dst.JobPagesCompleted = map.MapFromDicNullable<int?>(src, JobAttribute.JobPagesCompleted);
            if (src.ContainsKey(JobAttribute.JobPagesCompletedCol))
                dst.JobPagesCompletedCol = map.Map<JobCounter>(src[JobAttribute.JobPagesCompletedCol].FromBegCollection().ToIppDictionary());
            dst.JobProcessingTime = map.MapFromDicNullable<int?>(src, JobAttribute.JobProcessingTime);
            return dst;
        });

        mapper.CreateMap<JobAttributes, IDictionary<string, IppAttribute[]>>((
            src,
            dst,
            map) =>
        {
            // dst is ignored — always creates a new dictionary
            var dic = new Dictionary<string, IppAttribute[]>
            {
                { JobAttribute.JobId, [new IppAttribute(Tag.Integer, JobAttribute.JobId, src.JobId )] },
                { JobAttribute.JobState, new IppAttribute[] { new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)src.JobState ) } }
            };
            if (src.JobUri != null)
                dic.Add(JobAttribute.JobUri, new IppAttribute[] { new IppAttribute(Tag.Uri, JobAttribute.JobUri, src.JobUri) });
            if (src.JobStateReasons != null)
                dic.Add(JobAttribute.JobStateReasons, src.JobStateReasons.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobStateReasons, map.Map<string>(x))).ToArray());
            if (src.JobStateMessage != null)
                dic.Add(JobAttribute.JobStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, src.JobStateMessage) });
            if (src.NumberOfInterveningJobs != null)
                dic.Add(JobAttribute.NumberOfInterveningJobs, [new IppAttribute(Tag.Integer, JobAttribute.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value)]);
            if (src.ClientInfo != null)
                dic.Add(JobAttribute.ClientInfo, src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.ClientInfo)).ToArray());
            if (src.JobImpressionsCompletedCol != null)
                dic.Add(JobAttribute.JobImpressionsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCompletedCol).ToBegCollection(JobAttribute.JobImpressionsCompletedCol).ToArray());
            if (src.JobMediaSheetsCompletedCol != null)
                dic.Add(JobAttribute.JobMediaSheetsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCompletedCol).ToBegCollection(JobAttribute.JobMediaSheetsCompletedCol).ToArray());
            if (src.JobPagesCompleted != null)
                dic.Add(JobAttribute.JobPagesCompleted, [new IppAttribute(Tag.Integer, JobAttribute.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobPagesCompletedCol != null)
                dic.Add(JobAttribute.JobPagesCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCompletedCol).ToBegCollection(JobAttribute.JobPagesCompletedCol).ToArray());
            if (src.JobProcessingTime != null)
                dic.Add(JobAttribute.JobProcessingTime, [new IppAttribute(Tag.Integer, JobAttribute.JobProcessingTime, src.JobProcessingTime.Value)]);
            return dic;
        });
    }
}
