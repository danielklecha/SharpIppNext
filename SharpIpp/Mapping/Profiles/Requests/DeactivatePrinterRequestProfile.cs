using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class DeactivatePrinterRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<DeactivatePrinterRequest, DeactivatePrinterOperationAttributes>(mapper, IppOperation.DeactivatePrinter);
    }
}
