using System.Collections.Generic;
using System.Linq;

using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class GetJobsProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            // https://tools.ietf.org/html/rfc2911#section-3.3.4.1
            mapper.CreateMap<GetJobsRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.GetJobs };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
                if(src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, GetJobsRequest>( ( src, map ) =>
            {
                var dst = new GetJobsRequest();
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                dst.OperationAttributes = GetJobsOperationAttributes.Create<GetJobsOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            // https://tools.ietf.org/html/rfc2911#section-3.3.4.2
            mapper.CreateMap<IppResponseMessage, GetJobsResponse>((src, map) =>
            {
                var dst = new GetJobsResponse { Jobs = map.Map<List<IppSection>, JobDescriptionAttributes[]>(src.Sections) };
                map.Map<IppResponseMessage, IIppResponseMessage>(src, dst);
                return dst;
            });

            mapper.CreateMap<GetJobsResponse, IppResponseMessage>((src, map) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppResponseMessage, IppResponseMessage>( src, dst );
                dst.Sections.AddRange(map.Map<JobDescriptionAttributes[], List<IppSection>>(src.Jobs));
                return dst;
            } );

            //https://tools.ietf.org/html/rfc2911#section-4.4
            mapper.CreateMap<List<IppSection>, JobDescriptionAttributes[]>((src, map) =>
                src.Where(x => x.Tag == SectionTag.JobAttributesTag)
                    .Select(x => map.Map<JobDescriptionAttributes>(x.AllAttributes()))
                    .ToArray());

            mapper.CreateMap<JobDescriptionAttributes[], List<IppSection>>( (src, map) =>
            {
                return src.Select(x =>
                {
                    var section = new IppSection { Tag = SectionTag.JobAttributesTag };
                    section.Attributes.AddRange( map.Map<IDictionary<string, IppAttribute[]>>( x ).Values.SelectMany( x => x ) );
                    return section;
                }).ToList();
            });
        }
    }
}
