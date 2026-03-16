using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-My-Jobs Operation Attributes.
/// See: PWG 5100.7-2023 Section 5.2.1
/// </summary>
public class CancelMyJobsOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The Client MAY supply this operation attribute which specifies the target Job(s) for the operation.
    /// See: PWG 5100.7-2023 Section 5.2.1
    /// </summary>
    /// <code>job-ids</code>
    public int[]? JobIds { get; set; }

    /// <summary>
    /// The Client MAY supply this attribute, which provides a message to the Operator.
    /// See: PWG 5100.7-2023 Section 5.2.1
    /// </summary>
    /// <code>message</code>
    public string? Message { get; set; }
}
