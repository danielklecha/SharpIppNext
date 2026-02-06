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
using SharpIpp.Mapping.Profiles;
using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp;

public partial class SharpIppClient : ISharpIppClient
{
    private static readonly Lazy<IMapper> MapperSingleton;

    private readonly bool _disposeHttpClient;
    private readonly HttpClient _httpClient;
    private readonly IIppProtocol _ippProtocol;

    static SharpIppClient()
    {
        MapperSingleton = new Lazy<IMapper>(MapperFactory);
    }

    public SharpIppClient() : this(new HttpClient(), new IppProtocol(), true)
    {
    }

    public SharpIppClient(HttpClient httpClient) : this(httpClient, new IppProtocol(), false )
    {
    }

    public SharpIppClient(HttpClient httpClient, IIppProtocol ippProtocol) : this( httpClient, ippProtocol, false )
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
    ///     Status codes of <see cref="HttpResponseMessage" /> that are not successful,
    ///     but response still contains valid ipp-data in the body that can be parsed for better error description
    ///     Seems like they are printer specific
    /// </summary>
    private static readonly HttpStatusCode[] _plausibleHttpStatusCodes = [
        HttpStatusCode.Continue,
        HttpStatusCode.Unauthorized,
        HttpStatusCode.Forbidden,
        HttpStatusCode.UpgradeRequired,
    ];

    /// <inheritdoc />
    public async Task<IIppResponseMessage> SendAsync(
        Uri printer,
        IIppRequestMessage ippRequest,
        CancellationToken cancellationToken = default)
    {
        var httpRequest = GetHttpRequestMessage( printer );

        HttpResponseMessage? response;

        using (Stream stream = new MemoryStream())
        {
            await _ippProtocol.WriteIppRequestAsync(ippRequest, stream, cancellationToken).ConfigureAwait(false);
            stream.Seek(0, SeekOrigin.Begin);
            httpRequest.Content = new StreamContent(stream) { Headers = { { "Content-Type", "application/ipp" } } };
            response = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        }

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
        where TOut : IIppResponseMessage
    {
        var ippRequest = CreateRawRequest(data);
        if (data.OperationAttributes == null || data.OperationAttributes.PrinterUri == null)
            throw new Exception("PrinterUri is not set");
        var ippResponse = await SendAsync(data.OperationAttributes.PrinterUri, ippRequest, cancellationToken).ConfigureAwait(false);
        var res = CreateResponse<TOut>(ippResponse);
        return res;
    }

    public IIppRequestMessage CreateRawRequest<T>(T ippRequestMessage) where T : IIppRequest
    {
        if (ippRequestMessage is null)
            throw new ArgumentNullException(nameof(ippRequestMessage));
        var ippRequest = Mapper.Map<T, IppRequestMessage>(ippRequestMessage);
        return ippRequest;
    }

    public virtual T CreateResponse<T>(IIppResponseMessage ippResponse) where T : IIppResponseMessage
    {
        try
        {
            var r = Mapper.Map<T>(ippResponse);
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

    private static IMapper MapperFactory()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(TypesProfile));
        mapper.FillFromAssembly(assembly!);
        return mapper;
    }
}
