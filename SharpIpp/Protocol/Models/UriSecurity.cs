using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    public readonly record struct UriSecurity(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly UriSecurity None = new("none");
        public static readonly UriSecurity Ssl3 = new("ssl3");
        public static readonly UriSecurity Tls = new("tls");

        public override string ToString() => Value;
        public static implicit operator string(UriSecurity bin) => bin.Value;
        public static explicit operator UriSecurity(string value) => new(value);
    }
}
