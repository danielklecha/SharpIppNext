namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the output tray type.
/// See: PWG 5100.13-2023 Section 6.6.10
/// </summary>
public readonly record struct OutputTrayType(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly OutputTrayType Other = new("other");
    public static readonly OutputTrayType Unknown = new("unknown");
    public static readonly OutputTrayType RemovableBin = new("removableBin");
    public static readonly OutputTrayType UnRemovableBin = new("unRemovableBin");
    public static readonly OutputTrayType ContinuousCutter = new("continuousCutter");
    public static readonly OutputTrayType Stapler = new("stapler");
    public static readonly OutputTrayType EnvelopeTray = new("envelopeTray");
    public static readonly OutputTrayType Stacker = new("stacker");
    public static readonly OutputTrayType Mailbox = new("mailbox");
    public static readonly OutputTrayType Binder = new("binder");
    public static readonly OutputTrayType Sorter = new("sorter");
    public static readonly OutputTrayType Collator = new("collator");

    public override string ToString() => Value;
    public static implicit operator string(OutputTrayType bin) => bin.Value;
    public static implicit operator OutputTrayType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
