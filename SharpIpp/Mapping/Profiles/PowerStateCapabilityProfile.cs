using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerStateCapabilityProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateCapability>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerStateCapability>();

            return new PowerStateCapability
            {
                CanAcceptJobs = map.MapFromDicNullable<bool?>(src, "can-accept-jobs"),
                CanProcessJobs = map.MapFromDicNullable<bool?>(src, "can-process-jobs"),
                PowerActiveWatts = map.MapFromDicNullable<int?>(src, "power-active-watts"),
                PowerInactiveWatts = map.MapFromDicNullable<int?>(src, "power-inactive-watts"),
                PowerState = map.MapFromDicNullable<PowerState?>(src, "power-state")
            };
        });

        mapper.CreateMap<PowerStateCapability, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateCapabilitiesCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CanAcceptJobs.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "can-accept-jobs", src.CanAcceptJobs.Value));
            if (src.CanProcessJobs.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "can-process-jobs", src.CanProcessJobs.Value));
            if (src.PowerActiveWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "power-active-watts", src.PowerActiveWatts.Value));
            if (src.PowerInactiveWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "power-inactive-watts", src.PowerInactiveWatts.Value));
            if (src.PowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "power-state", map.Map<string>(src.PowerState.Value)));
            return attributes;
        });
    }
}
