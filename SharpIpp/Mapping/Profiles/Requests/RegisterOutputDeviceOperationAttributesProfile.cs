using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class RegisterOutputDeviceOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, RegisterOutputDeviceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new RegisterOutputDeviceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            dst.OutputDeviceX509Certificate = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.OutputDeviceX509Certificate);
            dst.OutputDeviceX509Request = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.OutputDeviceX509Request);
            dst.PrinterServiceType = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, IppAttributeNames.PrinterServiceType);
            if (src.ContainsKey(IppAttributeNames.PrinterXriRequested))
                dst.PrinterXriRequested = src[IppAttributeNames.PrinterXriRequested].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return dst;
        });

        mapper.CreateMap<RegisterOutputDeviceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.OutputDeviceX509Certificate != null)
                dst.AddRange(src.OutputDeviceX509Certificate.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.OutputDeviceX509Certificate, x)));
            if (src.OutputDeviceX509Request != null)
                dst.AddRange(src.OutputDeviceX509Request.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.OutputDeviceX509Request, x)));
            if (src.PrinterServiceType != null)
                dst.AddRange(src.PrinterServiceType.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceType, map.Map<string>(x))));
            if (src.PrinterXriRequested != null)
                dst.AddRange(src.PrinterXriRequested.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterXriRequested)));
            return dst;
        });
    }
}
