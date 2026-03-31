using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

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
        AddMap<CreateResourceSubscriptionsResponse>(mapper);
        AddMap<CreateSystemSubscriptionsResponse>(mapper);
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
        AddMap<GetSubscriptionAttributesResponse>(mapper);
        AddMap<GetSubscriptionsResponse>(mapper);
        AddMap<RenewSubscriptionResponse>(mapper);
        AddMap<RestartOnePrinterResponse>(mapper);
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
