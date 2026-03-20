namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-release-action</c> keyword values.
/// See: PWG 5100.11-2024 Section 5.2.4
/// </summary>
public readonly record struct JobReleaseAction(string Value)
{
    public static readonly JobReleaseAction None = new("none");
    public static readonly JobReleaseAction ButtonPress = new("button-press");
    public static readonly JobReleaseAction JobPassword = new("job-password");
    public static readonly JobReleaseAction OwnerAuthorized = new("owner-authorized");

    public override string ToString() => Value;
    public static implicit operator string(JobReleaseAction value) => value.Value;
    public static explicit operator JobReleaseAction(string value) => new(value);
}
