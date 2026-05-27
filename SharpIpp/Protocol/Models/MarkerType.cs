namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of marker.
/// See: PWG 5100.13-2023 Section 6.6.11
/// </summary>
public readonly record struct MarkerType(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly MarkerType Developer = new("developer");
    public static readonly MarkerType Fuser = new("fuser");
    public static readonly MarkerType FuserCleaningPad = new("fuser-cleaning-pad");
    public static readonly MarkerType FuserOil = new("fuser-oil");
    public static readonly MarkerType Ink = new("ink");
    public static readonly MarkerType Opc = new("opc");
    public static readonly MarkerType SolidWax = new("solid-wax");
    public static readonly MarkerType Staples = new("staples");
    public static readonly MarkerType Toner = new("toner");
    public static readonly MarkerType TransferUnit = new("transfer-unit");
    public static readonly MarkerType WasteInk = new("waste-ink");
    public static readonly MarkerType WasteToner = new("waste-toner");
    public static readonly MarkerType WasteWax = new("waste-wax");

    public override string ToString() => Value;
    public static implicit operator string(MarkerType bin) => bin.Value;
    public static implicit operator MarkerType(string value) => new(value);
}
