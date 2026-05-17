namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the reason for the current state of a Resource.
/// See: PWG 5100.22-2025 Section 7.9.13
/// </summary>
public readonly record struct ResourceStateReason(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// A cancel request has been received for the Resource.
    /// See: PWG 5100.22-2025 Section 7.9.13
    /// </summary>
    public static readonly ResourceStateReason CancelRequested = new("cancel-requested");

    /// <summary>
    /// An install request has been received for the Resource.
    /// See: PWG 5100.22-2025 Section 7.9.13
    /// </summary>
    public static readonly ResourceStateReason InstallRequested = new("install-requested");

    /// <summary>
    /// The Resource is being received by the System.
    /// See: PWG 5100.22-2025 Section 7.9.13
    /// </summary>
    public static readonly ResourceStateReason ResourceIncoming = new("resource-incoming");

    /// <summary>
    /// No resource state reasons apply.
    /// See: PWG 5100.22-2025 Section 7.9.13
    /// </summary>
    public static readonly ResourceStateReason None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(ResourceStateReason r) => r.Value;
    public static explicit operator ResourceStateReason(string value) => new(value);
}
