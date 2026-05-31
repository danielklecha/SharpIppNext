using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class OverrideInstructionProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
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
                var templateRequest = new IppRequestMessage();
                templateRequest.JobAttributes.AddRange(overrideTemplateMembers.Values.SelectMany(x => x));
                overrideTemplateAttributes = map.Map<IIppRequestMessage, JobTemplateAttributes>(templateRequest);
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
                var overrideTemplateMembers = templateRequest.JobAttributes
                    .ToIppDictionary()
                    .Where(x => x.Key != IppAttributeNames.Overrides && x.Key != IppAttributeNames.OverridesActual)
                    .SelectMany(x => x.Value);
                attributes.AddRange(overrideTemplateMembers);
            }

            return attributes;
        });
    }
}
