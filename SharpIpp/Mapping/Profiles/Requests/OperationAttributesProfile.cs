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
            dst.AttributesCharset = map.MapFromDicNullable<string?>(src, IppAttributeNames.AttributesCharset) ?? "utf-8";
            dst.AttributesNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.AttributesNaturalLanguage) ?? "en";
            dst.RequestingUserName = map.MapFromDicNullable<string?>(src, IppAttributeNames.RequestingUserName);
            dst.RequestingUserUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.RequestingUserUri);
            dst.PrinterUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PrinterUri);
            if (src.TryGetValue(IppAttributeNames.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.JobHoldUntilTime);
            return dst;
        });

        mapper.CreateMap<OperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();

            // RFC 8011 Section 4.1.4: attributes-charset and attributes-natural-language
            // MUST be the first and second attributes respectively in the operation attributes group.
            dst.Add(new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, src.AttributesCharset ?? "utf-8"));
            dst.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, src.AttributesNaturalLanguage ?? "en"));

            if (src.PrinterUri != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, src.PrinterUri.ToString()));

            if (src.RequestingUserName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, src.RequestingUserName));

            if (src.RequestingUserUri != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.RequestingUserUri, src.RequestingUserUri.ToString()));

            if (src.ClientInfo != null)
                dst.AddRange(src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.ClientInfo)));
            if (src.JobHoldUntilTime != null)
                dst.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.JobHoldUntilTime, src.JobHoldUntilTime.Value));

            return dst;
        });
    }
}
