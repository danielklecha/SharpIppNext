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
            dst.ResourceId = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceId);
            dst.ResourceName = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceName);
            dst.ResourceInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceInfo);
            dst.ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceNaturalLanguage);
            dst.ResourcePatches = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourcePatches);
            dst.ResourceStringVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceStringVersion);
            dst.ResourceType = map.MapFromDicNullable<ResourceType?>(src, IppAttributeNames.ResourceType);
            dst.ResourceVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceVersion);
            return dst;
        });

        mapper.CreateMap<SetResourceAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, src.ResourceId.Value));
            if (src.ResourceName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.ResourceName, src.ResourceName));
            if (src.ResourceInfo != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceInfo, src.ResourceInfo));
            if (src.ResourceNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.ResourceNaturalLanguage, src.ResourceNaturalLanguage));
            if (src.ResourcePatches != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourcePatches, src.ResourcePatches));
            if (src.ResourceStringVersion != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceStringVersion, src.ResourceStringVersion));
            if (src.ResourceType != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceType, src.ResourceType.Value.Value));
            if (src.ResourceVersion != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceVersion, src.ResourceVersion));
            return dst;
        });
    }
}
