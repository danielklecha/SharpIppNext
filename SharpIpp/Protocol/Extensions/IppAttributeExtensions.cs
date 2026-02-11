using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Protocol.Extensions;
/// <summary>
/// Extension methods for <see cref="IppAttribute"/>.
/// </summary>
public static class IppAttributeExtensions
{
    private class CollectionContext
    {
        public string? LastMemberName { get; set; }
        public bool NeedsMemberNameTag { get; set; } = true;
    }

    /// <summary>
    /// Converts a collection of <see cref="IppAttribute"/> to a dictionary where the key is the attribute name and the value is an array of attributes with that name.
    /// Handles collection tags by grouping attributes within the collection.
    /// </summary>
    /// <param name="attributes">The collection of attributes.</param>
    /// <returns>A dictionary of attributes grouped by name.</returns>
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

    /// <summary>
    /// Wraps a collection of <see cref="IppAttribute"/> with <see cref="Tag.BegCollection"/> and <see cref="Tag.EndCollection"/> tags.
    /// Internal collection attributes are correctly named to reflect their membership.
    /// </summary>
    /// <param name="attributes">The collection of attributes to wrap.</param>
    /// <param name="collectionName">The name of the collection.</param>
    /// <returns>An enumerable of attributes including the collection tags.</returns>
    public static IEnumerable<IppAttribute> ToBegCollection(this IEnumerable<IppAttribute> attributes, string collectionName)
    {
        var stack = new Stack<CollectionContext>();
        stack.Push(new CollectionContext()); // Root context
        
        yield return new IppAttribute(Tag.BegCollection, collectionName, NoValue.Instance);
        
        foreach (var attribute in attributes)
        {


            var context = stack.Peek();

            if (attribute.Tag == Tag.EndCollection)
            {
                yield return new IppAttribute(attribute.Tag, string.Empty, attribute.Value);
                if (stack.Count > 1) 
                {
                    stack.Pop();
                }
                else
                {
                    throw new System.ArgumentException(
                        "Unexpected EndCollection at root level. ToBegCollection manages the outer EndCollection.",
                        nameof(attributes));
                }
                continue;
            }

            if (attribute.Tag == Tag.MemberAttrName)
            {
                string? name = attribute.Value as string;
                if (!string.IsNullOrEmpty(name))
                {
                    context.LastMemberName = name;
                    context.NeedsMemberNameTag = false; // We just emitted the name tag
                }
                yield return new IppAttribute(attribute.Tag, string.Empty, attribute.Value);
                continue;
            }

            // check if we are dealing with a new member or the same member
            string? memberName = !string.IsNullOrEmpty(attribute.Name) ? attribute.Name : context.LastMemberName;
            
            if (memberName == null)
            {
                throw new System.ArgumentException($"Attribute without name found. Tag: {attribute.Tag}", nameof(attributes));
            }

            if (memberName != context.LastMemberName)
            {
                // New member name
                yield return new IppAttribute(Tag.MemberAttrName, string.Empty, memberName);
                context.LastMemberName = memberName;
                context.NeedsMemberNameTag = true; // Next value dealing with this name will need a tag (empty if multi-value)
            }
            else if (context.NeedsMemberNameTag)
            {
                // Same member name, but we need a tag (either because it's a multi-value continuation, or we haven't emitted one yet for this value?
                // Wait.
                // If NeedsMemberNameTag is true.
                // It means "The previous element was a Value".
                // So this is a SECOND value for the same name.
                // So we need MemberAttrName("").
                yield return new IppAttribute(Tag.MemberAttrName, string.Empty, string.Empty);
            }
            // else: NeedsMemberNameTag is false.
            // This happens if the previous element was `MemberAttrName("foo")`.
            // And current `memberName` matches "foo".
            // So we strictly DO NOT emit a MemberAttrName tag.
            
            yield return new IppAttribute(attribute.Tag, string.Empty, attribute.Value);
            
            // After emitting a Value, the next element (if same name) WILL need a MemberAttrName("") tag.
            // So we set NeedsMemberNameTag = true.
            context.NeedsMemberNameTag = true;

            if (attribute.Tag == Tag.BegCollection)
            {
                stack.Push(new CollectionContext());
            }
        }
        
        yield return new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance);
    }

    /// <summary>
    /// Flattens a collection of <see cref="IppAttribute"/> that starts with a <see cref="Tag.BegCollection"/> tag.
    /// Internal collection attributes are renamed to their original member names.
    /// </summary>
    /// <param name="attributes">The collection of attributes starting with a collection tag.</param>
    /// <returns>An enumerable of flattened attributes.</returns>
    public static IEnumerable<IppAttribute> FromBegCollection(this IEnumerable<IppAttribute> attributes)
    {
        var stack = new Stack<(string CollectionName, string? ParentLastMemberName)>();
        string? lastMemberName = null;
        string? nextMemberName = null;
        bool isFirst = true;

        foreach (IppAttribute attribute in attributes)
        {
            if (isFirst)
            {
                if (attribute.Tag != Tag.BegCollection)
                {
                    throw new System.ArgumentException("First attribute must be BegCollection", nameof(attributes));
                }
                isFirst = false;
                continue;
            }

            if (attribute.Tag == Tag.MemberAttrName)
            {
                if (nextMemberName != null)
                {
                    throw new System.ArgumentException("MemberAttrName cannot be followed by another MemberAttrName", nameof(attributes));
                }

                string? newName = attribute.Value as string;
                if (!string.IsNullOrEmpty(newName))
                {
                    lastMemberName = newName;
                }
                else if (lastMemberName == null)
                {
                    throw new System.ArgumentException("MemberAttrName with empty value must follow a named MemberAttrName", nameof(attributes));
                }
                nextMemberName = lastMemberName;
                continue;
            }
            
            if (attribute.Tag == Tag.EndCollection)
            {
                if (nextMemberName != null)
                {
                    throw new System.ArgumentException("MemberAttrName cannot be followed by EndCollection", nameof(attributes));
                }

                if (stack.Count == 0)
                {
                    yield break;
                }
                var (collectionName, parentLastMemberName) = stack.Pop();
                yield return new IppAttribute(Tag.EndCollection, collectionName, attribute.Value);
                lastMemberName = parentLastMemberName;
                continue;
            }

            if (nextMemberName == null)
            {
                if (!string.IsNullOrEmpty(attribute.Name))
                {
                    nextMemberName = attribute.Name;
                }
                else
                {
                    throw new System.ArgumentException($"Attribute value must be preceded by MemberAttrName. Tag: {attribute.Tag}", nameof(attributes));
                }
            }

            if (attribute.Tag == Tag.BegCollection)
            {
                yield return new IppAttribute(Tag.BegCollection, nextMemberName, attribute.Value);
                stack.Push((nextMemberName, lastMemberName));
                lastMemberName = null;
                nextMemberName = null;
            }
            else
            {
                yield return new IppAttribute(attribute.Tag, nextMemberName!, attribute.Value);
                nextMemberName = null;
            }
        }
    }
}
