using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class DestinationStatusProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationStatus>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DestinationStatus>();

            return new DestinationStatus
            {
                DestinationUri = map.MapFromDicNullable<Uri?>(src, "destination-uri"),
                ImagesCompleted = map.MapFromDicNullable<int?>(src, "images-completed"),
                TransmissionStatus = map.MapFromDicNullable<TransmissionStatus?>(src, "transmission-status")
            };
        });

        mapper.CreateMap<DestinationStatus, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.DestinationStatuses, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUri.ToString()));
            if (src.ImagesCompleted.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "images-completed", src.ImagesCompleted.Value));
            if (src.TransmissionStatus.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, "transmission-status", (int)src.TransmissionStatus.Value));
            return attributes;
        });
    }
}
