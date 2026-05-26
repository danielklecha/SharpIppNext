using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class IdentifyPrinterOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, IdentifyPrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new IdentifyPrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.IdentifyActions = map.MapFromDicSetNullable<IdentifyAction[]?>(src, IppAttributeNames.IdentifyActions);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            dst.JobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobId);
            dst.Message = map.MapFromDicNullable<string?>(src, IppAttributeNames.Message);
            return dst;
        });

        mapper.CreateMap<IdentifyPrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.IdentifyActions != null)
                dst.AddRange(src.IdentifyActions.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.IdentifyActions, map.Map<string>(x))));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.JobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId.Value));
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.Message, src.Message));
            return dst;
        });
    }
}
