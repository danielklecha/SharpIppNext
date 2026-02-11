using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIppTests.Protocol.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IppSectionExtensionsTests
    {
        [TestMethod]
        public void AllAttributes_ShouldGroupByName()
        {
            var section = new IppSection
            {
                Tag = SectionTag.OperationAttributesTag
            };
            section.Attributes.AddRange(new[]
            {
                new IppAttribute(Tag.Charset, "attributes-charset", "utf-8"),
                new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en"),
                new IppAttribute(Tag.Keyword, "operations-supported", 1),
                new IppAttribute(Tag.Keyword, "operations-supported", 2)
            });

            var result = section.AllAttributes();

            result.Should().HaveCount(3);
            result["operations-supported"].Should().HaveCount(2);
            result["operations-supported"][0].Value.Should().Be(1);
            result["operations-supported"][1].Value.Should().Be(2);
        }
    }
}
