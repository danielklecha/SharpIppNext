using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CUPSGetPrintersOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CUPSGetPrintersOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CUPSGetPrintersOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.FirstPrinterName = map.MapFromDicNullable<string?>(src, JobAttribute.FirstPrinterName);
            dst.Limit = map.MapFromDicNullable<int?>(src, JobAttribute.Limit);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId);
            dst.PrinterLocation = map.MapFromDicNullable<string?>(src, JobAttribute.PrinterLocation);
            dst.PrinterType = map.MapFromDicNullable<PrinterType?>(src, JobAttribute.PrinterType);
            dst.PrinterTypeMask = map.MapFromDicNullable<PrinterType?>(src, JobAttribute.PrinterTypeMask);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<CUPSGetPrintersOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.FirstPrinterName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.FirstPrinterName, src.FirstPrinterName));
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Limit, src.Limit.Value));
            if (src.PrinterId != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, src.PrinterId.Value));
            if (src.PrinterLocation != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.PrinterLocation, src.PrinterLocation));
            if (src.PrinterType != null)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.PrinterType, (int)src.PrinterType.Value));
            if (src.PrinterTypeMask != null)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.PrinterTypeMask, (int)src.PrinterTypeMask.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            return dst;
        });
    }
}
