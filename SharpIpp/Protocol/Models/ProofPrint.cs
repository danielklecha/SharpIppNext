using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>proof-print</c> collection.
/// DEPRECATED.
/// See: PWG 5100.11-2024 Section 5.2.6
/// </summary>
[Obsolete("The 'proof-print' attribute is deprecated in favor of 'proof-print-default'. See PWG 5100.11-2024 Section 5.2.6.")]
public class ProofPrint : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public int? ProofPrintCopies { get; set; }
    public Media? Media { get; set; }
    public MediaCol? MediaCol { get; set; }
}
