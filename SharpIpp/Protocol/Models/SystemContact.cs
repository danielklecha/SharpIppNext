using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

[IppAttribute(IppAttributeNames.SystemContactCol)]
public class SystemContact : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// contact-name (name(MAX))
    /// </summary>
    public string? ContactName { get; set; }

    /// <summary>
    /// contact-uri (uri)
    /// </summary>
    public Uri? ContactUri { get; set; }

    /// <summary>
    /// contact-vcard (1setOf text(MAX))
    /// </summary>
    [IppAttribute("contact-vcard", Tag = Tag.TextWithoutLanguage)]
    public string[]? ContactVcard { get; set; }
}
