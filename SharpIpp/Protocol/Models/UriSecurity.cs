using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the security mechanism used for the printer URI.
    /// See: RFC 8011 Section 5.4.3
    /// </summary>
    public readonly record struct UriSecurity(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// No security mechanism is used.
        /// See: RFC 8011 Section 5.4.3
        /// </summary>
        public static readonly UriSecurity None = new("none");

        /// <summary>
        /// SSL3 security is used.
        /// See: RFC 8011 Section 5.4.3
        /// </summary>
        public static readonly UriSecurity Ssl3 = new("ssl3");

        /// <summary>
        /// TLS security is used.
        /// See: RFC 8011 Section 5.4.3
        /// </summary>
        public static readonly UriSecurity Tls = new("tls");

        public override string ToString() => Value;
        public static implicit operator string(UriSecurity bin) => bin.Value;
        public static implicit operator UriSecurity(string value) => new(value);
    }
}
