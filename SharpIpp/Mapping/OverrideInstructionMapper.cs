using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

[MapperConfiguration(1)]
internal static class OverrideInstructionMapper
{
    public static void Configure(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OverrideInstruction>((src, map) =>
        {
            var pageRanges = map.MapFromDicNullable<IppValue<Range[]>?>(src, "pages");
            var documentNumberRanges = map.MapFromDicNullable<IppValue<Range[]>?>(src, "document-numbers");
            var documentCopyRanges = map.MapFromDicNullable<IppValue<Range[]>?>(src, "document-copies");

            var overrideTemplateMembers = src
                .Where(x => x.Key != "pages" && x.Key != "document-numbers" && x.Key != "document-copies")
                .ToDictionary(x => x.Key, x => x.Value);

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
        });

        mapper.CreateMap<OverrideInstruction, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();

            if (src.PageRanges.HasValue)
            {
                if (!src.PageRanges.Value.IsValue)
                    attributes.Add(new IppAttribute(Tag.NoValue, "pages", NoValue.Instance));
                else if (src.PageRanges.Value.Value != null)
                    attributes.AddRange(src.PageRanges.Value.Value.Select(x => new IppAttribute(Tag.RangeOfInteger, "pages", x)));
            }

            if (src.DocumentNumberRanges.HasValue)
            {
                if (!src.DocumentNumberRanges.Value.IsValue)
                    attributes.Add(new IppAttribute(Tag.NoValue, "document-numbers", NoValue.Instance));
                else if (src.DocumentNumberRanges.Value.Value != null)
                    attributes.AddRange(src.DocumentNumberRanges.Value.Value.Select(x => new IppAttribute(Tag.RangeOfInteger, "document-numbers", x)));
            }

            if (src.DocumentCopyRanges.HasValue)
            {
                if (!src.DocumentCopyRanges.Value.IsValue)
                    attributes.Add(new IppAttribute(Tag.NoValue, "document-copies", NoValue.Instance));
                else if (src.DocumentCopyRanges.Value.Value != null)
                    attributes.AddRange(src.DocumentCopyRanges.Value.Value.Select(x => new IppAttribute(Tag.RangeOfInteger, "document-copies", x)));
            }

            if (src.JobTemplateAttributes != null)
            {
                var templateRequest = map.Map<IppRequestMessage>(src.JobTemplateAttributes);
                var overrideAttrs = templateRequest.JobAttributes
                    .Where(x => x.Name != IppAttributeNames.Overrides && x.Name != IppAttributeNames.OverridesActual);
                attributes.AddRange(overrideAttrs);
            }

            return attributes;
        });


        mapper.CreateMap<OverrideInstruction, List<IppAttribute>>((src, map) => map.Map<IEnumerable<IppAttribute>>(src).ToList());
        mapper.CreateMap<OverrideInstruction, IDictionary<string, IppAttribute[]>>((src, map) => map.Map<List<IppAttribute>>(src).ToIppDictionary());
        mapper.CreateMap<OverrideInstruction, Dictionary<string, IppAttribute[]>>((src, map) => map.Map<List<IppAttribute>>(src).ToIppDictionary());
    }
}
