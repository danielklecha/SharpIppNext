using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol
{
    /// <summary>
    /// Ipp protocol reader-writer.
    /// Ipp protocol only supports common types:
    /// <see cref="int"/>
    /// <see cref="bool"/>
    /// <see cref="string" />
    /// <see cref="DateTimeOffset" />
    /// <see cref="NoValue" />
    /// <see cref="Range" />
    /// <see cref="Resolution" />
    /// <see cref="StringWithLanguage" />
    /// all other types must be mapped via IMapper in-onto these
    /// </summary>
    public partial class IppProtocol : IIppProtocol
    {
        /// <inheritdoc />
        public bool ReadDocumentStream { get; set; } = true;

        /// <inheritdoc />
        public long? MaxDocumentStreamBytes { get; set; } = 128L * 1024 * 1024;

        /// <inheritdoc />
        public int? MaxMessageAttributesCount { get; set; } = 5000;

        /// <inheritdoc />
        public long? MaxMessageAttributesBytes { get; set; } = 10L * 1024 * 1024;

        /// <inheritdoc />
        public async Task WriteIppRequestAsync(IIppRequestMessage ippRequestMessage, Stream stream, CancellationToken cancellationToken = default)
        {
            if (ippRequestMessage is null)
                throw new ArgumentNullException( nameof( ippRequestMessage ) );
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            using var writer = new BinaryWriter(stream, Encoding.ASCII, true);
            writer.WriteBigEndian( ippRequestMessage.Version.ToInt16BigEndian() );
            writer.WriteBigEndian( (short)ippRequestMessage.IppOperation );
            writer.WriteBigEndian( ippRequestMessage.RequestId );
            WriteSections(ippRequestMessage, writer);
            if (ippRequestMessage.Document != null)
            {
                await ippRequestMessage.Document.CopyToAsync(stream, 81920, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<IIppRequestMessage> ReadIppRequestAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var countingStream = new CountingStream( stream );
            using var reader = new BinaryReader( countingStream, Encoding.ASCII, true );
            return await ReadIppRequestAsync( reader, cancellationToken );
        }

        /// <inheritdoc />
        public Task<IIppResponseMessage> ReadIppResponseAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var res = new IppResponseMessage();
            try
            {
                var countingStream = new CountingStream( stream );
                using var reader = new BinaryReader(countingStream, Encoding.ASCII, true);
                res.Version = new IppVersion( reader.ReadInt16BigEndian() );
                res.StatusCode = (IppStatusCode)reader.ReadInt16BigEndian();
                res.RequestId = reader.ReadInt32BigEndian();
                ReadSections(reader, res);
                return Task.FromResult((IIppResponseMessage)res);
            }
            catch (Exception ex)
            {
                throw new IppResponseException($"Failed to parse ipp response. Current response parsing ended on: \n{res}", ex, res);
            }
        }

        private void ReadSections(BinaryReader reader, IIppResponseMessage res)
        {
            IppAttribute? prevAttribute = null;
            Stack<IppAttribute> prevBegCollectionAttributes = new();
            IppAttribute? prevBegCollectionAttribute = null;
            List<IppAttribute>? currentAttributes = null;
            SectionTag currentSectionTag = default;
            Encoding encoding = Encoding.ASCII;
            int parsedAttributesCount = 0;
            do
            {
                parsedAttributesCount++;
                if (MaxMessageAttributesCount.HasValue && parsedAttributesCount > MaxMessageAttributesCount.Value)
                {
                    throw new ArgumentException($"Maximum attribute limit of {MaxMessageAttributesCount.Value} exceeded.");
                }
                if (MaxMessageAttributesBytes.HasValue && reader.BaseStream is CountingStream countingStream && countingStream.BytesRead > MaxMessageAttributesBytes.Value)
                {
                    throw new ArgumentException($"Maximum attribute bytes limit of {MaxMessageAttributesBytes.Value} exceeded.");
                }
                var data = reader.ReadByte();
                var sectionTag = (SectionTag)data;

                switch (sectionTag)
                {
                    case SectionTag.OperationAttributesTag:
                    case SectionTag.JobAttributesTag:
                    case SectionTag.PrinterAttributesTag:
                    case SectionTag.UnsupportedAttributesTag:
                    case SectionTag.SubscriptionAttributesTag:
                    case SectionTag.EventNotificationAttributesTag:
                    case SectionTag.ResourceAttributesTag:
                    case SectionTag.DocumentAttributesTag:
                    case SectionTag.SystemAttributesTag:
                        currentAttributes = [];
                        currentSectionTag = sectionTag;
                        res.GetSectionList(sectionTag).Add(currentAttributes);
                        break;
                    case SectionTag.EndOfAttributesTag:
                        return;
                    default:
                        if ((byte)sectionTag <= 0x0F)
                        {
                            // Future/unknown group tag: parse attributes but drop the group.
                            currentAttributes = [];
                            currentSectionTag = sectionTag;
                            break;
                        }
                        if (currentAttributes is null)
                        {
                            throw new ArgumentException($"<Hex dump> Expected section tag, found {data:X2}");
                        }
                        var attribute = ReadAttribute((Tag)data, reader, prevAttribute, prevBegCollectionAttribute, encoding);
                        switch (attribute.Tag)
                        {
                            case Tag.Charset when currentSectionTag == SectionTag.OperationAttributesTag && attribute.Name == IppAttributeNames.AttributesCharset:
                                encoding = Encoding.GetEncoding((string)attribute.Value);
                                break;
                            case Tag.BegCollection:
                                prevBegCollectionAttributes.Push(attribute);
                                break;
                            case Tag.EndCollection:
                                if (prevBegCollectionAttributes.Count == 0)
                                {
                                    throw new ArgumentException("Unexpected EndCollection tag. No matching BegCollection.");
                                }
                                prevBegCollectionAttribute = prevBegCollectionAttributes.Pop();
                                break;
                        }
                        prevAttribute = attribute;
                        currentAttributes.Add(attribute);
                        break;
                }
            }
            while (true);
        }

        private void ReadSections( BinaryReader reader, IIppRequestMessage res )
        {
            IppAttribute? prevAttribute = null;
            Stack<IppAttribute> prevBegCollectionAttributes = new();
            IppAttribute? prevBegCollectionAttribute = null;
            List<IppAttribute>? attributes = null;
            Encoding encoding = Encoding.ASCII;
            int parsedAttributesCount = 0;
            do
            {
                parsedAttributesCount++;
                if (MaxMessageAttributesCount.HasValue && parsedAttributesCount > MaxMessageAttributesCount.Value)
                {
                    throw new IppRequestException($"Maximum attribute limit of {MaxMessageAttributesCount.Value} exceeded.", res, IppStatusCode.ClientErrorRequestEntityTooLarge);
                }
                if (MaxMessageAttributesBytes.HasValue && reader.BaseStream is CountingStream countingStream && countingStream.BytesRead > MaxMessageAttributesBytes.Value)
                {
                    throw new IppRequestException($"Maximum attribute bytes limit of {MaxMessageAttributesBytes.Value} exceeded.", res, IppStatusCode.ClientErrorRequestEntityTooLarge);
                }
                var data = reader.ReadByte();
                var sectionTag = (SectionTag)data;

                switch ( sectionTag )
                {
                    case SectionTag.OperationAttributesTag:
                        attributes = res.OperationAttributes;
                        break;
                    case SectionTag.JobAttributesTag:
                        attributes = res.JobAttributes;
                        break;
                    case SectionTag.PrinterAttributesTag:
                        attributes = res.PrinterAttributes;
                        break;
                    case SectionTag.UnsupportedAttributesTag:
                        attributes = res.UnsupportedAttributes;
                        break;
                    case SectionTag.SubscriptionAttributesTag:
                        attributes = res.SubscriptionAttributes;
                        break;
                    case SectionTag.EventNotificationAttributesTag:
                        attributes = res.EventNotificationAttributes;
                        break;
                    case SectionTag.ResourceAttributesTag:
                        attributes = res.ResourceAttributes;
                        break;
                    case SectionTag.DocumentAttributesTag:
                        attributes = res.DocumentAttributes;
                        break;
                    case SectionTag.SystemAttributesTag:
                        attributes = res.SystemAttributes;
                        break;
                    case SectionTag.EndOfAttributesTag:
                        return;
                    default:
                        if ((byte)sectionTag <= 0x0F)
                        {
                            // Future/unknown group tag: parse attributes but do not expose on the request model.
                            attributes = [];
                            break;
                        }
                        if ( attributes is null )
                        {
                            throw new IppRequestException($"<Hex dump> Expected section tag, found {data:X2}", res, IppStatusCode.ClientErrorBadRequest);
                        }
                        var attribute = ReadAttribute((Tag)data, reader, prevAttribute, prevBegCollectionAttribute, encoding);
                        switch (attribute.Tag)
                        {
                            case Tag.Charset when attributes == res.OperationAttributes && attribute.Name == IppAttributeNames.AttributesCharset:
                                encoding = Encoding.GetEncoding((string)attribute.Value);
                                break;
                            case Tag.BegCollection:
                                prevBegCollectionAttributes.Push(attribute);
                                break;
                            case Tag.EndCollection:
                                if (prevBegCollectionAttributes.Count == 0)
                                {
                                    throw new IppRequestException("Unexpected EndCollection tag. No matching BegCollection.", res, IppStatusCode.ClientErrorBadRequest);
                                }
                                prevBegCollectionAttribute = prevBegCollectionAttributes.Pop();
                                break;
                        }
                        prevAttribute = attribute;
                        attributes.Add( attribute );
                        break;
                }
            }
            while ( true );
        }

        public void WriteAttribute( BinaryWriter stream, IppAttribute attribute, IppAttribute? prevAttribute, Encoding encoding )
        {
            stream.Write( (byte)attribute.Tag );

            if (prevAttribute != null && prevAttribute.Value.Name == attribute.Name)
            {
                stream.WriteBigEndian( (short)0 );
            }
            else
            {
                stream.WriteBigEndian( (short)attribute.Name.Length );
                stream.Write( Encoding.ASCII.GetBytes( attribute.Name ) );
            }

            switch(attribute.Tag)
            {
                case Tag.NameWithLanguage:
                case Tag.TextWithLanguage:
                case Tag.NameWithoutLanguage:
                case Tag.TextWithoutLanguage:
                    WriteValue(attribute.Value, stream, encoding);
                    break;
                default:
                    WriteValue(attribute.Value, stream, null);
                    break;
            }
            
        }

        public void WriteValue(object value, BinaryWriter stream, Encoding? encoding = null)
        {
            //https://tools.ietf.org/html/rfc8010#section-3.5.2
            switch (value)
            {
                case NoValue v:
                    Write(v, stream);
                    break;
                case int v:
                    Write(v, stream);
                    break;
                case bool v:
                    Write(v, stream);
                    break;
                case string v:
                    Write(v, stream, encoding);
                    break;
                case DateTimeOffset v:
                    Write(v, stream);
                    break;
                case Resolution v:
                    Write(v, stream);
                    break;
                case Models.Range v:
                    Write(v, stream);
                    break;
                case StringWithLanguage v:
                    Write(v, stream, encoding);
                    break;
                case OctetString v:
                    Write(v, stream);
                    break;
                case byte[] v:
                    Write(v, stream);
                    break;
                case Tag v:
                    Write((byte)v, stream);
                    break;
                case Enum v when Enum.GetUnderlyingType(v.GetType()) == typeof(short):
                    Write(Convert.ToInt16(v), stream);
                    break;
                case Enum v when Enum.GetUnderlyingType(v.GetType()) == typeof(int):
                    Write(Convert.ToInt32(v), stream);
                    break;
                case ISmartEnum v:
                    Write(v.Value, stream, encoding);
                    break;
                case ExtendedValue v:
                    stream.WriteBigEndian((short)(4 + v.Raw.Length));
                    stream.WriteBigEndian(v.ExtendedTag);
                    stream.Write(v.Raw);
                    break;
                case UnknownValue v:
                    stream.WriteBigEndian((short)v.Raw.Length);
                    stream.Write(v.Raw);
                    break;
                default:
                    throw new ArgumentException($"Type {value.GetType()} not supported in ipp");
            }
        }

        /// <inheritdoc />
        public Task WriteIppResponseAsync( IIppResponseMessage ippResponseMessage, Stream stream, CancellationToken cancellationToken = default )
        {
            if (ippResponseMessage is null)
                throw new ArgumentNullException( nameof( ippResponseMessage ) );
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var charsetAttribute = ippResponseMessage.OperationAttributes
                .SelectMany(x => x)
                .FirstOrDefault(x => x.Tag == Tag.Charset && x.Name == IppAttributeNames.AttributesCharset);
            if (charsetAttribute.Value is not string charset)
            {
                throw new ArgumentException($"The operation attribute '{IppAttributeNames.AttributesCharset}' is missing or invalid.");
            }
            var encoding = Encoding.GetEncoding(charset);
            using var writer = new BinaryWriter( stream, Encoding.ASCII, true );
            writer.WriteBigEndian( ippResponseMessage.Version.ToInt16BigEndian() );
            writer.WriteBigEndian( (short)ippResponseMessage.StatusCode );
            writer.WriteBigEndian( ippResponseMessage.RequestId );
            WriteSections( ippResponseMessage, writer, encoding );
            return Task.CompletedTask;
        }

        public object ReadValue(BinaryReader stream, Tag tag, Encoding? encoding = null)
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            //https://tools.ietf.org/html/rfc8010#section-3.5.2
            return tag switch
            {
                Tag.Unsupported => ReadNoValue(stream),
                Tag.Unknown => ReadNoValue(stream),
                Tag.NoValue => ReadNoValue(stream),
                Tag.Integer => ReadInt(stream),
                Tag.Enum => ReadInt(stream),
                Tag.Boolean => ReadBool(stream),
                Tag.OctetStringWithAnUnspecifiedFormat => ReadOctetString(stream),
                Tag.DateTime => ReadDateTimeOffset(stream),
                Tag.Resolution => ReadResolution(stream),
                Tag.RangeOfInteger => ReadRange(stream),
                Tag.BegCollection => ReadNoValue(stream),
                Tag.TextWithLanguage => ReadStringWithLanguage(stream, encoding),
                Tag.NameWithLanguage => ReadStringWithLanguage(stream, encoding),
                Tag.EndCollection => ReadNoValue(stream),
                Tag.TextWithoutLanguage => ReadString(stream, encoding),
                Tag.NameWithoutLanguage => ReadString(stream, encoding),
                Tag.Keyword => ReadString(stream),
                Tag.Uri => ReadString(stream),
                Tag.UriScheme => ReadString(stream),
                Tag.Charset => ReadString(stream),
                Tag.NaturalLanguage => ReadString(stream),
                Tag.MimeMediaType => ReadString(stream),
                Tag.MemberAttrName => ReadString(stream),
                Tag.OctetStringUnassigned38 => ReadOctetString(stream),
                Tag.OctetStringUnassigned39 => ReadOctetString(stream),
                Tag.OctetStringUnassigned3A => ReadOctetString(stream),
                Tag.OctetStringUnassigned3B => ReadOctetString(stream),
                Tag.OctetStringUnassigned3C => ReadOctetString(stream),
                Tag.OctetStringUnassigned3D => ReadOctetString(stream),
                Tag.OctetStringUnassigned3E => ReadOctetString(stream),
                Tag.OctetStringUnassigned3F => ReadOctetString(stream),
                Tag.IntegerUnassigned20 => ReadInt(stream),
                Tag.IntegerUnassigned24 => ReadInt(stream),
                Tag.IntegerUnassigned25 => ReadInt(stream),
                Tag.IntegerUnassigned26 => ReadInt(stream),
                Tag.IntegerUnassigned27 => ReadInt(stream),
                Tag.IntegerUnassigned28 => ReadInt(stream),
                Tag.IntegerUnassigned29 => ReadInt(stream),
                Tag.IntegerUnassigned2A => ReadInt(stream),
                Tag.IntegerUnassigned2B => ReadInt(stream),
                Tag.IntegerUnassigned2C => ReadInt(stream),
                Tag.IntegerUnassigned2D => ReadInt(stream),
                Tag.IntegerUnassigned2E => ReadInt(stream),
                Tag.IntegerUnassigned2F => ReadInt(stream),
                Tag.StringUnassigned40 => ReadString(stream),
                Tag.StringUnassigned43 => ReadString(stream),
                Tag.StringUnassigned4B => ReadString(stream),
                Tag.StringUnassigned4C => ReadString(stream),
                Tag.StringUnassigned4D => ReadString(stream),
                Tag.StringUnassigned4E => ReadString(stream),
                Tag.StringUnassigned4F => ReadString(stream),
                Tag.StringUnassigned50 => ReadString(stream),
                Tag.StringUnassigned51 => ReadString(stream),
                Tag.StringUnassigned52 => ReadString(stream),
                Tag.StringUnassigned53 => ReadString(stream),
                Tag.StringUnassigned54 => ReadString(stream),
                Tag.StringUnassigned55 => ReadString(stream),
                Tag.StringUnassigned56 => ReadString(stream),
                Tag.StringUnassigned57 => ReadString(stream),
                Tag.StringUnassigned58 => ReadString(stream),
                Tag.StringUnassigned59 => ReadString(stream),
                Tag.StringUnassigned5A => ReadString(stream),
                Tag.StringUnassigned5B => ReadString(stream),
                Tag.StringUnassigned5C => ReadString(stream),
                Tag.StringUnassigned5D => ReadString(stream),
                Tag.StringUnassigned5E => ReadString(stream),
                Tag.StringUnassigned5F => ReadString(stream),
                Tag.Extended => ReadExtended(stream),
                _ => ReadUnknown(stream, tag)
            };
        }

        public virtual IppAttribute ReadAttribute(Tag tag, BinaryReader stream, IppAttribute? prevAttribute, IppAttribute? prevBegCollectionAttribute, Encoding encoding)
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var len = stream.ReadInt16BigEndian();
            if (len < 0)
            {
                throw new ArgumentException("Attribute name length cannot be negative");
            }
            var nameBytes = stream.ReadBytes(len);
            if (nameBytes.Length < len)
            {
                throw new EndOfStreamException("Unexpected end of stream while reading attribute name");
            }
            var name = Encoding.ASCII.GetString(nameBytes);
            var normalizedName = GetNormalizedName(tag, name, prevAttribute, prevBegCollectionAttribute);
            var value = ReadValue(stream, tag, encoding);
            var attribute = new IppAttribute(tag, normalizedName, value);
            return attribute;
        }

        public static string GetNormalizedName(Tag tag, string name, IppAttribute? prevAttribute, IppAttribute? prevBegCollectionAttribute)
        {
            if (!string.IsNullOrEmpty(name))
                return name;
            switch (tag)
            {
                case Tag.MemberAttrName:
                case Tag.EndCollection:
                    return string.Empty;
            }
            if(prevAttribute != null)
            {
                switch (prevAttribute.Value.Tag)
                {
                    case Tag.EndCollection:
                        if (prevBegCollectionAttribute != null && !string.IsNullOrEmpty(prevBegCollectionAttribute.Value.Name))
                            return prevBegCollectionAttribute.Value.Name;
                        break;
                    case Tag.MemberAttrName when prevAttribute.Value.Value is string memberAttrName:
                        return memberAttrName;
                    default:
                        if (!string.IsNullOrEmpty(prevAttribute.Value.Name))
                            return prevAttribute.Value.Name;
                        break;
                }
            }
            throw new ArgumentException("0 length attribute name found not in a 1setOf");
        }

        private async Task<IIppRequestMessage> ReadIppRequestAsync( BinaryReader reader, CancellationToken cancellationToken = default )
        {
            IppRequestMessage message;
            try
            {
                message = new IppRequestMessage
                {
                    Version = new IppVersion( reader.ReadInt16BigEndian() ),
                    IppOperation = (IppOperation)reader.ReadInt16BigEndian(),
                    RequestId = reader.ReadInt32BigEndian()
                };
            }
            catch (Exception ex)
            {
                throw new IppRequestException("Failed to parse initial ipp request message.", ex, new IppRequestMessage(), IppStatusCode.ClientErrorBadRequest);
            }

            try
            {
                ReadSections( reader, message );
            }
            catch (IppRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IppRequestException("Failed to parse ipp request sections.", ex, message, IppStatusCode.ClientErrorBadRequest);
            }

            if (ReadDocumentStream)
            {
                var docStream = new MemoryStream();
                message.Document = docStream;
                try
                {
                    await CopyDocumentStreamAsync(reader.BaseStream, docStream,
                        message, cancellationToken).ConfigureAwait(false);
                }
                catch (IppRequestException)
                {
                    docStream.Dispose();
                    message.Document = null;
                    throw;
                }
                catch (Exception ex)
                {
                    docStream.Dispose();
                    message.Document = null;
                    throw new IppRequestException("Failed to copy document stream.", ex, message, IppStatusCode.ClientErrorBadRequest);
                }
                docStream.Seek(0, SeekOrigin.Begin);
            }
            else
            {
                message.Document = Stream.Null;
            }
            return message;
        }

        protected virtual async Task CopyDocumentStreamAsync(Stream source, Stream destination, IIppRequestMessage message, CancellationToken cancellationToken = default)
        {
            if (MaxDocumentStreamBytes == null)
            {
                await source.CopyToAsync(destination, 81920, cancellationToken).ConfigureAwait(false);
                return;
            }

            byte[] buffer = new byte[81920];
            int read;
            long totalRead = 0;

            while ((read = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
            {
                totalRead += read;
                if (totalRead > MaxDocumentStreamBytes.Value)
                {
                    throw new IppRequestException("Document stream size exceeded the maximum allowed limit.", message, IppStatusCode.ClientErrorRequestEntityTooLarge);
                }

                await destination.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
            }
        }

        private void WriteSections(IIppRequestMessage requestMessage, BinaryWriter writer)
        {
            if (requestMessage.OperationAttributes.Count == 0
                && requestMessage.JobAttributes.Count == 0
                && requestMessage.PrinterAttributes.Count == 0
                && requestMessage.UnsupportedAttributes.Count == 0
                && requestMessage.SubscriptionAttributes.Count == 0
                && requestMessage.EventNotificationAttributes.Count == 0
                && requestMessage.ResourceAttributes.Count == 0
                && requestMessage.DocumentAttributes.Count == 0
                && requestMessage.SystemAttributes.Count == 0)
                return;
            var charsetAttribute = requestMessage.OperationAttributes
                    .FirstOrDefault(x => x.Tag == Tag.Charset && x.Name == IppAttributeNames.AttributesCharset);
            if (charsetAttribute.Value is not string charset)
            {
                throw new ArgumentException($"The operation attribute '{IppAttributeNames.AttributesCharset}' is missing or invalid.");
            }
            var encoding = Encoding.GetEncoding(charset);
            WriteSection(SectionTag.OperationAttributesTag, requestMessage.OperationAttributes, writer, encoding);
            WriteSection(SectionTag.JobAttributesTag, requestMessage.JobAttributes, writer, encoding);
            WriteSection(SectionTag.PrinterAttributesTag, requestMessage.PrinterAttributes, writer, encoding);
            WriteSection(SectionTag.UnsupportedAttributesTag, requestMessage.UnsupportedAttributes, writer, encoding);
            WriteSection(SectionTag.SubscriptionAttributesTag, requestMessage.SubscriptionAttributes, writer, encoding);
            WriteSection(SectionTag.EventNotificationAttributesTag, requestMessage.EventNotificationAttributes, writer, encoding);
            WriteSection(SectionTag.ResourceAttributesTag, requestMessage.ResourceAttributes, writer, encoding);
            WriteSection(SectionTag.DocumentAttributesTag, requestMessage.DocumentAttributes, writer, encoding);
            WriteSection(SectionTag.SystemAttributesTag, requestMessage.SystemAttributes, writer, encoding);
            //end-of-attributes-tag https://tools.ietf.org/html/rfc8010#section-3.5.1
            writer.Write((byte)SectionTag.EndOfAttributesTag);
        }

        private void WriteSections( IIppResponseMessage responseMessage, BinaryWriter writer, Encoding encoding )
        {
            WriteSections( SectionTag.OperationAttributesTag, responseMessage.OperationAttributes, writer, encoding );
            WriteSections( SectionTag.JobAttributesTag, responseMessage.JobAttributes, writer, encoding );
            WriteSections( SectionTag.PrinterAttributesTag, responseMessage.PrinterAttributes, writer, encoding );
            WriteSections( SectionTag.UnsupportedAttributesTag, responseMessage.UnsupportedAttributes, writer, encoding );
            WriteSections( SectionTag.SubscriptionAttributesTag, responseMessage.SubscriptionAttributes, writer, encoding );
            WriteSections( SectionTag.EventNotificationAttributesTag, responseMessage.EventNotificationAttributes, writer, encoding );
            WriteSections( SectionTag.ResourceAttributesTag, responseMessage.ResourceAttributes, writer, encoding );
            WriteSections( SectionTag.DocumentAttributesTag, responseMessage.DocumentAttributes, writer, encoding );
            WriteSections( SectionTag.SystemAttributesTag, responseMessage.SystemAttributes, writer, encoding );
            //end-of-attributes-tag https://tools.ietf.org/html/rfc8010#section-3.5.1
            writer.Write( (byte)SectionTag.EndOfAttributesTag );
        }

        private void WriteSections( SectionTag sectionTag, List<List<IppAttribute>> groups, BinaryWriter writer, Encoding encoding )
        {
            foreach ( var attributes in groups )
            {
                WriteSection( sectionTag, attributes, writer, encoding );
            }
        }

        /// <summary>
        /// Writes a single section of IPP attributes.
        /// </summary>
        /// <remarks>
        /// Attributes are serialized in the exact order they appear in the list.
        /// RFC 8011 Section 4.1.4 requires that for the Operation Attributes section,
        /// 'attributes-charset' and 'attributes-natural-language' MUST be the first
        /// and second attributes respectively. The caller/mapping layer must ensure
        /// they are placed first in the list.
        /// </remarks>
        public void WriteSection( SectionTag sectionTag, List<IppAttribute> attributes, BinaryWriter writer, Encoding encoding )
        {
            if (attributes is null)
                throw new ArgumentNullException( nameof( attributes ) );
            if (writer is null)
                throw new ArgumentNullException( nameof( writer ) );
            if (attributes.Count == 0)
                return;
            //operation-attributes-tag https://tools.ietf.org/html/rfc8010#section-3.5.1
            writer.Write( (byte)sectionTag );
            for (var i = 0; i < attributes.Count; i++)
                WriteAttribute( writer, attributes[ i ], i > 0 ? attributes[ i - 1 ] : null, encoding );
        }
    }
}
