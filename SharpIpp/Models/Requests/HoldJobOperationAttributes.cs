using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class HoldJobOperationAttributes : CancelJobOperationAttributes
{
    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer object
    ///     MUST support this attribute.  It indicates the time period during
    ///     which the job shall be held.
    /// </summary>
    public JobHoldUntil? JobHoldUntil { get; set; }
}
