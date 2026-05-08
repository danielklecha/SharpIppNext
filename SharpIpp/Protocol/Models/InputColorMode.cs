namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-color-mode</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.5
/// </summary>
public readonly record struct InputColorMode(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly InputColorMode Auto = new("auto");
    public static readonly InputColorMode BiLevel = new("bi-level");
    public static readonly InputColorMode Color = new("color");
    public static readonly InputColorMode Monochrome = new("monochrome");
    public static readonly InputColorMode ProcessBiLevel = new("process-bi-level");
    public static readonly InputColorMode ProcessMonochrome = new("process-monochrome");

    public override string ToString() => Value;
    public static implicit operator string(InputColorMode value) => value.Value;
    public static explicit operator InputColorMode(string value) => new(value);
}
