using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class PowerStateCounterProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateCounter>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PowerStateCounter>();

            return new PowerStateCounter
            {
                HibernateTransitions = map.MapFromDicNullable<int?>(src, "hibernate-transitions"),
                OnTransitions = map.MapFromDicNullable<int?>(src, "on-transitions"),
                StandbyTransitions = map.MapFromDicNullable<int?>(src, "standby-transitions"),
                SuspendTransitions = map.MapFromDicNullable<int?>(src, "suspend-transitions")
            };
        });

        mapper.CreateMap<PowerStateCounter, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateCountersCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.HibernateTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "hibernate-transitions", src.HibernateTransitions.Value));
            if (src.OnTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "on-transitions", src.OnTransitions.Value));
            if (src.StandbyTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "standby-transitions", src.StandbyTransitions.Value));
            if (src.SuspendTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "suspend-transitions", src.SuspendTransitions.Value));
            return attributes;
        });
    }
}
