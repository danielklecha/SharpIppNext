namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of binding.
/// See: PWG 5100.1-2022 Section 5.2.2.2
/// </summary>
public readonly record struct BindingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum 
{
    /// <summary>
    /// Adhesive binding (glued spine).
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Adhesive = new("adhesive");

    /// <summary>
    /// Comb binding (plastic comb).
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Comb = new("comb");

    /// <summary>
    /// Flat binding.
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Flat = new("flat");

    /// <summary>
    /// Padding binding (glued at one edge, like a notepad).
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Padding = new("padding");

    /// <summary>
    /// Perfect binding (square-spine glued binding).
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Perfect = new("perfect");

    /// <summary>
    /// Spiral binding (wire or plastic coil).
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Spiral = new("spiral");

    /// <summary>
    /// Tape binding.
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Tape = new("tape");

    /// <summary>
    /// Velo binding (plastic strip binding).
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public static readonly BindingType Velo = new("velo");

    public override string ToString() => Value;
    public static implicit operator string(BindingType bin) => bin.Value;
    public static implicit operator BindingType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
