using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CreateResourceOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CreateResourceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CreateResourceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceFormat = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceFormat);
            dst.ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceNaturalLanguage);
            dst.ResourceType = map.MapFromDicNullable<ResourceType?>(src, SystemAttribute.ResourceType);
            dst.ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName);
            dst.ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo);
            return dst;
        });

        mapper.CreateMap<CreateResourceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormat, src.ResourceFormat));
            if (src.ResourceNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, SystemAttribute.ResourceNaturalLanguage, src.ResourceNaturalLanguage));
            if (src.ResourceType != null)
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType.Value.Value));
            if (src.ResourceName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName));
            if (src.ResourceInfo != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo));
            return dst;
        });
    }
}
