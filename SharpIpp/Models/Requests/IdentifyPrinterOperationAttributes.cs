using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Identify-Printer operation attributes.
/// See: PWG 5100.13-2023 Section 6.8.4
/// </summary>
public class IdentifyPrinterOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>identify-actions</c> operation attribute.
    /// See: PWG 5100.13-2023 Section 6.8.4
    /// </summary>
    /// <code>identify-actions</code>
    public IdentifyAction[]? IdentifyActions { get; set; }

    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    /// <code>output-device-uuid</code>
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The optional <c>job-id</c> target attribute for Identify-Printer.
    /// See: PWG 5100.18-2025 Section 8.5
    /// </summary>
    /// <code>job-id</code>
    public int? JobId { get; set; }
}
