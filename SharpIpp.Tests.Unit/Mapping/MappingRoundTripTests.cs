// Feature: pwg5100-spec-parity, Property 2: Request Mapping Round-Trip
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping;

/// <summary>
/// Property-based tests for request mapping round-trips.
/// For each model class that received new properties, verifies that
/// serializing to IppRequestMessage and deserializing back produces field-by-field equality.
///
/// Property 2: Request Mapping Round-Trip
/// Validates: Requirements 2.6, 3.5, 8.1, 8.2, 9.4
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class MappingRoundTripTests : MapperTestBase
{
    private const int Iterations = 100;

    private static string RandomString(Random rng, int maxLen = 20)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz-";
        var len = rng.Next(1, maxLen + 1);
        return new string(Enumerable.Range(0, len).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
    }

    private static int RandomInt(Random rng) => rng.Next(1, 10000);

    // ── PrinterDescriptionAttributes — new collection properties ─────────────

    [TestMethod]
    public void PrinterDescriptionAttributes_NewCollectionProperties_RoundTrip()
    {
        // Feature: pwg5100-spec-parity, Property 2: Request Mapping Round-Trip
        var rng = new Random(100);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new PrinterDescriptionAttributes
            {
                PrinterInputTray = new[]
                {
                    new PrinterInputTray
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
                    }
                },
                PrinterOutputTray = new[]
                {
                    new PrinterOutputTray
                    {
                        Type = RandomString(rng),
                        Level = RandomInt(rng),
                        Status = RandomString(rng),
                        Unit = RandomString(rng),
                        StackingOrder = RandomString(rng),
                        PageDelivery = RandomString(rng),
                    }
                },
                PrinterSupply = new[]
                {
                    new PrinterSupply
                    {
                        Type = RandomString(rng),
                        Level = RandomInt(rng),
                        MaxCapacity = RandomInt(rng),
                        ColorName = RandomString(rng),
                        MarkerName = RandomString(rng),
                        MarkerType = RandomString(rng),
                        Unit = RandomString(rng),
                    }
                },
                JobConstraintsSupported = new[]
                {
                    new JobConstraintsSupported { ResolverName = RandomString(rng) }
                },
                JobPresetsSupported = new[]
                {
                    new JobPresetsSupported { PresetName = RandomString(rng) }
                },
                JobResolversSupported = new[]
                {
                    new JobResolversSupported { ResolverName = RandomString(rng) }
                },
                JobTriggersSupported = new[]
                {
                    new JobTriggersSupported { TriggerName = RandomString(rng) }
                },
                PrintColorModeIccProfile = new[]
                {
                    new PrintColorModeIccProfile
                    {
                        PrintColorMode = RandomString(rng),
                        IccProfileResourceId = RandomInt(rng),
                    }
                },
                PrinterIccProfile = new[]
                {
                    new PrinterIccProfile
                    {
                        ProfileName = RandomString(rng),
                        IccProfileResourceId = RandomInt(rng),
                    }
                },
            };

            // Serialize to IDictionary<string, IppAttribute[]>
            var dict = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(original);

            // Deserialize back
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(dict);

            // Assert PrinterInputTray
            roundTripped.PrinterInputTray.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterInputTray!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterInputTray[0].Type.Should().Be(original.PrinterInputTray[0].Type, $"iteration {i}: PrinterInputTray.Type");
            roundTripped.PrinterInputTray[0].Level.Should().Be(original.PrinterInputTray[0].Level, $"iteration {i}: PrinterInputTray.Level");
            roundTripped.PrinterInputTray[0].Status.Should().Be(original.PrinterInputTray[0].Status, $"iteration {i}: PrinterInputTray.Status");
            roundTripped.PrinterInputTray[0].MediaSizeX.Should().Be(original.PrinterInputTray[0].MediaSizeX, $"iteration {i}: PrinterInputTray.MediaSizeX");
            roundTripped.PrinterInputTray[0].MediaSizeY.Should().Be(original.PrinterInputTray[0].MediaSizeY, $"iteration {i}: PrinterInputTray.MediaSizeY");
            roundTripped.PrinterInputTray[0].MediaColor.Should().Be(original.PrinterInputTray[0].MediaColor, $"iteration {i}: PrinterInputTray.MediaColor");
            roundTripped.PrinterInputTray[0].MediaInfo.Should().Be(original.PrinterInputTray[0].MediaInfo, $"iteration {i}: PrinterInputTray.MediaInfo");
            roundTripped.PrinterInputTray[0].MediaType.Should().Be(original.PrinterInputTray[0].MediaType, $"iteration {i}: PrinterInputTray.MediaType");
            roundTripped.PrinterInputTray[0].Unit.Should().Be(original.PrinterInputTray[0].Unit, $"iteration {i}: PrinterInputTray.Unit");
            roundTripped.PrinterInputTray[0].FeedOrientation.Should().Be(original.PrinterInputTray[0].FeedOrientation, $"iteration {i}: PrinterInputTray.FeedOrientation");

            // Assert PrinterOutputTray
            roundTripped.PrinterOutputTray.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterOutputTray!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterOutputTray[0].Type.Should().Be(original.PrinterOutputTray[0].Type, $"iteration {i}: PrinterOutputTray.Type");
            roundTripped.PrinterOutputTray[0].Level.Should().Be(original.PrinterOutputTray[0].Level, $"iteration {i}: PrinterOutputTray.Level");
            roundTripped.PrinterOutputTray[0].Status.Should().Be(original.PrinterOutputTray[0].Status, $"iteration {i}: PrinterOutputTray.Status");
            roundTripped.PrinterOutputTray[0].Unit.Should().Be(original.PrinterOutputTray[0].Unit, $"iteration {i}: PrinterOutputTray.Unit");
            roundTripped.PrinterOutputTray[0].StackingOrder.Should().Be(original.PrinterOutputTray[0].StackingOrder, $"iteration {i}: PrinterOutputTray.StackingOrder");
            roundTripped.PrinterOutputTray[0].PageDelivery.Should().Be(original.PrinterOutputTray[0].PageDelivery, $"iteration {i}: PrinterOutputTray.PageDelivery");

            // Assert PrinterSupply
            roundTripped.PrinterSupply.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterSupply!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterSupply[0].Type.Should().Be(original.PrinterSupply[0].Type, $"iteration {i}: PrinterSupply.Type");
            roundTripped.PrinterSupply[0].Level.Should().Be(original.PrinterSupply[0].Level, $"iteration {i}: PrinterSupply.Level");
            roundTripped.PrinterSupply[0].MaxCapacity.Should().Be(original.PrinterSupply[0].MaxCapacity, $"iteration {i}: PrinterSupply.MaxCapacity");
            roundTripped.PrinterSupply[0].ColorName.Should().Be(original.PrinterSupply[0].ColorName, $"iteration {i}: PrinterSupply.ColorName");
            roundTripped.PrinterSupply[0].MarkerName.Should().Be(original.PrinterSupply[0].MarkerName, $"iteration {i}: PrinterSupply.MarkerName");
            roundTripped.PrinterSupply[0].MarkerType.Should().Be(original.PrinterSupply[0].MarkerType, $"iteration {i}: PrinterSupply.MarkerType");
            roundTripped.PrinterSupply[0].Unit.Should().Be(original.PrinterSupply[0].Unit, $"iteration {i}: PrinterSupply.Unit");

            // Assert JobConstraintsSupported
            roundTripped.JobConstraintsSupported.Should().NotBeNull($"iteration {i}");
            roundTripped.JobConstraintsSupported!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobConstraintsSupported[0].ResolverName.Should().Be(original.JobConstraintsSupported[0].ResolverName, $"iteration {i}: JobConstraintsSupported.ResolverName");

            // Assert JobPresetsSupported
            roundTripped.JobPresetsSupported.Should().NotBeNull($"iteration {i}");
            roundTripped.JobPresetsSupported!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobPresetsSupported[0].PresetName.Should().Be(original.JobPresetsSupported[0].PresetName, $"iteration {i}: JobPresetsSupported.PresetName");

            // Assert JobResolversSupported
            roundTripped.JobResolversSupported.Should().NotBeNull($"iteration {i}");
            roundTripped.JobResolversSupported!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobResolversSupported[0].ResolverName.Should().Be(original.JobResolversSupported[0].ResolverName, $"iteration {i}: JobResolversSupported.ResolverName");

            // Assert JobTriggersSupported
            roundTripped.JobTriggersSupported.Should().NotBeNull($"iteration {i}");
            roundTripped.JobTriggersSupported!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobTriggersSupported[0].TriggerName.Should().Be(original.JobTriggersSupported[0].TriggerName, $"iteration {i}: JobTriggersSupported.TriggerName");

            // Assert PrintColorModeIccProfile
            roundTripped.PrintColorModeIccProfile.Should().NotBeNull($"iteration {i}");
            roundTripped.PrintColorModeIccProfile!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrintColorModeIccProfile[0].PrintColorMode.Should().Be(original.PrintColorModeIccProfile[0].PrintColorMode, $"iteration {i}: PrintColorModeIccProfile.PrintColorMode");
            roundTripped.PrintColorModeIccProfile[0].IccProfileResourceId.Should().Be(original.PrintColorModeIccProfile[0].IccProfileResourceId, $"iteration {i}: PrintColorModeIccProfile.IccProfileResourceId");

            // Assert PrinterIccProfile
            roundTripped.PrinterIccProfile.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterIccProfile!.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterIccProfile[0].ProfileName.Should().Be(original.PrinterIccProfile[0].ProfileName, $"iteration {i}: PrinterIccProfile.ProfileName");
            roundTripped.PrinterIccProfile[0].IccProfileResourceId.Should().Be(original.PrinterIccProfile[0].IccProfileResourceId, $"iteration {i}: PrinterIccProfile.IccProfileResourceId");
        }
    }

    // ── JobTemplateAttributes — new properties round-trip ────────────────────

    [TestMethod]
    public void JobTemplateAttributes_NewProperties_RoundTrip()
    {
        // Feature: pwg5100-spec-parity, Property 2: Request Mapping Round-Trip
        var rng = new Random(101);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new JobTemplateAttributes
            {
                JobAccountId = RandomString(rng),
                JobAccountingUserId = RandomString(rng),
                JobCancelAfter = RandomInt(rng),
                JobSheetMessage = RandomString(rng),
                JobMessageToOperator = RandomString(rng),
                JobRecipientName = RandomString(rng),
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                PrintRenderingIntent = PrintRenderingIntent.Perceptual,
                JobErrorAction = JobErrorAction.AbortJob,
                PrintContentOptimize = PrintContentOptimize.Auto,
                NumberUp = RandomInt(rng),
                Copies = RandomInt(rng),
            };

            var request = _mapper.Map<JobTemplateAttributes, IppRequestMessage>(original);
            var roundTripped = _mapper.Map<IIppRequestMessage, JobTemplateAttributes>((IIppRequestMessage)request);

            roundTripped.JobAccountId.Should().Be(original.JobAccountId, $"iteration {i}: JobAccountId");
            roundTripped.JobAccountingUserId.Should().Be(original.JobAccountingUserId, $"iteration {i}: JobAccountingUserId");
            roundTripped.JobCancelAfter.Should().Be(original.JobCancelAfter, $"iteration {i}: JobCancelAfter");
            roundTripped.JobSheetMessage.Should().Be(original.JobSheetMessage, $"iteration {i}: JobSheetMessage");
            roundTripped.JobMessageToOperator.Should().Be(original.JobMessageToOperator, $"iteration {i}: JobMessageToOperator");
            roundTripped.JobRecipientName.Should().Be(original.JobRecipientName, $"iteration {i}: JobRecipientName");
            roundTripped.PrintScaling.Should().Be(original.PrintScaling, $"iteration {i}: PrintScaling");
            roundTripped.PrintColorMode.Should().Be(original.PrintColorMode, $"iteration {i}: PrintColorMode");
            roundTripped.PrintRenderingIntent.Should().Be(original.PrintRenderingIntent, $"iteration {i}: PrintRenderingIntent");
            roundTripped.JobErrorAction.Should().Be(original.JobErrorAction, $"iteration {i}: JobErrorAction");
            roundTripped.PrintContentOptimize.Should().Be(original.PrintContentOptimize, $"iteration {i}: PrintContentOptimize");
            roundTripped.NumberUp.Should().Be(original.NumberUp, $"iteration {i}: NumberUp");
            roundTripped.Copies.Should().Be(original.Copies, $"iteration {i}: Copies");
        }
    }

    // ── JobDescriptionAttributes — new properties round-trip ─────────────────

    [TestMethod]
    public void JobDescriptionAttributes_NewProperties_RoundTrip()
    {
        // Feature: pwg5100-spec-parity, Property 2: Request Mapping Round-Trip
        var rng = new Random(102);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new JobDescriptionAttributes
            {
                JobId = RandomInt(rng),
                JobName = RandomString(rng),
                JobOriginatingUserName = RandomString(rng),
                JobStateMessage = RandomString(rng),
                JobStateReasons = new[] { JobStateReason.JobPrinting },
                JobState = JobState.Processing,
                JobPages = RandomInt(rng),
                JobProcessingTime = RandomInt(rng),
                ErrorsCount = RandomInt(rng),
                WarningsCount = RandomInt(rng),
            };

            // JobDescriptionAttributes maps to/from IDictionary<string, IppAttribute[]>
            var dict = _mapper.Map<JobDescriptionAttributes, IDictionary<string, IppAttribute[]>>(original);
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, JobDescriptionAttributes>(dict);

            roundTripped.JobId.Should().Be(original.JobId, $"iteration {i}: JobId");
            roundTripped.JobName.Should().Be(original.JobName, $"iteration {i}: JobName");
            roundTripped.JobOriginatingUserName.Should().Be(original.JobOriginatingUserName, $"iteration {i}: JobOriginatingUserName");
            roundTripped.JobStateMessage.Should().Be(original.JobStateMessage, $"iteration {i}: JobStateMessage");
            roundTripped.JobStateReasons.Should().BeEquivalentTo(original.JobStateReasons, $"iteration {i}: JobStateReasons");
            roundTripped.JobState.Should().Be(original.JobState, $"iteration {i}: JobState");
            roundTripped.JobPages.Should().Be(original.JobPages, $"iteration {i}: JobPages");
            roundTripped.JobProcessingTime.Should().Be(original.JobProcessingTime, $"iteration {i}: JobProcessingTime");
            roundTripped.ErrorsCount.Should().Be(original.ErrorsCount, $"iteration {i}: ErrorsCount");
            roundTripped.WarningsCount.Should().Be(original.WarningsCount, $"iteration {i}: WarningsCount");
        }
    }

    // ── DocumentTemplateAttributes — new properties round-trip ───────────────

    [TestMethod]
    public void DocumentTemplateAttributes_NewProperties_RoundTrip()
    {
        // Feature: pwg5100-spec-parity, Property 2: Request Mapping Round-Trip
        var rng = new Random(103);

        for (var i = 0; i < Iterations; i++)
        {
            var original = new DocumentTemplateAttributes
            {
                Copies = RandomInt(rng),
                NumberUp = RandomInt(rng),
                Sides = Sides.OneSided,
                OrientationRequested = Orientation.Portrait,
                PrintQuality = PrintQuality.Normal,
            };

            // DocumentTemplateAttributes maps to/from List<IppAttribute>
            var attrs = _mapper.Map<DocumentTemplateAttributes, List<IppAttribute>>(original);
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, DocumentTemplateAttributes>(dict);

            roundTripped.Copies.Should().Be(original.Copies, $"iteration {i}: Copies");
            roundTripped.NumberUp.Should().Be(original.NumberUp, $"iteration {i}: NumberUp");
            roundTripped.Sides.Should().Be(original.Sides, $"iteration {i}: Sides");
            roundTripped.OrientationRequested.Should().Be(original.OrientationRequested, $"iteration {i}: OrientationRequested");
            roundTripped.PrintQuality.Should().Be(original.PrintQuality, $"iteration {i}: PrintQuality");
        }
    }
}
