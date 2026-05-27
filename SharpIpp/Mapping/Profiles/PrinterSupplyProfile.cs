using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrinterSupplyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterSupply>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrinterSupply>();

            return new PrinterSupply
            {
                Type = map.MapFromDicNullable<PrinterSupplyType?>(src, "type"),
                Level = map.MapFromDicNullable<int?>(src, "level"),
                MaxCapacity = map.MapFromDicNullable<int?>(src, "max-capacity"),
                ColorName = map.MapFromDicNullable<string?>(src, "color-name"),
                MarkerName = map.MapFromDicNullable<string?>(src, "marker-name"),
                MarkerType = map.MapFromDicNullable<MarkerType?>(src, "marker-type"),
                Unit = map.MapFromDicNullable<CapacityUnit?>(src, "unit"),
            };
        });
        mapper.CreateMap<PrinterSupply, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.PrinterSupply, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.Type != null) attrs.Add(new IppAttribute(Tag.Keyword, "type", src.Type.Value.Value));
            if (src.Level.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "level", src.Level.Value));
            if (src.MaxCapacity.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "max-capacity", src.MaxCapacity.Value));
            if (src.ColorName != null) attrs.Add(new IppAttribute(Tag.Keyword, "color-name", src.ColorName));
            if (src.MarkerName != null) attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, "marker-name", src.MarkerName));
            if (src.MarkerType != null) attrs.Add(new IppAttribute(Tag.Keyword, "marker-type", src.MarkerType.Value.Value));
            if (src.Unit != null) attrs.Add(new IppAttribute(Tag.Keyword, "unit", src.Unit.Value.Value));
            return attrs;
        });
    }
}
