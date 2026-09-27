using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

[IppAttribute(IppAttributeNames.SystemXriSupported)]
public class SystemXri : IIppCollection
{

    /// <summary>
    /// xri-uri (uri)
    /// </summary>
    public IppValue<Uri>? XriUri { get; set; }

    /// <summary>
    /// xri-authentication (type2 keyword)
    /// </summary>
    public IppValue<UriAuthentication>? XriAuthentication { get; set; }

    /// <summary>
    /// xri-security (type2 keyword)
    /// </summary>
    public IppValue<UriSecurity>? XriSecurity { get; set; }
}
