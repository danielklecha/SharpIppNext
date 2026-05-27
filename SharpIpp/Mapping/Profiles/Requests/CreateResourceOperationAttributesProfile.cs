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
            dst.ResourceFormat = map.MapFromDicNullable<ResourceFormat?>(src, IppAttributeNames.ResourceFormat);
            dst.ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceNaturalLanguage);
            dst.ResourceType = map.MapFromDicNullable<ResourceType?>(src, IppAttributeNames.ResourceType);
            dst.ResourceName = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceName);
            dst.ResourceInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.ResourceInfo);
            return dst;
        });

        mapper.CreateMap<CreateResourceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.ResourceFormat, src.ResourceFormat.Value.Value));
            if (src.ResourceNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.ResourceNaturalLanguage, src.ResourceNaturalLanguage));
            if (src.ResourceType != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceType, src.ResourceType.Value.Value));
            if (src.ResourceName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.ResourceName, src.ResourceName));
            if (src.ResourceInfo != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ResourceInfo, src.ResourceInfo));
            return dst;
        });
    }
}
