using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;

namespace SharpIpp;

public partial class SharpIppClient
{
    /// <inheritdoc />
    public Task<CancelJobResponse> CancelJobAsync(CancelJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CancelJobRequest, CancelJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CreateJobResponse> CreateJobAsync(CreateJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CreateJobRequest, CreateJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetJobAttributesResponse> GetJobAttributesAsync(GetJobAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetJobAttributesRequest, GetJobAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetJobsResponse> GetJobsAsync(GetJobsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetJobsRequest, GetJobsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetPrinterAttributesResponse> GetPrinterAttributesAsync(GetPrinterAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetPrinterAttributesRequest, GetPrinterAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HoldJobResponse> HoldJobAsync(HoldJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<HoldJobRequest, HoldJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PausePrinterResponse> PausePrinterAsync(PausePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PausePrinterRequest, PausePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IdentifyPrinterResponse> IdentifyPrinterAsync(IdentifyPrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<IdentifyPrinterRequest, IdentifyPrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PrintJobResponse> PrintJobAsync(PrintJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PrintJobRequest, PrintJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PrintUriResponse> PrintUriAsync(PrintUriRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PrintUriRequest, PrintUriResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PurgeJobsResponse> PurgeJobsAsync(PurgeJobsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PurgeJobsRequest, PurgeJobsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ReleaseJobResponse> ReleaseJobAsync(ReleaseJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ReleaseJobRequest, ReleaseJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<RestartJobResponse> RestartJobAsync(RestartJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<RestartJobRequest, RestartJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SetJobAttributesResponse> SetJobAttributesAsync(SetJobAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SetJobAttributesRequest, SetJobAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SetPrinterAttributesResponse> SetPrinterAttributesAsync(SetPrinterAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SetPrinterAttributesRequest, SetPrinterAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ResumePrinterResponse> ResumePrinterAsync(ResumePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ResumePrinterRequest, ResumePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SendDocumentResponse> SendDocumentAsync(SendDocumentRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SendDocumentRequest, SendDocumentResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SendUriResponse> SendUriAsync(SendUriRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SendUriRequest, SendUriResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ValidateDocumentResponse> ValidateDocumentAsync(ValidateDocumentRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ValidateDocumentRequest, ValidateDocumentResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ValidateJobResponse> ValidateJobAsync(ValidateJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ValidateJobRequest, ValidateJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CancelJobsResponse> CancelJobsAsync(CancelJobsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CancelJobsRequest, CancelJobsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CancelMyJobsResponse> CancelMyJobsAsync(CancelMyJobsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CancelMyJobsRequest, CancelMyJobsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ResubmitJobResponse> ResubmitJobAsync(ResubmitJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ResubmitJobRequest, ResubmitJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CloseJobResponse> CloseJobAsync(CloseJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CloseJobRequest, CloseJobResponse>(request, cancellationToken);
    }
    
    /// <inheritdoc />
    public Task<CancelDocumentResponse> CancelDocumentAsync(CancelDocumentRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CancelDocumentRequest, CancelDocumentResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetDocumentAttributesResponse> GetDocumentAttributesAsync(GetDocumentAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetDocumentAttributesRequest, GetDocumentAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetDocumentsResponse> GetDocumentsAsync(GetDocumentsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetDocumentsRequest, GetDocumentsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SetDocumentAttributesResponse> SetDocumentAttributesAsync(SetDocumentAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SetDocumentAttributesRequest, SetDocumentAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AcknowledgeDocumentResponse> AcknowledgeDocumentAsync(AcknowledgeDocumentRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<AcknowledgeDocumentRequest, AcknowledgeDocumentResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AcknowledgeIdentifyPrinterResponse> AcknowledgeIdentifyPrinterAsync(AcknowledgeIdentifyPrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<AcknowledgeIdentifyPrinterRequest, AcknowledgeIdentifyPrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AcknowledgeJobResponse> AcknowledgeJobAsync(AcknowledgeJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<AcknowledgeJobRequest, AcknowledgeJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DeregisterOutputDeviceResponse> DeregisterOutputDeviceAsync(DeregisterOutputDeviceRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<DeregisterOutputDeviceRequest, DeregisterOutputDeviceResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FetchDocumentResponse> FetchDocumentAsync(FetchDocumentRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<FetchDocumentRequest, FetchDocumentResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FetchJobResponse> FetchJobAsync(FetchJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<FetchJobRequest, FetchJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetOutputDeviceAttributesResponse> GetOutputDeviceAttributesAsync(GetOutputDeviceAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetOutputDeviceAttributesRequest, GetOutputDeviceAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<UpdateActiveJobsResponse> UpdateActiveJobsAsync(UpdateActiveJobsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<UpdateActiveJobsRequest, UpdateActiveJobsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<UpdateDocumentStatusResponse> UpdateDocumentStatusAsync(UpdateDocumentStatusRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<UpdateDocumentStatusRequest, UpdateDocumentStatusResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<UpdateJobStatusResponse> UpdateJobStatusAsync(UpdateJobStatusRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<UpdateJobStatusRequest, UpdateJobStatusResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<UpdateOutputDeviceAttributesResponse> UpdateOutputDeviceAttributesAsync(UpdateOutputDeviceAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<UpdateOutputDeviceAttributesRequest, UpdateOutputDeviceAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AllocatePrinterResourcesResponse> AllocatePrinterResourcesAsync(AllocatePrinterResourcesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<AllocatePrinterResourcesRequest, AllocatePrinterResourcesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CreatePrinterResponse> CreatePrinterAsync(CreatePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CreatePrinterRequest, CreatePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetSystemAttributesResponse> GetSystemAttributesAsync(GetSystemAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetSystemAttributesRequest, GetSystemAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetSystemSupportedValuesResponse> GetSystemSupportedValuesAsync(GetSystemSupportedValuesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetSystemSupportedValuesRequest, GetSystemSupportedValuesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<RegisterOutputDeviceResponse> RegisterOutputDeviceAsync(RegisterOutputDeviceRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<RegisterOutputDeviceRequest, RegisterOutputDeviceResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetResourcesResponse> GetResourcesAsync(GetResourcesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetResourcesRequest, GetResourcesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetResourceAttributesResponse> GetResourceAttributesAsync(GetResourceAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetResourceAttributesRequest, GetResourceAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CancelResourceResponse> CancelResourceAsync(CancelResourceRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CancelResourceRequest, CancelResourceResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CreateResourceResponse> CreateResourceAsync(CreateResourceRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CreateResourceRequest, CreateResourceResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<InstallResourceResponse> InstallResourceAsync(InstallResourceRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<InstallResourceRequest, InstallResourceResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SendResourceDataResponse> SendResourceDataAsync(SendResourceDataRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SendResourceDataRequest, SendResourceDataResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SetResourceAttributesResponse> SetResourceAttributesAsync(SetResourceAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SetResourceAttributesRequest, SetResourceAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DeallocatePrinterResourcesResponse> DeallocatePrinterResourcesAsync(DeallocatePrinterResourcesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<DeallocatePrinterResourcesRequest, DeallocatePrinterResourcesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DeletePrinterResponse> DeletePrinterAsync(DeletePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<DeletePrinterRequest, DeletePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetPrintersResponse> GetPrintersAsync(GetPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetPrintersRequest, GetPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetPrinterResourcesResponse> GetPrinterResourcesAsync(GetPrinterResourcesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetPrinterResourcesRequest, GetPrinterResourcesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ShutdownOnePrinterResponse> ShutdownOnePrinterAsync(ShutdownOnePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ShutdownOnePrinterRequest, ShutdownOnePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<StartupOnePrinterResponse> StartupOnePrinterAsync(StartupOnePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<StartupOnePrinterRequest, StartupOnePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<RestartOnePrinterResponse> RestartOnePrinterAsync(RestartOnePrinterRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<RestartOnePrinterRequest, RestartOnePrinterResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CreateResourceSubscriptionsResponse> CreateResourceSubscriptionsAsync(CreateResourceSubscriptionsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CreateResourceSubscriptionsRequest, CreateResourceSubscriptionsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CreateSystemSubscriptionsResponse> CreateSystemSubscriptionsAsync(CreateSystemSubscriptionsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CreateSystemSubscriptionsRequest, CreateSystemSubscriptionsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<CancelSubscriptionResponse> CancelSubscriptionAsync(CancelSubscriptionRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<CancelSubscriptionRequest, CancelSubscriptionResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetNotificationsResponse> GetNotificationsAsync(GetNotificationsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetNotificationsRequest, GetNotificationsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetSubscriptionAttributesResponse> GetSubscriptionAttributesAsync(GetSubscriptionAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetSubscriptionAttributesRequest, GetSubscriptionAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetSubscriptionsResponse> GetSubscriptionsAsync(GetSubscriptionsRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetSubscriptionsRequest, GetSubscriptionsResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<RenewSubscriptionResponse> RenewSubscriptionAsync(RenewSubscriptionRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<RenewSubscriptionRequest, RenewSubscriptionResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DisableAllPrintersResponse> DisableAllPrintersAsync(DisableAllPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<DisableAllPrintersRequest, DisableAllPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<EnableAllPrintersResponse> EnableAllPrintersAsync(EnableAllPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<EnableAllPrintersRequest, EnableAllPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PauseAllPrintersResponse> PauseAllPrintersAsync(PauseAllPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PauseAllPrintersRequest, PauseAllPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PauseAllPrintersAfterCurrentJobResponse> PauseAllPrintersAfterCurrentJobAsync(PauseAllPrintersAfterCurrentJobRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PauseAllPrintersAfterCurrentJobRequest, PauseAllPrintersAfterCurrentJobResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<RestartSystemResponse> RestartSystemAsync(RestartSystemRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<RestartSystemRequest, RestartSystemResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ResumeAllPrintersResponse> ResumeAllPrintersAsync(ResumeAllPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ResumeAllPrintersRequest, ResumeAllPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SetSystemAttributesResponse> SetSystemAttributesAsync(SetSystemAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<SetSystemAttributesRequest, SetSystemAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ShutdownAllPrintersResponse> ShutdownAllPrintersAsync(ShutdownAllPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<ShutdownAllPrintersRequest, ShutdownAllPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<StartupAllPrintersResponse> StartupAllPrintersAsync(StartupAllPrintersRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<StartupAllPrintersRequest, StartupAllPrintersResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetUserPrinterAttributesResponse> GetUserPrinterAttributesAsync(GetUserPrinterAttributesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetUserPrinterAttributesRequest, GetUserPrinterAttributesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AddDocumentImagesResponse> AddDocumentImagesAsync(AddDocumentImagesRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<AddDocumentImagesRequest, AddDocumentImagesResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetNextDocumentDataResponse> GetNextDocumentDataAsync(GetNextDocumentDataRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<GetNextDocumentDataRequest, GetNextDocumentDataResponse>(request, cancellationToken);
    }
}
