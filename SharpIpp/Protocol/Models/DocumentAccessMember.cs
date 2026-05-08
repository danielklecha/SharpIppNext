namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>document-access</code>.
/// See: PWG 5100.18-2015 Section 6.2.5
/// </summary>
public readonly record struct DocumentAccessMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly DocumentAccessMember CancelDocument = new("cancel-document");
    public static readonly DocumentAccessMember GetDocumentAttributes = new("get-document-attributes");
    public static readonly DocumentAccessMember GetDocumentData = new("get-document-data");
    public static readonly DocumentAccessMember GetNextDocumentData = new("get-next-document-data");
    public static readonly DocumentAccessMember ReleaseDocument = new("release-document");
    public static readonly DocumentAccessMember SendDocument = new("send-document");
    public static readonly DocumentAccessMember SendUri = new("send-uri");
    public static readonly DocumentAccessMember SetDocumentAttributes = new("set-document-attributes");

    public override string ToString() => Value;
    public static implicit operator string(DocumentAccessMember value) => value.Value;
    public static explicit operator DocumentAccessMember(string value) => new(value);
}
