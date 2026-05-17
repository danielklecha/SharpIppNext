using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AcknowledgeDocumentOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AcknowledgeDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AcknowledgeDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber) ?? 0;
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.FetchStatusCode = map.MapFromDicNullable<IppStatusCode?>(src, JobAttribute.FetchStatusCode);
            dst.FetchStatusMessage = map.MapFromDicNullable<string?>(src, JobAttribute.FetchStatusMessage);
            return dst;
        });

        mapper.CreateMap<AcknowledgeDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.FetchStatusCode.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)src.FetchStatusCode.Value));
            if (src.FetchStatusMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.FetchStatusMessage, src.FetchStatusMessage));
            return dst;
        });
    }
}
