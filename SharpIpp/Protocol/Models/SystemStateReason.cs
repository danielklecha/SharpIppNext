namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the reason for the current state of a System object.
/// See: PWG 5100.22-2025 Section 7.3.30
/// </summary>
public readonly record struct SystemStateReason(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly SystemStateReason None = new("none");
    public static readonly SystemStateReason Other = new("other");
    public static readonly SystemStateReason Stopping = new("stopping");

    public override string ToString() => Value;
    public static implicit operator string(SystemStateReason value) => value.Value;
    public static explicit operator SystemStateReason(string value) => new(value);
}
