using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
public class IppEncodingHelperTests
{
    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void GetEncoding_WhenNullOrWhitespace_ReturnsUtf8(string? charsetName)
    {
        var result = IppEncodingHelper.GetEncoding(charsetName);

        result.Should().BeSameAs(Encoding.UTF8);
    }

    [TestMethod]
    [DataRow("utf-8")]
    [DataRow("UTF-8")]
    [DataRow("utf8")]
    [DataRow("UTF8")]
    [DataRow("  utf-8  ")]
    [DataRow("  UTF8  ")]
    public void GetEncoding_WhenUtf8FastPath_ReturnsUtf8Singleton(string charsetName)
    {
        var result = IppEncodingHelper.GetEncoding(charsetName);

        result.Should().BeSameAs(Encoding.UTF8);
    }

    [TestMethod]
    [DataRow("us-ascii")]
    [DataRow("US-ASCII")]
    [DataRow("ascii")]
    [DataRow("ASCII")]
    [DataRow("  us-ascii  ")]
    [DataRow("  ASCII  ")]
    public void GetEncoding_WhenAsciiFastPath_ReturnsAsciiSingleton(string charsetName)
    {
        var result = IppEncodingHelper.GetEncoding(charsetName);

        result.Should().BeSameAs(Encoding.ASCII);
    }

    [TestMethod]
    [DataRow("iso-8859-1")]
    [DataRow("ISO-8859-1")]
    [DataRow("utf-16")]
    public void GetEncoding_WhenValidOtherCharset_ReturnsExpectedEncoding(string charsetName)
    {
        var result = IppEncodingHelper.GetEncoding(charsetName);

        result.Should().NotBeNull();
        result.WebName.Should().Be(Encoding.GetEncoding(charsetName.Trim()).WebName);
    }

    [TestMethod]
    [DataRow("invalid-charset-name")]
    [DataRow("unknown-12345")]
    [DataRow("???")]
    public void GetEncoding_WhenUnrecognizedCharset_FallsBackToUtf8(string charsetName)
    {
        var result = IppEncodingHelper.GetEncoding(charsetName);

        result.Should().BeSameAs(Encoding.UTF8);
    }

    [TestMethod]
    public void GetEncoding_FromAttribute_WhenNullAttribute_ReturnsUtf8()
    {
        var result = IppEncodingHelper.GetEncoding((IppAttribute?)null);

        result.Should().BeSameAs(Encoding.UTF8);
    }

    [TestMethod]
    public void GetEncoding_FromAttribute_WhenValueIsNull_ReturnsUtf8()
    {
        IppAttribute attr = default;

        var result = IppEncodingHelper.GetEncoding(attr);

        result.Should().BeSameAs(Encoding.UTF8);
    }

    [TestMethod]
    public void GetEncoding_FromAttribute_WhenValidFastPath_ReturnsMatchingSingleton()
    {
        var utf8Attr = new IppAttribute(Tag.Charset, "attributes-charset", "utf-8");
        var asciiAttr = new IppAttribute(Tag.Charset, "attributes-charset", "us-ascii");

        IppEncodingHelper.GetEncoding(utf8Attr).Should().BeSameAs(Encoding.UTF8);
        IppEncodingHelper.GetEncoding(asciiAttr).Should().BeSameAs(Encoding.ASCII);
    }

    [TestMethod]
    public void GetEncoding_FromAttribute_WhenOtherSupportedCharset_ReturnsEncoding()
    {
        var attr = new IppAttribute(Tag.Charset, "attributes-charset", "iso-8859-1");

        var result = IppEncodingHelper.GetEncoding(attr);

        result.WebName.Should().Be("iso-8859-1");
    }

    [TestMethod]
    public void GetEncoding_FromAttribute_WhenInvalidCharset_FallsBackToUtf8()
    {
        var attr = new IppAttribute(Tag.Charset, "attributes-charset", "unrecognized-encoding");

        var result = IppEncodingHelper.GetEncoding(attr);

        result.Should().BeSameAs(Encoding.UTF8);
    }
}
