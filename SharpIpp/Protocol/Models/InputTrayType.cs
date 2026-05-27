namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the input tray type.
/// See: PWG 5100.13-2023 Section 6.6.9
/// </summary>
public readonly record struct InputTrayType(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly InputTrayType Other = new("other");
    public static readonly InputTrayType Unknown = new("unknown");
    public static readonly InputTrayType SheetFeedAutoSheetFeeder = new("sheetFeedAutoSheetFeeder");
    public static readonly InputTrayType SheetFeedAutoRemovableTray = new("sheetFeedAutoRemovableTray");
    public static readonly InputTrayType SheetFeedAutoNonRemovableTray = new("sheetFeedAutoNonRemovableTray");
    public static readonly InputTrayType MainInputTray = new("mainInputTray");
    public static readonly InputTrayType ManualFeedTray = new("manualFeedTray");
    public static readonly InputTrayType EnvelopeFeedTray = new("envelopeFeedTray");
    public static readonly InputTrayType LargeCapacityInputTray = new("largeCapacityInputTray");
    public static readonly InputTrayType Subunit = new("subunit");

    public override string ToString() => Value;
    public static implicit operator string(InputTrayType bin) => bin.Value;
    public static implicit operator InputTrayType(string value) => new(value);
}
