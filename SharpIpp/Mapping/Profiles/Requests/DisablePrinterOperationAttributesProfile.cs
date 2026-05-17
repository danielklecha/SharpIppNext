using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class DisablePrinterOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DisablePrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new DisablePrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<DisablePrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PrinterMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterMessageFromOperator, src.PrinterMessageFromOperator));
            return dst;
        });
    }
}
