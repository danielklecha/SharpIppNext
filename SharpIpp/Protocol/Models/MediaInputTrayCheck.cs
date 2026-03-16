namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>media-input-tray-check</c> value.
/// See: PWG 5100.3-2023 Section 5.2.13
/// 
/// Note: This attribute uses tray keyword/name values (MSN2 tray keywords).
/// No PPX-defined fixed keyword set is provided.
/// </summary>
public readonly record struct MediaInputTrayCheck(string Value)
{
    public override string ToString() => Value;
    public static implicit operator string(MediaInputTrayCheck bin) => bin.Value;
    public static explicit operator MediaInputTrayCheck(string value) => new(value);
}
