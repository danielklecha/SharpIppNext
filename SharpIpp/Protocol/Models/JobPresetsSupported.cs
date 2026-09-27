using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the job-presets-supported collection attribute.
/// Each element describes a named preset of Job Template attribute values.
/// See: PWG 5100.13-2023 Section 6.5.8
/// </summary>
[IppAttribute(IppAttributeNames.JobPresetsSupported)]
public class JobPresetsSupported : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// preset-name — human-readable name of the preset (name).
    /// See: PWG 5100.13-2023 Section 6.5.8
    /// </summary>
    public IppValue<string>? PresetName { get; set; }
}
