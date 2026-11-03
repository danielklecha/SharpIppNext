using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;

namespace SharpIpp.Tests.Unit.Mapping;

public abstract class MapperTestBase
{
    protected readonly IMapper _mapper;

    protected MapperTestBase()
    {
        _mapper = SimpleMapper.Instance;
    }
}
