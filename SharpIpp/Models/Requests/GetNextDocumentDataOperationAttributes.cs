namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Next-Document-Data operation attributes.
/// See: PWG 5100.17-2014 Section 6.1.1
/// </summary>
public class GetNextDocumentDataOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>document-data-wait</c> operation attribute.
    /// See: PWG 5100.17-2014 Section 6.1.1
    /// </summary>
    public bool? DocumentDataWait { get; set; }
}
