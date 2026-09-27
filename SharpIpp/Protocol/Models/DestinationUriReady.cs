using System;
using System.Collections.Generic;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-uri-ready</c> member collection.
/// See: PWG 5100.17-2014 Section 8.3.3
/// </summary>
[IppAttribute(IppAttributeNames.DestinationUriReady)]
public class DestinationUriReady : IIppCollection
{

    /// <summary>
    /// The <c>destination-attributes</c> member attribute (1setOf collection).
    /// Stored as raw IPP dictionaries to preserve destination-specific attributes.
    /// </summary>
    [IppAttribute("destination-attributes")]
    public IDictionary<string, IppAttribute[]>[]? DestinationAttributes { get; set; }

    [IppAttribute("destination-attributes-supported", Tag = Tag.Keyword)]
    public IppValue<string[]>? DestinationAttributesSupported { get; set; }

    [IppAttribute("destination-info", Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? DestinationInfo { get; set; }

    [IppAttribute("destination-is-directory")]
    public IppValue<bool>? DestinationIsDirectory { get; set; }

    [IppAttribute("destination-mandatory-access-attributes", Tag = Tag.Keyword)]
    public IppValue<string[]>? DestinationMandatoryAccessAttributes { get; set; }

    [IppAttribute("destination-name", Tag = Tag.NameWithoutLanguage)]
    public IppValue<string>? DestinationName { get; set; }

    [IppAttribute("destination-oauth-scope")]
    public IppValue<OctetString[]>? DestinationOAuthScope { get; set; }

    [IppAttribute("destination-oauth-token")]
    public IppValue<OctetString[]>? DestinationOAuthToken { get; set; }

    [IppAttribute("destination-oauth-uri")]
    public IppValue<Uri>? DestinationOAuthUri { get; set; }

    [IppAttribute("destination-uri")]
    public IppValue<Uri>? DestinationUri { get; set; }
}
