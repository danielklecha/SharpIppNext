namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies which sides of the Media Sheets to coat or laminate.
/// See: PWG 5100.1-2022 Section 5.2.3.1, 5.2.7.1
/// </summary>
public readonly record struct CoatingSides(string Value)
{
    public static readonly CoatingSides Front = new("front");
    public static readonly CoatingSides Back = new("back");
    public static readonly CoatingSides Both = new("both");

    public override string ToString() => Value;
    public static implicit operator string(CoatingSides bin) => bin.Value;
    public static explicit operator CoatingSides(string value) => new(value);
}
