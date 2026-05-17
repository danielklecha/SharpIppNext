using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetPrinterSupportedValuesRequestProfile : AbstractPrinterRequestProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddPrinterMap<GetPrinterSupportedValuesRequest, GetPrinterSupportedValuesOperationAttributes>(mapper, IppOperation.GetPrinterSupportedValues);
    }
}
