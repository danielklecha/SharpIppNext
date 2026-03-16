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
}
