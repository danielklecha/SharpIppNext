using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AcknowledgeJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AcknowledgeJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AcknowledgeJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.OutputDeviceJobStates = map.MapFromDicSetNullable<JobState[]?>(src, JobAttribute.OutputDeviceJobStates);
            dst.FetchStatusCode = map.MapFromDicNullable<IppStatusCode?>(src, JobAttribute.FetchStatusCode);
            dst.FetchStatusMessage = map.MapFromDicNullable<string?>(src, JobAttribute.FetchStatusMessage);
            return dst;
        });

        mapper.CreateMap<AcknowledgeJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.OutputDeviceJobStates != null)
                dst.AddRange(src.OutputDeviceJobStates.Select(x => new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)x)));
            if (src.FetchStatusCode.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)src.FetchStatusCode.Value));
            if (src.FetchStatusMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.FetchStatusMessage, src.FetchStatusMessage));
            return dst;
        });
    }
}
