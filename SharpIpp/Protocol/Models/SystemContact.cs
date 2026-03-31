using System;

namespace SharpIpp.Protocol.Models;

public class SystemContact : IIppCollection
{
    public bool IsValue { get; set; } = true;

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
    public string[]? ContactVcard { get; set; }
}
