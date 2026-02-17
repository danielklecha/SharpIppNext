using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     Request to get a list of printers from a CUPS IPP server
    ///     <seealso href="http://www.cups.org/doc/spec-ipp.html#CUPS_GET_PRINTERS" />
    /// </summary>
    public class CUPSGetPrintersRequest : IppRequest<CUPSGetPrintersOperationAttributes>, IIppPrinterRequest
    {

    }
}
