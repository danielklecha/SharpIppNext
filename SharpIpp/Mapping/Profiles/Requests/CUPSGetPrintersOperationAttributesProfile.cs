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
            dst.FirstPrinterName = map.MapFromDicNullable<string?>(src, IppAttributeNames.FirstPrinterName);
            dst.Limit = map.MapFromDicNullable<int?>(src, IppAttributeNames.Limit);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterId);
            dst.PrinterLocation = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterLocation);
            dst.PrinterType = map.MapFromDicNullable<PrinterType?>(src, IppAttributeNames.PrinterType);
            dst.PrinterTypeMask = map.MapFromDicNullable<PrinterType?>(src, IppAttributeNames.PrinterTypeMask);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<CUPSGetPrintersOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.FirstPrinterName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.FirstPrinterName, src.FirstPrinterName));
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Limit, src.Limit.Value));
            if (src.PrinterId != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PrinterId, src.PrinterId.Value));
            if (src.PrinterLocation != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterLocation, src.PrinterLocation));
            if (src.PrinterType != null)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrinterType, (int)src.PrinterType.Value));
            if (src.PrinterTypeMask != null)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrinterTypeMask, (int)src.PrinterTypeMask.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, requestedAttribute)));
            return dst;
        });
    }
}
