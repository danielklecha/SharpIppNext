using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SetPrinterAttributesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SetPrinterAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SetPrinterAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.DocumentFormat = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormat);
            return dst;
        });

        mapper.CreateMap<SetPrinterAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentFormat != null)
            {
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat.Value));
            }
            return dst;
        });
    }
}
