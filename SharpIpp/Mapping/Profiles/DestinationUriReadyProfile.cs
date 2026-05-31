using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class DestinationUriReadyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationUriReady>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DestinationUriReady>();

            IDictionary<string, IppAttribute[]>[]? destinationAttributes = null;
            if (src.TryGetValue("destination-attributes", out var nestedDestinationAttributes) && nestedDestinationAttributes.GroupBegCollection().Any())
            {
                destinationAttributes = nestedDestinationAttributes
                    .GroupBegCollection()
                    .Select(x => x.FromBegCollection().ToIppDictionary())
                    .ToArray();
            }

            return new DestinationUriReady
            {
                DestinationAttributes = destinationAttributes,
                DestinationAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, "destination-attributes-supported"),
                DestinationInfo = map.MapFromDicNullable<string?>(src, "destination-info"),
                DestinationIsDirectory = map.MapFromDicNullable<bool?>(src, "destination-is-directory"),
                DestinationMandatoryAccessAttributes = map.MapFromDicSetNullable<string[]?>(src, "destination-mandatory-access-attributes"),
                DestinationName = map.MapFromDicNullable<string?>(src, "destination-name"),
                DestinationOAuthScope = map.MapFromDicSetNullable<OctetString[]?>(src, "destination-oauth-scope"),
                DestinationOAuthToken = map.MapFromDicSetNullable<OctetString[]?>(src, "destination-oauth-token"),
                DestinationOAuthUri = map.MapFromDicNullable<Uri?>(src, "destination-oauth-uri"),
                DestinationUri = map.MapFromDicNullable<Uri?>(src, "destination-uri")
            };
        });

        mapper.CreateMap<DestinationUriReady, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.DestinationUriReady, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationAttributes != null)
            {
                attributes.AddRange(src.DestinationAttributes.SelectMany(x => x.Values.SelectMany(y => y).ToBegCollection("destination-attributes")));
            }

            if (src.DestinationAttributesSupported != null)
                attributes.AddRange(src.DestinationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, "destination-attributes-supported", x)));
            if (src.DestinationInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "destination-info", src.DestinationInfo));
            if (src.DestinationIsDirectory.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "destination-is-directory", src.DestinationIsDirectory.Value));
            if (src.DestinationMandatoryAccessAttributes != null)
                attributes.AddRange(src.DestinationMandatoryAccessAttributes.Select(x => new IppAttribute(Tag.Keyword, "destination-mandatory-access-attributes", x)));
            if (src.DestinationName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "destination-name", src.DestinationName));
            if (src.DestinationOAuthScope != null)
                attributes.AddRange(src.DestinationOAuthScope.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "destination-oauth-scope", x)));
            if (src.DestinationOAuthToken != null)
                attributes.AddRange(src.DestinationOAuthToken.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "destination-oauth-token", x)));
            if (src.DestinationOAuthUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "destination-oauth-uri", src.DestinationOAuthUri.ToString()));
            if (src.DestinationUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUri.ToString()));

            return attributes;
        });
    }
}
