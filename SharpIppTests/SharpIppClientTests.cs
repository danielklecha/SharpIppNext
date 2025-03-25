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
using SharpIppTests.Extensions;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions.Equivalency;
using SharpIpp.Exceptions;
using System.Net.Http;
using SharpIpp.Models;

namespace SharpIpp.Tests;

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
            Version = IppVersion.V1_1,
            StatusCode = IppStatusCode.SuccessfulOk
        } );
        return protocol;
    }

    [TestMethod()]
    public async Task CreateJobAsync_CreateJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.CreateJobAsync( new Models.CreateJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task CancelJobAsync_CancelJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.CancelJobAsync( new Models.CancelJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 234
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.CancelJob,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 234 )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task HoldJobAsync_HoldJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.HoldJobAsync( new Models.HoldJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 234
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.HoldJob,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 234 )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task PausePrinterAsync_PausePrinterRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.PausePrinterAsync( new Models.PausePrinterRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.PausePrinter,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task ReleaseJobAsync_ReleaseJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.ReleaseJobAsync( new Models.ReleaseJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 234
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.ReleaseJob,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 234 )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task PrintJobAsync_PrintJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        using MemoryStream memoryStream = new();
        // Act
        await client.PrintJobAsync( new Models.PrintJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            },
            Document = memoryStream
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
            Version = IppVersion.V1_1,
            Document = memoryStream
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, x => x.Excluding( ( IMemberInfo x ) => x.Path == "Document.ReadTimeout" || x.Path == "Document.WriteTimeout" ), "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task ResumePrinterAsync_ResumePrinterRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.ResumePrinterAsync( new Models.ResumePrinterRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.ResumePrinter,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task ValidateJobAsync_ValidateJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        using MemoryStream memoryStream = new();
        // Act
        await client.ValidateJobAsync( new Models.ValidateJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            },
            Document = memoryStream
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.ValidateJob,
            RequestId = 123,
            Version = IppVersion.V1_1,
            Document = memoryStream
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task PrintUriAsync_PrintUriRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        var uri = new Uri( "http://test.com/document.pdf" );
        // Act
        await client.PrintUriAsync( new Models.PrintUriRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new PrintUriOperationAttributes
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                DocumentUri = uri
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.PrintUri,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Uri, JobAttribute.DocumentUri, uri.AbsoluteUri )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task SendDocumentAsync_SendDocumentRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        using MemoryStream memoryStream = new();
        // Act
        await client.SendDocumentAsync( new Models.SendDocumentRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 456
            },
            Document = memoryStream
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.SendDocument,
            RequestId = 123,
            Version = IppVersion.V1_1,
            Document = memoryStream
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 456 ),
            new IppAttribute( Tag.Boolean, JobAttribute.LastDocument, false )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task SendUriAsync_SendUriRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        var uri = new Uri( "http://test.com/document.pdf" );
        // Act
        await client.SendUriAsync( new Models.SendUriRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 456,
                DocumentUri = uri
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.SendUri,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Uri, JobAttribute.DocumentUri, uri.AbsoluteUri ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 456 ),
            new IppAttribute( Tag.Boolean, JobAttribute.LastDocument, false )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task GetJobAttributesAsync_GetJobAttributesRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.GetJobAttributesAsync( new Models.GetJobAttributesRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 456
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.GetJobAttributes,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 456 )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task GetJobsAsync_GetJobsRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.GetJobsAsync( new Models.GetJobsRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                MyJobs = true
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.GetJobs,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Boolean, JobAttribute.MyJobs, true )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task GetPrinterAttributesAsync_GetPrinterAttributesRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.GetPrinterAttributesAsync( new Models.GetPrinterAttributesRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.GetPrinterAttributes,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task PurgeJobsAsync_PurgeJobsRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.PurgeJobsAsync( new Models.PurgeJobsRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.PurgeJobs,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task RestartJobAsync_RestartJobRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.RestartJobAsync( new Models.RestartJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                JobId = 456
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.RestartJob,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, JobAttribute.JobId, 456 )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task GetCUPSPrintersAsync_CUPSGetPrintersRequest_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> protocol = GetMockOfIppProtocol();
        using SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        await client.GetCUPSPrintersAsync( new Models.CUPSGetPrintersRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user"
            }
        } );
        // Assert
        IppRequestMessage rawRequestMessage = new()
        {
            IppOperation = IppOperation.GetCUPSPrinters,
            RequestId = 123,
            Version = IppVersion.V1_1
        };
        rawRequestMessage.OperationAttributes.AddRange( new[]
        {
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "test-user" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "http://127.0.0.1:631/" )
        } );
        protocol.Verify( x => x.WriteIppRequestAsync(
            It.Is<IppRequestMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawRequestMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
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
            Version = IppVersion.V1_1,
            StatusCode = IppStatusCode.SuccessfulOk
        } );
        SharpIppClient client = new( httpClient, protocol.Object );
        // Act
        Func<Task<Models.CreateJobResponse>> act = async () => await client.CreateJobAsync( new Models.CreateJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
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
            Version = IppVersion.V1_1,
            StatusCode = IppStatusCode.SuccessfulOk
        } );
        SharpIppClient client = new( httpClient, protocol.Object );
        // Act
        Func<Task<Models.CreateJobResponse>> act = async () => await client.CreateJobAsync( new Models.CreateJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
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
            Version = IppVersion.V1_1,
            StatusCode = IppStatusCode.ServerErrorBusy
        } );
        SharpIppClient client = new( new( GetMockOfHttpMessageHandler().Object ), protocol.Object );
        // Act
        Func<Task<Models.CreateJobResponse>> act = async () => await client.CreateJobAsync( new Models.CreateJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
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
            Version = IppVersion.V1_1,
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
            Version = IppVersion.V1_1,
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
    public void Construct_InvalidData_ShouldThrowException()
    {
        // Arrange
        using SharpIppClient client = new();
        var message = new Mock<IIppResponseMessage>();
        //Act
        Func<IppResponseMessage> act = () => client.Construct<IppResponseMessage>( message.Object );
        // Assert
        act.Should().Throw<IppResponseException>();
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

    [DataTestMethod]
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
        await client.CreateJobAsync( new Models.CreateJobRequest
        {
            RequestId = 123,
            Version = IppVersion.V1_1,
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

    [DataTestMethod]
    [DataRow("RawIppResponses/GetPrinterAttributes_Canon_MX490_series_low_supply.bin")]
    [DataRow("RawIppResponses/GetPrinterAttributes_HP_Color_LaserJet_MFP_M476dn.bin")]
    public async Task Construct_ReadBinFileWithGetPrinterAttributes_ShouldBeMapped(string path)
    {
        var protocol = new IppProtocol();
        await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var ippResponse = await protocol.ReadIppResponseAsync(stream);
        using SharpIppClient client = new(new HttpClient(), protocol);
        var mapped = client.Construct<GetPrinterAttributesResponse>(ippResponse);
        Assert.IsNotNull(mapped);
    }

    [DataTestMethod]
    [DataRow("RawIppResponses/PrintJob_HP_Color_LaserJet_MFP_M476dn.bin")]
    public async Task Construct_ReadBinFileWithPrintJob_ShouldBeMapped(string path)
    {
        var protocol = new IppProtocol();
        await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var ippResponse = await protocol.ReadIppResponseAsync(stream);
        using SharpIppClient client = new(new HttpClient(), protocol);
        var mapped = client.Construct<PrintJobResponse>(ippResponse);
        Assert.IsNotNull(mapped);
    }

    [DataTestMethod]
    [DataRow("RawIppResponses/GetJobAttributes_HP_Color_LaserJet_MFP_M476dn.bin")]
    public async Task Construct_ReadBinFileWithGetJobAttributes_ShouldBeMapped(string path)
    {
        var protocol = new IppProtocol();
        await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var ippResponse = await protocol.ReadIppResponseAsync(stream);
        using SharpIppClient client = new(new HttpClient(), protocol);
        var mapped = client.Construct<GetJobAttributesResponse>(ippResponse);
        Assert.IsNotNull(mapped);
    }
}