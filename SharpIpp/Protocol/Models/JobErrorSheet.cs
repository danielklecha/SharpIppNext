using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>job-error-sheet</c> collection.
/// See: PWG 5100.3-2023 Section 5.2.9
/// </summary>
[IppAttribute(IppAttributeNames.JobErrorSheet)]
public class JobErrorSheet : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<JobErrorSheetType>? JobErrorSheetType { get; set; }

    /// <summary>
    /// type2 keyword
    /// </summary>
    public IppValue<JobErrorSheetWhen>? JobErrorSheetWhen { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public IppValue<Media>? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public IppValue<MediaCol>? MediaCol { get; set; }
}
