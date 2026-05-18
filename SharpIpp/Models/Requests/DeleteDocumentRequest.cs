using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Delete-Document Operation.
/// OBSOLETE.
/// See: PWG 5100.5-2024 and PWG 5100.18-2025
/// </summary>
[Obsolete("The 'Delete-Document' operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025.")]
public class DeleteDocumentRequest : IppRequest<DeleteDocumentOperationAttributes>, IIppPrinterRequest { }
