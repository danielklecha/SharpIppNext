using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PowerLogEntryProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerLogEntry>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerLogEntry>();

            return new PowerLogEntry
            {
                LogId = map.MapFromDicNullable<int?>(src, "log-id"),
                PowerState = map.MapFromDicNullable<PowerState?>(src, "power-state"),
                PowerStateDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, "power-state-date-time"),
                PowerStateMessage = map.MapFromDicNullable<string?>(src, "power-state-message")
            };
        });

        mapper.CreateMap<PowerLogEntry, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerLogCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.LogId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "log-id", src.LogId.Value));
            if (src.PowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "power-state", map.Map<string>(src.PowerState.Value)));
            if (src.PowerStateDateTime.HasValue)
                attributes.Add(new IppAttribute(Tag.DateTime, "power-state-date-time", src.PowerStateDateTime.Value));
            if (src.PowerStateMessage != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", src.PowerStateMessage));
            return attributes;
        });
    }
}
