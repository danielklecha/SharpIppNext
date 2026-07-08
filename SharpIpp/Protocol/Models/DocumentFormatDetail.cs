using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>document-format-details</code>.
/// See: PWG 5100.7-2023 Section 6.1.2
/// </summary>
public readonly record struct DocumentFormatDetail(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The document-format member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentFormat = new("document-format");
    /// <summary>
    /// The document-format-device-id member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentFormatDeviceId = new("document-format-device-id");
    /// <summary>
    /// The document-format-details-supported member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentFormatDetailsSupported = new("document-format-details-supported");
    /// <summary>
    /// The document-format-name member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentFormatName = new("document-format-name");
    /// <summary>
    /// The document-format-version member.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    [Obsolete("The 'document-format-version' member is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    public static readonly DocumentFormatDetail DocumentFormatVersion = new("document-format-version");
    /// <summary>
    /// The document-source-application-name member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentSourceApplicationName = new("document-source-application-name");
    /// <summary>
    /// The document-source-application-version member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentSourceApplicationVersion = new("document-source-application-version");
    /// <summary>
    /// The document-source-os-name member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentSourceOsName = new("document-source-os-name");
    /// <summary>
    /// The document-source-os-version member.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    public static readonly DocumentFormatDetail DocumentSourceOsVersion = new("document-source-os-version");

    public override string ToString() => Value;
    public static implicit operator string(DocumentFormatDetail value) => value.Value;
    public static implicit operator DocumentFormatDetail(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
