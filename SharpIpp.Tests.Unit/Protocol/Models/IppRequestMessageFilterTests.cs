using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestMessageFilterTests
{
    [TestMethod]
    public void Constructor_NullRequest_ThrowsArgumentNullException()
    {
        Action act = () => new IppRequestMessageFilter(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ClearProperty_NullPropertyName_ThrowsArgumentNullException()
    {
        var requestMock = new Mock<IIppRequestMessage>();
        var filter = new IppRequestMessageFilter(requestMock.Object);
        Action act = () => filter.ClearProperty(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("   ")]
    public void ClearProperty_EmptyOrWhitespacePropertyName_ThrowsArgumentException(string propertyName)
    {
        var requestMock = new Mock<IIppRequestMessage>();
        var filter = new IppRequestMessageFilter(requestMock.Object);
        Action act = () => filter.ClearProperty(propertyName);
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void Properties_Set_PropagatesToUnderlyingRequest()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        requestMock.SetupAllProperties();
        var filter = new IppRequestMessageFilter(requestMock.Object);
        using var stream = new MemoryStream();

        // Act
        filter.Version = new IppVersion(2, 0);
        filter.IppOperation = IppOperation.CreateJob;
        filter.RequestId = 999;
        filter.Document = stream;

        // Assert
        requestMock.Object.Version.Should().Be(new IppVersion(2, 0));
        requestMock.Object.IppOperation.Should().Be(IppOperation.CreateJob);
        requestMock.Object.RequestId.Should().Be(999);
        requestMock.Object.Document.Should().BeSameAs(stream);
    }

    [TestMethod]
    public void Property_WhenNotCleared_ReturnsOriginalValue()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        requestMock.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
        requestMock.SetupGet(x => x.RequestId).Returns(123);
        requestMock.SetupGet(x => x.IppOperation).Returns(IppOperation.PrintJob);
        using var originalStream = new MemoryStream();
        requestMock.SetupGet(x => x.Document).Returns(originalStream);
        
        var filter = new IppRequestMessageFilter(requestMock.Object);

        // Act
        var version = filter.Version;
        var requestId = filter.RequestId;
        var operation = filter.IppOperation;
        var document = filter.Document;

        // Assert
        version.Should().Be(new IppVersion(1, 1));
        requestId.Should().Be(123);
        operation.Should().Be(IppOperation.PrintJob);
        document.Should().BeSameAs(originalStream);
    }

    [TestMethod]
    public void Property_WhenCleared_ReturnsDefaultValue()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        requestMock.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
        requestMock.SetupGet(x => x.RequestId).Returns(123);
        requestMock.SetupGet(x => x.IppOperation).Returns(IppOperation.PrintJob);
        using var filteredStream = new MemoryStream();
        requestMock.SetupGet(x => x.Document).Returns(filteredStream);
        
        var filter = new IppRequestMessageFilter(requestMock.Object);

        // Act
        filter.ClearProperty(nameof(IIppRequestMessage.Version));
        filter.ClearProperty(nameof(IIppRequestMessage.RequestId));
        filter.ClearProperty(nameof(IIppRequestMessage.IppOperation));
        filter.ClearProperty(nameof(IIppRequestMessage.Document));

        var version = filter.Version;
        var requestId = filter.RequestId;
        var operation = filter.IppOperation;
        var document = filter.Document;

        // Assert
        version.Should().Be(default(IppVersion));
        requestId.Should().Be(0);
        operation.Should().Be(default(IppOperation));
        document.Should().BeNull();
    }

    [TestMethod]
    [DataRow(nameof(IIppRequestMessage.OperationAttributes))]
    [DataRow(nameof(IIppRequestMessage.JobAttributes))]
    [DataRow(nameof(IIppRequestMessage.PrinterAttributes))]
    [DataRow(nameof(IIppRequestMessage.UnsupportedAttributes))]
    [DataRow(nameof(IIppRequestMessage.SubscriptionAttributes))]
    [DataRow(nameof(IIppRequestMessage.EventNotificationAttributes))]
    [DataRow(nameof(IIppRequestMessage.ResourceAttributes))]
    [DataRow(nameof(IIppRequestMessage.DocumentAttributes))]
    [DataRow(nameof(IIppRequestMessage.SystemAttributes))]
    public void ListAttributes_WhenCleared_ReturnsNull(string propertyName)
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        var filter = new IppRequestMessageFilter(requestMock.Object);

        // Act
        filter.ClearProperty(propertyName);

        var actualList = typeof(IppRequestMessageFilter).GetProperty(propertyName)!.GetValue(filter);

        // Assert
        actualList.Should().BeNull();
    }

    [TestMethod]
    [DataRow(nameof(IIppRequestMessage.OperationAttributes))]
    [DataRow(nameof(IIppRequestMessage.JobAttributes))]
    [DataRow(nameof(IIppRequestMessage.PrinterAttributes))]
    [DataRow(nameof(IIppRequestMessage.UnsupportedAttributes))]
    [DataRow(nameof(IIppRequestMessage.SubscriptionAttributes))]
    [DataRow(nameof(IIppRequestMessage.EventNotificationAttributes))]
    [DataRow(nameof(IIppRequestMessage.ResourceAttributes))]
    [DataRow(nameof(IIppRequestMessage.DocumentAttributes))]
    [DataRow(nameof(IIppRequestMessage.SystemAttributes))]
    public void ListAttributes_WhenNotCleared_ReturnsOriginalValue(string propertyName)
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        var expectedList = new List<IppAttribute>();

        switch (propertyName)
        {
            case nameof(IIppRequestMessage.OperationAttributes): requestMock.SetupGet(x => x.OperationAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.JobAttributes): requestMock.SetupGet(x => x.JobAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.PrinterAttributes): requestMock.SetupGet(x => x.PrinterAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.UnsupportedAttributes): requestMock.SetupGet(x => x.UnsupportedAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.SubscriptionAttributes): requestMock.SetupGet(x => x.SubscriptionAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.EventNotificationAttributes): requestMock.SetupGet(x => x.EventNotificationAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.ResourceAttributes): requestMock.SetupGet(x => x.ResourceAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.DocumentAttributes): requestMock.SetupGet(x => x.DocumentAttributes).Returns(expectedList); break;
            case nameof(IIppRequestMessage.SystemAttributes): requestMock.SetupGet(x => x.SystemAttributes).Returns(expectedList); break;
        }

        var filter = new IppRequestMessageFilter(requestMock.Object);

        // Act
        var actualList = typeof(IppRequestMessageFilter).GetProperty(propertyName)!.GetValue(filter);

        // Assert
        actualList.Should().BeSameAs(expectedList);
    }
}
