using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the document-digital-signature attribute values.
/// DEPRECATED.
/// See: PWG 5100.7-2023 Section 6.2.1
/// </summary>
[Obsolete("The 'document-digital-signature' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
public readonly record struct DocumentDigitalSignature(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly DocumentDigitalSignature None = new("none");
    public static readonly DocumentDigitalSignature Dss = new("dss");
    public static readonly DocumentDigitalSignature Pgp = new("pgp");
    public static readonly DocumentDigitalSignature Smime = new("smime");
    public static readonly DocumentDigitalSignature XmlDsig = new("xmldsig");

    public override string ToString() => Value;
    public static implicit operator string(DocumentDigitalSignature value) => value.Value;
    public static implicit operator DocumentDigitalSignature(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
