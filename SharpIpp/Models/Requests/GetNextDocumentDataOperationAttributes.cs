namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Next-Document-Data operation attributes.
/// See: PWG 5100.17-2014 Section 6.1.1
/// </summary>
public class GetNextDocumentDataOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>job-id</c> operation attribute.
    /// Type: integer(1:MAX)
    /// See: PWG 5100.17-2014 Section 6.1.1
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    public int? JobId { get; set; }

    /// <summary>
    /// The <c>document-data-wait</c> operation attribute.
    /// See: PWG 5100.17-2014 Section 6.1.1
    /// </summary>
    public bool? DocumentDataWait { get; set; }
}
