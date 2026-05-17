using SharpIpp.Models.Responses;
using SharpIpp.Protocol;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class PausePrinterAfterCurrentJobResponseProfile : AbstractResponseProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<PausePrinterAfterCurrentJobResponse>(mapper);
    }
}
