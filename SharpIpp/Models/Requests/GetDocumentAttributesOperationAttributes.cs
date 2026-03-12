using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
///     PWG 5100.5-2024 Section 5.1.2.1
/// </summary>
public class GetDocumentAttributesOperationAttributes : JobOperationAttributes
{
    public int DocumentNumber { get; set; }
    public string[]? RequestedAttributes { get; set; }
}
