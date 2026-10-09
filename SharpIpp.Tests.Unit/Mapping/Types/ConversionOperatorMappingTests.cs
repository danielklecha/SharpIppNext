using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Types;

[TestClass]
[ExcludeFromCodeCoverage]
public class ConversionOperatorMappingTests : MapperTestBase
{
    [TestMethod]
    public void IppVersion_ExplicitCasts_And_MapperRoundTrips()
    {
        var version = new IppVersion(2, 0);

        // Explicit casts in C#
        var strVal = (string)version;
        strVal.Should().Be("2.0");

        var backToVersion = (IppVersion)strVal;
        backToVersion.Should().Be(version);

        // SimpleMapper round-trip
        _mapper.Map<string>(version).Should().Be("2.0");
        _mapper.Map<IppVersion>("2.0").Should().Be(version);

        // NoValue mapping
        var noVal = _mapper.Map<IppValue<IppVersion>>(NoValue.Instance);
        noVal.IsValue.Should().BeFalse();

        // Array mapping
        var versions = new[] { new IppVersion(1, 1), new IppVersion(2, 0) };
        var strArr = _mapper.Map<string[]>(versions);
        strArr.Should().Equal("1.1", "2.0");

        var backArr = _mapper.Map<IppVersion[]>(strArr);
        backArr.Should().Equal(versions);
    }

    [TestMethod]
    public void StringWithLanguage_ExplicitCasts_And_MapperRoundTrips()
    {
        var model = new StringWithLanguage("fr", "bonjour");

        // Explicit and implicit casts in C#
        var strVal = (string?)model;
        strVal.Should().Be("bonjour");
        string? implicitStr = model;
        implicitStr.Should().Be("bonjour");

        var fromStr = (StringWithLanguage)"hello";
        fromStr.Language.Should().BeNull();
        fromStr.Value.Should().Be("hello");
        fromStr.HasLanguage.Should().BeFalse();

        StringWithLanguage implicitFromStr = "hello";
        implicitFromStr.Language.Should().BeNull();
        implicitFromStr.Value.Should().Be("hello");

        // SimpleMapper round-trip
        _mapper.Map<string>(model).Should().Be("bonjour");
        var mapped = _mapper.Map<StringWithLanguage>("hello");
        mapped.Language.Should().BeNull();
        mapped.Value.Should().Be("hello");

        // NoValue mapping
        var noVal = _mapper.Map<IppValue<StringWithLanguage>>(NoValue.Instance);
        noVal.IsValue.Should().BeFalse();

        var noValNullable = _mapper.MapNullable<StringWithLanguage?>(NoValue.Instance);
        noValNullable.HasValue.Should().BeTrue();
        noValNullable!.Value.IsValue.Should().BeFalse();

        var strNullable = _mapper.MapNullable<StringWithLanguage?>("hello");
        strNullable.HasValue.Should().BeTrue();
        strNullable!.Value.Value.Should().Be("hello");

        // MapFromDicNullable verification without hardcoded check
        var dictMissing = new Dictionary<string, IppAttribute[]>();
        _mapper.MapFromDicNullable<StringWithLanguage?>(dictMissing, "key").Should().BeNull();

        var dictNoValue = new Dictionary<string, IppAttribute[]>
        {
            { "key", new[] { new IppAttribute(Tag.NoValue, "key", NoValue.Instance) } }
        };
        var resNoVal = _mapper.MapFromDicNullable<StringWithLanguage?>(dictNoValue, "key");
        resNoVal.HasValue.Should().BeTrue();
        resNoVal!.Value.IsValue.Should().BeFalse();

        var dictStr = new Dictionary<string, IppAttribute[]>
        {
            { "key", new[] { new IppAttribute(Tag.NameWithoutLanguage, "key", "printer-1") } }
        };
        var resStr = _mapper.MapFromDicNullable<StringWithLanguage?>(dictStr, "key");
        resStr.HasValue.Should().BeTrue();
        resStr!.Value.Value.Should().Be("printer-1");
        resStr.Value.Language.Should().BeNull();

        var dictSwl = new Dictionary<string, IppAttribute[]>
        {
            { "key", new[] { new IppAttribute(Tag.NameWithLanguage, "key", new StringWithLanguage("en", "printer-1")) } }
        };
        var resSwl = _mapper.MapFromDicNullable<StringWithLanguage?>(dictSwl, "key");
        resSwl.HasValue.Should().BeTrue();
        resSwl!.Value.Value.Should().Be("printer-1");
        resSwl.Value.Language.Should().Be("en");
    }

    [TestMethod]
    public void Range_ExplicitCasts_And_MapperRoundTrips()
    {
        var range = new SharpIpp.Protocol.Models.Range(1, 10);

        // Implicit cast int -> Range
        SharpIpp.Protocol.Models.Range fromInt = 42;
        fromInt.Lower.Should().Be(42);
        fromInt.Upper.Should().Be(42);

        // SimpleMapper
        var mappedRange = _mapper.Map<SharpIpp.Protocol.Models.Range>(42);
        mappedRange.Lower.Should().Be(42);
        mappedRange.Upper.Should().Be(42);

        // NoValue mapping
        var noVal = _mapper.Map<IppValue<SharpIpp.Protocol.Models.Range>>(NoValue.Instance);
        noVal.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void OctetString_ExplicitCasts_And_MapperRoundTrips()
    {
        var bytes = Encoding.UTF8.GetBytes("sample-data");
        var octet = new OctetString(bytes);

        // Explicit cast to string
        var strVal = (string?)octet;
        strVal.Should().Be("sample-data");

        // Implicit cast from string and byte[]
        OctetString fromStr = "sample-data";
        fromStr.Value.Should().Equal(bytes);

        OctetString fromBytes = bytes;
        fromBytes.Value.Should().Equal(bytes);

        byte[]? backBytes = octet;
        backBytes.Should().Equal(bytes);

        // SimpleMapper conversions
        _mapper.Map<string>(octet).Should().Be("sample-data");
        _mapper.Map<OctetString>("sample-data").Value.Should().Equal(bytes);
        _mapper.Map<byte[]>(octet).Should().Equal(bytes);
        _mapper.Map<OctetString>(bytes).Value.Should().Equal(bytes);

        // NoValue mapping
        var noVal = _mapper.Map<IppValue<OctetString>>(NoValue.Instance);
        noVal.IsValue.Should().BeFalse();

        // Array mapping
        var octetArr = new[] { new OctetString("a"), new OctetString("b") };
        var stringArr = _mapper.Map<string[]>(octetArr);
        stringArr.Should().Equal("a", "b");
    }

    [TestMethod]
    public void StructuredStrings_ExplicitCasts_And_MapperRoundTrips()
    {
        // PrinterAlert
        var alert = new PrinterAlert { Code = "jam", Severity = "critical" };
        var alertStr = (string)alert;
        alertStr.Should().Be("code=jam;severity=critical");

        var fromAlertStr = (PrinterAlert)alertStr;
        fromAlertStr.Code.Should().Be("jam");
        fromAlertStr.Severity.Should().Be("critical");

        var alertBytes = (byte[])alert;
        alertBytes.Should().Equal(Encoding.UTF8.GetBytes(alertStr));

        var fromAlertBytes = (PrinterAlert)alertBytes;
        fromAlertBytes.Code.Should().Be("jam");

        var alertOctet = (OctetString)alert;
        alertOctet.ToString().Should().Be(alertStr);

        var fromAlertOctet = (PrinterAlert?)alertOctet;
        fromAlertOctet!.Code.Should().Be("jam");

        // Mapper round trips
        _mapper.Map<string>(alert).Should().Be(alertStr);
        _mapper.Map<PrinterAlert>(alertStr).Code.Should().Be("jam");
        _mapper.Map<byte[]>(alert).Should().Equal(alertBytes);
        _mapper.Map<PrinterAlert>(alertBytes).Code.Should().Be("jam");
        _mapper.Map<OctetString>(alert).ToString().Should().Be(alertStr);
        _mapper.Map<PrinterAlert>(alertOctet).Code.Should().Be("jam");

        // PrinterFinisher
        var finisher = new PrinterFinisher { Unit = "stapler", Capacity = 100 };
        var finisherStr = (string)finisher;
        var fromFinisherStr = (PrinterFinisher)finisherStr;
        fromFinisherStr.Unit?.Value.Should().Be("stapler");
        fromFinisherStr.Capacity.Should().Be(100);

        _mapper.Map<string>(finisher).Should().Be(finisherStr);
        _mapper.Map<PrinterFinisher>(finisherStr).Unit?.Value.Should().Be("stapler");

        // PrinterFinisherSupply
        var supply = new PrinterFinisherSupply { Color = "blue", Level = 50 };
        var supplyStr = (string)supply;
        var fromSupplyStr = (PrinterFinisherSupply)supplyStr;
        fromSupplyStr.Color.Should().Be("blue");
        fromSupplyStr.Level.Should().Be(50);

        var supplyBytes = (byte[])supply;
        supplyBytes.Should().Equal(Encoding.UTF8.GetBytes(supplyStr));

        var fromSupplyBytes = (PrinterFinisherSupply)supplyBytes;
        fromSupplyBytes.Color.Should().Be("blue");

        var supplyOctet = (OctetString)supply;
        supplyOctet.ToString().Should().Be(supplyStr);

        var fromSupplyOctet = (PrinterFinisherSupply?)supplyOctet;
        fromSupplyOctet!.Color.Should().Be("blue");

        _mapper.Map<string>(supply).Should().Be(supplyStr);
        _mapper.Map<PrinterFinisherSupply>(supplyStr).Color.Should().Be("blue");
        _mapper.Map<byte[]>(supply).Should().Equal(supplyBytes);
        _mapper.Map<PrinterFinisherSupply>(supplyBytes).Color.Should().Be("blue");
        _mapper.Map<OctetString>(supply).ToString().Should().Be(supplyStr);
        _mapper.Map<PrinterFinisherSupply>(supplyOctet).Color.Should().Be("blue");

        // DocumentMetadata
        var metadata = new DocumentMetadata { Title = "Doc1", Creator = "Author1" };
        var metadataStr = (string)metadata;
        var fromMetadataStr = (DocumentMetadata)metadataStr;
        fromMetadataStr.Title.Should().Be("Doc1");
        fromMetadataStr.Creator.Should().Be("Author1");

        _mapper.Map<string>(metadata).Should().Be(metadataStr);
        _mapper.Map<DocumentMetadata>(metadataStr).Title.Should().Be("Doc1");
    }


    [TestMethod]
    public void ProtocolEnums_GeneratedMapper_Conversions()
    {
        // int <-> JobState
        _mapper.Map<JobState>(3).Should().Be(JobState.Pending);
        _mapper.Map<int>(JobState.Pending).Should().Be(3);
        _mapper.Map<IppValue<JobState>>(NoValue.Instance).IsValue.Should().BeFalse();

        // int <-> IppStatusCode (short underlying)
        _mapper.Map<IppStatusCode>(0x0000).Should().Be(IppStatusCode.SuccessfulOk);
        _mapper.Map<int>(IppStatusCode.SuccessfulOk).Should().Be(0);
        _mapper.Map<IppValue<IppStatusCode>>(NoValue.Instance).IsValue.Should().BeFalse();

        // int <-> IppOperation (short underlying)
        _mapper.Map<IppOperation>(0x0002).Should().Be(IppOperation.PrintJob);
        _mapper.Map<int>(IppOperation.PrintJob).Should().Be(2);

        // int <-> PrinterState
        _mapper.Map<PrinterState>(3).Should().Be(PrinterState.Idle);
        _mapper.Map<int>(PrinterState.Idle).Should().Be(3);
        _mapper.Map<IppValue<PrinterState>>(NoValue.Instance).IsValue.Should().BeFalse();

        // int <-> Finishings
        _mapper.Map<Finishings>(3).Should().Be(Finishings.None);
        _mapper.Map<int>(Finishings.None).Should().Be(3);

        // int <-> ResolutionUnit
        _mapper.Map<ResolutionUnit>(3).Should().Be(ResolutionUnit.DotsPerInch);
        _mapper.Map<int>(ResolutionUnit.DotsPerInch).Should().Be(3);

        // int <-> PrintQuality
        _mapper.Map<PrintQuality>(3).Should().Be(PrintQuality.Draft);
        _mapper.Map<int>(PrintQuality.Draft).Should().Be(3);
    }

    [TestMethod]
    public void StructuredStrings_EmptyOctetString_CollectionMapping_MapsToEmptyModels()
    {
        var validFinisher = new OctetString("type=stitcher;");
        var emptyFinisher = new OctetString(Array.Empty<byte>());
        var whitespaceFinisher = new OctetString("   ");

        var finishers = _mapper.Map<PrinterFinisher[]>(new[] { validFinisher, emptyFinisher, whitespaceFinisher });
        finishers.Should().HaveCount(3);
        finishers[0].Type.Should().Be(FinisherType.Stitcher);
        finishers[1].Count.Should().Be(0);
        finishers[2].Count.Should().Be(0);

        var allEmptyFinishers = _mapper.Map<PrinterFinisher[]>(new[] { emptyFinisher, whitespaceFinisher });
        allEmptyFinishers.Should().HaveCount(2);
        allEmptyFinishers.Should().OnlyContain(x => x.Count == 0);

        var validSupply = new OctetString("class=consumed; type=staples;");
        var emptySupply = new OctetString(string.Empty);

        var supplies = _mapper.Map<PrinterFinisherSupply[]>(new[] { validSupply, emptySupply });
        supplies.Should().HaveCount(2);
        supplies[0].Class.Should().Be(FinisherSupplyClass.Consumed);
        supplies[0].Type.Should().Be(FinisherSupplyType.Staples);
        supplies[1].Count.Should().Be(0);

        var allEmptySupplies = _mapper.Map<PrinterFinisherSupply[]>(new[] { emptySupply });
        allEmptySupplies.Should().HaveCount(1);
        allEmptySupplies[0].Count.Should().Be(0);
    }
}
