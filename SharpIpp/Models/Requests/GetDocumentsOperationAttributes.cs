using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.2.1.1
/// </summary>
public class GetDocumentsOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The limit IPP attribute.
    /// See: pwg5100.1-2022 Section 2.2
    /// </summary>
    /// <code>limit</code>
    public int? Limit { get; set; }
    /// <summary>
    /// The requested-attributes IPP attribute.
    /// See: RFC 8011 Section 5.4.1
    /// </summary>
    /// <code>requested-attributes</code>
    public string[]? RequestedAttributes { get; set; }
}
