using SharpIpp.Exceptions;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
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
    private IMapper Mapper => MapperSingleton.Value;

    static SharpIppServer()
    {
        MapperSingleton = new Lazy<IMapper>(MapperFactory);
    }

    public SharpIppServer()
    {
        _ippProtocol = new IppProtocol();
    }

    public SharpIppServer(IIppProtocol ippProtocol)
    {
        _ippProtocol = ippProtocol;
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
        ValidateRawRequest(request);
        return await ReceiveRequestAsync(request);
    }

    public Task<IIppRequest> ReceiveRequestAsync(
        IIppRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        IIppRequest mappedRequest = request.IppOperation switch
        {
            IppOperation.CancelJob => Mapper.Map<IIppRequestMessage, CancelJobRequest>(request),
            IppOperation.CreateJob => Mapper.Map<IIppRequestMessage, CreateJobRequest>(request),
            IppOperation.GetCUPSPrinters => Mapper.Map<IIppRequestMessage, CUPSGetPrintersRequest>(request),
            IppOperation.GetJobAttributes => Mapper.Map<IIppRequestMessage, GetJobAttributesRequest>(request),
            IppOperation.GetJobs => Mapper.Map<IIppRequestMessage, GetJobsRequest>(request),
            IppOperation.GetPrinterAttributes => Mapper.Map<IIppRequestMessage, GetPrinterAttributesRequest>(request),
            IppOperation.HoldJob => Mapper.Map<IIppRequestMessage, HoldJobRequest>(request),
            IppOperation.PausePrinter => Mapper.Map<IIppRequestMessage, PausePrinterRequest>(request),
            IppOperation.PrintJob => Mapper.Map<IIppRequestMessage, PrintJobRequest>(request),
            IppOperation.PrintUri => Mapper.Map<IIppRequestMessage, PrintUriRequest>(request),
            IppOperation.PurgeJobs => Mapper.Map<IIppRequestMessage, PurgeJobsRequest>(request),
            IppOperation.ReleaseJob => Mapper.Map<IIppRequestMessage, ReleaseJobRequest>(request),
            IppOperation.RestartJob => Mapper.Map<IIppRequestMessage, RestartJobRequest>(request),
            IppOperation.ResumePrinter => Mapper.Map<IIppRequestMessage, ResumePrinterRequest>(request),
            IppOperation.SendDocument => Mapper.Map<IIppRequestMessage, SendDocumentRequest>(request),
            IppOperation.SendUri => Mapper.Map<IIppRequestMessage, SendUriRequest>(request),
            IppOperation.ValidateJob => Mapper.Map<IIppRequestMessage, ValidateJobRequest>(request),
            _ => throw new IppRequestException($"Unable to handle {request.IppOperation} operation", request, IppStatusCode.ClientErrorBadRequest)
        };
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(mappedRequest);
    }

    public void ValidateRawRequest(IIppRequestMessage request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (request.RequestId <= 0)
            throw new IppRequestException("Bad request-id value", request, IppStatusCode.ClientErrorBadRequest);
        if (!request.OperationAttributes.Any())
            throw new IppRequestException("No Operation Attributes", request, IppStatusCode.ClientErrorBadRequest);
        if (request.OperationAttributes.First().Name != JobAttribute.AttributesCharset)
            throw new IppRequestException("attributes-charset MUST be the first attribute", request, IppStatusCode.ClientErrorBadRequest);
        if (request.OperationAttributes.Skip(1).FirstOrDefault()?.Name != JobAttribute.AttributesNaturalLanguage)
            throw new IppRequestException("attributes-natural-language MUST be the second attribute", request, IppStatusCode.ClientErrorBadRequest);
        if (request.Version < new IppVersion(1, 0))
            throw new IppRequestException("Unsupported IPP version", request, IppStatusCode.ServerErrorVersionNotSupported);
        if (!request.OperationAttributes.Any(x => x.Name == JobAttribute.PrinterUri))
            throw new IppRequestException("No printer-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);
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
