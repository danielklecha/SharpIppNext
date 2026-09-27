using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Deregister-Output-Device operation attributes.
/// See: PWG 5100.18-2025 Section 5.4.1
/// </summary>
[IppAttribute]
public class DeregisterOutputDeviceOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public IppValue<Uri>? OutputDeviceUuid { get; set; }
}
