namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>current-page-order</c> Document Status attribute values.
/// See: PWG 5100.5-2024 Section 6.2
/// See: PWG 5100.3-2023 Section 5.2.13
/// </summary>
public readonly record struct CurrentPageOrder(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>
    /// Pages are in 1-to-N order (first page first).
    /// See: PWG 5100.3-2023 Section 5.2.13
    /// </summary>
    public static readonly CurrentPageOrder OneToNOrder = new("1-to-n-order");

    /// <summary>
    /// Pages are in N-to-1 order (last page first).
    /// See: PWG 5100.3-2023 Section 5.2.13
    /// </summary>
    public static readonly CurrentPageOrder NTo1Order = new("n-to-1-order");

    public override string ToString() => Value;
    public static implicit operator string(CurrentPageOrder value) => value.Value;
    public static implicit operator CurrentPageOrder(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
