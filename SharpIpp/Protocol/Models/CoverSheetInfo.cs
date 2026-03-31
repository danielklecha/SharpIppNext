namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>cover-sheet-info</c> collection.
/// See: PWG 5100.15-2013 Section 7.4.8
/// </summary>
public class CoverSheetInfo : IIppCollection
{
    public bool IsValue { get; set; } = true;
    public string? FromName { get; set; }
    public string? Logo { get; set; }
    public string? Message { get; set; }
    public string? OrganizationName { get; set; }
    public string? Subject { get; set; }
    public string? ToName { get; set; }
}
