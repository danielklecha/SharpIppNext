using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrinterVolumeSupportedProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterVolumeSupported>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrinterVolumeSupported>();

            return new PrinterVolumeSupported
            {
                XDimension = map.MapFromDicNullable<int?>(src, nameof(PrinterVolumeSupported.XDimension).ConvertCamelCaseToKebabCase()),
                YDimension = map.MapFromDicNullable<int?>(src, nameof(PrinterVolumeSupported.YDimension).ConvertCamelCaseToKebabCase()),
                ZDimension = map.MapFromDicNullable<int?>(src, nameof(PrinterVolumeSupported.ZDimension).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<PrinterVolumeSupported, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.PrinterVolumeSupported, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.XDimension.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrinterVolumeSupported.XDimension).ConvertCamelCaseToKebabCase(), src.XDimension.Value));
            if (src.YDimension.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrinterVolumeSupported.YDimension).ConvertCamelCaseToKebabCase(), src.YDimension.Value));
            if (src.ZDimension.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrinterVolumeSupported.ZDimension).ConvertCamelCaseToKebabCase(), src.ZDimension.Value));
            return attributes;
        });
    }
}
