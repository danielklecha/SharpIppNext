namespace SharpIpp.Protocol.Models;

/// <summary>
/// IPP print-quality attribute values.
/// See: RFC 8011 Section 5.2.13
/// </summary>
public enum PrintQuality
{
    /// <summary>
    /// Lowest quality available on the printer.
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    Draft = 3,

    /// <summary>
    /// Normal or intermediate quality on the printer.
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    Normal = 4,

    /// <summary>
    /// Highest quality available on the printer.
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    High = 5,
}
