using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class OctetStringProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<OctetString>();
        mapper.CreateIppMap<OctetString, byte[]>((src, map) => src.Value);
        mapper.CreateIppMap<byte[], OctetString>((src, map) => new OctetString(src));
        mapper.CreateIppMap<OctetString, string>((src, map) => src.ToString());
        mapper.CreateIppMap<string, OctetString>((src, map) => new OctetString(src));
        mapper.CreateIppMap<NoValue, OctetString>((src, map) => NoValue.GetNoValue<OctetString>());
    }
}
