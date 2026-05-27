namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>accuracy-units</c> member attribute.
/// See: PWG 5100.21-2019 Section 8.1.6.1
/// </summary>
public readonly record struct AccuracyUnits(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// Accuracy measured in micrometers (µm).
    /// See: PWG 5100.21-2019 Section 8.1.6.1
    /// </summary>
    public static readonly AccuracyUnits Micrometers = new("um");

    /// <summary>
    /// Accuracy measured in millimeters (mm).
    /// See: PWG 5100.21-2019 Section 8.1.6.1
    /// </summary>
    public static readonly AccuracyUnits Millimeters = new("mm");

    /// <summary>
    /// Accuracy measured in nanometers (nm).
    /// See: PWG 5100.21-2019 Section 8.1.6.1
    /// </summary>
    public static readonly AccuracyUnits Nanometers = new("nm");

    public override string ToString() => Value;
    public static implicit operator string(AccuracyUnits value) => value.Value;
    public static implicit operator AccuracyUnits(string value) => new(value);
}
