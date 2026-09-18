using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Fetch-Document operation attributes.
/// See: PWG 5100.18-2025 Section 5.5.1
/// </summary>
[IppAttribute]
public class FetchDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>document-number</c> operation attribute.
    /// See: PWG 5100.5-2024 Section 5.1.2
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.DocumentNumber, Tag = Tag.Integer)]
    public int DocumentNumber { get; set; }

    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>compression-accepted</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.3
    /// </summary>
    [IppAttribute(IppAttributeNames.CompressionAccepted, Tag = Tag.Keyword)]
    public Compression[]? CompressionAccepted { get; set; }

    /// <summary>
    /// The <c>document-format-accepted</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.4
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatAccepted, Tag = Tag.MimeMediaType)]
    public string[]? DocumentFormatAccepted { get; set; }
}
