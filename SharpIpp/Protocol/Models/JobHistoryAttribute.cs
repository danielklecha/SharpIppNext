namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>job-history-attributes-configured</code> and <code>job-history-attributes-supported</code>.
/// See: RFC 8011 Section 5.4.31
/// </summary>
public readonly record struct JobHistoryAttribute(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly JobHistoryAttribute JobId = new("job-id");
    public static readonly JobHistoryAttribute JobName = new("job-name");
    public static readonly JobHistoryAttribute JobState = new("job-state");
    public static readonly JobHistoryAttribute JobStateReasons = new("job-state-reasons");
    public static readonly JobHistoryAttribute JobUri = new("job-uri");

    public override string ToString() => Value;
    public static implicit operator string(JobHistoryAttribute value) => value.Value;
    public static explicit operator JobHistoryAttribute(string value) => new(value);
}
