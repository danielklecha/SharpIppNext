namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the finisher supply type.
/// See: PWG 5100.13-2023 Section 7.3
/// </summary>
public readonly record struct FinisherSupplyType(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly FinisherSupplyType Other = new("other");
    public static readonly FinisherSupplyType Unknown = new("unknown");
    public static readonly FinisherSupplyType Thread = new("thread");
    public static readonly FinisherSupplyType Staples = new("staples");
    public static readonly FinisherSupplyType BandOuter = new("bandOuter");
    public static readonly FinisherSupplyType BandInner = new("bandInner");
    public static readonly FinisherSupplyType Coil = new("coil");
    public static readonly FinisherSupplyType Ring = new("ring");
    public static readonly FinisherSupplyType Wire = new("wire");
    public static readonly FinisherSupplyType Glue = new("glue");

    public override string ToString() => Value;
    public static implicit operator string(FinisherSupplyType bin) => bin.Value;
    public static implicit operator FinisherSupplyType(string value) => new(value);
}
