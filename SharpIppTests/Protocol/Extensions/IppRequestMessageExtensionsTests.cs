using Moq;
using SharpIpp;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpIppTests.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestMessageExtensionsTests
{
    [TestMethod()]
    public void Validate_RequestIdIsInvalid_ShoouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = -123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("Bad request-id value")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_EmptyListOfOperations_ShoouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("No Operation Attributes")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_InvalidFirstOperationAttribute_ShoouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("attributes-charset MUST be the first attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_InvalidSecondOperationAttribute_ShoouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("attributes-natural-language MUST be the second attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_MissingSecondOperationAttribute_ShoouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("attributes-natural-language MUST be the second attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_InvalidIppVersion_ShoouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            Version = new IppVersion(0, 9),
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("Unsupported IPP version")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_MissingPrinterUriAttribute_ShouldThrowException()
    {
        // Arrange
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().Throw<IppRequestException>()
            .WithMessage("No printer-uri operation attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_MessageIsNull_ShouldThrowException()
    {
        // Act
        Action act = () => ((IppRequestMessage?)null).Validate();
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod()]
    public void Validate_Message_ShouldBeSuccess()
    {
        // Arrange
        SharpIppServer server = new(Mock.Of<IIppProtocol>());
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);
        // Act
        Action act = () => message.Validate();
        // Assert
        act.Should().NotThrow();
    }
}
