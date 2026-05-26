using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SharpIpp.Tests.Unit.Mapping;

[ExcludeFromCodeCoverage]
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
