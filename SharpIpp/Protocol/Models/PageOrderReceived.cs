namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>page-order-received</c> Job/Document Template attribute values.
/// See: PWG 5100.5-2024 Section 6.3 and PWG 5100.3-2001.
/// </summary>
public readonly record struct PageOrderReceived(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly PageOrderReceived OneToNOrder = new("1-to-n-order");
    public static readonly PageOrderReceived NTo1Order = new("n-to-1-order");

    public override string ToString() => Value;
    public static implicit operator string(PageOrderReceived value) => value.Value;
    public static explicit operator PageOrderReceived(string value) => new(value);
}
