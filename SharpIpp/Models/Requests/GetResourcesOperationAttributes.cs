using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Resources operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.7
/// </summary>
public class GetResourcesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// Filter by a set of Resource IDs.
    /// See: PWG 5100.22-2025 Section 7.1.15
    /// </summary>
    [ItemRange(1, int.MaxValue)]
    public int[]? ResourceIds { get; set; }

    /// <summary>
    /// Requested resource attributes to return.
    /// </summary>
    public string[]? RequestedAttributes { get; set; }
}
