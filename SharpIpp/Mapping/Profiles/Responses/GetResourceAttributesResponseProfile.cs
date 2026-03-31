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
                ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId),
                DateTimeAtCanceled = map.MapFromDicNullable<DateTimeOffset?>(src, SystemAttribute.ResourceDateTimeAtCanceled),
                DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, SystemAttribute.ResourceDateTimeAtCreation),
                DateTimeAtInstalled = map.MapFromDicNullable<DateTimeOffset?>(src, SystemAttribute.ResourceDateTimeAtInstalled),
                ResourceState = map.MapFromDicNullable<ResourceState?>(src, SystemAttribute.ResourceState),
                ResourceStateMessage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceStateMessage),
                ResourceStateReasons = map.MapFromDicSetNullable<ResourceStateReason[]?>(src, SystemAttribute.ResourceStateReasons),
                ResourceKOctets = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceKOctets),
                ResourceUuid = map.MapFromDicNullable<Uri?>(src, SystemAttribute.ResourceUuid),
                TimeAtCanceled = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceTimeAtCanceled),
                TimeAtCreation = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceTimeAtCreation),
                TimeAtInstalled = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceTimeAtInstalled),
                ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceNaturalLanguage),
                ResourcePatches = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourcePatches),
                ResourceSignature = map.MapFromDicSetNullable<byte[][]?>(src, SystemAttribute.ResourceSignature),
                ResourceDataUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.ResourceDataUri),
                ResourceUseCount = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceUseCount),
                ResourceVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceVersion),
                ResourceStringVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceStringVersion),
                ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo),
                ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName),
                ResourceFormat = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceFormat),
                ResourceFormats = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.ResourceFormats),
                ResourceType = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceType),
            });

        mapper.CreateMap<ResourceStatusAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.ResourceId.HasValue)
                dic.Add(SystemAttribute.ResourceId, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value)]);
            if (src.DateTimeAtCanceled.HasValue)
                dic.Add(SystemAttribute.ResourceDateTimeAtCanceled, [new IppAttribute(Tag.DateTime, SystemAttribute.ResourceDateTimeAtCanceled, src.DateTimeAtCanceled.Value)]);
            if (src.DateTimeAtCreation.HasValue)
                dic.Add(SystemAttribute.ResourceDateTimeAtCreation, [new IppAttribute(Tag.DateTime, SystemAttribute.ResourceDateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtInstalled.HasValue)
                dic.Add(SystemAttribute.ResourceDateTimeAtInstalled, [new IppAttribute(Tag.DateTime, SystemAttribute.ResourceDateTimeAtInstalled, src.DateTimeAtInstalled.Value)]);
            if (src.ResourceState.HasValue)
                dic.Add(SystemAttribute.ResourceState, [new IppAttribute(Tag.Enum, SystemAttribute.ResourceState, (int)src.ResourceState.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceStateMessage))
                dic.Add(SystemAttribute.ResourceStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceStateMessage, src.ResourceStateMessage!) });
            if (src.ResourceStateReasons != null)
                dic.Add(SystemAttribute.ResourceStateReasons, src.ResourceStateReasons.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.ResourceStateReasons, x.ToString())).ToArray());
            if (src.ResourceKOctets.HasValue)
                dic.Add(SystemAttribute.ResourceKOctets, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceKOctets, src.ResourceKOctets.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceNaturalLanguage))
                dic.Add(SystemAttribute.ResourceNaturalLanguage, new IppAttribute[] { new IppAttribute(Tag.NaturalLanguage, SystemAttribute.ResourceNaturalLanguage, src.ResourceNaturalLanguage!) });
            if (!string.IsNullOrEmpty(src.ResourcePatches))
                dic.Add(SystemAttribute.ResourcePatches, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourcePatches, src.ResourcePatches!) });
            if (src.ResourceSignature != null)
                dic.Add(SystemAttribute.ResourceSignature, src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.ResourceSignature, x)).ToArray());
            if (src.ResourceDataUri != null)
                dic.Add(SystemAttribute.ResourceDataUri, new IppAttribute[] { new IppAttribute(Tag.Uri, SystemAttribute.ResourceDataUri, src.ResourceDataUri.ToString()) });
            if (src.ResourceUseCount.HasValue)
                dic.Add(SystemAttribute.ResourceUseCount, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceUseCount, src.ResourceUseCount.Value)]);
            if (src.TimeAtCanceled.HasValue)
                dic.Add(SystemAttribute.ResourceTimeAtCanceled, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceTimeAtCanceled, src.TimeAtCanceled.Value)]);
            if (src.TimeAtCreation.HasValue)
                dic.Add(SystemAttribute.ResourceTimeAtCreation, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceTimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtInstalled.HasValue)
                dic.Add(SystemAttribute.ResourceTimeAtInstalled, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceTimeAtInstalled, src.TimeAtInstalled.Value)]);
            if (src.ResourceUuid != null)
                dic.Add(SystemAttribute.ResourceUuid, new IppAttribute[] { new IppAttribute(Tag.Uri, SystemAttribute.ResourceUuid, src.ResourceUuid.ToString()!) });
            if (!string.IsNullOrEmpty(src.ResourceVersion))
                dic.Add(SystemAttribute.ResourceVersion, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceVersion, src.ResourceVersion!) });
            if (!string.IsNullOrEmpty(src.ResourceStringVersion))
                dic.Add(SystemAttribute.ResourceStringVersion, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceStringVersion, src.ResourceStringVersion!) });
            if (!string.IsNullOrEmpty(src.ResourceInfo))
                dic.Add(SystemAttribute.ResourceInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo!) });
            if (!string.IsNullOrEmpty(src.ResourceName))
                dic.Add(SystemAttribute.ResourceName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName!) });
            if (!string.IsNullOrEmpty(src.ResourceFormat))
                dic.Add(SystemAttribute.ResourceFormat, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormat, src.ResourceFormat!) });
            if (src.ResourceFormats != null)
                dic.Add(SystemAttribute.ResourceFormats, src.ResourceFormats.Select(x => new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormats, x)).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceType))
                dic.Add(SystemAttribute.ResourceType, new IppAttribute[] { new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType!) });
            return dic;
        });
    }
}
