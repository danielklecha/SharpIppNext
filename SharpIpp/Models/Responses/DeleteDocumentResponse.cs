using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Delete-Document Response.
/// OBSOLETE.
/// See: PWG 5100.5-2024 and PWG 5100.18-2025
/// </summary>
[Obsolete("The 'Delete-Document' operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025.")]
public class DeleteDocumentResponse : IppResponse<OperationAttributes> { }
