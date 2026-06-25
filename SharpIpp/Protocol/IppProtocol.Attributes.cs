using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppProtocol
{
    private static readonly byte Plus = Encoding.ASCII.GetBytes("+")[0];
    private static readonly byte Minus = Encoding.ASCII.GetBytes("-")[0];

    private static async Task WriteAsync(NoValue _, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)0, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<NoValue> ReadNoValueAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);

        if (length != 0)
        {
            throw new ArgumentException($"Expected no-value length: 0, actual :{length}");
        }

        return NoValue.Instance;
    }

    private static async Task WriteAsync(bool value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)1, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync(value ? (byte)0x01 : (byte)0x00, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<bool> ReadBoolAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);

        if (length != 1)
        {
            throw new ArgumentException($"Expected bool value length: 1, actual :{length}");
        }

        var value = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);

        if (value == 0x00)
        {
            return false;
        }

        if (value == 0x01)
        {
            return true;
        }

        throw new ArgumentException($"boolean value {value} not supported");
    }

    private static async Task WriteAsync(DateTimeOffset value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)11, cancellationToken).ConfigureAwait(false);
        await stream.WriteBigEndianAsync((short)value.Year, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)value.Month, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)value.Day, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)value.Hour, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)value.Minute, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)value.Second, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)(value.Millisecond / 100), cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync(value.Offset > TimeSpan.Zero ? Plus : Minus, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)Math.Abs(value.Offset.Hours), cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)Math.Abs(value.Offset.Minutes), cancellationToken).ConfigureAwait(false);
    }

    private static async Task<DateTimeOffset> ReadDateTimeOffsetAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);

        if ( length != 11 )
        {
            throw new ArgumentException( $"Expected datetime value length: 11, actual :{length}" );
        }

        var year = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        var month = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var day = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var hour = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var minute = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var second = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var decisecond = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var plusMinus = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);

        var offsetDir = plusMinus == Plus ? 1 :
            plusMinus == Minus ? -1 :
            throw new ArgumentException($"DateTime offset direction {plusMinus} not supported");
        var offsetHour = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        var offsetMinute = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);

        var dateTimeOffset = new DateTimeOffset(year,
            month,
            day,
            hour,
            minute,
            second,
            decisecond * 100,
            new TimeSpan(offsetHour * offsetDir, offsetMinute * offsetDir, 0));
        return dateTimeOffset;
    }

    private static async Task WriteAsync(int value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)4, cancellationToken).ConfigureAwait(false);
        await stream.WriteBigEndianAsync(value, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<int> ReadIntAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);

        if (length != 4)
        {
            throw new ArgumentException($"Expected integer value length: 4, actual :{length}");
        }

        var value = await stream.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
        return value;
    }

    private static async Task WriteAsync(Models.Range value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)8, cancellationToken).ConfigureAwait(false);
        await stream.WriteBigEndianAsync(value.Lower, cancellationToken).ConfigureAwait(false);
        await stream.WriteBigEndianAsync(value.Upper, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<Models.Range> ReadRangeAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);

        if (length != 8)
        {
            throw new ArgumentException($"Expected range value length: 8, actual :{length}");
        }

        var lower = await stream.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
        var upper = await stream.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
        return new Models.Range(lower, upper);
    }

    private static async Task WriteAsync(byte[] value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)value.Length, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync(value, cancellationToken).ConfigureAwait(false);
    }

    private static async Task WriteAsync(OctetString value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await WriteAsync(value.Value, stream, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<OctetString> ReadOctetStringAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        if (length < 0)
        {
            throw new ArgumentException("OctetString length cannot be negative");
        }
        var bytes = await stream.ReadBytesAsync(length, cancellationToken).ConfigureAwait(false);
        if (bytes.Length < length)
        {
            throw new EndOfStreamException("Unexpected end of stream while reading octet string");
        }
        return new OctetString(bytes);
    }

    private static async Task WriteAsync(Resolution value, IppBinaryWriter stream, CancellationToken cancellationToken = default)
    {
        await stream.WriteBigEndianAsync((short)9, cancellationToken).ConfigureAwait(false);
        await stream.WriteBigEndianAsync(value.Width, cancellationToken).ConfigureAwait(false);
        await stream.WriteBigEndianAsync(value.Height, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync((byte)value.Units, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<Resolution> ReadResolutionAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        var length = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);

        if (length != 9)
        {
            throw new ArgumentException($"Expected resolution value length: 9, actual :{length}");
        }

        var width = await stream.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
        var height = await stream.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
        var units = await stream.ReadByteAsync(cancellationToken).ConfigureAwait(false);
        return new Resolution(width, height, (ResolutionUnit)units);
    }

    public static async Task WriteAsync(string value, IppBinaryWriter stream, Encoding? encoding, CancellationToken cancellationToken = default)
    {
        var bytes = (encoding ?? Encoding.ASCII).GetBytes(value);
        await stream.WriteBigEndianAsync((short)bytes.Length, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<string> ReadStringAsync(IppBinaryReader stream, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        var result = await ReadStringWithLengthAsync(stream, encoding, cancellationToken).ConfigureAwait(false);
        return result.Value;
    }

    private static async Task<UnknownValue> ReadUnknownAsync(IppBinaryReader stream, Tag tag, CancellationToken cancellationToken = default)
    {
        short len;
        try
        {
            len = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is EndOfStreamException || ex is ObjectDisposedException)
        {
            throw new ArgumentException("Invalid unknown value payload", nameof(stream), ex);
        }

        if (len < 0)
        {
            throw new ArgumentException("Invalid unknown value payload", nameof(stream));
        }

        byte[] raw;
        try
        {
            raw = await stream.ReadBytesAsync(len, cancellationToken).ConfigureAwait(false);
            if (raw.Length < len)
            {
                throw new EndOfStreamException();
            }
        }
        catch (Exception ex) when (ex is EndOfStreamException || ex is ObjectDisposedException)
        {
            throw new ArgumentException("Invalid unknown value payload", nameof(stream), ex);
        }

        return new UnknownValue(tag, raw);
    }

    private static async Task<ExtendedValue> ReadExtendedAsync(IppBinaryReader stream, CancellationToken cancellationToken = default)
    {
        short len;
        try
        {
            len = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is EndOfStreamException || ex is ObjectDisposedException)
        {
            throw new ArgumentException("Invalid extended value payload", nameof(stream), ex);
        }

        if (len < 4)
        {
            throw new ArgumentException($"Expected extended value length >= 4, actual: {len}");
        }

        int extendedTag;
        byte[] raw;
        try
        {
            extendedTag = await stream.ReadInt32BigEndianAsync(cancellationToken).ConfigureAwait(false);
            raw = await stream.ReadBytesAsync(len - 4, cancellationToken).ConfigureAwait(false);
            if (raw.Length < len - 4)
            {
                throw new EndOfStreamException();
            }
        }
        catch (Exception ex) when (ex is EndOfStreamException || ex is ObjectDisposedException)
        {
            throw new ArgumentException("Invalid extended value payload", nameof(stream), ex);
        }

        return new ExtendedValue(extendedTag, raw);
    }

    private static async Task<(string Value, short Length)> ReadStringWithLengthAsync(IppBinaryReader stream, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        var len = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        if (len < 0)
        {
            throw new ArgumentException("String length cannot be negative");
        }
        var bytes = await stream.ReadBytesAsync(len, cancellationToken).ConfigureAwait(false);
        if (bytes.Length < len)
        {
            throw new EndOfStreamException("Unexpected end of stream while reading string");
        }
        return ((encoding ?? Encoding.ASCII).GetString(bytes), len);
    }

    private static async Task WriteAsync(StringWithLanguage value, IppBinaryWriter stream, Encoding? encoding, CancellationToken cancellationToken = default)
    {
        var languageBytes = Encoding.ASCII.GetBytes(value.Language);
        var valueBytes = (encoding ?? Encoding.ASCII).GetBytes(value.Value);
        await stream.WriteBigEndianAsync((short)(languageBytes.Length + valueBytes.Length + 4), cancellationToken).ConfigureAwait(false);
        await WriteAsync(value.Language, stream, null, cancellationToken).ConfigureAwait(false);
        await WriteAsync(value.Value, stream, encoding, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<StringWithLanguage> ReadStringWithLanguageAsync(IppBinaryReader stream, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        var len = await stream.ReadInt16BigEndianAsync(cancellationToken).ConfigureAwait(false);
        var language = await ReadStringWithLengthAsync(stream, null, cancellationToken).ConfigureAwait(false);
        var value = await ReadStringWithLengthAsync(stream, encoding, cancellationToken).ConfigureAwait(false);
        if (len != language.Length + value.Length + 4)
        {
            throw new ArgumentException($"Expected string-with-language length: {language.Length + value.Length + 4}, actual: {len}");
        }
        return new StringWithLanguage(language.Value, value.Value);
    }
}
