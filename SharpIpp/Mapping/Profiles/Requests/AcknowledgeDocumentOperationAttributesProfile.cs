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
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentNumber) ?? 0;
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            dst.FetchStatusCode = map.MapFromDicNullable<IppStatusCode?>(src, IppAttributeNames.FetchStatusCode);
            dst.FetchStatusMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.FetchStatusMessage);
            return dst;
        });

        mapper.CreateMap<AcknowledgeDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, src.DocumentNumber));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.FetchStatusCode.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.FetchStatusCode, (int)src.FetchStatusCode.Value));
            if (src.FetchStatusMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.FetchStatusMessage, src.FetchStatusMessage));
            return dst;
        });
    }
}
