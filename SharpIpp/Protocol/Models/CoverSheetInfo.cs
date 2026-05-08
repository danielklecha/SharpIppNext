namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>cover-sheet-info</c> collection.
/// See: PWG 5100.15-2013 Section 7.4.8
/// </summary>
public class CoverSheetInfo : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    public string? FromName { get; set; }
    public string? Logo { get; set; }
    public string? Message { get; set; }
    public string? OrganizationName { get; set; }
    public string? Subject { get; set; }
    public string? ToName { get; set; }
}
