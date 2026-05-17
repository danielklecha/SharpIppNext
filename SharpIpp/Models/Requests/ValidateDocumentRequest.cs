using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Validate-Document Operation.
/// <br/>
/// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as Validate-Document (Deprecated in PWG 5100.13-2023).
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
