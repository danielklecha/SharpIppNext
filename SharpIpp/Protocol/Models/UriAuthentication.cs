using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the authentication mechanism used for the printer URI.
    /// See: RFC 8011 Section 5.4.2
    /// </summary>
    public readonly record struct UriAuthentication(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// No authentication is used.
        /// See: RFC 8011 Section 5.4.2
        /// </summary>
        public static readonly UriAuthentication None = new("none");

        /// <summary>
        /// The requesting user name is used for authentication.
        /// See: RFC 8011 Section 5.4.2
        /// </summary>
        public static readonly UriAuthentication RequestingUserName = new("requesting-user-name");

        /// <summary>
        /// HTTP Basic authentication is used.
        /// See: RFC 8011 Section 5.4.2
        /// </summary>
        public static readonly UriAuthentication Basic = new("basic");

        /// <summary>
        /// HTTP Digest authentication is used.
        /// See: RFC 8011 Section 5.4.2
        /// </summary>
        public static readonly UriAuthentication Digest = new("digest");

        /// <summary>
        /// Certificate-based authentication is used.
        /// See: RFC 8011 Section 5.4.2
        /// </summary>
        public static readonly UriAuthentication Certificate = new("certificate");

        public override string ToString() => Value;
        public static implicit operator string(UriAuthentication bin) => bin.Value;
        public static explicit operator UriAuthentication(string value) => new(value);
    }
}
