using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class UpdateDocumentStatusOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, UpdateDocumentStatusOperationAttributes>((src, dst, map) =>
        {
            dst ??= new UpdateDocumentStatusOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentNumber) ?? 0;
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            return dst;
        });

        mapper.CreateMap<UpdateDocumentStatusOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, src.DocumentNumber));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            return dst;
        });
    }
}
