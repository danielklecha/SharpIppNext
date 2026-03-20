namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>overrides</c> member collection.
/// See: PWG 5100.6-2003 Section 8.2.4
/// </summary>
public class OverrideInstruction : IIppCollection
{
    public bool IsNoValue { get; set; }
    public string? Pages { get; set; }
    public int[]? DocumentNumbers { get; set; }
    public int? DocumentCopies { get; set; }
}
