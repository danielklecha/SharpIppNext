using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class JobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new JobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobId);
            dst.JobUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobUri);
            return dst;
        });

        mapper.CreateMap<JobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobId != null)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId.Value));
            if (src.JobUri != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, src.JobUri.ToString()));
            return dst;
        });
    }
}
