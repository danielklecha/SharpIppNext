using System;

namespace SharpIpp.Protocol.Models;

public class SystemXri : IIppCollection
{
    public bool IsValue { get; set; } = true;

    /// <summary>
    /// xri-uri (uri)
    /// </summary>
    public Uri? XriUri { get; set; }

    /// <summary>
    /// xri-authentication (type2 keyword)
    /// </summary>
    public string? XriAuthentication { get; set; }

    /// <summary>
    /// xri-security (type2 keyword)
    /// </summary>
    public string? XriSecurity { get; set; }
}
