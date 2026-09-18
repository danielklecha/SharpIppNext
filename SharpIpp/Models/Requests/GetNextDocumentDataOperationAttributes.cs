using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Next-Document-Data operation attributes.
/// See: PWG 5100.17-2014 Section 6.1.1
/// </summary>
[IppAttribute]
public class GetNextDocumentDataOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>job-id</c> operation attribute.
    /// See: PWG 5100.17-2014 Section 6.1.1
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.JobId, Tag = Tag.Integer)]
    public int? JobId { get; set; }

    /// <summary>
    /// The <c>document-data-wait</c> operation attribute.
    /// See: PWG 5100.17-2014 Section 6.1.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentDataWait, Tag = Tag.Boolean)]
    public bool? DocumentDataWait { get; set; }
}
