namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-System-Attributes operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.8
/// </summary>
public class GetSystemAttributesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>requested-attributes</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 6.3.8.1
    /// </summary>
    public string[]? RequestedAttributes { get; set; }
}
