using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

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

    [TestMethod]
    [DataRow("staple-top-left", Tag.Keyword)]
    [DataRow("Staple Top Left", Tag.NameWithoutLanguage)]
    [DataRow("staple_top_left", Tag.NameWithoutLanguage)]
    public void GetKeywordOrNameTag_ShouldReturnCorrectTag(string input, Tag expected)
    {
        input.GetKeywordOrNameTag().Should().Be(expected);
    }

    [TestMethod]
    public void GetKeywordOrNameTag_WithCustomRegex_ShouldUseProvidedRegex()
    {
        var customRegex = new Regex("^(?:top|bottom)$", RegexOptions.Compiled);

        "top".GetKeywordOrNameTag(customRegex).Should().Be(Tag.Keyword);
        "staple-top-left".GetKeywordOrNameTag(customRegex).Should().Be(Tag.NameWithoutLanguage);
    }
}
