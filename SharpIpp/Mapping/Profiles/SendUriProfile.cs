using System;
using System.Linq;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class SendUriProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<SendUriRequest, IppRequestMessage>((src, map) =>
        {
            if (src.OperationAttributes != null && src.OperationAttributes.DocumentUri == null && !src.OperationAttributes.LastDocument)
            {
                throw new ArgumentException($"{nameof(src.OperationAttributes.DocumentUri)} must be set for non-last document");
            }

            var dst = new IppRequestMessage { IppOperation = IppOperation.SendUri };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, SendUriRequest>( ( src, map ) =>
        {
            var dst = new SendUriRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
            dst.OperationAttributes = SendUriOperationAttributes.Create<SendUriOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
            return dst;
        } );

        mapper.CreateMap<IppResponseMessage, SendUriResponse>((src, map) =>
        {
            var dst = new SendUriResponse();
            map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<SendUriResponse, IppResponseMessage>( ( src, map ) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppJobResponse, IppResponseMessage>( src, dst );
            return dst;
        } );
    }
}
