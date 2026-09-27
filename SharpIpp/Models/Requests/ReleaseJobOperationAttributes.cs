using System;
using System.Collections.Generic;
using System.Text;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
[IppAttribute]
public class ReleaseJobOperationAttributes : CancelJobOperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// See: PWG 5100.18-2025 Section 8.6
    /// </summary>
    /// <code>output-device-uuid</code>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public IppValue<Uri>? OutputDeviceUuid { get; set; }
}
