using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AcknowledgeIdentifyPrinterOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AcknowledgeIdentifyPrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AcknowledgeIdentifyPrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            return dst;
        });

        mapper.CreateMap<AcknowledgeIdentifyPrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            return dst;
        });
    }
}
