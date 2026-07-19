using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class SubscriptionDescriptionAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<List<List<IppAttribute>>, SubscriptionDescriptionAttributes[]>((src, map) =>
            src.Select(x => map.Map<SubscriptionDescriptionAttributes>(x.ToIppDictionary())).ToArray());

        mapper.CreateMap<SubscriptionDescriptionAttributes[], List<List<IppAttribute>>>((src, map) =>
            src.Select(x => map.Map<IDictionary<string, IppAttribute[]>>(x)
                .Values.SelectMany(v => v)
                .ToList())
                .ToList());

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SubscriptionDescriptionAttributes>((src, map) =>
        {
            var dst = new SubscriptionDescriptionAttributes
            {
                NotifySubscriptionId = map.MapFromDicNullable<int?>(src, IppAttributeNames.NotifySubscriptionId),
                NotifyPullMethod = map.MapFromDicNullable<NotifyPullMethod?>(src, IppAttributeNames.NotifyPullMethod),
                NotifyEvents = map.MapFromDicSetNullable<NotifyEvent[]?>(src, IppAttributeNames.NotifyEvents),
                NotifyLeaseDuration = map.MapFromDicNullable<int?>(src, IppAttributeNames.NotifyLeaseDuration),
                NotifyRecipientUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.NotifyRecipientUri),
                NotifyUserData = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.NotifyUserData),
                NotifySubscriberUserName = map.MapFromDicNullable<string?>(src, IppAttributeNames.NotifySubscriberUserName),
                NotifyCharset = map.MapFromDicNullable<string?>(src, IppAttributeNames.NotifyCharset),
                NotifyNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.NotifyNaturalLanguage)
            };
            return dst;
        });

        mapper.CreateMap<SubscriptionDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>();
            if (src.NotifySubscriptionId != null)
                dic.Add(IppAttributeNames.NotifySubscriptionId, [new IppAttribute(Tag.Integer, IppAttributeNames.NotifySubscriptionId, src.NotifySubscriptionId.Value)]);
            if (src.NotifyPullMethod != null)
                dic.Add(IppAttributeNames.NotifyPullMethod, [new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyPullMethod, src.NotifyPullMethod.Value.Value)]);
            if (src.NotifyEvents != null)
                dic.Add(IppAttributeNames.NotifyEvents, src.NotifyEvents.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyEvents, map.Map<string>(x))).ToArray());
            if (src.NotifyLeaseDuration != null)
                dic.Add(IppAttributeNames.NotifyLeaseDuration, [new IppAttribute(Tag.Integer, IppAttributeNames.NotifyLeaseDuration, src.NotifyLeaseDuration.Value)]);
            if (src.NotifyRecipientUri != null)
                dic.Add(IppAttributeNames.NotifyRecipientUri, [new IppAttribute(Tag.Uri, IppAttributeNames.NotifyRecipientUri, src.NotifyRecipientUri.ToString())]);
            if (src.NotifyUserData != null)
            {
                dic.Add(IppAttributeNames.NotifyUserData, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.NotifyUserData, src.NotifyUserData.Value)]);
            }
            if (src.NotifySubscriberUserName != null)
                dic.Add(IppAttributeNames.NotifySubscriberUserName, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.NotifySubscriberUserName, src.NotifySubscriberUserName)]);
            if (src.NotifyCharset != null)
                dic.Add(IppAttributeNames.NotifyCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.NotifyCharset, src.NotifyCharset)]);
            if (src.NotifyNaturalLanguage != null)
                dic.Add(IppAttributeNames.NotifyNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.NotifyNaturalLanguage, src.NotifyNaturalLanguage)]);
            return dic;
        });
    }
}
