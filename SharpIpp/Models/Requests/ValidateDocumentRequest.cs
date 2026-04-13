using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Validate-Document Operation.
/// See: PWG 5100.13-2023 Section 5.2
/// </summary>
public class ValidateDocumentRequest : IppRequest<ValidateDocumentOperationAttributes>, IIppPrinterRequest
{
    /// <summary>
    /// The document-template-attributes IPP attribute group.
    /// See: PWG 5100.13-2023 Section 5.2.1
    /// </summary>
    public DocumentTemplateAttributes? DocumentTemplateAttributes { get; set; }
}