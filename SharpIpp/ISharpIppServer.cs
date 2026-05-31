using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Validation;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp;

/// <summary>
/// Defines a server interface for receiving IPP requests, parsing them, validating, and sending responses.
/// </summary>
public interface ISharpIppServer
{
    /// <summary>
    /// Validation properties for configuring request and response validation.
    /// </summary>
    #region Properties

    /// <summary>
    /// Gets or sets the validator used to validate raw IPP request messages.
    /// </summary>
    IIppRequestMessageValidator? RequestMessageValidator { get; set; }

    /// <summary>
    /// Gets or sets the validator used to validate typed IPP request objects.
    /// </summary>
    IIppRequestValidator? RequestValidator { get; set; }

    /// <summary>
    /// Gets or sets the validator used to validate raw IPP response messages.
    /// </summary>
    IIppResponseMessageValidator? ResponseMessageValidator { get; set; }

    /// <summary>
    /// Gets or sets the validator used to validate typed IPP response objects.
    /// </summary>
    IIppResponseValidator? ResponseValidator { get; set; }

    #endregion

    /// <summary>
    /// Methods for processing requests and sending responses.
    /// </summary>
    #region Methods

    /// <summary>
    /// Creates a raw IPP response message from a typed IPP response model.
    /// </summary>
    /// <typeparam name="T">The type of the IPP response.</typeparam>
    /// <param name="ippResponsMessage">The typed IPP response model.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the raw IPP response message.</returns>
    Task<IIppResponseMessage> CreateRawResponseAsync<T>(T ippResponsMessage, CancellationToken cancellationToken = default) where T : IIppResponse;

    /// <summary>
    /// Reads and parses a raw IPP request message from the specified stream.
    /// </summary>
    /// <param name="stream">The input stream containing the raw IPP request data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the parsed raw IPP request message.</returns>
    Task<IIppRequestMessage> ReceiveRawRequestAsync(Stream stream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a raw IPP request message and maps it to its corresponding typed IPP request model.
    /// </summary>
    /// <param name="request">The raw IPP request message to map.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the mapped typed IPP request.</returns>
    Task<IIppRequest> ReceiveRequestAsync(IIppRequestMessage request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a raw IPP request from the specified stream, parses it, validates it, and maps it to a typed IPP request model.
    /// </summary>
    /// <param name="stream">The input stream containing the raw IPP request data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the parsed and mapped typed IPP request.</returns>
    Task<IIppRequest> ReceiveRequestAsync(Stream stream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a raw IPP response message and writes it to the specified stream.
    /// </summary>
    /// <param name="ippResponseMessage">The raw IPP response message to write.</param>
    /// <param name="stream">The output stream to write the response data to.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendRawResponseAsync(IIppResponseMessage ippResponseMessage, Stream stream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Maps a typed IPP response model to a raw IPP response message, validates it, and writes it to the specified stream.
    /// </summary>
    /// <typeparam name="T">The type of the IPP response.</typeparam>
    /// <param name="ippResponsMessage">The typed IPP response model.</param>
    /// <param name="stream">The output stream to write the response data to.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendResponseAsync<T>(T ippResponsMessage, Stream stream, CancellationToken cancellationToken = default) where T : IIppResponse;

    #endregion
}
