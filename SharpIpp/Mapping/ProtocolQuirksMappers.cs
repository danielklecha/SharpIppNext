using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

/// <summary>
/// Configures mappings for protocol-specific quirks and compatibility workarounds
/// (e.g., handling non-standard printer attribute encodings and out-of-band values).
/// </summary>
[MapperConfiguration(1)]
internal static class ProtocolQuirksMappers
{
    public static void Configure(IMapperConstructor mapper)
    {
        // Workaround for printers returning RangeOfInteger for boolean page-ranges-supported
        mapper.CreateIppMap<Range, bool>((_, _) => true);
        mapper.CreateIppMap<Range, IppValue<bool>>((_, _) => new IppValue<bool>(true));

        // Fallback for non-IppValue bool properties receiving Tag.NoValue
        mapper.CreateIppMap<NoValue, bool>((_, _) => false);
    }
}