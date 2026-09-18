using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Resource-Attributes operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.3 / 6.3.7 (Resource range in system service)
/// </summary>
[IppAttribute]
public class GetResourceAttributesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The resource-id for the requested resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    [Range(1, 2147483647)]
    [IppAttribute(IppAttributeNames.ResourceId, Tag = Tag.Integer)]
    public int? ResourceId { get; set; }

    /// <summary>
    /// Requested resource attributes to return.
    /// </summary>
    [IppAttribute(IppAttributeNames.RequestedAttributes, Tag = Tag.Keyword)]
    public string[]? RequestedAttributes { get; set; }
}
