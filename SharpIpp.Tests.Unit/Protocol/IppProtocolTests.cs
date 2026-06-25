using FluentAssertions.Equivalency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Moq;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppProtocolTests
{
    [TestMethod()]
    public async Task WriteValue_NoValue_ShouldBeWritten()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        await protocol.WriteValueAsync(NoValue.Instance, binaryWriter);
        memoryStream.ToArray().Should().Equal( 0x00, 0x00 );
    }

    [TestMethod]
    [DataRow( true, new byte[] { 0x00, 0x01, 0x01 } )]
    [DataRow( false, new byte[] { 0x00, 0x01, 0x00 } )]
    public async Task WriteValue_Boolean_ShouldBeWritten( bool value, byte[] expected )
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        await protocol.WriteValueAsync(value, binaryWriter);
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    [DataRow( "12/31/1999 23:59:59 +02:30", new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2B, 0x02, 0x1E } )]
    [DataRow( "12/31/1999 23:59:59 -02:30", new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2D, 0x02, 0x1E } )]
    [DataRow( "01/01/0001 01:01:01 +00:00", new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 } )]
    public async Task WriteValue_DateTimeOffset_ShouldBeWritten( string value, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteValueAsync(DateTimeOffset.Parse( value, CultureInfo.InvariantCulture ), binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    [DataRow( int.MinValue, new byte[] { 0x00, 0x04, 0x80, 0x00, 0x00, 0x00 } )]
    [DataRow( 0, new byte[] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 } )]
    [DataRow( int.MaxValue, new byte[] { 0x00, 0x04, 0x7F, 0xFF, 0xFF, 0xFF } )]
    public async Task WriteValue_Int32_ShouldBeWritten( int value, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteValueAsync(value, binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    [DataRow( int.MinValue, int.MinValue, new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 } )]
    [DataRow( int.MinValue, int.MaxValue, new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF } )]
    [DataRow( int.MaxValue, int.MaxValue, new byte[] { 0x00, 0x08, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF } )]
    public async Task WriteValue_Range_ShouldBeWritten( int lower, int upper, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteValueAsync(new SharpIpp.Protocol.Models.Range( lower, upper ), binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    [DataRow( 0, int.MaxValue, ResolutionUnit.DotsPerCm, new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 } )]
    [DataRow( 0, int.MaxValue, ResolutionUnit.DotsPerInch, new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 } )]
    [DataRow( int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerCm, new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 } )]
    [DataRow( int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerInch, new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 } )]
    public async Task WriteValue_Resolution_ShouldBeWritten( int width, int height, ResolutionUnit unit, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteValueAsync(new Resolution( width, height, unit ), binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    public async Task WriteValue_ResourceState_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );

        // Act
        await protocol.WriteValueAsync(ResourceState.Available, binaryWriter);

        // Assert
        memoryStream.ToArray().Should().Equal( 0x00, 0x04, 0x00, 0x00, 0x00, 0x04 );
    }

    [TestMethod]
    [DataRow( "en-us", "Lorem", new byte[] { 0x00, 0x0E, 0x00, 0x05, 0x65, 0x6E, 0x2D, 0x75, 0x73, 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } )]
    public async Task Write_StringWithLanguage_ShouldBeWritten( string language, string text, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteValueAsync(new StringWithLanguage( language, text ), binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    public async Task Write_String_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteValueAsync("Lorem", binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D );
    }

    [TestMethod]
    public async Task WriteValue_BytesArray_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        var value = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
        // Act
        await protocol.WriteValueAsync(value, binaryWriter);
        // Assert
        memoryStream.ToArray().Should().Equal( 0x00, 0x04, 0xDE, 0xAD, 0xBE, 0xEF );
    }

    [TestMethod]
    public async Task WriteValue_ExtendedValue_ShouldBeWritten()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new(memoryStream);

        await protocol.WriteValueAsync(new ExtendedValue(0x01020304, new byte[] { 0xAA, 0xBB }), binaryWriter);

        memoryStream.ToArray().Should().Equal(0x00, 0x06, 0x01, 0x02, 0x03, 0x04, 0xAA, 0xBB);
    }

    [TestMethod]
    public async Task WriteValue_UnknownValue_ShouldBeWritten()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new(memoryStream);

        await protocol.WriteValueAsync(new UnknownValue((Tag)0x60, new byte[] { 0xDE, 0xAD }), binaryWriter);

        memoryStream.ToArray().Should().Equal(0x00, 0x02, 0xDE, 0xAD);
    }

    [TestMethod]
    public async Task WriteValue_UnsupportedType_ThrowsArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        Func<Task> act = async () => await protocol.WriteValueAsync(123L, binaryWriter);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_NoAttributes_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123
        };
        // Act
        await protocol.WriteIppRequestAsync( message, memoryStream );
        // Assert
        memoryStream.ToArray().Should().Equal( 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B );
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_MessageIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task> act = async () => await protocol.WriteIppRequestAsync( null, memoryStream );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task> act = async () => await protocol.WriteIppRequestAsync( new IppRequestMessage(), null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_TwoSections_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123
        };
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NameWithoutLanguage, IppAttributeNames.JobName, "Test Job" ) );
        message.JobAttributes.Add( new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1 ) );
        // Act
        await protocol.WriteIppRequestAsync( message, memoryStream );
        // Assert
        memoryStream.ToArray().Should().Equal( 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00,
            0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73,
            0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38, 0x48, 0x00, 0x1B, 0x61, 0x74, 0x74, 0x72, 0x69,
            0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x6E, 0x61, 0x74, 0x75, 0x72, 0x61, 0x6C, 0x2D, 0x6C, 0x61, 0x6E,
            0x67, 0x75, 0x61, 0x67, 0x65, 0x00, 0x02, 0x65, 0x6E, 0x42, 0x00, 0x08, 0x6A, 0x6F, 0x62, 0x2D, 0x6E,
            0x61, 0x6D, 0x65, 0x00, 0x08, 0x54, 0x65, 0x73, 0x74, 0x20, 0x4A, 0x6F, 0x62, 0x02, 0x21, 0x00, 0x06,
            0x63, 0x6F, 0x70, 0x69, 0x65, 0x73, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x03 );
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_Document_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new();
        using MemoryStream documentStream = new( Encoding.ASCII.GetBytes( "Lorem" ) );
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
            Document = documentStream
        };
        // Act
        await protocol.WriteIppRequestAsync( message, requestStream );
        // Assert
        requestStream.ToArray().Should().Equal( 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x4C, 0x6F, 0x72, 0x65, 0x6D );
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_TwoSections_ShouldMatch()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new( new byte[] {
            0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00,
            0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73,
            0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38, 0x48, 0x00, 0x1B, 0x61, 0x74, 0x74, 0x72, 0x69,
            0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x6E, 0x61, 0x74, 0x75, 0x72, 0x61, 0x6C, 0x2D, 0x6C, 0x61, 0x6E,
            0x67, 0x75, 0x61, 0x67, 0x65, 0x00, 0x02, 0x65, 0x6E, 0x42, 0x00, 0x08, 0x6A, 0x6F, 0x62, 0x2D, 0x6E,
            0x61, 0x6D, 0x65, 0x00, 0x08, 0x54, 0x65, 0x73, 0x74, 0x20, 0x4A, 0x6F, 0x62, 0x02, 0x21, 0x00, 0x06,
            0x63, 0x6F, 0x70, 0x69, 0x65, 0x73, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x03 } );
        // Act
        Func<Task<IIppRequestMessage>> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        // Assert
        using MemoryStream documentStream = new();
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
            Document = documentStream
        };
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NameWithoutLanguage, IppAttributeNames.JobName, "Test Job" ) );
        message.JobAttributes.Add( new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1 ) );
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( message, x => x.Excluding( ( IMemberInfo x ) => x.Path == "Document.ReadTimeout" || x.Path == "Document.WriteTimeout" ) );
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_Null_ShouldMatch()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<IIppRequestMessage>> act = async () => await protocol.ReadIppRequestAsync( null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public async Task ReadIppRequestAsync_Document_ShouldMatch()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x03, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        // Act
        Func<Task<IIppRequestMessage>> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        // Assert
        using var expectedStream = new MemoryStream( new byte[] { 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
            Document = expectedStream
        }, x => x.Excluding( ( IMemberInfo x ) => x.Path == "Document.ReadTimeout" || x.Path == "Document.WriteTimeout" ) );
    }

    [TestMethod()]
    public async Task ReadIppRequestAsync_DocumentExceedsMaxSize_ShouldThrowClientErrorRequestEntityTooLarge()
    {
        // Arrange
        var protocol = new IppProtocol { MaxDocumentStreamBytes = 4 };
        // 5 bytes of payload: 0x4C, 0x6F, 0x72, 0x65, 0x6D ("Lorem")
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x03, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        
        // Act
        Func<Task<IIppRequestMessage>> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        
        // Assert
        var exceptionAssertion = await act.Should().ThrowAsync<IppRequestException>();
        exceptionAssertion.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorRequestEntityTooLarge);
    }

    [TestMethod()]
    public async Task ReadIppRequestAsync_DocumentWithNullMaxBytes_ShouldMatch()
    {
        // Arrange
        var protocol = new IppProtocol { MaxDocumentStreamBytes = null };
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x03, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        // Act
        Func<Task<IIppRequestMessage>> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        // Assert
        using var expectedStream = new MemoryStream( new byte[] { 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123,
            Document = expectedStream
        }, x => x.Excluding( ( IMemberInfo x ) => x.Path == "Document.ReadTimeout" || x.Path == "Document.WriteTimeout" ) );
    }

    [TestMethod()]
    public async Task ReadIppRequestAsync_DocumentStreamCopyThrows_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Header bytes (9 bytes): version 1.1, operation print-job, request id 123, end-of-attributes 0x03
        var headerBytes = new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x03 };
        using var requestStream = new ThrowingStream(headerBytes);

        // Act
        Func<Task<IIppRequestMessage>> act = async () => await protocol.ReadIppRequestAsync( requestStream );

        // Assert
        var exceptionAssertion = await act.Should().ThrowAsync<IppRequestException>();
        exceptionAssertion.Which.Message.Should().Be("Failed to copy document stream.");
        exceptionAssertion.Which.InnerException.Should().BeOfType<IOException>().Which.Message.Should().Be("Simulated stream read failure");
    }

    [TestMethod()]
    public async Task WriteSection_EmptyListOfAttributes_ShouldNotWriteAnything()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteSectionAsync(SectionTag.OperationAttributesTag, new List<IppAttribute>(), binaryWriter, Encoding.ASCII );
        // Assert
        memoryStream.Length.Should().Be( 0 );
    }

    [TestMethod()]
    public async Task WriteSection_ListIsNull_ShouldNotWriteAnything()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task> act = async () => await protocol.WriteSectionAsync(SectionTag.OperationAttributesTag, null, binaryWriter, Encoding.ASCII);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public async Task WriteSection_StreamIsNull_ShouldNotWriteAnything()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task> act = async () => await protocol.WriteSectionAsync( SectionTag.OperationAttributesTag, new List<IppAttribute>
        {
            new( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion(1,0).ToString() )
        }, null, Encoding.ASCII );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public async Task WriteSection_OneAttribute_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteSectionAsync( SectionTag.OperationAttributesTag, new List<IppAttribute>
        {
            new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion(1,0).ToString() )
        }, binaryWriter, Encoding.ASCII );
        // Assert
        memoryStream.ToArray().Should().Equal( 0x01, 0x44, 0x00, 0x16, 0x69, 0x70, 0x70, 0x2D, 0x76, 0x65, 0x72, 0x73,
            0x69, 0x6F, 0x6E, 0x73, 0x2D, 0x73, 0x75, 0x70, 0x70, 0x6F, 0x72, 0x74, 0x65, 0x64, 0x00, 0x03, 0x31, 0x2E, 0x30 );
    }

    [TestMethod]
    public async Task WriteAttribute_BegCollection_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new(memoryStream);
        // Act
        await protocol.WriteAttributeAsync(binaryWriter, new IppAttribute(Tag.BegCollection, IppAttributeNames.MediaColDefault, NoValue.Instance),
            null,
            Encoding.ASCII);
        // Assert
        memoryStream.ToArray().Should().Equal(0x34, 0x00, 0x11, 0x6D, 0x65, 0x64, 0x69, 0x61, 0x2D, 0x63, 0x6F, 0x6C,
            0x2D, 0x64, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x00, 0x00);
    }

    [TestMethod()]
    public async Task WriteAttribute_OneAttribute_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteAttributeAsync(binaryWriter, new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ),
            null,
            Encoding.ASCII);
        // Assert
        memoryStream.ToArray().Should().Equal( 0x44, 0x00, 0x16, 0x69, 0x70, 0x70, 0x2D, 0x76, 0x65, 0x72, 0x73, 0x69,
            0x6F, 0x6E, 0x73, 0x2D, 0x73, 0x75, 0x70, 0x70, 0x6F, 0x72, 0x74, 0x65, 0x64, 0x00, 0x03, 0x31, 0x2E, 0x31 );
    }

    [TestMethod()]
    public async Task WriteAttribute_SecondSimilarAttribute_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryWriter binaryWriter = new( memoryStream );
        // Act
        await protocol.WriteAttributeAsync(binaryWriter, new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ),
            new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion( 1, 0 ).ToString() ),
            Encoding.ASCII );
        // Assert
        memoryStream.ToArray().Should().Equal( 0x44, 0x00, 0x00, 0x00, 0x03, 0x31, 0x2E, 0x31 );
    }

    [TestMethod]
    public async Task WriteIppResponseAsync_NoSections_ShouldThrowArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new();
        var message = new IppResponseMessage
        {
            RequestId = 123
        };
        // Act
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( message, requestStream );
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task WriteIppResponseAsync_TwoSections_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new();
        var message = new IppResponseMessage
        {
            RequestId = 123
        };
        var operationAttrs = new List<IppAttribute>
        {
            new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en" )
        };
        message.OperationAttributes.Add( operationAttrs );
        var jobAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1 )
        };
        message.JobAttributes.Add( jobAttrs );
        // Act
        await protocol.WriteIppResponseAsync( message, requestStream );
        // Assert
        requestStream.ToArray().Should().Equal( 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00, 0x12, 0x61,
            0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05,
            0x75, 0x74, 0x66, 0x2D, 0x38, 0x48, 0x00, 0x1B, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D,
            0x6E, 0x61, 0x74, 0x75, 0x72, 0x61, 0x6C, 0x2D, 0x6C, 0x61, 0x6E, 0x67, 0x75, 0x61, 0x67, 0x65, 0x00, 0x02, 0x65,
            0x6E, 0x02, 0x21, 0x00, 0x06, 0x63, 0x6F, 0x70, 0x69, 0x65, 0x73, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x03 );
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_NoSection_ShouldMatch()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B, 0x03 } );
        // Act
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        // Assert
        var message = new IppResponseMessage
        {
            RequestId = 123
        };
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( message );
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_TwoSection_ShouldMatch()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new( new byte[] {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00, 0x12, 0x61,
            0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05,
            0x75, 0x74, 0x66, 0x2D, 0x38, 0x48, 0x00, 0x1B, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D,
            0x6E, 0x61, 0x74, 0x75, 0x72, 0x61, 0x6C, 0x2D, 0x6C, 0x61, 0x6E, 0x67, 0x75, 0x61, 0x67, 0x65, 0x00, 0x02, 0x65,
            0x6E, 0x02, 0x21, 0x00, 0x06, 0x63, 0x6F, 0x70, 0x69, 0x65, 0x73, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x03 } );
        // Act
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        // Assert
        var message = new IppResponseMessage
        {
            RequestId = 123
        };
        var operationAttrs = new List<IppAttribute>
        {
            new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en" )
        };
        message.OperationAttributes.Add( operationAttrs );
        var jobAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1 )
        };
        message.JobAttributes.Add( jobAttrs );
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( message );
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_FutureGroup_ShouldIgnoreAndSucceed()
    {
        var protocol = new IppProtocol();
        // version 1.1, op Print-Job, request-id 1
        // operation group with attributes-charset=utf-8, then future group 0x0B containing one keyword, then end tag
        var bytes = new byte[]
        {
            0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01,
            0x01, // operation-attributes-tag
            0x47, 0x00, 0x12, // charset name length 18
            0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74,
            0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x0B, // future group tag
            0x44, 0x00, 0x03, 0x66, 0x6F, 0x6F, 0x00, 0x03, 0x62, 0x61, 0x72,
            0x03
        };
        using MemoryStream requestStream = new(bytes);

        var result = await protocol.ReadIppRequestAsync(requestStream);

        result.OperationAttributes.Should().HaveCount(1);
        result.OperationAttributes[0].Name.Should().Be(IppAttributeNames.AttributesCharset);
        result.OperationAttributes[0].Value.Should().Be("utf-8");
        result.JobAttributes.Should().BeEmpty();
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_FutureGroup_ShouldIgnoreAndSucceed()
    {
        var protocol = new IppProtocol();
        // version 1.1, status 0, request-id 1, operation group charset, future group 0x0C with keyword, end tag
        var bytes = new byte[]
        {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x01, // operation-attributes-tag
            0x47, 0x00, 0x12,
            0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74,
            0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x0C, // future group tag
            0x44, 0x00, 0x03, 0x66, 0x6F, 0x6F, 0x00, 0x03, 0x62, 0x61, 0x72,
            0x03
        };
        using MemoryStream stream = new(bytes);

        var result = await protocol.ReadIppResponseAsync(stream);

        result.OperationAttributes.Should().HaveCount(1);
        result.OperationAttributes[0].Should().ContainSingle();
        result.OperationAttributes[0][0].Name.Should().Be(IppAttributeNames.AttributesCharset);
        result.OperationAttributes[0][0].Value.Should().Be("utf-8");
        result.JobAttributes.Should().BeEmpty();
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_MissingSectionTag_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B, 0x47 } );
        // Act
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        // Assert
        await act.Should().ThrowAsync<IppResponseException>();
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_EmptyStream_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new();
        // Act
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        // Assert
        await act.Should().ThrowAsync<IppResponseException>();
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_MaxAttributesExceeded_ThrowsIppResponseExceptionWithArgumentExceptionInnerException()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesCount = 1 };
        using MemoryStream requestStream = new( new byte[] {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B,
            0x01, // operation-attributes-tag
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x03 // EndOfAttributesTag
        } );
        // Act
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        // Assert
        var exception = await act.Should().ThrowAsync<IppResponseException>();
        exception.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("Maximum attribute limit of 1 exceeded.");
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_UnexpectedEndCollection_ThrowsIppResponseExceptionWithArgumentExceptionInnerException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new( new byte[] {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B,
            0x01, // operation-attributes-tag
            0x37, 0x00, 0x00, 0x00, 0x00, // Tag.EndCollection, name length 0, value length 0
            0x03 // EndOfAttributesTag
        } );
        // Act
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        // Assert
        var exception = await act.Should().ThrowAsync<IppResponseException>();
        exception.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("Unexpected EndCollection tag. No matching BegCollection.");
    }

    [TestMethod]
    [DataRow( Tag.TextWithLanguage )]
    [DataRow( Tag.NameWithLanguage )]
    public async Task ReadValue_StringWithLanguage_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        // 5 (lang) + 5 (value) + 4 = 14 (0x0E)
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x0E, 0x00, 0x05, 0x65, 0x6E, 0x2D, 0x75, 0x73, 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, tag);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new StringWithLanguage( "en-us", "Lorem" ) );
    }

    [TestMethod]
    [DataRow( Tag.TextWithLanguage )]
    [DataRow( Tag.NameWithLanguage )]
    public async Task ReadValue_InvalidStringWithLanguage_ThrowsArgumentException( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        // 10 (0x0A) != 5 + 5 + 4
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x0A, 0x00, 0x05, 0x65, 0x6E, 0x2D, 0x75, 0x73, 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, tag);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    [DataRow( Tag.TextWithoutLanguage )]
    [DataRow( Tag.NameWithoutLanguage )]
    [DataRow( Tag.Keyword )]
    [DataRow( Tag.Uri )]
    [DataRow( Tag.UriScheme )]
    [DataRow( Tag.Charset )]
    [DataRow( Tag.NaturalLanguage )]
    [DataRow( Tag.MimeMediaType )]
    [DataRow( Tag.MemberAttrName )]
    [DataRow( Tag.StringUnassigned40 )]
    [DataRow( Tag.StringUnassigned43 )]
    [DataRow( Tag.StringUnassigned4B )]
    [DataRow( Tag.StringUnassigned4C )]
    [DataRow( Tag.StringUnassigned4D )]
    [DataRow( Tag.StringUnassigned4E )]
    [DataRow( Tag.StringUnassigned4F )]
    [DataRow( Tag.StringUnassigned50 )]
    [DataRow( Tag.StringUnassigned51 )]
    [DataRow( Tag.StringUnassigned52 )]
    [DataRow( Tag.StringUnassigned53 )]
    [DataRow( Tag.StringUnassigned54 )]
    [DataRow( Tag.StringUnassigned55 )]
    [DataRow( Tag.StringUnassigned56 )]
    [DataRow( Tag.StringUnassigned57 )]
    [DataRow( Tag.StringUnassigned58 )]
    [DataRow( Tag.StringUnassigned59 )]
    [DataRow( Tag.StringUnassigned5A )]
    [DataRow( Tag.StringUnassigned5B )]
    [DataRow( Tag.StringUnassigned5C )]
    [DataRow( Tag.StringUnassigned5D )]
    [DataRow( Tag.StringUnassigned5E )]
    [DataRow( Tag.StringUnassigned5F )]
    public async Task ReadValue_String_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, tag);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( "Lorem" );
    }

    [TestMethod]
    public async Task ReadValue_String_FewerBytesReturned_ThrowsEndOfStreamException()
    {
        var protocol = new IppProtocol();
        var mockStream = new MemoryStream(new byte[] { 0x00, 0x05 });
        var mockReader = new Mock<IppBinaryReader>(mockStream) { CallBase = true };
        mockReader.Setup(x => x.ReadBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new byte[] { 0x01, 0x02 });
        
        Func<Task> act = async () => await protocol.ReadValueAsync(mockReader.Object, Tag.Charset);
        
        await act.Should().ThrowAsync<EndOfStreamException>()
                 .WithMessage("Unexpected end of stream while reading string");
    }

    [TestMethod]
    [DataRow( Tag.OctetStringWithAnUnspecifiedFormat )]
    [DataRow( Tag.OctetStringUnassigned38 )]
    [DataRow( Tag.OctetStringUnassigned39 )]
    [DataRow( Tag.OctetStringUnassigned3A )]
    [DataRow( Tag.OctetStringUnassigned3B )]
    [DataRow( Tag.OctetStringUnassigned3C )]
    [DataRow( Tag.OctetStringUnassigned3D )]
    [DataRow( Tag.OctetStringUnassigned3E )]
    [DataRow( Tag.OctetStringUnassigned3F )]
    public async Task ReadValue_OctetStrings_ReturnsOctetString( Tag tag )
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using IppBinaryReader binaryReader = new( memoryStream );

        var result = await protocol.ReadValueAsync(binaryReader, tag);

        result.Should().BeOfType<OctetString>();
        ((OctetString)result).Value.Should().BeEquivalentTo(new byte[] { 0x4C, 0x6F, 0x72, 0x65, 0x6D });
    }

    [TestMethod]
    public async Task ReadValue_OctetString_FewerBytesReturned_ThrowsEndOfStreamException()
    {
        var protocol = new IppProtocol();
        var mockStream = new MemoryStream(new byte[] { 0x00, 0x05 });
        var mockReader = new Mock<IppBinaryReader>(mockStream) { CallBase = true };
        mockReader.Setup(x => x.ReadBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new byte[] { 0x01, 0x02 });
        
        Func<Task> act = async () => await protocol.ReadValueAsync(mockReader.Object, Tag.OctetStringWithAnUnspecifiedFormat);
        
        await act.Should().ThrowAsync<EndOfStreamException>()
                 .WithMessage("Unexpected end of stream while reading octet string");
    }

    [TestMethod]
    public async Task ReadValue_ExtendedTag_ReturnsExtendedValue()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new(new byte[] { 0x00, 0x06, 0x00, 0x00, 0x01, 0x02, 0xAA, 0xBB });
        using IppBinaryReader binaryReader = new(memoryStream);

        var result = await protocol.ReadValueAsync(binaryReader, Tag.Extended);

        result.Should().BeEquivalentTo(new ExtendedValue(0x00000102, new byte[] { 0xAA, 0xBB }));
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task ReadValue_ExtendedTag_WithPayloadShorterThanLengthPrefix_ThrowsArgumentException(bool useDisposedStream)
    {
        var protocol = new IppProtocol();
        IppBinaryReader binaryReader;
        if (useDisposedStream)
        {
            var ms = new MemoryStream();
            ms.Dispose();
            binaryReader = new IppBinaryReader(ms);
        }
        else
        {
            var ms = new MemoryStream(new byte[] { 0x00 });
            binaryReader = new IppBinaryReader(ms);
        }

        Func<Task> act = async () =>
        {
            try
            {
                await protocol.ReadValueAsync(binaryReader, Tag.Extended);
            }
            finally
            {
                binaryReader.Dispose();
            }
        };

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("Invalid extended value payload*")
           .Which.InnerException.Should().Match(ex => ex is EndOfStreamException || ex is ObjectDisposedException);
    }

    [TestMethod]
    public async Task ReadValue_ExtendedTag_LengthLessThan4_ThrowsArgumentException()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new(new byte[] { 0x00, 0x03, 0xAA, 0xBB, 0xCC });
        using IppBinaryReader binaryReader = new(memoryStream);

        Func<Task> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.Extended);

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("Expected extended value length >= 4, actual: 3*");
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task ReadValue_ExtendedTag_InvalidPayloadLength_ThrowsArgumentException(bool useDisposedStream)
    {
        var protocol = new IppProtocol();
        IppBinaryReader binaryReader;
        if (useDisposedStream)
        {
            var ms = new CustomExceptionStream(new byte[] { 0x00, 0x05, 0x00, 0x00, 0x01, 0x02 }, 2, new ObjectDisposedException("Stream"));
            binaryReader = new IppBinaryReader(ms);
        }
        else
        {
            var ms = new MemoryStream(new byte[] { 0x00, 0x05, 0x00, 0x00, 0x01, 0x02 });
            binaryReader = new IppBinaryReader(ms);
        }

        Func<Task> act = async () =>
        {
            try
            {
                await protocol.ReadValueAsync(binaryReader, Tag.Extended);
            }
            finally
            {
                binaryReader.Dispose();
            }
        };

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("Invalid extended value payload*")
           .Which.InnerException.Should().Match(ex => ex is EndOfStreamException || ex is ObjectDisposedException);
    }

    [TestMethod]
    public async Task ReadWriteValue_ExtendedValue_RoundTrips() {
        var protocol = new IppProtocol();
        var inputValue = new ExtendedValue(0x0A0B0C0D, new byte[] { 0xA1, 0xB2 });

        using MemoryStream writeStream = new();
        using IppBinaryWriter binaryWriter = new(writeStream);
        await protocol.WriteValueAsync(inputValue, binaryWriter);

        using IppBinaryReader binaryReader = new(new MemoryStream(writeStream.ToArray()));
        var result = await protocol.ReadValueAsync(binaryReader, Tag.Extended);

        result.Should().BeEquivalentTo(inputValue);
    }

    [TestMethod]
    public async Task ReadValue_ExtendedTag_FewerBytesReturned_ThrowsArgumentException()
    {
        var protocol = new IppProtocol();
        var mockStream = new MemoryStream(new byte[] { 0x00, 0x06, 0x00, 0x00, 0x00, 0x01 });
        var mockReader = new Mock<IppBinaryReader>(mockStream) { CallBase = true };
        mockReader.Setup(x => x.ReadBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new byte[] { 0xAA });
        
        Func<Task> act = async () => await protocol.ReadValueAsync(mockReader.Object, Tag.Extended);
        
        var exception = await act.Should().ThrowAsync<ArgumentException>()
                 .WithMessage("Invalid extended value payload*");
        exception.Which.InnerException.Should().BeOfType<EndOfStreamException>();
    }

    [TestMethod]
    public void ExtendedValue_WithExpression_CopiesAllValues()
    {
        var original = new ExtendedValue(unchecked((int)0xDEADBEEF), new byte[] { 0xAA, 0xBB, 0xCC });

        var copy = original with { };

        copy.Should().NotBeNull();
        copy.Should().BeEquivalentTo(original);
        copy.ExtendedTag.Should().Be(original.ExtendedTag);
        copy.Raw.Should().BeSameAs(original.Raw);
    }

    [TestMethod]
    public void ExtendedValue_Reflection_Setters_Coverage()
    {
        var value = new ExtendedValue(0, new byte[] { 0x01 });

        var type = typeof(ExtendedValue);
        var extendedTagSetter = type.GetProperty(nameof(ExtendedValue.ExtendedTag))?.SetMethod;
        var rawSetter = type.GetProperty(nameof(ExtendedValue.Raw))?.SetMethod;

        extendedTagSetter.Should().NotBeNull();
        rawSetter.Should().NotBeNull();

        extendedTagSetter!.Invoke(value, new object[] { 0x12345678 });
        rawSetter!.Invoke(value, new object[] { new byte[] { 0x99 } });

        value.ExtendedTag.Should().Be(0x12345678);
        value.Raw.Should().BeEquivalentTo(new byte[] { 0x99 });
    }

    [TestMethod]
    public async Task ReadValue_UnknownTag_ReturnsUnknownValue()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new(new byte[] { 0x00, 0x02, 0xDE, 0xAD });
        using IppBinaryReader binaryReader = new(memoryStream);

        var tag = (Tag)0x60;
        var result = await protocol.ReadValueAsync(binaryReader, tag);

        result.Should().BeEquivalentTo(new UnknownValue(tag, new byte[] { 0xDE, 0xAD }));
    }

    [TestMethod]
    public async Task ReadValue_UnknownTag_FewerBytesReturned_ThrowsArgumentException()
    {
        var protocol = new IppProtocol();
        var mockStream = new MemoryStream(new byte[] { 0x00, 0x05 });
        var mockReader = new Mock<IppBinaryReader>(mockStream) { CallBase = true };
        mockReader.Setup(x => x.ReadBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new byte[] { 0x01, 0x02 });
        
        Func<Task> act = async () => await protocol.ReadValueAsync(mockReader.Object, (Tag)0x60);
        
        var exception = await act.Should().ThrowAsync<ArgumentException>()
                 .WithMessage("Invalid unknown value payload*");
        exception.Which.InnerException.Should().BeOfType<EndOfStreamException>();
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task ReadValue_UnknownTag_WithPayloadShorterThanLengthPrefix_ThrowsArgumentException(bool useDisposedStream)
    {
        var protocol = new IppProtocol();
        IppBinaryReader binaryReader;
        if (useDisposedStream)
        {
            var ms = new MemoryStream();
            ms.Dispose();
            binaryReader = new IppBinaryReader(ms);
        }
        else
        {
            var ms = new MemoryStream(new byte[] { 0x00 });
            binaryReader = new IppBinaryReader(ms);
        }

        var tag = (Tag)0x60;
        Func<Task> act = async () =>
        {
            try
            {
                await protocol.ReadValueAsync(binaryReader, tag);
            }
            finally
            {
                binaryReader.Dispose();
            }
        };

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("Invalid unknown value payload*")
           .Which.InnerException.Should().Match(ex => ex is EndOfStreamException || ex is ObjectDisposedException);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task ReadValue_UnknownTag_WithDeclaredLengthGreaterThanPayload_ThrowsArgumentException(bool useDisposedStream)
    {
        var protocol = new IppProtocol();
        IppBinaryReader binaryReader;
        if (useDisposedStream)
        {
            var ms = new CustomExceptionStream(new byte[] { 0x00, 0x02, 0xDE }, 2, new ObjectDisposedException("Stream"));
            binaryReader = new IppBinaryReader(ms);
        }
        else
        {
            var ms = new MemoryStream(new byte[] { 0x00, 0x02, 0xDE });
            binaryReader = new IppBinaryReader(ms);
        }

        var tag = (Tag)0x60;
        Func<Task> act = async () =>
        {
            try
            {
                await protocol.ReadValueAsync(binaryReader, tag);
            }
            finally
            {
                binaryReader.Dispose();
            }
        };

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("Invalid unknown value payload*")
           .Which.InnerException.Should().Match(ex => ex is EndOfStreamException || ex is ObjectDisposedException);
    }

    [TestMethod]
    public async Task ReadValue_UnknownTag_WithNegativeDeclaredLength_ThrowsArgumentException()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new(new byte[] { 0xFF, 0xFF });
        using IppBinaryReader binaryReader = new(memoryStream);

        var tag = (Tag)0x60;
        Func<Task> act = async () => await protocol.ReadValueAsync(binaryReader, tag);

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("Invalid unknown value payload*");
    }

    [TestMethod]
    public async Task ReadWriteValue_UnknownValue_RoundTrips() {
        var protocol = new IppProtocol();
        var inputValue = new UnknownValue((Tag)0x60, new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });

        using MemoryStream writeStream = new();
        using IppBinaryWriter binaryWriter = new(writeStream);
        await protocol.WriteValueAsync(inputValue, binaryWriter);

        using IppBinaryReader binaryReader = new(new MemoryStream(writeStream.ToArray()));
        var result = await protocol.ReadValueAsync(binaryReader, inputValue.Tag);

        result.Should().BeEquivalentTo(inputValue);
    }

    [TestMethod]
    public void UnknownValue_WithExpression_CopiesAllValues()
    {
        var original = new UnknownValue((Tag)0x60, new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });

        var copy = original with { };

        copy.Should().NotBeNull();
        copy.Should().BeEquivalentTo(original);
        copy.Tag.Should().Be(original.Tag);
        copy.Raw.Should().BeSameAs(original.Raw);
    }

    [TestMethod]
    public void UnknownValue_Reflection_Setters_Coverage()
    {
        var value = new UnknownValue((Tag)0x60, new byte[] { 0x01 });

        var type = typeof(UnknownValue);
        var tagSetter = type.GetProperty(nameof(UnknownValue.Tag))?.SetMethod;
        var rawSetter = type.GetProperty(nameof(UnknownValue.Raw))?.SetMethod;

        tagSetter.Should().NotBeNull();
        rawSetter.Should().NotBeNull();

        tagSetter!.Invoke(value, new object[] { (Tag)0x61 });
        rawSetter!.Invoke(value, new object[] { new byte[] { 0x99 } });

        value.Tag.Should().Be((Tag)0x61);
        value.Raw.Should().BeEquivalentTo(new byte[] { 0x99 });
    }

    [TestMethod]
    [DataRow( Tag.Unsupported )]
    [DataRow( Tag.Unknown )]
    [DataRow( Tag.NoValue )]
    [DataRow( Tag.BegCollection )]
    [DataRow( Tag.EndCollection )]
    public async Task ReadValue_NoValue_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x00 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, tag);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( NoValue.Instance );
    }

    [TestMethod]
    public async Task ReadValue_BrokenNoValue_ThrowsArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x01, 0x00 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.NoValue);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }


    [TestMethod]
    [DataRow( Tag.Integer )]
    [DataRow( Tag.Enum )]
    [DataRow( Tag.IntegerUnassigned20 )]
    [DataRow( Tag.IntegerUnassigned24 )]
    [DataRow( Tag.IntegerUnassigned25 )]
    [DataRow( Tag.IntegerUnassigned26 )]
    [DataRow( Tag.IntegerUnassigned27 )]
    [DataRow( Tag.IntegerUnassigned28 )]
    [DataRow( Tag.IntegerUnassigned29 )]
    [DataRow( Tag.IntegerUnassigned2A )]
    [DataRow( Tag.IntegerUnassigned2B )]
    [DataRow( Tag.IntegerUnassigned2C )]
    [DataRow( Tag.IntegerUnassigned2D )]
    [DataRow( Tag.IntegerUnassigned2E )]
    [DataRow( Tag.IntegerUnassigned2F )]
    public async Task ReadValue_Int_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x10 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, tag);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( 16 );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF }, DisplayName = "Invalid second byte" )]
    public async Task ReadValue_Int_ThrowsArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.Integer);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x01, 0x00 }, false )]
    [DataRow( new byte[] { 0x00, 0x01, 0x01 }, true )]
    public async Task ReadValue_Bool_ReturnsCorrectResult( byte[] value, bool expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.Boolean);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().Be( expected );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x01, 0x02 } )]
    [DataRow( new byte[] { 0x00, 0x00, 0x00 } )]
    public async Task ReadValue_InvalidBool_ThrowsArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.Boolean);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2B, 0x02, 0x1E }, "12/31/1999 23:59:59 +02:30", DisplayName = "Time with negative offset" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2D, 0x02, 0x1E }, "12/31/1999 23:59:59 -02:30", DisplayName = "Time with positive offset" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 }, "01/01/0001 01:01:01 +00:00", DisplayName = "Minimal DateTime" )]
    public async Task ReadValue_DateTimeOffset_ReturnsCorrectResult( byte[] value, string expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.DateTime);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().Be( DateTimeOffset.Parse( expected, CultureInfo.InvariantCulture ) );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 }, DisplayName = "Invalid second byte" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00 }, DisplayName = "Invalid offset sign" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x0F, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 }, DisplayName = "Invalid month" )]
    public async Task ReadValue_InvalidDateTimeOffset_ThrowsArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.DateTime);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 }, int.MinValue, int.MinValue )]
    [DataRow( new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF }, int.MinValue, int.MaxValue )]
    [DataRow( new byte[] { 0x00, 0x08, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF }, int.MaxValue, int.MaxValue )]
    public async Task ReadValue_Range_ReturnsCorrectResult( byte[] value, int lower, int upper )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.RangeOfInteger);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new SharpIpp.Protocol.Models.Range( lower, upper ) );
    }


    [TestMethod()]
    [DataRow( new byte[] { 0x01, 0x08, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF }, DisplayName = "Invalid first byte" )]
    [DataRow( new byte[] { 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF }, DisplayName = "Invalid second byte" )]
    public async Task ReadValue_InvalidRange_ShouldThrowArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.RangeOfInteger);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, 0, int.MaxValue, ResolutionUnit.DotsPerCm )]
    [DataRow( new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 }, 0, int.MaxValue, ResolutionUnit.DotsPerInch )]
    [DataRow( new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerCm )]
    [DataRow( new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 }, int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerInch )]
    public async Task ReadValue_Resolution_ReturnsCorrectResult( byte[] bytes, int width, int height, ResolutionUnit unit )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( bytes, 0, bytes.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.Resolution);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new Resolution( width, height, unit ) );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x01, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, DisplayName = "Invalid first byte" )]
    [DataRow( new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, DisplayName = "Invalid second byte" )]
    public async Task ReadValue_InvalidResolution_ThrowsArgumentException( byte[] bytes )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( bytes, 0, bytes.Length );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, Tag.Resolution);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task ReadValue_InvalidTag_ThrowsArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<object>> act = async () => await protocol.ReadValueAsync(binaryReader, (Tag)0x01 );
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task ReadAttribute_OneAttribute_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x16, 0x69, 0x70, 0x70, 0x2D, 0x76, 0x65, 0x72, 0x73, 0x69,
            0x6F, 0x6E, 0x73, 0x2D, 0x73, 0x75, 0x70, 0x70, 0x6F, 0x72, 0x74, 0x65, 0x64, 0x00, 0x03, 0x31, 0x2E, 0x31 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<IppAttribute>> act = async () => await protocol.ReadAttributeAsync( Tag.Keyword, binaryReader, null, null, Encoding.ASCII );
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ) );
    }

    [TestMethod]
    public async Task ReadAttribute_BegCollection_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new(new byte[] { 0x00, 0x11, 0x6D, 0x65, 0x64, 0x69, 0x61, 0x2D, 0x63, 0x6F, 0x6C,
            0x2D, 0x64, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x00, 0x00 });
        using IppBinaryReader binaryReader = new(memoryStream);
        // Act
        Func<Task<IppAttribute>> act = async () => await protocol.ReadAttributeAsync(Tag.BegCollection, binaryReader, null, null, Encoding.ASCII);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo(new IppAttribute(Tag.BegCollection, IppAttributeNames.MediaColDefault, NoValue.Instance));
    }

    [TestMethod]
    public async Task ReadAttribute_SecondSimilarAttribute_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x00, 0x00, 0x03, 0x31, 0x2E, 0x31 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        var previousAttribute = new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion( 1, 0 ).ToString() );
        // Act
        Func<Task<IppAttribute>> act = async () => await protocol.ReadAttributeAsync( Tag.Keyword, binaryReader, previousAttribute, null, Encoding.ASCII);
        // Assert
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( new IppAttribute( Tag.Keyword, IppAttributeNames.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ) );
    }

    [TestMethod]
    public async Task ReadAttribute_AttributeWithoutName_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x00, 0x00, 0x03, 0x31, 0x2E, 0x31 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task<IppAttribute>> act = async () => await protocol.ReadAttributeAsync( Tag.Keyword, binaryReader, null, null, Encoding.ASCII);
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod()]
    public async Task ReadIppResponseAsync_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public async Task ReadValue_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<object>> act = async () => await protocol.ReadValueAsync( null, Tag.Charset );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public async Task ReadAttribute_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<IppAttribute>> act = async () => await protocol.ReadAttributeAsync( Tag.Keyword, null, null, null, Encoding.ASCII );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task ReadAttribute_AttributeNameTooShort_ShouldThrowEndOfStreamException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using var stream = new MemoryStream(new byte[] { 0x00, 0x02 });
        var mockReader = new Mock<IppBinaryReader>(stream) { CallBase = true };
        mockReader.Setup(r => r.ReadBytesAsync(2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new byte[1]);

        // Act
        Func<Task<IppAttribute>> act = async () => await protocol.ReadAttributeAsync(Tag.Keyword, mockReader.Object, null, null, Encoding.ASCII);

        // Assert
        var exceptionAssertion = await act.Should().ThrowAsync<EndOfStreamException>();
        exceptionAssertion.Which.Message.Should().Be("Unexpected end of stream while reading attribute name");
    }

    [TestMethod()]
    public async Task WriteIppResponseAsync_MessageIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream requestStream = new();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( null, requestStream );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public async Task WriteIppResponseAsync_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        var message = new IppResponseMessage
        {
            RequestId = 123
        };
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( message, null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
// Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_NoCharset_ShouldThrowArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123
        };
        message.JobAttributes.Add( new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1 ) );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppRequestAsync( message, memoryStream );
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_CharsetNotString_ShouldThrowArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123
        };
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, 1 ) );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppRequestAsync( message, memoryStream );
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task WriteIppRequestAsync_MultipleAttributes_ShouldProvideFullLambdaCoverage()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.PrintJob,
            RequestId = 123
        };
        message.OperationAttributes.Add( new IppAttribute( Tag.Integer, IppAttributeNames.Copies, 1 ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.Integer, IppAttributeNames.Copies, 2 ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, IppAttributeNames.JobName, "utf-8" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" ) );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppRequestAsync( message, memoryStream );
        
        // Assert
        await act.Should().NotThrowAsync();
    }

    [TestMethod]
    public async Task WriteIppResponseAsync_NoCharset_ShouldThrowArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppResponseMessage
        {
            StatusCode = IppStatusCode.SuccessfulOk,
            RequestId = 123
        };
        message.JobAttributes.Add( new List<IppAttribute> { new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1 ) } );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( message, memoryStream );
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task WriteIppResponseAsync_CharsetNotString_ShouldThrowArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppResponseMessage
        {
            StatusCode = IppStatusCode.SuccessfulOk,
            RequestId = 123
        };
        message.OperationAttributes.Add( new List<IppAttribute> { new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, 1 ) } );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( message, memoryStream );
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [TestMethod]
    public async Task WriteIppResponseAsync_MultipleAttributes_ShouldProvideFullLambdaCoverage()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        var message = new IppResponseMessage
        {
            StatusCode = IppStatusCode.SuccessfulOk,
            RequestId = 123
        };
        message.OperationAttributes.Add( new List<IppAttribute> 
        { 
            new IppAttribute( Tag.Integer, IppAttributeNames.Copies, 1 ),
            new IppAttribute( Tag.Charset, IppAttributeNames.JobName, "utf-8" ),
            new IppAttribute( Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8" )
        } );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( message, memoryStream );
        
        // Assert
        await act.Should().NotThrowAsync();
    }

    [TestMethod]
    public async Task GetNormalizedName_PrevBegCollection_ShouldReturnPrevName()
    {
        // Case for verifying 1setOf Collection support (formerly line 418 break)
        var prevAttribute = new IppAttribute(Tag.BegCollection, "test-collection", NoValue.Instance);

        var name = IppProtocol.GetNormalizedName(Tag.BegCollection, "", prevAttribute, null);
        name.Should().Be("test-collection");
    }

    [TestMethod]
    public async Task GetNormalizedName_PrevEndCollection_NoPrevBegCollection_ShouldThrow()
    {
        // Case for Line 422 (break) coverage
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);

        Func<Task> act = async () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public async Task GetNormalizedName_PrevEndCollection_PrevBegCollectionNoName_ShouldThrow()
    {
        // Case for Line 422/420 (condition false) coverage
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        var prevBeg = new IppAttribute(Tag.BegCollection, "", NoValue.Instance); // Empty name

        Func<Task> act = async () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, prevBeg);

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public async Task GetNormalizedName_Default_PrevNameEmpty_ShouldThrow()
    {
        // Case for Line 428 (break) coverage
        // Use a tag that hits default (e.g. Integer) but has empty name
        var prevAttribute = new IppAttribute(Tag.Integer, "", 1);

        Func<Task> act = async () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);

        (await act.Should().ThrowAsync<ArgumentException>()).WithMessage("0 length attribute name found not in a 1setOf");
    }

    /// <summary>
    /// Guards against a regression where encoding is incorrectly reset to ASCII when entering a new section.
    /// Per RFC 8011 Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€žĂ„ÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â§4.1.4, attributes-charset applies to the entire message, not just the operation section.
    /// A non-ASCII text attribute (e.g. job-name "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ") in the job-attributes section must be decoded
    /// using the charset negotiated in operation-attributes, not ASCII.
    /// </summary>
    [TestMethod]
    public async Task ReadIppRequestAsync_Utf8JobNameInJobSection_ShouldDecodeCorrectly()
    {
        // Arrange
        // Payload: version 1.1, op PrintJob, request-id 1
        //   operation-attributes-tag (0x01)
        //     attributes-charset = "utf-8"   (Tag.Charset = 0x47)
        //   job-attributes-tag (0x02)
        //     job-name = "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ" in UTF-8         (Tag.NameWithoutLanguage = 0x42)
        //       "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ" = 0xC3 0x84 in UTF-8
        //   end-of-attributes-tag (0x03)
        var bytes = new byte[]
        {
            0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, // header
            0x01,                                             // operation-attributes-tag
            0x47,                                             // Tag.Charset
            0x00, 0x12,                                       // name length: 18
            // "attributes-charset"
            0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73,
            0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74,
            0x00, 0x05,                                       // value length: 5
            0x75, 0x74, 0x66, 0x2D, 0x38,                   // "utf-8"
            0x02,                                             // job-attributes-tag
            0x42,                                             // Tag.NameWithoutLanguage
            0x00, 0x08,                                       // name length: 8
            0x6A, 0x6F, 0x62, 0x2D, 0x6E, 0x61, 0x6D, 0x65, // "job-name"
            0x00, 0x02,                                       // value length: 2
            0xC3, 0x84,                                       // "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ" in UTF-8
            0x03                                              // end-of-attributes-tag
        };
        var protocol = new IppProtocol();
        using var stream = new MemoryStream(bytes);

        // Act
        var result = await protocol.ReadIppRequestAsync(stream);

        // Assert
        result.JobAttributes.Should().HaveCount(1);
        result.JobAttributes[0].Name.Should().Be(IppAttributeNames.JobName);
        result.JobAttributes[0].Value.Should().Be("\u00C4"); // "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ"
    }

    /// <summary>
    /// Guards against a regression where encoding is incorrectly reset to ASCII when entering a new section.
    /// Per RFC 8011 Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€žĂ„ÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â§4.1.4, attributes-charset applies to the entire message, not just the operation section.
    /// A non-ASCII text attribute (e.g. job-name "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ") in the job-attributes section must be decoded
    /// using the charset negotiated in operation-attributes, not ASCII.
    /// </summary>
    [TestMethod]
    public async Task ReadIppResponseAsync_Utf8JobNameInJobSection_ShouldDecodeCorrectly()
    {
        // Arrange
        // Payload: version 1.1, status SuccessfulOk, request-id 1
        //   operation-attributes-tag (0x01)
        //     attributes-charset = "utf-8"   (Tag.Charset = 0x47)
        //   job-attributes-tag (0x02)
        //     job-name = "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ" in UTF-8         (Tag.NameWithoutLanguage = 0x42)
        //       "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ" = 0xC3 0x84 in UTF-8
        //   end-of-attributes-tag (0x03)
        var bytes = new byte[]
        {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, // header
            0x01,                                             // operation-attributes-tag
            0x47,                                             // Tag.Charset
            0x00, 0x12,                                       // name length: 18
            // "attributes-charset"
            0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73,
            0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74,
            0x00, 0x05,                                       // value length: 5
            0x75, 0x74, 0x66, 0x2D, 0x38,                   // "utf-8"
            0x02,                                             // job-attributes-tag
            0x42,                                             // Tag.NameWithoutLanguage
            0x00, 0x08,                                       // name length: 8
            0x6A, 0x6F, 0x62, 0x2D, 0x6E, 0x61, 0x6D, 0x65, // "job-name"
            0x00, 0x02,                                       // value length: 2
            0xC3, 0x84,                                       // "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ" in UTF-8
            0x03                                              // end-of-attributes-tag
        };
        var protocol = new IppProtocol();
        using var stream = new MemoryStream(bytes);

        // Act
        var result = await protocol.ReadIppResponseAsync(stream);

        // Assert
        result.JobAttributes.Should().HaveCount(1);
        result.JobAttributes[0].Should().ContainSingle();
        result.JobAttributes[0][0].Name.Should().Be(IppAttributeNames.JobName);
        result.JobAttributes[0][0].Value.Should().Be("\u00C4"); // "Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬Ă‚Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â€šÂ¬ÄąË‡Ä‚â€šĂ‚Â¬Ä‚â€žĂ„â€¦Ä‚â€ąĂ˘â‚¬Ë‡Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€žĂ˘â‚¬Â¦Ä‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬Ă„â€¦Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚ÂĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬Ä…Ä‚â€šĂ‚ÂÄ‚â€žĂ˘â‚¬ĹˇÄ‚â€ąĂ‚ÂĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚â€žĂ˘â‚¬Â¦Ă„â€šĂ˘â‚¬Ä…Ä‚ËĂ˘â€šÂ¬Ă‹â€ˇĂ„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă‹â€ˇÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ˘â‚¬ĹˇÄ‚â€šĂ‚Â¬Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ä‚â€šĂ‚Â¦Ă„â€šĂ˘â‚¬ĹľÄ‚ËĂ˘â€šÂ¬ÄąË‡Ă„â€šĂ‹ÂÄ‚ËĂ˘â‚¬ĹˇĂ‚Â¬Ă„Ä…Ă„ÄľÄ‚â€žĂ˘â‚¬ĹˇÄ‚ËĂ˘â€šÂ¬ÄąÄľĂ„â€šĂ˘â‚¬ĹľÄ‚â€žĂ„Äľ"
    }

    [TestMethod]
    public async Task GetNormalizedName_PrevMember_NameIsValue()
    {
        var prevAttribute = new IppAttribute(Tag.MemberAttrName, "", "member-name");
        var name = IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);
        name.Should().Be("member-name");
    }

    [TestMethod]
    public async Task GetNormalizedName_EndCollection_TakesPrevBegCollectionName()
    {
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        var prevBeg = new IppAttribute(Tag.BegCollection, "collection-name", NoValue.Instance);

        var name = IppProtocol.GetNormalizedName(Tag.BegCollection, "", prevAttribute, prevBeg);
        name.Should().Be("collection-name");
    }
    [TestMethod]
    public async Task ReadIppRequestAsync_MaxMessageAttributesExceeded_ThrowsException()
    {
        var protocol = new IppProtocol { MaxMessageAttributesCount = 0 };
        // 0x01 = OperationAttributesTag, 0x47 = Charset tag (1 attribute)
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38, 0x03 } );
        Func<Task> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorRequestEntityTooLarge);
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_InvalidSectionTag_ThrowsException()
    {
        var protocol = new IppProtocol();
        // 0x50 is invalid section tag
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x50 } );
        Func<Task> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_MismatchedEndCollection_ThrowsException()
    {
        var protocol = new IppProtocol();
        // 0x01 = OperationAttributesTag, 0x37 = EndCollection (no BegCollection)
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x37, 0x00, 0x00, 0x00, 0x00, 0x03 } );
        Func<Task> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_MaxMessageAttributesNull_ShouldSucceed()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesCount = null, MaxMessageAttributesBytes = null };
        // 0x01 = OperationAttributesTag, 0x47 = Charset tag (1 attribute)
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38, 0x03 } );

        // Act
        var result = await protocol.ReadIppRequestAsync( requestStream );

        // Assert
        result.OperationAttributes.Should().HaveCount(1);
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_MaxMessageAttributesNull_ShouldSucceed()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesCount = null, MaxMessageAttributesBytes = null };
        using MemoryStream requestStream = new( new byte[] {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B,
            0x01, // operation-attributes-tag
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x03 // EndOfAttributesTag
        } );

        // Act
        var result = await protocol.ReadIppResponseAsync( requestStream );

        // Assert
        result.OperationAttributes.Should().HaveCount( 1 );
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_MaxMessageAttributesBytesExceeded_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesBytes = 10 };
        // Request bytes are ~38 bytes
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x01, 0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38, 0x03 } );
        
        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync( requestStream );
        
        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorRequestEntityTooLarge);
        ex.Which.Message.Should().Contain("Maximum attribute bytes limit of 10 exceeded.");
    }

    [TestMethod]
    public async Task ReadIppResponseAsync_MaxMessageAttributesBytesExceeded_ThrowsIppResponseException()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesBytes = 10 };
        using MemoryStream requestStream = new( new byte[] {
            0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B,
            0x01, // operation-attributes-tag
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x03 // EndOfAttributesTag
        } );
        
        // Act
        Func<Task> act = async () => await protocol.ReadIppResponseAsync( requestStream );
        
        // Assert
        var ex = await act.Should().ThrowAsync<IppResponseException>();
        ex.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("Maximum attribute bytes limit of 10 exceeded.");
    }

    [TestMethod]
    public async Task ReadSections_WithNonCountingStream_Response_ShouldNotThrow()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesBytes = 100 };
        // Empty operation attributes section (0x01, 0x03)
        using var ms = new MemoryStream(new byte[] { 0x01, 0x03 });
        using var reader = new IppBinaryReader(ms);
        var response = new IppResponseMessage();

        // Act
        var method = typeof(IppProtocol).GetMethod("ReadSectionsAsync", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
            null,
            new[] { typeof(IppBinaryReader), typeof(IIppResponseMessage), typeof(System.Threading.CancellationToken) },
            null);
        
        Func<Task> act = async () => await (Task)method!.Invoke(protocol, new object[] { reader, response, System.Threading.CancellationToken.None })!;

        // Assert
        await act.Should().NotThrowAsync();
    }

    [TestMethod]
    public async Task ReadSections_WithNonCountingStream_Request_ShouldNotThrow()
    {
        // Arrange
        var protocol = new IppProtocol { MaxMessageAttributesBytes = 100 };
        // Empty operation attributes section (0x01, 0x03)
        using var ms = new MemoryStream(new byte[] { 0x01, 0x03 });
        using var reader = new IppBinaryReader(ms);
        var request = new IppRequestMessage();

        // Act
        var method = typeof(IppProtocol).GetMethod("ReadSectionsAsync", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
            null,
            new[] { typeof(IppBinaryReader), typeof(IIppRequestMessage), typeof(System.Threading.CancellationToken) },
            null);
        
        Func<Task> act = async () => await (Task)method!.Invoke(protocol, new object[] { reader, request, System.Threading.CancellationToken.None })!;

        // Assert
        await act.Should().NotThrowAsync();
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_NonSeekableStream_ShouldSucceed()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01, // version
            0x00, 0x02, // Print-Job operation
            0x00, 0x00, 0x00, 0x01, // request-id
            0x01, // operation-attributes-tag
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38, // attributes-charset = utf-8
            0x12, 0x00, 0x0C, 0x6d, 0x79, 0x2d, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x00, // Tag.Unknown, name: "my-attribute", value len: 0
            0x7F, 0x00, 0x0B, 0x6d, 0x79, 0x2d, 0x65, 0x78, 0x74, 0x65, 0x6e, 0x64, 0x65, 0x64, 0x00, 0x05, 0x00, 0x00, 0x00, 0x01, 0x00, // Tag.Extended, name: "my-extended", value len: 5, extendedTag: 1, raw: [0]
            0x03 // EndOfAttributesTag
        };
        using var memoryStream = new MemoryStream(bytes);
        using var nonSeekableStream = new NonSeekableStream(memoryStream);

        // Act
        var result = await protocol.ReadIppRequestAsync(nonSeekableStream);

        // Assert
        result.OperationAttributes.Should().HaveCount(3);
        result.OperationAttributes[0].Tag.Should().Be(Tag.Charset);
        result.OperationAttributes[1].Tag.Should().Be(Tag.NoValue);
        result.OperationAttributes[2].Tag.Should().Be(Tag.Extended);
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_NegativeNameLength_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0xFF, 0xFF // name length -1
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        ex.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("Attribute name length cannot be negative");
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_PrematureEndOfStream_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0x00, 0x12, 0x61, 0x74 // name length 18, but only 2 bytes provided
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        ex.Which.InnerException.Should().BeOfType<EndOfStreamException>();
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_NegativeOctetStringLength_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x30, 0x00, 0x06, 0x6d, 0x79, 0x2d, 0x6f, 0x63, 0x74, 0xFF, 0xFF // Tag.OctetString, name "my-oct", value length -1
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("OctetString length cannot be negative");
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_PrematureEOFOctetString_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x30, 0x00, 0x06, 0x6d, 0x79, 0x2d, 0x6f, 0x63, 0x74, 0x00, 0x05, 0x01, 0x02 // Tag.OctetString, value length 5, but only 2 bytes provided
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.InnerException.Should().BeOfType<EndOfStreamException>();
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_NegativeUnknownLength_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x60, 0x00, 0x06, 0x6d, 0x79, 0x2d, 0x75, 0x6e, 0x6b, 0xFF, 0xFF // Tag.Unknown (unrecognized tag 0x60), name "my-unk", value length -1
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("Invalid unknown value payload");
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_ExtendedLengthLessThanFour_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x7F, 0x00, 0x06, 0x6d, 0x79, 0x2d, 0x65, 0x78, 0x74, 0x00, 0x02, 0x00, 0x01 // Tag.Extended, name "my-ext", value length 2 (invalid, < 4)
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("Expected extended value length >= 4");
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_NegativeStringLength_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x01,
            0x01,
            0x47, 0x00, 0x12, 0x61, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73, 0x2D, 0x63, 0x68, 0x61, 0x72, 0x73, 0x65, 0x74, 0x00, 0x05, 0x75, 0x74, 0x66, 0x2D, 0x38,
            0x44, 0x00, 0x06, 0x6d, 0x79, 0x2d, 0x6b, 0x65, 0x79, 0xFF, 0xFF // Tag.Keyword, name "my-key", value length -1
        };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.InnerException.Should().BeOfType<ArgumentException>()
            .Which.Message.Should().Contain("String length cannot be negative");
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_TooShortInitialMessage_ThrowsIppRequestException()
    {
        // Arrange
        var protocol = new IppProtocol();
        byte[] bytes = new byte[] { 0x01, 0x01, 0x00 };
        using var memoryStream = new MemoryStream(bytes);

        // Act
        Func<Task> act = async () => await protocol.ReadIppRequestAsync(memoryStream);

        // Assert
        var ex = await act.Should().ThrowAsync<IppRequestException>();
        ex.Which.Message.Should().Be("Failed to parse initial ipp request message.");
        ex.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        ex.Which.InnerException.Should().BeOfType<EndOfStreamException>();
    }

    [TestMethod]
    [DataRow( Tag.TextWithoutLanguage )]
    [DataRow( Tag.NameWithoutLanguage )]
    [DataRow( Tag.Keyword )]
    [DataRow( Tag.Uri )]
    public async Task ReadValue_IncompleteString_ThrowsEndOfStreamException( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        // Length prefix is 5, but we only provide 3 bytes of payload
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x05, 0x41, 0x41, 0x41 } );
        using IppBinaryReader binaryReader = new( memoryStream );
        // Act
        Func<Task> act = async () => await protocol.ReadValueAsync(binaryReader, tag);
        // Assert
        (await act.Should().ThrowAsync<EndOfStreamException>()).WithMessage( "*Attempted to read past the end of the stream*" );
    }

    [TestMethod]
    public async Task ReadIppRequestAsync_ValidCollection_ShouldSucceed()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Payload:
        // Version: 1.1
        // Operation: PrintJob (0x0002)
        // Request ID: 123 (0x0000007B)
        // SectionTag: OperationAttributesTag (0x01)
        // Tag: BegCollection (0x34)
        //   Name: "media-col" (length 9) -> 0x6D, 0x65, 0x64, 0x69, 0x61, 0x2D, 0x63, 0x6F, 0x6C
        //   Value: 0 length
        // Tag: EndCollection (0x37)
        //   Name: 0 length
        //   Value: 0 length
        // SectionTag: EndOfAttributesTag (0x03)
        byte[] bytes = new byte[] {
            0x01, 0x01,
            0x00, 0x02,
            0x00, 0x00, 0x00, 0x7B,
            0x01,
            0x34, 0x00, 0x09, 0x6D, 0x65, 0x64, 0x69, 0x61, 0x2D, 0x63, 0x6F, 0x6C, 0x00, 0x00,
            0x37, 0x00, 0x00, 0x00, 0x00,
            0x03
        };
        using MemoryStream requestStream = new( bytes );

        // Act
        var result = await protocol.ReadIppRequestAsync( requestStream );

        // Assert
        result.OperationAttributes.Should().HaveCount( 2 );
        result.OperationAttributes[ 0 ].Tag.Should().Be( Tag.BegCollection );
        result.OperationAttributes[ 0 ].Name.Should().Be( "media-col" );
        result.OperationAttributes[ 1 ].Tag.Should().Be( Tag.EndCollection );
        result.OperationAttributes[ 1 ].Name.Should().Be( string.Empty );
    }

    private class NonSeekableStream : Stream
    {
        private readonly Stream _underlying;

        public NonSeekableStream(Stream underlying)
        {
            _underlying = underlying;
        }

        public override bool CanRead => _underlying.CanRead;
        public override bool CanSeek => false;
        public override bool CanWrite => _underlying.CanWrite;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush() => _underlying.Flush();
        public override int Read(byte[] buffer, int offset, int count) => _underlying.Read(buffer, offset, count);
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken) =>
            _underlying.ReadAsync(buffer, offset, count, cancellationToken);
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => _underlying.Write(buffer, offset, count);
    }

    private class ThrowingStream : Stream
    {
        private readonly MemoryStream _underlying;
        private bool _shouldThrow;

        public ThrowingStream(byte[] headerBytes)
        {
            _underlying = new MemoryStream(headerBytes);
        }

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush() {}

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_shouldThrow)
            {
                throw new IOException("Simulated stream read failure");
            }
            int read = _underlying.Read(buffer, offset, count);
            if (_underlying.Position >= _underlying.Length)
            {
                _shouldThrow = true;
            }
            return read;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            if (_shouldThrow)
            {
                throw new IOException("Simulated stream read failure");
            }
            int read = await _underlying.ReadAsync(buffer, offset, count, cancellationToken);
            if (_underlying.Position >= _underlying.Length)
            {
                _shouldThrow = true;
            }
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }

    private class CustomExceptionStream : Stream
    {
        private readonly MemoryStream _underlying;
        private readonly int _throwAfterBytes;
        private int _bytesRead;
        private readonly Exception _exceptionToThrow;

        public CustomExceptionStream(byte[] data, int throwAfterBytes, Exception exceptionToThrow)
        {
            _underlying = new MemoryStream(data);
            _throwAfterBytes = throwAfterBytes;
            _exceptionToThrow = exceptionToThrow;
        }

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush() => _underlying.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_bytesRead >= _throwAfterBytes)
            {
                throw _exceptionToThrow;
            }
            int read = _underlying.Read(buffer, offset, count);
            _bytesRead += read;
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}
