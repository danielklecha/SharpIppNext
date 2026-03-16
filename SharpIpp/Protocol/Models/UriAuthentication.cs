using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    public readonly record struct UriAuthentication(string Value)
    {
        public static readonly UriAuthentication None = new("none");
        public static readonly UriAuthentication RequestingUserName = new("requesting-user-name");
        public static readonly UriAuthentication Basic = new("basic");
        public static readonly UriAuthentication Digest = new("digest");
        public static readonly UriAuthentication Certificate = new("certificate");

        public override string ToString() => Value;
        public static implicit operator string(UriAuthentication bin) => bin.Value;
        public static explicit operator UriAuthentication(string value) => new(value);
    }
}
