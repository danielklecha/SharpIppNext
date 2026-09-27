using SharpIpp.Mapping;
using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Validate-Document Operation.
/// </summary>
[IppRequest(IppOperation.ValidateDocument)]
public class ValidateDocumentRequest : IppRequest<ValidateDocumentOperationAttributes>, IIppPrinterRequest
{
    /// <summary>
    /// The document-template-attributes IPP attribute group.
    /// See: PWG 5100.13-2023 Section 5.2.1
    /// </summary>
    public IppValue<DocumentTemplateAttributes>? DocumentTemplateAttributes { get; set; }
}
