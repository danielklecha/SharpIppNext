using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc8011#section-5.1.7
    /// </summary>
    public enum UriScheme
    {
        Unsupported,
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
