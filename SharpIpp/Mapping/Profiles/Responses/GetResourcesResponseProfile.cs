using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetResourcesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetResourcesResponse>((src, map) =>
        {
            var dst = new GetResourcesResponse();
            if (src.ResourceAttributes.Count > 0)
                dst.ResourcesAttributes = map.Map<List<List<IppAttribute>>, ResourceDescriptionAttributes[]>(src.ResourceAttributes);
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetResourcesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.ResourcesAttributes != null)
            {
                dst.ResourceAttributes.AddRange(map.Map<ResourceDescriptionAttributes[], List<List<IppAttribute>>>(src.ResourcesAttributes));
            }
            return dst;
        });

        mapper.CreateMap<List<List<IppAttribute>>, ResourceDescriptionAttributes[]>((src, map) =>
            src.Select(x => map.Map<ResourceDescriptionAttributes>(x.ToIppDictionary())).ToArray());

        mapper.CreateMap<ResourceDescriptionAttributes[], List<List<IppAttribute>>>((src, map) =>
            src.Select(r => map.Map<IDictionary<string, IppAttribute[]>>(r).Values.SelectMany(g => g).ToList()).ToList());

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ResourceDescriptionAttributes>((src, map) => new ResourceDescriptionAttributes
        {
            ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId),
            ResourceFormat = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceFormat),
            ResourceFormats = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.ResourceFormats),
            ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName),
            ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo),
            ResourceStates = map.MapFromDicSetNullable<ResourceState[]?>(src, SystemAttribute.ResourceStates),
            ResourceType = map.MapFromDicNullable<ResourceType?>(src, SystemAttribute.ResourceType),
            ResourceVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceVersion),
            ResourceState = map.MapFromDicNullable<ResourceState?>(src, SystemAttribute.ResourceState),
            ResourceStateReasons = map.MapFromDicSetNullable<ResourceStateReason[]?>(src, SystemAttribute.ResourceStateReasons),
            ResourceStateMessage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceStateMessage),
            ResourceKOctets = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceKOctets),
            ResourceDataUri = map.MapFromDicNullable<System.Uri?>(src, SystemAttribute.ResourceDataUri),
            ResourceUseCount = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceUseCount),
            ResourceUuid = map.MapFromDicNullable<OctetString?>(src, SystemAttribute.ResourceUuid),
            DateTimeAtCreation = map.MapFromDicNullable<System.DateTimeOffset?>(src, SystemAttribute.ResourceDateTimeAtCreation),
            DateTimeAtInstalled = map.MapFromDicNullable<System.DateTimeOffset?>(src, SystemAttribute.ResourceDateTimeAtInstalled),
            DateTimeAtCanceled = map.MapFromDicNullable<System.DateTimeOffset?>(src, SystemAttribute.ResourceDateTimeAtCanceled),
            TimeAtCreation = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceTimeAtCreation),
            TimeAtInstalled = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceTimeAtInstalled),
            TimeAtCanceled = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceTimeAtCanceled),
            ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceNaturalLanguage),
            ResourcePatches = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourcePatches),
            ResourceSignature = map.MapFromDicSetNullable<OctetString[]?>(src, SystemAttribute.ResourceSignature),
            ResourceStringVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceStringVersion),
        });

        mapper.CreateMap<ResourceDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.ResourceId.HasValue)
                dic.Add(SystemAttribute.ResourceId, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceFormat))
                dic.Add(SystemAttribute.ResourceFormat, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormat, src.ResourceFormat!) });
            if (src.ResourceFormats != null)
                dic.Add(SystemAttribute.ResourceFormats, src.ResourceFormats.Select(x => new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormats, x)).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceName))
                dic.Add(SystemAttribute.ResourceName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName!) });
            if (!string.IsNullOrEmpty(src.ResourceInfo))
                dic.Add(SystemAttribute.ResourceInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo!) });
            if (src.ResourceStates != null)
                dic.Add(SystemAttribute.ResourceStates, src.ResourceStates.Select(x => new IppAttribute(Tag.Enum, SystemAttribute.ResourceStates, (int)x)).ToArray());
            if (src.ResourceType != null)
                dic.Add(SystemAttribute.ResourceType, new IppAttribute[] { new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType.Value.Value) });
            if (!string.IsNullOrEmpty(src.ResourceVersion))
                dic.Add(SystemAttribute.ResourceVersion, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceVersion, src.ResourceVersion!) });
            if (src.ResourceState.HasValue)
                dic.Add(SystemAttribute.ResourceState, [new IppAttribute(Tag.Enum, SystemAttribute.ResourceState, (int)src.ResourceState.Value)]);
            if (src.ResourceStateReasons != null)
                dic.Add(SystemAttribute.ResourceStateReasons, src.ResourceStateReasons.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.ResourceStateReasons, x.ToString())).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceStateMessage))
                dic.Add(SystemAttribute.ResourceStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceStateMessage, src.ResourceStateMessage!)]);
            if (src.ResourceKOctets.HasValue)
                dic.Add(SystemAttribute.ResourceKOctets, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceKOctets, src.ResourceKOctets.Value)]);
            if (src.ResourceDataUri != null)
                dic.Add(SystemAttribute.ResourceDataUri, [new IppAttribute(Tag.Uri, SystemAttribute.ResourceDataUri, src.ResourceDataUri.ToString())]);
            if (src.ResourceUseCount.HasValue)
                dic.Add(SystemAttribute.ResourceUseCount, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceUseCount, src.ResourceUseCount.Value)]);
            if (src.ResourceUuid != null)
                dic.Add(SystemAttribute.ResourceUuid, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.ResourceUuid, src.ResourceUuid.Value)]);
            if (src.DateTimeAtCreation.HasValue)
                dic.Add(SystemAttribute.ResourceDateTimeAtCreation, [new IppAttribute(Tag.DateTime, SystemAttribute.ResourceDateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtInstalled.HasValue)
                dic.Add(SystemAttribute.ResourceDateTimeAtInstalled, [new IppAttribute(Tag.DateTime, SystemAttribute.ResourceDateTimeAtInstalled, src.DateTimeAtInstalled.Value)]);
            if (src.DateTimeAtCanceled.HasValue)
                dic.Add(SystemAttribute.ResourceDateTimeAtCanceled, [new IppAttribute(Tag.DateTime, SystemAttribute.ResourceDateTimeAtCanceled, src.DateTimeAtCanceled.Value)]);
            if (src.TimeAtCreation.HasValue)
                dic.Add(SystemAttribute.ResourceTimeAtCreation, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceTimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtInstalled.HasValue)
                dic.Add(SystemAttribute.ResourceTimeAtInstalled, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceTimeAtInstalled, src.TimeAtInstalled.Value)]);
            if (src.TimeAtCanceled.HasValue)
                dic.Add(SystemAttribute.ResourceTimeAtCanceled, [new IppAttribute(Tag.Integer, SystemAttribute.ResourceTimeAtCanceled, src.TimeAtCanceled.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceNaturalLanguage))
                dic.Add(SystemAttribute.ResourceNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, SystemAttribute.ResourceNaturalLanguage, src.ResourceNaturalLanguage!)]);
            if (!string.IsNullOrEmpty(src.ResourcePatches))
                dic.Add(SystemAttribute.ResourcePatches, [new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourcePatches, src.ResourcePatches!)]);
            if (src.ResourceSignature != null)
                dic.Add(SystemAttribute.ResourceSignature, src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.ResourceSignature, x)).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceStringVersion))
                dic.Add(SystemAttribute.ResourceStringVersion, [new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceStringVersion, src.ResourceStringVersion!)]);
            return dic;
        });
    }
}
