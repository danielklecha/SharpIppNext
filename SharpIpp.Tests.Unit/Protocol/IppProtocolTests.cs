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

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppProtocolTests
{
    [TestMethod()]
    public void WriteValue_NoValue_ShouldBeWritten()
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        protocol.WriteValue( NoValue.Instance, binaryWriter );
        memoryStream.ToArray().Should().Equal( 0x00, 0x00 );
    }

    [DataTestMethod]
    [DataRow( true, new byte[] { 0x00, 0x01, 0x01 } )]
    [DataRow( false, new byte[] { 0x00, 0x01, 0x00 } )]
    public void WriteValue_Boolean_ShouldBeWritten( bool value, byte[] expected )
    {
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        protocol.WriteValue( value, binaryWriter );
        memoryStream.ToArray().Should().Equal( expected );
    }

    [DataTestMethod]
    [DataRow( "12/31/1999 23:59:59 +02:30", new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2B, 0x02, 0x1E } )]
    [DataRow( "12/31/1999 23:59:59 -02:30", new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2D, 0x02, 0x1E } )]
    [DataRow( "01/01/0001 01:01:01 +00:00", new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 } )]
    public void WriteValue_DateTimeOffset_ShouldBeWritten( string value, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteValue( DateTimeOffset.Parse( value, CultureInfo.InvariantCulture ), binaryWriter );
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [DataTestMethod]
    [DataRow( int.MinValue, new byte[] { 0x00, 0x04, 0x80, 0x00, 0x00, 0x00 } )]
    [DataRow( 0, new byte[] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 } )]
    [DataRow( int.MaxValue, new byte[] { 0x00, 0x04, 0x7F, 0xFF, 0xFF, 0xFF } )]
    public void WriteValue_Int32_ShouldBeWritten( int value, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteValue( value, binaryWriter );
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [DataTestMethod]
    [DataRow( int.MinValue, int.MinValue, new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 } )]
    [DataRow( int.MinValue, int.MaxValue, new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF } )]
    [DataRow( int.MaxValue, int.MaxValue, new byte[] { 0x00, 0x08, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF } )]
    public void WriteValue_Range_ShouldBeWritten( int lower, int upper, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteValue( new SharpIpp.Protocol.Models.Range( lower, upper ), binaryWriter );
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [DataTestMethod]
    [DataRow( 0, int.MaxValue, ResolutionUnit.DotsPerCm, new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 } )]
    [DataRow( 0, int.MaxValue, ResolutionUnit.DotsPerInch, new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 } )]
    [DataRow( int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerCm, new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 } )]
    [DataRow( int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerInch, new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 } )]
    public void WriteValue_Resolution_ShouldBeWritten( int width, int height, ResolutionUnit unit, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteValue( new Resolution( width, height, unit ), binaryWriter );
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [DataTestMethod]
    [DataRow( "en-us", "Lorem", new byte[] { 0x00, 0x0E, 0x00, 0x05, 0x65, 0x6E, 0x2D, 0x75, 0x73, 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } )]
    public void Write_StringWithLanguage_ShouldBeWritten( string language, string text, byte[] expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteValue( new StringWithLanguage( language, text ), binaryWriter );
        // Assert
        memoryStream.ToArray().Should().Equal( expected );
    }

    [TestMethod]
    public void Write_String_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteValue( "Lorem", binaryWriter );
        // Assert
        memoryStream.ToArray().Should().Equal( 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D );
    }

    [TestMethod]
    public void WriteValue_UnsupportedType_ThrowsArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        Action act = () => protocol.WriteValue( 123L, binaryWriter );
        // Assert
        act.Should().Throw<ArgumentException>();
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
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.JobName, "Test Job" ) );
        message.JobAttributes.Add( new IppAttribute(Tag.Integer, JobAttribute.Copies, 1 ) );
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
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.JobName, "Test Job" ) );
        message.JobAttributes.Add( new IppAttribute(Tag.Integer, JobAttribute.Copies, 1 ) );
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
        using MemoryStream requestStream = new( new byte[] { 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x7B, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
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
    public void WriteSection_EmptyListOfAttributes_ShouldNotWriteAnything()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteSection( SectionTag.OperationAttributesTag, new List<IppAttribute>(), binaryWriter, Encoding.ASCII );
        // Assert
        memoryStream.Length.Should().Be( 0 );
    }

    [TestMethod()]
    public void WriteSection_ListIsNull_ShouldNotWriteAnything()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => protocol.WriteSection( SectionTag.OperationAttributesTag, null, binaryWriter, Encoding.ASCII );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod()]
    public void WriteSection_StreamIsNull_ShouldNotWriteAnything()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => protocol.WriteSection( SectionTag.OperationAttributesTag, new List<IppAttribute>
        {
            new( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion(1,0).ToString() )
        }, null, Encoding.ASCII );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod()]
    public void WriteSection_OneAttribute_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteSection( SectionTag.OperationAttributesTag, new List<IppAttribute>
        {
            new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion(1,0).ToString() )
        }, binaryWriter, Encoding.ASCII );
        // Assert
        memoryStream.ToArray().Should().Equal( 0x01, 0x44, 0x00, 0x16, 0x69, 0x70, 0x70, 0x2D, 0x76, 0x65, 0x72, 0x73,
            0x69, 0x6F, 0x6E, 0x73, 0x2D, 0x73, 0x75, 0x70, 0x70, 0x6F, 0x72, 0x74, 0x65, 0x64, 0x00, 0x03, 0x31, 0x2E, 0x30 );
    }

    [TestMethod]
    public void WriteAttribute_BegCollection_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new(memoryStream);
        // Act
        protocol.WriteAttribute(
            binaryWriter,
            new IppAttribute(Tag.BegCollection, PrinterAttribute.MediaColDefault, NoValue.Instance),
            null,
            Encoding.ASCII);
        // Assert
        memoryStream.ToArray().Should().Equal(0x34, 0x00, 0x11, 0x6D, 0x65, 0x64, 0x69, 0x61, 0x2D, 0x63, 0x6F, 0x6C,
            0x2D, 0x64, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x00, 0x00);
    }

    [TestMethod()]
    public void WriteAttribute_OneAttribute_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteAttribute(
            binaryWriter,
            new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ),
            null,
            Encoding.ASCII);
        // Assert
        memoryStream.ToArray().Should().Equal( 0x44, 0x00, 0x16, 0x69, 0x70, 0x70, 0x2D, 0x76, 0x65, 0x72, 0x73, 0x69,
            0x6F, 0x6E, 0x73, 0x2D, 0x73, 0x75, 0x70, 0x70, 0x6F, 0x72, 0x74, 0x65, 0x64, 0x00, 0x03, 0x31, 0x2E, 0x31 );
    }

    [TestMethod()]
    public void WriteAttribute_SecondSimilarAttribute_ShouldBeWritten()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryWriter binaryWriter = new( memoryStream );
        // Act
        protocol.WriteAttribute(
            binaryWriter,
            new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ),
            new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion( 1, 0 ).ToString() ),
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
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" )
        };
        message.OperationAttributes.Add( operationAttrs );
        var jobAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, JobAttribute.Copies, 1 )
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
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ),
            new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" )
        };
        message.OperationAttributes.Add( operationAttrs );
        var jobAttrs = new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, JobAttribute.Copies, 1 )
        };
        message.JobAttributes.Add( jobAttrs );
        (await act.Should().NotThrowAsync()).Which.Should().BeEquivalentTo( message );
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

    [DataTestMethod]
    [DataRow( Tag.TextWithLanguage )]
    [DataRow( Tag.NameWithLanguage )]
    public void ReadValue_StringWithLanguage_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        // 5 (lang) + 5 (value) + 4 = 14 (0x0E)
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x0E, 0x00, 0x05, 0x65, 0x6E, 0x2D, 0x75, 0x73, 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, tag );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( new StringWithLanguage( "en-us", "Lorem" ) );
    }

    [DataTestMethod]
    [DataRow( Tag.TextWithLanguage )]
    [DataRow( Tag.NameWithLanguage )]
    public void ReadValue_InvalidStringWithLanguage_ThrowsArgumentException( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        // 10 (0x0A) != 5 + 5 + 4
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x0A, 0x00, 0x05, 0x65, 0x6E, 0x2D, 0x75, 0x73, 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, tag );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [DataTestMethod]
    [DataRow( Tag.OctetStringWithAnUnspecifiedFormat )]
    [DataRow( Tag.TextWithoutLanguage )]
    [DataRow( Tag.NameWithoutLanguage )]
    [DataRow( Tag.Keyword )]
    [DataRow( Tag.Uri )]
    [DataRow( Tag.UriScheme )]
    [DataRow( Tag.Charset )]
    [DataRow( Tag.NaturalLanguage )]
    [DataRow( Tag.MimeMediaType )]
    [DataRow( Tag.MemberAttrName )]
    [DataRow( Tag.OctetStringUnassigned38 )]
    [DataRow( Tag.OctetStringUnassigned39 )]
    [DataRow( Tag.OctetStringUnassigned3A )]
    [DataRow( Tag.OctetStringUnassigned3B )]
    [DataRow( Tag.OctetStringUnassigned3C )]
    [DataRow( Tag.OctetStringUnassigned3D )]
    [DataRow( Tag.OctetStringUnassigned3E )]
    [DataRow( Tag.OctetStringUnassigned3F )]
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
    public void ReadValue_String_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x05, 0x4C, 0x6F, 0x72, 0x65, 0x6D } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, tag );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( "Lorem" );
    }

    [DataTestMethod]
    [DataRow( Tag.Unsupported )]
    [DataRow( Tag.Unknown )]
    [DataRow( Tag.NoValue )]
    [DataRow( Tag.BegCollection )]
    [DataRow( Tag.EndCollection )]
    public void ReadValue_NoValue_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x00 } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, tag );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( NoValue.Instance );
    }

    [TestMethod]
    public void ReadValue_BrokenNoValue_ThrowsArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x01, 0x00 } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.NoValue );
        // Assert
        act.Should().Throw<ArgumentException>();
    }


    [DataTestMethod]
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
    public void ReadValue_Int_ReturnsCorrectResult( Tag tag )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x10 } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, tag );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( 16 );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF }, DisplayName = "Invalid second byte" )]
    public void ReadValue_Int_ThrowsArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.Integer );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x01, 0x00 }, false )]
    [DataRow( new byte[] { 0x00, 0x01, 0x01 }, true )]
    public void ReadValue_Bool_ReturnsCorrectResult( byte[] value, bool expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.Boolean );
        // Assert
        act.Should().NotThrow().Which.Should().Be( expected );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x01, 0x02 } )]
    [DataRow( new byte[] { 0x00, 0x00, 0x00 } )]
    public void ReadValue_InvalidBool_ThrowsArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.Boolean );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2B, 0x02, 0x1E }, "12/31/1999 23:59:59 +02:30", DisplayName = "Time with negative offset" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x07, 0xCF, 0x0C, 0x1F, 0x17, 0x3B, 0x3B, 0x00, 0x2D, 0x02, 0x1E }, "12/31/1999 23:59:59 -02:30", DisplayName = "Time with positive offset" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 }, "01/01/0001 01:01:01 +00:00", DisplayName = "Minimal DateTime" )]
    public void ReadValue_DateTimeOffset_ReturnsCorrectResult( byte[] value, string expected )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.DateTime );
        // Assert
        act.Should().NotThrow().Which.Should().Be( DateTimeOffset.Parse( expected, CultureInfo.InvariantCulture ) );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 }, DisplayName = "Invalid second byte" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00 }, DisplayName = "Invalid offset sign" )]
    [DataRow( new byte[] { 0x00, 0x0B, 0x00, 0x01, 0x0F, 0x01, 0x01, 0x01, 0x01, 0x00, 0x2D, 0x00, 0x00 }, DisplayName = "Invalid month" )]
    public void ReadValue_InvalidDateTimeOffset_ThrowsArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.DateTime );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 }, int.MinValue, int.MinValue )]
    [DataRow( new byte[] { 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF }, int.MinValue, int.MaxValue )]
    [DataRow( new byte[] { 0x00, 0x08, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF }, int.MaxValue, int.MaxValue )]
    public void ReadValue_Range_ReturnsCorrectResult( byte[] value, int lower, int upper )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value, 0, value.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.RangeOfInteger );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( new SharpIpp.Protocol.Models.Range( lower, upper ) );
    }


    [TestMethod()]
    [DataRow( new byte[] { 0x01, 0x08, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF }, DisplayName = "Invalid first byte" )]
    [DataRow( new byte[] { 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF }, DisplayName = "Invalid second byte" )]
    public void ReadValue_InvalidRange_ShouldThrowArgumentException( byte[] value )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( value );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.RangeOfInteger );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, 0, int.MaxValue, ResolutionUnit.DotsPerCm )]
    [DataRow( new byte[] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 }, 0, int.MaxValue, ResolutionUnit.DotsPerInch )]
    [DataRow( new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerCm )]
    [DataRow( new byte[] { 0x00, 0x09, 0x7F, 0xFF, 0xFF, 0xFF, 0x7F, 0xFF, 0xFF, 0xFF, 0x03 }, int.MaxValue, int.MaxValue, ResolutionUnit.DotsPerInch )]
    public void ReadValue_Resolution_ReturnsCorrectResult( byte[] bytes, int width, int height, ResolutionUnit unit )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( bytes, 0, bytes.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.Resolution );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( new Resolution( width, height, unit ) );
    }

    [TestMethod()]
    [DataRow( new byte[] { 0x01, 0x09, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, DisplayName = "Invalid first byte" )]
    [DataRow( new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xFF, 0xFF, 0x04 }, DisplayName = "Invalid second byte" )]
    public void ReadValue_InvalidResolution_ThrowsArgumentException( byte[] bytes )
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( bytes, 0, bytes.Length );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, Tag.Resolution );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void ReadValue_InvalidTag_ThrowsArgumentException()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new();
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<object> act = () => protocol.ReadValue( binaryReader, (Tag)0x01 );
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void ReadAttribute_OneAttribute_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x16, 0x69, 0x70, 0x70, 0x2D, 0x76, 0x65, 0x72, 0x73, 0x69,
            0x6F, 0x6E, 0x73, 0x2D, 0x73, 0x75, 0x70, 0x70, 0x6F, 0x72, 0x74, 0x65, 0x64, 0x00, 0x03, 0x31, 0x2E, 0x31 } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<IppAttribute> act = () => protocol.ReadAttribute( Tag.Keyword, binaryReader, null, null, Encoding.ASCII );
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ) );
    }

    [TestMethod]
    public void ReadAttribute_BegCollection_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new(new byte[] { 0x00, 0x11, 0x6D, 0x65, 0x64, 0x69, 0x61, 0x2D, 0x63, 0x6F, 0x6C,
            0x2D, 0x64, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x00, 0x00 });
        using BinaryReader binaryReader = new(memoryStream);
        // Act
        Func<IppAttribute> act = () => protocol.ReadAttribute(Tag.BegCollection, binaryReader, null, null, Encoding.ASCII);
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo(new IppAttribute(Tag.BegCollection, PrinterAttribute.MediaColDefault, NoValue.Instance));
    }

    [TestMethod]
    public void ReadAttribute_SecondSimilarAttribute_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x00, 0x00, 0x03, 0x31, 0x2E, 0x31 } );
        using BinaryReader binaryReader = new( memoryStream );
        var previousAttribute = new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion( 1, 0 ).ToString() );
        // Act
        Func<IppAttribute> act = () => protocol.ReadAttribute( Tag.Keyword, binaryReader, previousAttribute, null, Encoding.ASCII);
        // Assert
        act.Should().NotThrow().Which.Should().BeEquivalentTo( new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, new IppVersion( 1, 1 ).ToString() ) );
    }

    [TestMethod]
    public void ReadAttribute_AttributeWithoutName_ReturnsCorrectResult()
    {
        // Arrange
        var protocol = new IppProtocol();
        using MemoryStream memoryStream = new( new byte[] { 0x00, 0x00, 0x00, 0x03, 0x31, 0x2E, 0x31 } );
        using BinaryReader binaryReader = new( memoryStream );
        // Act
        Func<IppAttribute> act = () => protocol.ReadAttribute( Tag.Keyword, binaryReader, null, null, Encoding.ASCII);
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod()]
    public void ReadIppResponseAsync_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<Task<IIppResponseMessage>> act = async () => await protocol.ReadIppResponseAsync( null );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod()]
    public void ReadValue_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<object> act = () => protocol.ReadValue( null, Tag.Charset );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod()]
    public void ReadAttribute_StreamIsNull_ShouldThrowException()
    {
        // Arrange
        var protocol = new IppProtocol();
        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Func<IppAttribute> act = () => protocol.ReadAttribute( Tag.Keyword, null, null, null, Encoding.ASCII );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        // Assert
        act.Should().Throw<ArgumentNullException>();
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
        message.JobAttributes.Add( new IppAttribute(Tag.Integer, JobAttribute.Copies, 1 ) );
        
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
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, 1 ) );
        
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
        message.OperationAttributes.Add( new IppAttribute( Tag.Integer, JobAttribute.Copies, 1 ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, JobAttribute.JobName, "utf-8" ) );
        message.OperationAttributes.Add( new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ) );
        
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
        message.JobAttributes.Add( new List<IppAttribute> { new IppAttribute(Tag.Integer, JobAttribute.Copies, 1 ) } );
        
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
        message.OperationAttributes.Add( new List<IppAttribute> { new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, 1 ) } );
        
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
            new IppAttribute( Tag.Integer, JobAttribute.Copies, 1 ),
            new IppAttribute( Tag.Charset, JobAttribute.JobName, "utf-8" ),
            new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" )
        } );
        
        // Act
        Func<Task> act = async () => await protocol.WriteIppResponseAsync( message, memoryStream );
        
        // Assert
        await act.Should().NotThrowAsync();
    }

    [TestMethod]
    public void GetNormalizedName_PrevBegCollection_ShouldReturnPrevName()
    {
        // Case for verifying 1setOf Collection support (formerly line 418 break)
        var prevAttribute = new IppAttribute(Tag.BegCollection, "test-collection", NoValue.Instance);

        var name = IppProtocol.GetNormalizedName(Tag.BegCollection, "", prevAttribute, null);
        name.Should().Be("test-collection");
    }

    [TestMethod]
    public void GetNormalizedName_PrevEndCollection_NoPrevBegCollection_ShouldThrow()
    {
        // Case for Line 422 (break) coverage
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);

        Action act = () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);

        act.Should().Throw<ArgumentException>().WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public void GetNormalizedName_PrevEndCollection_PrevBegCollectionNoName_ShouldThrow()
    {
        // Case for Line 422/420 (condition false) coverage
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        var prevBeg = new IppAttribute(Tag.BegCollection, "", NoValue.Instance); // Empty name

        Action act = () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, prevBeg);

        act.Should().Throw<ArgumentException>().WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public void GetNormalizedName_Default_PrevNameEmpty_ShouldThrow()
    {
        // Case for Line 428 (break) coverage
        // Use a tag that hits default (e.g. Integer) but has empty name
        var prevAttribute = new IppAttribute(Tag.Integer, "", 1);

        Action act = () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);

        act.Should().Throw<ArgumentException>().WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public void GetNormalizedName_PrevMember_NameIsValue()
    {
        var prevAttribute = new IppAttribute(Tag.MemberAttrName, "", "member-name");
        var name = IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);
        name.Should().Be("member-name");
    }

    [TestMethod]
    public void GetNormalizedName_EndCollection_TakesPrevBegCollectionName()
    {
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        var prevBeg = new IppAttribute(Tag.BegCollection, "collection-name", NoValue.Instance);

        var name = IppProtocol.GetNormalizedName(Tag.BegCollection, "", prevAttribute, prevBeg);
        name.Should().Be("collection-name");
    }
}
