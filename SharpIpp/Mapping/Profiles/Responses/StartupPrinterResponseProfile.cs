using SharpIpp.Models.Responses;
using SharpIpp.Protocol;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class StartupPrinterResponseProfile : AbstractResponseProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<StartupPrinterResponse>(mapper);
    }
}
