namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the finisher supply class.
/// See: PWG 5100.13-2023 Section 7.3
/// </summary>
public readonly record struct FinisherSupplyClass(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly FinisherSupplyClass Other = new("other");
    public static readonly FinisherSupplyClass Unknown = new("unknown");
    public static readonly FinisherSupplyClass Consumed = new("consumed");
    public static readonly FinisherSupplyClass Receptacle = new("receptacle");

    public override string ToString() => Value;
    public static implicit operator string(FinisherSupplyClass bin) => bin.Value;
    public static implicit operator FinisherSupplyClass(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
