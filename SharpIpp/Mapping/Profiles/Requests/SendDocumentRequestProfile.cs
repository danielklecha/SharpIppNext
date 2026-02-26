using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class SendDocumentRequestProfile : IProfile
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
                IppOperation = IppOperation.SendDocument,
                Document = src.Document,
            };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<SendDocumentOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, SendDocumentRequest>((src, map) =>
        {
            var dst = new SendDocumentRequest
            {
                Document = src.Document
            };
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (!src.OperationAttributes.Any(x => x.Name == JobAttribute.LastDocument))
                throw new IppRequestException("missing last-document", src, IppStatusCode.ClientErrorBadRequest);
            return dst;
        });
    }
}
