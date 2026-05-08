namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known values for <code>pdf-features-supported</code>.
/// See: PWG 5100.21-2019 Section 8.1.24
/// </summary>
public readonly record struct PdfFeature(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly PdfFeature Annotation = new("annotation");
    public static readonly PdfFeature Form = new("form");
    public static readonly PdfFeature Outline = new("outline");
    public static readonly PdfFeature Signature = new("signature");
    public static readonly PdfFeature StructElement = new("struct-element");
    public static readonly PdfFeature TrapNet = new("trapnet");

    public override string ToString() => Value;
    public static implicit operator string(PdfFeature value) => value.Value;
    public static explicit operator PdfFeature(string value) => new(value);
}
