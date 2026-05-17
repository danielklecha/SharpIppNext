using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ReleaseHeldNewJobsRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<ReleaseHeldNewJobsRequest, ReleaseHeldNewJobsOperationAttributes>(mapper, IppOperation.ReleaseHeldNewJobs);
    }
}
