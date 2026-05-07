using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerStateMonitorProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateMonitor>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerStateMonitor>();

            var dst = new PowerStateMonitor
            {
                CurrentMonthKwh = map.MapFromDicNullable<int?>(src, "current-month-kwh"),
                CurrentWatts = map.MapFromDicNullable<int?>(src, "current-watts"),
                LifetimeKwh = map.MapFromDicNullable<int?>(src, "lifetime-kwh"),
                MetersAreActual = map.MapFromDicNullable<bool?>(src, "meters-are-actual"),
                PowerState = map.MapFromDicNullable<PowerState?>(src, "power-state"),
                PowerStateMessage = map.MapFromDicNullable<string?>(src, "power-state-message"),
                PowerUsageIsRmsWatts = map.MapFromDicNullable<bool?>(src, "power-usage-is-rms-watts")
            };
            if (src.ContainsKey("valid-request-power-state"))
                dst.ValidRequestPowerStates = src["valid-request-power-state"].Select(x => (IppOperation)Enum.Parse(typeof(IppOperation), x.Value.ToString()!)).ToArray();
            return dst;
        });

        mapper.CreateMap<PowerStateMonitor, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateMonitorCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CurrentMonthKwh.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "current-month-kwh", src.CurrentMonthKwh.Value));
            if (src.CurrentWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "current-watts", src.CurrentWatts.Value));
            if (src.LifetimeKwh.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "lifetime-kwh", src.LifetimeKwh.Value));
            if (src.MetersAreActual.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "meters-are-actual", src.MetersAreActual.Value));
            if (src.PowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "power-state", map.Map<string>(src.PowerState.Value)));
            if (src.PowerStateMessage != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", src.PowerStateMessage));
            if (src.PowerUsageIsRmsWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "power-usage-is-rms-watts", src.PowerUsageIsRmsWatts.Value));
            if (src.ValidRequestPowerStates != null)
                attributes.AddRange(src.ValidRequestPowerStates.Select(x => new IppAttribute(Tag.Enum, "valid-request-power-state", (int)x)));
            return attributes;
        });
    }
}
