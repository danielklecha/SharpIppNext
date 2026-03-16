namespace SharpIpp.Protocol.Models;

/// <summary>
/// "document-format-details" member attributes.
/// DEPRECATED.
/// See: PWG 5100.7-2023 Section 6.1.2
/// </summary>
public class DocumentFormatDetails : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// name(MAX)
    /// </summary>
    public string? DocumentSourceApplicationName { get; set; }

    /// <summary>
    /// text(127)
    /// </summary>
    public string? DocumentSourceApplicationVersion { get; set; }

    /// <summary>
    /// name(40)
    /// </summary>
    public string? DocumentSourceOsName { get; set; }

    /// <summary>
    /// text(40)
    /// </summary>
    public string? DocumentSourceOsVersion { get; set; }
}
