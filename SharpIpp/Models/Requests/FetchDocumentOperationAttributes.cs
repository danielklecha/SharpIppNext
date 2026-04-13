using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Fetch-Document operation attributes.
/// See: PWG 5100.18-2025 Section 5.5.1
/// </summary>
public class FetchDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>document-number</c> operation attribute.
    /// See: PWG 5100.5-2024 Section 5.1.2
    /// </summary>
    public int DocumentNumber { get; set; }

    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>compression-accepted</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.3
    /// </summary>
    public Compression[]? CompressionAccepted { get; set; }

    /// <summary>
    /// The <c>document-format-accepted</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.4
    /// </summary>
    public string[]? DocumentFormatAccepted { get; set; }
}
