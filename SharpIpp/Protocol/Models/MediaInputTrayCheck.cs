namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies whether the media-input-tray-check attribute is supported.
/// See: PWG 5100.1-2022 Section 6.13
/// </summary>
public readonly record struct MediaInputTrayCheck(string Value)
{
    public static readonly MediaInputTrayCheck None = new("none");
    public static readonly MediaInputTrayCheck Trace = new("trace");
    public static readonly MediaInputTrayCheck AllowTrayCheck = new("allow-tray-check");

    public override string ToString() => Value;
    public static implicit operator string(MediaInputTrayCheck bin) => bin.Value;
    public static explicit operator MediaInputTrayCheck(string value) => new(value);
}
