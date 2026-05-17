using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class HoldNewJobsRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<HoldNewJobsRequest, HoldNewJobsOperationAttributes>(mapper, IppOperation.HoldNewJobs);
    }
}
