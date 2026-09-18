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
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<OverrideInstruction>();

            var pageRanges = map.MapFromDicSetNullable<SharpIpp.Protocol.Models.Range[]?>(src, "pages");
            var documentNumberRanges = map.MapFromDicSetNullable<SharpIpp.Protocol.Models.Range[]?>(src, "document-numbers");
            var documentCopyRanges = map.MapFromDicSetNullable<SharpIpp.Protocol.Models.Range[]?>(src, "document-copies");

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
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.Overrides, NoValue.Instance) };

            var pageRanges = src.PageRanges;
            var documentNumberRanges = src.DocumentNumberRanges;
            var documentCopyRanges = src.DocumentCopyRanges;

            var attributes = new List<IppAttribute>();
            if (pageRanges != null)
                attributes.AddRange(pageRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, "pages", x)));
            if (documentNumberRanges != null)
                attributes.AddRange(documentNumberRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, "document-numbers", x)));
            if (documentCopyRanges != null)
                attributes.AddRange(documentCopyRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, "document-copies", x)));

            if (src.JobTemplateAttributes != null)
            {
                var templateRequest = map.Map<IppRequestMessage>(src.JobTemplateAttributes);
                var overrideAttrs = templateRequest.JobAttributes
                    .Where(x => x.Name != IppAttributeNames.Overrides && x.Name != IppAttributeNames.OverridesActual);
                attributes.AddRange(overrideAttrs);
            }

            return attributes;
        });

        mapper.CreateMap<NoValue, OverrideInstruction>((_, _) => NoValue.GetNoValue<OverrideInstruction>());
        mapper.CreateMap<OverrideInstruction, List<IppAttribute>>((src, map) => map.Map<IEnumerable<IppAttribute>>(src).ToList());
    }
}
