using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Current-Job Operation Attributes.
/// See: RFC 3998 Section 3.2.2
/// </summary>
[IppAttribute]
public class CancelCurrentJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The client MAY supply this attribute. It provides a message from the operator about the job's state.
    /// See: RFC 3998 Section 6
    /// </summary>
    /// <code>job-message-from-operator</code>
    [IppAttribute(IppAttributeNames.JobMessageFromOperator, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? JobMessageFromOperator { get; set; }
}
