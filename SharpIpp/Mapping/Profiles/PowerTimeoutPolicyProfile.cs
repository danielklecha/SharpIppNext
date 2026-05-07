using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerTimeoutPolicyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerTimeoutPolicy>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerTimeoutPolicy>();

            return new PowerTimeoutPolicy
            {
                RequestPowerState = map.MapFromDicNullable<PowerState?>(src, "request-power-state"),
                StartPowerState = map.MapFromDicNullable<PowerState?>(src, "start-power-state"),
                TimeoutId = map.MapFromDicNullable<int?>(src, "timeout-id"),
                TimeoutPredicate = map.MapFromDicNullable<string?>(src, "timeout-predicate"),
                TimeoutSeconds = map.MapFromDicNullable<int?>(src, "timeout-seconds")
            };
        });

        mapper.CreateMap<PowerTimeoutPolicy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerTimeoutPolicyCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.RequestPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "request-power-state", map.Map<string>(src.RequestPowerState.Value)));
            if (src.StartPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "start-power-state", map.Map<string>(src.StartPowerState.Value)));
            if (src.TimeoutId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "timeout-id", src.TimeoutId.Value));
            if (src.TimeoutPredicate != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "timeout-predicate", src.TimeoutPredicate));
            if (src.TimeoutSeconds.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "timeout-seconds", src.TimeoutSeconds.Value));
            return attributes;
        });
    }
}
