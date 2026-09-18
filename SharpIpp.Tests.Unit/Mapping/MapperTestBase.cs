using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Mapping;

[ExcludeFromCodeCoverage]
public abstract class MapperTestBase
{
    protected readonly IMapper _mapper;

    protected MapperTestBase()
    {
        var mapper = new SimpleMapper();
        mapper.RegisterGeneratedProfiles();
        _mapper = mapper;
    }
}
