using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

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
        using var writer = new IppBinaryWriter(stream);
        await writer.WriteBigEndianAsync( ippRequestMessage.Version.ToInt16BigEndian(), cancellationToken ).ConfigureAwait(false);
        await writer.WriteBigEndianAsync( (short)ippRequestMessage.IppOperation, cancellationToken ).ConfigureAwait(false);
        await writer.WriteBigEndianAsync( ippRequestMessage.RequestId, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync(ippRequestMessage, writer, cancellationToken).ConfigureAwait(false);
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
        using var reader = new IppBinaryReader( countingStream );
        return await ReadIppRequestAsync( reader, cancellationToken ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IIppResponseMessage> ReadIppResponseAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream is null)
            throw new ArgumentNullException( nameof( stream ) );
        var res = new IppResponseMessage();
        try
        {
            var countingStream = new CountingStream( stream );
            using var reader = new IppBinaryReader(countingStream);
            res.Version = new IppVersion( await reader.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false) );
            res.StatusCode = (IppStatusCode)(await reader.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false));
            res.RequestId = await reader.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
            await ReadSectionsAsync(reader, res, cancellationToken).ConfigureAwait(false);
            return res;
        }
        catch (Exception ex)
        {
            throw new IppResponseException($"Failed to parse ipp response. Current response parsing ended on: \n{res}", ex, res);
        }
    }

    private async Task ReadSectionsAsync(IppBinaryReader reader, IIppResponseMessage res, CancellationToken cancellationToken)
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
            var data = await reader.ReadByteAsync(cancellationToken).ConfigureAwait(false);
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
                    var attribute = await ReadAttributeAsync((Tag)data, reader, prevAttribute, prevBegCollectionAttribute, encoding, cancellationToken).ConfigureAwait(false);
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

    private async Task ReadSectionsAsync( IppBinaryReader reader, IIppRequestMessage res, CancellationToken cancellationToken )
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
            var data = await reader.ReadByteAsync(cancellationToken).ConfigureAwait(false);
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
                    var attribute = await ReadAttributeAsync((Tag)data, reader, prevAttribute, prevBegCollectionAttribute, encoding, cancellationToken).ConfigureAwait(false);
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

    public async Task WriteAttributeAsync( IppBinaryWriter stream, IppAttribute attribute, IppAttribute? prevAttribute, Encoding encoding, CancellationToken cancellationToken = default )
    {
        await stream.WriteAsync( (byte)attribute.Tag, cancellationToken ).ConfigureAwait(false);

        if (prevAttribute != null && prevAttribute.Value.Name == attribute.Name)
        {
            await stream.WriteBigEndianAsync( (short)0, cancellationToken ).ConfigureAwait(false);
        }
        else
        {
            await stream.WriteBigEndianAsync( (short)attribute.Name.Length, cancellationToken ).ConfigureAwait(false);
            await stream.WriteAsync( Encoding.ASCII.GetBytes( attribute.Name ), cancellationToken ).ConfigureAwait(false);
        }

        switch(attribute.Tag)
        {
            case Tag.NameWithLanguage:
            case Tag.TextWithLanguage:
            case Tag.NameWithoutLanguage:
            case Tag.TextWithoutLanguage:
                await WriteValueAsync(attribute.Value, stream, encoding, cancellationToken).ConfigureAwait(false);
                break;
            default:
                await WriteValueAsync(attribute.Value, stream, null, cancellationToken).ConfigureAwait(false);
                break;
        }
        
    }

    public async Task WriteValueAsync(object value, IppBinaryWriter stream, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        //https://tools.ietf.org/html/rfc8010#section-3.5.2
        switch (value)
        {
            case NoValue v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case int v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case bool v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case string v:
                await WriteAsync(v, stream, encoding, cancellationToken).ConfigureAwait(false);
                break;
            case DateTimeOffset v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case Resolution v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case Models.Range v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case StringWithLanguage v:
                await WriteAsync(v, stream, encoding, cancellationToken).ConfigureAwait(false);
                break;
            case OctetString v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case byte[] v:
                await WriteAsync(v, stream, cancellationToken).ConfigureAwait(false);
                break;
            case Enum v:
                await WriteAsync(Convert.ToInt32(v), stream, cancellationToken).ConfigureAwait(false);
                break;
            case ISmartEnum v:
                await WriteAsync(v.Value, stream, encoding, cancellationToken).ConfigureAwait(false);
                break;
            case ExtendedValue v:
                await stream.WriteBigEndianAsync((short)(4 + v.Raw.Length), cancellationToken).ConfigureAwait(false);
                await stream.WriteBigEndianAsync(v.ExtendedTag, cancellationToken).ConfigureAwait(false);
                await stream.WriteAsync(v.Raw, cancellationToken).ConfigureAwait(false);
                break;
            case UnknownValue v:
                await stream.WriteBigEndianAsync((short)v.Raw.Length, cancellationToken).ConfigureAwait(false);
                await stream.WriteAsync(v.Raw, cancellationToken).ConfigureAwait(false);
                break;
            default:
                throw new ArgumentException($"Type {value.GetType()} not supported in ipp");
        }
    }

    /// <inheritdoc />
    public async Task WriteIppResponseAsync( IIppResponseMessage ippResponseMessage, Stream stream, CancellationToken cancellationToken = default )
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
        using var writer = new IppBinaryWriter( stream );
        await writer.WriteBigEndianAsync( ippResponseMessage.Version.ToInt16BigEndian(), cancellationToken ).ConfigureAwait(false);
        await writer.WriteBigEndianAsync( (short)ippResponseMessage.StatusCode, cancellationToken ).ConfigureAwait(false);
        await writer.WriteBigEndianAsync( ippResponseMessage.RequestId, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( ippResponseMessage, writer, encoding, cancellationToken ).ConfigureAwait(false);
    }

    public async Task<object> ReadValueAsync(IppBinaryReader stream, Tag tag, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        if (stream is null)
            throw new ArgumentNullException( nameof( stream ) );
        //https://tools.ietf.org/html/rfc8010#section-3.5.2
        return tag switch
        {
            Tag.Unsupported => await ReadNoValueAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.Unknown => await ReadNoValueAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.NoValue => await ReadNoValueAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.Integer => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.Enum => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.Boolean => await ReadBoolAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringWithAnUnspecifiedFormat => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.DateTime => await ReadDateTimeOffsetAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.Resolution => await ReadResolutionAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.RangeOfInteger => await ReadRangeAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.BegCollection => await ReadNoValueAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.TextWithLanguage => await ReadStringWithLanguageAsync(stream, encoding, cancellationToken).ConfigureAwait(false),
            Tag.NameWithLanguage => await ReadStringWithLanguageAsync(stream, encoding, cancellationToken).ConfigureAwait(false),
            Tag.EndCollection => await ReadNoValueAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.TextWithoutLanguage => await ReadStringAsync(stream, encoding, cancellationToken).ConfigureAwait(false),
            Tag.NameWithoutLanguage => await ReadStringAsync(stream, encoding, cancellationToken).ConfigureAwait(false),
            Tag.Keyword => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.Uri => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.UriScheme => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.Charset => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.NaturalLanguage => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.MimeMediaType => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.MemberAttrName => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned38 => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned39 => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned3A => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned3B => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned3C => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned3D => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned3E => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.OctetStringUnassigned3F => await ReadOctetStringAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned20 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned24 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned25 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned26 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned27 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned28 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned29 => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned2A => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned2B => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned2C => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned2D => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned2E => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.IntegerUnassigned2F => await ReadIntAsync(stream, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned40 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned43 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned4B => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned4C => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned4D => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned4E => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned4F => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned50 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned51 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned52 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned53 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned54 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned55 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned56 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned57 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned58 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned59 => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned5A => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned5B => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned5C => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned5D => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned5E => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.StringUnassigned5F => await ReadStringAsync(stream, null, cancellationToken).ConfigureAwait(false),
            Tag.Extended => await ReadExtendedAsync(stream, cancellationToken).ConfigureAwait(false),
            _ => await ReadUnknownAsync(stream, tag, cancellationToken).ConfigureAwait(false)
        };
    }

    public virtual async Task<IppAttribute> ReadAttributeAsync(Tag tag, IppBinaryReader stream, IppAttribute? prevAttribute, IppAttribute? prevBegCollectionAttribute, Encoding encoding, CancellationToken cancellationToken = default)
    {
        if (stream is null)
            throw new ArgumentNullException( nameof( stream ) );
        var len = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        if (len < 0)
        {
            throw new ArgumentException("Attribute name length cannot be negative");
        }
        var nameBytes = await stream.ReadBytesAsync(len, cancellationToken).ConfigureAwait(false);
        if (nameBytes.Length < len)
        {
            throw new EndOfStreamException("Unexpected end of stream while reading attribute name");
        }
        var name = Encoding.ASCII.GetString(nameBytes);
        var normalizedName = GetNormalizedName(tag, name, prevAttribute, prevBegCollectionAttribute);
        var value = await ReadValueAsync(stream, tag, encoding, cancellationToken).ConfigureAwait(false);
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

    private async Task<IIppRequestMessage> ReadIppRequestAsync( IppBinaryReader reader, CancellationToken cancellationToken = default )
    {
        IppRequestMessage message;
        try
        {
            message = new IppRequestMessage
            {
                Version = new IppVersion( await reader.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false) ),
                IppOperation = (IppOperation)(await reader.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false)),
                RequestId = await reader.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false)
            };
        }
        catch (Exception ex)
        {
            throw new IppRequestException("Failed to parse initial ipp request message.", ex, new IppRequestMessage(), IppStatusCode.ClientErrorBadRequest);
        }

        try
        {
            await ReadSectionsAsync( reader, message, cancellationToken ).ConfigureAwait(false);
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

    private async Task WriteSectionsAsync(IIppRequestMessage requestMessage, IppBinaryWriter writer, CancellationToken cancellationToken)
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
        await WriteSectionAsync(SectionTag.OperationAttributesTag, requestMessage.OperationAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.JobAttributesTag, requestMessage.JobAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.PrinterAttributesTag, requestMessage.PrinterAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.UnsupportedAttributesTag, requestMessage.UnsupportedAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.SubscriptionAttributesTag, requestMessage.SubscriptionAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.EventNotificationAttributesTag, requestMessage.EventNotificationAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.ResourceAttributesTag, requestMessage.ResourceAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.DocumentAttributesTag, requestMessage.DocumentAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        await WriteSectionAsync(SectionTag.SystemAttributesTag, requestMessage.SystemAttributes, writer, encoding, cancellationToken).ConfigureAwait(false);
        //end-of-attributes-tag https://tools.ietf.org/html/rfc8010#section-3.5.1
        await writer.WriteAsync((byte)SectionTag.EndOfAttributesTag, cancellationToken).ConfigureAwait(false);
    }

    private async Task WriteSectionsAsync( IIppResponseMessage responseMessage, IppBinaryWriter writer, Encoding encoding, CancellationToken cancellationToken )
    {
        await WriteSectionsAsync( SectionTag.OperationAttributesTag, responseMessage.OperationAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.JobAttributesTag, responseMessage.JobAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.PrinterAttributesTag, responseMessage.PrinterAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.UnsupportedAttributesTag, responseMessage.UnsupportedAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.SubscriptionAttributesTag, responseMessage.SubscriptionAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.EventNotificationAttributesTag, responseMessage.EventNotificationAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.ResourceAttributesTag, responseMessage.ResourceAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.DocumentAttributesTag, responseMessage.DocumentAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        await WriteSectionsAsync( SectionTag.SystemAttributesTag, responseMessage.SystemAttributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
        //end-of-attributes-tag https://tools.ietf.org/html/rfc8010#section-3.5.1
        await writer.WriteAsync( (byte)SectionTag.EndOfAttributesTag, cancellationToken ).ConfigureAwait(false);
    }

    private async Task WriteSectionsAsync( SectionTag sectionTag, List<List<IppAttribute>> groups, IppBinaryWriter writer, Encoding encoding, CancellationToken cancellationToken )
    {
        foreach ( var attributes in groups )
        {
            await WriteSectionAsync( sectionTag, attributes, writer, encoding, cancellationToken ).ConfigureAwait(false);
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
    public async Task WriteSectionAsync( SectionTag sectionTag, List<IppAttribute> attributes, IppBinaryWriter writer, Encoding encoding, CancellationToken cancellationToken = default )
    {
        if (attributes is null)
            throw new ArgumentNullException( nameof( attributes ) );
        if (writer is null)
            throw new ArgumentNullException( nameof( writer ) );
        if (attributes.Count == 0)
            return;
        //operation-attributes-tag https://tools.ietf.org/html/rfc8010#section-3.5.1
        await writer.WriteAsync( (byte)sectionTag, cancellationToken ).ConfigureAwait(false);
        for (var i = 0; i < attributes.Count; i++)
            await WriteAttributeAsync( writer, attributes[ i ], i > 0 ? attributes[ i - 1 ] : null, encoding, cancellationToken ).ConfigureAwait(false);
    }
}
