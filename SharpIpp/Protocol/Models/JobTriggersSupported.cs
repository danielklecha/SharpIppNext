namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the job-triggers-supported collection attribute.
/// Each element describes a trigger that can cause a job constraint to be evaluated.
/// See: PWG 5100.13-2023 Section 6.5.10
/// </summary>
public class JobTriggersSupported : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// trigger-name — name of the trigger (name).
    /// See: PWG 5100.13-2023 Section 6.5.10
    /// </summary>
    public string? TriggerName { get; set; }
}
