using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class DocumentMetadataProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<string[], DocumentMetadata>((src, map) => Parse(src));
        mapper.CreateMap<object[], DocumentMetadata>((src, map) =>
            src.Length == 1 && src[0] is NoValue
                ? NoValue.GetNoValue<DocumentMetadata>()
                : map.Map<DocumentMetadata>(map.Map<string[]>(src)));
        mapper.CreateMap<NoValue, DocumentMetadata>((src, map) => NoValue.GetNoValue<DocumentMetadata>());
    }

    private static DocumentMetadata Parse(IEnumerable<string> values)
    {
        var metadata = new DocumentMetadata();
        foreach (var item in values)
        {
            if (string.IsNullOrEmpty(item))
                continue;

            var eqIndex = item.IndexOf('=');
            if (eqIndex > 0)
            {
                var keyword = item.Substring(0, eqIndex);
                var val = item.Substring(eqIndex + 1);
                metadata[keyword] = val;
            }
        }
        return metadata;
    }
}
