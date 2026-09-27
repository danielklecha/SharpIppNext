using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterFinisherTests
{
    [TestMethod]
    public void Parse_WithKnownAndExtensionElements_ShouldParse()
    {
        var raw = "type=stitcher; unit=items; maxcapacity=500; capacity=250; index=1; presentonoff=on; status=0; vendor=x;";

        var parsed = PrinterFinisher.Parse(raw);

        parsed.Should().NotBeNull();
        parsed.Type.Should().Be(FinisherType.Stitcher);
        parsed.Unit.Should().Be(CapacityUnit.Items);
        parsed.MaxCapacity.Should().Be(500);
        parsed.Capacity.Should().Be(250);
        parsed.Index.Should().Be(1);
        parsed.PresentOnOff.Should().Be(PresentOnOff.On);
        parsed.Status.Should().Be(0);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_NullOrWhiteSpace_ShouldThrow()
    {
        Action act1 = () => PrinterFinisher.Parse(null!);
        act1.Should().Throw<ArgumentNullException>();

        Action act2 = () => PrinterFinisher.Parse("");
        act2.Should().Throw<FormatException>();

        Action act3 = () => PrinterFinisher.Parse("   ");
        act3.Should().Throw<FormatException>();
    }

    [TestMethod]
    public void TryParse_ValidInput_ShouldReturnTrueAndPopulateResult()
    {
        var raw = "type=folder; unit=items;";

        var success = PrinterFinisher.TryParse(raw, out var parsed);

        success.Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.Type.Should().Be(FinisherType.Folder);
        parsed.Unit.Should().Be(CapacityUnit.Items);
    }

    [TestMethod]
    public void TryParse_NullOrWhiteSpace_ShouldReturnFalse()
    {
        PrinterFinisher.TryParse(null, out var r1).Should().BeFalse();
        r1.Should().BeNull();

        PrinterFinisher.TryParse("", out var r2).Should().BeFalse();
        r2.Should().BeNull();

        PrinterFinisher.TryParse("   ", out var r3).Should().BeFalse();
        r3.Should().BeNull();
    }

    [TestMethod]
    public void ToString_WithPopulatedModel_ShouldFollowDefinedOrderWithTrailingSemicolon()
    {
        var finisher = new PrinterFinisher
        {
            Type = FinisherType.Stitcher,
            Unit = CapacityUnit.Items,
            MaxCapacity = 500,
            Index = 1,
            PresentOnOff = PresentOnOff.On,
            Status = 0,
            Capacity = 250,
            Extensions = new Dictionary<string, string> { { "vendor", "x" } }
        };

        var raw = finisher.ToString();

        raw.Should().Be("type=stitcher; unit=items; maxcapacity=500; index=1; presentonoff=on; status=0; capacity=250; vendor=x;");
    }

    [TestMethod]
    public void ToString_EmptyModel_ShouldReturnEmptyString()
    {
        var finisher = new PrinterFinisher();
        finisher.ToString().Should().Be(string.Empty);
    }

    [TestMethod]
    public void PrinterFinisher_Properties_And_Extensions_ShouldSynchronizeWithDictionary()
    {
        var finisher = new PrinterFinisher
        {
            Type = "stitcher",
            MaxCapacity = 100,
            Extensions = new Dictionary<string, string> { { "x-custom", "value" } }
        };

        // 1. Check properties are correct
        finisher.Type.Should().Be(FinisherType.Stitcher);
        finisher.MaxCapacity.Should().Be(100);
        finisher.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");

        // 2. Change via properties
        finisher.Type = FinisherType.Folder;
        finisher.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");
        finisher.Extensions.Should().NotContainKey("type");

        // 3. Clear extensions
        finisher.Extensions = null;
        finisher.Extensions.Should().BeNull();
        finisher.Type.Should().Be(FinisherType.Folder);
    }

    [TestMethod]
    public void Equals_WithSameValues_ShouldReturnTrue()
    {
        var finisher1 = new PrinterFinisher
        {
            Type = FinisherType.Stitcher,
            Unit = CapacityUnit.Items,
            MaxCapacity = 500,
            Index = 1,
            PresentOnOff = PresentOnOff.On,
            Status = 0,
            Capacity = 250,
            Extensions = new Dictionary<string, string> { { "vendor", "x" } }
        };
        var finisher2 = new PrinterFinisher
        {
            Type = FinisherType.Stitcher,
            Unit = CapacityUnit.Items,
            MaxCapacity = 500,
            Index = 1,
            PresentOnOff = PresentOnOff.On,
            Status = 0,
            Capacity = 250,
            Extensions = new Dictionary<string, string> { { "vendor", "x" } }
        };

        finisher1.Equals(finisher2).Should().BeTrue();
        finisher1.Equals((object)finisher2).Should().BeTrue();
        finisher1.Equals(finisher1).Should().BeTrue();
        ((IEquatable<PrinterFinisher>)finisher1).Equals(finisher2).Should().BeTrue();
        finisher1.GetHashCode().Should().Be(finisher2.GetHashCode());
    }

    [TestMethod]
    public void Equals_WithDifferentValues_ShouldReturnFalse()
    {
        var finisher1 = new PrinterFinisher
        {
            Type = FinisherType.Stitcher
        };
        var finisher2 = new PrinterFinisher
        {
            Type = FinisherType.Folder
        };

        finisher1.Equals(finisher2).Should().BeFalse();
        finisher1.Equals((object)finisher2).Should().BeFalse();
        ((IEquatable<PrinterFinisher>)finisher1).Equals(finisher2).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_WithNullOrDifferentType_ShouldReturnFalse()
    {
        var finisher = new PrinterFinisher
        {
            Type = FinisherType.Stitcher
        };

        finisher!.Equals((PrinterFinisher?)null).Should().BeFalse();
        finisher!.Equals((object?)null).Should().BeFalse();
        finisher!.Equals(new object()).Should().BeFalse();
        ((IEquatable<PrinterFinisher>)finisher!).Equals(null).Should().BeFalse();
    }

    [TestMethod]
    public void ExplicitOperator_String_NonNullAndNull()
    {
        var finisher = new PrinterFinisher { Type = FinisherType.Stitcher };
        var str = (string)finisher;
        str.Should().Be("type=stitcher;");

        var nullStr = (string)(PrinterFinisher)null!;
        nullStr.Should().BeNull();
    }

    [TestMethod]
    public void ExplicitOperator_FromString_ValidAndInvalid()
    {
        var raw = "type=stitcher;";
        var finisher = (PrinterFinisher)raw;
        finisher.Type.Should().Be(FinisherType.Stitcher);

        Action actNull = () => { var _ = (PrinterFinisher)(string)null!; };
        actNull.Should().Throw<ArgumentNullException>();

        Action actEmpty = () => { var _ = (PrinterFinisher)""; };
        actEmpty.Should().Throw<FormatException>();
    }

    [TestMethod]
    public void ExplicitOperator_ByteArray_NonNullAndNull()
    {
        var finisher = new PrinterFinisher { Type = FinisherType.Stitcher };
        var bytes = (byte[])finisher;
        bytes.Should().Equal(System.Text.Encoding.UTF8.GetBytes("type=stitcher;"));

        var nullBytes = (byte[])(PrinterFinisher)null!;
        nullBytes.Should().Equal(Array.Empty<byte>());
    }

    [TestMethod]
    public void ExplicitOperator_FromByteArray_ValidAndInvalid()
    {
        var raw = "type=stitcher;";
        var bytes = System.Text.Encoding.UTF8.GetBytes(raw);
        var finisher = (PrinterFinisher)bytes;
        finisher.Type.Should().Be(FinisherType.Stitcher);

        Action actNull = () => { var _ = (PrinterFinisher)(byte[])null!; };
        actNull.Should().Throw<ArgumentNullException>();

        Action actEmpty = () => { var _ = (PrinterFinisher)Array.Empty<byte>(); };
        actEmpty.Should().Throw<FormatException>();
    }

    [TestMethod]
    public void ExplicitOperator_OctetString_NonNullAndNull()
    {
        var finisher = new PrinterFinisher { Type = FinisherType.Stitcher };
        var octet = (OctetString)finisher;
        octet.ToString().Should().Be("type=stitcher;");

        var nullOctet = (OctetString)(PrinterFinisher)null!;
        nullOctet.ToString().Should().Be(string.Empty);
    }

    [TestMethod]
    public void ExplicitOperator_FromOctetString_ValidAndInvalid()
    {
        var raw = "type=stitcher;";
        var octet = new OctetString(raw);
        var finisher = (PrinterFinisher)octet;
        finisher.Type.Should().Be(FinisherType.Stitcher);

        Action actDefault = () => { var _ = (PrinterFinisher)default(OctetString); };
        actDefault.Should().Throw<FormatException>();
    }
}
