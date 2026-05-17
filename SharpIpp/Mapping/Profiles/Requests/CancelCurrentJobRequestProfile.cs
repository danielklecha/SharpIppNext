using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CancelCurrentJobRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<CancelCurrentJobRequest, CancelCurrentJobOperationAttributes>(mapper, IppOperation.CancelCurrentJob);
    }
}
