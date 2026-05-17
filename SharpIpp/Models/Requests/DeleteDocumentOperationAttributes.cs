namespace SharpIpp.Models.Requests;

/// <summary>
/// Delete-Document Operation Attributes.
/// See: PWG 5100.5-2003 Section 6.2
/// </summary>
public class DeleteDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The document number within the job.
    /// See: PWG 5100.5-2024 Section 5.1
    /// </summary>
    public int? DocumentNumber { get; set; }
}
