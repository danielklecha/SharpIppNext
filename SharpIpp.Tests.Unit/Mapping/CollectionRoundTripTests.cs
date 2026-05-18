// Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping;

/// <summary>
/// Property-based tests for collection attribute round-trips.
/// For each new collection class, verifies that serializing to IEnumerable&lt;IppAttribute&gt;
/// via the collection profile and deserializing back produces field-by-field equality.
///
/// Property 3: Collection Attribute Round-Trip
/// Validates: Requirements 4.5, 8.4
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class CollectionRoundTripTests : MapperTestBase
{
    private const int Iterations = 100;

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static string RandomString(Random rng, int maxLen = 20)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz-";
        var len = rng.Next(1, maxLen + 1);
        return new string(Enumerable.Range(0, len).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
    }

    private static int RandomInt(Random rng) => rng.Next(1, 10000);

    // ── PrinterInputTray ─────────────────────────────────────────────────────

    [TestMethod]
    public void PrinterInputTray_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(42);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new PrinterInputTray
            {
                Type = RandomString(rng),
                Level = RandomInt(rng),
                Status = RandomString(rng),
                MediaSizeX = RandomInt(rng),
                MediaSizeY = RandomInt(rng),
                MediaColor = RandomString(rng),
                MediaInfo = RandomString(rng),
                MediaType = RandomString(rng),
                Unit = RandomString(rng),
                FeedOrientation = RandomString(rng),
            };

            // Serialize to IEnumerable<IppAttribute>
            var attrs = _mapper.Map<PrinterInputTray, IEnumerable<IppAttribute>>(original).ToList();

            // Deserialize back via collection profile
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterInputTray>(dict);

            roundTripped.Type.Should().Be(original.Type, $"iteration {i}: Type mismatch");
            roundTripped.Level.Should().Be(original.Level, $"iteration {i}: Level mismatch");
            roundTripped.Status.Should().Be(original.Status, $"iteration {i}: Status mismatch");
            roundTripped.MediaSizeX.Should().Be(original.MediaSizeX, $"iteration {i}: MediaSizeX mismatch");
            roundTripped.MediaSizeY.Should().Be(original.MediaSizeY, $"iteration {i}: MediaSizeY mismatch");
            roundTripped.MediaColor.Should().Be(original.MediaColor, $"iteration {i}: MediaColor mismatch");
            roundTripped.MediaInfo.Should().Be(original.MediaInfo, $"iteration {i}: MediaInfo mismatch");
            roundTripped.MediaType.Should().Be(original.MediaType, $"iteration {i}: MediaType mismatch");
            roundTripped.Unit.Should().Be(original.Unit, $"iteration {i}: Unit mismatch");
            roundTripped.FeedOrientation.Should().Be(original.FeedOrientation, $"iteration {i}: FeedOrientation mismatch");
        }
    }

    // ── PrinterOutputTray ────────────────────────────────────────────────────

    [TestMethod]
    public void PrinterOutputTray_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(43);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new PrinterOutputTray
            {
                Type = RandomString(rng),
                Level = RandomInt(rng),
                Status = RandomString(rng),
                Unit = RandomString(rng),
                StackingOrder = RandomString(rng),
                PageDelivery = RandomString(rng),
            };

            var attrs = _mapper.Map<PrinterOutputTray, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterOutputTray>(dict);

            roundTripped.Type.Should().Be(original.Type, $"iteration {i}: Type mismatch");
            roundTripped.Level.Should().Be(original.Level, $"iteration {i}: Level mismatch");
            roundTripped.Status.Should().Be(original.Status, $"iteration {i}: Status mismatch");
            roundTripped.Unit.Should().Be(original.Unit, $"iteration {i}: Unit mismatch");
            roundTripped.StackingOrder.Should().Be(original.StackingOrder, $"iteration {i}: StackingOrder mismatch");
            roundTripped.PageDelivery.Should().Be(original.PageDelivery, $"iteration {i}: PageDelivery mismatch");
        }
    }

    // ── PrinterSupply ────────────────────────────────────────────────────────

    [TestMethod]
    public void PrinterSupply_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(44);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new PrinterSupply
            {
                Type = RandomString(rng),
                Level = RandomInt(rng),
                MaxCapacity = RandomInt(rng),
                ColorName = RandomString(rng),
                MarkerName = RandomString(rng),
                MarkerType = RandomString(rng),
                Unit = RandomString(rng),
            };

            var attrs = _mapper.Map<PrinterSupply, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterSupply>(dict);

            roundTripped.Type.Should().Be(original.Type, $"iteration {i}: Type mismatch");
            roundTripped.Level.Should().Be(original.Level, $"iteration {i}: Level mismatch");
            roundTripped.MaxCapacity.Should().Be(original.MaxCapacity, $"iteration {i}: MaxCapacity mismatch");
            roundTripped.ColorName.Should().Be(original.ColorName, $"iteration {i}: ColorName mismatch");
            roundTripped.MarkerName.Should().Be(original.MarkerName, $"iteration {i}: MarkerName mismatch");
            roundTripped.MarkerType.Should().Be(original.MarkerType, $"iteration {i}: MarkerType mismatch");
            roundTripped.Unit.Should().Be(original.Unit, $"iteration {i}: Unit mismatch");
        }
    }

    // ── JobConstraintsSupported ──────────────────────────────────────────────

    [TestMethod]
    public void JobConstraintsSupported_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(45);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new JobConstraintsSupported
            {
                ResolverName = RandomString(rng),
            };

            var attrs = _mapper.Map<JobConstraintsSupported, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, JobConstraintsSupported>(dict);

            roundTripped.ResolverName.Should().Be(original.ResolverName, $"iteration {i}: ResolverName mismatch");
        }
    }

    // ── JobPresetsSupported ──────────────────────────────────────────────────

    [TestMethod]
    public void JobPresetsSupported_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(46);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new JobPresetsSupported
            {
                PresetName = RandomString(rng),
            };

            var attrs = _mapper.Map<JobPresetsSupported, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, JobPresetsSupported>(dict);

            roundTripped.PresetName.Should().Be(original.PresetName, $"iteration {i}: PresetName mismatch");
        }
    }

    // ── JobResolversSupported ────────────────────────────────────────────────

    [TestMethod]
    public void JobResolversSupported_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(47);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new JobResolversSupported
            {
                ResolverName = RandomString(rng),
            };

            var attrs = _mapper.Map<JobResolversSupported, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, JobResolversSupported>(dict);

            roundTripped.ResolverName.Should().Be(original.ResolverName, $"iteration {i}: ResolverName mismatch");
        }
    }

    // ── JobTriggersSupported ─────────────────────────────────────────────────

    [TestMethod]
    public void JobTriggersSupported_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(48);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new JobTriggersSupported
            {
                TriggerName = RandomString(rng),
            };

            var attrs = _mapper.Map<JobTriggersSupported, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, JobTriggersSupported>(dict);

            roundTripped.TriggerName.Should().Be(original.TriggerName, $"iteration {i}: TriggerName mismatch");
        }
    }

    // ── PrintColorModeIccProfile ─────────────────────────────────────────────

    [TestMethod]
    public void PrintColorModeIccProfile_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(49);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new PrintColorModeIccProfile
            {
                PrintColorMode = RandomString(rng),
                ProfileUri = new Uri($"https://example.com/{RandomString(rng)}.icc"),
            };

            var attrs = _mapper.Map<PrintColorModeIccProfile, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrintColorModeIccProfile>(dict);

            roundTripped.PrintColorMode.Should().Be(original.PrintColorMode, $"iteration {i}: PrintColorMode mismatch");
            roundTripped.ProfileUri.Should().Be(original.ProfileUri, $"iteration {i}: ProfileUri mismatch");
        }
    }

    // ── PrinterIccProfile ────────────────────────────────────────────────────

    [TestMethod]
    public void PrinterIccProfile_RoundTrip_FieldByFieldEquality()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var rng = new Random(50);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new PrinterIccProfile
            {
                ProfileName = RandomString(rng),
                ProfileUri = new Uri($"https://example.com/{RandomString(rng)}.icc"),
            };

            var attrs = _mapper.Map<PrinterIccProfile, IEnumerable<IppAttribute>>(original).ToList();
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterIccProfile>(dict);

            roundTripped.ProfileName.Should().Be(original.ProfileName, $"iteration {i}: ProfileName mismatch");
            roundTripped.ProfileUri.Should().Be(original.ProfileUri, $"iteration {i}: ProfileUri mismatch");
        }
    }

    // ── NoValue round-trip ───────────────────────────────────────────────────

    [TestMethod]
    public void PrinterInputTray_NoValue_RoundTrip_PreservesNoValueState()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var noValue = NoValue.GetNoValue<PrinterInputTray>();
        var attrs = _mapper.Map<PrinterInputTray, IEnumerable<IppAttribute>>(noValue).ToList();

        attrs.Should().HaveCount(1);
        attrs[0].Tag.Should().Be(Tag.NoValue);

        var dict = new Dictionary<string, IppAttribute[]>
        {
            [PrinterAttribute.PrinterInputTray] = attrs.ToArray()
        };
        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterInputTray>(dict);
        ((IIppCollection)roundTripped).IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void PrinterSupply_NoValue_RoundTrip_PreservesNoValueState()
    {
        // Feature: pwg5100-spec-parity, Property 3: Collection Attribute Round-Trip
        var noValue = NoValue.GetNoValue<PrinterSupply>();
        var attrs = _mapper.Map<PrinterSupply, IEnumerable<IppAttribute>>(noValue).ToList();

        attrs.Should().HaveCount(1);
        attrs[0].Tag.Should().Be(Tag.NoValue);

        var dict = new Dictionary<string, IppAttribute[]>
        {
            [PrinterAttribute.PrinterSupply] = attrs.ToArray()
        };
        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterSupply>(dict);
        ((IIppCollection)roundTripped).IsValue.Should().BeFalse();
    }
}

// ── Task 11.2: Null Array Serialization Tests ─────────────────────────────────

/// <summary>
/// Property-based tests for null array serialization (Property 4).
/// For each model class with 1setOf-typed properties, verifies that setting those
/// properties to null results in no IppAttribute with the corresponding name in the output.
///
/// Property 4: Null Array Attributes Serialize as Absent
/// Validates: Requirements 8.3
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class NullArraySerializationTests : MapperTestBase
{
    private const int Iterations = 100;

    // ── PrinterDescriptionAttributes — new collection properties ─────────────

    [TestMethod]
    public void PrinterDescriptionAttributes_NullCollectionProperties_SerializeAsAbsent()
    {
        // Feature: pwg5100-spec-parity, Property 4: Null Array Attributes Serialize as Absent
        for (var i = 0; i < Iterations; i++)
        {
            var model = new PrinterDescriptionAttributes
            {
                PrinterInputTray = null,
                PrinterOutputTray = null,
                PrinterSupply = null,
                JobConstraintsSupported = null,
                JobPresetsSupported = null,
                JobResolversSupported = null,
                JobTriggersSupported = null,
                PrintColorModeIccProfile = null,
                PrinterIccProfile = null,
            };

            var dict = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(model);

            dict.Should().NotContainKey(PrinterAttribute.PrinterInputTray,
                $"iteration {i}: null PrinterInputTray should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.PrinterOutputTray,
                $"iteration {i}: null PrinterOutputTray should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.PrinterSupply,
                $"iteration {i}: null PrinterSupply should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.JobConstraintsSupported,
                $"iteration {i}: null JobConstraintsSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.JobPresetsSupported,
                $"iteration {i}: null JobPresetsSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.JobResolversSupported,
                $"iteration {i}: null JobResolversSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.JobTriggersSupported,
                $"iteration {i}: null JobTriggersSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.PrintColorModeIccProfiles,
                $"iteration {i}: null PrintColorModeIccProfile should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.PrinterIccProfiles,
                $"iteration {i}: null PrinterIccProfile should not produce an attribute");
        }
    }

    // ── JobTemplateAttributes — null array properties ─────────────────────────

    [TestMethod]
    public void JobTemplateAttributes_NullArrayProperties_SerializeAsAbsent()
    {
        // Feature: pwg5100-spec-parity, Property 4: Null Array Attributes Serialize as Absent
        for (var i = 0; i < Iterations; i++)
        {
            var model = new JobTemplateAttributes
            {
                Finishings = null,
                FinishingsCol = null,
                PageRanges = null,
                InsertSheet = null,
            };

            var request = _mapper.Map<JobTemplateAttributes, IppRequestMessage>(model);
            var attrNames = request.JobAttributes.Select(a => a.Name).ToHashSet();

            attrNames.Should().NotContain(JobAttribute.Finishings,
                $"iteration {i}: null Finishings should not produce an attribute");
            attrNames.Should().NotContain(JobAttribute.FinishingsCol,
                $"iteration {i}: null FinishingsCol should not produce an attribute");
            attrNames.Should().NotContain(JobAttribute.PageRanges,
                $"iteration {i}: null PageRanges should not produce an attribute");
            attrNames.Should().NotContain(JobAttribute.InsertSheet,
                $"iteration {i}: null InsertSheet should not produce an attribute");
        }
    }

    // ── PrinterDescriptionAttributes — null scalar arrays ────────────────────

    [TestMethod]
    public void PrinterDescriptionAttributes_NullScalarArrayProperties_SerializeAsAbsent()
    {
        // Feature: pwg5100-spec-parity, Property 4: Null Array Attributes Serialize as Absent
        for (var i = 0; i < Iterations; i++)
        {
            var model = new PrinterDescriptionAttributes
            {
                PrinterUriSupported = null,
                UriSecuritySupported = null,
                UriAuthenticationSupported = null,
                DocumentFormatSupported = null,
                OperationsSupported = null,
                PrinterStateReasons = null,
                PrinterSupplyDescription = null,
            };

            var dict = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(model);

            dict.Should().NotContainKey(PrinterAttribute.PrinterUriSupported,
                $"iteration {i}: null PrinterUriSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.UriSecuritySupported,
                $"iteration {i}: null UriSecuritySupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.UriAuthenticationSupported,
                $"iteration {i}: null UriAuthenticationSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.DocumentFormatSupported,
                $"iteration {i}: null DocumentFormatSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.OperationsSupported,
                $"iteration {i}: null OperationsSupported should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.PrinterStateReasons,
                $"iteration {i}: null PrinterStateReasons should not produce an attribute");
            dict.Should().NotContainKey(PrinterAttribute.PrinterSupplyDescription,
                $"iteration {i}: null PrinterSupplyDescription should not produce an attribute");
        }
    }
}
