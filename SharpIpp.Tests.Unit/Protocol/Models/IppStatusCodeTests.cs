using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
public class IppStatusCodeTests
{
    [TestMethod]
    public void ServerErrorTooManyPrinters_ShouldHaveExpectedNumericValue()
    {
        ((short)IppStatusCode.ServerErrorTooManyPrinters).Should().Be(unchecked((short)0x050D));
    }
}
