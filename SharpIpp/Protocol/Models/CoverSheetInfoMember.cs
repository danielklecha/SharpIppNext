namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>cover-sheet-info</code>.
/// See: PWG 5100.15-2014 Section 7.2.2
/// </summary>
public readonly record struct CoverSheetInfoMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The name of the sender on the cover sheet.
    /// See: PWG 5100.15-2014 Section 7.2.2
    /// </summary>
    public static readonly CoverSheetInfoMember FromName = new("from-name");

    /// <summary>
    /// The message body on the cover sheet.
    /// See: PWG 5100.15-2014 Section 7.2.2
    /// </summary>
    public static readonly CoverSheetInfoMember Message = new("message");

    /// <summary>
    /// The organization name on the cover sheet.
    /// See: PWG 5100.15-2014 Section 7.2.2
    /// </summary>
    public static readonly CoverSheetInfoMember OrganizationName = new("organization-name");

    /// <summary>
    /// The subject line on the cover sheet.
    /// See: PWG 5100.15-2014 Section 7.2.2
    /// </summary>
    public static readonly CoverSheetInfoMember Subject = new("subject");

    /// <summary>
    /// The name of the recipient on the cover sheet.
    /// See: PWG 5100.15-2014 Section 7.2.2
    /// </summary>
    public static readonly CoverSheetInfoMember ToName = new("to-name");

    public override string ToString() => Value;
    public static implicit operator string(CoverSheetInfoMember value) => value.Value;
    public static explicit operator CoverSheetInfoMember(string value) => new(value);
}
