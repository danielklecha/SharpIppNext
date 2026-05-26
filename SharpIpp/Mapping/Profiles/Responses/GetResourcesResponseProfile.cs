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
            ResourceId = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceId),
            ResourceFormat = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceFormat),
            ResourceFormats = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.ResourceFormats),
            ResourceName = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceName),
            ResourceInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceInfo),
            ResourceStates = map.MapFromDicSetNullable<ResourceState[]?>(src, IppAttributeNames.ResourceStates),
            ResourceType = map.MapFromDicNullable<ResourceType?>(src, IppAttributeNames.ResourceType),
            ResourceVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceVersion),
            ResourceState = map.MapFromDicNullable<ResourceState?>(src, IppAttributeNames.ResourceState),
            ResourceStateReasons = map.MapFromDicSetNullable<ResourceStateReason[]?>(src, IppAttributeNames.ResourceStateReasons),
            ResourceStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceStateMessage),
            ResourceKOctets = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceKOctets),
            ResourceDataUri = map.MapFromDicNullable<System.Uri?>(src, IppAttributeNames.ResourceDataUri),
            ResourceUseCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceUseCount),
            ResourceUuid = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.ResourceUuid),
            DateTimeAtCreation = map.MapFromDicNullable<System.DateTimeOffset?>(src, IppAttributeNames.ResourceDateTimeAtCreation),
            DateTimeAtInstalled = map.MapFromDicNullable<System.DateTimeOffset?>(src, IppAttributeNames.ResourceDateTimeAtInstalled),
            DateTimeAtCanceled = map.MapFromDicNullable<System.DateTimeOffset?>(src, IppAttributeNames.ResourceDateTimeAtCanceled),
            TimeAtCreation = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceTimeAtCreation),
            TimeAtInstalled = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceTimeAtInstalled),
            TimeAtCanceled = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceTimeAtCanceled),
            ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceNaturalLanguage),
            ResourcePatches = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourcePatches),
            ResourceSignature = map.MapFromDicSetNullable<OctetString[]?>(src, IppAttributeNames.ResourceSignature),
            ResourceStringVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceStringVersion),
        });

        mapper.CreateMap<ResourceDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.ResourceId.HasValue)
                dic.Add(IppAttributeNames.ResourceId, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, src.ResourceId.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceFormat))
                dic.Add(IppAttributeNames.ResourceFormat, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, IppAttributeNames.ResourceFormat, src.ResourceFormat!) });
            if (src.ResourceFormats != null)
                dic.Add(IppAttributeNames.ResourceFormats, src.ResourceFormats.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.ResourceFormats, x)).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceName))
                dic.Add(IppAttributeNames.ResourceName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.ResourceName, src.ResourceName!) });
            if (!string.IsNullOrEmpty(src.ResourceInfo))
                dic.Add(IppAttributeNames.ResourceInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceInfo, src.ResourceInfo!) });
            if (src.ResourceStates != null)
                dic.Add(IppAttributeNames.ResourceStates, src.ResourceStates.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.ResourceStates, (int)x)).ToArray());
            if (src.ResourceType != null)
                dic.Add(IppAttributeNames.ResourceType, new IppAttribute[] { new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceType, src.ResourceType.Value.Value) });
            if (!string.IsNullOrEmpty(src.ResourceVersion))
                dic.Add(IppAttributeNames.ResourceVersion, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceVersion, src.ResourceVersion!) });
            if (src.ResourceState.HasValue)
                dic.Add(IppAttributeNames.ResourceState, [new IppAttribute(Tag.Enum, IppAttributeNames.ResourceState, (int)src.ResourceState.Value)]);
            if (src.ResourceStateReasons != null)
                dic.Add(IppAttributeNames.ResourceStateReasons, src.ResourceStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceStateReasons, x.ToString())).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceStateMessage))
                dic.Add(IppAttributeNames.ResourceStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceStateMessage, src.ResourceStateMessage!)]);
            if (src.ResourceKOctets.HasValue)
                dic.Add(IppAttributeNames.ResourceKOctets, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceKOctets, src.ResourceKOctets.Value)]);
            if (src.ResourceDataUri != null)
                dic.Add(IppAttributeNames.ResourceDataUri, [new IppAttribute(Tag.Uri, IppAttributeNames.ResourceDataUri, src.ResourceDataUri.ToString())]);
            if (src.ResourceUseCount.HasValue)
                dic.Add(IppAttributeNames.ResourceUseCount, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceUseCount, src.ResourceUseCount.Value)]);
            if (src.ResourceUuid != null)
                dic.Add(IppAttributeNames.ResourceUuid, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.ResourceUuid, src.ResourceUuid.Value)]);
            if (src.DateTimeAtCreation.HasValue)
                dic.Add(IppAttributeNames.ResourceDateTimeAtCreation, [new IppAttribute(Tag.DateTime, IppAttributeNames.ResourceDateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtInstalled.HasValue)
                dic.Add(IppAttributeNames.ResourceDateTimeAtInstalled, [new IppAttribute(Tag.DateTime, IppAttributeNames.ResourceDateTimeAtInstalled, src.DateTimeAtInstalled.Value)]);
            if (src.DateTimeAtCanceled.HasValue)
                dic.Add(IppAttributeNames.ResourceDateTimeAtCanceled, [new IppAttribute(Tag.DateTime, IppAttributeNames.ResourceDateTimeAtCanceled, src.DateTimeAtCanceled.Value)]);
            if (src.TimeAtCreation.HasValue)
                dic.Add(IppAttributeNames.ResourceTimeAtCreation, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceTimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtInstalled.HasValue)
                dic.Add(IppAttributeNames.ResourceTimeAtInstalled, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceTimeAtInstalled, src.TimeAtInstalled.Value)]);
            if (src.TimeAtCanceled.HasValue)
                dic.Add(IppAttributeNames.ResourceTimeAtCanceled, [new IppAttribute(Tag.Integer, IppAttributeNames.ResourceTimeAtCanceled, src.TimeAtCanceled.Value)]);
            if (!string.IsNullOrEmpty(src.ResourceNaturalLanguage))
                dic.Add(IppAttributeNames.ResourceNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.ResourceNaturalLanguage, src.ResourceNaturalLanguage!)]);
            if (!string.IsNullOrEmpty(src.ResourcePatches))
                dic.Add(IppAttributeNames.ResourcePatches, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourcePatches, src.ResourcePatches!)]);
            if (src.ResourceSignature != null)
                dic.Add(IppAttributeNames.ResourceSignature, src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.ResourceSignature, x)).ToArray());
            if (!string.IsNullOrEmpty(src.ResourceStringVersion))
                dic.Add(IppAttributeNames.ResourceStringVersion, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceStringVersion, src.ResourceStringVersion!)]);
            return dic;
        });
    }
}
