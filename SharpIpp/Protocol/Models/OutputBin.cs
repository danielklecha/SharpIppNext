namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the output-bin attribute.
/// See: PWG 5100.2
/// </summary>
public readonly record struct OutputBin(string Value)
{
    public static readonly OutputBin Top = new("top");
    public static readonly OutputBin Bottom = new("bottom");
    public static readonly OutputBin Middle = new("middle");
    public static readonly OutputBin Rear = new("rear");
    public static readonly OutputBin Side = new("side");
    public static readonly OutputBin Left = new("left");
    public static readonly OutputBin Right = new("right");
    public static readonly OutputBin Center = new("center");
    public static readonly OutputBin FaceUp = new("face-up");
    public static readonly OutputBin FaceDown = new("face-down");
    public static readonly OutputBin LargeCapacity = new("large-capacity");
    public static readonly OutputBin Auto = new("auto");
    public static readonly OutputBin Tray1 = new("tray-1");
    public static readonly OutputBin Tray2 = new("tray-2");
    public static readonly OutputBin Tray3 = new("tray-3");
    public static readonly OutputBin Tray4 = new("tray-4");
    public static readonly OutputBin Tray5 = new("tray-5");
    public static readonly OutputBin Tray6 = new("tray-6");
    public static readonly OutputBin Tray7 = new("tray-7");
    public static readonly OutputBin Tray8 = new("tray-8");
    public static readonly OutputBin Tray9 = new("tray-9");
    public static readonly OutputBin Tray10 = new("tray-10");

    public override string ToString() => Value;
    public static implicit operator string(OutputBin bin) => bin.Value;
    public static explicit operator OutputBin(string value) => new(value);
}
