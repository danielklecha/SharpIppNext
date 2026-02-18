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
    ///     Ipp protocol reader-writer.
    ///     Ipp protocol only supports common types:
    ///     <see cref="int"/>
    ///     <see cref="bool"/>
    ///     <see cref="string" />
    ///     <see cref="DateTimeOffset" />
    ///     <see cref="NoValue" />
    ///     <see cref="Range" />
    ///     <see cref="Resolution" />
    ///     <see cref="StringWithLanguage" />
    ///     all other types must be mapped via IMapper in-onto these
    /// </summary>
    public partial class IppProtocol : IIppProtocol
    {
        /// <summary>
        /// Controls the behavior of ReadIppRequestAsync() method.
        /// If true, the whole incoming document is read into a memory stream, 
        /// and can be accessed via message.Document.
        /// If false, the document is not read into a memory stream, and it should
        /// be consumed from the input stream by the caller.
        /// Defaults to true.
        /// </summary>
        public bool ReadDocumentStream { get; set; } = true;

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

        public async Task<IIppRequestMessage> ReadIppRequestAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            using var reader = new BinaryReader( stream, Encoding.ASCII, true );
            return await ReadIppRequestAsync( reader, cancellationToken );
        }

        public Task<IIppResponseMessage> ReadIppResponseAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var res = new IppResponseMessage();
            try
            {
                using var reader = new BinaryReader(stream, Encoding.ASCII, true);
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

        private static List<List<IppAttribute>> GetSectionList(IIppResponseMessage res, SectionTag sectionTag)
        {
            return sectionTag switch
            {
                SectionTag.OperationAttributesTag => res.OperationAttributes,
                SectionTag.JobAttributesTag => res.JobAttributes,
                SectionTag.PrinterAttributesTag => res.PrinterAttributes,
                SectionTag.UnsupportedAttributesTag => res.UnsupportedAttributes,
                SectionTag.SubscriptionAttributesTag => res.SubscriptionAttributes,
                SectionTag.EventNotificationAttributesTag => res.EventNotificationAttributes,
                SectionTag.ResourceAttributesTag => res.ResourceAttributes,
                SectionTag.DocumentAttributesTag => res.DocumentAttributes,
                SectionTag.SystemAttributesTag => res.SystemAttributes,
                _ => throw new ArgumentException($"Unknown section tag: {sectionTag}")
            };
        }

        private void ReadSections(BinaryReader reader, IIppResponseMessage res)
        {
            IppAttribute? prevAttribute = null;
            Stack<IppAttribute> prevBegCollectionAttributes = new();
            IppAttribute? prevBegCollectionAttribute = new();
            List<IppAttribute>? currentAttributes = null;
            SectionTag currentSectionTag = default;
            Encoding encoding = Encoding.ASCII;
            do
            {
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
                        currentAttributes = new List<IppAttribute>();
                        currentSectionTag = sectionTag;
                        GetSectionList(res, sectionTag).Add(currentAttributes);
                        break;
                    case SectionTag.EndOfAttributesTag:
                        return;
                    default:
                        if (currentAttributes is null)
                        {
                            throw new ArgumentException($"<Hex dump> Expected section tag, found {data:X2}");
                        }
                        var attribute = ReadAttribute((Tag)data, reader, prevAttribute, prevBegCollectionAttribute, encoding);
                        switch (attribute.Tag)
                        {
                            case Tag.Charset when currentSectionTag == SectionTag.OperationAttributesTag && attribute.Name == JobAttribute.AttributesCharset && attribute.Value is string charsetName:
                                try
                                {
                                    encoding = Encoding.GetEncoding(charsetName);
                                }
                                catch (ArgumentException)
                                {
                                    //ignore invalid charset, keep previous one
                                }
                                break;
                            case Tag.BegCollection:
                                prevBegCollectionAttributes.Push(attribute);
                                break;
                            case Tag.EndCollection:
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
            IppAttribute? prevBegCollectionAttribute = new();
            List<IppAttribute>? attributes = null;
            Encoding encoding = Encoding.ASCII;
            do
            {
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
                        if ( attributes is null )
                        {
                            reader.BaseStream.Position--;
                            return;
                        }
                        var attribute = ReadAttribute((Tag)data, reader, prevAttribute, prevBegCollectionAttribute, encoding);
                        switch (attribute.Tag)
                        {
                            case Tag.Charset when attributes == res.OperationAttributes && attribute.Name == JobAttribute.AttributesCharset && attribute.Value is string charsetName:
                                try
                                {
                                    encoding = Encoding.GetEncoding(charsetName);
                                }
                                catch (ArgumentException)
                                {
                                    //ignore invalid charset, keep previous one
                                }
                                break;
                            case Tag.BegCollection:
                                prevBegCollectionAttributes.Push(attribute);
                                break;
                            case Tag.EndCollection:
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

            if (prevAttribute != null && prevAttribute.Name == attribute.Name)
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
                case Range v:
                    Write(v, stream);
                    break;
                case StringWithLanguage v:
                    Write(v, stream, encoding);
                    break;
                default:
                    throw new ArgumentException($"Type {value.GetType()} not supported in ipp");
            }
        }

        public Task WriteIppResponseAsync( IIppResponseMessage ippResponseMessage, Stream stream, CancellationToken cancellationToken = default )
        {
            if (ippResponseMessage is null)
                throw new ArgumentNullException( nameof( ippResponseMessage ) );
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var encoding = Encoding.ASCII;
            try
            {
                var charset = ippResponseMessage.OperationAttributes
                    .SelectMany(x => x)
                    .FirstOrDefault(x => x.Tag == Tag.Charset && x.Name == JobAttribute.AttributesCharset)
                    ?.Value as string;
                if (charset is not null)
                    encoding = Encoding.GetEncoding(charset);
            }
            catch (ArgumentException)
            {
                //ignore invalid charset, keep previous one
            }
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
                Tag.OctetStringWithAnUnspecifiedFormat => ReadString(stream),
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
                Tag.OctetStringUnassigned38 => ReadString(stream),
                Tag.OctetStringUnassigned39 => ReadString(stream),
                Tag.OctetStringUnassigned3A => ReadString(stream),
                Tag.OctetStringUnassigned3B => ReadString(stream),
                Tag.OctetStringUnassigned3C => ReadString(stream),
                Tag.OctetStringUnassigned3D => ReadString(stream),
                Tag.OctetStringUnassigned3E => ReadString(stream),
                Tag.OctetStringUnassigned3F => ReadString(stream),
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
                _ => throw new ArgumentException($"Ipp tag {tag} not supported")
            };
        }

        public IppAttribute ReadAttribute(Tag tag, BinaryReader stream, IppAttribute? prevAttribute, IppAttribute? prevBegCollectionAttribute, Encoding encoding)
        {
            if (stream is null)
                throw new ArgumentNullException( nameof( stream ) );
            var len = stream.ReadInt16BigEndian();
            var name = Encoding.ASCII.GetString(stream.ReadBytes(len));
            var normalizedName = GetNormalizedName(tag, name, prevAttribute, prevBegCollectionAttribute);
            var value = ReadValue(stream, tag, encoding);
            var attribute = new IppAttribute(tag, normalizedName, value);
            return attribute;
        }

        private static string GetNormalizedName(Tag tag, string name, IppAttribute? prevAttribute, IppAttribute? prevBegCollectionAttribute)
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
                switch (prevAttribute.Tag)
                {
                    case Tag.BegCollection:
                        break;
                    case Tag.EndCollection:
                        if (prevBegCollectionAttribute != null && !string.IsNullOrEmpty(prevBegCollectionAttribute.Name))
                            return prevBegCollectionAttribute.Name;
                        break;
                    case Tag.MemberAttrName when prevAttribute.Value is string memberAttrName:
                        return memberAttrName;
                    default:
                        if (!string.IsNullOrEmpty(prevAttribute.Name))
                            return prevAttribute.Name;
                        break;
                }
            }
            throw new ArgumentException("0 length attribute name found not in a 1setOf");
        }

        private async Task<IIppRequestMessage> ReadIppRequestAsync( BinaryReader reader, CancellationToken cancellationToken = default )
        {
            IppRequestMessage message = new IppRequestMessage
            {
                Version = new IppVersion( reader.ReadInt16BigEndian() ),
                IppOperation = (IppOperation)reader.ReadInt16BigEndian(),
                RequestId = reader.ReadInt32BigEndian()
            };
            ReadSections( reader, message );
            message.Document = new MemoryStream();
            if (ReadDocumentStream)
            {
                await reader.BaseStream.CopyToAsync(message.Document);
                message.Document.Seek(0, SeekOrigin.Begin);
            }
            return message;
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
            var encoding = Encoding.ASCII;
            try
            {
                var charset = requestMessage.OperationAttributes
                    .FirstOrDefault(x => x.Tag == Tag.Charset && x.Name == JobAttribute.AttributesCharset)
                    ?.Value as string;
                if (charset is not null)
                    encoding = Encoding.GetEncoding(charset);
            }
            catch (ArgumentException)
            {
                //ignore invalid charset, keep previous one
            }
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
