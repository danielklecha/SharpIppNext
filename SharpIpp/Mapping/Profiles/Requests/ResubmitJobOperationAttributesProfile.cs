using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ResubmitJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ResubmitJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ResubmitJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            if (src.TryGetValue(IppAttributeNames.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobMandatoryAttributes);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, IppAttributeNames.IppAttributeFidelity);
            return dst;
        });

        mapper.CreateMap<ResubmitJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(IppAttributeNames.DocumentFormatDetails));
            if (src.IppAttributeFidelity.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, src.IppAttributeFidelity.Value));
            if (src.JobMandatoryAttributes != null)
                dst.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, x)));
            return dst;
        });
    }
}
