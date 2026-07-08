namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>page-delivery</c> Job/Document Template attribute values.
/// See: PWG 5100.3-2023 Section 5.2.14
/// </summary>
public readonly record struct PageDelivery(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Pages are delivered in the same order as printed, face up.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public static readonly PageDelivery SameOrderFaceUp = new("same-order-face-up");

    /// <summary>
    /// Pages are delivered in the same order as printed, face down.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public static readonly PageDelivery SameOrderFaceDown = new("same-order-face-down");

    /// <summary>
    /// Pages are delivered in reverse order, face up.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public static readonly PageDelivery ReverseOrderFaceUp = new("reverse-order-face-up");

    /// <summary>
    /// Pages are delivered in reverse order, face down.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public static readonly PageDelivery ReverseOrderFaceDown = new("reverse-order-face-down");

    /// <summary>
    /// The Printer selects the page delivery order.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public static readonly PageDelivery SystemSpecified = new("system-specified");

    public override string ToString() => Value;
    public static implicit operator string(PageDelivery bin) => bin.Value;
    public static implicit operator PageDelivery(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
