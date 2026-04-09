using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.3
/// </summary>
public class SetDocumentAttributesRequest : IppRequest<SetDocumentAttributesOperationAttributes>, IIppJobRequest
{
    /// <summary>
    /// The document-name IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.1.1
    /// </summary>
    /// <code>document-name</code>
    public string? DocumentName { get; set; }

    /// <summary>
    /// The document-template-attributes IPP attribute.
    /// See: IPP
    /// </summary>
    /// <code>document-template-attributes</code>
    public DocumentTemplateAttributes? DocumentTemplateAttributes { get; set; }
}
