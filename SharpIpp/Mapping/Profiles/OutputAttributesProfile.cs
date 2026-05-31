using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class OutputAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OutputAttributes>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<OutputAttributes>();

            return new OutputAttributes
            {
                NoiseRemoval = map.MapFromDicNullable<int?>(src, nameof(OutputAttributes.NoiseRemoval).ConvertCamelCaseToKebabCase()),
                OutputCompressionQualityFactor = map.MapFromDicNullable<int?>(src, nameof(OutputAttributes.OutputCompressionQualityFactor).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<OutputAttributes, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.OutputAttributes, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.NoiseRemoval.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(OutputAttributes.NoiseRemoval).ConvertCamelCaseToKebabCase(), src.NoiseRemoval.Value));
            if (src.OutputCompressionQualityFactor.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(OutputAttributes.OutputCompressionQualityFactor).ConvertCamelCaseToKebabCase(), src.OutputCompressionQualityFactor.Value));
            return attributes;
        });
    }
}
