namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>multiple-operation-time-out-action</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.5.19
/// </summary>
public readonly record struct MultipleOperationTimeOutAction(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Abort the job.
    /// See: PWG 5100.13-2023 Section 6.5.19
    /// </summary>
    public static readonly MultipleOperationTimeOutAction AbortJob = new("abort-job");

    /// <summary>
    /// Hold the job.
    /// See: PWG 5100.13-2023 Section 6.5.19
    /// </summary>
    public static readonly MultipleOperationTimeOutAction HoldJob = new("hold-job");

    /// <summary>
    /// Process the job.
    /// See: PWG 5100.13-2023 Section 6.5.19
    /// </summary>
    public static readonly MultipleOperationTimeOutAction ProcessJob = new("process-job");

    public override string ToString() => Value;
    public static implicit operator string(MultipleOperationTimeOutAction value) => value.Value;
    public static implicit operator MultipleOperationTimeOutAction(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
