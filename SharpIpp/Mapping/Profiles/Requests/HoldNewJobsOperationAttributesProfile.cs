using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class HoldNewJobsOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, HoldNewJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new HoldNewJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<HoldNewJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PrinterMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterMessageFromOperator, src.PrinterMessageFromOperator));
            return dst;
        });
    }
}
