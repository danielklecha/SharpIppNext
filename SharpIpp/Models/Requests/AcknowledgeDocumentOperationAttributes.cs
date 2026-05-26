using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Document operation attributes.
/// See: PWG 5100.18-2025 Section 5.1
/// See: PWG 5100.18-2025 Section 5.1.1
/// See: PWG 5100.18-2025 Section 14.4
/// </summary>
public class AcknowledgeDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>document-number</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.5-2024 Section 5.1.2
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int DocumentNumber { get; set; }

    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// See: PWG 5100.18-2025 Section 14.1
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>fetch-status-code</c> operation attribute.
    /// Allowed values include all registered IPP status codes (except 'successful-ok' (0x0000)).
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.5
    /// See: PWG 5100.18-2025 Section 14.3
    /// </summary>
    public IppStatusCode? FetchStatusCode { get; set; }

    /// <summary>
    /// The <c>fetch-status-message</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.6
    /// See: PWG 5100.18-2025 Section 14.1
    /// </summary>
    public string? FetchStatusMessage { get; set; }
}
