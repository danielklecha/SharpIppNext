using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
///     PWG 5100.3-2023 Section 5.2.16
/// </summary>
public enum PageDelivery
{
    SameOrderFaceUp,
    SameOrderFaceDown,
    ReverseOrderFaceUp,
    ReverseOrderFaceDown,
    SystemSpecified
}
