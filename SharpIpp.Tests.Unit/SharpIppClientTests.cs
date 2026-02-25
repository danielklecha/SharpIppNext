using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.Protected;
using Moq;
using SharpIpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Extensions;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions.Equivalency;
using SharpIpp.Exceptions;
using System.Net.Http;
using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit;

[TestClass]
[ExcludeFromCodeCoverage]
public class SharpIppClientTests
{
    private static Mock<HttpMessageHandler> GetMockOfHttpMessageHandler( HttpStatusCode statusCode = HttpStatusCode.OK )
    {
        Mock<HttpMessageHandler> handlerMock = new( MockBehavior.Strict );
        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync( new HttpResponseMessage()
           {
               StatusCode = statusCode,
               Content = new ByteArrayContent( Array.Empty<byte>() ),
           } )
           .Verifiable();
        return handlerMock;
    }

    private static Mock<IIppProtocol> GetMockOfIppProtocol()
    {
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( new IppResponseMessage
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            JobAttributes = { new List<IppAttribute> { 
                new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/1"),
                new IppAttribute(Tag.Integer, JobAttribute.JobId, 1),
                new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Pending)
            } }
        } );
        return protocol;
    }

    [TestMethod()]
    public async Task CreateJobAsync_OperationAttributes_ShouldThrowException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new(new(GetMockOfHttpMessageHandler().Object), protocol.Object);
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync(new CreateJobRequest
        {
            RequestId = 123,

        });
        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task CreateJobAsync_PrinterUri_ShouldThrowException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new(new(GetMockOfHttpMessageHandler().Object), protocol.Object);
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync(new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes()
        });
        // Assert
        await act.Should().ThrowAsync<Exception>();
    }


    public static IEnumerable<object[]> ClientMappingData
    {
        get
        {
            yield return [
                IppOperation.CancelJob,
                new CancelJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.CancelJobAsync((CancelJobRequest)r)),
                new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 234) },
                "CancelJob"
            ];
            yield return [
                IppOperation.HoldJob,
                new HoldJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.HoldJobAsync((HoldJobRequest)r)),
                new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 234) },
                "HoldJob"
            ];
            yield return [
                IppOperation.ReleaseJob,
                new ReleaseJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.ReleaseJobAsync((ReleaseJobRequest)r)),
                new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 234) },
                "ReleaseJob"
            ];
            yield return [
                IppOperation.RestartJob,
                new RestartJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.RestartJobAsync((RestartJobRequest)r)),
                new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 234) },
                "RestartJob"
            ];
            yield return [
                IppOperation.PausePrinter,
                new PausePrinterRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.PausePrinterAsync((PausePrinterRequest)r)),
                null!,
                "PausePrinter"
            ];
            yield return [
                IppOperation.ResumePrinter,
                new ResumePrinterRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.ResumePrinterAsync((ResumePrinterRequest)r)),
                null!,
                "ResumePrinter"
            ];
            yield return [
                IppOperation.PurgeJobs,
                new PurgeJobsRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.PurgeJobsAsync((PurgeJobsRequest)r)),
                null!,
                "PurgeJobs"
            ];
            yield return [
                IppOperation.GetPrinterAttributes,
                new GetPrinterAttributesRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.GetPrinterAttributesAsync((GetPrinterAttributesRequest)r)),
                null!,
                "GetPrinterAttributes"
            ];
            yield return [
                IppOperation.GetCUPSPrinters,
                new CUPSGetPrintersRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.GetCUPSPrintersAsync((CUPSGetPrintersRequest)r)),
                null!,
                "GetCUPSPrinters"
            ];
            yield return [
                IppOperation.GetJobs,
                new GetJobsRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", MyJobs = true } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.GetJobsAsync((GetJobsRequest)r)),
                new[] { new IppAttribute(Tag.Boolean, JobAttribute.MyJobs, true) },
                "GetJobs"
            ];
            yield return [
                IppOperation.GetJobAttributes,
                new GetJobAttributesRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.GetJobAttributesAsync((GetJobAttributesRequest)r)),
                new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 234) },
                "GetJobAttributes"
            ];
            yield return [
                IppOperation.CreateJob,
                new CreateJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.CreateJobAsync((CreateJobRequest)r)),
                null!,
                "CreateJob"
            ];
            yield return [
                IppOperation.PrintJob,
                new PrintJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" }, Document = new MemoryStream() },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.PrintJobAsync((PrintJobRequest)r)),
                null!,
                "PrintJob"
            ];
            yield return [
                IppOperation.ValidateJob,
                new ValidateJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" }, Document = new MemoryStream() },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.ValidateJobAsync((ValidateJobRequest)r)),
                null!,
                "ValidateJob"
            ];
            yield return [
                IppOperation.PrintUri,
                new PrintUriRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", DocumentUri = new Uri("http://test.com/document.pdf") } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.PrintUriAsync((PrintUriRequest)r)),
                new[] { new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, "http://test.com/document.pdf") },
                "PrintUri"
            ];
            yield return [
                IppOperation.SendDocument,
                new SendDocumentRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 456 }, Document = new MemoryStream() },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.SendDocumentAsync((SendDocumentRequest)r)),
                new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 456), new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false) },
                "SendDocument"
            ];
            yield return [
                IppOperation.SendUri,
                new SendUriRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 456, DocumentUri = new Uri("http://test.com/document.pdf") } },
                new Func<SharpIppClient, object, Task>(async (c, r) => await c.SendUriAsync((SendUriRequest)r)),
                new[] { new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, "http://test.com/document.pdf"), new IppAttribute(Tag.Integer, JobAttribute.JobId, 456), new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false) },
                "SendUri"
            ];
        }
    }

    [TestMethod]
    [DynamicData(nameof(ClientMappingData))]
    public async Task Request_ShouldBeMapped(IppOperation operation, object request, Func<SharpIppClient, object, Task> act, IppAttribute[] additionalAttributes, string description)
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new(new(GetMockOfHttpMessageHandler().Object), protocol.Object);
        Stream? document = request switch
        {
            PrintJobRequest r => r.Document,
            ValidateJobRequest r => r.Document,
            SendDocumentRequest r => r.Document,
            _ => null
        };

        // Act
        await act(client, request);

        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = operation,
            RequestId = 123,
            Document = document
        };
        rawRequestMessage.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/"),
        });
        if (additionalAttributes != null)
        {
            rawRequestMessage.OperationAttributes.AddRange(additionalAttributes);
        }
        protocol.Verify(x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>(x => x.VerifyAssertionScope(_ => x.Should().BeEquivalentTo(rawRequestMessage, options => options.Excluding((IMemberInfo m) => m.Path == "Document.ReadTimeout" || m.Path == "Document.WriteTimeout"), ""))),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>()));
    }




    [TestMethod()]
    public async Task CreateJobAsync_ResponseWithValidIppCodeAndInvalidHttpState_ShouldThrowException()
    {
        // Arrange
        HttpClient httpClient = new( GetMockOfHttpMessageHandler( HttpStatusCode.BadRequest ).Object );
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( new IppResponseMessage
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            JobAttributes = { new List<IppAttribute> { 
                new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/1"),
                new IppAttribute(Tag.Integer, JobAttribute.JobId, 1),
                new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Pending)
            } }
        } );
        SharpIppClient client = new( httpClient, protocol.Object );
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync( new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        await act.Should().ThrowAsync<HttpRequestException>();
    }

    [TestMethod()]
    public async Task CreateJobAsync_ResponseWithPlausibleHttpStateAndValidData_ShouldThrowException()
    {
        // Arrange
        HttpClient httpClient = new( GetMockOfHttpMessageHandler( HttpStatusCode.Unauthorized ).Object );
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( new IppResponseMessage
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            JobAttributes = { new List<IppAttribute> { 
                new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/1"),
                new IppAttribute(Tag.Integer, JobAttribute.JobId, 1),
                new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Pending)
            } }
        } );
        SharpIppClient client = new( httpClient, protocol.Object );
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync( new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        await act.Should().ThrowAsync<IppResponseException>();
    }

    [TestMethod()]
    public async Task CreateJobAsync_ResponseWithValidHttpStateAndInvalidIppCode_ShouldThrowException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( new IppResponseMessage
        {
            RequestId = 123,
            StatusCode = IppStatusCode.ServerErrorBusy,
            JobAttributes = { new List<IppAttribute> { 
                new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/1"),
                new IppAttribute(Tag.Integer, JobAttribute.JobId, 1),
                new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Pending)
            } }
        } );
        SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync( new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes= new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        await act.Should().ThrowAsync<IppResponseException>();
    }

    [TestMethod()]
    public async Task CreateJobAsync_ResponseWithValidHttpStateAndInvalidData_ShouldThrowException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ThrowsAsync( new InvalidCastException() );
        SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync( new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        await act.Should().ThrowAsync<InvalidCastException>();
    }

    [TestMethod()]
    public async Task CreateJobAsync_ResponseWithPlausibleHttpStateAndInvalidData_ShouldThrowException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ThrowsAsync( new InvalidCastException() );
        SharpIppClient client = new( new( GetMockOfHttpMessageHandler( HttpStatusCode.Unauthorized ).Object ), protocol.Object );
        // Act
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync( new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        await act.Should().ThrowAsync<HttpRequestException>();
    }

    [TestMethod]
    public void Constructor_Default_InstanceShouldBeCreated()
    {
        // Arrange & Act
        using SharpIppClient client = new();
        // Assert
        client.Should().NotBeNull();
    }

    [TestMethod]
    public void Constructor_HttpClient_InstanceShouldBeCreated()
    {
        // Arrange & Act
        using SharpIppClient client = new( new HttpClient() );
        // Assert
        client.Should().NotBeNull();
    }

    [TestMethod]
    public void Constructor_HttpClientAndIppProtocol_InstanceShouldBeCreated()
    {
        // Arrange & Act
        using SharpIppClient client = new( new HttpClient(), new IppProtocol() );
        // Assert
        client.Should().NotBeNull();
    }

    [TestMethod()]
    public void Construct_InvalidData_ShouldReturnDefault()
    {
        // Arrange
        using SharpIppClient client = new();
        var message = new Mock<IIppResponseMessage>();
        //Act
        var result = client.CreateResponse<IppResponseMessage>( message.Object );
        // Assert
        result.Should().NotBeNull();
    }

    [TestMethod()]
    public async Task CreateJobAsync_Null_ShouldThrowException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<CreateJobResponse>> act = async () => await client.CreateJobAsync( null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    [DataRow("http://127.0.0.1:631", "http://127.0.0.1:631")]
    [DataRow("https://127.0.0.1:631", "https://127.0.0.1:631")]
    [DataRow("ipp://127.0.0.1:631", "http://127.0.0.1:631")]
    [DataRow("ipps://127.0.0.1:631", "https://127.0.0.1:631")]
    [DataRow("http://127.0.0.1", "http://127.0.0.1:80")]
    [DataRow("https://127.0.0.1", "https://127.0.0.1:443")]
    [DataRow("ipp://127.0.0.1", "http://127.0.0.1:631")]
    [DataRow("ipps://127.0.0.1", "https://127.0.0.1:631")]
    [DataRow("ipp://127.0.0.1:631/myPrinter", "http://127.0.0.1:631/myPrinter")]
    [DataRow("ipp://127.0.0.1:631/?myVariable=true", "http://127.0.0.1:631/?myVariable=true")]
    public async Task CreateJobAsync_PrinterUri_ShouldBeUpdated(string printerUri, string expected )
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        Mock<HttpMessageHandler> messageHandler = GetMockOfHttpMessageHandler();
        using SharpIppClient client = new( new( messageHandler.Object ), protocol.Object );
        // Act
        await client.CreateJobAsync( new CreateJobRequest
        {
            RequestId = 123,
            OperationAttributes = new()
            {
                PrinterUri = new Uri(printerUri),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        messageHandler
            .Protected()
            .Verify<Task<HttpResponseMessage>>(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(x => x.VerifyAssertionScope(_ => x.RequestUri.Should().BeEquivalentTo(new Uri(expected), "" ))),
                ItExpr.IsAny<CancellationToken>() );
    }

    [TestMethod]
    [DataRow("RawIppResponses/GetPrinterAttributes_Canon_MX490_series_low_supply.bin", typeof(GetPrinterAttributesResponse))]
    [DataRow("RawIppResponses/GetPrinterAttributes_HP_Color_LaserJet_MFP_M476dn.bin", typeof(GetPrinterAttributesResponse))]
    [DataRow("RawIppResponses/PrintJob_HP_Color_LaserJet_MFP_M476dn.bin", typeof(PrintJobResponse))]
    [DataRow("RawIppResponses/GetJobAttributes_HP_Color_LaserJet_MFP_M476dn.bin", typeof(GetJobAttributesResponse))]
    public async Task Construct_ReadBinFile_ShouldBeMapped(string path, Type responseType)
    {
        var protocol = new IppProtocol();
        await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var ippResponse = await protocol.ReadIppResponseAsync(stream);
        using SharpIppClient client = new(new HttpClient(), protocol);
        var mapped = client.CreateResponse(responseType, ippResponse);
        mapped.Should().NotBeNull();
    }
    [TestMethod()]
    public async Task GetPrinterAttributesAsync_MappingException_ShouldThrowIppResponseException()
    {
        // Arrange
        Mock<IIppProtocol> protocol = new();
        protocol.Setup( x => x.ReadIppResponseAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( new IppResponseMessage
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            PrinterAttributes = { new List<IppAttribute> { 
                // PrinterState is expected to be int, but we provide a string
                new IppAttribute(Tag.Charset, PrinterAttribute.PrinterState, "invalid-type-should-be-int")
            } }
        } );
        using SharpIppClient client = new(new(GetMockOfHttpMessageHandler().Object), protocol.Object);
        
        // Act
        Func<Task<GetPrinterAttributesResponse>> act = async () => await client.GetPrinterAttributesAsync(new GetPrinterAttributesRequest
        {
            RequestId = 123,
            OperationAttributes = new GetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("http://127.0.0.1:631")
            }
        });

        // Assert
        await act.Should().ThrowAsync<IppResponseException>().WithMessage("Ipp attributes mapping exception");
    }
}
