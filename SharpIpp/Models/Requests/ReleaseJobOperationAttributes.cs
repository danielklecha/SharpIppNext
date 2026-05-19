using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class ReleaseJobOperationAttributes : CancelJobOperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// See: PWG 5100.18-2025 Section 8.6
    /// </summary>
    /// <code>output-device-uuid</code>
    public Uri? OutputDeviceUuid { get; set; }
}
