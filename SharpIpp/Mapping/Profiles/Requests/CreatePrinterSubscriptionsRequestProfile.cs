using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CreatePrinterSubscriptionsRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<CreatePrinterSubscriptionsRequest, CreatePrinterSubscriptionsOperationAttributes>(mapper, IppOperation.CreatePrinterSubscriptions);
    }
}
