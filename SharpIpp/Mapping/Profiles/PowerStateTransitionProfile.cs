using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerStateTransitionProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateTransition>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerStateTransition>();

            return new PowerStateTransition
            {
                EndPowerState = map.MapFromDicNullable<PowerState?>(src, "end-power-state"),
                StartPowerState = map.MapFromDicNullable<PowerState?>(src, "start-power-state"),
                StateTransitionSeconds = map.MapFromDicNullable<int?>(src, "state-transition-seconds")
            };
        });

        mapper.CreateMap<PowerStateTransition, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateTransitionsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.EndPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "end-power-state", map.Map<string>(src.EndPowerState.Value)));
            if (src.StartPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "start-power-state", map.Map<string>(src.StartPowerState.Value)));
            if (src.StateTransitionSeconds.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "state-transition-seconds", src.StateTransitionSeconds.Value));
            return attributes;
        });
    }
}
