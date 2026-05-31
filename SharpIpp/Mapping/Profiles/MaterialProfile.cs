using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class MaterialProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, Material>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Material>();

            return new Material
            {
                MaterialAmount = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialAmount).ConvertCamelCaseToKebabCase()),
                MaterialColor = map.MapFromDicNullable<MaterialColor?>(src, nameof(Material.MaterialColor).ConvertCamelCaseToKebabCase()),
                MaterialDiameter = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialDiameter).ConvertCamelCaseToKebabCase()),
                MaterialFillDensity = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialFillDensity).ConvertCamelCaseToKebabCase()),
                MaterialKey = map.MapFromDicNullable<MaterialKey?>(src, nameof(Material.MaterialKey).ConvertCamelCaseToKebabCase()),
                MaterialName = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialName).ConvertCamelCaseToKebabCase()),
                MaterialPurpose = map.MapFromDicSetNullable<MaterialPurpose[]?>(src, nameof(Material.MaterialPurpose).ConvertCamelCaseToKebabCase()),
                MaterialRate = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialRate).ConvertCamelCaseToKebabCase()),
                MaterialRateUnits = map.MapFromDicNullable<MaterialRateUnits?>(src, nameof(Material.MaterialRateUnits).ConvertCamelCaseToKebabCase()),
                MaterialShellThickness = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialShellThickness).ConvertCamelCaseToKebabCase()),
                MaterialTemperature = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialTemperature).ConvertCamelCaseToKebabCase()),
                MaterialType = map.MapFromDicNullable<MaterialType?>(src, nameof(Material.MaterialType).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<Material, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.MaterialsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.MaterialAmount.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialAmount).ConvertCamelCaseToKebabCase(), src.MaterialAmount.Value));
            if (src.MaterialColor != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialColor).ConvertCamelCaseToKebabCase(), src.MaterialColor.Value.Value));
            if (src.MaterialDiameter.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialDiameter).ConvertCamelCaseToKebabCase(), src.MaterialDiameter.Value));
            if (src.MaterialFillDensity.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialFillDensity).ConvertCamelCaseToKebabCase(), src.MaterialFillDensity.Value));
            if (src.MaterialKey != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialKey).ConvertCamelCaseToKebabCase(), src.MaterialKey.Value.Value));
            if (src.MaterialName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(Material.MaterialName).ConvertCamelCaseToKebabCase(), src.MaterialName));
            if (src.MaterialPurpose != null) attributes.AddRange(src.MaterialPurpose.Select(x => new IppAttribute(Tag.Keyword, nameof(Material.MaterialPurpose).ConvertCamelCaseToKebabCase(), x.Value)));
            if (src.MaterialRate.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialRate).ConvertCamelCaseToKebabCase(), src.MaterialRate.Value));
            if (src.MaterialRateUnits != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialRateUnits).ConvertCamelCaseToKebabCase(), src.MaterialRateUnits.Value.Value));
            if (src.MaterialShellThickness.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialShellThickness).ConvertCamelCaseToKebabCase(), src.MaterialShellThickness.Value));
            if (src.MaterialTemperature.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialTemperature).ConvertCamelCaseToKebabCase(), src.MaterialTemperature.Value));
            if (src.MaterialType != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialType).ConvertCamelCaseToKebabCase(), src.MaterialType.Value.Value));
            return attributes;
        });
    }
}
