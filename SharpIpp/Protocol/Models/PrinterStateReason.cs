namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the reason for the current state of a Printer.
/// See: RFC 8011 Section 5.4.12
/// </summary>
public enum PrinterStateReason
{
    None,
    Other,
    ConnectingToDevice,
    DeviceDied,
    DeveloperEmpty,
    DeveloperLow,
    DoorOpen,
    FuserOverTemp,
    FuserUnderTemp,
    InputTrayMissing,
    InterlockOpen,
    InterpreterResourceUnavailable,
    MarkerSupplyEmpty,
    MarkerSupplyLow,
    MarkerWasteAlmostFull,
    MarkerWasteFull,
    MediaEmpty,
    MediaJam,
    MediaLow,
    MediaNeeded,
    MovingToPaused,
    OpcAlmostFull,
    OpcFull,
    OutputAreaAlmostFull,
    OutputAreaFull,
    OutputTrayMissing,
    Paused,
    Shutdown,
    SpoolAreaFull,
    StoppedPartially,
    Stopping,
    TimedOut,
    TonerEmpty,
    TonerLow
}
