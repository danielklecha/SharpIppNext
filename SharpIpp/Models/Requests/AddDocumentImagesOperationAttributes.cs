using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Add-Document-Images operation attributes.
/// See: PWG 5100.15-2013 Section 6.4.1
/// </summary>
public class AddDocumentImagesOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>input-attributes</c> operation attribute.
    /// See: PWG 5100.15-2013 Section 7.4.17
    /// </summary>
    public DocumentTemplateAttributes? InputAttributes { get; set; }
}
