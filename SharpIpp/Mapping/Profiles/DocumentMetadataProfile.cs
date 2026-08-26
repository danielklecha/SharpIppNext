using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class DocumentMetadataProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<string[], DocumentMetadata>((src, map) => DocumentMetadata.Parse(src));
        mapper.CreateMap<object[], DocumentMetadata>((src, map) =>
            src.Length == 1 && src[0] is NoValue
                ? NoValue.GetNoValue<DocumentMetadata>()
                : map.Map<DocumentMetadata>(map.Map<string[]>(src)));
        mapper.CreateMap<NoValue, DocumentMetadata>((src, map) => NoValue.GetNoValue<DocumentMetadata>());
    }
}
