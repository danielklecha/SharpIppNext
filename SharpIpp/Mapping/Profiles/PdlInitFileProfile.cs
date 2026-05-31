using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PdlInitFileProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PdlInitFile>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PdlInitFile>();

            return new PdlInitFile
            {
                PdlInitFileLocation = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.PdlInitFileLocation),
                PdlInitFileName = map.MapFromDicNullable<string?>(src, IppAttributeNames.PdlInitFileName)
            };
        });

        mapper.CreateMap<PdlInitFile, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.PdlInitFile, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.PdlInitFileLocation != null)
                attributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.PdlInitFileLocation, src.PdlInitFileLocation.ToString()));
            if (src.PdlInitFileName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.PdlInitFileName, src.PdlInitFileName));
            return attributes;
        });
    }
}
