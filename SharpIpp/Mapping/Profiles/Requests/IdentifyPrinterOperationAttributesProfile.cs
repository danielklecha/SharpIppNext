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
            dst.IdentifyActions = map.MapFromDicSetNullable<IdentifyAction[]?>(src, JobAttribute.IdentifyActions);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.JobId = map.MapFromDicNullable<int?>(src, JobAttribute.JobId);
            dst.Message = map.MapFromDicNullable<string?>(src, JobAttribute.Message);
            return dst;
        });

        mapper.CreateMap<IdentifyPrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.IdentifyActions != null)
                dst.AddRange(src.IdentifyActions.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.IdentifyActions, map.Map<string>(x))));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.JobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobId, src.JobId.Value));
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.Message, src.Message));
            return dst;
        });
    }
}
