using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class IppJobProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<IIppJobRequest, IppRequestMessage>((src, dst, map) =>
            {
                map.Map<IIppRequest, IppRequestMessage>(src, dst);
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, IIppJobRequest>( ( src, dst, map ) =>
            {
                map.Map<IIppRequestMessage, IIppRequest>( src, dst );
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, IIppJobResponse>((src, dst, map) =>
            {
                map.Map<IppResponseMessage, IIppResponse>(src, dst);
                var jobAttrs = new JobAttributes();
                map.Map(src.JobAttributes.SelectMany(x => x).ToIppDictionary(), jobAttrs);
                dst.JobAttributes = jobAttrs;
                return dst;
            });

            mapper.CreateMap<IIppJobResponse, IppResponseMessage>( ( src, dst, map ) =>
            {
                map.Map<IIppResponse, IppResponseMessage>( src, dst );
                if (src.JobAttributes != null)
                {
                    var jobAttrs = new List<IppAttribute>();
                    jobAttrs.AddRange( map.Map<JobAttributes, IDictionary<string, IppAttribute[]>>( src.JobAttributes ).Values.SelectMany( x => x ) );
                    dst.JobAttributes.Add( jobAttrs );
                }
                return dst;
            } );

            mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobAttributes>((
                src,
                dst,
                map) =>
            {
                dst.JobUri = map.MapFromDic<string>(src, JobAttribute.JobUri);
                dst.JobId = map.MapFromDic<int>(src, JobAttribute.JobId);
                dst.JobState = map.MapFromDic<JobState>(src, JobAttribute.JobState);
                dst.JobStateReasons = map.MapFromDicSet<JobStateReason[]>(src, JobAttribute.JobStateReasons);
                dst.JobStateMessage = map.MapFromDic<string?>(src, JobAttribute.JobStateMessage);
                dst.NumberOfInterveningJobs = map.MapFromDic<int?>(src, JobAttribute.NumberOfInterveningJobs);
                return dst;
            });

            mapper.CreateMap<JobAttributes, IDictionary<string, IppAttribute[]>>( (
                src,
                dst,
                map ) =>
            {
                var dic = new Dictionary<string, IppAttribute[]>
                {
                    { JobAttribute.JobUri, new IppAttribute[] { new IppAttribute( Tag.Uri, JobAttribute.JobUri, src.JobUri ) } },
                    { JobAttribute.JobId, [IppAttribute.CreateInt( JobAttribute.JobId, src.JobId )] },
                    { JobAttribute.JobState, new IppAttribute[] { IppAttribute.CreateEnum( JobAttribute.JobState, (int)src.JobState ) } }
                };
                if ( src.JobStateReasons?.Any() ?? false )
                    dic.Add( JobAttribute.JobStateReasons, src.JobStateReasons.Select( x => new IppAttribute( Tag.Keyword, JobAttribute.JobStateReasons, map.Map<string>( x ) ) ).ToArray() );
                if( src.JobStateMessage != null )
                    dic.Add( JobAttribute.JobStateMessage, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, src.JobStateMessage ) } );
                if(src.NumberOfInterveningJobs != null )
                    dic.Add( JobAttribute.NumberOfInterveningJobs, [IppAttribute.CreateInt( JobAttribute.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value )] );
                return dic;
            } );
        }
    }
}
