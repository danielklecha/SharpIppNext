using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public sealed class IppRequestValidationContext
{
    public string? Source { get; set; }

    public IReadOnlyCollection<OverrideSupported>? OverridesSupported { get; set; }
}
