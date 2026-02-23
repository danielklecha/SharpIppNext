using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Requests;
using ResponseOperationAttributes = SharpIpp.Models.Responses.OperationAttributes;
using SharpIpp.Mapping.Extensions;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class IppProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<IIppRequest, IppRequestMessage>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, IIppRequest>( ( src, dst, map ) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                return dst;
            } );



            mapper.CreateMap<IppResponseMessage, IIppResponse>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                dst.StatusCode = src.StatusCode;
                var operationAttributes = src.OperationAttributes.SelectMany(x => x).ToIppDictionary();
                if (operationAttributes != null)
                {
                    var statusMessage = map.MapFromDicNullable<string?>(operationAttributes, JobAttribute.StatusMessage);
                    var detailedStatusMessage = map.MapFromDicSetNullable<string[]?>(operationAttributes, JobAttribute.DetailedStatusMessage);
                    var documentAccessError = map.MapFromDicNullable<string?>(operationAttributes, JobAttribute.DocumentAccessError);
                    if (statusMessage != null || detailedStatusMessage != null || documentAccessError != null)
                    {
                        dst.OperationAttributes = new ResponseOperationAttributes
                        {
                            StatusMessage = statusMessage,
                            DetailedStatusMessage = detailedStatusMessage,
                            DocumentAccessError = documentAccessError
                        };
                    }
                }
                return dst;
            });

            mapper.CreateMap<IIppResponse, IppResponseMessage>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                dst.StatusCode = src.StatusCode;
                var operationAttrs = new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")
                };
                if (src.OperationAttributes?.StatusMessage != null)
                    operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.StatusMessage, src.OperationAttributes.StatusMessage));
                if (src.OperationAttributes?.DetailedStatusMessage!= null)
                    operationAttrs.AddRange(src.OperationAttributes.DetailedStatusMessage.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DetailedStatusMessage, x)));
                if (src.OperationAttributes?.DocumentAccessError != null)
                    operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DocumentAccessError, src.OperationAttributes.DocumentAccessError));
                dst.OperationAttributes.Add(operationAttrs);
                return dst;
            });

            mapper.CreateMap<IIppPrinterRequest, IppRequestMessage>((src, dst, map) =>
            {
                map.Map<IIppRequest, IppRequestMessage>(src, dst);
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, IIppPrinterRequest>( ( src, dst, map ) =>
            {
                map.Map<IIppRequestMessage, IIppRequest>( src, dst );
                return dst;
            } );
        }
    }
}
