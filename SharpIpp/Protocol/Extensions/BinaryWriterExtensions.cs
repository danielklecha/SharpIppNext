using System;
using System.IO;

namespace SharpIpp.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="BinaryWriter"/>.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes a two-byte signed integer to the current stream using big-endian encoding and advances the stream position by two bytes.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        /// <param name="value">The two-byte signed integer to write.</param>
        public static void WriteBigEndian(this BinaryWriter writer, short value)
        {
            if (BitConverter.IsLittleEndian)
                value = value.ReverseBytes();

            writer.Write(value);
        }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream using big-endian encoding and advances the stream position by four bytes.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        /// <param name="value">The four-byte signed integer to write.</param>
        public static void WriteBigEndian(this BinaryWriter writer, int value)
        {
            if (BitConverter.IsLittleEndian)
                value = value.ReverseBytes();

            writer.Write(value);
        }
    }
}
