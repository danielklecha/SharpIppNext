using System;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class ValidateJobProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<ValidateJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.ValidateJob };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);

            if (src.JobTemplateAttributes != null)
            {
                map.Map(src.JobTemplateAttributes, dst);
            }

            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, ValidateJobRequest>( ( src, map ) =>
        {
            var dst = new ValidateJobRequest
            {
                JobTemplateAttributes = new JobTemplateAttributes()
            };
            map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
            map.Map( src, dst.JobTemplateAttributes );
            dst.OperationAttributes = ValidateJobOperationAttributes.Create<ValidateJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
            return dst;
        } );

        mapper.CreateMap<IppResponseMessage, ValidateJobResponse>((src, map) =>
        {
            var dst = new ValidateJobResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<ValidateJobResponse, IppResponseMessage>( ( src, map ) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>( src, dst );
            return dst;
        } );
    }
}
