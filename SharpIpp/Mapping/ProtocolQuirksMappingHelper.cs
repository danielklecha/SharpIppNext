using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

internal static class ProtocolQuirksMappingHelper
{
    public static bool RangeToBool(Range _, IMapperApplier __) => true;
    public static IppValue<bool> RangeToIppValueBool(Range _, IMapperApplier __) => new(true);
    public static bool NoValueToBool(NoValue _, IMapperApplier __) => false;
}

