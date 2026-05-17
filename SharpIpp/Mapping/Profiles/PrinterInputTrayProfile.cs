using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrinterInputTrayProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterInputTray>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrinterInputTray>();

            return new PrinterInputTray
            {
                Type = map.MapFromDicNullable<string?>(src, "type"),
                Level = map.MapFromDicNullable<int?>(src, "level"),
                Status = map.MapFromDicNullable<string?>(src, "status"),
                MediaSizeX = map.MapFromDicNullable<int?>(src, "media-size-x"),
                MediaSizeY = map.MapFromDicNullable<int?>(src, "media-size-y"),
                MediaColor = map.MapFromDicNullable<string?>(src, "media-color"),
                MediaInfo = map.MapFromDicNullable<string?>(src, "media-info"),
                MediaType = map.MapFromDicNullable<string?>(src, "media-type"),
                Unit = map.MapFromDicNullable<string?>(src, "unit"),
                FeedOrientation = map.MapFromDicNullable<string?>(src, "feed-orientation"),
            };
        });
        mapper.CreateMap<PrinterInputTray, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterInputTray, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.Type != null) attrs.Add(new IppAttribute(Tag.Keyword, "type", src.Type));
            if (src.Level.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "level", src.Level.Value));
            if (src.Status != null) attrs.Add(new IppAttribute(Tag.Keyword, "status", src.Status));
            if (src.MediaSizeX.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "media-size-x", src.MediaSizeX.Value));
            if (src.MediaSizeY.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "media-size-y", src.MediaSizeY.Value));
            if (src.MediaColor != null) attrs.Add(new IppAttribute(Tag.Keyword, "media-color", src.MediaColor));
            if (src.MediaInfo != null) attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, "media-info", src.MediaInfo));
            if (src.MediaType != null) attrs.Add(new IppAttribute(Tag.Keyword, "media-type", src.MediaType));
            if (src.Unit != null) attrs.Add(new IppAttribute(Tag.Keyword, "unit", src.Unit));
            if (src.FeedOrientation != null) attrs.Add(new IppAttribute(Tag.Keyword, "feed-orientation", src.FeedOrientation));
            return attrs;
        });
    }
}
