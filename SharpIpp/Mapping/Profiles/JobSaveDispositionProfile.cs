using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobSaveDispositionProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobSaveDisposition>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobSaveDisposition>();

            var dst = new JobSaveDisposition
            {
                SaveDisposition = map.MapFromDicNullable<SaveDisposition?>(src, IppAttributeNames.SaveDisposition),
                SaveLocation = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SaveLocation)
            };

            if (src.TryGetValue(IppAttributeNames.SaveInfo, out var saveInfo) && saveInfo.GroupBegCollection().Any())
            {
                dst.SaveInfo = saveInfo
                    .GroupBegCollection()
                    .Select(x => map.Map<SaveInfo>(x.FromBegCollection().ToIppDictionary()))
                    .ToArray();
            }

            return dst;
        });

        mapper.CreateMap<JobSaveDisposition, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.JobSaveDisposition, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.SaveDisposition != null)
                attributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.SaveDisposition, src.SaveDisposition.Value.Value));
            if (src.SaveLocation != null)
                attributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.SaveLocation, src.SaveLocation.ToString()));
            if (src.SaveInfo != null)
            {
                attributes.AddRange(src.SaveInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SaveInfo)));
            }
            return attributes;
        });
    }
}
