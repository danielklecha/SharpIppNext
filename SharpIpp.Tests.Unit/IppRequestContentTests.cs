using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp.Tests.Unit;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestContentTests
{
    [TestMethod]
    public void TryComputeLength_WithoutDocument_ComputesCorrectLength()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        requestMock.SetupProperty(x => x.Document, null);
        var protocolMock = new Mock<IIppProtocol>();

        // We simulate a mock protocol that writes exactly 15 bytes to the stream
        protocolMock
            .Setup(x => x.WriteIppRequestAsync(It.IsAny<IIppRequestMessage>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((IIppRequestMessage req, Stream stream, CancellationToken token) =>
            {
                stream.Write(new byte[15], 0, 15);
                return Task.CompletedTask;
            });

        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        var contentLength = content.Headers.ContentLength;

        // Assert
        Assert.IsNotNull(contentLength);
        Assert.AreEqual(15, contentLength.Value);
    }

    [TestMethod]
    public void TryComputeLength_WithSeekableDocument_ComputesCorrectLength()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        using var documentStream = new MemoryStream(new byte[50]); // 50 bytes document
        
        // request.Document setter is invoked during TryComputeLength to temporarily set to null
        Stream? currentDocument = documentStream;
        requestMock.SetupGet(x => x.Document).Returns(() => currentDocument);
        requestMock.SetupSet(x => x.Document = It.IsAny<Stream?>()).Callback<Stream?>(val => currentDocument = val);

        var protocolMock = new Mock<IIppProtocol>();
        protocolMock
            .Setup(x => x.WriteIppRequestAsync(It.IsAny<IIppRequestMessage>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((IIppRequestMessage req, Stream stream, CancellationToken token) =>
            {
                stream.Write(new byte[20], 0, 20); // 20 bytes headers
                return Task.CompletedTask;
            });

        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        var contentLength = content.Headers.ContentLength;

        // Assert
        Assert.IsNotNull(contentLength);
        Assert.AreEqual(70, contentLength.Value); // 20 + 50
        Assert.AreSame(documentStream, currentDocument); // Ensures the document is restored
    }

    [TestMethod]
    public void TryComputeLength_WithUnseekableDocument_ReturnsNullContentLength()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        
        var unseekableStreamMock = new Mock<Stream>();
        unseekableStreamMock.SetupGet(x => x.CanSeek).Returns(false);
        
        Stream? currentDocument = unseekableStreamMock.Object;
        requestMock.SetupGet(x => x.Document).Returns(() => currentDocument);
        requestMock.SetupSet(x => x.Document = It.IsAny<Stream?>()).Callback<Stream?>(val => currentDocument = val);

        var protocolMock = new Mock<IIppProtocol>();

        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        var contentLength = content.Headers.ContentLength;

        // Assert
        Assert.IsNull(contentLength); // HttpClient won't set Content-Length if TryComputeLength returns false
    }

    private class TestIppRequestContent : IppRequestContent
    {
        public TestIppRequestContent(IIppRequestMessage request, IIppProtocol protocol, CancellationToken cancellationToken)
            : base(request, protocol, cancellationToken)
        {
        }

        public bool CallTryComputeLength(out long length)
        {
            return TryComputeLength(out length);
        }
    }

    [TestMethod]
    public void TryComputeLength_WhenCalledMultipleTimes_ReturnsCachedLength()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        var protocolMock = new Mock<IIppProtocol>();

        // We simulate a mock protocol that writes exactly 15 bytes to the stream
        var callCount = 0;
        protocolMock
            .Setup(x => x.WriteIppRequestAsync(It.IsAny<IIppRequestMessage>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((IIppRequestMessage req, Stream stream, CancellationToken token) =>
            {
                callCount++;
                stream.Write(new byte[15], 0, 15);
                return Task.CompletedTask;
            });

        using var content = new TestIppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        var result1 = content.CallTryComputeLength(out var length1);
        var result2 = content.CallTryComputeLength(out var length2);

        // Assert
        Assert.IsTrue(result1);
        Assert.IsTrue(result2);
        Assert.AreEqual(15, length1);
        Assert.AreEqual(15, length2);
        Assert.AreEqual(1, callCount, "TryComputeLength should cache the result and only call serialization once.");
    }

    [TestMethod]
    public void TryComputeLength_WhenDocumentLengthThrows_ReturnsNullContentLength()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        
        var streamMock = new Mock<Stream>();
        streamMock.SetupGet(x => x.CanSeek).Returns(true);
        streamMock.SetupGet(x => x.Length).Throws(new System.NotSupportedException());
        
        requestMock.SetupGet(x => x.Document).Returns(streamMock.Object);

        var protocolMock = new Mock<IIppProtocol>();

        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        var contentLength = content.Headers.ContentLength;

        // Assert
        Assert.IsNull(contentLength);
    }

    [TestMethod]
    public void TryComputeLength_WhenSerializationThrows_ReturnsNullContentLength()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        var protocolMock = new Mock<IIppProtocol>();

        protocolMock
            .Setup(x => x.WriteIppRequestAsync(It.IsAny<IIppRequestMessage>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Throws(new System.Exception("Serialization failed"));

        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        var contentLength = content.Headers.ContentLength;

        // Assert
        Assert.IsNull(contentLength);
    }

    [TestMethod]
    public void Constructor_WhenDocumentPositionThrows_FallsBackToZero()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        var streamMock = new Mock<Stream>();
        streamMock.SetupGet(x => x.CanSeek).Returns(true);
        streamMock.SetupGet(x => x.Position).Throws(new System.Exception("Simulated Position exception"));
        requestMock.SetupGet(x => x.Document).Returns(streamMock.Object);

        var protocolMock = new Mock<IIppProtocol>();
        protocolMock
            .Setup(x => x.WriteIppRequestAsync(It.IsAny<IIppRequestMessage>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((IIppRequestMessage req, Stream stream, CancellationToken token) =>
            {
                stream.Write(new byte[15], 0, 15);
                return Task.CompletedTask;
            });

        // Act
        streamMock.SetupGet(x => x.Length).Returns(50);
        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);
        var contentLength = content.Headers.ContentLength;

        // Assert
        Assert.IsNotNull(contentLength);
        Assert.AreEqual(65, contentLength.Value);
    }

    [TestMethod]
    public async Task SerializeToStreamAsync_WhenDocumentPositionSetThrows_StillSucceedsSerialization()
    {
        // Arrange
        var requestMock = new Mock<IIppRequestMessage>();
        var streamMock = new Mock<Stream>();
        streamMock.SetupGet(x => x.CanSeek).Returns(true);
        streamMock.SetupGet(x => x.Position).Returns(0);
        streamMock.SetupSet(x => x.Position = It.IsAny<long>()).Throws(new System.Exception("Simulated Position set exception"));
        requestMock.SetupGet(x => x.Document).Returns(streamMock.Object);

        var protocolMock = new Mock<IIppProtocol>();
        var writeCalled = false;
        protocolMock
            .Setup(x => x.WriteIppRequestAsync(It.IsAny<IIppRequestMessage>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((IIppRequestMessage req, Stream stream, CancellationToken token) =>
            {
                writeCalled = true;
                stream.Write(new byte[10], 0, 10);
                return Task.CompletedTask;
            });

        using var content = new IppRequestContent(requestMock.Object, protocolMock.Object, CancellationToken.None);

        // Act
        using var ms = new MemoryStream();
        await content.CopyToAsync(ms);

        // Assert
        Assert.IsTrue(writeCalled);
        Assert.AreEqual(10, ms.Length);
    }
}
