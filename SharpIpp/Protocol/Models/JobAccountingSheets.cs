using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>job-accounting-sheets</c> collection.
/// See: PWG 5100.3-2023 Section 5.2.6
/// Deprecated in: PWG 5100.3-2023 Section 5.2.6
/// </summary>
[Obsolete("See PWG 5100.3-2023 Section 5.2.6.")]
public class JobAccountingSheets : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public OutputBin? JobAccountingOutputBin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public JobAccountingSheetsType? JobAccountingSheetsType { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
