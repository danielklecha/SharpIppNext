using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

// System service responses

public class DeallocatePrinterResourcesResponse : IppResponse<OperationAttributes> { }
public class DeletePrinterResponse : IppResponse<OperationAttributes> { }
public class GetPrintersResponse : IppResponse<OperationAttributes> { }
public class GetPrinterResourcesResponse : IppResponse<OperationAttributes> { }
public class ShutdownOnePrinterResponse : IppResponse<OperationAttributes> { }
public class StartupOnePrinterResponse : IppResponse<OperationAttributes> { }
public class RestartOnePrinterResponse : IppResponse<OperationAttributes> { }
public class CreateResourceSubscriptionsResponse : IppResponse<OperationAttributes> { }
public class CreateSystemSubscriptionsResponse : IppResponse<OperationAttributes> { }
public class DisableAllPrintersResponse : IppResponse<OperationAttributes> { }
public class EnableAllPrintersResponse : IppResponse<OperationAttributes> { }
public class PauseAllPrintersResponse : IppResponse<OperationAttributes> { }
public class PauseAllPrintersAfterCurrentJobResponse : IppResponse<OperationAttributes> { }
public class RestartSystemResponse : IppResponse<OperationAttributes> { }
public class ResumeAllPrintersResponse : IppResponse<OperationAttributes> { }
public class SetSystemAttributesResponse : IppResponse<OperationAttributes> { }
public class ShutdownAllPrintersResponse : IppResponse<OperationAttributes> { }
public class StartupAllPrintersResponse : IppResponse<OperationAttributes> { }

public class CancelResourceResponse : IppResponse<OperationAttributes> { }
public class CancelSubscriptionResponse : IppResponse<OperationAttributes> { }
public class GetNotificationsResponse : IppResponse<OperationAttributes> { }
public class GetSubscriptionAttributesResponse : IppResponse<OperationAttributes> { }
public class GetSubscriptionsResponse : IppResponse<OperationAttributes> { }
public class RenewSubscriptionResponse : IppResponse<OperationAttributes> { }public class CreateResourceResponse : IppResponse<OperationAttributes> { }
public class InstallResourceResponse : IppResponse<OperationAttributes> { }
public class SendResourceDataResponse : IppResponse<OperationAttributes> { }
public class SetResourceAttributesResponse : IppResponse<OperationAttributes> { }
