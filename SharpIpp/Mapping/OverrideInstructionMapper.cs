using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

[MapperConfiguration(1, typeof(OverrideInstruction))]
internal static class OverrideInstructionMapper
{
    public static void Configure(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OverrideInstruction>(OverrideInstructionMappingHelper.MapDictionaryToOverrideInstruction);
        mapper.CreateMap<Dictionary<string, IppAttribute[]>, OverrideInstruction>(OverrideInstructionMappingHelper.MapConcreteDictionaryToOverrideInstruction);
        mapper.CreateMap<OverrideInstruction, IEnumerable<IppAttribute>>(OverrideInstructionMappingHelper.MapOverrideInstructionToIppAttributes);
        mapper.CreateMap<OverrideInstruction, List<IppAttribute>>(OverrideInstructionMappingHelper.MapOverrideInstructionToList);
        mapper.CreateMap<OverrideInstruction, IDictionary<string, IppAttribute[]>>(OverrideInstructionMappingHelper.MapOverrideInstructionToDictionary);
        mapper.CreateMap<OverrideInstruction, Dictionary<string, IppAttribute[]>>(OverrideInstructionMappingHelper.MapOverrideInstructionToConcreteDictionary);
    }
}
