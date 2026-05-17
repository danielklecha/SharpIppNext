namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>resource-type</c> attribute.
/// See: PWG 5100.22-2025 Section 7.9.11
/// </summary>
public readonly record struct ResourceType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// A static font resource (e.g., a TrueType or OpenType font file).
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    public static readonly ResourceType StaticFont = new("static-font");

    /// <summary>
    /// A static ICC color profile resource.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    public static readonly ResourceType StaticIccProfile = new("static-icc-profile");

    /// <summary>
    /// A static image resource (e.g., a logo or background image).
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    public static readonly ResourceType StaticImage = new("static-image");

    /// <summary>
    /// A static strings resource (e.g., localized string tables).
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    public static readonly ResourceType StaticStrings = new("static-strings");

    /// <summary>
    /// A static template resource (e.g., a job or document template).
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    public static readonly ResourceType StaticTemplate = new("static-template");

    public override string ToString() => Value;
    public static implicit operator string(ResourceType value) => value.Value;
    public static explicit operator ResourceType(string value) => new(value);
}
