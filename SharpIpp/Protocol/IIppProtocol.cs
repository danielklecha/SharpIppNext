using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public interface IIppProtocol
{
    /// <summary>
    /// Controls the behavior of ReadIppRequestAsync() method.
    /// If true, the whole incoming document is read into a memory stream,
    /// and can be accessed via message.Document.
    /// If false, the document is not read into a memory stream, and it should
    /// be consumed from the input stream by the caller.
    /// Defaults to true.
    /// </summary>
    bool ReadDocumentStream { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed size (in bytes) of the IPP document payload when <see cref="ReadDocumentStream"/> is true.
    /// If the document stream exceeds this limit, an <see cref="IppRequestException"/> is thrown 
    /// with the <see cref="IppStatusCode.ClientErrorRequestEntityTooLarge"/> status code.
    /// Defaults to 128 MB. Set to null to disable the size limit.
    /// </summary>
    long? MaxDocumentStreamBytes { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed number of attributes to parse in a single message.
    /// Defaults to 100,000. Set to null to disable the limit.
    /// </summary>
    int? MaxMessageAttributes { get; set; }

    /// <summary>
    /// Reads and parses an IPP request from the specified stream.
    /// </summary>
    /// <param name="stream">The input stream containing the raw IPP request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation. The task result contains the parsed <see cref="IIppRequestMessage"/>.</returns>
    Task<IIppRequestMessage> ReadIppRequestAsync(Stream stream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads and parses an IPP response from the specified stream.
    /// </summary>
    /// <param name="stream">The input stream containing the raw IPP response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation. The task result contains the parsed <see cref="IIppResponseMessage"/>.</returns>
    Task<IIppResponseMessage> ReadIppResponseAsync(Stream stream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes and serializes an IPP request to the specified stream.
    /// </summary>
    /// <param name="ippRequestMessage">The IPP request message to serialize.</param>
    /// <param name="stream">The output stream to write the serialized IPP request to.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    Task WriteIppRequestAsync(IIppRequestMessage ippRequestMessage, Stream stream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes and serializes an IPP response to the specified stream.
    /// </summary>
    /// <param name="message">The IPP response message to serialize.</param>
    /// <param name="stream">The output stream to write the serialized IPP response to.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    Task WriteIppResponseAsync(IIppResponseMessage message, Stream stream, CancellationToken cancellationToken = default);
}
