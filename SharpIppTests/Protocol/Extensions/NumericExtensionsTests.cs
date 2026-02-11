using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Protocol.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NumericExtensionsTests
    {
        [TestMethod]
        public void ReverseBytes_UInt16_ShouldFlipBytes()
        {
            ushort value = 0x1234;
            value.ReverseBytes().Should().Be(0x3412);
        }

        [TestMethod]
        public void ReverseBytes_Int16_ShouldFlipBytes()
        {
            short value = 0x1234;
            value.ReverseBytes().Should().Be(0x3412);
        }

        [TestMethod]
        public void ReverseBytes_UInt32_ShouldReverseBytes()
        {
            uint value = 0x12345678;
            value.ReverseBytes().Should().Be(0x78563412);
        }

        [TestMethod]
        public void ReverseBytes_Int32_ShouldReverseBytes()
        {
            int value = 0x12345678;
            value.ReverseBytes().Should().Be(0x78563412);
        }

        [TestMethod]
        public void ReverseBytes_UInt64_ShouldReverseBytes()
        {
            ulong value = 0x1234567890ABCDEF;
            value.ReverseBytes().Should().Be(0xEFCDAB9078563412);
        }

        [TestMethod]
        public void ReverseBytes_Int64_ShouldReverseBytes()
        {
            long value = 0x1234567890ABCDEF;
            value.ReverseBytes().Should().Be(unchecked((long)0xEFCDAB9078563412));
        }

        [TestMethod]
        public void ReverseBytes_Single_ShouldReverseBytes()
        {
            float value = 1.23456f;
            var expectedBytes = BitConverter.GetBytes(value);
            Array.Reverse(expectedBytes);
            var expected = BitConverter.ToSingle(expectedBytes, 0);

            value.ReverseBytes().Should().Be(expected);
        }

        [TestMethod]
        public void ReverseBytes_Double_ShouldReverseBytes()
        {
            double value = 1.23456789;
            var expectedBytes = BitConverter.GetBytes(value);
            Array.Reverse(expectedBytes);
            var expected = BitConverter.ToDouble(expectedBytes, 0);

            value.ReverseBytes().Should().Be(expected);
        }
    }
}
