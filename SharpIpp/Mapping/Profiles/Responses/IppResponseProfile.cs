using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using ResponseOperationAttributes = SharpIpp.Models.Responses.OperationAttributes;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class IppResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, IIppResponse>((src, dst, map) =>
        {
            dst = dst ?? throw new ArgumentNullException(nameof(dst));
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
            dst ??= new IppResponseMessage();
            dst.Version = src.Version;
            dst.RequestId = src.RequestId;
            dst.StatusCode = src.StatusCode;
            var operationAttrs = new List<IppAttribute>
            {
                new(Tag.Charset, JobAttribute.AttributesCharset, src.OperationAttributes?.AttributesCharset ?? "utf-8"),
                new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, src.OperationAttributes?.AttributesNaturalLanguage ?? "en")
            };
            if (src.OperationAttributes?.StatusMessage != null)
                operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.StatusMessage, src.OperationAttributes.StatusMessage));
            if (src.OperationAttributes?.DetailedStatusMessage != null)
                operationAttrs.AddRange(src.OperationAttributes.DetailedStatusMessage.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DetailedStatusMessage, x)));
            if (src.OperationAttributes?.DocumentAccessError != null)
                operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DocumentAccessError, src.OperationAttributes.DocumentAccessError));
            dst.OperationAttributes.Add(operationAttrs);
            return dst;
        });
    }
}
