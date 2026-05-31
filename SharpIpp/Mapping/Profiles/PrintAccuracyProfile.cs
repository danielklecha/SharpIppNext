using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrintAccuracyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintAccuracy>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrintAccuracy>();

            return new PrintAccuracy
            {
                AccuracyUnits = map.MapFromDicNullable<AccuracyUnits?>(src, nameof(PrintAccuracy.AccuracyUnits).ConvertCamelCaseToKebabCase()),
                XAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.XAccuracy).ConvertCamelCaseToKebabCase()),
                YAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.YAccuracy).ConvertCamelCaseToKebabCase()),
                ZAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.ZAccuracy).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<PrintAccuracy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.PrintAccuracy, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.AccuracyUnits != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(PrintAccuracy.AccuracyUnits).ConvertCamelCaseToKebabCase(), src.AccuracyUnits.Value.Value));
            if (src.XAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.XAccuracy).ConvertCamelCaseToKebabCase(), src.XAccuracy.Value));
            if (src.YAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.YAccuracy).ConvertCamelCaseToKebabCase(), src.YAccuracy.Value));
            if (src.ZAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.ZAccuracy).ConvertCamelCaseToKebabCase(), src.ZAccuracy.Value));
            return attributes;
        });
    }
}
