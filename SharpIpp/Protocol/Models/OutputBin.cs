using System;
using System.Text.RegularExpressions;
using SharpIpp.Protocol.Extensions;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the output-bin attribute.
/// See: PWG 5100.2
/// </summary>
public readonly record struct OutputBin(string Value)
{
    public static readonly Regex KeywordRegex = new(
        "^(?:top|middle|bottom|side|left|right|center|rear|face-up|face-down|large-capacity|my-mailbox|auto|stacker-\\d+|mailbox-\\d+|tray-\\d+)$",
        RegexOptions.Compiled);

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
    public static readonly OutputBin MyMailbox = new("my-mailbox");
    public static readonly OutputBin Stacker1 = new("stacker-1");
    public static readonly OutputBin Mailbox1 = new("mailbox-1");
    public static readonly OutputBin Tray1 = new("tray-1");
    public static readonly OutputBin Auto = new("auto");

    public static OutputBin Stacker(int number) => new($"stacker-{ValidatePositive(number, nameof(number))}");
    public static OutputBin Mailbox(int number) => new($"mailbox-{ValidatePositive(number, nameof(number))}");
    public static OutputBin Tray(int number) => new($"tray-{ValidatePositive(number, nameof(number))}");

    public override string ToString() => Value;
    public static implicit operator string(OutputBin bin) => bin.Value;
    public static explicit operator OutputBin(string value) => new(value);

    private static int ValidatePositive(int value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(parameterName, "Value must be greater than 0.");

        return value;
    }
}
