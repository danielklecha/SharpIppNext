using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class PausePrinterAfterCurrentJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PausePrinterAfterCurrentJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PausePrinterAfterCurrentJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<PausePrinterAfterCurrentJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PrinterMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterMessageFromOperator, src.PrinterMessageFromOperator));
            return dst;
        });
    }
}
