using SharpIpp.Protocol.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;
[TestClass]
[ExcludeFromCodeCoverage]
public class StringExtensionsTests
{
    [TestMethod]
    [DataRow("printer-uri", "PrinterUri")]
    [DataRow("job-name", "JobName")]
    [DataRow("attributes-charset", "AttributesCharset")]
    public void ConvertKebabCaseToCamelCase_ShouldConvertCorrectly(string input, string expected)
    {
        input.ConvertKebabCaseToCamelCase().Should().Be(expected);
    }

    [TestMethod]
    [DataRow("PrinterUri", "printer-uri")]
    [DataRow("JobName", "job-name")]
    [DataRow("AttributesCharset", "attributes-charset")]
    public void ConvertCamelCaseToKebabCase_ShouldConvertCorrectly(string input, string expected)
    {
        input.ConvertCamelCaseToKebabCase().Should().Be(expected);
    }
}
