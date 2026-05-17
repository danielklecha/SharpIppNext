using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class StartupPrinterRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<StartupPrinterRequest, StartupPrinterOperationAttributes>(mapper, IppOperation.StartupPrinter);
    }
}
