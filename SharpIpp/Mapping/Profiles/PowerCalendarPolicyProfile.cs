using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerCalendarPolicyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerCalendarPolicy>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerCalendarPolicy>();

            return new PowerCalendarPolicy
            {
                CalendarId = map.MapFromDicNullable<int?>(src, "calendar-id"),
                DayOfMonth = map.MapFromDicNullable<int?>(src, "day-of-month"),
                DayOfWeek = map.MapFromDicNullable<int?>(src, "day-of-week"),
                Hour = map.MapFromDicNullable<int?>(src, "hour"),
                Minute = map.MapFromDicNullable<int?>(src, "minute"),
                Month = map.MapFromDicNullable<int?>(src, "month"),
                RequestPowerState = map.MapFromDicNullable<PowerState?>(src, "request-power-state"),
                RunOnce = map.MapFromDicNullable<bool?>(src, "run-once")
            };
        });

        mapper.CreateMap<PowerCalendarPolicy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerCalendarPolicyCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CalendarId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "calendar-id", src.CalendarId.Value));
            if (src.DayOfMonth.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "day-of-month", src.DayOfMonth.Value));
            if (src.DayOfWeek.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "day-of-week", src.DayOfWeek.Value));
            if (src.Hour.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "hour", src.Hour.Value));
            if (src.Minute.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "minute", src.Minute.Value));
            if (src.Month.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "month", src.Month.Value));
            if (src.RequestPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "request-power-state", map.Map<string>(src.RequestPowerState.Value)));
            if (src.RunOnce.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "run-once", src.RunOnce.Value));
            return attributes;
        });
    }
}
