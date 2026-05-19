using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Delete-Document Operation Attributes.
/// OBSOLETE.
/// See: PWG 5100.5-2024 and PWG 5100.18-2025
/// </summary>
[Obsolete("The 'Delete-Document' operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025.")]
public class DeleteDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The document number within the job.
    /// Type: integer(1:MAX)
    /// See: PWG 5100.5-2024 Section 5.1
    /// </summary>
    public int? DocumentNumber { get; set; }
}
