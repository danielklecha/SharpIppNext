using System;
using SharpIpp.Mapping;
using SharpIpp.Validation;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Resource operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.1
/// </summary>
[IppAttribute]
public class CancelResourceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    [Range(1, 2147483647)]
    [IppAttribute(IppAttributeNames.ResourceId, Tag = Tag.Integer)]
    public int? ResourceId { get; set; }
}

/// <summary>
/// Create-Resource operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.2
/// </summary>
[IppAttribute]
public class CreateResourceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-format</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.11
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceFormat, Tag = Tag.MimeMediaType)]
    public ResourceFormat? ResourceFormat { get; set; }

    /// <summary>
    /// The <c>resource-natural-language</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.17
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceNaturalLanguage, Tag = Tag.NaturalLanguage)]
    public string? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// The <c>resource-type</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.22
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceType, Tag = Tag.Keyword)]
    public ResourceType? ResourceType { get; set; }

    /// <summary>
    /// The <c>resource-name</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.2
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceName, Tag = Tag.NameWithoutLanguage)]
    public string? ResourceName { get; set; }

    /// <summary>
    /// The <c>resource-info</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.1
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceInfo, Tag = Tag.TextWithoutLanguage)]
    public string? ResourceInfo { get; set; }
}

/// <summary>
/// Install-Resource operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.4
/// </summary>
[IppAttribute]
public class InstallResourceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    [Range(1, 2147483647)]
    [IppAttribute(IppAttributeNames.ResourceId, Tag = Tag.Integer)]
    public int? ResourceId { get; set; }
}

/// <summary>
/// Send-Resource-Data operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.5
/// </summary>
[IppAttribute]
public class SendResourceDataOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    [Range(1, 2147483647)]
    [IppAttribute(IppAttributeNames.ResourceId, Tag = Tag.Integer)]
    public int? ResourceId { get; set; }

    /// <summary>
    /// The <c>resource-k-octets</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.16
    /// </summary>
    [Range(0, 2147483647)]
    [IppAttribute(IppAttributeNames.ResourceKOctets, Tag = Tag.Integer)]
    public int? ResourceKOctets { get; set; }

    /// <summary>
    /// The <c>resource-signature</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.19
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceSignature, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString[]? ResourceSignature { get; set; }
}

/// <summary>
/// Set-Resource-Attributes operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.6
/// </summary>
[IppAttribute]
public class SetResourceAttributesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    [Range(1, 2147483647)]
    [IppAttribute(IppAttributeNames.ResourceId, Tag = Tag.Integer)]
    public int? ResourceId { get; set; }

    /// <summary>
    /// The <c>resource-name</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.2
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceName, Tag = Tag.NameWithoutLanguage)]
    public string? ResourceName { get; set; }

    /// <summary>
    /// The <c>resource-info</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.1
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceInfo, Tag = Tag.TextWithoutLanguage)]
    public string? ResourceInfo { get; set; }

    /// <summary>
    /// The <c>resource-natural-language</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.17
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceNaturalLanguage, Tag = Tag.NaturalLanguage)]
    public string? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// The <c>resource-patches</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.18
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourcePatches, Tag = Tag.TextWithoutLanguage)]
    public string? ResourcePatches { get; set; }

    /// <summary>
    /// The <c>resource-string-version</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.21
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceStringVersion, Tag = Tag.TextWithoutLanguage)]
    public string? ResourceStringVersion { get; set; }

    /// <summary>
    /// The <c>resource-type</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.22
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceType, Tag = Tag.Keyword)]
    public ResourceType? ResourceType { get; set; }

    /// <summary>
    /// The <c>resource-version</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.24
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceVersion, Tag = Tag.TextWithoutLanguage)]
    public string? ResourceVersion { get; set; }
}
