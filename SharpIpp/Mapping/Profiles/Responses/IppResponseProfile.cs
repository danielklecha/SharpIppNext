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
                var statusMessage = map.MapFromDicNullable<string?>(operationAttributes, IppAttributeNames.StatusMessage);
                var detailedStatusMessage = map.MapFromDicNullable<string?>(operationAttributes, IppAttributeNames.DetailedStatusMessage);
                var documentAccessError = map.MapFromDicNullable<string?>(operationAttributes, IppAttributeNames.DocumentAccessError);
                if (statusMessage != null || detailedStatusMessage != null || documentAccessError != null)
                {
                    dst.OperationAttributes ??= new ResponseOperationAttributes();
                    dst.OperationAttributes.StatusMessage = statusMessage;
                    dst.OperationAttributes.DetailedStatusMessage = detailedStatusMessage;
                    dst.OperationAttributes.DocumentAccessError = documentAccessError;
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
            // RFC 8011 Section 4.1.4: attributes-charset and attributes-natural-language
            // MUST be the first and second attributes respectively in the operation attributes group.
            var operationAttrs = new List<IppAttribute>
            {
                new(Tag.Charset, IppAttributeNames.AttributesCharset, src.OperationAttributes?.AttributesCharset ?? "utf-8"),
                new(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, src.OperationAttributes?.AttributesNaturalLanguage ?? NaturalLanguage.En)
            };
            if (src.OperationAttributes?.StatusMessage != null)
                operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.StatusMessage, src.OperationAttributes.StatusMessage));
            if (src.OperationAttributes?.DetailedStatusMessage != null)
                operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DetailedStatusMessage, src.OperationAttributes.DetailedStatusMessage));
            if (src.OperationAttributes?.DocumentAccessError != null)
                operationAttrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentAccessError, src.OperationAttributes.DocumentAccessError));
            dst.OperationAttributes.Add(operationAttrs);
            return dst;
        });
    }
}
