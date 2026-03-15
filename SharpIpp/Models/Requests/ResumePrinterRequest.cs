using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Resume-Printer Operation
    /// This operation allows a client to resume the Printer object
    /// scheduling jobs on all its devices.  The Printer object MUST remove
    /// the 'paused' and 'moving-to-paused' values from the Printer object's
    /// "printer-state-reasons" attribute, if present.  If there are no other
    /// reasons to keep a device paused (such as media-jam), the IPP Printer
    /// is free to transition itself to the 'processing' or 'idle' states,
    /// depending on whether there are jobs to be processed or not,
    /// respectively, and the device(s) resume processing jobs.
    /// See: RFC 2911 Section 3.2.8
    /// </summary>
    public class ResumePrinterRequest : IppRequest<ResumePrinterOperationAttributes>, IIppPrinterRequest
    {
        
    }
}
