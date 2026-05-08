using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>job-error-sheet</c> collection.
/// See: PWG 5100.3-2023 Section 5.2.9
/// </summary>
public class JobErrorSheet : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public JobSheetsType? JobErrorSheetType { get; set; }

    /// <summary>
    /// type2 keyword
    /// </summary>
    public JobErrorSheetWhen? JobErrorSheetWhen { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
