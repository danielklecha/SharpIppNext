using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class SmartEnumTests
{
    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void StaticFields_AreNotNull(Type type, string _)
    {
        // Arrange
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == type);

        // Act & Assert
        foreach (var field in fields)
        {
            var value = field.GetValue(null);
            value.Should().NotBeNull($"Field {field.Name} in {type.Name} should not be null");
            value?.ToString().Should().NotBeNullOrEmpty($"Field {field.Name} in {type.Name} should have a valid string value");
        }
    }

    public static IEnumerable<object[]> SmartEnumData
    {
        get
        {
            yield return [typeof(BalingWhen), "after-job"];
            yield return [typeof(BalingType), "band"];
            yield return [typeof(BindingType), "adhesive"];
            yield return [typeof(CoatingSides), "both"];
            yield return [typeof(CoatingType), "archival"];
            yield return [typeof(Compression), "gzip"];
            yield return [typeof(CoverType), "no-cover"];
            yield return [typeof(CoveringName), "plain"];
            yield return [typeof(DocumentStateReason), "none"];
            yield return [typeof(FinishingReferenceEdge), "bottom"];
            yield return [typeof(FinishingTemplate), "staple"];
            yield return [typeof(FoldingDirection), "inward"];
            yield return [typeof(ImpositionTemplate), "none"];
            yield return [typeof(JobErrorSheetWhen), "always"];
            yield return [typeof(JobHoldUntil), "no-hold"];
            yield return [typeof(JobPhoneNumberScheme), "tel"];
            yield return [typeof(JobSheets), "none"];
            yield return [typeof(JobSheetsType), "job-sheets"];
            yield return [typeof(JobSpooling), "automatic"];
            yield return [typeof(JobErrorAction), "abandon-job"];
            yield return [typeof(JobReleaseAction), "release-to-printer"];
            yield return [typeof(JobAccountType), "none"];
            yield return [typeof(JobPasswordEncryption), "none"];
            yield return [typeof(IdentifyAction), "display"];
            yield return [typeof(JobStateReason), "none"];
            yield return [typeof(LaminatingType), "archival"];
            yield return [typeof(Media), "iso_a4_210x297mm"];
            yield return [typeof(MediaKey), "media-key"];
            yield return [typeof(MediaCoating), "none"];
            yield return [typeof(MediaColor), "white"];
            yield return [typeof(MediaGrain), "x-direction"];
            yield return [typeof(MediaInputTrayCheck), "main"];
            yield return [typeof(MediaPrePrinted), "blank"];
            yield return [typeof(MediaRecycled), "none"];
            yield return [typeof(MediaSource), "main"];
            yield return [typeof(MediaSourceFeedDirection), "long-edge-first"];
            yield return [typeof(MediaTooth), "smooth"];
            yield return [typeof(MediaType), "stationery"];
            yield return [typeof(MultipleDocumentHandling), "separate-documents-uncollated-copies"];
            yield return [typeof(OutputBin), "top"];
            yield return [typeof(PageDelivery), "same-order-face-up"];
            yield return [typeof(PdlOverride), "attempted"];
            yield return [typeof(PresentationDirectionNumberUp), "tobottom-toleft"];
            yield return [typeof(PrintColorMode), "auto"];
            yield return [typeof(PrintContentOptimize), "auto"];
            yield return [typeof(PrintRenderingIntent), "relative"];
            yield return [typeof(PrintScaling), "auto"];
            yield return [typeof(PrinterStateReason), "none"];
            yield return [typeof(SeparatorSheetsType), "none"];
            yield return [typeof(Sides), "one-sided"];
            yield return [typeof(StitchingMethod), "auto"];
            yield return [typeof(TrimmingType), "draw-line"];
            yield return [typeof(TrimmingWhen), "after-job"];
            yield return [typeof(UriAuthentication), "none"];
            yield return [typeof(UriScheme), "ipp"];
            yield return [typeof(UriSecurity), "none"];
            yield return [typeof(WhichJobs), "completed"];
            yield return [typeof(XImagePosition), "center"];
            yield return [typeof(YImagePosition), "center"];
        }
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ImplicitOperator_ToString_ReturnsValue(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type, value);
        var implicitOperator = type.GetMethod("op_Implicit", new[] { type });

        // Act
        var result = implicitOperator?.Invoke(null, new[] { instance });

        // Assert
        result.Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void PropertyInitializer_SetsValue(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type);
        var property = type.GetProperty("Value");

        // Act
        property?.SetValue(instance, value);

        // Assert
        instance?.ToString().Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ExplicitOperator_FromString_ReturnsInstance(Type type, string value)
    {
        // Arrange
        var explicitOperator = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.Name == "op_Explicit" && m.ReturnType == type);

        // Act
        var result = explicitOperator?.Invoke(null, new object[] { value });

        // Assert
        result.Should().BeOfType(type);
        result?.ToString().Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ToString_ReturnsValue(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type, value);

        // Act
        var result = instance?.ToString();

        // Assert
        result.Should().Be(value);
    }
}
