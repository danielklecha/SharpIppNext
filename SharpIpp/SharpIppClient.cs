using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Exceptions;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp;

public partial class SharpIppClient : ISharpIppClient
{
    private static readonly Lazy<IMapper> MapperSingleton;

    private readonly bool _disposeHttpClient;
    private readonly HttpClient _httpClient;
    private readonly IIppProtocol _ippProtocol;

    /// <inheritdoc />
    public IIppRequestMessageValidator? RequestMessageValidator { get; set; } = IppRequestMessageValidator.Default;

    /// <inheritdoc />
    public IIppRequestValidator? RequestValidator { get; set; } = IppRequestValidator.Default;

    /// <inheritdoc />
    public IIppResponseMessageValidator? ResponseMessageValidator { get; set; }

    /// <inheritdoc />
    public IIppResponseValidator? ResponseValidator { get; set; }

    static SharpIppClient()
    {
        MapperSingleton = new Lazy<IMapper>(MapperFactory);
    }

    public SharpIppClient() : this(new HttpClient(), new IppProtocol(), true)
    {
    }

    public SharpIppClient(HttpClient httpClient) : this(httpClient, new IppProtocol(), false)
    {
    }

    public SharpIppClient(HttpClient httpClient, IIppProtocol ippProtocol) : this(httpClient, ippProtocol, false)
    {
    }

    internal SharpIppClient(HttpClient httpClient, IIppProtocol ippProtocol, bool disposeHttpClient)
    {
        _httpClient = httpClient;
        _ippProtocol = ippProtocol;
        _disposeHttpClient = disposeHttpClient;
    }

    private IMapper Mapper => MapperSingleton.Value;

    /// <summary>
    /// Status codes of <see cref="HttpResponseMessage" /> that are not successful,
    /// but response still contains valid ipp-data in the body that can be parsed for better error description
    /// Seems like they are printer specific
    /// </summary>
    private static readonly HttpStatusCode[] _plausibleHttpStatusCodes = [
        HttpStatusCode.Continue,
        HttpStatusCode.Unauthorized,
        HttpStatusCode.Forbidden,
        HttpStatusCode.UpgradeRequired,
    ];

    /// <inheritdoc />
    public async Task<IIppResponseMessage> SendAsync(
        Uri printerUri,
        IIppRequestMessage ippRequest,
        CancellationToken cancellationToken = default)
    {
        if (RequestMessageValidator != null)
        {
            RequestMessageValidator.Validate(ippRequest);
        }

        var httpRequest = GetHttpRequestMessage( printerUri );

        httpRequest.Content = new IppRequestContent(ippRequest, _ippProtocol, cancellationToken);
        HttpResponseMessage response = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        Exception? httpException = null;

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            if (!_plausibleHttpStatusCodes.Contains(response.StatusCode))
            {
                throw;
            }

            httpException = ex;
        }

        IIppResponseMessage? ippResponse;

        try
        {
            using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            ippResponse = await _ippProtocol.ReadIppResponseAsync(responseStream, cancellationToken).ConfigureAwait(false);
            ResponseMessageValidator?.Validate(ippResponse);
            if (!ippResponse.IsSuccessfulStatusCode())
                throw new IppResponseException($"Printer returned error code", ippResponse);
        }
        catch
        {
            if (httpException == null)
            {
                throw;
            }

            throw httpException;
        }

        if (httpException == null)
        {
            return ippResponse;
        }

        throw new IppResponseException(httpException.Message, httpException, ippResponse);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient.Dispose();
        }
    }

    protected async Task<TOut> SendAsync<TIn, TOut>(
        TIn data,
        CancellationToken cancellationToken)
        where TIn : IIppRequest
        where TOut : IIppResponse
    {
        RequestValidator?.Validate(data);
        var ippRequest = CreateRawRequest(data);
        if (data.OperationAttributes == null)
            throw new Exception("OperationAttributes is not set");

        Uri? targetUri;
        if (data is IIppSystemRequest)
        {
            var systemAttrs = data.OperationAttributes as SystemOperationAttributes;
            targetUri = systemAttrs?.SystemUri;
            if (targetUri == null)
                throw new Exception("SystemUri is not set");
        }
        else
        {
            targetUri = data.OperationAttributes.PrinterUri;
        }

        if (targetUri == null)
            throw new Exception("PrinterUri or SystemUri is not set");

        var ippResponse = await SendAsync(targetUri, ippRequest, cancellationToken).ConfigureAwait(false);
        var res = CreateResponse<TOut>(ippResponse);
        ResponseValidator?.Validate(res);
        return res;
    }

    /// <inheritdoc />
    public IIppRequestMessage CreateRawRequest<T>(T ippRequestMessage) where T : IIppRequest
    {
        if (ippRequestMessage is null)
            throw new ArgumentNullException(nameof(ippRequestMessage));
        var ippRequest = Mapper.Map<T, IppRequestMessage>(ippRequestMessage);
        return ippRequest;
    }

    /// <inheritdoc />
    public virtual T CreateResponse<T>(IIppResponseMessage ippResponse) where T : IIppResponse
    {
        return (T)CreateResponse(typeof(T), ippResponse);
    }

    /// <summary>
    /// Maps a raw IPP response message to the specified response type.
    /// </summary>
    /// <param name="responseType">The type of the IPP response.</param>
    /// <param name="ippResponse">The raw IPP response message.</param>
    /// <returns>The mapped IPP response object.</returns>
    public virtual object CreateResponse(Type responseType, IIppResponseMessage ippResponse)
    {
        try
        {
            var r = Mapper.Map(ippResponse, ippResponse.GetType(), responseType, null);
            return r;
        }
        catch (Exception ex)
        {
            throw new IppResponseException("Ipp attributes mapping exception", ex, ippResponse);
        }
    }

    private static HttpRequestMessage GetHttpRequestMessage( Uri printer )
    {
        var isSecured = printer.Scheme.Equals( "https", StringComparison.OrdinalIgnoreCase )
            || printer.Scheme.Equals( "ipps", StringComparison.OrdinalIgnoreCase );
        var defaultPort = printer.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase)
            ? 443
            : printer.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase)
            ? 80
            : 631;
        var uriBuilder = new UriBuilder(isSecured ? "https" : "http", printer.Host, printer.Port == -1 ? defaultPort : printer.Port, printer.AbsolutePath)
        {
            Query = printer.Query
        };
        return new HttpRequestMessage( HttpMethod.Post, uriBuilder.Uri );
    }

    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Mapping profiles are preserved via ILLink.Descriptors.xml")]
    private static IMapper MapperFactory()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(TypesProfile));
        mapper.FillFromAssembly(assembly!);
        return mapper;
    }

    /// <inheritdoc />
    public Task<CUPSGetPrintersResponse> GetCUPSPrintersAsync(CUPSGetPrintersRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<CUPSGetPrintersRequest, CUPSGetPrintersResponse>(request, cancellationToken);

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
    [Obsolete("The 'Print-URI' operation is deprecated.")]
    public Task<PrintUriResponse> PrintUriAsync(PrintUriRequest request, CancellationToken cancellationToken = default)
    {
        return SendAsync<PrintUriRequest, PrintUriResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    [Obsolete("The 'Purge-Jobs' operation is deprecated. See RFC 8011 Section 4.2.9.")]
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
    [Obsolete("The 'Restart-Job' operation is deprecated. See RFC 8011 Section 4.3.7.")]
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
    [Obsolete("The 'Send-URI' operation is deprecated.")]
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

    /// <inheritdoc />
    public Task<ActivatePrinterResponse> ActivatePrinterAsync(ActivatePrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<ActivatePrinterRequest, ActivatePrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<DeactivatePrinterResponse> DeactivatePrinterAsync(DeactivatePrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<DeactivatePrinterRequest, DeactivatePrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<DisablePrinterResponse> DisablePrinterAsync(DisablePrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<DisablePrinterRequest, DisablePrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<EnablePrinterResponse> EnablePrinterAsync(EnablePrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<EnablePrinterRequest, EnablePrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<GetPrinterSupportedValuesResponse> GetPrinterSupportedValuesAsync(GetPrinterSupportedValuesRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<GetPrinterSupportedValuesRequest, GetPrinterSupportedValuesResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<HoldNewJobsResponse> HoldNewJobsAsync(HoldNewJobsRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<HoldNewJobsRequest, HoldNewJobsResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<PausePrinterAfterCurrentJobResponse> PausePrinterAfterCurrentJobAsync(PausePrinterAfterCurrentJobRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<PausePrinterAfterCurrentJobRequest, PausePrinterAfterCurrentJobResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<ReleaseHeldNewJobsResponse> ReleaseHeldNewJobsAsync(ReleaseHeldNewJobsRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<ReleaseHeldNewJobsRequest, ReleaseHeldNewJobsResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<RestartPrinterResponse> RestartPrinterAsync(RestartPrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<RestartPrinterRequest, RestartPrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<ShutdownPrinterResponse> ShutdownPrinterAsync(ShutdownPrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<ShutdownPrinterRequest, ShutdownPrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<StartupPrinterResponse> StartupPrinterAsync(StartupPrinterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<StartupPrinterRequest, StartupPrinterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<CancelCurrentJobResponse> CancelCurrentJobAsync(CancelCurrentJobRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<CancelCurrentJobRequest, CancelCurrentJobResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<PromoteJobResponse> PromoteJobAsync(PromoteJobRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<PromoteJobRequest, PromoteJobResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<ResumeJobResponse> ResumeJobAsync(ResumeJobRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<ResumeJobRequest, ResumeJobResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<ScheduleJobAfterResponse> ScheduleJobAfterAsync(ScheduleJobAfterRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<ScheduleJobAfterRequest, ScheduleJobAfterResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<SuspendCurrentJobResponse> SuspendCurrentJobAsync(SuspendCurrentJobRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<SuspendCurrentJobRequest, SuspendCurrentJobResponse>(request, cancellationToken);

    /// <inheritdoc />
    [Obsolete("The 'Delete-Document' operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025.")]
    public Task<DeleteDocumentResponse> DeleteDocumentAsync(DeleteDocumentRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<DeleteDocumentRequest, DeleteDocumentResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<CreatePrinterSubscriptionsResponse> CreatePrinterSubscriptionsAsync(CreatePrinterSubscriptionsRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<CreatePrinterSubscriptionsRequest, CreatePrinterSubscriptionsResponse>(request, cancellationToken);

    /// <inheritdoc />
    public Task<CreateJobSubscriptionsResponse> CreateJobSubscriptionsAsync(CreateJobSubscriptionsRequest request, CancellationToken cancellationToken = default) =>
        SendAsync<CreateJobSubscriptionsRequest, CreateJobSubscriptionsResponse>(request, cancellationToken);
}
