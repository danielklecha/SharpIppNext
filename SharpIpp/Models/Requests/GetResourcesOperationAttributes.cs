using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Resources operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.7
/// </summary>
[IppAttribute]
public class GetResourcesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// Filter by a set of Resource IDs.
    /// See: PWG 5100.22-2025 Section 7.1.15
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.ResourceIds, Tag = Tag.Integer)]
    public IppValue<int[]>? ResourceIds { get; set; }

    /// <summary>
    /// Requested resource attributes to return.
    /// </summary>
    [IppAttribute(IppAttributeNames.RequestedAttributes, Tag = Tag.Keyword)]
    public IppValue<string[]>? RequestedAttributes { get; set; }
}
