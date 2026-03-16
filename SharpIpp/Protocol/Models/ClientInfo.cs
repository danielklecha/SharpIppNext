namespace SharpIpp.Protocol.Models;

/// <summary>
/// "client-info" member attributes.
/// See: PWG 5100.7-2023 Section 6.1.1
/// </summary>
public class ClientInfo : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// name(127)
    /// </summary>
    public string? ClientName { get; set; }

    /// <summary>
    /// text(255) | no-value
    /// </summary>
    public string? ClientPatches { get; set; }

    /// <summary>
    /// text(127)
    /// </summary>
    public string? ClientStringVersion { get; set; }

    /// <summary>
    /// type2 enum
    /// </summary>
    public ClientType? ClientType { get; set; }

    /// <summary>
    /// octetString(64) | no-value
    /// </summary>
    public string? ClientVersion { get; set; }
}
