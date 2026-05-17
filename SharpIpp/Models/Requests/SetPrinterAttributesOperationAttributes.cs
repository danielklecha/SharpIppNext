namespace SharpIpp.Models.Requests;

/// <summary>
/// Set-Printer-Attributes Operation Attributes.
/// See: RFC 3380 Section 4.1
/// </summary>
public class SetPrinterAttributesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute.
    /// It identifies the format of the printer-x-xxx attributes being set.
    /// See: RFC 3380 Section 4.1.2
    /// </summary>
    /// <example>application/octet-stream</example>
    /// <code>document-format</code>
    public string? DocumentFormat { get; set; }
}
