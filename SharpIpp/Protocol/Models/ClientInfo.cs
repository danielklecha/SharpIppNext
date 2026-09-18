using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// "client-info" member attributes.
/// See: PWG 5100.7-2023 Section 6.1.1
/// </summary>
[IppAttribute(IppAttributeNames.ClientInfo)]
public class ClientInfo : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// name(127)
    /// </summary>
    [IppAttribute(Tag = Tag.NameWithoutLanguage)]
    public string? ClientName { get; set; }

    /// <summary>
    /// text(255) | no-value
    /// </summary>
    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? ClientPatches { get; set; }

    /// <summary>
    /// text(127)
    /// </summary>
    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? ClientStringVersion { get; set; }

    /// <summary>
    /// type2 enum
    /// </summary>
    [IppAttribute(Tag = Tag.Enum)]
    public ClientType? ClientType { get; set; }

    /// <summary>
    /// octetString(64) | no-value
    /// </summary>
    [IppAttribute(Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString? ClientVersion { get; set; }
}
