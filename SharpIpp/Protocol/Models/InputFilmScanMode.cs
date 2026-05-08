namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-film-scan-mode</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.8
/// </summary>
public readonly record struct InputFilmScanMode(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly InputFilmScanMode Positive = new("positive");
    public static readonly InputFilmScanMode Negative = new("negative");

    public override string ToString() => Value;
    public static implicit operator string(InputFilmScanMode value) => value.Value;
    public static explicit operator InputFilmScanMode(string value) => new(value);
}
