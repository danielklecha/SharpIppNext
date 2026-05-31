using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobSheetsColProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobSheetsCol>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobSheetsCol>();

            var dst = new JobSheetsCol
            {
                JobSheets = map.MapFromDicNullable<JobSheets?>(src, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
            };

            if (src.ContainsKey(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());

            return dst;
        });

        mapper.CreateMap<JobSheetsCol, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.JobSheetsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobSheets.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobSheets.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
