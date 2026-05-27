namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the pull methods supported for notifications.
/// See: RFC 3995 Section 10
/// </summary>
public readonly record struct NotifyPullMethod(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The IPP Get-Notifications pull method.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyPullMethod IppGet = new("ippget");

    public override string ToString() => Value;
    public static implicit operator string(NotifyPullMethod value) => value.Value;
    public static implicit operator NotifyPullMethod(string value) => new(value);
}
