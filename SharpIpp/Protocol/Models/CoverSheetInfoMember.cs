namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>cover-sheet-info</code>.
/// See: PWG 5100.18-2015 Section 6.2.2
/// </summary>
public readonly record struct CoverSheetInfoMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly CoverSheetInfoMember FromName = new("from-name");
    public static readonly CoverSheetInfoMember Message = new("message");
    public static readonly CoverSheetInfoMember OrganizationName = new("organization-name");
    public static readonly CoverSheetInfoMember Subject = new("subject");
    public static readonly CoverSheetInfoMember ToName = new("to-name");

    public override string ToString() => Value;
    public static implicit operator string(CoverSheetInfoMember value) => value.Value;
    public static explicit operator CoverSheetInfoMember(string value) => new(value);
}
