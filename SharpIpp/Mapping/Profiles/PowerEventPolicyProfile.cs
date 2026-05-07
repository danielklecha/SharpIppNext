using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerEventPolicyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerEventPolicy>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerEventPolicy>();

            return new PowerEventPolicy
            {
                EventId = map.MapFromDicNullable<int?>(src, "event-id"),
                EventName = map.MapFromDicNullable<string?>(src, "event-name"),
                RequestPowerState = map.MapFromDicNullable<PowerState?>(src, "request-power-state")
            };
        });

        mapper.CreateMap<PowerEventPolicy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerEventPolicyCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.EventId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "event-id", src.EventId.Value));
            if (src.EventName != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "event-name", src.EventName));
            if (src.RequestPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "request-power-state", map.Map<string>(src.RequestPowerState.Value)));
            return attributes;
        });
    }
}
