using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Add-Document-Images operation attributes.
/// See: PWG 5100.15-2013 Section 6.1.1
/// </summary>
[IppAttribute]
public class AddDocumentImagesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>job-id</c> operation attribute.
    /// See: PWG 5100.15-2013 Section 6.1.1
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.JobId, Tag = Tag.Integer)]
    public IppValue<int>? JobId { get; set; }

    /// <summary>
    /// The <c>input-attributes</c> operation attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1
    /// </summary>
    [IppAttribute(IppAttributeNames.InputAttributes)]
    public IppValue<DocumentTemplateAttributes>? InputAttributes { get; set; }
}
