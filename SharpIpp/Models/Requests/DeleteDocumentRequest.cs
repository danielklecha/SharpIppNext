using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Delete-Document Operation.
/// See: PWG 5100.5-2003 Section 6.2
/// </summary>
public class DeleteDocumentRequest : IppRequest<DeleteDocumentOperationAttributes>, IIppPrinterRequest { }
