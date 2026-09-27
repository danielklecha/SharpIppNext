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
                        Type = (InputTrayType)RandomString(rng),
                        Level = RandomInt(rng),
                        Status = RandomString(rng),
                        MediaSizeX = RandomInt(rng),
                        MediaSizeY = RandomInt(rng),
                        MediaColor = (MediaColor)RandomString(rng),
                        MediaInfo = RandomString(rng),
                        MediaType = (MediaType)RandomString(rng),
                        Unit = (CapacityUnit)RandomString(rng),
                        FeedOrientation = (FeedOrientation)RandomString(rng),
                    }
                },
                PrinterOutputTray = new[]
                {
                    new PrinterOutputTray
                    {
                        Type = (OutputTrayType)RandomString(rng),
                        Level = RandomInt(rng),
                        Status = RandomString(rng),
                        Unit = (CapacityUnit)RandomString(rng),
                        StackingOrder = (StackingOrder)RandomString(rng),
                        PageDelivery = (PageDelivery)RandomString(rng),
                    }
                },
                PrinterSupply = new[]
                {
                    new PrinterSupply
                    {
                        Type = (PrinterSupplyType)RandomString(rng),
                        Level = RandomInt(rng),
                        MaxCapacity = RandomInt(rng),
                        ColorName = RandomString(rng),
                        MarkerName = RandomString(rng),
                        MarkerType = (MarkerType)RandomString(rng),
                        Unit = (CapacityUnit)RandomString(rng),
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
                        PrintColorMode = (PrintColorMode)RandomString(rng),
                        ProfileUri = new Uri($"https://example.com/{RandomString(rng)}.icc"),
                    }
                },
                PrinterIccProfile = new[]
                {
                    new PrinterIccProfile
                    {
                        ProfileName = RandomString(rng),
                        ProfileUri = new Uri($"https://example.com/{RandomString(rng)}.icc"),
                    }
                },
                XSide1ImageOffsetSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                XSide2ImageOffsetSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                YSide1ImageOffsetSupported = new SharpIpp.Protocol.Models.Range(2, 20),
                YSide2ImageOffsetSupported = new SharpIpp.Protocol.Models.Range(2, 20),
                UserDefinedValuesSupported = new[] { RandomString(rng) },
                PdlInitFileSupported = new[] { RandomString(rng) },
                PdlInitFileDefault = new PdlInitFile { PdlInitFileName = RandomString(rng), PdlInitFileLocation = new Uri($"https://example.com/{RandomString(rng)}") },
                JobSaveDispositionSupported = new[] { RandomString(rng) },
                JobSaveDispositionDefault = new JobSaveDisposition { SaveDisposition = SaveDisposition.SaveOnly, SaveLocation = new Uri($"https://example.com/{RandomString(rng)}") },
                SaveDispositionSupported = new[] { SaveDisposition.SaveOnly, SaveDisposition.PrintSave },
                SaveInfoSupported = new[] { RandomString(rng) },
                SaveLocationSupported = new[] { new Uri($"https://example.com/{RandomString(rng)}") },
            };

            // Serialize to IDictionary<string, IppAttribute[]>
            var dict = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(original);

            // Deserialize back
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(dict);

            // Assert PrinterInputTray
            roundTripped.PrinterInputTray?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterInputTray!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterInputTray!.Value.Value[0].Type?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].Type?.Value, $"iteration {i}: PrinterInputTray.Type");
            roundTripped.PrinterInputTray!.Value.Value[0].Level?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].Level?.Value, $"iteration {i}: PrinterInputTray.Level");
            roundTripped.PrinterInputTray!.Value.Value[0].Status?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].Status?.Value, $"iteration {i}: PrinterInputTray.Status");
            roundTripped.PrinterInputTray!.Value.Value[0].MediaSizeX?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].MediaSizeX?.Value, $"iteration {i}: PrinterInputTray.MediaSizeX");
            roundTripped.PrinterInputTray!.Value.Value[0].MediaSizeY?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].MediaSizeY?.Value, $"iteration {i}: PrinterInputTray.MediaSizeY");
            roundTripped.PrinterInputTray!.Value.Value[0].MediaColor?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].MediaColor?.Value, $"iteration {i}: PrinterInputTray.MediaColor");
            roundTripped.PrinterInputTray!.Value.Value[0].MediaInfo?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].MediaInfo?.Value, $"iteration {i}: PrinterInputTray.MediaInfo");
            roundTripped.PrinterInputTray!.Value.Value[0].MediaType?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].MediaType?.Value, $"iteration {i}: PrinterInputTray.MediaType");
            roundTripped.PrinterInputTray!.Value.Value[0].Unit?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].Unit?.Value, $"iteration {i}: PrinterInputTray.Unit");
            roundTripped.PrinterInputTray!.Value.Value[0].FeedOrientation?.Value.Should().Be(original.PrinterInputTray!.Value.Value[0].FeedOrientation?.Value, $"iteration {i}: PrinterInputTray.FeedOrientation");

            // Assert PrinterOutputTray
            roundTripped.PrinterOutputTray?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterOutputTray!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterOutputTray!.Value.Value[0].Type?.Value.Should().Be(original.PrinterOutputTray!.Value.Value[0].Type?.Value, $"iteration {i}: PrinterOutputTray.Type");
            roundTripped.PrinterOutputTray!.Value.Value[0].Level?.Value.Should().Be(original.PrinterOutputTray!.Value.Value[0].Level?.Value, $"iteration {i}: PrinterOutputTray.Level");
            roundTripped.PrinterOutputTray!.Value.Value[0].Status?.Value.Should().Be(original.PrinterOutputTray!.Value.Value[0].Status?.Value, $"iteration {i}: PrinterOutputTray.Status");
            roundTripped.PrinterOutputTray!.Value.Value[0].Unit?.Value.Should().Be(original.PrinterOutputTray!.Value.Value[0].Unit?.Value, $"iteration {i}: PrinterOutputTray.Unit");
            roundTripped.PrinterOutputTray!.Value.Value[0].StackingOrder?.Value.Should().Be(original.PrinterOutputTray!.Value.Value[0].StackingOrder?.Value, $"iteration {i}: PrinterOutputTray.StackingOrder");
            roundTripped.PrinterOutputTray!.Value.Value[0].PageDelivery?.Value.Should().Be(original.PrinterOutputTray!.Value.Value[0].PageDelivery?.Value, $"iteration {i}: PrinterOutputTray.PageDelivery");

            // Assert PrinterSupply
            roundTripped.PrinterSupply?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterSupply!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterSupply!.Value.Value[0].Type?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].Type?.Value, $"iteration {i}: PrinterSupply.Type");
            roundTripped.PrinterSupply!.Value.Value[0].Level?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].Level?.Value, $"iteration {i}: PrinterSupply.Level");
            roundTripped.PrinterSupply!.Value.Value[0].MaxCapacity?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].MaxCapacity?.Value, $"iteration {i}: PrinterSupply.MaxCapacity");
            roundTripped.PrinterSupply!.Value.Value[0].ColorName?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].ColorName?.Value, $"iteration {i}: PrinterSupply.ColorName");
            roundTripped.PrinterSupply!.Value.Value[0].MarkerName?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].MarkerName?.Value, $"iteration {i}: PrinterSupply.MarkerName");
            roundTripped.PrinterSupply!.Value.Value[0].MarkerType?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].MarkerType?.Value, $"iteration {i}: PrinterSupply.MarkerType");
            roundTripped.PrinterSupply!.Value.Value[0].Unit?.Value.Should().Be(original.PrinterSupply!.Value.Value[0].Unit?.Value, $"iteration {i}: PrinterSupply.Unit");

            // Assert JobConstraintsSupported
            roundTripped.JobConstraintsSupported?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.JobConstraintsSupported!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobConstraintsSupported!.Value.Value[0].ResolverName?.Value.Should().Be(original.JobConstraintsSupported!.Value.Value[0].ResolverName?.Value, $"iteration {i}: JobConstraintsSupported.ResolverName");

            // Assert JobPresetsSupported
            roundTripped.JobPresetsSupported?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.JobPresetsSupported!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobPresetsSupported!.Value.Value[0].PresetName?.Value.Should().Be(original.JobPresetsSupported!.Value.Value[0].PresetName?.Value, $"iteration {i}: JobPresetsSupported.PresetName");

            // Assert JobResolversSupported
            roundTripped.JobResolversSupported?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.JobResolversSupported!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobResolversSupported!.Value.Value[0].ResolverName?.Value.Should().Be(original.JobResolversSupported!.Value.Value[0].ResolverName?.Value, $"iteration {i}: JobResolversSupported.ResolverName");

            // Assert JobTriggersSupported
            roundTripped.JobTriggersSupported?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.JobTriggersSupported!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.JobTriggersSupported!.Value.Value[0].TriggerName?.Value.Should().Be(original.JobTriggersSupported!.Value.Value[0].TriggerName?.Value, $"iteration {i}: JobTriggersSupported.TriggerName");

            // Assert PrintColorModeIccProfile
            roundTripped.PrintColorModeIccProfile?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.PrintColorModeIccProfile!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrintColorModeIccProfile!.Value.Value[0].PrintColorMode?.Value.Should().Be(original.PrintColorModeIccProfile!.Value.Value[0].PrintColorMode?.Value, $"iteration {i}: PrintColorModeIccProfile.PrintColorMode");
            roundTripped.PrintColorModeIccProfile!.Value.Value[0].ProfileUri?.Value.Should().Be(original.PrintColorModeIccProfile!.Value.Value[0].ProfileUri?.Value, $"iteration {i}: PrintColorModeIccProfile.ProfileUri");

            // Assert PrinterIccProfile
            roundTripped.PrinterIccProfile?.Value.Should().NotBeNull($"iteration {i}");
            roundTripped.PrinterIccProfile!.Value.Value.Should().HaveCount(1, $"iteration {i}");
            roundTripped.PrinterIccProfile!.Value.Value[0].ProfileName?.Value.Should().Be(original.PrinterIccProfile!.Value.Value[0].ProfileName?.Value, $"iteration {i}: PrinterIccProfile.ProfileName");
            roundTripped.PrinterIccProfile!.Value.Value[0].ProfileUri?.Value.Should().Be(original.PrinterIccProfile!.Value.Value[0].ProfileUri?.Value, $"iteration {i}: PrinterIccProfile.ProfileUri");

            roundTripped.XSide1ImageOffsetSupported.Should().Be(original.XSide1ImageOffsetSupported, $"iteration {i}: XSide1ImageOffsetSupported");
            roundTripped.XSide2ImageOffsetSupported.Should().Be(original.XSide2ImageOffsetSupported, $"iteration {i}: XSide2ImageOffsetSupported");
            roundTripped.YSide1ImageOffsetSupported.Should().Be(original.YSide1ImageOffsetSupported, $"iteration {i}: YSide1ImageOffsetSupported");
            roundTripped.YSide2ImageOffsetSupported.Should().Be(original.YSide2ImageOffsetSupported, $"iteration {i}: YSide2ImageOffsetSupported");
            roundTripped.UserDefinedValuesSupported.Should().BeEquivalentTo(original.UserDefinedValuesSupported, $"iteration {i}: UserDefinedValuesSupported");
            roundTripped.PdlInitFileSupported.Should().BeEquivalentTo(original.PdlInitFileSupported, $"iteration {i}: PdlInitFileSupported");
            roundTripped.PdlInitFileDefault.Should().NotBeNull($"iteration {i}: PdlInitFileDefault");
            roundTripped.PdlInitFileDefault!.Value.Value.PdlInitFileName?.Value.Should().Be(original.PdlInitFileDefault!.Value.Value.PdlInitFileName?.Value, $"iteration {i}: PdlInitFileDefault.PdlInitFileName");
            roundTripped.PdlInitFileDefault!.Value.Value.PdlInitFileLocation?.Value.Should().Be(original.PdlInitFileDefault!.Value.Value.PdlInitFileLocation?.Value, $"iteration {i}: PdlInitFileDefault.PdlInitFileLocation");
            roundTripped.JobSaveDispositionSupported.Should().BeEquivalentTo(original.JobSaveDispositionSupported, $"iteration {i}: JobSaveDispositionSupported");
            roundTripped.JobSaveDispositionDefault.Should().NotBeNull($"iteration {i}: JobSaveDispositionDefault");
            roundTripped.JobSaveDispositionDefault!.Value.Value.SaveDisposition?.Value.Should().Be(original.JobSaveDispositionDefault!.Value.Value.SaveDisposition?.Value, $"iteration {i}: JobSaveDispositionDefault.SaveDisposition");
            roundTripped.JobSaveDispositionDefault!.Value.Value.SaveLocation?.Value.Should().Be(original.JobSaveDispositionDefault!.Value.Value.SaveLocation?.Value, $"iteration {i}: JobSaveDispositionDefault.SaveLocation");
            roundTripped.SaveDispositionSupported.Should().BeEquivalentTo(original.SaveDispositionSupported, $"iteration {i}: SaveDispositionSupported");
            roundTripped.SaveInfoSupported.Should().BeEquivalentTo(original.SaveInfoSupported, $"iteration {i}: SaveInfoSupported");
            roundTripped.SaveLocationSupported.Should().BeEquivalentTo(original.SaveLocationSupported, $"iteration {i}: SaveLocationSupported");
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
                JobSaveDisposition = new JobSaveDisposition
                {
                    SaveDisposition = SaveDisposition.SaveOnly,
                    SaveLocation = new Uri($"https://example.com/{RandomString(rng)}"),
                    SaveInfo = new[]
                    {
                        new SaveInfo
                        {
                            SaveLocation = new Uri($"https://example.com/{RandomString(rng)}"),
                            SaveName = RandomString(rng),
                            SaveDocumentFormat = RandomString(rng)
                        }
                    }
                },
                PdlInitFile = new PdlInitFile
                {
                    PdlInitFileName = RandomString(rng),
                    PdlInitFileLocation = new Uri($"https://example.com/{RandomString(rng)}")
                }
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

            roundTripped.JobSaveDisposition.Should().NotBeNull($"iteration {i}: JobSaveDisposition");
            roundTripped.JobSaveDisposition!.Value.Value.SaveDisposition?.Value.Should().Be(original.JobSaveDisposition!.Value.Value.SaveDisposition?.Value, $"iteration {i}: JobSaveDisposition.SaveDisposition");
            roundTripped.JobSaveDisposition!.Value.Value.SaveLocation?.Value.Should().Be(original.JobSaveDisposition!.Value.Value.SaveLocation?.Value, $"iteration {i}: JobSaveDisposition.SaveLocation");
            roundTripped.JobSaveDisposition!.Value.Value.SaveInfo?.Value.Should().HaveCount(1, $"iteration {i}: JobSaveDisposition.SaveInfo");
            roundTripped.JobSaveDisposition!.Value.Value.SaveInfo!.Value.Value[0].SaveLocation?.Value.Should().Be(original.JobSaveDisposition!.Value.Value.SaveInfo!.Value.Value[0].SaveLocation?.Value, $"iteration {i}: JobSaveDisposition.SaveInfo.SaveLocation");
            roundTripped.JobSaveDisposition!.Value.Value.SaveInfo!.Value.Value[0].SaveName?.Value.Should().Be(original.JobSaveDisposition!.Value.Value.SaveInfo!.Value.Value[0].SaveName?.Value, $"iteration {i}: JobSaveDisposition.SaveInfo.SaveName");
            roundTripped.JobSaveDisposition!.Value.Value.SaveInfo!.Value.Value[0].SaveDocumentFormat?.Value.Should().Be(original.JobSaveDisposition!.Value.Value.SaveInfo!.Value.Value[0].SaveDocumentFormat?.Value, $"iteration {i}: JobSaveDisposition.SaveInfo.SaveDocumentFormat");

            roundTripped.PdlInitFile.Should().NotBeNull($"iteration {i}: PdlInitFile");
            roundTripped.PdlInitFile!.Value.Value.PdlInitFileName?.Value.Should().Be(original.PdlInitFile!.Value.Value.PdlInitFileName?.Value, $"iteration {i}: PdlInitFile.PdlInitFileName");
            roundTripped.PdlInitFile!.Value.Value.PdlInitFileLocation?.Value.Should().Be(original.PdlInitFile!.Value.Value.PdlInitFileLocation?.Value, $"iteration {i}: PdlInitFile.PdlInitFileLocation");
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
                JobPagesCompletedCurrentCopy = RandomInt(rng),
                PagesCompletedCurrentCopy = RandomInt(rng),
                PagesPerSubsetActual = new[] { RandomInt(rng), RandomInt(rng) }
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
            roundTripped.JobPagesCompletedCurrentCopy.Should().Be(original.JobPagesCompletedCurrentCopy, $"iteration {i}: JobPagesCompletedCurrentCopy");
            roundTripped.PagesCompletedCurrentCopy.Should().Be(original.PagesCompletedCurrentCopy, $"iteration {i}: PagesCompletedCurrentCopy");
            roundTripped.PagesPerSubsetActual.Should().BeEquivalentTo(original.PagesPerSubsetActual, $"iteration {i}: PagesPerSubsetActual");
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
