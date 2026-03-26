using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;

namespace SharpIpp.Tests.Unit.Mapping.Profiles
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IppRequestProfileTests
    {
        [TestMethod]
        public void Map_IIppSystemRequest_To_IppRequestMessage_Should_Create_New_Message()
        {
            // Arrange
            var mapper = new SimpleMapper();
            var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
            mapper.FillFromAssembly(assembly!);

            var src = new GetSystemAttributesRequest
            {
                Version = new IppVersion(2, 1),
                RequestId = 1234
            };

            // Act
            var result = mapper.Map<GetSystemAttributesRequest, SharpIpp.Protocol.Models.IppRequestMessage>(src);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(src.RequestId, result.RequestId);
            Assert.AreEqual(src.Version.Major, result.Version.Major);
            Assert.AreEqual(src.Version.Minor, result.Version.Minor);
        }

        [TestMethod]
        public void Map_IIppRequestMessage_To_IIppSystemRequest_With_Null_Destination_Should_Throw()
        {
            // Arrange
            var mapper = new SimpleMapper();
            var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
            mapper.FillFromAssembly(assembly!);

            var src = new SharpIpp.Protocol.Models.IppRequestMessage
            {
                Version = new IppVersion(1, 1),
                RequestId = 42
            } as SharpIpp.Protocol.IIppRequestMessage;

            // Act
            Action act = () => mapper.Map(src!, typeof(SharpIpp.Protocol.IIppRequestMessage), typeof(SharpIpp.Models.Requests.IIppSystemRequest));

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Map_IIppSystemRequest_To_IppRequestMessage_When_Destination_Is_Null_Should_Create_Dst()
        {
            // Arrange
            var mapper = new SimpleMapper();
            var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
            mapper.FillFromAssembly(assembly!);

            IIppSystemRequest src = new GetSystemAttributesRequest
            {
                Version = new IppVersion(1, 0),
                RequestId = 777
            };

            // Act
            var result = mapper.Map<IIppSystemRequest, SharpIpp.Protocol.Models.IppRequestMessage>(src);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(777, result.RequestId);
            Assert.AreEqual(1, result.Version.Major);
            Assert.AreEqual(0, result.Version.Minor);
        }

        [TestMethod]
        public void Map_IIppRequestMessage_To_IIppSystemRequest_With_Provided_Destination_Should_Map_Fields()
        {
            // Arrange
            var mapper = new SimpleMapper();
            var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
            mapper.FillFromAssembly(assembly!);

            var src = new SharpIpp.Protocol.Models.IppRequestMessage
            {
                Version = new IppVersion(2, 5),
                RequestId = 314
            } as SharpIpp.Protocol.IIppRequestMessage;

            var dst = new GetSystemAttributesRequest();

            // Act
            var result = mapper.Map<SharpIpp.Protocol.IIppRequestMessage, IIppSystemRequest>(src!, dst);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(dst, result);
            Assert.AreEqual(314, dst.RequestId);
            Assert.AreEqual(2, dst.Version.Major);
            Assert.AreEqual(5, dst.Version.Minor);
        }
    }
}
