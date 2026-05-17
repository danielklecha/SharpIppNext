using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CancelJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.Message = map.MapFromDicNullable<string?>(src, JobAttribute.Message);
            return dst;
        });

        mapper.CreateMap<CancelJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.Message, src.Message));
            return dst;
        });
    }
}
