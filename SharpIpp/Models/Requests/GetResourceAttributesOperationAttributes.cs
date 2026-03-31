namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Resource-Attributes operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.3 / 6.3.7 (Resource range in system service)
/// </summary>
public class GetResourceAttributesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The resource-id for the requested resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    public int? ResourceId { get; set; }

    /// <summary>
    /// Requested resource attributes to return.
    /// </summary>
    public string[]? RequestedAttributes { get; set; }
}
