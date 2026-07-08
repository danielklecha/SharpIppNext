namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known values for <code>pdf-features-supported</code>.
/// See: PWG 5100.21-2019 Section 8.3.22
/// </summary>
public readonly record struct PdfFeature(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The Printer supports PDF annotation features.
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public static readonly PdfFeature Annotation = new("annotation");

    /// <summary>
    /// The Printer supports PDF form features.
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public static readonly PdfFeature Form = new("form");

    /// <summary>
    /// The Printer supports PDF outline (bookmark) features.
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public static readonly PdfFeature Outline = new("outline");

    /// <summary>
    /// The Printer supports PDF digital signature features.
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public static readonly PdfFeature Signature = new("signature");

    /// <summary>
    /// The Printer supports PDF structure element (tagged PDF) features.
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public static readonly PdfFeature StructElement = new("struct-element");

    /// <summary>
    /// The Printer supports PDF trap network annotation features.
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public static readonly PdfFeature TrapNet = new("trapnet");

    public override string ToString() => Value;
    public static implicit operator string(PdfFeature value) => value.Value;
    public static implicit operator PdfFeature(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
