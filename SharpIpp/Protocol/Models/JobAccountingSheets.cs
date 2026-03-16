using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// See: PWG 5100.3-2023 Section 5.2.7
/// </summary>
public class JobAccountingSheets : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public OutputBin? JobAccountingOutputBin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public JobSheetsType? JobAccountingSheetsType { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
