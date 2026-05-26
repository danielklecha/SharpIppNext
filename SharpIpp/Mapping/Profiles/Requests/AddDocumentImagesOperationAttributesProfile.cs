using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AddDocumentImagesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AddDocumentImagesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AddDocumentImagesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobId);
            if (src.ContainsKey(IppAttributeNames.InputAttributes))
                dst.InputAttributes = map.Map<DocumentTemplateAttributes>(src[IppAttributeNames.InputAttributes].FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<AddDocumentImagesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId.Value));
            if (src.InputAttributes != null)
                dst.AddRange(map.Map<List<IppAttribute>>(src.InputAttributes).ToBegCollection(IppAttributeNames.InputAttributes));
            return dst;
        });
    }
}
