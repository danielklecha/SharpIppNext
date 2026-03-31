namespace SharpIpp.Protocol.Models;

/// <summary>
/// "job-sheets-col" member attributes.
/// See: PWG 5100.7-2023 Section 6.8.11 / Table 12.
/// </summary>
public class JobSheetsCol : IIppCollection
{
    /// <inheritdoc />
    public bool IsValue { get; set; } = true;

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public JobSheets? JobSheets { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
