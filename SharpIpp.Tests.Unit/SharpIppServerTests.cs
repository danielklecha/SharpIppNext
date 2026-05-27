using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using SharpIpp.Validation;
using SharpIpp;
using Moq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using SharpIpp.Tests.Unit.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;

namespace SharpIpp.Tests.Unit;

[TestClass]
[ExcludeFromCodeCoverage]
public class SharpIppServerTests
{
    [TestMethod]
    public void Constructor_Default_InstanceShouldBeCreated()
    {
        // Arrange & Act
        SharpIppServer server = new();
        // Assert
        server.Should().NotBeNull();
    }

    [TestMethod]
    public void Constructor_WithNullRequestValidator_ShouldNotThrow()
    {
        Action act = () => _ = new SharpIppServer(Mock.Of<IIppProtocol>(), null!);

        act.Should().NotThrow();
    }

    [TestMethod]
    public async Task ReceiveRequestAsync_WithNullRequestValidator_ShouldNotThrowNullReferenceException()
    {
        SharpIppServer server = new SharpIppServer(Mock.Of<IIppProtocol>(), null!);
        IppRequestMessage ippRequestMessage = new()
        {
            IppOperation = IppOperation.Reserved1,
            RequestId = 123,
        };

        Func<Task<IIppRequest>> act = async () => await server.ReceiveRequestAsync(ippRequestMessage);
        var exception = await act.Should().ThrowAsync<IppRequestException>();
        exception.Which.Message.Should().Be("Unable to handle Reserved1 operation");
    }


    [TestMethod]
    public async Task ReceiveRequestAsync_UnsupportedOperation_ShouldThrowError()
    {
        // Arrange
        SharpIppServer server = new( Mock.Of<IIppProtocol>() );
        IppRequestMessage ippRequestMessage = new()
        {
            IppOperation = IppOperation.Reserved1,
            RequestId = 123,
        };
        // Act
        Func<Task<IIppRequest>> act = async () => await server.ReceiveRequestAsync( ippRequestMessage );
        // Assert
        await act.Should().ThrowAsync<IppRequestException>();
    }

    [TestMethod]
    public async Task ReceiveRequestAsync_UnknownOperation_ShouldThrowBadRequestFromSwitchDefault()
    {
        // Arrange
        Mock<IIppRequestMessageValidator> validator = new();
        var context = new IppRequestValidationContext();
        validator.SetupGet(x => x.Context).Returns(context);

        var server = new SharpIppServer(Mock.Of<IIppProtocol>(), validator.Object);
        var request = new IppRequestMessage
        {
            IppOperation = (IppOperation)0x7FFF,
            RequestId = 123,
        };

        // Act
        Func<Task<IIppRequest>> act = async () => await server.ReceiveRequestAsync(request);

        // Assert
        var exception = await act.Should().ThrowAsync<IppRequestException>();
        exception.Which.Message.Should().Be($"Unable to handle {request.IppOperation} operation");
        exception.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        exception.Which.RequestMessage.Should().Be(request);
    }

    [TestMethod]
    public async Task ReceiveRequestAsync_MessageIsNull_ShouldThrowError()
    {
        // Arrange
        SharpIppServer server = new( Mock.Of<IIppProtocol>() );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<IIppRequest>> act = async () => await server.ReceiveRequestAsync( request: null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task ReceiveRequestAsync_MessageViolatesOperationRules_ShouldThrowError()
    {
        // Arrange
        SharpIppServer server = new(Mock.Of<IIppProtocol>());
        IppRequestMessage ippRequestMessage = new()
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
        };
        ippRequestMessage.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        // Act
        Func<Task<IIppRequest>> act = async () => await server.ReceiveRequestAsync(ippRequestMessage);

        // Assert
        await act.Should().ThrowAsync<IppRequestException>().WithMessage("document stream required");
    }

    [TestMethod]
    public async Task ReceiveRequestAsync_WithInjectedRequestValidator_ShouldInvokeValidator()
    {
        Mock<IIppRequestMessageValidator> validator = new();
        var context = new IppRequestValidationContext();
        validator.SetupGet(x => x.Context).Returns(context);

        var server = new SharpIppServer(Mock.Of<IIppProtocol>(), validator.Object);
        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        var result = await server.ReceiveRequestAsync(request);

        result.Should().NotBeNull();
        validator.Verify(x => x.Validate(It.Is<IIppRequestMessage>(m => m == request)), Times.Once);
        context.Source.Should().Be(nameof(SharpIppServer));
    }

    [TestMethod()]
    public async Task ReceiveRawRequestAsync_Stream_ShouldReturnMessage()
    {
        // Arrange
        Mock<IIppProtocol> protocol = new();
        IppRequestMessage message = new();
        protocol.Setup( x => x.ReadIppRequestAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( message );
        SharpIppServer server = new( protocol.Object );
        // Act
        Func<Task<IIppRequestMessage>> act = async () => await server.ReceiveRawRequestAsync( Stream.Null );
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().Be( message );
    }

    [TestMethod()]
    public async Task ReceiveRawRequestAsync_Null_ShouldThrowException()
    {
        // Arrange
        SharpIppServer server = new( Mock.Of<IIppProtocol>() );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<IIppRequestMessage>> act = async () => await server.ReceiveRawRequestAsync( null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }


    [TestMethod()]
    public void SendRawResponseAsync_MessageIsNull_ShouldThrowException()
    {
        // Arrange
        SharpIppServer server = new( Mock.Of<IIppProtocol>() );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => server.SendRawResponseAsync( null, Stream.Null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod()]
    public void SendRawResponseAsync_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        SharpIppServer server = new( Mock.Of<IIppProtocol>() );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => server.SendRawResponseAsync( new IppResponseMessage(), null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod()]
    public void SendRawResponseAsync_Data_ShouldBeSuccess()
    {
        // Arrange
        SharpIppServer server = new( Mock.Of<IIppProtocol>() );
        var response = new IppResponseMessage
        {
            RequestId = 1,
            Version = new IppVersion(1, 1)
        };
        response.OperationAttributes.AddRange([
            new IppAttribute(Tag.Charset, "attributes-charset", "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en")
        ]);
        // Act
        Action act = () => server.SendRawResponseAsync( response, Stream.Null );
        // Assert
        act.Should().NotThrow();
    }

    [TestMethod]
    public async Task ReceiveRequestAsync_Stream_ShouldBeSuccess()
    {
        // Arrange
        Mock<IIppProtocol> ippProtocol = new();
        IppRequestMessage ippRequestMessage = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        ippRequestMessage.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user" )
        ] );
        ippProtocol.Setup( x => x.ReadIppRequestAsync( It.IsAny<Stream>(), It.IsAny<CancellationToken>() ) ).ReturnsAsync( ippRequestMessage );
        SharpIppServer server = new( ippProtocol.Object );
        // Act
        Func<Task<IIppRequest>> act = async () => await server.ReceiveRequestAsync( Stream.Null );
        // Assert
        await act.Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task SendResponseAsync_MesageIsNull_ShouldThrowException()
    {
        // Arrange
        SharpIppServer server = new(Mock.Of<IIppProtocol>());
        // Act
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
        Func<Task> act = async () => await server.SendResponseAsync((IIppResponseMessage)null, Stream.Null);
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task SendResponseAsync_CreateJobResponse_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> ippProtocol = new();
        SharpIppServer server = new( ippProtocol.Object );
        var message = new CreateJobResponse
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            JobAttributes = new()
            {
                JobId = 234,
                JobUri = new Uri("http://127.0.0.1:631/234"),
                JobState = JobState.Pending,
                JobStateMessage = "custom state",
                NumberOfInterveningJobs = 0,
                JobStateReasons = [JobStateReason.None]
            }
        };
        var rawMessage = new IppResponseMessage
        {
            RequestId = 123
        };
        var operationAttrs = new List<IppAttribute>
        {
            new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en" )
        };
        rawMessage.OperationAttributes.Add( operationAttrs );
        var jobAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, "http://127.0.0.1:631/234"),
            new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 234),
            new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)JobState.Pending),
            new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobStateMessage, "custom state"),
            new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfInterveningJobs, 0),
            new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, "none")
        };
        rawMessage.JobAttributes.Add(jobAttrs);
        // Act
        await server.SendResponseAsync( message, Stream.Null );
        // Assert
        ippProtocol.Verify( x => x.WriteIppResponseAsync(
            It.Is<IppResponseMessage>( x => x.VerifyAssertionScope( _ => x.Should().BeEquivalentTo( rawMessage, "" ) ) ),
            It.IsAny<Stream>(),
            It.IsAny<CancellationToken>() ) );
    }

    [TestMethod()]
    public async Task CreateRawResponseAsync_CreateJobResponse_ShouldBeMapped()
    {
        // Arrange
        Mock<IIppProtocol> ippProtocol = new();
        SharpIppServer server = new(ippProtocol.Object);
        var message = new CreateJobResponse
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            JobAttributes = new()
            {
                JobId = 234,
                JobUri = new Uri("http://127.0.0.1:631/234"),
                JobState = JobState.Pending,
                JobStateMessage = "custom state",
                NumberOfInterveningJobs = 0,
                JobStateReasons = [JobStateReason.None]
            }
        };
        var rawMessage = new IppResponseMessage
        {
            RequestId = 123
        };
        var operationAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")
        };
        rawMessage.OperationAttributes.Add(operationAttrs);
        var jobAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, "http://127.0.0.1:631/234"),
            new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 234),
            new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)JobState.Pending),
            new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobStateMessage, "custom state"),
            new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfInterveningJobs, 0),
            new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, "none")
        };
        rawMessage.JobAttributes.Add(jobAttrs);
        // Act
        Func<Task<IIppResponseMessage>> act = () => server.CreateRawResponseAsync(message);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo(rawMessage);
    }

    public static IEnumerable<object[]> ReceiveRequestData
    {
        get
        {
            yield return [
                IppOperation.CancelJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123),
                    new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user")
                },
                new List<List<IppAttribute>>(),
                null!,
                new CancelJobRequest { RequestId = 123, OperationAttributes = new() { JobId = 123, PrinterUri = new Uri("ipp://127.0.0.1:631/"), RequestingUserName = "test-user" } },
                "CancelJob"
            ];
            yield return [
                IppOperation.CreateJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123),
                    new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user")
                },
                new List<List<IppAttribute>> { new() {
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobPriority, 99 ),
                    new IppAttribute( Tag.Keyword, IppAttributeNames.JobHoldUntil, "day-time"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 2 ),
                    new IppAttribute( Tag.Keyword, IppAttributeNames.Finishings, 4),
                    new IppAttribute( Tag.Keyword, IppAttributeNames.Sides, "two-sided-long-edge"),
                    new IppAttribute( Tag.Keyword, IppAttributeNames.OrientationRequested, 4)
                } },
                null!,
                new CreateJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("ipp://127.0.0.1:631/"), RequestingUserName = "test-user" }, JobTemplateAttributes = new() { JobPriority = 99, JobHoldUntil = JobHoldUntil.DayTime, Copies = 2, Finishings = new[] { Finishings.Staple }, Sides = Sides.TwoSidedLongEdge, OrientationRequested = Orientation.Landscape } },
                "CreateJob with attributes"
            ];
            yield return [
                IppOperation.GetCUPSPrinters,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                null!,
                new CUPSGetPrintersRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                "GetCUPSPrinters"
            ];
            yield return [
                IppOperation.GetJobAttributes,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 456)
                },
                new List<List<IppAttribute>>(),
                null!,
                new GetJobAttributesRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 456 } },
                "GetJobAttributes"
            ];
            yield return [
                IppOperation.GetJobs,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                null!,
                new GetJobsRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                "GetJobs"
            ];
            yield return [
                IppOperation.GetPrinterAttributes,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                null!,
                new GetPrinterAttributesRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                "GetPrinterAttributes"
            ];
            yield return [
                IppOperation.HoldJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 234)
                },
                new List<List<IppAttribute>>(),
                null!,
                new HoldJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                "HoldJob"
            ];
            yield return [
                IppOperation.PausePrinter,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                null!,
                new PausePrinterRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                "PausePrinter"
            ];
            var stream = new MemoryStream();
            yield return [
                IppOperation.PrintJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                stream,
                new PrintJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" }, Document = stream, JobTemplateAttributes = new() },
                "PrintJob"
            ];
            yield return [
                IppOperation.PrintUri,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.DocumentUri, "http://test.com/document.pdf")
                },
                new List<List<IppAttribute>>(),
                null!,
                new PrintUriRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", DocumentUri = new Uri("http://test.com/document.pdf") }, JobTemplateAttributes = new() },
                "PrintUri"
            ];
            yield return [
                IppOperation.PurgeJobs,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                null!,
                new PurgeJobsRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                "PurgeJobs"
            ];
            yield return [
                IppOperation.ReleaseJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 234)
                },
                new List<List<IppAttribute>>(),
                null!,
                new ReleaseJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 234 } },
                "ReleaseJob"
            ];
            yield return [
                IppOperation.RestartJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 456)
                },
                new List<List<IppAttribute>>(),
                null!,
                new RestartJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 456 } },
                "RestartJob"
            ];
            yield return [
                IppOperation.ResumePrinter,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                null!,
                new ResumePrinterRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" } },
                "ResumePrinter"
            ];
            var stream2 = new MemoryStream();
            yield return [
                IppOperation.SendDocument,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 456),
                    new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, false)
                },
                new List<List<IppAttribute>>(),
                stream2,
                new SendDocumentRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 456 }, Document = stream2 },
                "SendDocument"
            ];
            yield return [
                IppOperation.SendUri,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.DocumentUri, "http://test.com/document.pdf"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 456),
                    new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, false)
                },
                new List<List<IppAttribute>>(),
                null!,
                new SendUriRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user", JobId = 456, DocumentUri = new Uri("http://test.com/document.pdf") } },
                "SendUri"
            ];
            var stream3 = new MemoryStream();
            yield return [
                IppOperation.ValidateJob,
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
                    new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "test-user"),
                    new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "http://127.0.0.1:631/")
                },
                new List<List<IppAttribute>>(),
                stream3,
                new ValidateJobRequest { RequestId = 123, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestingUserName = "test-user" }, JobTemplateAttributes = new() },
                "ValidateJob"
            ];
        }
    }

    [TestMethod]
    [DynamicData(nameof(ReceiveRequestData))]
    public async Task ReceiveRequestAsync_ShouldBeMapped(IppOperation operation, List<IppAttribute> operationAttributes, List<List<IppAttribute>> jobAttributes, Stream document, IIppRequest expected, string description)
    {
        // Arrange
        SharpIppServer server = new(Mock.Of<IIppProtocol>());
        IppRequestMessage ippRequestMessage = new()
        {
            IppOperation = operation,
            RequestId = 123,
            Document = document
        };
        ippRequestMessage.OperationAttributes.AddRange(operationAttributes);
        foreach (var jobAttrList in jobAttributes)
            ippRequestMessage.JobAttributes.AddRange(jobAttrList);

        // Act
        var result = await server.ReceiveRequestAsync(ippRequestMessage);

        // Assert
        result.Should().BeEquivalentTo(expected, description);
    }

    [TestMethod]
    public async Task CreateRawResponseAsync_WithDefaultResponseValidator_WhenResponseIsInvalid_ShouldThrowValidationException()
    {
        // Default response validator is IppResponseValidator.Default on the server
        SharpIppServer server = new(Mock.Of<IIppProtocol>());
        var message = new CreateJobResponse
        {
            RequestId = 0, // Invalid: RequestId should be 1 or greater
            StatusCode = IppStatusCode.SuccessfulOk
        };

        Func<Task<IIppResponseMessage>> act = () => server.CreateRawResponseAsync(message);
        await act.Should().ThrowAsync<ValidationException>().WithMessage("*RequestId*");
    }

    [TestMethod]
    public async Task CreateRawResponseAsync_WithNullResponseValidator_WhenResponseIsInvalid_ShouldNotThrow()
    {
        // Disable response validator and response message validator on the server by passing null
        SharpIppServer server = new(Mock.Of<IIppProtocol>(), IppRequestMessageValidator.Default, null, null);
        var message = new CreateJobResponse
        {
            RequestId = 0, // Invalid value
            StatusCode = IppStatusCode.SuccessfulOk
        };

        Func<Task<IIppResponseMessage>> act = () => server.CreateRawResponseAsync(message);
        await act.Should().NotThrowAsync();
    }

    [TestMethod]
    public async Task CreateRawResponseAsync_WithResponseMessageValidator_ShouldInvokeValidator()
    {
        Mock<IIppResponseMessageValidator> responseMessageValidator = new();
        SharpIppServer server = new(Mock.Of<IIppProtocol>(), IppRequestMessageValidator.Default, null, responseMessageValidator.Object);
        var message = new CreateJobResponse
        {
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk
        };

        var response = await server.CreateRawResponseAsync(message);

        responseMessageValidator.Verify(x => x.Validate(It.Is<IIppResponseMessage>(m => m == response)), Times.Once);
    }

    [TestMethod]
    public async Task SendRawResponseAsync_WithResponseMessageValidator_ShouldInvokeValidator()
    {
        Mock<IIppResponseMessageValidator> responseMessageValidator = new();
        SharpIppServer server = new(Mock.Of<IIppProtocol>(), IppRequestMessageValidator.Default, null, responseMessageValidator.Object);
        var response = new IppResponseMessage
        {
            RequestId = 123,
            Version = new IppVersion(1, 1),
            StatusCode = IppStatusCode.SuccessfulOk
        };

        await server.SendRawResponseAsync(response, Stream.Null);

        responseMessageValidator.Verify(x => x.Validate(It.Is<IIppResponseMessage>(m => m == response)), Times.Once);
    }

    [TestMethod]
    public void Constructor_WithThreeArguments_ShouldUseDefaultResponseMessageValidator()
    {
        // Arrange
        var server = new SharpIppServer(
            Mock.Of<IIppProtocol>(),
            Mock.Of<IIppRequestMessageValidator>(),
            Mock.Of<IIppResponseValidator>());

        // Act
        Action act = () => server.SendRawResponseAsync(new IppResponseMessage(), Stream.Null);

        // Assert
        act.Should().Throw<IppResponseException>();
    }

    [TestMethod]
    public async Task SendRawResponseAsync_WithNullResponseMessageValidator_ShouldNotThrow()
    {
        // Arrange
        SharpIppServer server = new(Mock.Of<IIppProtocol>(), IppRequestMessageValidator.Default, null, null);
        var response = new IppResponseMessage(); // Invalid response, but should not throw because validator is null

        // Act
        Func<Task> act = () => server.SendRawResponseAsync(response, Stream.Null);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
