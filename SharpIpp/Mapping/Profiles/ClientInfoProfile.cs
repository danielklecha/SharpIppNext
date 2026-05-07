using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class ClientInfoProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ClientInfo>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<ClientInfo>();

            var dst = new ClientInfo
            {
                ClientName = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientName).ConvertCamelCaseToKebabCase()),
                ClientPatches = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientPatches).ConvertCamelCaseToKebabCase()),
                ClientStringVersion = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientStringVersion).ConvertCamelCaseToKebabCase()),
                ClientVersion = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientVersion).ConvertCamelCaseToKebabCase()),
            };

            var clientType = map.MapFromDicNullable<int?>(src, nameof(ClientInfo.ClientType).ConvertCamelCaseToKebabCase());
            if (clientType.HasValue)
                dst.ClientType = (ClientType)clientType.Value;

            return dst;
        });
        mapper.CreateMap<ClientInfo, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.ClientInfo, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ClientName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(ClientInfo.ClientName).ConvertCamelCaseToKebabCase(), src.ClientName));
            if (src.ClientPatches != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(ClientInfo.ClientPatches).ConvertCamelCaseToKebabCase(), src.ClientPatches));
            if (src.ClientStringVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(ClientInfo.ClientStringVersion).ConvertCamelCaseToKebabCase(), src.ClientStringVersion));
            if (src.ClientType.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, nameof(ClientInfo.ClientType).ConvertCamelCaseToKebabCase(), (int)src.ClientType.Value));
            if (src.ClientVersion != null)
                attributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, nameof(ClientInfo.ClientVersion).ConvertCamelCaseToKebabCase(), src.ClientVersion));
            return attributes;
        });
    }
}
