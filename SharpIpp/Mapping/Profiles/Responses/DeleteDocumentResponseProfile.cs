using SharpIpp.Models.Responses;
using SharpIpp.Protocol;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class DeleteDocumentResponseProfile : AbstractResponseProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<DeleteDocumentResponse>(mapper);
    }
}
