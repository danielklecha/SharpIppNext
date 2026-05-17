using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class OperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OperationAttributes>((src, dst, map) =>
        {
            dst ??= new OperationAttributes();
            dst.AttributesCharset = map.MapFromDicNullable<string?>(src, JobAttribute.AttributesCharset) ?? "utf-8";
            dst.AttributesNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.AttributesNaturalLanguage) ?? "en";
            dst.RequestingUserName = map.MapFromDicNullable<string?>(src, JobAttribute.RequestingUserName);
            dst.RequestingUserUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.RequestingUserUri);
            dst.PrinterUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.PrinterUri);
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.JobHoldUntilTime);
            return dst;
        });

        mapper.CreateMap<OperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            dst.Add(new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, src.AttributesCharset ?? "utf-8"));
            dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, src.AttributesNaturalLanguage ?? "en"));

            if (src.PrinterUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, src.PrinterUri.ToString()));

            if (src.RequestingUserName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, src.RequestingUserName));

            if (src.RequestingUserUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.RequestingUserUri, src.RequestingUserUri.ToString()));

            if (src.ClientInfo != null)
                dst.AddRange(src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.ClientInfo)));
            if (src.JobHoldUntilTime != null)
                dst.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobHoldUntilTime, src.JobHoldUntilTime.Value));

            return dst;
        });
    }
}
