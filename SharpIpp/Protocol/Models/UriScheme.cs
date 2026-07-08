using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the URI scheme used for printer URIs.
    /// See: RFC 8011 Section 5.1.7
    /// </summary>
    public readonly record struct UriScheme(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// The IPP URI scheme (ipp://).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Ipp = new("ipp");

        /// <summary>
        /// The IPPS (IPP over TLS) URI scheme (ipps://).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Ipps = new("ipps");

        /// <summary>
        /// The HTTP URI scheme (http://).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Http = new("http");

        /// <summary>
        /// The HTTPS URI scheme (https://).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Https = new("https");

        /// <summary>
        /// The FTP URI scheme (ftp://).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Ftp = new("ftp");

        /// <summary>
        /// The mailto URI scheme (mailto:).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Mailto = new("mailto");

        /// <summary>
        /// The file URI scheme (file://).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme File = new("file");

        /// <summary>
        /// The URN URI scheme (urn:).
        /// See: RFC 8011 Section 5.1.7
        /// </summary>
        public static readonly UriScheme Urn = new("urn");

        public override string ToString() => Value;
        public static implicit operator string(UriScheme bin) => bin.Value;
        public static implicit operator UriScheme(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
    }
}
