using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

// System service responses

[IppResponse(IppOperation.DeallocatePrinterResources)]
public class DeallocatePrinterResourcesResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.DeletePrinter)]
public class DeletePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.GetPrinters)]
public class GetPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.GetPrinterResources)]
public class GetPrinterResourcesResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.ShutdownOnePrinter)]
public class ShutdownOnePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.StartupOnePrinter)]
public class StartupOnePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.RestartOnePrinter)]
public class RestartOnePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.CreateResourceSubscriptions)]
public class CreateResourceSubscriptionsResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}

[IppResponse(IppOperation.CreateSystemSubscriptions)]
public class CreateSystemSubscriptionsResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}

[IppResponse(IppOperation.DisableAllPrinters)]
public class DisableAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.EnableAllPrinters)]
public class EnableAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.PauseAllPrinters)]
public class PauseAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.PauseAllPrintersAfterCurrentJob)]
public class PauseAllPrintersAfterCurrentJobResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.RestartSystem)]
public class RestartSystemResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.ResumeAllPrinters)]
public class ResumeAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.SetSystemAttributes)]
public class SetSystemAttributesResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.ShutdownAllPrinters)]
public class ShutdownAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.StartupAllPrinters)]
public class StartupAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.CancelResource)]
public class CancelResourceResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.CancelSubscription)]
public class CancelSubscriptionResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.GetNotifications)]
public class GetNotificationsResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.GetSubscriptionAttributes)]
public class GetSubscriptionAttributesResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes? SubscriptionAttributes { get; set; }
}

[IppResponse(IppOperation.GetSubscriptions)]
public class GetSubscriptionsResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}

[IppResponse(IppOperation.RenewSubscription)]
public class RenewSubscriptionResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.CreateResource)]
public class CreateResourceResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.InstallResource)]
public class InstallResourceResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.SendResourceData)]
public class SendResourceDataResponse : IppResponse<OperationAttributes> { }

[IppResponse(IppOperation.SetResourceAttributes)]
public class SetResourceAttributesResponse : IppResponse<OperationAttributes> { }
