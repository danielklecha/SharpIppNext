namespace SharpIpp.Models.Requests;

/// <summary>
/// Promote-Job Operation Attributes.
/// See: RFC 3998 Section 3.2.3.1
/// </summary>
public class PromoteJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The client MAY supply this attribute. It provides a message from the operator about the job's state.
    /// See: RFC 3998 Section 6
    /// </summary>
    /// <code>job-message-from-operator</code>
    public string? JobMessageFromOperator { get; set; }
}
