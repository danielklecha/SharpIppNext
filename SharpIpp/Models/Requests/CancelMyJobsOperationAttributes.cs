using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-My-Jobs Operation Attributes.
/// See: PWG 5100.7-2023 Section 5.2.1
/// </summary>
[IppAttribute]
public class CancelMyJobsOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The Client MAY supply this operation attribute which specifies the target Job(s) for the operation.
    /// See: PWG 5100.7-2023 Section 5.2.1
    /// </summary>
    /// <code>job-ids</code>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.JobIds, Tag = Tag.Integer)]
    public IppValue<int[]>? JobIds { get; set; }

    /// <summary>
    /// The Client MAY supply this attribute, which provides a message to the Operator.
    /// See: PWG 5100.7-2023 Section 5.2.1
    /// </summary>
    /// <code>message</code>
    [IppAttribute(IppAttributeNames.Message, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? Message { get; set; }
}
