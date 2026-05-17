using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ScheduleJobAfterRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<ScheduleJobAfterRequest, ScheduleJobAfterOperationAttributes>(mapper, IppOperation.ScheduleJobAfter);
    }
}
