namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-error-action</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.2.6
/// </summary>
public readonly record struct JobErrorAction(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobErrorAction AbortJob = new("abort-job");
    public static readonly JobErrorAction CancelJob = new("cancel-job");
    public static readonly JobErrorAction ContinueJob = new("continue-job");
    public static readonly JobErrorAction SuspendJob = new("suspend-job");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorAction value) => value.Value;
    public static explicit operator JobErrorAction(string value) => new(value);
}
