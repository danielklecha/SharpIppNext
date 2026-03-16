using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// See: RFC 8011 Section 5.1.7
    /// </summary>
    public readonly record struct UriScheme(string Value)
    {
        public static readonly UriScheme Ipp = new("ipp");
        public static readonly UriScheme Ipps = new("ipps");
        public static readonly UriScheme Http = new("http");
        public static readonly UriScheme Https = new("https");
        public static readonly UriScheme Ftp = new("ftp");
        public static readonly UriScheme Mailto = new("mailto");
        public static readonly UriScheme File = new("file");
        public static readonly UriScheme Urn = new("urn");

        public override string ToString() => Value;
        public static implicit operator string(UriScheme bin) => bin.Value;
        public static explicit operator UriScheme(string value) => new(value);
    }
}
