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

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

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
            .WithMessage("No printer-uri or system-uri operation attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_DocumentOperationWithJobUriOnly_ShouldBeSuccess()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.GetDocuments,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/123" ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);

        Action act = () => message.Validate();

        act.Should().NotThrow();
    }

    [TestMethod()]
    public void Validate_SystemOperationWithoutSystemUri_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CancelResource,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/" ),
            new IppAttribute( Tag.Integer, SystemAttribute.ResourceId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);

        Action act = () => message.Validate();

        act.Should().Throw<IppRequestException>()
            .WithMessage("No system-uri operation attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void Validate_SystemOperationWithSystemUri_ShouldBeSuccess()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.CancelResource,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ),
            new IppAttribute( Tag.Uri, SystemAttribute.SystemUri, "ipp://127.0.0.1:8631/system" ),
            new IppAttribute( Tag.Integer, SystemAttribute.ResourceId, 123 ),
            new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.RequestingUserName, "test-user" )
        ]);

        Action act = () => message.Validate();

        act.Should().NotThrow();
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

    [TestMethod()]
    public void ValidateOperationRules_GetDocumentAttributesMissingDocumentNumber_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.GetDocumentAttributes,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123)
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing document-number")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_GetDocumentAttributesInvalidDocumentNumber_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.GetDocumentAttributes,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123),
            new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 0)
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-number")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_PrintJobWithoutDocumentStream_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("document stream required")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_SendDocumentInvalidLastDocument_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.SendDocument,
            RequestId = 123,
            Document = new MemoryStream([1, 2, 3])
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123),
            new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.LastDocument, "false")
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid last-document")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_SendDocumentMissingLastDocument_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.SendDocument,
            RequestId = 123,
            Document = new MemoryStream([1, 2, 3])
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123)
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing last-document")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_SendDocumentWithoutDocumentStream_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.SendDocument,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123),
            new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false)
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("document stream required when last-document=false")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_SendUriMissingDocumentUri_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.SendUri,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123),
            new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false)
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing document-uri")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_PrintUriMissingDocumentUri_ShouldThrowException()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.PrintUri,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing document-uri")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod()]
    public void ValidateOperationRules_SendUriWithLastDocumentAndNoDocumentUri_ShouldBeSuccess()
    {
        IppRequestMessage message = new()
        {
            IppOperation = IppOperation.SendUri,
            RequestId = 123,
        };
        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123),
            new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true)
        ]);

        Action act = () => message.ValidateOperationRules();

        act.Should().NotThrow();
    }

    [TestMethod()]
    public void ValidateOperationRules_MessageIsNull_ShouldThrowException()
    {
        Action act = () => ((IppRequestMessage?)null).ValidateOperationRules();

        act.Should().Throw<ArgumentNullException>();
    }
}
