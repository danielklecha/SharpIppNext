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
    /// See: PWG 5100.5-2024 Section 5.1
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int? DocumentNumber { get; set; }
}
