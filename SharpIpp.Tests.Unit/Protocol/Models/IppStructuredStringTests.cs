using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppStructuredStringTests
{
    private class TestMetadata : IppStructuredString
    {
        public TestMetadata() : base()
        {
        }

        public TestMetadata(IEqualityComparer<string>? comparer) : base(comparer)
        {
        }

        public TestMetadata(IDictionary<string, string>? dictionary, IEqualityComparer<string>? comparer = null) 
            : base(dictionary, comparer)
        {
        }

        public override HashSet<string> StandardKeys => new(StringComparer.Ordinal);

        public string? GetValue(string key) => Get(key);
        public void SetValue(string key, string? val) => Set(key, val);
        public DateTimeOffset? GetDateTimeOffsetValue(string key) => GetDateTimeOffset(key);
        public void SetDateTimeOffsetValue(string key, DateTimeOffset? val) => SetDateTimeOffset(key, val);
        public Uri? GetUriValue(string key) => GetUri(key);
        public void SetUriValue(string key, Uri? val) => SetUri(key, val);
        public int? GetIntValue(string key) => GetInt(key);
        public void SetIntValue(string key, int? val) => SetInt(key, val);
    }

    [TestMethod]
    public void Constructor_WithNullComparer_ShouldDefaultToOrdinal()
    {
        // Line 20 comparer ?? StringComparer.Ordinal (when comparer is null)
        var metadata = new TestMetadata(comparer: null);
        metadata.SetValue("TestKey", "value");

        // Ordinal is case-sensitive, so "testkey" should return null
        metadata.GetValue("testkey").Should().BeNull();
        metadata.GetValue("TestKey").Should().Be("value");
    }

    [TestMethod]
    public void Constructor_WithCaseInsensitiveComparer_ShouldHonorComparer()
    {
        var metadata = new TestMetadata(StringComparer.OrdinalIgnoreCase);
        metadata.SetValue("TestKey", "value");

        metadata.GetValue("testkey").Should().Be("value");
    }

    [TestMethod]
    public void Constructor_WithDictionaryAndNullComparer_ShouldCopyAndDefaultToOrdinal()
    {
        // Line 23-33
        var source = new Dictionary<string, string>
        {
            { "TestKey", "value" }
        };

        var metadata = new TestMetadata(source, comparer: null);

        metadata.GetValue("TestKey").Should().Be("value");
        metadata.GetValue("testkey").Should().BeNull(); // ordinal case-sensitive
    }

    [TestMethod]
    public void Constructor_WithNullDictionary_ShouldInitializeEmpty()
    {
        // Line 23-33 null dictionary branch
        var metadata = new TestMetadata(dictionary: null, comparer: null);
        metadata.Dictionary.Count.Should().Be(0);
    }

    [TestMethod]
    public void GetDateTimeOffset_WhenRoundtripFailsButLocalSucceeds_ShouldReturnLocalTime()
    {
        // TryParse (roundtrip) fails, but TryParse (local) succeeds
        var originalCulture = System.Globalization.CultureInfo.CurrentCulture;
        try
        {
            var targetCulture = new System.Globalization.CultureInfo("pl-PL");
            System.Globalization.CultureInfo.CurrentCulture = targetCulture;
            var metadata = new TestMetadata();
            
            // This format ('dd.MM.yyyy HH:mm:ss K') is valid under pl-PL, but fails under InvariantCulture
            var expected = new DateTimeOffset(2026, 5, 21, 14, 0, 0, TimeSpan.FromHours(2));
            metadata.SetValue("date", expected.ToString(targetCulture));

            var result = metadata.GetDateTimeOffsetValue("date");
            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [TestMethod]
    public void GetDateTimeOffset_WhenBothParsersFail_ShouldReturnNull()
    {
        var metadata = new TestMetadata();
        metadata.SetValue("date", "not-a-datetime");
        var result = metadata.GetDateTimeOffsetValue("date");
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetUri_WhenUriIsInvalid_ShouldReturnNull()
    {
        // Line 72 coverage: Uri.TryCreate returns false, returns null
        var metadata = new TestMetadata();
        metadata.SetValue("uri", "http://[invalid]");

        var result = metadata.GetUriValue("uri");
        result.Should().BeNull();
    }

    [TestMethod]
    public void SetInt_WhenValueIsNull_ShouldRemoveKey()
    {
        // Line 96 coverage: val == null removes key
        var metadata = new TestMetadata();
        metadata.SetValue("key", "123");
        metadata.GetValue("key").Should().Be("123");

        metadata.SetIntValue("key", null);
        metadata.GetValue("key").Should().BeNull();
    }

    [TestMethod]
    public void Count_ShouldReturnNumberOfEntries()
    {
        var metadata = new TestMetadata();
        metadata.Count.Should().Be(0);
        metadata.SetValue("key1", "val1");
        metadata.Count.Should().Be(1);
        metadata.SetValue("key2", "val2");
        metadata.Count.Should().Be(2);
    }

    [TestMethod]
    public void KeyValues_ShouldExposeUnderlyingDictionary()
    {
        var metadata = new TestMetadata();
        metadata.SetValue("key1", "val1");
        metadata.KeyValues.Should().ContainSingle(kv => kv.Key == "key1" && kv.Value == "val1");
    }

    [TestMethod]
    public void AddEntry_WithValidEntry_ShouldAddKeyValuePair()
    {
        var metadata = new TestMetadata();
        metadata.Add("key=value");
        metadata.GetValue("key").Should().Be("value");
    }

    [TestMethod]
    public void AddEntry_WithoutEqualChar_ShouldAddWithEmptyValue()
    {
        var metadata = new TestMetadata();
        metadata.Add("key");
        metadata.GetValue("key").Should().Be(string.Empty);
    }

    [TestMethod]
    public void AddEntry_WithEmptyOrNullEntry_ShouldDoNothing()
    {
        var metadata = new TestMetadata();
        metadata.Add("");
        metadata.Count.Should().Be(0);

        metadata.Add(null!);
        metadata.Count.Should().Be(0);
    }

    [TestMethod]
    public void AddKeyValue_ShouldAddEntry()
    {
        var metadata = new TestMetadata();
        metadata.Add("key", "value");
        metadata.GetValue("key").Should().Be("value");
    }

    [TestMethod]
    public void Remove_ShouldRemoveEntryAndReturnTrueIfExisted()
    {
        var metadata = new TestMetadata();
        metadata.Add("key", "value");
        metadata.Remove("key").Should().BeTrue();
        metadata.GetValue("key").Should().BeNull();
        metadata.Remove("key").Should().BeFalse();
    }

    [TestMethod]
    public void TryGetValue_ShouldReturnTrueAndValueIfExisted()
    {
        var metadata = new TestMetadata();
        metadata.Add("key", "value");
        
        metadata.TryGetValue("key", out var val).Should().BeTrue();
        val.Should().Be("value");

        metadata.TryGetValue("nonexistent", out var val2).Should().BeFalse();
        val2.Should().BeNull();
    }

    [TestMethod]
    public void Clear_ShouldEmptyDictionary()
    {
        var metadata = new TestMetadata();
        metadata.Add("key1", "value1");
        metadata.Add("key2", "value2");
        metadata.Clear();
        metadata.Count.Should().Be(0);
    }

    [TestMethod]
    public void GetEnumerator_ShouldYieldStringRepresentations()
    {
        var metadata = new TestMetadata();
        metadata.Add("key1", "value1");
        metadata.Add("key2", "value2");

        var list = new List<string>();
        foreach (var entry in metadata)
        {
            list.Add(entry);
        }
        list.Should().Contain("key1=value1");
        list.Should().Contain("key2=value2");

        // Test non-generic IEnumerable GetEnumerator
        var enumerable = (System.Collections.IEnumerable)metadata;
        var enumerator = enumerable.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be("key1=value1");
    }

    [TestMethod]
    public void Indexer_ShouldGetAndSetValue()
    {
        var metadata = new TestMetadata();
        metadata["key"] = "value";
        metadata["key"].Should().Be("value");
        metadata.GetValue("key").Should().Be("value");
    }

    [TestMethod]
    public void ContainsKey_ShouldReturnTrueOrFalse()
    {
        var metadata = new TestMetadata();
        metadata.ContainsKey("key").Should().BeFalse();
        metadata.Add("key", "value");
        metadata.ContainsKey("key").Should().BeTrue();
    }

    [TestMethod]
    public void IsValidUtf8String_WithBytes_ShouldValidateCorrectly()
    {
        var validBytes = System.Text.Encoding.UTF8.GetBytes("hello world");
        IppStructuredString.IsValidUtf8String(validBytes).Should().BeTrue();

        var invalidBytes = new byte[] { 0xFF, 0xFE, 0xFD };
        IppStructuredString.IsValidUtf8String(invalidBytes).Should().BeFalse();

        var controlBytes = System.Text.Encoding.UTF8.GetBytes("hello\nworld");
        IppStructuredString.IsValidUtf8String(controlBytes).Should().BeFalse();
    }

    [TestMethod]
    public void IsValidUtf8String_WithString_ShouldValidateCorrectly()
    {
        IppStructuredString.IsValidUtf8String("hello world").Should().BeTrue();
        IppStructuredString.IsValidUtf8String("hello\nworld").Should().BeFalse();
        IppStructuredString.IsValidUtf8String("hello\u007Fworld").Should().BeFalse();
    }

    [TestMethod]
    public void Validate_ShouldNoOpByDefault()
    {
        var metadata = new TestMetadata();
        metadata.SetValue("key", "value");
        Action act = () => metadata.Validate();
        act.Should().NotThrow();
    }

    [TestMethod]
    public void IsValidUtf8String_WithStringThrowingException_ShouldReturnFalse()
    {
        // An isolated surrogate character is invalid UTF-16 and throws ArgumentException / EncoderFallbackException when converted to UTF-8
        var invalidString = "\uD800";
        IppStructuredString.IsValidUtf8String(invalidString).Should().BeFalse();
    }
}
