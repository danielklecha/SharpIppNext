using System;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class UriProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<string, Uri?>((src, dst, map) =>
        {
            if (Uri.TryCreate(src, UriKind.RelativeOrAbsolute, out var uri))
            {
                return uri;
            }
            return null;
        });
        
        mapper.CreateMap<Uri, string>((src, dst, map) => src.ToString());
        mapper.CreateMap<SharpIpp.Protocol.Models.NoValue, Uri?>((src, dst, map) => null);
    }
}
