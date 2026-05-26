using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SendResourceDataOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SendResourceDataOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SendResourceDataOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceId);
            dst.ResourceKOctets = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceKOctets);
            dst.ResourceSignature = map.MapFromDicSetNullable<OctetString[]?>(src, IppAttributeNames.ResourceSignature);
            return dst;
        });

        mapper.CreateMap<SendResourceDataOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, src.ResourceId.Value));
            if (src.ResourceKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ResourceKOctets, src.ResourceKOctets.Value));
            if (src.ResourceSignature != null)
                dst.AddRange(src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.ResourceSignature, x)));
            return dst;
        });
    }
}
