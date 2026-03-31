namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies when a job should be held.
/// </summary>
public readonly record struct JobHoldUntil(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// 'no-hold': immediately, if there are not other reasons to hold the job
    /// </summary>
    public static readonly JobHoldUntil NoHold = new("no-hold");

    /// <summary>
    /// 'indefinite':  - the job is held indefinitely, until a client performs a Release-Job
    /// </summary>
    public static readonly JobHoldUntil Indefinite = new("indefinite");

    /// <summary>
    /// 'day-time': during the day
    /// </summary>
    public static readonly JobHoldUntil DayTime = new("day-time");

    /// <summary>
    /// 'evening': evening
    /// </summary>
    public static readonly JobHoldUntil Evening = new("evening");

    /// <summary>
    /// 'night': night
    /// </summary>
    public static readonly JobHoldUntil Night = new("night");

    /// <summary>
    /// 'weekend': weekend
    /// </summary>
    public static readonly JobHoldUntil Weekend = new("weekend");

    /// <summary>
    /// 'second-shift': second-shift (after close of business)
    /// </summary>
    public static readonly JobHoldUntil SecondShift = new("second-shift");

    /// <summary>
    /// 'third-shift': third-shift (after midnight)
    /// </summary>
    public static readonly JobHoldUntil ThirdShift = new("third-shift");

    /// <summary>
    /// 'no-delay-output': do not delay output.
    /// </summary>
    public static readonly JobHoldUntil NoDelayOutput = new("no-delay-output");

    /// <summary>
    /// 'end-of-day': retain until end of the current day.
    /// </summary>
    public static readonly JobHoldUntil EndOfDay = new("end-of-day");

    /// <summary>
    /// 'end-of-week': retain until end of the current week.
    /// </summary>
    public static readonly JobHoldUntil EndOfWeek = new("end-of-week");

    /// <summary>
    /// 'end-of-month': retain until end of the current month.
    /// </summary>
    public static readonly JobHoldUntil EndOfMonth = new("end-of-month");

    /// <summary>
    /// 'none': do not retain the Job.
    /// </summary>
    public static readonly JobHoldUntil None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(JobHoldUntil bin) => bin.Value;
    public static explicit operator JobHoldUntil(string value) => new(value);
}
