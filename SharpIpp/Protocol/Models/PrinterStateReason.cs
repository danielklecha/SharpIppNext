namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the reason for the current state of a Printer.
/// See: RFC 8011 Section 5.4.12
/// </summary>
public readonly record struct PrinterStateReason(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly PrinterStateReason None = new("none");
    public static readonly PrinterStateReason Other = new("other");
    public static readonly PrinterStateReason ConnectingToDevice = new("connecting-to-device");
    public static readonly PrinterStateReason DeviceDied = new("device-died");
    public static readonly PrinterStateReason DeveloperEmpty = new("developer-empty");
    public static readonly PrinterStateReason DeveloperLow = new("developer-low");
    public static readonly PrinterStateReason DoorOpen = new("door-open");
    public static readonly PrinterStateReason FuserOverTemp = new("fuser-over-temp");
    public static readonly PrinterStateReason FuserUnderTemp = new("fuser-under-temp");
    public static readonly PrinterStateReason InputTrayMissing = new("input-tray-missing");
    public static readonly PrinterStateReason InterlockOpen = new("interlock-open");
    public static readonly PrinterStateReason InterpreterResourceUnavailable = new("interpreter-resource-unavailable");
    public static readonly PrinterStateReason MarkerSupplyEmpty = new("marker-supply-empty");
    public static readonly PrinterStateReason MarkerSupplyLow = new("marker-supply-low");
    public static readonly PrinterStateReason MarkerWasteAlmostFull = new("marker-waste-almost-full");
    public static readonly PrinterStateReason MarkerWasteFull = new("marker-waste-full");
    public static readonly PrinterStateReason MediaEmpty = new("media-empty");
    public static readonly PrinterStateReason MediaJam = new("media-jam");
    public static readonly PrinterStateReason MediaLow = new("media-low");
    public static readonly PrinterStateReason MediaNeeded = new("media-needed");
    public static readonly PrinterStateReason MovingToPaused = new("moving-to-paused");
    public static readonly PrinterStateReason OpcNearEol = new("opc-near-eol");
    public static readonly PrinterStateReason OpcLifeOver = new("opc-life-over");
    public static readonly PrinterStateReason OutputAreaAlmostFull = new("output-area-almost-full");
    public static readonly PrinterStateReason OutputAreaFull = new("output-area-full");
    public static readonly PrinterStateReason OutputTrayMissing = new("output-tray-missing");
    public static readonly PrinterStateReason Paused = new("paused");
    public static readonly PrinterStateReason Shutdown = new("shutdown");
    public static readonly PrinterStateReason SpoolAreaFull = new("spool-area-full");
    public static readonly PrinterStateReason CoverOpen = new("cover-open");
    public static readonly PrinterStateReason StoppedPartly = new("stopped-partly");
    public static readonly PrinterStateReason Stopping = new("stopping");
    public static readonly PrinterStateReason TimedOut = new("timed-out");
    public static readonly PrinterStateReason TonerEmpty = new("toner-empty");
    public static readonly PrinterStateReason TonerLow = new("toner-low");

    public override string ToString() => Value;
    public static implicit operator string(PrinterStateReason bin) => bin.Value;
    public static explicit operator PrinterStateReason(string value) => new(value);
}
