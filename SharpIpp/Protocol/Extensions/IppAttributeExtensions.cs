using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Protocol.Extensions;
public static class IppAttributeExtensions
{
    public static Dictionary<string, IppAttribute[]> ToIppDictionary(this IEnumerable<IppAttribute> attributes)
    {
        Dictionary<string, List<IppAttribute>> groups = new Dictionary<string, List<IppAttribute>>();
        int level = 0;
        string? collectionName = null;
        foreach (var attribute in attributes)
        {
            switch (attribute.Tag)
            {
                case Tag.BegCollection:
                    if (level == 0)
                        collectionName = attribute.Name;
                    level++;
                    break;
                case Tag.EndCollection:
                    level--;
                    if (level == 0)
                        collectionName = null;
                    break;
            }
            var groupName = collectionName ?? attribute.Name;
            if (groups.TryGetValue(groupName, out List<IppAttribute> value))
                value.Add(attribute);
            else
                groups.Add(groupName, new List<IppAttribute> { attribute });
        }
        return groups.ToDictionary(x => x.Key, x => x.Value.ToArray());
    }

    public static IEnumerable<IppAttribute> ToBegCollection(this IEnumerable<IppAttribute> attributes, string collectionName)
    {
        int level = 0;
        yield return new IppAttribute(Tag.BegCollection, collectionName, NoValue.Instance);
        foreach (var attribute in attributes)
        {
            switch (attribute.Tag)
            {
                case Tag.BegCollection:
                    if (level == 0)
                    {
                        yield return new IppAttribute(Tag.MemberAttrName, string.Empty, attribute.Name);
                        yield return new IppAttribute(attribute.Tag, string.Empty, attribute.Value);
                    }
                    else
                    {
                        yield return new IppAttribute(attribute.Tag, attribute.Name, attribute.Value);
                    }
                    level++;
                    break;
                case Tag.EndCollection:
                    level--;
                    yield return new IppAttribute(attribute.Tag, attribute.Name, attribute.Value);
                    break;
                default:
                    if(level == 0)
                    {
                        yield return new IppAttribute(Tag.MemberAttrName, string.Empty, attribute.Name);
                        yield return new IppAttribute(attribute.Tag, string.Empty, attribute.Value);
                    }
                    else
                    {
                        yield return new IppAttribute(attribute.Tag, attribute.Name, attribute.Value);
                    }
                    break;
            }
        }
        yield return new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance);
    }

    public static IEnumerable<IppAttribute> FromBegCollection(this IEnumerable<IppAttribute> attributes)
    {
        int level = 0;
        string? memberName = null;
        foreach (IppAttribute attribute in attributes)
        {
            switch (attribute.Tag)
            {
                case Tag.BegCollection:
                    if (level > 0)
                    {
                        var name = memberName ?? attribute.Name;
                        yield return new IppAttribute(attribute.Tag, name, attribute.Value);
                        memberName = null;
                    }
                    level++;
                    break;
                case Tag.EndCollection:
                    level--;
                    if (level > 0)
                    {
                        yield return new IppAttribute(attribute.Tag, attribute.Name, attribute.Value);
                    }
                    break;
                case Tag.MemberAttrName when (level == 1):
                    memberName = attribute.Value as string;
                    break;
                default:
                    if (memberName != null)
                    {
                        yield return new IppAttribute(attribute.Tag, memberName, attribute.Value);
                        memberName = null;
                    }
                    else
                    {
                        yield return new IppAttribute(attribute.Tag, attribute.Name, attribute.Value);
                    }
                    break;
            }
        }
    }
}
