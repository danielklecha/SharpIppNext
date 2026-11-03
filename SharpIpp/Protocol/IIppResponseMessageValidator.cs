using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Defines a validator for IPP response messages.
/// This is a low-end validator focusing on syntactic, structural, and protocol-level constraint validation
/// (such as versioning, character sets, attribute uniqueness, and basic response rules)
/// rather than high-end business logic.
/// </summary>
public interface IIppResponseMessageValidator : IIppMessageValidator
{
    /// <summary>
    /// Validates the response message, optionally against operation-specific expectations.
    /// </summary>
    /// <param name="response">The response message to validate.</param>
    /// <param name="operation">The IPP operation that triggered this response, or <c>null</c>.</param>
    void Validate(IIppResponseMessage? response, IppOperation? operation = null);
}
