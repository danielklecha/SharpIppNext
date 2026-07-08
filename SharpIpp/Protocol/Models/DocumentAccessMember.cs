namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>document-access</code>.
/// See: PWG 5100.18-2025 Section 7.1.2
/// </summary>
public readonly record struct DocumentAccessMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The Cancel-Document operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember CancelDocument = new("cancel-document");

    /// <summary>
    /// The Get-Document-Attributes operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember GetDocumentAttributes = new("get-document-attributes");

    /// <summary>
    /// The Get-Document-Data operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember GetDocumentData = new("get-document-data");

    /// <summary>
    /// The Get-Next-Document-Data operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember GetNextDocumentData = new("get-next-document-data");

    /// <summary>
    /// The Release-Document operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember ReleaseDocument = new("release-document");

    /// <summary>
    /// The Send-Document operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember SendDocument = new("send-document");

    /// <summary>
    /// The Send-URI operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember SendUri = new("send-uri");

    /// <summary>
    /// The Set-Document-Attributes operation access.
    /// See: PWG 5100.18-2025 Section 7.1.2
    /// </summary>
    public static readonly DocumentAccessMember SetDocumentAttributes = new("set-document-attributes");

    public override string ToString() => Value;
    public static implicit operator string(DocumentAccessMember value) => value.Value;
    public static implicit operator DocumentAccessMember(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
