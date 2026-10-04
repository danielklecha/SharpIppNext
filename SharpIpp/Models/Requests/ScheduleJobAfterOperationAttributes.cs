using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Schedule-Job-After Operation Attributes.
/// See: RFC 3998 Section 3.2.6
/// </summary>
[IppAttribute]
public class ScheduleJobAfterOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The client MAY supply this attribute. It provides a message from the operator about the job's state.
    /// See: RFC 3998 Section 6
    /// </summary>
    /// <code>job-message-from-operator</code>
    [IppAttribute(IppAttributeNames.JobMessageFromOperator, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? JobMessageFromOperator { get; set; }

    /// <summary>
    /// The client MUST supply this attribute. It specifies the job-id of the predecessor job.
    /// See: RFC 3998 Section 3.2.6.1
    /// </summary>
    /// <code>predecessor-job-id</code>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.PredecessorJobId, Tag = Tag.Integer)]
    public IppValue<int>? PredecessorJobId { get; set; }
}
