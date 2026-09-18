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
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    [IppAttribute("destination-uri")]
    public Uri? DestinationUriValue { get; set; }

    [IppAttribute("post-dial-string", Tag = Tag.TextWithoutLanguage)]
    public string? PostDialString { get; set; }

    [IppAttribute("pre-dial-string", Tag = Tag.TextWithoutLanguage)]
    public string? PreDialString { get; set; }

    public int? T33Subaddress { get; set; }
}
