using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class JobErrorSheetProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobErrorSheet>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobErrorSheet>();

            var dst = new JobErrorSheet
            {
                JobErrorSheetType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobErrorSheet.JobErrorSheetType).ConvertCamelCaseToKebabCase()),
                JobErrorSheetWhen = map.MapFromDicNullable<JobErrorSheetWhen?>(src, nameof(JobErrorSheet.JobErrorSheetWhen).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword))
            };
            if (src.ContainsKey(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobErrorSheet, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobErrorSheet, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobErrorSheetType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.JobErrorSheetType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobErrorSheetType.Value)));
            if (src.JobErrorSheetWhen.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.JobErrorSheetWhen).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobErrorSheetWhen.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
