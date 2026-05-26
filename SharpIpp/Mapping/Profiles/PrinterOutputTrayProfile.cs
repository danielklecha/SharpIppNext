using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrinterOutputTrayProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterOutputTray>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrinterOutputTray>();

            return new PrinterOutputTray
            {
                Type = map.MapFromDicNullable<string?>(src, "type"),
                Level = map.MapFromDicNullable<int?>(src, "level"),
                Status = map.MapFromDicNullable<string?>(src, "status"),
                Unit = map.MapFromDicNullable<string?>(src, "unit"),
                StackingOrder = map.MapFromDicNullable<string?>(src, "stackingorder"),
                PageDelivery = map.MapFromDicNullable<string?>(src, "pagedelivery"),
            };
        });
        mapper.CreateMap<PrinterOutputTray, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.PrinterOutputTray, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.Type != null) attrs.Add(new IppAttribute(Tag.Keyword, "type", src.Type));
            if (src.Level.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "level", src.Level.Value));
            if (src.Status != null) attrs.Add(new IppAttribute(Tag.Keyword, "status", src.Status));
            if (src.Unit != null) attrs.Add(new IppAttribute(Tag.Keyword, "unit", src.Unit));
            if (src.StackingOrder != null) attrs.Add(new IppAttribute(Tag.Keyword, "stackingorder", src.StackingOrder));
            if (src.PageDelivery != null) attrs.Add(new IppAttribute(Tag.Keyword, "pagedelivery", src.PageDelivery));
            return attrs;
        });
    }
}
