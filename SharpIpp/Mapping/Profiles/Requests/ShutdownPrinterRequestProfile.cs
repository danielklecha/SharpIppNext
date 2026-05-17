using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ShutdownPrinterRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<ShutdownPrinterRequest, ShutdownPrinterOperationAttributes>(mapper, IppOperation.ShutdownPrinter);
    }
}
