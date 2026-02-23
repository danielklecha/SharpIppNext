using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpIpp.Protocol.Models;
public class MediaSize
{
    /// <summary>
    /// integer(0:MAX))
    /// </summary>
    public int? XDimension { get; set; }

    /// <summary>
    /// integer(0:MAX))
    /// </summary>
    public int? YDimension { get; set; }

    public IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        if (XDimension.HasValue)
            yield return new IppAttribute(Tag.Integer, nameof(XDimension).ConvertCamelCaseToKebabCase(), XDimension.Value);
        if (YDimension.HasValue)
            yield return new IppAttribute(Tag.Integer, nameof(YDimension).ConvertCamelCaseToKebabCase(), YDimension.Value);
    }

    public static MediaSize Create(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper)
    {
        return new MediaSize
        {
            XDimension = mapper.MapFromDicNullable<int?>(dict, nameof(XDimension).ConvertCamelCaseToKebabCase()),
            YDimension = mapper.MapFromDicNullable<int?>(dict, nameof(YDimension).ConvertCamelCaseToKebabCase())
        };
    }
}
