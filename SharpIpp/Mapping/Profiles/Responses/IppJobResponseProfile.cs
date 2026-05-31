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
            if(src.JobAttributes.Count > 0)
            {
                var jobAttrs = new JobAttributes();
                map.Map(src.JobAttributes.SelectMany(x => x).ToIppDictionary(), jobAttrs);
                dst.JobAttributes = jobAttrs;
            }
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
            dst.JobUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobUri);
            dst.JobId = map.MapFromDic<int>(src, IppAttributeNames.JobId);
            dst.JobState = map.MapFromDic<JobState>(src, IppAttributeNames.JobState);
            dst.JobStateReasons = map.MapFromDicSetNullable<JobStateReason[]?>(src, IppAttributeNames.JobStateReasons);
            dst.JobStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobStateMessage);
            dst.NumberOfInterveningJobs = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberOfInterveningJobs);
            if (src.TryGetValue(IppAttributeNames.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.JobImpressionsCompletedCol))
                dst.JobImpressionsCompletedCol = map.Map<JobCounter>(src[IppAttributeNames.JobImpressionsCompletedCol].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(IppAttributeNames.JobMediaSheetsCompletedCol))
                dst.JobMediaSheetsCompletedCol = map.Map<JobCounter>(src[IppAttributeNames.JobMediaSheetsCompletedCol].FromBegCollection().ToIppDictionary());
            dst.JobPagesCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobPagesCompleted);
            if (src.ContainsKey(IppAttributeNames.JobPagesCompletedCol))
                dst.JobPagesCompletedCol = map.Map<JobCounter>(src[IppAttributeNames.JobPagesCompletedCol].FromBegCollection().ToIppDictionary());
            dst.JobProcessingTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobProcessingTime);
            dst.PlatformTemperatureActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.PlatformTemperatureActual);
            dst.ChamberHumidityActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ChamberHumidityActual);
            dst.ChamberTemperatureActual = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ChamberTemperatureActual);
            dst.ChamberHumidityCurrent = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberHumidityCurrent);
            dst.ChamberTemperatureCurrent = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberTemperatureCurrent);
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
                { IppAttributeNames.JobId, [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId )] },
                { IppAttributeNames.JobState, new IppAttribute[] { new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)src.JobState ) } }
            };
            if (src.JobUri != null)
                dic.Add(IppAttributeNames.JobUri, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, src.JobUri.ToString()) });
            if (src.JobStateReasons != null)
                dic.Add(IppAttributeNames.JobStateReasons, src.JobStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, map.Map<string>(x))).ToArray());
            if (src.JobStateMessage != null)
                dic.Add(IppAttributeNames.JobStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobStateMessage, src.JobStateMessage) });
            if (src.NumberOfInterveningJobs != null)
                dic.Add(IppAttributeNames.NumberOfInterveningJobs, [new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value)]);
            if (src.ClientInfo != null)
                dic.Add(IppAttributeNames.ClientInfo, src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.ClientInfo)).ToArray());
            if (src.JobImpressionsCompletedCol != null)
                dic.Add(IppAttributeNames.JobImpressionsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobImpressionsCompletedCol).ToBegCollection(IppAttributeNames.JobImpressionsCompletedCol).ToArray());
            if (src.JobMediaSheetsCompletedCol != null)
                dic.Add(IppAttributeNames.JobMediaSheetsCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobMediaSheetsCompletedCol).ToBegCollection(IppAttributeNames.JobMediaSheetsCompletedCol).ToArray());
            if (src.JobPagesCompleted != null)
                dic.Add(IppAttributeNames.JobPagesCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.JobPagesCompleted, src.JobPagesCompleted.Value)]);
            if (src.JobPagesCompletedCol != null)
                dic.Add(IppAttributeNames.JobPagesCompletedCol, map.Map<IEnumerable<IppAttribute>>(src.JobPagesCompletedCol).ToBegCollection(IppAttributeNames.JobPagesCompletedCol).ToArray());
            if (src.JobProcessingTime != null)
                dic.Add(IppAttributeNames.JobProcessingTime, [new IppAttribute(Tag.Integer, IppAttributeNames.JobProcessingTime, src.JobProcessingTime.Value)]);
            if (src.PlatformTemperatureActual != null)
                dic.Add(IppAttributeNames.PlatformTemperatureActual, src.PlatformTemperatureActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperatureActual, x)).ToArray());
            if (src.ChamberHumidityActual != null)
                dic.Add(IppAttributeNames.ChamberHumidityActual, src.ChamberHumidityActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidityActual, x)).ToArray());
            if (src.ChamberTemperatureActual != null)
                dic.Add(IppAttributeNames.ChamberTemperatureActual, src.ChamberTemperatureActual.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperatureActual, x)).ToArray());
            if (src.ChamberHumidityCurrent != null)
                dic.Add(IppAttributeNames.ChamberHumidityCurrent, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidityCurrent, src.ChamberHumidityCurrent.Value)]);
            if (src.ChamberTemperatureCurrent != null)
                dic.Add(IppAttributeNames.ChamberTemperatureCurrent, [new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperatureCurrent, src.ChamberTemperatureCurrent.Value)]);
            return dic;
        });
    }
}
