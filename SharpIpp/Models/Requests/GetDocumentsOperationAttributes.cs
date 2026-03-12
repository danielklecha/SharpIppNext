using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
///     PWG 5100.5-2024 Section 5.2.1.1
/// </summary>
public class GetDocumentsOperationAttributes : JobOperationAttributes
{
    public int? Limit { get; set; }
    public string[]? RequestedAttributes { get; set; }
}
