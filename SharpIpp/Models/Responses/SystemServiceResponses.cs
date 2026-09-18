using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

// System service responses

[IppResponse]
public class DeallocatePrinterResourcesResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class DeletePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class GetPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class GetPrinterResourcesResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class ShutdownOnePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class StartupOnePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class RestartOnePrinterResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class CreateResourceSubscriptionsResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}

[IppResponse]
public class CreateSystemSubscriptionsResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}

[IppResponse]
public class DisableAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class EnableAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class PauseAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class PauseAllPrintersAfterCurrentJobResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class RestartSystemResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class ResumeAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class SetSystemAttributesResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class ShutdownAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class StartupAllPrintersResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class CancelResourceResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class CancelSubscriptionResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class GetNotificationsResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class GetSubscriptionAttributesResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes? SubscriptionAttributes { get; set; }
}

[IppResponse]
public class GetSubscriptionsResponse : IppResponse<OperationAttributes>
{
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}

[IppResponse]
public class RenewSubscriptionResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class CreateResourceResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class InstallResourceResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class SendResourceDataResponse : IppResponse<OperationAttributes> { }

[IppResponse]
public class SetResourceAttributesResponse : IppResponse<OperationAttributes> { }
