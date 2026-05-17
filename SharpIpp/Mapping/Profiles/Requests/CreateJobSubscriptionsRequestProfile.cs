using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CreateJobSubscriptionsRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<CreateJobSubscriptionsRequest, CreateJobSubscriptionsOperationAttributes>(mapper, IppOperation.CreateJobSubscriptions);
    }
}
