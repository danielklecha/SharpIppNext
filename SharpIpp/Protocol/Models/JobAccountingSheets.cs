using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>job-accounting-sheets</c> collection.
/// See: PWG 5100.3-2023 Section 5.2.6
/// Deprecated in: PWG 5100.3-2023 Section 5.2.6
/// </summary>
[Obsolete("See PWG 5100.3-2023 Section 5.2.6.")]
[IppAttribute(IppAttributeNames.JobAccountingSheets)]
public class JobAccountingSheets : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<OutputBin>? JobAccountingOutputBin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<JobAccountingSheetsType>? JobAccountingSheetsType { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public IppValue<Media>? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public IppValue<MediaCol>? MediaCol { get; set; }
}
