using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Resource operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.1
/// </summary>
public class CancelResourceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    public int? ResourceId { get; set; }
}

/// <summary>
/// Create-Resource operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.2
/// </summary>
public class CreateResourceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-format</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.11
    /// </summary>
    public string? ResourceFormat { get; set; }

    /// <summary>
    /// The <c>resource-natural-language</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.17
    /// </summary>
    public string? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// The <c>resource-type</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.22
    /// </summary>
    public ResourceType? ResourceType { get; set; }

    /// <summary>
    /// The <c>resource-name</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.2
    /// </summary>
    public string? ResourceName { get; set; }

    /// <summary>
    /// The <c>resource-info</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.1
    /// </summary>
    public string? ResourceInfo { get; set; }
}

/// <summary>
/// Install-Resource operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.4
/// </summary>
public class InstallResourceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    public int? ResourceId { get; set; }
}

/// <summary>
/// Send-Resource-Data operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.5
/// </summary>
public class SendResourceDataOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    public int? ResourceId { get; set; }

    /// <summary>
    /// The <c>resource-k-octets</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.16
    /// </summary>
    public int? ResourceKOctets { get; set; }

    /// <summary>
    /// The <c>resource-signature</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.19
    /// </summary>
    public byte[][]? ResourceSignature { get; set; }
}

/// <summary>
/// Set-Resource-Attributes operation attributes.
/// See: PWG 5100.22-2025 Section 6.2.6
/// </summary>
public class SetResourceAttributesOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-id</c> operation attribute identifying the target Resource.
    /// See: PWG 5100.22-2025 Section 7.1.14
    /// </summary>
    public int? ResourceId { get; set; }

    /// <summary>
    /// The <c>resource-name</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.2
    /// </summary>
    public string? ResourceName { get; set; }

    /// <summary>
    /// The <c>resource-info</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.8.1
    /// </summary>
    public string? ResourceInfo { get; set; }

    /// <summary>
    /// The <c>resource-natural-language</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.17
    /// </summary>
    public string? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// The <c>resource-patches</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.18
    /// </summary>
    public string? ResourcePatches { get; set; }

    /// <summary>
    /// The <c>resource-string-version</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.21
    /// </summary>
    public string? ResourceStringVersion { get; set; }

    /// <summary>
    /// The <c>resource-type</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.22
    /// </summary>
    public ResourceType? ResourceType { get; set; }

    /// <summary>
    /// The <c>resource-version</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.24
    /// </summary>
    public string? ResourceVersion { get; set; }
}
