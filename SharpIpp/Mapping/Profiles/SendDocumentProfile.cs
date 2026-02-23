using System;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class SendDocumentProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<SendDocumentRequest, IppRequestMessage>((src, map) =>
            {
                if (src.Document == null && !(src.OperationAttributes?.LastDocument ?? false))
                {
                    throw new ArgumentException($"{nameof(src.Document)} must be set for non-last document");
                }

                var dst = new IppRequestMessage
                {
                    IppOperation = IppOperation.SendDocument, Document = src.Document,
                };
                map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
                if (src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, SendDocumentRequest>( ( src, map ) =>
            {
                var dst = new SendDocumentRequest
                {
                    Document = src.Document
                };
                map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
                dst.OperationAttributes = SendDocumentOperationAttributes.Create<SendDocumentOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                if (!src.OperationAttributes.Any(x => x.Name == JobAttribute.LastDocument))
                    throw new IppRequestException( "missing last-document", src, IppStatusCode.ClientErrorBadRequest );
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, SendDocumentResponse>((src, map) =>
            {
                var dst = new SendDocumentResponse();
                map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<SendDocumentResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppJobResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
