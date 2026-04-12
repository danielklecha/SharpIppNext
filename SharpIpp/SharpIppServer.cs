using SharpIpp.Exceptions;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp;

public partial class SharpIppServer : ISharpIppServer
{
    private static readonly Lazy<IMapper> MapperSingleton;
    private readonly IIppProtocol _ippProtocol;
    private readonly IIppRequestValidator _requestValidator;
    private IMapper Mapper => MapperSingleton.Value;

    static SharpIppServer()
    {
        MapperSingleton = new Lazy<IMapper>(MapperFactory);
    }

    public SharpIppServer()
    {
        _ippProtocol = new IppProtocol();
        _requestValidator = IppRequestValidator.Default;
    }

    public SharpIppServer(IIppProtocol ippProtocol)
    {
        _ippProtocol = ippProtocol;
        _requestValidator = IppRequestValidator.Default;
    }

    public SharpIppServer(IIppProtocol ippProtocol, IIppRequestValidator requestValidator)
    {
        _ippProtocol = ippProtocol;
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
    }

    public Task<IIppRequestMessage> ReceiveRawRequestAsync(
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        return _ippProtocol.ReadIppRequestAsync(stream, cancellationToken);
    }

    public async Task<IIppRequest> ReceiveRequestAsync(
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        var request = await ReceiveRawRequestAsync(stream, cancellationToken);
        return await ReceiveRequestAsync(request);
    }

    public Task<IIppRequest> ReceiveRequestAsync(
        IIppRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        _requestValidator.Context.Source = nameof(SharpIppServer);
        _requestValidator.Validate(request);

        IIppRequest mappedRequest = request.IppOperation switch
        {
            IppOperation.AllocatePrinterResources => Mapper.Map<IIppRequestMessage, AllocatePrinterResourcesRequest>(request),
            IppOperation.AddDocumentImages => Mapper.Map<IIppRequestMessage, AddDocumentImagesRequest>(request),
            IppOperation.AcknowledgeDocument => Mapper.Map<IIppRequestMessage, AcknowledgeDocumentRequest>(request),
            IppOperation.AcknowledgeJob => Mapper.Map<IIppRequestMessage, AcknowledgeJobRequest>(request),
            IppOperation.CancelDocument => Mapper.Map<IIppRequestMessage, CancelDocumentRequest>(request),
            IppOperation.CancelJob => Mapper.Map<IIppRequestMessage, CancelJobRequest>(request),
            IppOperation.CancelJobs => Mapper.Map<IIppRequestMessage, CancelJobsRequest>(request),
            IppOperation.CancelMyJobs => Mapper.Map<IIppRequestMessage, CancelMyJobsRequest>(request),
            IppOperation.CloseJob => Mapper.Map<IIppRequestMessage, CloseJobRequest>(request),
            IppOperation.CreatePrinter => Mapper.Map<IIppRequestMessage, CreatePrinterRequest>(request),
            IppOperation.CreateJob => Mapper.Map<IIppRequestMessage, CreateJobRequest>(request),
            IppOperation.FetchDocument => Mapper.Map<IIppRequestMessage, FetchDocumentRequest>(request),
            IppOperation.GetCUPSPrinters => Mapper.Map<IIppRequestMessage, CUPSGetPrintersRequest>(request),
            IppOperation.GetJobAttributes => Mapper.Map<IIppRequestMessage, GetJobAttributesRequest>(request),
            IppOperation.GetOutputDeviceAttributes => Mapper.Map<IIppRequestMessage, GetOutputDeviceAttributesRequest>(request),
            IppOperation.GetNextDocumentData => Mapper.Map<IIppRequestMessage, GetNextDocumentDataRequest>(request),
            IppOperation.GetSystemAttributes => Mapper.Map<IIppRequestMessage, GetSystemAttributesRequest>(request),
            IppOperation.GetSystemSupportedValues => Mapper.Map<IIppRequestMessage, GetSystemSupportedValuesRequest>(request),
            IppOperation.GetResources => Mapper.Map<IIppRequestMessage, GetResourcesRequest>(request),
            IppOperation.GetResourceAttributes => Mapper.Map<IIppRequestMessage, GetResourceAttributesRequest>(request),
            IppOperation.DeallocatePrinterResources => Mapper.Map<IIppRequestMessage, DeallocatePrinterResourcesRequest>(request),
            IppOperation.DeletePrinter => Mapper.Map<IIppRequestMessage, DeletePrinterRequest>(request),
            IppOperation.GetPrinters => Mapper.Map<IIppRequestMessage, GetPrintersRequest>(request),
            IppOperation.GetPrinterResources => Mapper.Map<IIppRequestMessage, GetPrinterResourcesRequest>(request),
            IppOperation.ShutdownOnePrinter => Mapper.Map<IIppRequestMessage, ShutdownOnePrinterRequest>(request),
            IppOperation.StartupOnePrinter => Mapper.Map<IIppRequestMessage, StartupOnePrinterRequest>(request),
            IppOperation.RestartOnePrinter => Mapper.Map<IIppRequestMessage, RestartOnePrinterRequest>(request),
            IppOperation.CreateResourceSubscriptions => Mapper.Map<IIppRequestMessage, CreateResourceSubscriptionsRequest>(request),
            IppOperation.CreateSystemSubscriptions => Mapper.Map<IIppRequestMessage, CreateSystemSubscriptionsRequest>(request),
            IppOperation.CancelSubscription => Mapper.Map<IIppRequestMessage, CancelSubscriptionRequest>(request),
            IppOperation.GetNotifications => Mapper.Map<IIppRequestMessage, GetNotificationsRequest>(request),
            IppOperation.GetSubscriptionAttributes => Mapper.Map<IIppRequestMessage, GetSubscriptionAttributesRequest>(request),
            IppOperation.GetSubscriptions => Mapper.Map<IIppRequestMessage, GetSubscriptionsRequest>(request),
            IppOperation.RenewSubscription => Mapper.Map<IIppRequestMessage, RenewSubscriptionRequest>(request),
            IppOperation.DisableAllPrinters => Mapper.Map<IIppRequestMessage, DisableAllPrintersRequest>(request),
            IppOperation.EnableAllPrinters => Mapper.Map<IIppRequestMessage, EnableAllPrintersRequest>(request),
            IppOperation.PauseAllPrinters => Mapper.Map<IIppRequestMessage, PauseAllPrintersRequest>(request),
            IppOperation.PauseAllPrintersAfterCurrentJob => Mapper.Map<IIppRequestMessage, PauseAllPrintersAfterCurrentJobRequest>(request),
            IppOperation.RestartSystem => Mapper.Map<IIppRequestMessage, RestartSystemRequest>(request),
            IppOperation.ResumeAllPrinters => Mapper.Map<IIppRequestMessage, ResumeAllPrintersRequest>(request),
            IppOperation.SetSystemAttributes => Mapper.Map<IIppRequestMessage, SetSystemAttributesRequest>(request),
            IppOperation.ShutdownAllPrinters => Mapper.Map<IIppRequestMessage, ShutdownAllPrintersRequest>(request),
            IppOperation.StartupAllPrinters => Mapper.Map<IIppRequestMessage, StartupAllPrintersRequest>(request),
            IppOperation.CancelResource => Mapper.Map<IIppRequestMessage, CancelResourceRequest>(request),
            IppOperation.CreateResource => Mapper.Map<IIppRequestMessage, CreateResourceRequest>(request),
            IppOperation.InstallResource => Mapper.Map<IIppRequestMessage, InstallResourceRequest>(request),
            IppOperation.SendResourceData => Mapper.Map<IIppRequestMessage, SendResourceDataRequest>(request),
            IppOperation.SetResourceAttributes => Mapper.Map<IIppRequestMessage, SetResourceAttributesRequest>(request),
            IppOperation.GetUserPrinterAttributes => Mapper.Map<IIppRequestMessage, GetUserPrinterAttributesRequest>(request),
            IppOperation.GetJobs => Mapper.Map<IIppRequestMessage, GetJobsRequest>(request),
            IppOperation.GetPrinterAttributes => Mapper.Map<IIppRequestMessage, GetPrinterAttributesRequest>(request),
            IppOperation.HoldJob => Mapper.Map<IIppRequestMessage, HoldJobRequest>(request),
            IppOperation.IdentifyPrinter => Mapper.Map<IIppRequestMessage, IdentifyPrinterRequest>(request),
            IppOperation.PausePrinter => Mapper.Map<IIppRequestMessage, PausePrinterRequest>(request),
            IppOperation.PrintJob => Mapper.Map<IIppRequestMessage, PrintJobRequest>(request),
            IppOperation.PrintUri => Mapper.Map<IIppRequestMessage, PrintUriRequest>(request),
            IppOperation.PurgeJobs => Mapper.Map<IIppRequestMessage, PurgeJobsRequest>(request),
            IppOperation.ReleaseJob => Mapper.Map<IIppRequestMessage, ReleaseJobRequest>(request),
            IppOperation.RestartJob => Mapper.Map<IIppRequestMessage, RestartJobRequest>(request),
            IppOperation.SetJobAttributes => Mapper.Map<IIppRequestMessage, SetJobAttributesRequest>(request),
            IppOperation.SetPrinterAttributes => Mapper.Map<IIppRequestMessage, SetPrinterAttributesRequest>(request),
            IppOperation.ResubmitJob => Mapper.Map<IIppRequestMessage, ResubmitJobRequest>(request),
            IppOperation.ResumePrinter => Mapper.Map<IIppRequestMessage, ResumePrinterRequest>(request),
            IppOperation.RegisterOutputDevice => Mapper.Map<IIppRequestMessage, RegisterOutputDeviceRequest>(request),
            IppOperation.SendDocument => Mapper.Map<IIppRequestMessage, SendDocumentRequest>(request),
            IppOperation.SendUri => Mapper.Map<IIppRequestMessage, SendUriRequest>(request),
            IppOperation.UpdateActiveJobs => Mapper.Map<IIppRequestMessage, UpdateActiveJobsRequest>(request),
            IppOperation.ValidateJob => Mapper.Map<IIppRequestMessage, ValidateJobRequest>(request),
            IppOperation.GetDocumentAttributes => Mapper.Map<IIppRequestMessage, GetDocumentAttributesRequest>(request),
            IppOperation.GetDocuments => Mapper.Map<IIppRequestMessage, GetDocumentsRequest>(request),
            IppOperation.SetDocumentAttributes => Mapper.Map<IIppRequestMessage, SetDocumentAttributesRequest>(request),
            _ => throw new IppRequestException($"Unable to handle {request.IppOperation} operation", request, IppStatusCode.ClientErrorBadRequest)
        };
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(mappedRequest);
    }

    public Task SendRawResponseAsync(
        IIppResponseMessage ippResponseMessage,
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        if (ippResponseMessage is null)
            throw new ArgumentNullException(nameof(ippResponseMessage));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        return _ippProtocol.WriteIppResponseAsync(ippResponseMessage, stream, cancellationToken);
    }

    public async Task SendResponseAsync<T>(
        T ippResponsMessage,
        Stream stream,
        CancellationToken cancellationToken = default) where T : IIppResponse
    {
        var ippResponse = await CreateRawResponseAsync(ippResponsMessage, cancellationToken);
        await _ippProtocol.WriteIppResponseAsync(ippResponse, stream, cancellationToken);
    }

    public Task<IIppResponseMessage> CreateRawResponseAsync<T>(
        T ippResponsMessage,
        CancellationToken cancellationToken = default) where T : IIppResponse
    {
        if (ippResponsMessage is null)
            throw new ArgumentNullException(nameof(ippResponsMessage));
        var ippResponse = Mapper.Map<IppResponseMessage>(ippResponsMessage);
        return Task.FromResult<IIppResponseMessage>(ippResponse);
    }

    private static IMapper MapperFactory()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(TypesProfile));
        mapper.FillFromAssembly(assembly!);
        return mapper;
    }
}
