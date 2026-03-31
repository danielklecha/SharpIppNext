namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>page-delivery</c> Job/Document Template attribute values.
/// See: PWG 5100.3-2023 Section 5.2.14
/// </summary>
public readonly record struct PageDelivery(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly PageDelivery SameOrderFaceUp = new("same-order-face-up");
    public static readonly PageDelivery SameOrderFaceDown = new("same-order-face-down");
    public static readonly PageDelivery ReverseOrderFaceUp = new("reverse-order-face-up");
    public static readonly PageDelivery ReverseOrderFaceDown = new("reverse-order-face-down");

    /// <summary>
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public static readonly PageDelivery SystemSpecified = new("system-specified");

    public override string ToString() => Value;
    public static implicit operator string(PageDelivery bin) => bin.Value;
    public static explicit operator PageDelivery(string value) => new(value);
}
