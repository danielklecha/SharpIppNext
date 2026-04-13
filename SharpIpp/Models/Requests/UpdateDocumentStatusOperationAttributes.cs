using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Document-Status operation attributes.
/// See: PWG 5100.18-2025 Section 5.8.1
/// </summary>
public class UpdateDocumentStatusOperationAttributes : JobOperationAttributes
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
}
