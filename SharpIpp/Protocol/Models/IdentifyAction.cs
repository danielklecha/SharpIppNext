namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>identify-actions</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.8.4
/// </summary>
public readonly record struct IdentifyAction(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer displays a visual indication on its control panel or display.
    /// See: PWG 5100.13-2023 Section 6.8.4
    /// </summary>
    public static readonly IdentifyAction Display = new("display");

    /// <summary>
    /// The Printer flashes a light or indicator.
    /// See: PWG 5100.13-2023 Section 6.8.4
    /// </summary>
    public static readonly IdentifyAction Flash = new("flash");

    /// <summary>
    /// The Printer emits an audible sound.
    /// See: PWG 5100.13-2023 Section 6.8.4
    /// </summary>
    public static readonly IdentifyAction Sound = new("sound");

    /// <summary>
    /// The Printer speaks an audible message.
    /// See: PWG 5100.13-2023 Section 6.8.4
    /// </summary>
    public static readonly IdentifyAction Speak = new("speak");

    public override string ToString() => Value;
    public static implicit operator string(IdentifyAction value) => value.Value;
    public static implicit operator IdentifyAction(string value) => new(value);
}
