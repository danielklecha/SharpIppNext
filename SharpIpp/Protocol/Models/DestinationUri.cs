using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-uris</c> member collection.
/// See: PWG 5100.15-2013 Section 7.4.10
/// </summary>
[IppAttribute(IppAttributeNames.DestinationUris)]
public class DestinationUri : IIppCollection
{

    [IppAttribute("destination-uri")]
    public IppValue<Uri>? DestinationUriValue { get; set; }

    [IppAttribute("post-dial-string", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? PostDialString { get; set; }

    [IppAttribute("pre-dial-string", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? PreDialString { get; set; }

    public IppValue<int>? T33Subaddress { get; set; }
}
