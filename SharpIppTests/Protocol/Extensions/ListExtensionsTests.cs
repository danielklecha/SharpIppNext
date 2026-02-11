using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Protocol.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void Populate_ShouldAddAndReplaceAttributes()
        {
            var list = new List<IppAttribute>
            {
                new IppAttribute(Tag.Charset, "attributes-charset", "old-charset"),
                new IppAttribute(Tag.Uri, "printer-uri", "ipp://localhost")
            };

            var other = new List<IppAttribute>
            {
                new IppAttribute(Tag.Charset, "attributes-charset", "new-charset"),
                new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en")
            };

            list.Populate(other);

            list.Should().HaveCount(3);
            list.Find(x => x.Name == "attributes-charset")?.Value.Should().Be("new-charset");
            list.Find(x => x.Name == "printer-uri")?.Value.Should().Be("ipp://localhost");
            list.Find(x => x.Name == "attributes-natural-language")?.Value.Should().Be("en");
        }

        [TestMethod]
        public void Populate_WithNull_ShouldDoNothing()
        {
            var list = new List<IppAttribute> { new IppAttribute(Tag.Charset, "a", "b") };
            list.Populate(null);
            list.Should().HaveCount(1);
        }

        [TestMethod]
        public void Populate_ShouldNotRemoveAttributesWithNullOrEmptyNames()
        {
            var list = new List<IppAttribute>
            {
                new IppAttribute(Tag.Charset, null!, "val1"),
                new IppAttribute(Tag.Charset, string.Empty, "val2"),
                new IppAttribute(Tag.Charset, "real-name", "val3")
            };

            var other = new List<IppAttribute>
            {
                new IppAttribute(Tag.Charset, "real-name", "new-val")
            };

            list.Populate(other);

            list.Should().HaveCount(3);
            list.Should().Contain(x => x.Name == null && (string)x.Value == "val1");
            list.Should().Contain(x => x.Name == string.Empty && (string)x.Value == "val2");
            list.Find(x => x.Name == "real-name")?.Value.Should().Be("new-val");
        }
    }
}
