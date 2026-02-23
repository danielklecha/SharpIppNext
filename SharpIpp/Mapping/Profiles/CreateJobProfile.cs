using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class CreateJobProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<CreateJobRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.CreateJob };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
                if (src.JobTemplateAttributes != null)
                    map.Map(src.JobTemplateAttributes, dst);
                if(src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, CreateJobRequest>( ( src, map ) =>
            {
                var dst = new CreateJobRequest { JobTemplateAttributes = new JobTemplateAttributes() };
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                map.Map( src, dst.JobTemplateAttributes );
                dst.OperationAttributes = CreateJobOperationAttributes.Create<CreateJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, CreateJobResponse>((src, map) =>
            {
                var dst = new CreateJobResponse();
                map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<CreateJobResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppJobResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
