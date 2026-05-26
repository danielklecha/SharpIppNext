namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the job-presets-supported collection attribute.
/// Each element describes a named preset of Job Template attribute values.
/// See: PWG 5100.13-2023 Section 6.5.8
/// </summary>
public class JobPresetsSupported : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// preset-name — human-readable name of the preset (name).
    /// See: PWG 5100.13-2023 Section 6.5.8
    /// </summary>
    public string? PresetName { get; set; }
}
