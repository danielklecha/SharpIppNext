using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppStatusCodeTests
{
    [TestMethod]
    public void ServerErrorTooManyPrinters_ShouldHaveExpectedNumericValue()
    {
        ((short)IppStatusCode.ServerErrorTooManyPrinters).Should().Be(unchecked((short)0x050D));
    }
}
