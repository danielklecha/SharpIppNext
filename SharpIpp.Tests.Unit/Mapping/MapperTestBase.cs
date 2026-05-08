using System.Reflection;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;

namespace SharpIpp.Tests.Unit.Mapping;

public abstract class MapperTestBase
{
    protected readonly IMapper _mapper;

    protected MapperTestBase()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }
}
