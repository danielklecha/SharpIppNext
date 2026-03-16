namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of binding.
/// See: PWG 5100.1-2022 Section 5.2.2.2
/// </summary>
public readonly record struct BindingType(string Value)
{
    public static readonly BindingType Adhesive = new("adhesive");
    public static readonly BindingType Comb = new("comb");
    public static readonly BindingType Flat = new("flat");
    public static readonly BindingType Padding = new("padding");
    public static readonly BindingType Perfect = new("perfect");
    public static readonly BindingType PlasticComb = new("plastic-comb");
    public static readonly BindingType Spiral = new("spiral");
    public static readonly BindingType Tape = new("tape");
    public static readonly BindingType Velo = new("velo");

    public override string ToString() => Value;
    public static implicit operator string(BindingType bin) => bin.Value;
    public static explicit operator BindingType(string value) => new(value);
}
