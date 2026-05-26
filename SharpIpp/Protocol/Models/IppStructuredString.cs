using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Abstract base class for dictionary-backed structured string models providing strongly typed property helpers.
/// </summary>
public abstract class IppStructuredString : IEnumerable<string>, IIppStructuredString
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    public int Count => Dictionary.Count;

    public void Add(string entry)
    {
        if (string.IsNullOrEmpty(entry))
            return;
        var eqIndex = entry.IndexOf('=');
        if (eqIndex > 0)
        {
            var key = entry.Substring(0, eqIndex);
            var val = entry.Substring(eqIndex + 1);
            Dictionary[key] = val;
        }
        else
        {
            Dictionary[entry] = string.Empty;
        }
    }

    public void Add(string key, string value)
    {
        Dictionary[key] = value;
    }

    public bool Remove(string key) => Dictionary.Remove(key);

    public bool TryGetValue(string key, out string value) => Dictionary.TryGetValue(key, out value!);

    public void Clear() => Dictionary.Clear();

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var kvp in Dictionary)
        {
            yield return $"{kvp.Key}={kvp.Value}";
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<KeyValuePair<string, string>> KeyValues => Dictionary;

    protected internal readonly Dictionary<string, string> Dictionary;

    protected IppStructuredString(IEqualityComparer<string>? comparer = null)
    {
        Dictionary = new Dictionary<string, string>(comparer ?? StringComparer.Ordinal);
    }

    protected IppStructuredString(IDictionary<string, string>? dictionary, IEqualityComparer<string>? comparer = null)
    {
        Dictionary = new Dictionary<string, string>(comparer ?? StringComparer.Ordinal);
        if (dictionary != null)
        {
            foreach (var kvp in dictionary)
            {
                Dictionary[kvp.Key] = kvp.Value;
            }
        }
    }

    protected string? Get(string key) => Dictionary.TryGetValue(key, out var val) ? val : null;

    protected void Set(string key, string? val)
    {
        if (val == null)
            Dictionary.Remove(key);
        else
            Dictionary[key] = val;
    }

    protected DateTimeOffset? GetDateTimeOffset(string key)
    {
        var str = Get(key);
        if (str == null)
            return null;
        if (DateTimeOffset.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dt))
            return dt;
        if (DateTimeOffset.TryParse(str, out var dtLocal))
            return dtLocal;
        return null;
    }

    protected void SetDateTimeOffset(string key, DateTimeOffset? val)
    {
        if (val == null)
            Dictionary.Remove(key);
        else
            Dictionary[key] = val.Value.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture);
    }

    protected Uri? GetUri(string key)
    {
        var str = Get(key);
        if (str == null)
            return null;
        if (Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out var uri))
            return uri;
        return null;
    }

    protected void SetUri(string key, Uri? val)
    {
        if (val == null)
            Dictionary.Remove(key);
        else
            Dictionary[key] = val.OriginalString;
    }

    protected int? GetInt(string key)
    {
        var str = Get(key);
        if (str == null)
            return null;
        if (int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out var val))
            return val;
        return null;
    }

    protected void SetInt(string key, int? val)
    {
        if (val == null)
            Dictionary.Remove(key);
        else
            Dictionary[key] = val.Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Validates the metadata values.
    /// </summary>
    public virtual void Validate()
    {
    }

    public abstract HashSet<string> StandardKeys { get; }

    public IDictionary<string, string>? Extensions
    {
        get
        {
            var ext = new Dictionary<string, string>(Dictionary.Comparer);
            foreach (var kvp in Dictionary)
            {
                if (!StandardKeys.Contains(kvp.Key))
                {
                    ext[kvp.Key] = kvp.Value;
                }
            }
            return ext.Count > 0 ? ext : null;
        }
        set
        {
            var keysToRemove = new List<string>();
            foreach (var kvp in Dictionary)
            {
                if (!StandardKeys.Contains(kvp.Key))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }
            foreach (var key in keysToRemove)
            {
                Dictionary.Remove(key);
            }

            if (value != null)
            {
                foreach (var kvp in value)
                {
                    Dictionary[kvp.Key] = kvp.Value;
                }
            }
        }
    }

    private static readonly System.Text.UTF8Encoding StrictUtf8 = new(false, true);

    public string this[string key]
    {
        get => Dictionary[key];
        set => Dictionary[key] = value;
    }

    public bool ContainsKey(string key) => Dictionary.ContainsKey(key);

    internal static bool IsValidUtf8String(byte[] bytes)
    {
        try
        {
            var str = StrictUtf8.GetString(bytes);
            foreach (var c in str)
            {
                if (c < 0x20 || c == 0x7F)
                    return false;
            }
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    internal static bool IsValidUtf8String(string str)
    {
        try
        {
            var bytes = StrictUtf8.GetBytes(str);
            return IsValidUtf8String(bytes);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
