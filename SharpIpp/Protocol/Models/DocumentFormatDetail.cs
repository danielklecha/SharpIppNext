namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>document-format-details</code>.
/// See: PWG 5100.7-2019 Section 6.9.2
/// </summary>
public readonly record struct DocumentFormatDetail(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly DocumentFormatDetail DocumentFormat = new("document-format");
    public static readonly DocumentFormatDetail DocumentFormatDeviceId = new("document-format-device-id");
    public static readonly DocumentFormatDetail DocumentFormatDetailsSupported = new("document-format-details-supported");
    public static readonly DocumentFormatDetail DocumentFormatName = new("document-format-name");
    public static readonly DocumentFormatDetail DocumentFormatVersion = new("document-format-version");
    public static readonly DocumentFormatDetail DocumentSourceApplicationName = new("document-source-application-name");
    public static readonly DocumentFormatDetail DocumentSourceApplicationVersion = new("document-source-application-version");
    public static readonly DocumentFormatDetail DocumentSourceOsName = new("document-source-os-name");
    public static readonly DocumentFormatDetail DocumentSourceOsVersion = new("document-source-os-version");

    public override string ToString() => Value;
    public static implicit operator string(DocumentFormatDetail value) => value.Value;
    public static explicit operator DocumentFormatDetail(string value) => new(value);
}
