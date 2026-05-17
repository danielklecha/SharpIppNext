using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ValidateDocumentOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ValidateDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ValidateDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, PrintJobOperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<ValidateDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<PrintJobOperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });
    }
}
