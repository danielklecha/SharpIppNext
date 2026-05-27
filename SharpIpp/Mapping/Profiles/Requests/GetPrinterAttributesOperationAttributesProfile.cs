using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetPrinterAttributesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetPrinterAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetPrinterAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.FirstIndex = map.MapFromDicNullable<int?>(src, IppAttributeNames.FirstIndex);
            dst.Limit = map.MapFromDicNullable<int?>(src, IppAttributeNames.Limit);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterId);
            dst.DocumentFormat = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormat);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            return dst;
        });

        mapper.CreateMap<GetPrinterAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.FirstIndex != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.FirstIndex, src.FirstIndex.Value));
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Limit, src.Limit.Value));
            if (src.PrinterId != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PrinterId, src.PrinterId.Value));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, requestedAttribute)));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            return dst;
        });
    }
}
