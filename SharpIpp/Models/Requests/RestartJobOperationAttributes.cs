using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
/// <summary>
/// Restart-Job operation attributes.
/// <br/>
/// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as the Restart-Job operation (Deprecated in RFC 8011).
/// See: RFC 2911 Section 3.3.7.1
/// </summary>
public class RestartJobOperationAttributes :  CancelJobOperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It indicates the time period during which the job shall be held
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0 Section 6.1.5
    /// </summary>
    /// <code>job-hold-until</code>
    public JobHoldUntil? JobHoldUntil { get; set; }

}
