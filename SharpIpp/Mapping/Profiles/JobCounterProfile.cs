using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class JobCounterProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobCounter>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobCounter>();

            return new JobCounter
            {
                Blank = map.MapFromDicNullable<int?>(src, nameof(JobCounter.Blank).ConvertCamelCaseToKebabCase()),
                BlankTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.BlankTwoSided).ConvertCamelCaseToKebabCase()),
                FullColor = map.MapFromDicNullable<int?>(src, nameof(JobCounter.FullColor).ConvertCamelCaseToKebabCase()),
                FullColorTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.FullColorTwoSided).ConvertCamelCaseToKebabCase()),
                HighlightColor = map.MapFromDicNullable<int?>(src, nameof(JobCounter.HighlightColor).ConvertCamelCaseToKebabCase()),
                HighlightColorTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.HighlightColorTwoSided).ConvertCamelCaseToKebabCase()),
                Monochrome = map.MapFromDicNullable<int?>(src, nameof(JobCounter.Monochrome).ConvertCamelCaseToKebabCase()),
                MonochromeTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.MonochromeTwoSided).ConvertCamelCaseToKebabCase()),
            };
        });
        mapper.CreateMap<JobCounter, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "job-counter", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.Blank.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.Blank).ConvertCamelCaseToKebabCase(), src.Blank.Value));
            if (src.BlankTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.BlankTwoSided).ConvertCamelCaseToKebabCase(), src.BlankTwoSided.Value));
            if (src.FullColor.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.FullColor).ConvertCamelCaseToKebabCase(), src.FullColor.Value));
            if (src.FullColorTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.FullColorTwoSided).ConvertCamelCaseToKebabCase(), src.FullColorTwoSided.Value));
            if (src.HighlightColor.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.HighlightColor).ConvertCamelCaseToKebabCase(), src.HighlightColor.Value));
            if (src.HighlightColorTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.HighlightColorTwoSided).ConvertCamelCaseToKebabCase(), src.HighlightColorTwoSided.Value));
            if (src.Monochrome.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.Monochrome).ConvertCamelCaseToKebabCase(), src.Monochrome.Value));
            if (src.MonochromeTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.MonochromeTwoSided).ConvertCamelCaseToKebabCase(), src.MonochromeTwoSided.Value));
            return attributes;
        });
    }
}
