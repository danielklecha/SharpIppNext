using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

internal static class OverrideInstructionMappingHelper
{
    public static OverrideInstruction MapDictionaryToOverrideInstruction(IDictionary<string, IppAttribute[]> src, IMapperApplier map)
    {
        var pageRanges = map.MapFromDicNullable<IppValue<Range[]>?>(src, "pages");
        var documentNumberRanges = map.MapFromDicNullable<IppValue<Range[]>?>(src, "document-numbers");
        var documentCopyRanges = map.MapFromDicNullable<IppValue<Range[]>?>(src, "document-copies");

        var overrideTemplateMembers = new Dictionary<string, IppAttribute[]>(src.Count);
        foreach (var kvp in src)
        {
            if (kvp.Key != "pages" && kvp.Key != "document-numbers" && kvp.Key != "document-copies")
            {
                overrideTemplateMembers[kvp.Key] = kvp.Value;
            }
        }

        JobTemplateAttributes? overrideTemplateAttributes = null;
        if (overrideTemplateMembers.Count > 0)
        {
            overrideTemplateAttributes = map.Map<IDictionary<string, IppAttribute[]>, JobTemplateAttributes>(overrideTemplateMembers);
        }

        return new OverrideInstruction
        {
            PageRanges = pageRanges,
            DocumentNumberRanges = documentNumberRanges,
            DocumentCopyRanges = documentCopyRanges,
            JobTemplateAttributes = overrideTemplateAttributes
        };
    }

    public static OverrideInstruction MapConcreteDictionaryToOverrideInstruction(Dictionary<string, IppAttribute[]> src, IMapperApplier map)
    {
        return map.Map<IDictionary<string, IppAttribute[]>, OverrideInstruction>(src);
    }

    public static IEnumerable<IppAttribute> MapOverrideInstructionToIppAttributes(OverrideInstruction src, IMapperApplier map)
    {
        var attributes = new List<IppAttribute>();

        if (src.PageRanges.HasValue)
        {
            if (!src.PageRanges.Value.IsValue)
            {
                attributes.Add(new IppAttribute(Tag.NoValue, "pages", NoValue.Instance));
            }
            else if (src.PageRanges.Value.Value != null)
            {
                foreach (var r in src.PageRanges.Value.Value)
                {
                    attributes.Add(new IppAttribute(Tag.RangeOfInteger, "pages", r));
                }
            }
        }

        if (src.DocumentNumberRanges.HasValue)
        {
            if (!src.DocumentNumberRanges.Value.IsValue)
            {
                attributes.Add(new IppAttribute(Tag.NoValue, "document-numbers", NoValue.Instance));
            }
            else if (src.DocumentNumberRanges.Value.Value != null)
            {
                foreach (var r in src.DocumentNumberRanges.Value.Value)
                {
                    attributes.Add(new IppAttribute(Tag.RangeOfInteger, "document-numbers", r));
                }
            }
        }

        if (src.DocumentCopyRanges.HasValue)
        {
            if (!src.DocumentCopyRanges.Value.IsValue)
            {
                attributes.Add(new IppAttribute(Tag.NoValue, "document-copies", NoValue.Instance));
            }
            else if (src.DocumentCopyRanges.Value.Value != null)
            {
                foreach (var r in src.DocumentCopyRanges.Value.Value)
                {
                    attributes.Add(new IppAttribute(Tag.RangeOfInteger, "document-copies", r));
                }
            }
        }

        if (src.JobTemplateAttributes != null)
        {
            var templateRequest = map.Map<IppRequestMessage>(src.JobTemplateAttributes);
            foreach (var attr in templateRequest.JobAttributes)
            {
                if (attr.Name != IppAttributeNames.Overrides && attr.Name != IppAttributeNames.OverridesActual)
                {
                    attributes.Add(attr);
                }
            }
        }

        return attributes;
    }

    public static List<IppAttribute> MapOverrideInstructionToList(OverrideInstruction src, IMapperApplier map)
    {
        return map.Map<IEnumerable<IppAttribute>>(src).ToList();
    }

    public static IDictionary<string, IppAttribute[]> MapOverrideInstructionToDictionary(OverrideInstruction src, IMapperApplier map)
    {
        return map.Map<List<IppAttribute>>(src).ToIppDictionary();
    }

    public static Dictionary<string, IppAttribute[]> MapOverrideInstructionToConcreteDictionary(OverrideInstruction src, IMapperApplier map)
    {
        return (Dictionary<string, IppAttribute[]>)map.Map<List<IppAttribute>>(src).ToIppDictionary();
    }
}
