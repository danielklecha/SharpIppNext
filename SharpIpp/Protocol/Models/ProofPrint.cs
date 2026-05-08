namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>proof-print</c> collection.
/// See: PWG 5100.11-2024 Section 5.2.6
/// </summary>
public class ProofPrint : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    public int? ProofPrintCopies { get; set; }
    public Media? Media { get; set; }
    public MediaCol? MediaCol { get; set; }
}
