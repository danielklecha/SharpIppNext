using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetUserPrinterAttributesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetUserPrinterAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetUserPrinterAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, GetPrinterAttributesOperationAttributes>(src, dst);
            dst.RequestingUserVcard = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestingUserVcard);
            return dst;
        });

        mapper.CreateMap<GetUserPrinterAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<GetPrinterAttributesOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestingUserVcard != null)
                dst.AddRange(src.RequestingUserVcard.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.RequestingUserVcard, x)));
            return dst;
        });
    }
}
