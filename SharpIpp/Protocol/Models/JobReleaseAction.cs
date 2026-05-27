namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-release-action</c> keyword values.
/// See: PWG 5100.11-2024 Section 5.2.4
/// </summary>
public readonly record struct JobReleaseAction(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// No release action is required; the job is released immediately.
    /// See: PWG 5100.11-2024 Section 5.2.4
    /// </summary>
    public static readonly JobReleaseAction None = new("none");

    /// <summary>
    /// The job is released when the user presses a button on the device.
    /// See: PWG 5100.11-2024 Section 5.2.4
    /// </summary>
    public static readonly JobReleaseAction ButtonPress = new("button-press");

    /// <summary>
    /// The job is released when the user enters the correct job password.
    /// See: PWG 5100.11-2024 Section 5.2.4
    /// </summary>
    public static readonly JobReleaseAction JobPassword = new("job-password");

    /// <summary>
    /// The job is released when the job owner authorizes it.
    /// See: PWG 5100.11-2024 Section 5.2.4
    /// </summary>
    public static readonly JobReleaseAction OwnerAuthorized = new("owner-authorized");

    public override string ToString() => Value;
    public static implicit operator string(JobReleaseAction value) => value.Value;
    public static implicit operator JobReleaseAction(string value) => new(value);
}
