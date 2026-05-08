using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class SystemConfiguredResourceProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemConfiguredResource>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<SystemConfiguredResource>();

            return new SystemConfiguredResource
            {
                ResourceFormat = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceFormat),
                ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId),
                ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo),
                ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName),
                ResourceState = map.MapFromDicNullable<ResourceState?>(src, SystemAttribute.ResourceState),
                ResourceStateReasons = map.MapFromDicSetNullable<ResourceStateReason[]?>(src, SystemAttribute.ResourceStateReasons),
                ResourceType = map.MapFromDicNullable<ResourceType?>(src, SystemAttribute.ResourceType)
            };
        });

        mapper.CreateMap<SystemConfiguredResource, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.SystemConfiguredResources, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ResourceFormat != null)
                attributes.Add(new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormat, src.ResourceFormat));
            if (src.ResourceId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.ResourceInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo));
            if (src.ResourceName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName));
            if (src.ResourceState.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, SystemAttribute.ResourceState, (int)src.ResourceState.Value));
            if (src.ResourceStateReasons != null)
                attributes.AddRange(src.ResourceStateReasons.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.ResourceStateReasons, x.ToString())));
            if (src.ResourceType != null)
                attributes.Add(new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType.Value.Value));
            return attributes;
        });
    }
}
