// Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping;

/// <summary>
/// Property-based tests for smart enum round-trips.
/// For each enum type that received new values in Task 8, verifies that
/// encoding to an IppAttribute and decoding back preserves Value and IsKeyword (for IKeywordOrNameEnum).
///
/// Property 5: Smart Enum Round-Trip Preserves All Fields
/// Validates: Requirements 8.5
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class SmartEnumRoundTripTests : MapperTestBase
{
    private const int Iterations = 100;

    private static string RandomString(Random rng, int maxLen = 30)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz-0123456789";
        var len = rng.Next(1, maxLen + 1);
        return new string(Enumerable.Range(0, len).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
    }

    // Known values for each smart enum type to include in the test pool
    private static readonly string[] KnownJobStateReasonValues =
    {
        "none", "job-incoming", "job-data-insufficient", "document-access-error",
        "submission-interrupted", "job-outgoing", "job-hold-until-specified",
        "resources-are-not-ready", "resources-are-not-supported", "printer-stopped-partly",
        "printer-stopped", "job-interpreting", "job-queued", "job-transforming",
        "job-printing", "job-canceled-by-user", "job-canceled-by-operator",
        "job-canceled-at-device", "aborted-by-system", "unsupported-compression",
        "compression-error", "unsupported-document-format", "document-format-error",
        "processing-to-stop-point", "service-off-line", "job-completed-successfully",
        "job-completed-with-warnings", "job-completed-with-errors", "job-restartable",
        "queued-in-device", "digital-signature-did-not-verify", "errors-detected",
        "job-delay-output-until-specified", "job-spooling", "job-streaming",
        "warnings-detected", "conflicting-attributes", "job-canceled-after-timeout",
        "job-held-for-review", "account-authorization-failed", "account-closed",
        "account-info-needed", "account-limit-reached", "job-held-for-authorization",
        "job-held-for-button-press", "job-held-for-release", "job-password-wait",
        "job-printed-successfully", "job-printed-with-errors", "job-printed-with-warnings",
        "job-resuming", "job-release-wait", "job-stored", "job-stored-with-errors",
        "job-stored-with-warnings", "job-storing", "job-fetchable",
        "job-suspended-by-operator", "job-suspended-by-system", "job-suspended-by-user",
        "job-suspended-for-approval", "job-suspending", "job-suspended",
        "unsupported-attributes-or-values", "document-password-error",
        "document-permission-error", "document-security-error", "document-unprintable-error",
    };

    private static readonly string[] KnownPrinterStateReasonValues =
    {
        "none", "other", "connecting-to-device", "device-died", "developer-empty",
        "developer-low", "door-open", "fuser-over-temp", "fuser-under-temp",
        "input-tray-missing", "interlock-open", "identify-printer-requested",
        "interpreter-resource-unavailable", "marker-supply-empty", "marker-supply-low",
        "marker-waste-almost-full", "marker-waste-full", "media-empty", "media-jam",
        "media-low", "media-needed", "moving-to-paused", "opc-near-eol", "opc-life-over",
        "output-area-almost-full", "output-area-full", "output-tray-missing", "paused",
        "shutdown", "spool-area-full", "cover-open", "stopped-partly", "stopping",
        "timed-out", "toner-empty", "toner-low",
    };

    private static readonly string[] KnownNotifyEventValues =
    {
        "job-created", "job-completed", "job-state-changed", "job-config-changed",
        "job-progress", "job-stopped", "job-fetchable",
        "printer-config-changed", "printer-created", "printer-deleted",
        "printer-state-changed", "printer-stopped",
        "resource-canceled", "resource-config-changed", "resource-created",
        "resource-installed", "resource-state-changed",
        "system-config-changed", "system-restarted", "system-shutdown",
        "system-state-changed", "system-stopped",
        "document-completed", "document-config-changed", "document-created",
        "document-state-changed", "document-stopped", "document-fetchable",
    };

    // ── JobStateReason ────────────────────────────────────────────────────────

    [TestMethod]
    public void JobStateReason_KnownValues_RoundTrip_PreservesAllFields()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        foreach (var value in KnownJobStateReasonValues)
        {
            // JobStateReason(string Value)
            var original = new JobStateReason(value);

            // Encode to IppAttribute
            var attr = new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, _mapper.Map<string>(original));

            // Decode back
            var decoded = _mapper.Map<string, JobStateReason>((string)attr.Value!);

            decoded.Value.Should().Be(original.Value, $"JobStateReason '{value}': Value mismatch");
        }
    }

    [TestMethod]
    public void JobStateReason_UnrecognizedValues_RoundTrip_PreservesValue()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        var rng = new Random(200);

        for (var i = 0; i < Iterations; i++)
        {
            var value = "vendor-" + RandomString(rng);
            var original = new JobStateReason(value);

            var attr = new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, _mapper.Map<string>(original));
            var decoded = _mapper.Map<string, JobStateReason>((string)attr.Value!);

            decoded.Value.Should().Be(original.Value, $"iteration {i}: JobStateReason unrecognized value '{value}': Value mismatch");
        }
    }

    // ── PrinterStateReason ────────────────────────────────────────────────────

    [TestMethod]
    public void PrinterStateReason_KnownValues_RoundTrip_PreservesAllFields()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        foreach (var value in KnownPrinterStateReasonValues)
        {
            // PrinterStateReason(string Value)
            var original = new PrinterStateReason(value);

            var attr = new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterStateReasons, _mapper.Map<string>(original));
            var decoded = _mapper.Map<string, PrinterStateReason>((string)attr.Value!);

            decoded.Value.Should().Be(original.Value, $"PrinterStateReason '{value}': Value mismatch");
        }
    }

    [TestMethod]
    public void PrinterStateReason_UnrecognizedValues_RoundTrip_PreservesValue()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        var rng = new Random(201);

        for (var i = 0; i < Iterations; i++)
        {
            var value = "vendor-" + RandomString(rng);
            var original = new PrinterStateReason(value);

            var attr = new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterStateReasons, _mapper.Map<string>(original));
            var decoded = _mapper.Map<string, PrinterStateReason>((string)attr.Value!);

            decoded.Value.Should().Be(original.Value, $"iteration {i}: PrinterStateReason unrecognized value '{value}': Value mismatch");
        }
    }

    // ── NotifyEvent (IKeywordEnum) ────────────────────────────────────────────

    [TestMethod]
    public void NotifyEvent_KnownValues_WithKeywordTag_RoundTrip_PreservesAllFields()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        foreach (var value in KnownNotifyEventValues)
        {
            // NotifyEvent(string Value)
            var original = new NotifyEvent(value);

            // Encode: use keyword tag
            var attr = new IppAttribute(Tag.Keyword, "notify-events", _mapper.Map<string>(original));

            // Decode: map string → NotifyEvent
            var decoded = _mapper.Map<string, NotifyEvent>((string)attr.Value!);

            decoded.Value.Should().Be(original.Value, $"NotifyEvent '{value}': Value mismatch");
        }
    }

    [TestMethod]
    public void NotifyEvent_UnrecognizedValues_RoundTrip_PreservesValue()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        var rng = new Random(202);

        for (var i = 0; i < Iterations; i++)
        {
            var value = "vendor-" + RandomString(rng);
            var original = new NotifyEvent(value);
            var attr = new IppAttribute(Tag.Keyword, "notify-events", _mapper.Map<string>(original));
            var decoded = _mapper.Map<string, NotifyEvent>((string)attr.Value!);

            decoded.Value.Should().Be(original.Value, $"iteration {i}: NotifyEvent keyword '{value}': Value mismatch");
        }
    }

    // ── Mixed random string values for all three types ────────────────────────

    [TestMethod]
    public void JobStateReason_RandomStrings_RoundTrip_ValueAlwaysPreserved()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        var rng = new Random(203);
        var allValues = KnownJobStateReasonValues.Concat(
            Enumerable.Range(0, 50).Select(_ => "vendor-" + RandomString(rng))).ToArray();

        foreach (var value in allValues)
        {
            var original = new JobStateReason(value);
            var serialized = _mapper.Map<JobStateReason, string>(original);
            var decoded = _mapper.Map<string, JobStateReason>(serialized);

            decoded.Value.Should().Be(value, $"JobStateReason '{value}': Value must be preserved through round-trip");
        }
    }

    [TestMethod]
    public void PrinterStateReason_RandomStrings_RoundTrip_ValueAlwaysPreserved()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        var rng = new Random(204);
        var allValues = KnownPrinterStateReasonValues.Concat(
            Enumerable.Range(0, 50).Select(_ => "vendor-" + RandomString(rng))).ToArray();

        foreach (var value in allValues)
        {
            var original = new PrinterStateReason(value);
            var serialized = _mapper.Map<PrinterStateReason, string>(original);
            var decoded = _mapper.Map<string, PrinterStateReason>(serialized);

            decoded.Value.Should().Be(value, $"PrinterStateReason '{value}': Value must be preserved through round-trip");
        }
    }

    [TestMethod]
    public void NotifyEvent_RandomStrings_RoundTrip_ValueAlwaysPreserved()
    {
        // Feature: pwg5100-spec-parity, Property 5: Smart Enum Round-Trip Preserves All Fields
        var rng = new Random(205);
        var allValues = KnownNotifyEventValues.Concat(
            Enumerable.Range(0, 50).Select(_ => "vendor-" + RandomString(rng))).ToArray();

        foreach (var value in allValues)
        {
            var original = new NotifyEvent(value);
            var serialized = _mapper.Map<NotifyEvent, string>(original);
            var decoded = _mapper.Map<string, NotifyEvent>(serialized);

            decoded.Value.Should().Be(value, $"NotifyEvent '{value}': Value must be preserved through round-trip");
        }
    }
}
