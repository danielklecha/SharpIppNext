namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the unit of measure for level.
/// See: PWG 5100.13-2023 Section 6.6.9
/// </summary>
public readonly record struct CapacityUnit(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly CapacityUnit Other = new("other");
    public static readonly CapacityUnit Unknown = new("unknown");
    public static readonly CapacityUnit TenThousandthsOfInches = new("tenThousandthsOfInches");
    public static readonly CapacityUnit Micrometers = new("micrometers");
    public static readonly CapacityUnit TenthsOfOunces = new("tenthsOfOunces");
    public static readonly CapacityUnit TenthsOfGrams = new("tenthsOfGrams");
    public static readonly CapacityUnit HundredthsOfFluidOunces = new("hundredthsOfFluidOunces");
    public static readonly CapacityUnit TenthsOfMilliliters = new("tenthsOfMilliliters");
    public static readonly CapacityUnit Percent = new("percent");
    public static readonly CapacityUnit Items = new("items");
    public static readonly CapacityUnit DisplayCol = new("displayCol");
    public static readonly CapacityUnit TenthsOfHours = new("tenthsOfHours");

    public override string ToString() => Value;
    public static implicit operator string(CapacityUnit bin) => bin.Value;
    public static implicit operator CapacityUnit(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
