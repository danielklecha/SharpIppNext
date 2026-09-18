using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Output-Device-Attributes operation attributes.
/// See: PWG 5100.18-2025 Section 5.10.1
/// </summary>
[IppAttribute]
public class UpdateOutputDeviceAttributesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public Uri? OutputDeviceUuid { get; set; }
}
