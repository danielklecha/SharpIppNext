namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-User-Printer-Attributes operation attributes.
/// See: PWG 5100.11-2024 Section 5.1.1
/// </summary>
public class GetUserPrinterAttributesOperationAttributes : GetPrinterAttributesOperationAttributes
{
    /// <summary>
    /// The <c>requesting-user-vcard</c> operation attribute.
    /// Type: 1setOf text(MAX)
    /// See: PWG 5100.11-2024 Section 5.1.1
    /// See: PWG 5100.22-2025 Section 7.1.6
    /// </summary>
    /// <code>requesting-user-vcard</code>
    public string[]? RequestingUserVcard { get; set; }
}
