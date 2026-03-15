namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of coating.
/// See: PWG 5100.1-2022 Section 5.2.3.2
/// </summary>
public enum CoatingType
{
    Archival,
    ArchivalGlossy,
    ArchivalMatte,
    ArchivalSemiGloss,
    Glossy,
    HighGloss,
    Matte,
    SemiGloss,
    Translucent,
    WaterResistant
}
