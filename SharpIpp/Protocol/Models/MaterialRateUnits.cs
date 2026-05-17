namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-rate-units</c> member attribute.
/// See: PWG 5100.21-2019 Section 8.1.3.12
/// </summary>
public readonly record struct MaterialRateUnits(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>Rate measured in grams per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits GramsPerMinute = new("g-per-min");
    /// <summary>Rate measured in inches per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits InchesPerMinute = new("in-per-min");
    /// <summary>Rate measured in liters per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits LitersPerMinute = new("liter-per-min");
    /// <summary>Rate measured in meters per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits MetersPerMinute = new("m-per-min");
    /// <summary>Rate measured in milliliters per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits MillilitersPerMinute = new("ml-per-min");
    /// <summary>Rate measured in millimeters per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits MillimetersPerMinute = new("mm-per-min");
    /// <summary>Rate measured in ounces per minute. See: PWG 5100.21-2019 Section 8.1.3.12</summary>
    public static readonly MaterialRateUnits OuncesPerMinute = new("oz-per-min");

    public override string ToString() => Value;
    public static implicit operator string(MaterialRateUnits value) => value.Value;
    public static explicit operator MaterialRateUnits(string value) => new(value);
}
