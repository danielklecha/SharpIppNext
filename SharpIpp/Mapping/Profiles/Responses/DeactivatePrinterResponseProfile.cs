using SharpIpp.Models.Responses;
using SharpIpp.Protocol;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class DeactivatePrinterResponseProfile : AbstractResponseProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<DeactivatePrinterResponse>(mapper);
    }
}
