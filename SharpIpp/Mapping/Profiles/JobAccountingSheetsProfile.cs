using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class JobAccountingSheetsProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobAccountingSheets>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobAccountingSheets>();

            var dst = new JobAccountingSheets
            {
                JobAccountingOutputBin = map.MapFromDicNullable<string, OutputBin?>(src, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase(), (attribute, value) =>
                    new OutputBin(value, attribute.Tag == Tag.Keyword)),
                JobAccountingSheetsType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword))
            };
            if (src.ContainsKey(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobAccountingSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobAccountingSheets, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobAccountingOutputBin != null)
                attributes.Add(new IppAttribute(src.JobAccountingOutputBin.Value.ToIppTag(), nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingOutputBin.Value)));
            if (src.JobAccountingSheetsType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingSheetsType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
