using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ResumeJobRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<ResumeJobRequest, ResumeJobOperationAttributes>(mapper, IppOperation.ResumeJob);
    }
}
