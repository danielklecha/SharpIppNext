namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the job-constraints-supported collection attribute.
/// Each element describes a constraint between two or more Job Template attributes.
/// See: PWG 5100.13-2023 Section 6.5.5
/// </summary>
public class JobConstraintsSupported : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// resolver-name — name of the resolver that resolves this constraint (name).
    /// See: PWG 5100.13-2023 Section 6.5.5
    /// </summary>
    public string? ResolverName { get; set; }
}
