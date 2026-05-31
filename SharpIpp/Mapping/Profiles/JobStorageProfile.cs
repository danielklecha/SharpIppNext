using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobStorageProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobStorage>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobStorage>();

            return new JobStorage
            {
                JobStorageAccess = map.MapFromDicNullable<JobStorageAccess?>(src, nameof(JobStorage.JobStorageAccess).ConvertCamelCaseToKebabCase()),
                JobStorageDisposition = map.MapFromDicNullable<JobStorageDisposition?>(src, nameof(JobStorage.JobStorageDisposition).ConvertCamelCaseToKebabCase()),
                JobStorageGroup = map.MapFromDicNullable<string?>(src, nameof(JobStorage.JobStorageGroup).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<JobStorage, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.JobStorage, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobStorageAccess != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobStorage.JobStorageAccess).ConvertCamelCaseToKebabCase(), src.JobStorageAccess.Value.Value));
            if (src.JobStorageDisposition != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobStorage.JobStorageDisposition).ConvertCamelCaseToKebabCase(), src.JobStorageDisposition.Value.Value));
            if (src.JobStorageGroup != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(JobStorage.JobStorageGroup).ConvertCamelCaseToKebabCase(), src.JobStorageGroup));
            return attributes;
        });
    }
}
