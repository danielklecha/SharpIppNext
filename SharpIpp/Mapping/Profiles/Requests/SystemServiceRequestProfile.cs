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
        AddMap<DeallocatePrinterResourcesRequest, SystemOperationAttributes>(mapper, IppOperation.DeallocatePrinterResources);
        AddMap<DeletePrinterRequest, SystemOperationAttributes>(mapper, IppOperation.DeletePrinter);
        AddMap<GetPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.GetPrinters);
        AddMap<GetPrinterResourcesRequest, SystemOperationAttributes>(mapper, IppOperation.GetPrinterResources);
        AddMap<ShutdownOnePrinterRequest, SystemOperationAttributes>(mapper, IppOperation.ShutdownOnePrinter);
        AddMap<StartupOnePrinterRequest, SystemOperationAttributes>(mapper, IppOperation.StartupOnePrinter);
        AddMap<RestartOnePrinterRequest, SystemOperationAttributes>(mapper, IppOperation.RestartOnePrinter);
        AddMap<CreateResourceSubscriptionsRequest, SystemOperationAttributes>(mapper, IppOperation.CreateResourceSubscriptions);
        AddMap<CreateSystemSubscriptionsRequest, SystemOperationAttributes>(mapper, IppOperation.CreateSystemSubscriptions);
        AddMap<CancelSubscriptionRequest, SystemOperationAttributes>(mapper, IppOperation.CancelSubscription);
        AddMap<GetNotificationsRequest, SystemOperationAttributes>(mapper, IppOperation.GetNotifications);
        AddMap<GetSubscriptionAttributesRequest, SystemOperationAttributes>(mapper, IppOperation.GetSubscriptionAttributes);
        AddMap<GetSubscriptionsRequest, SystemOperationAttributes>(mapper, IppOperation.GetSubscriptions);
        AddMap<RenewSubscriptionRequest, SystemOperationAttributes>(mapper, IppOperation.RenewSubscription);
        AddMap<DisableAllPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.DisableAllPrinters);
        AddMap<EnableAllPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.EnableAllPrinters);
        AddMap<PauseAllPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.PauseAllPrinters);
        AddMap<PauseAllPrintersAfterCurrentJobRequest, SystemOperationAttributes>(mapper, IppOperation.PauseAllPrintersAfterCurrentJob);
        AddMap<RestartSystemRequest, SystemOperationAttributes>(mapper, IppOperation.RestartSystem);
        AddMap<ResumeAllPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.ResumeAllPrinters);
        AddMap<SetSystemAttributesRequest, SystemOperationAttributes>(mapper, IppOperation.SetSystemAttributes);
        AddMap<ShutdownAllPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.ShutdownAllPrinters);
        AddMap<StartupAllPrintersRequest, SystemOperationAttributes>(mapper, IppOperation.StartupAllPrinters);

        AddMap<CancelResourceRequest, CancelResourceOperationAttributes>(mapper, IppOperation.CancelResource);
        AddMap<CreateResourceRequest, CreateResourceOperationAttributes>(mapper, IppOperation.CreateResource);
        AddMap<InstallResourceRequest, InstallResourceOperationAttributes>(mapper, IppOperation.InstallResource);
        AddMap<SendResourceDataRequest, SendResourceDataOperationAttributes>(mapper, IppOperation.SendResourceData);
        AddMap<SetResourceAttributesRequest, SetResourceAttributesOperationAttributes>(mapper, IppOperation.SetResourceAttributes);
    }

    private static void AddMap<T, TOperationAttributes>(IMapperConstructor mapper, IppOperation operation)
        where T : IppRequest<TOperationAttributes>, IIppSystemRequest, new()
        where TOperationAttributes : SystemOperationAttributes, new()
    {
        mapper.CreateMap<T, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = operation };
            map.Map<IIppSystemRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<TOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, T>((src, map) =>
        {
            var dst = new T();
            map.Map<IIppRequestMessage, IIppSystemRequest>(src, dst);
            var operationAttributes = map.Map<IDictionary<string, IppAttribute[]>, TOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            dst.OperationAttributes = operationAttributes;
            return dst;
        });
    }
}
