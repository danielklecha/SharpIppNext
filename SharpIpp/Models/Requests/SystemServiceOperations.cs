namespace SharpIpp.Models.Requests;

// System service requests

public class DeallocatePrinterResourcesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class DeletePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class GetPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class GetPrinterResourcesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class ShutdownOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class StartupOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class RestartOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class CreateResourceSubscriptionsRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class CreateSystemSubscriptionsRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class DisableAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class EnableAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class PauseAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class PauseAllPrintersAfterCurrentJobRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class RestartSystemRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class ResumeAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class SetSystemAttributesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class ShutdownAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class StartupAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }

public class CancelResourceRequest : IppRequest<CancelResourceOperationAttributes>, IIppSystemRequest { }
public class CreateResourceRequest : IppRequest<CreateResourceOperationAttributes>, IIppSystemRequest { }
public class InstallResourceRequest : IppRequest<InstallResourceOperationAttributes>, IIppSystemRequest { }
public class SendResourceDataRequest : IppRequest<SendResourceDataOperationAttributes>, IIppSystemRequest { }
public class SetResourceAttributesRequest : IppRequest<SetResourceAttributesOperationAttributes>, IIppSystemRequest { }

public class CancelSubscriptionRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class GetNotificationsRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class GetSubscriptionAttributesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class GetSubscriptionsRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
public class RenewSubscriptionRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest { }
