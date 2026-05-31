namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the character repertoire.
/// See: PWG 5101.2-2004 Section 3.1
/// </summary>
public readonly record struct Repertoire(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// unicode_utf-8
    /// </summary>
    public static readonly Repertoire UnicodeUtf8 = new("unicode_utf-8", false);

    /// <summary>
    /// iana_iso_8859-1
    /// </summary>
    public static readonly Repertoire IanaIso88591 = new("iana_iso_8859-1", false);

    /// <summary>
    /// iana_utf-8
    /// </summary>
    public static readonly Repertoire IanaUtf8 = new("iana_utf-8", false);

    public override string ToString() => Value;
    public static implicit operator string(Repertoire val) => val.Value;
    public static implicit operator Repertoire(string value) => new(value);
}
