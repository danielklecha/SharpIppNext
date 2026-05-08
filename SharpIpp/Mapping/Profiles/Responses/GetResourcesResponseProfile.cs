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
            return dic;
        });
    }
}
