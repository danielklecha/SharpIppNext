using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    public enum UriAuthentication
    {
        Unsupported,
        None,
        RequestingUserName,
        Basic,
        Digest,
        Certificate
    }
}
