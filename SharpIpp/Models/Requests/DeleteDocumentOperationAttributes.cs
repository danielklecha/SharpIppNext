using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Delete-Document Operation Attributes.
/// OBSOLETE.
/// See: PWG 5100.5-2024 and PWG 5100.18-2025
/// </summary>
[Obsolete("The 'Delete-Document' operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025.")]
[IppAttribute]
public class DeleteDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The document number within the job.
    /// See: PWG 5100.5-2024 Section 5.1
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.DocumentNumber, Tag = Tag.Integer)]
    public int? DocumentNumber { get; set; }
}
