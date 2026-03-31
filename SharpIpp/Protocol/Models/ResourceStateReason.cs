namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the reason for the current state of a Resource.
/// See: PWG 5100.22-2025 Section 7.9.13
/// </summary>
public readonly record struct ResourceStateReason(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly ResourceStateReason CancelRequested = new("cancel-requested");
    public static readonly ResourceStateReason InstallRequested = new("install-requested");
    public static readonly ResourceStateReason ResourceIncoming = new("resource-incoming");
    public static readonly ResourceStateReason None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(ResourceStateReason r) => r.Value;
    public static explicit operator ResourceStateReason(string value) => new(value);
}
