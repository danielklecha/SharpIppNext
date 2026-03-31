using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SystemServiceRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        AddMap<DeallocatePrinterResourcesRequest>(mapper, IppOperation.DeallocatePrinterResources);
        AddMap<DeletePrinterRequest>(mapper, IppOperation.DeletePrinter);
        AddMap<GetPrintersRequest>(mapper, IppOperation.GetPrinters);
        AddMap<GetPrinterResourcesRequest>(mapper, IppOperation.GetPrinterResources);
        AddMap<ShutdownOnePrinterRequest>(mapper, IppOperation.ShutdownOnePrinter);
        AddMap<StartupOnePrinterRequest>(mapper, IppOperation.StartupOnePrinter);
        AddMap<RestartOnePrinterRequest>(mapper, IppOperation.RestartOnePrinter);
        AddMap<CreateResourceSubscriptionsRequest>(mapper, IppOperation.CreateResourceSubscriptions);
        AddMap<CreateSystemSubscriptionsRequest>(mapper, IppOperation.CreateSystemSubscriptions);
        AddMap<CancelSubscriptionRequest>(mapper, IppOperation.CancelSubscription);
        AddMap<GetNotificationsRequest>(mapper, IppOperation.GetNotifications);
        AddMap<GetSubscriptionAttributesRequest>(mapper, IppOperation.GetSubscriptionAttributes);
        AddMap<GetSubscriptionsRequest>(mapper, IppOperation.GetSubscriptions);
        AddMap<RenewSubscriptionRequest>(mapper, IppOperation.RenewSubscription);
        AddMap<DisableAllPrintersRequest>(mapper, IppOperation.DisableAllPrinters);
        AddMap<EnableAllPrintersRequest>(mapper, IppOperation.EnableAllPrinters);
        AddMap<PauseAllPrintersRequest>(mapper, IppOperation.PauseAllPrinters);
        AddMap<PauseAllPrintersAfterCurrentJobRequest>(mapper, IppOperation.PauseAllPrintersAfterCurrentJob);
        AddMap<RestartSystemRequest>(mapper, IppOperation.RestartSystem);
        AddMap<ResumeAllPrintersRequest>(mapper, IppOperation.ResumeAllPrinters);
        AddMap<SetSystemAttributesRequest>(mapper, IppOperation.SetSystemAttributes);
        AddMap<ShutdownAllPrintersRequest>(mapper, IppOperation.ShutdownAllPrinters);
        AddMap<StartupAllPrintersRequest>(mapper, IppOperation.StartupAllPrinters);

        AddMap<CancelResourceRequest>(mapper, IppOperation.CancelResource);
        AddMap<CreateResourceRequest>(mapper, IppOperation.CreateResource);
        AddMap<InstallResourceRequest>(mapper, IppOperation.InstallResource);
        AddMap<SendResourceDataRequest>(mapper, IppOperation.SendResourceData);
        AddMap<SetResourceAttributesRequest>(mapper, IppOperation.SetResourceAttributes);
    }

    private static void AddMap<T>(IMapperConstructor mapper, IppOperation operation)
        where T : IppRequest<SystemOperationAttributes>, IIppSystemRequest, new()
    {
        mapper.CreateMap<T, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = operation };
            map.Map<IIppSystemRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<SystemOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, T>((src, map) =>
        {
            var dst = new T();
            map.Map<IIppRequestMessage, IIppSystemRequest>(src, dst);
            var operationAttributes = map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            dst.OperationAttributes = operationAttributes;
            return dst;
        });
    }
}
