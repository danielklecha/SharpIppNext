namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>accuracy-units</c> member attribute.
/// See: PWG 5100.21-2019 Section 6.8.14
/// </summary>
public readonly record struct AccuracyUnits(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly AccuracyUnits Micrometers = new("um");
    public static readonly AccuracyUnits Millimeters = new("mm");
    public static readonly AccuracyUnits Nanometers = new("nm");

    public override string ToString() => Value;
    public static implicit operator string(AccuracyUnits value) => value.Value;
    public static explicit operator AccuracyUnits(string value) => new(value);
}
