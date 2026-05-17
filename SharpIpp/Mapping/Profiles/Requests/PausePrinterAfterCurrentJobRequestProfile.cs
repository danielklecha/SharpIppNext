using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class PausePrinterAfterCurrentJobRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<PausePrinterAfterCurrentJobRequest, PausePrinterAfterCurrentJobOperationAttributes>(mapper, IppOperation.PausePrinterAfterCurrentJob);
    }
}
