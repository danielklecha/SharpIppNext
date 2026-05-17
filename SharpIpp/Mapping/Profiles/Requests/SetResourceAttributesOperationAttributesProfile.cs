using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SetResourceAttributesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SetResourceAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SetResourceAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            dst.ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName);
            dst.ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo);
            dst.ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceNaturalLanguage);
            dst.ResourcePatches = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourcePatches);
            dst.ResourceStringVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceStringVersion);
            dst.ResourceType = map.MapFromDicNullable<ResourceType?>(src, SystemAttribute.ResourceType);
            dst.ResourceVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceVersion);
            return dst;
        });

        mapper.CreateMap<SetResourceAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.ResourceName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName));
            if (src.ResourceInfo != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo));
            if (src.ResourceNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, SystemAttribute.ResourceNaturalLanguage, src.ResourceNaturalLanguage));
            if (src.ResourcePatches != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourcePatches, src.ResourcePatches));
            if (src.ResourceStringVersion != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceStringVersion, src.ResourceStringVersion));
            if (src.ResourceType != null)
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType.Value.Value));
            if (src.ResourceVersion != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceVersion, src.ResourceVersion));
            return dst;
        });
    }
}
