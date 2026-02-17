using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Protocol.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IppResponseMessageExtensionsTests
    {
        [TestMethod]
        [DataRow((IppStatusCode)(-1), false)]
        [DataRow(IppStatusCode.SuccessfulOk, true)]
        [DataRow(IppStatusCode.SuccessfulOkIgnoredOrSubstitutedAttributes, true)]
        [DataRow(IppStatusCode.SuccessfulOkConflictingAttributes, true)]
        [DataRow(IppStatusCode.SuccessfulOkIgnoredSubscriptions, true)]
        [DataRow(IppStatusCode.SuccessfulOkTooManyEvents, true)]
        [DataRow(IppStatusCode.SuccessfulOkEventsComplete, true)]
        [DataRow((IppStatusCode)0x0008, false)]
        [DataRow(IppStatusCode.ClientErrorBadRequest, false)]
        [DataRow(IppStatusCode.ServerErrorInternalError, false)]
        public void IsSuccessfulStatusCode_ShouldIdentifySuccessCorrectly(IppStatusCode statusCode, bool expected)
        {
            var mock = new Mock<IIppResponseMessage>();
            mock.Setup(x => x.StatusCode).Returns(statusCode);

            mock.Object.IsSuccessfulStatusCode().Should().Be(expected);
        }
    }
}
