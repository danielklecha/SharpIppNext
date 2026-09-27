using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// "job-sheets-col" member attributes.
/// See: PWG 5100.7-2023 Section 6.8.11 / Table 12.
/// </summary>
[IppAttribute(IppAttributeNames.JobSheetsCol)]
public class JobSheetsCol : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<JobSheets>? JobSheets { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<Media>? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public IppValue<MediaCol>? MediaCol { get; set; }
}
