using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Allocate-Printer-Resources operation attributes.
/// See: PWG 5100.22-2025 Section 6.1.1
/// </summary>
[IppAttribute]
public class AllocatePrinterResourcesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-ids</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.15
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.ResourceIds, Tag = Tag.Integer)]
    public IppValue<int[]>? ResourceIds { get; set; }
}
