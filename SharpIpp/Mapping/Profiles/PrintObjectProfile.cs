using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrintObjectProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintObject>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrintObject>();

            return new PrintObject
            {
                DocumentNumber = map.MapFromDicNullable<int?>(src, nameof(PrintObject.DocumentNumber).ConvertCamelCaseToKebabCase()),
                PrintObjectsSource = map.MapFromDicNullable<Uri?>(src, "print-objects-source"),
                TransformationMatrix = map.MapFromDicSetNullable<int[]?>(src, nameof(PrintObject.TransformationMatrix).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<PrintObject, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.PrintObjects, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DocumentNumber.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintObject.DocumentNumber).ConvertCamelCaseToKebabCase(), src.DocumentNumber.Value));
            if (src.PrintObjectsSource != null) attributes.Add(new IppAttribute(Tag.Uri, "print-objects-source", src.PrintObjectsSource.ToString()));
            if (src.TransformationMatrix != null)
                attributes.AddRange(src.TransformationMatrix.Select(x => new IppAttribute(Tag.Integer, nameof(PrintObject.TransformationMatrix).ConvertCamelCaseToKebabCase(), x)));
            return attributes;
        });
    }
}
