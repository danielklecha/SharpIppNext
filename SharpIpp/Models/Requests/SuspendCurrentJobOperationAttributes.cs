namespace SharpIpp.Models.Requests;

/// <summary>
/// Suspend-Current-Job Operation Attributes.
/// See: RFC 3998 Section 3.2.1.1
/// </summary>
public class SuspendCurrentJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The client MAY supply this attribute. It provides a message from the operator about the job's state.
    /// See: RFC 3998 Section 6
    /// </summary>
    /// <code>job-message-from-operator</code>
    public string? JobMessageFromOperator { get; set; }
}
