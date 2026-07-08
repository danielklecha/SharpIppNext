namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>job-history-attributes-configured</code> and <code>job-history-attributes-supported</code>.
/// See: RFC 8011 Section 5.4.31
/// </summary>
public readonly record struct JobHistoryAttribute(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>The job-id attribute. See: RFC 8011 Section 5.4.31</summary>
    public static readonly JobHistoryAttribute JobId = new("job-id");
    /// <summary>The job-name attribute. See: RFC 8011 Section 5.4.31</summary>
    public static readonly JobHistoryAttribute JobName = new("job-name");
    /// <summary>The job-state attribute. See: RFC 8011 Section 5.4.31</summary>
    public static readonly JobHistoryAttribute JobState = new("job-state");
    /// <summary>The job-state-reasons attribute. See: RFC 8011 Section 5.4.31</summary>
    public static readonly JobHistoryAttribute JobStateReasons = new("job-state-reasons");
    /// <summary>The job-uri attribute. See: RFC 8011 Section 5.4.31</summary>
    public static readonly JobHistoryAttribute JobUri = new("job-uri");

    public override string ToString() => Value;
    public static implicit operator string(JobHistoryAttribute value) => value.Value;
    public static implicit operator JobHistoryAttribute(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
