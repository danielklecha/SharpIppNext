namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the printer supply type.
/// See: PWG 5100.13-2023 Section 6.6.11
/// </summary>
public readonly record struct PrinterSupplyType(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly PrinterSupplyType Other = new("other");
    public static readonly PrinterSupplyType Unknown = new("unknown");
    public static readonly PrinterSupplyType Toner = new("toner");
    public static readonly PrinterSupplyType WasteToner = new("wasteToner");
    public static readonly PrinterSupplyType Ink = new("ink");
    public static readonly PrinterSupplyType InkCartridge = new("inkCartridge");
    public static readonly PrinterSupplyType InkRibbon = new("inkRibbon");
    public static readonly PrinterSupplyType WasteInk = new("wasteInk");
    public static readonly PrinterSupplyType Opc = new("opc");
    public static readonly PrinterSupplyType Developer = new("developer");
    public static readonly PrinterSupplyType FuserOil = new("fuserOil");
    public static readonly PrinterSupplyType SolidWax = new("solidWax");
    public static readonly PrinterSupplyType RibbonWax = new("ribbonWax");
    public static readonly PrinterSupplyType WasteWax = new("wasteWax");
    public static readonly PrinterSupplyType Fuser = new("fuser");
    public static readonly PrinterSupplyType FuserCleaningPad = new("fuserCleaningPad");
    public static readonly PrinterSupplyType TransferUnit = new("transferUnit");
    public static readonly PrinterSupplyType Staples = new("staples");
    public static readonly PrinterSupplyType Puncher = new("puncher");
    public static readonly PrinterSupplyType BookletMaker = new("bookletMaker");
    public static readonly PrinterSupplyType Folder = new("folder");
    public static readonly PrinterSupplyType Binder = new("binder");
    public static readonly PrinterSupplyType Stitcher = new("stitcher");
    public static readonly PrinterSupplyType Bander = new("bander");
    public static readonly PrinterSupplyType SaddleStitcher = new("saddleStitcher");

    public override string ToString() => Value;
    public static implicit operator string(PrinterSupplyType bin) => bin.Value;
    public static implicit operator PrinterSupplyType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
