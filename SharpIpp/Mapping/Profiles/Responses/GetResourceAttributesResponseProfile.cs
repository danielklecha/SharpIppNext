using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetResourceAttributesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetResourceAttributesResponse>((src, map) =>
        {
            var dst = new GetResourceAttributesResponse
            {
                ResourceAttributes = map.Map<ResourceStatusAttributes>(src.ResourceAttributes.SelectMany(x => x).ToIppDictionary())
            };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetResourceAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.ResourceAttributes != null)
            {
                var resourceAttrs = new List<IppAttribute>();
                resourceAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.ResourceAttributes).Values.SelectMany(x => x));
                dst.ResourceAttributes.Add(resourceAttrs);
            }
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ResourceStatusAttributes>((src, map) =>
            new ResourceStatusAttributes
            {
                ResourceId = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceId),
                DateTimeAtCanceled = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.ResourceDateTimeAtCanceled),
                DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.ResourceDateTimeAtCreation),
                DateTimeAtInstalled = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.ResourceDateTimeAtInstalled),
                ResourceState = map.MapFromDicNullable<ResourceState?>(src, IppAttributeNames.ResourceState),
                ResourceStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceStateMessage),
                ResourceStateReasons = map.MapFromDicSetNullable<ResourceStateReason[]?>(src, IppAttributeNames.ResourceStateReasons),
                ResourceKOctets = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceKOctets),
                ResourceUuid = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.ResourceUuid),
                TimeAtCanceled = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceTimeAtCanceled),
                TimeAtCreation = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceTimeAtCreation),
                TimeAtInstalled = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceTimeAtInstalled),
                ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceNaturalLanguage),
                ResourcePatches = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourcePatches),
                ResourceSignature = map.MapFromDicSetNullable<OctetString[]?>(src, IppAttributeNames.ResourceSignature),
                ResourceDataUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.ResourceDataUri),
                ResourceUseCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceUseCount),
                ResourceVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceVersion),
                ResourceStringVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceStringVersion),
                ResourceInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceInfo),
                ResourceName = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceName),
                ResourceFormat = map.MapFromDicNullable<ResourceFormat?>(src, IppAttributeNames.ResourceFormat),
                ResourceFormats = map.MapFromDicSetNullable<ResourceFormat[]?>(src, IppAttributeNames.ResourceFormats),
                ResourceType = map.MapFromDicNullable<ResourceType?>(src, IppAttributeNames.ResourceType),
            });

        mapper.CreateMap<ResourceStatusAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.ResourceId.HasValue)
                dic.Add(IppAttributeNames.ResourceId, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, src.ResourceId.Value)]);
            if (src.DateTimeAtCanceled.HasValue)
                dic.Add(IppAttributeNames.ResourceDateTimeAtCanceled, [new IppAttribute(Tag.DateTime, IppAttributeNames.ResourceDateTimeAtCanceled, src.DateTimeAtCanceled.Value)]);
            if (src.DateTimeAtCreation.HasValue)
                dic.Add(IppAttributeNames.ResourceDateTimeAtCreation, [new IppAttribute(Tag.DateTime, IppAttributeNames.ResourceDateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtInstalled.HasValue)
                dic.Add(IppAttributeNames.ResourceDateTimeAtInstalled, [new IppAttribute(Tag.DateTime, IppAttributeNames.ResourceDateTimeAtInstalled, src.DateTimeAtInstalled.Value)]);
            if (src.ResourceState.HasValue)
                dic.Add(IppAttributeNames.ResourceState, [new IppAttribute(Tag.Enum, IppAttributeNames.ResourceState, (int)src.ResourceState.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceStateMessage))
                dic.Add(IppAttributeNames.ResourceStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceStateMessage, src.ResourceStateMessage!) });
            if (src.ResourceStateReasons != null)
                dic.Add(IppAttributeNames.ResourceStateReasons, src.ResourceStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceStateReasons, x.ToString())).ToArray());
            if (src.ResourceKOctets.HasValue)
                dic.Add(IppAttributeNames.ResourceKOctets, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceKOctets, src.ResourceKOctets.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceNaturalLanguage))
                dic.Add(IppAttributeNames.ResourceNaturalLanguage, new IppAttribute[] { new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.ResourceNaturalLanguage, src.ResourceNaturalLanguage!) });
            if (!string.IsNullOrEmpty(src.ResourcePatches))
                dic.Add(IppAttributeNames.ResourcePatches, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourcePatches, src.ResourcePatches!) });
            if (src.ResourceSignature != null)
                dic.Add(IppAttributeNames.ResourceSignature, src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.ResourceSignature, x)).ToArray());
            if (src.ResourceDataUri != null)
                dic.Add(IppAttributeNames.ResourceDataUri, new IppAttribute[] { new IppAttribute(Tag.Uri, IppAttributeNames.ResourceDataUri, src.ResourceDataUri.ToString()) });
            if (src.ResourceUseCount.HasValue)
                dic.Add(IppAttributeNames.ResourceUseCount, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceUseCount, src.ResourceUseCount.Value)]);
            if (src.TimeAtCanceled.HasValue)
                dic.Add(IppAttributeNames.ResourceTimeAtCanceled, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceTimeAtCanceled, src.TimeAtCanceled.Value)]);
            if (src.TimeAtCreation.HasValue)
                dic.Add(IppAttributeNames.ResourceTimeAtCreation, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceTimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtInstalled.HasValue)
                dic.Add(IppAttributeNames.ResourceTimeAtInstalled, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceTimeAtInstalled, src.TimeAtInstalled.Value)]);
            if (src.ResourceUuid != null)
                dic.Add(IppAttributeNames.ResourceUuid, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.ResourceUuid, src.ResourceUuid.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceVersion))
                dic.Add(IppAttributeNames.ResourceVersion, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceVersion, src.ResourceVersion!) });
            if (!string.IsNullOrEmpty(src.ResourceStringVersion))
                dic.Add(IppAttributeNames.ResourceStringVersion, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceStringVersion, src.ResourceStringVersion!) });
            if (!string.IsNullOrEmpty(src.ResourceInfo))
                dic.Add(IppAttributeNames.ResourceInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceInfo, src.ResourceInfo!) });
            if (!string.IsNullOrEmpty(src.ResourceName))
                dic.Add(IppAttributeNames.ResourceName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.ResourceName, src.ResourceName!) });
            if (src.ResourceFormat != null)
                dic.Add(IppAttributeNames.ResourceFormat, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, IppAttributeNames.ResourceFormat, src.ResourceFormat.Value.Value) });
            if (src.ResourceFormats != null)
                dic.Add(IppAttributeNames.ResourceFormats, src.ResourceFormats.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.ResourceFormats, x.Value)).ToArray());
            if (src.ResourceType != null)
                dic.Add(IppAttributeNames.ResourceType, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceType, src.ResourceType.Value.Value) });
            return dic;
        });
    }
}
