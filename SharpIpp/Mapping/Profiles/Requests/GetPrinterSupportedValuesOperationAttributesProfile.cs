using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetPrinterSupportedValuesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetPrinterSupportedValuesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetPrinterSupportedValuesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            dst.DocumentFormat = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormat);
            dst.FirstIndex = map.MapFromDicNullable<int?>(src, IppAttributeNames.FirstIndex);
            dst.Limit = map.MapFromDicNullable<int?>(src, IppAttributeNames.Limit);
            return dst;
        });

        mapper.CreateMap<GetPrinterSupportedValuesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, x)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat.Value));
            if (src.FirstIndex != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.FirstIndex, src.FirstIndex.Value));
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Limit, src.Limit.Value));
            return dst;
        });
    }
}
