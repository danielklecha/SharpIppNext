using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Identify-Printer operation attributes.
/// See: PWG 5100.13-2023 Section 5.1.1
/// </summary>
public class IdentifyPrinterOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>identify-actions</c> operation attribute.
    /// See: PWG 5100.13-2023 Section 6.1.4
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
    /// Type: integer(1:MAX)
    /// See: PWG 5100.18-2025 Section 8.5
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    /// <code>job-id</code>
    public int? JobId { get; set; }

    /// <summary>
    /// The <c>message</c> operation attribute.
    /// Type: text(127)
    /// See: PWG 5100.13-2023 Section 5.1.1
    /// </summary>
    /// <code>message</code>
    public string? Message { get; set; }
}
