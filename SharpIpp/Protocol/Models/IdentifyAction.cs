namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>identify-actions</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.8.4
/// </summary>
public readonly record struct IdentifyAction(string Value)
{
    public static readonly IdentifyAction Display = new("display");
    public static readonly IdentifyAction Flash = new("flash");
    public static readonly IdentifyAction Sound = new("sound");
    public static readonly IdentifyAction Speak = new("speak");

    public override string ToString() => Value;
    public static implicit operator string(IdentifyAction value) => value.Value;
    public static explicit operator IdentifyAction(string value) => new(value);
}
