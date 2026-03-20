namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents an unknown or vendor-specific value tag. Retains the raw bytes for round-tripping.
/// </summary>
public sealed record UnknownValue(Tag Tag, byte[] Raw);
