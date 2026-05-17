using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Add-Document-Images operation attributes.
/// See: PWG 5100.15-2013 Section 6.1.1
/// </summary>
public class AddDocumentImagesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>job-id</c> operation attribute.
    /// See: PWG 5100.15-2013 Section 6.1.1
    /// </summary>
    public int? JobId { get; set; }

    /// <summary>
    /// The <c>input-attributes</c> operation attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1
    /// </summary>
    public DocumentTemplateAttributes? InputAttributes { get; set; }
}
