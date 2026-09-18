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
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// The <c>destination-attributes</c> member attribute (1setOf collection).
    /// Stored as raw IPP dictionaries to preserve destination-specific attributes.
    /// </summary>
    [IppAttribute("destination-attributes")]
    public IDictionary<string, IppAttribute[]>[]? DestinationAttributes { get; set; }

    [IppAttribute("destination-attributes-supported", Tag = Tag.Keyword)]
    public string[]? DestinationAttributesSupported { get; set; }

    [IppAttribute("destination-info", Tag = Tag.TextWithoutLanguage)]
    public string? DestinationInfo { get; set; }

    [IppAttribute("destination-is-directory")]
    public bool? DestinationIsDirectory { get; set; }

    [IppAttribute("destination-mandatory-access-attributes", Tag = Tag.Keyword)]
    public string[]? DestinationMandatoryAccessAttributes { get; set; }

    [IppAttribute("destination-name", Tag = Tag.NameWithoutLanguage)]
    public string? DestinationName { get; set; }

    [IppAttribute("destination-oauth-scope")]
    public OctetString[]? DestinationOAuthScope { get; set; }

    [IppAttribute("destination-oauth-token")]
    public OctetString[]? DestinationOAuthToken { get; set; }

    [IppAttribute("destination-oauth-uri")]
    public Uri? DestinationOAuthUri { get; set; }

    [IppAttribute("destination-uri")]
    public Uri? DestinationUri { get; set; }
}
