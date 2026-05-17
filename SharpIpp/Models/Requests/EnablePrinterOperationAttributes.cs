namespace SharpIpp.Models.Requests;

/// <summary>
/// Enable-Printer Operation Attributes.
/// See: RFC 3998 Section 3.1.1.1
/// </summary>
public class EnablePrinterOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client MAY supply this attribute. It provides a message from the operator about the printer's state.
    /// See: RFC 3998 Section 3.1.1.1 and RFC 2911 Section 3.2.7.1
    /// </summary>
    /// <code>printer-message-from-operator</code>
    public string? PrinterMessageFromOperator { get; set; }
}
