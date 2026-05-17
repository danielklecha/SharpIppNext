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
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            dst.ResourceKOctets = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceKOctets);
            dst.ResourceSignature = map.MapFromDicSetNullable<OctetString[]?>(src, SystemAttribute.ResourceSignature);
            return dst;
        });

        mapper.CreateMap<SendResourceDataOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.ResourceKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceKOctets, src.ResourceKOctets.Value));
            if (src.ResourceSignature != null)
                dst.AddRange(src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.ResourceSignature, x)));
            return dst;
        });
    }
}
