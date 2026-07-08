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

    public static IEnumerable<object[]> SmartEnumData =>
        typeof(ISmartEnum).Assembly
            .GetTypes()
            .Where(type => typeof(ISmartEnum).IsAssignableFrom(type) && type is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(type => type.FullName)
            .Select(type => new object[] { type, GetSampleValue(type) });

    public static IEnumerable<object[]> MarkedSmartEnumData =>
        typeof(IMarkedSmartEnum).Assembly
            .GetTypes()
            .Where(type => typeof(IMarkedSmartEnum).IsAssignableFrom(type) && type is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(type => type.FullName)
            .Select(type => new object[] { type, GetSampleValue(type) });

    private static string GetSampleValue(Type type)
    {
        var knownValue = type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => field.FieldType == type)
            .Select(field => field.GetValue(null)?.ToString())
            .FirstOrDefault(value => !string.IsNullOrEmpty(value));

        return knownValue ?? "sample-value";
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ImplicitOperator_ToString_ReturnsValue(Type type, string value)
    {
        // Arrange
        var instance = CreatePopulatedSmartEnum(type, value);
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
    [DynamicData(nameof(MarkedSmartEnumData))]
    public void PropertyInitializer_SetsIsMarked(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type);
        var valueProperty = type.GetProperty("Value");
        var keywordProperty = type.GetProperty("IsMarked");

        // Act
        valueProperty?.SetValue(instance, value);
        keywordProperty?.SetValue(instance, true);

        // Assert
        keywordProperty.Should().NotBeNull($"{type.Name} should expose IsMarked property");
        keywordProperty?.GetValue(instance).Should().Be(true);
        instance.Should().BeAssignableTo<IMarkedSmartEnum>();
        ((IMarkedSmartEnum)instance!).Value.Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void PropertyInitializer_SetsIsValue(Type type, string value)
    {
        // Arrange
        var instance = CreatePopulatedSmartEnum(type, value);
        var property = type.GetProperty("IsValue");

        // Act
        property?.SetValue(instance, false);

        // Assert
        property.Should().NotBeNull($"{type.Name} should expose IsValue property");
        property?.GetValue(instance).Should().Be(false);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ConversionOperator_FromString_ReturnsInstance(Type type, string value)
    {
        // Arrange
        var conversionOperator = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => (m.Name == "op_Implicit" || m.Name == "op_Explicit") && m.ReturnType == type);

        // Act
        var result = conversionOperator?.Invoke(null, new object[] { value });

        // Assert
        result.Should().BeOfType(type);
        result?.ToString().Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ToString_ReturnsValue(Type type, string value)
    {
        // Arrange
        var instance = CreatePopulatedSmartEnum(type, value);

        // Act
        var result = instance?.ToString();

        // Assert
        result.Should().Be(value);
    }

    internal static object CreatePopulatedSmartEnum(Type type, string value)
    {
        var threeArgumentConstructor = type.GetConstructor([typeof(string), typeof(bool), typeof(bool)]);
        if (threeArgumentConstructor != null)
            return threeArgumentConstructor.Invoke([value, true, true]);

        var twoArgumentConstructor = type.GetConstructor([typeof(string), typeof(bool)]);
        if (twoArgumentConstructor != null)
            return twoArgumentConstructor.Invoke([value, true]);

        throw new MissingMethodException($"No supported constructor found for smart enum type '{type.FullName}'.");
    }

    [TestMethod]
    [DataRow(typeof(AccuracyUnits))]
    [DataRow(typeof(BalingType))]
    [DataRow(typeof(BalingWhen))]
    [DataRow(typeof(BindingType))]
    [DataRow(typeof(CapacityUnit))]
    [DataRow(typeof(Charset))]
    [DataRow(typeof(ClientInfoMember))]
    [DataRow(typeof(CoatingSides))]
    [DataRow(typeof(CoatingType))]
    [DataRow(typeof(Compression))]
    [DataRow(typeof(CoveringName))]
    [DataRow(typeof(CoverMember))]
    [DataRow(typeof(CoverSheetInfoMember))]
    [DataRow(typeof(CoverType))]
    [DataRow(typeof(CurrentPageOrder))]
    [DataRow(typeof(DestinationAccessMember))]
    [DataRow(typeof(DestinationUrisMember))]
    [DataRow(typeof(DocumentAccessMember))]
    [DataRow(typeof(DocumentCreationAttribute))]
    [DataRow(typeof(DocumentDigitalSignature))]
    [DataRow(typeof(DocumentFormat))]
    [DataRow(typeof(DocumentFormatDetail))]
    [DataRow(typeof(DocumentStateReason))]
    [DataRow(typeof(FeedOrientation))]
    [DataRow(typeof(FetchDocumentAttribute))]
    [DataRow(typeof(FinisherSupplyClass))]
    [DataRow(typeof(FinisherSupplyType))]
    [DataRow(typeof(FinisherType))]
    [DataRow(typeof(FinishingReferenceEdge))]
    [DataRow(typeof(FinishingsColMember))]
    [DataRow(typeof(FinishingTemplate))]
    [DataRow(typeof(FoldingDirection))]
    [DataRow(typeof(IdentifyAction))]
    [DataRow(typeof(ImpositionTemplate))]
    [DataRow(typeof(InputAttributesMember))]
    [DataRow(typeof(InputColorMode))]
    [DataRow(typeof(InputContentType))]
    [DataRow(typeof(InputFilmScanMode))]
    [DataRow(typeof(InputSource))]
    [DataRow(typeof(InputTrayType))]
    [DataRow(typeof(InsertSheetMember))]
    [DataRow(typeof(IppFeature))]
    [DataRow(typeof(JobAccountingSheetsMember))]
    [DataRow(typeof(JobAccountingSheetsType))]
    [DataRow(typeof(JobAccountType))]
    [DataRow(typeof(JobCompleteBefore))]
    [DataRow(typeof(JobCreationAttribute))]
    [DataRow(typeof(JobErrorAction))]
    [DataRow(typeof(JobErrorSheetMember))]
    [DataRow(typeof(JobErrorSheetType))]
    [DataRow(typeof(JobErrorSheetWhen))]
    [DataRow(typeof(JobHistoryAttribute))]
    [DataRow(typeof(JobHoldUntil))]
    [DataRow(typeof(JobPasswordEncryption))]
    [DataRow(typeof(JobPhoneNumberScheme))]
    [DataRow(typeof(JobReleaseAction))]
    [DataRow(typeof(JobSheets))]
    [DataRow(typeof(JobSheetsColMember))]
    [DataRow(typeof(JobSheetsType))]
    [DataRow(typeof(JobSpooling))]
    [DataRow(typeof(JobStateReason))]
    [DataRow(typeof(JobStorageAccess))]
    [DataRow(typeof(JobStorageDisposition))]
    [DataRow(typeof(LaminatingType))]
    [DataRow(typeof(MarkerType))]
    [DataRow(typeof(MaterialAmountUnits))]
    [DataRow(typeof(MaterialColor))]
    [DataRow(typeof(MaterialKey))]
    [DataRow(typeof(MaterialPurpose))]
    [DataRow(typeof(MaterialRateUnits))]
    [DataRow(typeof(MaterialsColMember))]
    [DataRow(typeof(MaterialType))]
    [DataRow(typeof(Media))]
    [DataRow(typeof(MediaCoating))]
    [DataRow(typeof(MediaColMember))]
    [DataRow(typeof(MediaColor))]
    [DataRow(typeof(MediaGrain))]
    [DataRow(typeof(MediaInputTrayCheck))]
    [DataRow(typeof(MediaKey))]
    [DataRow(typeof(MediaPrePrinted))]
    [DataRow(typeof(MediaRecycled))]
    [DataRow(typeof(MediaSource))]
    [DataRow(typeof(MediaSourceFeedDirection))]
    [DataRow(typeof(MediaTooth))]
    [DataRow(typeof(MediaType))]
    [DataRow(typeof(MultipleDocumentHandling))]
    [DataRow(typeof(MultipleObjectHandling))]
    [DataRow(typeof(MultipleOperationTimeOutAction))]
    [DataRow(typeof(NaturalLanguage))]
    [DataRow(typeof(NotifyEvent))]
    [DataRow(typeof(NotifyPullMethod))]
    [DataRow(typeof(OutputAttributesMember))]
    [DataRow(typeof(OutputBin))]
    [DataRow(typeof(OutputDevice))]
    [DataRow(typeof(OutputTrayType))]
    [DataRow(typeof(OverrideSupported))]
    [DataRow(typeof(PageDelivery))]
    [DataRow(typeof(PageOrderReceived))]
    [DataRow(typeof(PdfFeature))]
    [DataRow(typeof(PdfVersion))]
    [DataRow(typeof(PdlOverride))]
    [DataRow(typeof(PlatformShape))]
    [DataRow(typeof(PowerState))]
    [DataRow(typeof(PresentationDirectionNumberUp))]
    [DataRow(typeof(PresentOnOff))]
    [DataRow(typeof(PrintBase))]
    [DataRow(typeof(PrintColorMode))]
    [DataRow(typeof(PrintContentOptimize))]
    [DataRow(typeof(PrinterCreationAttribute))]
    [DataRow(typeof(PrinterMandatoryJobAttribute))]
    [DataRow(typeof(PrinterMode))]
    [DataRow(typeof(PrinterRequestedJobAttribute))]
    [DataRow(typeof(PrinterServiceType))]
    [DataRow(typeof(PrinterStateReason))]
    [DataRow(typeof(PrinterSupplyType))]
    [DataRow(typeof(PrintObjectsMember))]
    [DataRow(typeof(PrintRenderingIntent))]
    [DataRow(typeof(PrintScaling))]
    [DataRow(typeof(PrintSupports))]
    [DataRow(typeof(PwgRasterDocumentSheetBack))]
    [DataRow(typeof(Repertoire))]
    [DataRow(typeof(ResourceFormat))]
    [DataRow(typeof(ResourceSettableAttribute))]
    [DataRow(typeof(ResourceStateReason))]
    [DataRow(typeof(ResourceType))]
    [DataRow(typeof(SaveDisposition))]
    [DataRow(typeof(SeparatorSheetsMember))]
    [DataRow(typeof(SeparatorSheetsType))]
    [DataRow(typeof(SheetCollate))]
    [DataRow(typeof(Sides))]
    [DataRow(typeof(StackingOrder))]
    [DataRow(typeof(StitchingMethod))]
    [DataRow(typeof(SystemMandatoryPrinterAttribute))]
    [DataRow(typeof(SystemMandatoryRegistrationAttribute))]
    [DataRow(typeof(SystemSettableAttribute))]
    [DataRow(typeof(SystemStateReason))]
    [DataRow(typeof(SystemTimeSourceConfigured))]
    [DataRow(typeof(TrimmingType))]
    [DataRow(typeof(TrimmingWhen))]
    [DataRow(typeof(UriAuthentication))]
    [DataRow(typeof(UriScheme))]
    [DataRow(typeof(UriSecurity))]
    [DataRow(typeof(WhichJobs))]
    [DataRow(typeof(WhichPrinters))]
    [DataRow(typeof(X509Type))]
    [DataRow(typeof(XImagePosition))]
    [DataRow(typeof(YImagePosition))]
    public void ImplicitOperator_NullString_ThrowsArgumentNullException(Type type)
    {
        // Arrange
        var conversionOperator = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => (m.Name == "op_Implicit" || m.Name == "op_Explicit")
                && m.ReturnType == type
                && m.GetParameters().FirstOrDefault()?.ParameterType == typeof(string));

        conversionOperator.Should().NotBeNull($"Type {type.Name} should have an implicit/explicit conversion operator from string");

        // Act
        var action = () => conversionOperator!.Invoke(null, new object?[] { null });

        // Assert
        action.Should().Throw<System.Reflection.TargetInvocationException>()
            .WithInnerException<ArgumentNullException>();
    }
}
