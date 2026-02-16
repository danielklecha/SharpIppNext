using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Models;
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
                map.Map(src.AllAttributes(), dst);
                return dst;
            });

            mapper.CreateMap<IIppJobResponse, IppResponseMessage>( ( src, dst, map ) =>
            {
                map.Map<IIppResponse, IppResponseMessage>( src, dst );
                var section = new IppSection { Tag = SectionTag.JobAttributesTag };
                section.Attributes.AddRange( map.Map<IIppJobResponse, IDictionary<string, IppAttribute[]>>( src ).Values.SelectMany( x => x ) );
                dst.Sections.Add( section );
                return dst;
            } );

            mapper.CreateMap<IDictionary<string, IppAttribute[]>, IIppJobResponse>((
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

            mapper.CreateMap<IIppJobResponse, IDictionary<string, IppAttribute[]>>( (
                src,
                dst,
                map ) =>
            {
                var dic = new Dictionary<string, IppAttribute[]>
                {
                    { JobAttribute.JobUri, new IppAttribute[] { new IppAttribute( Tag.Uri, JobAttribute.JobUri, src.JobUri ) } },
                    { JobAttribute.JobId, new IppAttribute[] { new IppAttribute( Tag.Integer, JobAttribute.JobId, src.JobId ) } },
                    { JobAttribute.JobState, new IppAttribute[] { new IppAttribute( Tag.Enum, JobAttribute.JobState, (int)src.JobState ) } }
                };
                if ( src.JobStateReasons?.Any() ?? false )
                    dic.Add( JobAttribute.JobStateReasons, src.JobStateReasons.Select( x => new IppAttribute( Tag.Keyword, JobAttribute.JobStateReasons, map.Map<string>( x ) ) ).ToArray() );
                if( src.JobStateMessage != null )
                    dic.Add( JobAttribute.JobStateMessage, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, src.JobStateMessage ) } );
                if(src.NumberOfInterveningJobs != null )
                    dic.Add( JobAttribute.NumberOfInterveningJobs, new IppAttribute[] { new IppAttribute( Tag.Integer, JobAttribute.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value ) } );
                return dic;
            } );
        }
    }
}
