namespace SharpIpp.Protocol.Models;

/// <summary>
///     Specifies whether the Printer's Page Description Language (PDL) override is supported.
///     See: RFC 8011 Section 5.4.28
/// </summary>
public enum PdlOverride
{
    Attempted,
    NotAttempted
}
