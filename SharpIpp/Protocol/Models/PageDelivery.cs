namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the page delivery.
/// See: PWG 5100.1-2022 Section 6.17
/// </summary>
public readonly record struct PageDelivery(string Value)
{
    public static readonly PageDelivery SameOrderFaceUp = new("same-order-face-up");
    public static readonly PageDelivery SameOrderFaceDown = new("same-order-face-down");
    public static readonly PageDelivery ReverseOrderFaceUp = new("reverse-order-face-up");
    public static readonly PageDelivery ReverseOrderFaceDown = new("reverse-order-face-down");

    public override string ToString() => Value;
    public static implicit operator string(PageDelivery bin) => bin.Value;
    public static explicit operator PageDelivery(string value) => new(value);
}
