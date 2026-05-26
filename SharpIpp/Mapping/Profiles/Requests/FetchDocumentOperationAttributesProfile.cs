using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class FetchDocumentOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, FetchDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new FetchDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentNumber) ?? 0;
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            dst.CompressionAccepted = map.MapFromDicSetNullable<Compression[]?>(src, IppAttributeNames.CompressionAccepted);
            dst.DocumentFormatAccepted = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DocumentFormatAccepted);
            return dst;
        });

        mapper.CreateMap<FetchDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, src.DocumentNumber));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.CompressionAccepted != null)
                dst.AddRange(src.CompressionAccepted.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.CompressionAccepted, map.Map<string>(x))));
            if (src.DocumentFormatAccepted != null)
                dst.AddRange(src.DocumentFormatAccepted.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatAccepted, x)));
            return dst;
        });
    }
}
