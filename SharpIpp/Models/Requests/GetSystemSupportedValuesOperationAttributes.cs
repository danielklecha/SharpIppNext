namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-System-Supported-Values operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.9
/// </summary>
public class GetSystemSupportedValuesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>requested-attributes</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 6.3.9.1
    /// </summary>
    public string[]? RequestedAttributes { get; set; }
}
