namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>fetch-document-attributes-supported</code>.
/// See: PWG 5100.18-2025 Section 7.4.2
/// </summary>
public readonly record struct FetchDocumentAttribute(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>
    /// The document-format attribute.
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    public static readonly FetchDocumentAttribute DocumentFormat = new("document-format");
    /// <summary>
    /// The document-name attribute.
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    public static readonly FetchDocumentAttribute DocumentName = new("document-name");
    /// <summary>
    /// The document-number attribute.
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    public static readonly FetchDocumentAttribute DocumentNumber = new("document-number");
    /// <summary>
    /// The document-state attribute.
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    public static readonly FetchDocumentAttribute DocumentState = new("document-state");

    public override string ToString() => Value;
    public static implicit operator string(FetchDocumentAttribute value) => value.Value;
    public static explicit operator FetchDocumentAttribute(string value) => new(value);
}
