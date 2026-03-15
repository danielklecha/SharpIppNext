using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// PWG 5100.3-2023 Section 5.3.5 / 5.2.1
/// </summary>
public enum CoverType
{
    NoCover,
    PrintNone,
    PrintFront,
    PrintBack,
    PrintBoth
}
