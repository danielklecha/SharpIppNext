namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>resource-type</c> attribute.
/// See: PWG 5100.22-2025 Section 7.9.11
/// </summary>
public readonly record struct ResourceType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly ResourceType StaticFont = new("static-font");
    public static readonly ResourceType StaticIccProfile = new("static-icc-profile");
    public static readonly ResourceType StaticImage = new("static-image");
    public static readonly ResourceType StaticStrings = new("static-strings");
    public static readonly ResourceType StaticTemplate = new("static-template");

    public override string ToString() => Value;
    public static implicit operator string(ResourceType value) => value.Value;
    public static explicit operator ResourceType(string value) => new(value);
}
