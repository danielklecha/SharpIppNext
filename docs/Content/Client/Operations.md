# SharpIppNext Operations

The `ISharpIppClient` interface currently exposes the following operation methods. Each operation below links to a dedicated example page.

SharpIppNext also supports attribute extensions that are modeled on the request and response types without adding new client methods. For the PWG 5100.2 `output-bin` extension, see [OutputBinExtension.md](OutputBinExtension.md).

For the PWG 5100.6 page-override attributes (`overrides`, `overrides-supported`, `overrides-actual`), see [PageOverridesExtension.md](PageOverridesExtension.md).

For the PWG 5100.8 "-actual" attributes extension (including the `job-actual` requested-attributes group for Get-Job-Attributes and Get-Jobs), use the strongly typed properties on `JobDescriptionAttributes`.

For the PWG 5100.9 printer state extension attributes (`printer-alert`, `printer-alert-description`, and extended `printer-state-reasons` keyword values), use the strongly typed properties on `PrinterDescriptionAttributes` and `PrinterStateReason`.

| Operation | Method | Example |
| --- | --- | --- |
| Acknowledge-Document | `AcknowledgeDocumentAsync` | [AcknowledgeDocument.md](AcknowledgeDocument.md) |
| Acknowledge-Job | `AcknowledgeJobAsync` | [AcknowledgeJob.md](AcknowledgeJob.md) |
| Add-Document-Images | `AddDocumentImagesAsync` | [AddDocumentImages.md](AddDocumentImages.md) |
| Allocate-Printer-Resources | `AllocatePrinterResourcesAsync` | [AllocatePrinterResources.md](AllocatePrinterResources.md) |
| Cancel-Document | `CancelDocumentAsync` | [CancelDocument.md](CancelDocument.md) |
| Cancel-Job | `CancelJobAsync` | [CancelJob.md](CancelJob.md) |
| Cancel-Jobs | `CancelJobsAsync` | [CancelJobs.md](CancelJobs.md) |
| Cancel-My-Jobs | `CancelMyJobsAsync` | [CancelMyJobs.md](CancelMyJobs.md) |
| Cancel-Resource | `CancelResourceAsync` | [CancelResource.md](CancelResource.md) |
| Cancel-Subscription | `CancelSubscriptionAsync` | [CancelSubscription.md](CancelSubscription.md) |
| Close-Job | `CloseJobAsync` | [CloseJob.md](CloseJob.md) |
| Create-Job | `CreateJobAsync` | [CreateJob.md](CreateJob.md) |
| Create-Printer | `CreatePrinterAsync` | [CreatePrinter.md](CreatePrinter.md) |
| Create-Resource | `CreateResourceAsync` | [CreateResource.md](CreateResource.md) |
| Create-Resource-Subscriptions | `CreateResourceSubscriptionsAsync` | [CreateResourceSubscriptions.md](CreateResourceSubscriptions.md) |
| Create-System-Subscriptions | `CreateSystemSubscriptionsAsync` | [CreateSystemSubscriptions.md](CreateSystemSubscriptions.md) |
| CUPS-Get-Printers | `GetCUPSPrintersAsync` | [CUPSGetPrinters.md](CUPSGetPrinters.md) |
| Deallocate-Printer-Resources | `DeallocatePrinterResourcesAsync` | [DeallocatePrinterResources.md](DeallocatePrinterResources.md) |
| Delete-Printer | `DeletePrinterAsync` | [DeletePrinter.md](DeletePrinter.md) |
| Disable-All-Printers | `DisableAllPrintersAsync` | [DisableAllPrinters.md](DisableAllPrinters.md) |
| Enable-All-Printers | `EnableAllPrintersAsync` | [EnableAllPrinters.md](EnableAllPrinters.md) |
| Fetch-Document | `FetchDocumentAsync` | [FetchDocument.md](FetchDocument.md) |
| Get-Document-Attributes | `GetDocumentAttributesAsync` | [GetDocumentAttributes.md](GetDocumentAttributes.md) |
| Get-Documents | `GetDocumentsAsync` | [GetDocuments.md](GetDocuments.md) |
| Get-Job-Attributes | `GetJobAttributesAsync` | [GetJobAttributes.md](GetJobAttributes.md) |
| Get-Jobs | `GetJobsAsync` | [GetJobs.md](GetJobs.md) |
| Get-Next-Document-Data | `GetNextDocumentDataAsync` | [GetNextDocumentData.md](GetNextDocumentData.md) |
| Get-Notifications | `GetNotificationsAsync` | [GetNotifications.md](GetNotifications.md) |
| Get-Output-Device-Attributes | `GetOutputDeviceAttributesAsync` | [GetOutputDeviceAttributes.md](GetOutputDeviceAttributes.md) |
| Get-Printer-Attributes | `GetPrinterAttributesAsync` | [GetPrinterAttributes.md](GetPrinterAttributes.md) |
| Get-Printer-Resources | `GetPrinterResourcesAsync` | [GetPrinterResources.md](GetPrinterResources.md) |
| Get-Printers | `GetPrintersAsync` | [GetPrinters.md](GetPrinters.md) |
| Get-Resource-Attributes | `GetResourceAttributesAsync` | [GetResourceAttributes.md](GetResourceAttributes.md) |
| Get-Resources | `GetResourcesAsync` | [GetResources.md](GetResources.md) |
| Get-Subscription-Attributes | `GetSubscriptionAttributesAsync` | [GetSubscriptionAttributes.md](GetSubscriptionAttributes.md) |
| Get-Subscriptions | `GetSubscriptionsAsync` | [GetSubscriptions.md](GetSubscriptions.md) |
| Get-System-Attributes | `GetSystemAttributesAsync` | [GetSystemAttributes.md](GetSystemAttributes.md) |
| Get-System-Supported-Values | `GetSystemSupportedValuesAsync` | [GetSystemSupportedValues.md](GetSystemSupportedValues.md) |
| Get-User-Printer-Attributes | `GetUserPrinterAttributesAsync` | [GetUserPrinterAttributes.md](GetUserPrinterAttributes.md) |
| Hold-Job | `HoldJobAsync` | [HoldJob.md](HoldJob.md) |
| Identify-Printer | `IdentifyPrinterAsync` | [IdentifyPrinter.md](IdentifyPrinter.md) |
| Install-Resource | `InstallResourceAsync` | [InstallResource.md](InstallResource.md) |
| Pause-All-Printers | `PauseAllPrintersAsync` | [PauseAllPrinters.md](PauseAllPrinters.md) |
| Pause-All-Printers-After-Current-Job | `PauseAllPrintersAfterCurrentJobAsync` | [PauseAllPrintersAfterCurrentJob.md](PauseAllPrintersAfterCurrentJob.md) |
| Pause-Printer | `PausePrinterAsync` | [PausePrinter.md](PausePrinter.md) |
| Print-Job | `PrintJobAsync` | [PrintJob.md](PrintJob.md) |
| Print-URI | `PrintUriAsync` | [PrintUri.md](PrintUri.md) |
| Purge-Jobs | `PurgeJobsAsync` | [PurgeJobs.md](PurgeJobs.md) |
| Register-Output-Device | `RegisterOutputDeviceAsync` | [RegisterOutputDevice.md](RegisterOutputDevice.md) |
| Release-Job | `ReleaseJobAsync` | [ReleaseJob.md](ReleaseJob.md) |
| Renew-Subscription | `RenewSubscriptionAsync` | [RenewSubscription.md](RenewSubscription.md) |
| Restart-Job | `RestartJobAsync` | [RestartJob.md](RestartJob.md) |
| Restart-One-Printer | `RestartOnePrinterAsync` | [RestartOnePrinter.md](RestartOnePrinter.md) |
| Restart-System | `RestartSystemAsync` | [RestartSystem.md](RestartSystem.md) |
| Resubmit-Job | `ResubmitJobAsync` | [ResubmitJob.md](ResubmitJob.md) |
| Resume-All-Printers | `ResumeAllPrintersAsync` | [ResumeAllPrinters.md](ResumeAllPrinters.md) |
| Resume-Printer | `ResumePrinterAsync` | [ResumePrinter.md](ResumePrinter.md) |
| Send-Document | `SendDocumentAsync` | [SendDocument.md](SendDocument.md) |
| Send-Resource-Data | `SendResourceDataAsync` | [SendResourceData.md](SendResourceData.md) |
| Send-URI | `SendUriAsync` | [SendUri.md](SendUri.md) |
| Set-Document-Attributes | `SetDocumentAttributesAsync` | [SetDocumentAttributes.md](SetDocumentAttributes.md) |
| Set-Job-Attributes | `SetJobAttributesAsync` | [SetJobAttributes.md](SetJobAttributes.md) |
| Set-Printer-Attributes | `SetPrinterAttributesAsync` | [SetPrinterAttributes.md](SetPrinterAttributes.md) |
| Set-Resource-Attributes | `SetResourceAttributesAsync` | [SetResourceAttributes.md](SetResourceAttributes.md) |
| Set-System-Attributes | `SetSystemAttributesAsync` | [SetSystemAttributes.md](SetSystemAttributes.md) |
| Shutdown-All-Printers | `ShutdownAllPrintersAsync` | [ShutdownAllPrinters.md](ShutdownAllPrinters.md) |
| Shutdown-One-Printer | `ShutdownOnePrinterAsync` | [ShutdownOnePrinter.md](ShutdownOnePrinter.md) |
| Startup-All-Printers | `StartupAllPrintersAsync` | [StartupAllPrinters.md](StartupAllPrinters.md) |
| Startup-One-Printer | `StartupOnePrinterAsync` | [StartupOnePrinter.md](StartupOnePrinter.md) |
| Update-Active-Jobs | `UpdateActiveJobsAsync` | [UpdateActiveJobs.md](UpdateActiveJobs.md) |
| Validate-Job | `ValidateJobAsync` | [ValidateJob.md](ValidateJob.md) |

If you need to send a custom operation or an operation not currently exposed by a strongly-typed method in `ISharpIppClient`, you can use the generic `SendAsync` overload. See the [Custom Requests](CustomRequests.md) documentation for more details.
