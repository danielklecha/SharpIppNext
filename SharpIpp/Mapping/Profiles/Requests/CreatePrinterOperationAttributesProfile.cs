using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CreatePrinterOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CreatePrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CreatePrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUri);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ResourceIds);
            dst.PrinterServiceType = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, IppAttributeNames.PrinterServiceType);
            if(src.ContainsKey(IppAttributeNames.PrinterXriRequested))
                dst.PrinterXriRequested = src[IppAttributeNames.PrinterXriRequested].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return dst;
        });

        mapper.CreateMap<CreatePrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, x)));
            if (src.PrinterServiceType != null)
                dst.AddRange(src.PrinterServiceType.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceType, map.Map<string>(x))));
            if (src.PrinterXriRequested != null)
                dst.AddRange(src.PrinterXriRequested.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterXriRequested)));
            return dst;
        });
    }
}
