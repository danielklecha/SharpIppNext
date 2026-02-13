using SharpIpp.Mapping;
using SharpIpp.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpIpp.Protocol.Models;
public class MediaSourceProperties
{
    /// <summary>
    /// type2 keyword
    /// </summary>
    public MediaSourceFeedDirection? MediaSourceFeedDirection { get; set; }

    /// <summary>
    /// type2 enum
    /// </summary>
    public Orientation? MediaSourceFeedOrientation { get; set; }

    public IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        if (MediaSourceFeedDirection.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaSourceFeedDirection).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaSourceFeedDirection.Value));
        if (MediaSourceFeedOrientation.HasValue)
            yield return new IppAttribute(Tag.Enum, nameof(MediaSourceFeedOrientation).ConvertCamelCaseToKebabCase(), (int)MediaSourceFeedOrientation.Value);
    }

    public static MediaSourceProperties Create(IMapperApplier mapper, Dictionary<string, IppAttribute[]> dict)
    {
        return new MediaSourceProperties
        {
            MediaSourceFeedDirection = mapper.MapFromDic<MediaSourceFeedDirection?>(dict, nameof(MediaSourceFeedDirection).ConvertCamelCaseToKebabCase()),
            MediaSourceFeedOrientation = mapper.MapFromDic<Orientation?>(dict, nameof(MediaSourceFeedOrientation).ConvertCamelCaseToKebabCase())
        };
    }
}
