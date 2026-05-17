using SharpIpp.Models.Responses;
using SharpIpp.Protocol;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class ShutdownPrinterResponseProfile : AbstractResponseProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<ShutdownPrinterResponse>(mapper);
    }
}
