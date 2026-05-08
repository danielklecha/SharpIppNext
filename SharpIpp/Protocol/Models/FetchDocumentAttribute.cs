namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>fetch-document-attributes-supported</code>.
/// See: PWG 5100.18-2015 Section 6.2.6
/// </summary>
public readonly record struct FetchDocumentAttribute(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly FetchDocumentAttribute DocumentFormat = new("document-format");
    public static readonly FetchDocumentAttribute DocumentName = new("document-name");
    public static readonly FetchDocumentAttribute DocumentNumber = new("document-number");
    public static readonly FetchDocumentAttribute DocumentState = new("document-state");

    public override string ToString() => Value;
    public static implicit operator string(FetchDocumentAttribute value) => value.Value;
    public static explicit operator FetchDocumentAttribute(string value) => new(value);
}
