using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class SaveInfoProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SaveInfo>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<SaveInfo>();

            return new SaveInfo
            {
                SaveLocation = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SaveLocation),
                SaveName = map.MapFromDicNullable<string?>(src, IppAttributeNames.SaveName),
                SaveDocumentFormat = map.MapFromDicNullable<string?>(src, IppAttributeNames.SaveDocumentFormat)
            };
        });

        mapper.CreateMap<SaveInfo, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.SaveInfo, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.SaveLocation != null)
                attributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.SaveLocation, src.SaveLocation.ToString()));
            if (src.SaveName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.SaveName, src.SaveName));
            if (src.SaveDocumentFormat != null)
                attributes.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.SaveDocumentFormat, src.SaveDocumentFormat));
            return attributes;
        });
    }
}
