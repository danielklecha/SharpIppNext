using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class CreateJobSubscriptionsResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, CreateJobSubscriptionsResponse>((src, map) =>
        {
            var dst = new CreateJobSubscriptionsResponse
            {
                SubscriptionsAttributes = src.SubscriptionAttributes.Any()
                    ? map.Map<SubscriptionDescriptionAttributes[]>(src.SubscriptionAttributes)
                    : null
            };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CreateJobSubscriptionsResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.SubscriptionsAttributes != null)
            {
                dst.SubscriptionAttributes.AddRange(map.Map<List<List<IppAttribute>>>(src.SubscriptionsAttributes));
            }
            return dst;
        });
    }
}
