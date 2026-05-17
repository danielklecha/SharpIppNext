using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class BinaryOctetStringTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task PrintJobAsync_WithBinaryPassword_ShouldRoundTripCorrectly()
    {
        // Arrange
        var binaryPassword = new byte[] { 0x01, 0x02, 0x03, 0x00, 0xFF, 0xFE, 0xFD };
        using var memoryStream = new MemoryStream();
        var clientRequest = new PrintJobRequest
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentPassword = binaryPassword
            }
        };

        var server = new SharpIppServer();
        IIppRequest? serverRequest = null;
        
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var response = new PrintJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(response, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new HttpClient(GetMockOfHttpMessageHandler(func).Object));

        // Act
        await client.PrintJobAsync(clientRequest);

        // Assert
        serverRequest.Should().NotBeNull();
        var printJobRequest = serverRequest.As<PrintJobRequest>();
        printJobRequest!.OperationAttributes!.DocumentPassword.Should().NotBeNull();
        printJobRequest.OperationAttributes.DocumentPassword!.Value.Value.Should().BeEquivalentTo(binaryPassword);
    }

    [TestMethod]
    public async Task CreateJobAsync_WithBinaryPassword_ShouldRoundTripCorrectly()
    {
        // Arrange
        var binaryPassword = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF, 0x00, 0x42 };
        var clientRequest = new CreateJobRequest
        {
            RequestId = 456,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobPassword = binaryPassword
            }
        };

        var server = new SharpIppServer();
        IIppRequest? serverRequest = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var response = new CreateJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(response, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new HttpClient(GetMockOfHttpMessageHandler(func).Object));

        // Act
        await client.CreateJobAsync(clientRequest);

        // Assert
        serverRequest.Should().NotBeNull();
        var createJobRequest = serverRequest.As<CreateJobRequest>();
        createJobRequest!.OperationAttributes!.JobPassword.Should().NotBeNull();
        createJobRequest.OperationAttributes.JobPassword!.Value.Value.Should().BeEquivalentTo(binaryPassword);
    }
}
