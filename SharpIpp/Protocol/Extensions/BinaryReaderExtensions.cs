using System;
using System.IO;

namespace SharpIpp.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="BinaryReader"/>.
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads a 2-byte signed integer from the current stream using big-endian encoding and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The 2-byte signed integer read from the current stream.</returns>
        public static short ReadInt16BigEndian(this BinaryReader reader)
        {
            var value = reader.ReadInt16();

            if (BitConverter.IsLittleEndian)
                value = value.ReverseBytes();

            return value;
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream using big-endian encoding and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The 4-byte signed integer read from the current stream.</returns>
        public static int ReadInt32BigEndian(this BinaryReader reader)
        {
            var value = reader.ReadInt32();

            if (BitConverter.IsLittleEndian)
                value = value.ReverseBytes();

            return value;
        }
    }
}
