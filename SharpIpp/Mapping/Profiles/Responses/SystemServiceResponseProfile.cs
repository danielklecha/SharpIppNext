using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class SystemServiceResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<DeallocatePrinterResourcesResponse>(mapper);
        AddMap<DeletePrinterResponse>(mapper);
        AddMap<GetPrintersResponse>(mapper);
        AddMap<GetPrinterResourcesResponse>(mapper);
        AddMap<ShutdownOnePrinterResponse>(mapper);
        AddMap<StartupOnePrinterResponse>(mapper);
        AddMap<DisableAllPrintersResponse>(mapper);
        AddMap<EnableAllPrintersResponse>(mapper);
        AddMap<PauseAllPrintersResponse>(mapper);
        AddMap<PauseAllPrintersAfterCurrentJobResponse>(mapper);
        AddMap<RestartSystemResponse>(mapper);
        AddMap<ResumeAllPrintersResponse>(mapper);
        AddMap<SetSystemAttributesResponse>(mapper);
        AddMap<ShutdownAllPrintersResponse>(mapper);
        AddMap<StartupAllPrintersResponse>(mapper);
        AddMap<CancelResourceResponse>(mapper);
        AddMap<CreateResourceResponse>(mapper);
        AddMap<InstallResourceResponse>(mapper);
        AddMap<SendResourceDataResponse>(mapper);
        AddMap<SetResourceAttributesResponse>(mapper);
        AddMap<CancelSubscriptionResponse>(mapper);
        AddMap<GetNotificationsResponse>(mapper);
        AddMap<RenewSubscriptionResponse>(mapper);
        AddMap<RestartOnePrinterResponse>(mapper);

        // Custom Subscription mappings
        mapper.CreateMap<IppResponseMessage, CreateResourceSubscriptionsResponse>((src, map) =>
        {
            var dst = new CreateResourceSubscriptionsResponse
            {
                SubscriptionsAttributes = src.SubscriptionAttributes.Any()
                    ? map.Map<SubscriptionDescriptionAttributes[]>(src.SubscriptionAttributes)
                    : null
            };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CreateResourceSubscriptionsResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.SubscriptionsAttributes != null)
            {
                dst.SubscriptionAttributes.AddRange(map.Map<List<List<IppAttribute>>>(src.SubscriptionsAttributes));
            }
            return dst;
        });

        mapper.CreateMap<IppResponseMessage, CreateSystemSubscriptionsResponse>((src, map) =>
        {
            var dst = new CreateSystemSubscriptionsResponse
            {
                SubscriptionsAttributes = src.SubscriptionAttributes.Any()
                    ? map.Map<SubscriptionDescriptionAttributes[]>(src.SubscriptionAttributes)
                    : null
            };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CreateSystemSubscriptionsResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.SubscriptionsAttributes != null)
            {
                dst.SubscriptionAttributes.AddRange(map.Map<List<List<IppAttribute>>>(src.SubscriptionsAttributes));
            }
            return dst;
        });

        mapper.CreateMap<IppResponseMessage, GetSubscriptionAttributesResponse>((src, map) =>
        {
            var dst = new GetSubscriptionAttributesResponse();
            if (src.SubscriptionAttributes.Count > 0)
                dst.SubscriptionAttributes = map.Map<SubscriptionDescriptionAttributes>(src.SubscriptionAttributes.SelectMany(x => x).ToIppDictionary());
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetSubscriptionAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.SubscriptionAttributes != null)
            {
                var attrs = new List<IppAttribute>();
                attrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.SubscriptionAttributes).Values.SelectMany(x => x));
                dst.SubscriptionAttributes.Add(attrs);
            }
            return dst;
        });

        mapper.CreateMap<IppResponseMessage, GetSubscriptionsResponse>((src, map) =>
        {
            var dst = new GetSubscriptionsResponse();
            if (src.SubscriptionAttributes.Count > 0)
                dst.SubscriptionsAttributes = map.Map<SubscriptionDescriptionAttributes[]>(src.SubscriptionAttributes);
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetSubscriptionsResponse, IppResponseMessage>((src, map) =>
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

    private static void AddMap<T>(IMapperConstructor mapper)
        where T : IIppResponse, new()
    {
        mapper.CreateMap<IppResponseMessage, T>((src, map) =>
        {
            var dst = new T();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<T, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
