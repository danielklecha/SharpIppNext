namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-error-action</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.2.6
/// </summary>
public readonly record struct JobErrorAction(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer aborts the job when an error is encountered.
    /// See: PWG 5100.13-2023 Section 6.2.6
    /// </summary>
    public static readonly JobErrorAction AbortJob = new("abort-job");

    /// <summary>
    /// The Printer cancels the job when an error is encountered.
    /// See: PWG 5100.13-2023 Section 6.2.6
    /// </summary>
    public static readonly JobErrorAction CancelJob = new("cancel-job");

    /// <summary>
    /// The Printer continues processing the job when an error is encountered.
    /// See: PWG 5100.13-2023 Section 6.2.6
    /// </summary>
    public static readonly JobErrorAction ContinueJob = new("continue-job");

    /// <summary>
    /// The Printer suspends the job when an error is encountered.
    /// See: PWG 5100.13-2023 Section 6.2.6
    /// </summary>
    public static readonly JobErrorAction SuspendJob = new("suspend-job");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorAction value) => value.Value;
    public static explicit operator JobErrorAction(string value) => new(value);
}
