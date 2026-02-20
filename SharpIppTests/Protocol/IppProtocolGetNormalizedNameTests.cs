using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Protocol.Tests;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppProtocolGetNormalizedNameTests
{
    [TestMethod]
    public void GetNormalizedName_PrevBegCollection_ShouldReturnPrevName()
    {
        // Case for verifying 1setOf Collection support (formerly line 418 break)
        var prevAttribute = new IppAttribute(Tag.BegCollection, "test-collection", NoValue.Instance);
        
        var name = IppProtocol.GetNormalizedName(Tag.BegCollection, "", prevAttribute, null);
        name.Should().Be("test-collection");
    }

    [TestMethod]
    public void GetNormalizedName_PrevEndCollection_NoPrevBegCollection_ShouldThrow()
    {
        // Case for Line 422 (break) coverage
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        
        Action act = () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);
        
        act.Should().Throw<ArgumentException>().WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public void GetNormalizedName_PrevEndCollection_PrevBegCollectionNoName_ShouldThrow()
    {
        // Case for Line 422/420 (condition false) coverage
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        var prevBeg = new IppAttribute(Tag.BegCollection, "", NoValue.Instance); // Empty name

        Action act = () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, prevBeg);

        act.Should().Throw<ArgumentException>().WithMessage("0 length attribute name found not in a 1setOf");
    }

    [TestMethod]
    public void GetNormalizedName_Default_PrevNameEmpty_ShouldThrow()
    {
        // Case for Line 428 (break) coverage
        // Use a tag that hits default (e.g. Integer) but has empty name
        var prevAttribute = new IppAttribute(Tag.Integer, "", 1);

        Action act = () => IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);

        act.Should().Throw<ArgumentException>().WithMessage("0 length attribute name found not in a 1setOf");
    }
    
    [TestMethod]
    public void GetNormalizedName_PrevMember_NameIsValue()
    {
        var prevAttribute = new IppAttribute(Tag.MemberAttrName, "", "member-name");
        var name = IppProtocol.GetNormalizedName(Tag.Integer, "", prevAttribute, null);
        name.Should().Be("member-name");
    }

    [TestMethod]
    public void GetNormalizedName_EndCollection_TakesPrevBegCollectionName()
    {
        var prevAttribute = new IppAttribute(Tag.EndCollection, "", NoValue.Instance);
        var prevBeg = new IppAttribute(Tag.BegCollection, "collection-name", NoValue.Instance);

        var name = IppProtocol.GetNormalizedName(Tag.BegCollection, "", prevAttribute, prevBeg);
        name.Should().Be("collection-name");
    }
}
