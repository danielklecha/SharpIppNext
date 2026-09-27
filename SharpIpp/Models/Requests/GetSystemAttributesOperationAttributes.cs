using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-System-Attributes operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.8
/// </summary>
[IppAttribute]
public class GetSystemAttributesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>requested-attributes</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 6.3.8.1
    /// </summary>
    [IppAttribute(IppAttributeNames.RequestedAttributes, Tag = Tag.Keyword)]
    public IppValue<string[]>? RequestedAttributes { get; set; }
}
