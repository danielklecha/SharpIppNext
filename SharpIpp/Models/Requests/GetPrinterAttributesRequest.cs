using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Get-Printer-Attributes Operation
    /// This REQUIRED operation allows a client to request the values of
    /// attributes of a Printer object.
    /// See: RFC 2911 Section 3.2.5
    /// </summary>
    public class GetPrinterAttributesRequest : IppRequest<GetPrinterAttributesOperationAttributes>, IIppPrinterRequest
    {
        
    }
}
