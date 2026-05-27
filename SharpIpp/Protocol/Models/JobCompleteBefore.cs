namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-complete-before</c> Job Template attribute keyword values.
/// See: PWG 5100.3-2023 Section 5.2.7
/// </summary>
public readonly record struct JobCompleteBefore(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>'day-time': Complete the job before the beginning of the day (typically first shift or 8am).</summary>
    public static readonly JobCompleteBefore DayTime = new("day-time");
    /// <summary>'evening': Complete the Job before the beginning of the evening (typically 6pm).</summary>
    public static readonly JobCompleteBefore Evening = new("evening");
    /// <summary>'night': Complete the Job before sundown (typically 9pm).</summary>
    public static readonly JobCompleteBefore Night = new("night");
    /// <summary>'none': Complete the Job at any time (no specific request).</summary>
    public static readonly JobCompleteBefore None = new("none");
    /// <summary>'second-shift': Complete the Job before the second shift (typically 4pm).</summary>
    public static readonly JobCompleteBefore SecondShift = new("second-shift");
    /// <summary>'third-shift': Complete the Job before the third shift (typically 12am).</summary>
    public static readonly JobCompleteBefore ThirdShift = new("third-shift");
    /// <summary>'weekend': Complete the Job before Saturday at 12am.</summary>
    public static readonly JobCompleteBefore Weekend = new("weekend");

    public override string ToString() => Value;
    public static implicit operator string(JobCompleteBefore v) => v.Value;
    public static implicit operator JobCompleteBefore(string value) => new(value);
}
