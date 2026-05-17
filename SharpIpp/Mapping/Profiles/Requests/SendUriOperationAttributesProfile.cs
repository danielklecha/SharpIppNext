using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SendUriOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SendUriOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SendUriOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>(src, dst);
            dst.DocumentUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.DocumentUri);
            if (src.TryGetValue(JobAttribute.DocumentAccess, out var documentAccess))
                dst.DocumentAccess = map.Map<DocumentAccess>(documentAccess.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<SendUriOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SendDocumentOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, src.DocumentUri.ToString()));
            if (src.DocumentAccess != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentAccess).ToBegCollection(JobAttribute.DocumentAccess));
            return dst;
        });
    }
}
