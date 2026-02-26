using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class GetJobAttributesOperationAttributes : JobOperationAttributes
{
    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  It is a set of Job
    ///     attribute names and/or attribute groups names in whose values
    ///     the requester is interested.  This set of attributes is
    ///     returned for each Job object that is returned.  The allowed
    ///     attribute group names are the same as those defined in the
    ///     Get-Job-Attributes operation in section 3.3.4.  If the client
    ///     does not supply this attribute, the Printer MUST respond as if
    ///     the client had supplied this attribute with two values: 'job-
    ///     uri' and 'job-id'.
    /// </summary>
    public string[]? RequestedAttributes { get; set; }
}
