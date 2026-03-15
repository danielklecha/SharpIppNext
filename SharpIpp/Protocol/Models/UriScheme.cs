using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// See: RFC 8011 Section 5.1.7
    /// </summary>
    public enum UriScheme
    {
        Ipp,
        Ipps,
        Http,
        Https,
        Ftp,
        Mailto,
        File,
        Urn
    }
}
