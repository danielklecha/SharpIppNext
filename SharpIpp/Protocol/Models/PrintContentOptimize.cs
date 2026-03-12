namespace SharpIpp.Protocol.Models;

/// <summary>
///     Specifies the type of content optimization.
///     See: PWG 5100.7-2023 Section 6.9.58
/// </summary>
public enum PrintContentOptimize
{
    Graphic,
    Photo,
    Text,
    TextAndGraphic
}
