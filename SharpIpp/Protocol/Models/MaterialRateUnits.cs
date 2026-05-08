namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-rate-units</c> member attribute.
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public readonly record struct MaterialRateUnits(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly MaterialRateUnits GramsPerMinute = new("g-per-min");
    public static readonly MaterialRateUnits InchesPerMinute = new("in-per-min");
    public static readonly MaterialRateUnits LitersPerMinute = new("liter-per-min");
    public static readonly MaterialRateUnits MetersPerMinute = new("m-per-min");
    public static readonly MaterialRateUnits MillilitersPerMinute = new("ml-per-min");
    public static readonly MaterialRateUnits MillimetersPerMinute = new("mm-per-min");
    public static readonly MaterialRateUnits OuncesPerMinute = new("oz-per-min");

    public override string ToString() => Value;
    public static implicit operator string(MaterialRateUnits value) => value.Value;
    public static explicit operator MaterialRateUnits(string value) => new(value);
}
