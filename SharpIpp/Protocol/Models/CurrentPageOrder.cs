namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the current-page-order.
/// See: PWG 5100.1-2022 Section 6.6
/// </summary>
public readonly record struct CurrentPageOrder(string Value)
{
    public static readonly CurrentPageOrder OneToNOrder = new("1-to-n-order");
    public static readonly CurrentPageOrder NToOneOrder = new("n-to-1-order");
    public static readonly CurrentPageOrder OneToN = new("1-to-n");

    public override string ToString() => Value;
    public static implicit operator string(CurrentPageOrder bin) => bin.Value;
    public static explicit operator CurrentPageOrder(string value) => new(value);
}
