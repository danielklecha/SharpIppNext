namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>overrides</c> member collection.
/// See: PWG 5100.6-2003 Section 4.1
/// </summary>
public class OverrideInstruction : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// The selected page ranges (member attribute <c>pages</c>).
    /// </summary>
    public Range[]? PageRanges { get; set; }

    /// <summary>
    /// The selected document number ranges (member attribute <c>document-numbers</c>).
    /// </summary>
    public Range[]? DocumentNumberRanges { get; set; }

    /// <summary>
    /// The selected document copy ranges (member attribute <c>document-copies</c>).
    /// </summary>
    public Range[]? DocumentCopyRanges { get; set; }

    /// <summary>
    /// Job Template attributes that override the selected pages.
    /// </summary>
    public JobTemplateAttributes? JobTemplateAttributes { get; set; }
}
