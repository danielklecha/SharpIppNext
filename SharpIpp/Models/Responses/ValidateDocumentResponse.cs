using System;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Validate-Document Response.
/// See: PWG 5100.13-2023 Section 5.2.2
/// </summary>
[Obsolete("See PWG 5100.13-2023 Section 5.2.")]
public class ValidateDocumentResponse : IppResponse<ValidateOperationAttributes>
{
}
