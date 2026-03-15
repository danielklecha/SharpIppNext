using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class JobOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client MUST supply either (1) the "job-uri" attribute or (2) the "printer-uri" and "job-id" attributes. The Printer object MUST support both of these forms of target identification. It contains the URI of the Job object which is the target of this operation
    /// See: RFC 8011 Section 5.3.2
    /// </summary>
    /// <code>job-uri</code>
    public Uri? JobUri { get; set; }
    /// <summary>
    /// The client MUST supply either (1) the "job-uri" attribute or (2) the "printer-uri" and "job-id" attributes. It contains the ID of the Job object which is the target of this operation
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    /// <code>job-id</code>
    public int? JobId { get; set; }

}
