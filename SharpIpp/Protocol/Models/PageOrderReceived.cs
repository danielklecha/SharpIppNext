namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the page-order-received.
/// See: PWG 5100.1-2022 Section 6.18
/// </summary>
public readonly record struct PageOrderReceived(string Value)
{
    public static readonly PageOrderReceived Forward = new("forward");
    public static readonly PageOrderReceived Reverse = new("reverse");
    public static readonly PageOrderReceived OneToNOrder = new("1-to-n-order");

    public override string ToString() => Value;
    public static implicit operator string(PageOrderReceived bin) => bin.Value;
    public static explicit operator PageOrderReceived(string value) => new(value);
}
