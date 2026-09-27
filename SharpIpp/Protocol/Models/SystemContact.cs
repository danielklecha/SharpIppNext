using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

[IppAttribute(IppAttributeNames.SystemContactCol)]
public class SystemContact : IIppCollection
{

    /// <summary>
    /// contact-name (name(MAX))
    /// </summary>
    public IppValue<string>? ContactName { get; set; }

    /// <summary>
    /// contact-uri (uri)
    /// </summary>
    public IppValue<Uri>? ContactUri { get; set; }

    /// <summary>
    /// contact-vcard (1setOf text(MAX))
    /// </summary>
    [IppAttribute("contact-vcard", Tag = Tag.TextWithoutLanguage)]
    public IppValue<string[]>? ContactVcard { get; set; }
}
