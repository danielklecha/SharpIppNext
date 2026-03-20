namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents an extended value tag (0x7f) where the first four bytes of the value encode the extended tag id.
/// </summary>
public sealed record ExtendedValue(int ExtendedTag, byte[] Raw);
