namespace SharpIpp.Protocol.Models;

/// <summary>
///     Specifies the type of laminating.
///     See: PWG 5100.1-2022 Section 5.2.7.2
/// </summary>
public enum LaminatingType
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
