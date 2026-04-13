using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Identify-Printer operation attributes.
/// See: PWG 5100.18-2025 Section 5.2.1
/// </summary>
public class AcknowledgeIdentifyPrinterOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }
}
