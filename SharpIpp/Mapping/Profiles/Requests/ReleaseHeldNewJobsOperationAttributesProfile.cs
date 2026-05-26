using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ReleaseHeldNewJobsOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ReleaseHeldNewJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ReleaseHeldNewJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<ReleaseHeldNewJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PrinterMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterMessageFromOperator, src.PrinterMessageFromOperator));
            return dst;
        });
    }
}
