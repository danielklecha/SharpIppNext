using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Validation;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class ValidationContextTests
{
    [TestMethod]
    public void DefaultStruct_HasUtf8EncodingAndEmptyMemberName()
    {
        ValidationContext context = default;

        context.Encoding.Should().Be(Encoding.UTF8);
        context.MemberName.Should().BeEmpty();
    }

    [TestMethod]
    public void ParameterlessConstructor_HasUtf8EncodingAndEmptyMemberName()
    {
        var context = new ValidationContext();

        context.Encoding.Should().Be(Encoding.UTF8);
        context.MemberName.Should().BeEmpty();
    }

    [TestMethod]
    public void Constructor_WithExplicitValues_PreservesValues()
    {
        var context = new ValidationContext(Encoding.ASCII, "CustomMember");

        context.Encoding.Should().Be(Encoding.ASCII);
        context.MemberName.Should().Be("CustomMember");
    }

    [TestMethod]
    public void Constructor_WithNullValues_DefaultsToUtf8AndEmptyMemberName()
    {
        var context = new ValidationContext(null, null);

        context.Encoding.Should().Be(Encoding.UTF8);
        context.MemberName.Should().BeEmpty();
    }
}
