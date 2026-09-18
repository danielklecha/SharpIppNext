using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Document-Status operation attributes.
/// See: PWG 5100.18-2025 Section 5.8.1
/// </summary>
[IppAttribute]
public class UpdateDocumentStatusOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>document-number</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.8.1
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
}
