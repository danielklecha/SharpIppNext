namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the reason for the current state of a Printer.
/// See: RFC 8011 Section 5.4.12
/// See: PWG 5100.9-2009 Sections 5.1.3, 5.1.4, 6.1.1, 8.1
/// See: PWG 5100.13-2023 Section 9.3
/// See: PWG 5100.18-2025 Section 9.5
/// See: PWG 5100.22-2025 Section 9.3
/// </summary>
public readonly record struct PrinterStateReason(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// No printer state reasons apply.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason None = new("none");

    /// <summary>
    /// The Printer state reason is not one of the predefined values.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason Other = new("other");

    /// <summary>
    /// The Printer is in the process of connecting to a shared network output device.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason ConnectingToDevice = new("connecting-to-device");

    /// <summary>
    /// The Printer's output device is not functioning.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason DeviceDied = new("device-died");

    /// <summary>
    /// The Printer's developer is empty.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason DeveloperEmpty = new("developer-empty");

    /// <summary>
    /// The Printer's developer is low.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason DeveloperLow = new("developer-low");

    /// <summary>
    /// One or more covers on the Printer are open.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason DoorOpen = new("door-open");

    /// <summary>
    /// The Printer's fuser is over temperature.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason FuserOverTemp = new("fuser-over-temp");

    /// <summary>
    /// The Printer's fuser is under temperature.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason FuserUnderTemp = new("fuser-under-temp");

    /// <summary>
    /// At least one input tray is missing from the Printer.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason InputTrayMissing = new("input-tray-missing");

    /// <summary>
    /// One or more interlocks are open.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason InterlockOpen = new("interlock-open");

    /// <summary>
    /// A pending Identify-Printer request exists on the Infrastructure Printer.
    /// See: RFC 8011 Section 5.4.12
    /// See: PWG 5100.18-2025 Section 9.5
    /// </summary>
    public static readonly PrinterStateReason IdentifyPrinterRequested = new("identify-printer-requested");

    /// <summary>
    /// An interpreter resource (e.g., a font) is unavailable.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason InterpreterResourceUnavailable = new("interpreter-resource-unavailable");

    /// <summary>
    /// At least one marker supply is empty.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MarkerSupplyEmpty = new("marker-supply-empty");

    /// <summary>
    /// At least one marker supply is low.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MarkerSupplyLow = new("marker-supply-low");

    /// <summary>
    /// The Printer's marker waste receptacle is almost full.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MarkerWasteAlmostFull = new("marker-waste-almost-full");

    /// <summary>
    /// The Printer's marker waste receptacle is full.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MarkerWasteFull = new("marker-waste-full");

    /// <summary>
    /// At least one input tray is empty.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MediaEmpty = new("media-empty");

    /// <summary>
    /// The Printer has a media jam.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MediaJam = new("media-jam");

    /// <summary>
    /// At least one input tray is low on media.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MediaLow = new("media-low");

    /// <summary>
    /// A tray has run out of media and the Printer is waiting for media to be loaded.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MediaNeeded = new("media-needed");

    /// <summary>
    /// The Printer is in the process of moving to the 'stopped' state.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason MovingToPaused = new("moving-to-paused");

    /// <summary>
    /// The Printer's optical photo conductor is near end of life.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason OpcNearEol = new("opc-near-eol");

    /// <summary>
    /// The Printer's optical photo conductor life is over.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason OpcLifeOver = new("opc-life-over");

    /// <summary>
    /// The Printer's output area is almost full.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason OutputAreaAlmostFull = new("output-area-almost-full");

    /// <summary>
    /// The Printer's output area is full.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason OutputAreaFull = new("output-area-full");

    /// <summary>
    /// At least one output tray is missing from the Printer.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason OutputTrayMissing = new("output-tray-missing");

    /// <summary>
    /// Someone has paused the Printer and the Printer's "printer-state" is 'stopped'.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason Paused = new("paused");

    /// <summary>
    /// The Printer has been shutdown.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason Shutdown = new("shutdown");

    /// <summary>
    /// The Printer's spool area is full and no more jobs can be accepted.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason SpoolAreaFull = new("spool-area-full");

    /// <summary>
    /// One or more covers on the Printer are open.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason CoverOpen = new("cover-open");

    /// <summary>
    /// The Printer has stopped partly.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StoppedPartly = new("stopped-partly");

    /// <summary>
    /// The Printer is in the process of stopping.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason Stopping = new("stopping");

    /// <summary>
    /// The Printer did not respond and may be off-line.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason TimedOut = new("timed-out");

    /// <summary>
    /// The Printer's toner is empty.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason TonerEmpty = new("toner-empty");

    /// <summary>
    /// The Printer's toner is low.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason TonerLow = new("toner-low");

    /// <summary>
    /// A storage device has been added to the Printer.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageAdded = new("storage-added");

    /// <summary>
    /// A storage device on the Printer is almost full.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageAlmostFull = new("storage-almost-full");

    /// <summary>
    /// A storage device cover has been closed.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageCoverClosed = new("storage-cover-closed");

    /// <summary>
    /// A storage device cover is open.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageCoverOpen = new("storage-cover-open");

    /// <summary>
    /// A storage device configuration has changed.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageConfigurationChange = new("storage-configuration-change");

    /// <summary>
    /// A storage device on the Printer is full.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageFull = new("storage-full");

    /// <summary>
    /// A storage device interlock has been closed.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageInterlockClosed = new("storage-interlock-closed");

    /// <summary>
    /// A storage device interlock is open.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageInterlockOpen = new("storage-interlock-open");

    /// <summary>
    /// A storage device's life is almost over.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageLifeAlmostOver = new("storage-life-almost-over");

    /// <summary>
    /// A storage device's life is over.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageLifeOver = new("storage-life-over");

    /// <summary>
    /// A storage device is missing from the Printer.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageMissing = new("storage-missing");

    /// <summary>
    /// A storage device is offline.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageOffline = new("storage-offline");

    /// <summary>
    /// A storage device is over temperature.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageOverTemperature = new("storage-over-temperature");

    /// <summary>
    /// A storage device is in power-saver mode.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StoragePowerSaver = new("storage-power-saver");

    /// <summary>
    /// A storage device has a recoverable failure.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageRecoverableFailure = new("storage-recoverable-failure");

    /// <summary>
    /// A storage device has been removed from the Printer.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageRemoved = new("storage-removed");

    /// <summary>
    /// A storage device thermistor has failed.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageThermistorFailure = new("storage-thermistor-failure");

    /// <summary>
    /// A storage device has been turned off.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageTurnedOff = new("storage-turned-off");

    /// <summary>
    /// A storage device has been turned on.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageTurnedOn = new("storage-turned-on");

    /// <summary>
    /// A storage device is under temperature.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageUnderTemperature = new("storage-under-temperature");

    /// <summary>
    /// A storage device has an unrecoverable failure.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageUnrecoverableFailure = new("storage-unrecoverable-failure");

    /// <summary>
    /// A storage device is warming up.
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public static readonly PrinterStateReason StorageWarmingUp = new("storage-warming-up");

    // PWG 5100.9-2009 Section 8.1 registered printer-state-reasons keywords.
    /// <summary>
    /// Alert Removal Of Binary Change Entry printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason AlertRemovalOfBinaryChangeEntry = new("alert-removal-of-binary-change-entry");
    /// <summary>
    /// Bander Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderAdded = new("bander-added");
    /// <summary>
    /// Bander Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderAlmostEmpty = new("bander-almost-empty");
    /// <summary>
    /// Bander Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderAlmostFull = new("bander-almost-full");
    /// <summary>
    /// Bander At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderAtLimit = new("bander-at-limit");
    /// <summary>
    /// Bander Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderClosed = new("bander-closed");
    /// <summary>
    /// Bander Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderConfigurationChange = new("bander-configuration-change");
    /// <summary>
    /// Bander Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderCoverClosed = new("bander-cover-closed");
    /// <summary>
    /// Bander Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderCoverOpen = new("bander-cover-open");
    /// <summary>
    /// Bander Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderEmpty = new("bander-empty");
    /// <summary>
    /// Bander Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderFull = new("bander-full");
    /// <summary>
    /// Bander Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderInterlockClosed = new("bander-interlock-closed");
    /// <summary>
    /// Bander Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderInterlockOpen = new("bander-interlock-open");
    /// <summary>
    /// Bander Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderLifeAlmostOver = new("bander-life-almost-over");
    /// <summary>
    /// Bander Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderLifeOver = new("bander-life-over");
    /// <summary>
    /// Bander Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderMemoryExhausted = new("bander-memory-exhausted");
    /// <summary>
    /// Bander Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderMissing = new("bander-missing");
    /// <summary>
    /// Bander Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderMotorFailure = new("bander-motor-failure");
    /// <summary>
    /// Bander Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderNearLimit = new("bander-near-limit");
    /// <summary>
    /// Bander Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderOffline = new("bander-offline");
    /// <summary>
    /// Bander Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderOpened = new("bander-opened");
    /// <summary>
    /// Bander Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderOverTemperature = new("bander-over-temperature");
    /// <summary>
    /// Bander Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderPowerSaver = new("bander-power-saver");
    /// <summary>
    /// Bander Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderRecoverableFailure = new("bander-recoverable-failure");
    /// <summary>
    /// Bander Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderRecoverableStorageError = new("bander-recoverable-storage-error");
    /// <summary>
    /// Bander Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderRemoved = new("bander-removed");
    /// <summary>
    /// Bander Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderResourceAdded = new("bander-resource-added");
    /// <summary>
    /// Bander Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderResourceRemoved = new("bander-resource-removed");
    /// <summary>
    /// Bander Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderThermistorFailure = new("bander-thermistor-failure");
    /// <summary>
    /// Bander Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderTimingFailure = new("bander-timing-failure");
    /// <summary>
    /// Bander Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderTurnedOff = new("bander-turned-off");
    /// <summary>
    /// Bander Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderTurnedOn = new("bander-turned-on");
    /// <summary>
    /// Bander Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderUnderTemperature = new("bander-under-temperature");
    /// <summary>
    /// Bander Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderUnrecoverableFailure = new("bander-unrecoverable-failure");
    /// <summary>
    /// Bander Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderUnrecoverableStorageError = new("bander-unrecoverable-storage-error");
    /// <summary>
    /// Bander Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BanderWarmingUp = new("bander-warming-up");
    /// <summary>
    /// Binder Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderAdded = new("binder-added");
    /// <summary>
    /// Binder Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderAlmostEmpty = new("binder-almost-empty");
    /// <summary>
    /// Binder Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderAlmostFull = new("binder-almost-full");
    /// <summary>
    /// Binder At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderAtLimit = new("binder-at-limit");
    /// <summary>
    /// Binder Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderClosed = new("binder-closed");
    /// <summary>
    /// Binder Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderConfigurationChange = new("binder-configuration-change");
    /// <summary>
    /// Binder Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderCoverClosed = new("binder-cover-closed");
    /// <summary>
    /// Binder Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderCoverOpen = new("binder-cover-open");
    /// <summary>
    /// Binder Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderEmpty = new("binder-empty");
    /// <summary>
    /// Binder Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderFull = new("binder-full");
    /// <summary>
    /// Binder Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderInterlockClosed = new("binder-interlock-closed");
    /// <summary>
    /// Binder Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderInterlockOpen = new("binder-interlock-open");
    /// <summary>
    /// Binder Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderJam = new("binder-jam");
    /// <summary>
    /// Binder Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderLifeAlmostOver = new("binder-life-almost-over");
    /// <summary>
    /// Binder Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderLifeOver = new("binder-life-over");
    /// <summary>
    /// Binder Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderMemoryExhausted = new("binder-memory-exhausted");
    /// <summary>
    /// Binder Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderMissing = new("binder-missing");
    /// <summary>
    /// Binder Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderMotorFailure = new("binder-motor-failure");
    /// <summary>
    /// Binder Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderNearLimit = new("binder-near-limit");
    /// <summary>
    /// Binder Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderOffline = new("binder-offline");
    /// <summary>
    /// Binder Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderOpened = new("binder-opened");
    /// <summary>
    /// Binder Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderOverTemperature = new("binder-over-temperature");
    /// <summary>
    /// Binder Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderRecoverableFailure = new("binder-recoverable-failure");
    /// <summary>
    /// Binder Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderRecoverableStorageError = new("binder-recoverable-storage-error");
    /// <summary>
    /// Binder Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderRemoved = new("binder-removed");
    /// <summary>
    /// Binder Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderResourceAdded = new("binder-resource-added");
    /// <summary>
    /// Binder Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderResourceRemoved = new("binder-resource-removed");
    /// <summary>
    /// Binder Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderThermistorFailure = new("binder-thermistor-failure");
    /// <summary>
    /// Binder Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderTimingFailure = new("binder-timing-failure");
    /// <summary>
    /// Binder Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderTurnedOff = new("binder-turned-off");
    /// <summary>
    /// Binder Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderTurnedOn = new("binder-turned-on");
    /// <summary>
    /// Binder Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderUnderTemperature = new("binder-under-temperature");
    /// <summary>
    /// Binder Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderUnrecoverableFailure = new("binder-unrecoverable-failure");
    /// <summary>
    /// Binder Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderUnrecoverableStorageError = new("binder-unrecoverable-storage-error");
    /// <summary>
    /// Binder Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason BinderWarmingUp = new("binder-warming-up");
    /// <summary>
    /// Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ConfigurationChange = new("configuration-change");
    /// <summary>
    /// Die Cutter Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterAdded = new("die-cutter-added");
    /// <summary>
    /// Die Cutter Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterAlmostEmpty = new("die-cutter-almost-empty");
    /// <summary>
    /// Die Cutter Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterAlmostFull = new("die-cutter-almost-full");
    /// <summary>
    /// Die Cutter At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterAtLimit = new("die-cutter-at-limit");
    /// <summary>
    /// Die Cutter Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterClosed = new("die-cutter-closed");
    /// <summary>
    /// Die Cutter Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterConfigurationChange = new("die-cutter-configuration-change");
    /// <summary>
    /// Die Cutter Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterCoverClosed = new("die-cutter-cover-closed");
    /// <summary>
    /// Die Cutter Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterCoverOpen = new("die-cutter-cover-open");
    /// <summary>
    /// Die Cutter Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterEmpty = new("die-cutter-empty");
    /// <summary>
    /// Die Cutter Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterFull = new("die-cutter-full");
    /// <summary>
    /// Die Cutter Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterInterlockClosed = new("die-cutter-interlock-closed");
    /// <summary>
    /// Die Cutter Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterInterlockOpen = new("die-cutter-interlock-open");
    /// <summary>
    /// Die Cutter Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterJam = new("die-cutter-jam");
    /// <summary>
    /// Die Cutter Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterLifeAlmostOver = new("die-cutter-life-almost-over");
    /// <summary>
    /// Die Cutter Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterLifeOver = new("die-cutter-life-over");
    /// <summary>
    /// Die Cutter Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterMemoryExhausted = new("die-cutter-memory-exhausted");
    /// <summary>
    /// Die Cutter Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterMissing = new("die-cutter-missing");
    /// <summary>
    /// Die Cutter Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterMotorFailure = new("die-cutter-motor-failure");
    /// <summary>
    /// Die Cutter Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterNearLimit = new("die-cutter-near-limit");
    /// <summary>
    /// Die Cutter Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterOffline = new("die-cutter-offline");
    /// <summary>
    /// Die Cutter Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterOpened = new("die-cutter-opened");
    /// <summary>
    /// Die Cutter Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterOverTemperature = new("die-cutter-over-temperature");
    /// <summary>
    /// Die Cutter Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterPowerSaver = new("die-cutter-power-saver");
    /// <summary>
    /// Die Cutter Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterRecoverableFailure = new("die-cutter-recoverable-failure");
    /// <summary>
    /// Die Cutter Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterRecoverableStorageError = new("die-cutter-recoverable-storage-error");
    /// <summary>
    /// Die Cutter Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterRemoved = new("die-cutter-removed");
    /// <summary>
    /// Die Cutter Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterResourceAdded = new("die-cutter-resource-added");
    /// <summary>
    /// Die Cutter Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterResourceRemoved = new("die-cutter-resource-removed");
    /// <summary>
    /// Die Cutter Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterTimingFailure = new("die-cutter-timing-failure");
    /// <summary>
    /// Die Cutter Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterTurnedOff = new("die-cutter-turned-off");
    /// <summary>
    /// Die Cutter Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterTurnedOn = new("die-cutter-turned-on");
    /// <summary>
    /// Die Cutter Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterUnderTemperature = new("die-cutter-under-temperature");
    /// <summary>
    /// Die Cutter Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterUnrecoverableFailure = new("die-cutter-unrecoverable-failure");
    /// <summary>
    /// Die Cutter Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterUnrecoverableStorageError = new("die-cutter-unrecoverable-storage-error");
    /// <summary>
    /// Die Cutter Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason DieCutterWarmingUp = new("die-cutter-warming-up");
    /// <summary>
    /// Folder Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderAdded = new("folder-added");
    /// <summary>
    /// Folder Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderAlmostEmpty = new("folder-almost-empty");
    /// <summary>
    /// Folder Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderAlmostFull = new("folder-almost-full");
    /// <summary>
    /// Folder At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderAtLimit = new("folder-at-limit");
    /// <summary>
    /// Folder Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderClosed = new("folder-closed");
    /// <summary>
    /// Folder Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderConfigurationChange = new("folder-configuration-change");
    /// <summary>
    /// Folder Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderCoverClosed = new("folder-cover-closed");
    /// <summary>
    /// Folder Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderCoverOpen = new("folder-cover-open");
    /// <summary>
    /// Folder Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderEmpty = new("folder-empty");
    /// <summary>
    /// Folder Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderFull = new("folder-full");
    /// <summary>
    /// Folder Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderInterlockClosed = new("folder-interlock-closed");
    /// <summary>
    /// Folder Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderInterlockOpen = new("folder-interlock-open");
    /// <summary>
    /// Folder Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderJam = new("folder-jam");
    /// <summary>
    /// Folder Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderLifeOver = new("folder-life-over");
    /// <summary>
    /// Folder Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderMemoryExhausted = new("folder-memory-exhausted");
    /// <summary>
    /// Folder Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderMissing = new("folder-missing");
    /// <summary>
    /// Folder Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderMotorFailure = new("folder-motor-failure");
    /// <summary>
    /// Folder Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderNearLimit = new("folder-near-limit");
    /// <summary>
    /// Folder Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderOffline = new("folder-offline");
    /// <summary>
    /// Folder Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderOverTemperature = new("folder-over-temperature");
    /// <summary>
    /// Folder Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderPowerSaver = new("folder-power-saver");
    /// <summary>
    /// Folder Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderRecoverableStorageError = new("folder-recoverable-storage-error");
    /// <summary>
    /// Folder Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderRemoved = new("folder-removed");
    /// <summary>
    /// Folder Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderResourceAdded = new("folder-resource-added");
    /// <summary>
    /// Folder Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderResourceRemoved = new("folder-resource-removed");
    /// <summary>
    /// Folder Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderThermistorFailure = new("folder-thermistor-failure");
    /// <summary>
    /// Folder Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderTimingFailure = new("folder-timing-failure");
    /// <summary>
    /// Folder Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderTurnedOff = new("folder-turned-off");
    /// <summary>
    /// Folder Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderTurnedOn = new("folder-turned-on");
    /// <summary>
    /// Folder Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderUnderTemperature = new("folder-under-temperature");
    /// <summary>
    /// Folder Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderUnrecoverableFailure = new("folder-unrecoverable-failure");
    /// <summary>
    /// Folder Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderUnrecoverableStorageError = new("folder-unrecoverable-storage-error");
    /// <summary>
    /// Folder Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason FolderWarmingUp = new("folder-warming-up");
    /// <summary>
    /// Imprinter Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterAdded = new("imprinter-added");
    /// <summary>
    /// Imprinter Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterAlmostEmpty = new("imprinter-almost-empty");
    /// <summary>
    /// Imprinter Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterAlmostFull = new("imprinter-almost-full");
    /// <summary>
    /// Imprinter At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterAtLimit = new("imprinter-at-limit");
    /// <summary>
    /// Imprinter Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterClosed = new("imprinter-closed");
    /// <summary>
    /// Imprinter Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterConfigurationChange = new("imprinter-configuration-change");
    /// <summary>
    /// Imprinter Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterCoverClosed = new("imprinter-cover-closed");
    /// <summary>
    /// Imprinter Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterCoverOpen = new("imprinter-cover-open");
    /// <summary>
    /// Imprinter Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterEmpty = new("imprinter-empty");
    /// <summary>
    /// Imprinter Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterFull = new("imprinter-full");
    /// <summary>
    /// Imprinter Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterInterlockClosed = new("imprinter-interlock-closed");
    /// <summary>
    /// Imprinter Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterInterlockOpen = new("imprinter-interlock-open");
    /// <summary>
    /// Imprinter Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterJam = new("imprinter-jam");
    /// <summary>
    /// Imprinter Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterLifeAlmostOver = new("imprinter-life-almost-over");
    /// <summary>
    /// Imprinter Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterLifeOver = new("imprinter-life-over");
    /// <summary>
    /// Imprinter Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterMissing = new("imprinter-missing");
    /// <summary>
    /// Imprinter Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterMotorFailure = new("imprinter-motor-failure");
    /// <summary>
    /// Imprinter Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterNearLimit = new("imprinter-near-limit");
    /// <summary>
    /// Imprinter Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterOffline = new("imprinter-offline");
    /// <summary>
    /// Imprinter Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterOpened = new("imprinter-opened");
    /// <summary>
    /// Imprinter Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterOverTemperature = new("imprinter-over-temperature");
    /// <summary>
    /// Imprinter Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterPowerSaver = new("imprinter-power-saver");
    /// <summary>
    /// Imprinter Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterRecoverableFailure = new("imprinter-recoverable-failure");
    /// <summary>
    /// Imprinter Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterRecoverableStorageError = new("imprinter-recoverable-storage-error");
    /// <summary>
    /// Imprinter Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterRemoved = new("imprinter-removed");
    /// <summary>
    /// Imprinter Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterResourceAdded = new("imprinter-resource-added");
    /// <summary>
    /// Imprinter Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterResourceRemoved = new("imprinter-resource-removed");
    /// <summary>
    /// Imprinter Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterThermistorFailure = new("imprinter-thermistor-failure");
    /// <summary>
    /// Imprinter Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterTimingFailure = new("imprinter-timing-failure");
    /// <summary>
    /// Imprinter Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterTurnedOff = new("imprinter-turned-off");
    /// <summary>
    /// Imprinter Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterTurnedOn = new("imprinter-turned-on");
    /// <summary>
    /// Imprinter Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterUnrecoverableFailure = new("imprinter-unrecoverable-failure");
    /// <summary>
    /// Imprinter Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterUnrecoverableStorageError = new("imprinter-unrecoverable-storage-error");
    /// <summary>
    /// Imprinter Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason ImprinterWarmingUp = new("imprinter-warming-up");
    /// <summary>
    /// Input Cannot Feed Size Selected printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputCannotFeedSizeSelected = new("input-cannot-feed-size-selected");
    /// <summary>
    /// Input Manual Input Request printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputManualInputRequest = new("input-manual-input-request");
    /// <summary>
    /// Input Media Color Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputMediaColorChange = new("input-media-color-change");
    /// <summary>
    /// Input Media Form Parts Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputMediaFormPartsChange = new("input-media-form-parts-change");
    /// <summary>
    /// Input Media Size Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputMediaSizeChange = new("input-media-size-change");
    /// <summary>
    /// Input Media Type Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputMediaTypeChange = new("input-media-type-change");
    /// <summary>
    /// Input Media Weight Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputMediaWeightChange = new("input-media-weight-change");
    /// <summary>
    /// Input Tray Elevation Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputTrayElevationFailure = new("input-tray-elevation-failure");
    /// <summary>
    /// Input Tray Position Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InputTrayPositionFailure = new("input-tray-position-failure");
    /// <summary>
    /// Inserter Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterAdded = new("inserter-added");
    /// <summary>
    /// Inserter Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterAlmostEmpty = new("inserter-almost-empty");
    /// <summary>
    /// Inserter Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterAlmostFull = new("inserter-almost-full");
    /// <summary>
    /// Inserter At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterAtLimit = new("inserter-at-limit");
    /// <summary>
    /// Inserter Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterClosed = new("inserter-closed");
    /// <summary>
    /// Inserter Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterCoverClosed = new("inserter-cover-closed");
    /// <summary>
    /// Inserter Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterCoverOpen = new("inserter-cover-open");
    /// <summary>
    /// Inserter Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterEmpty = new("inserter-empty");
    /// <summary>
    /// Inserter Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterFull = new("inserter-full");
    /// <summary>
    /// Inserter Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterInterlockClosed = new("inserter-interlock-closed");
    /// <summary>
    /// Inserter Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterInterlockOpen = new("inserter-interlock-open");
    /// <summary>
    /// Inserter Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterJam = new("inserter-jam");
    /// <summary>
    /// Inserter Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterLifeAlmostOver = new("inserter-life-almost-over");
    /// <summary>
    /// Inserter Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterLifeOver = new("inserter-life-over");
    /// <summary>
    /// Inserter Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterMemoryExhausted = new("inserter-memory-exhausted");
    /// <summary>
    /// Inserter Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterMissing = new("inserter-missing");
    /// <summary>
    /// Inserter Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterMotorFailure = new("inserter-motor-failure");
    /// <summary>
    /// Inserter Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterNearLimit = new("inserter-near-limit");
    /// <summary>
    /// Inserter Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterOffline = new("inserter-offline");
    /// <summary>
    /// Inserter Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterOpened = new("inserter-opened");
    /// <summary>
    /// Inserter Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterOverTemperature = new("inserter-over-temperature");
    /// <summary>
    /// Inserter Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterPowerSaver = new("inserter-power-saver");
    /// <summary>
    /// Inserter Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterRecoverableFailure = new("inserter-recoverable-failure");
    /// <summary>
    /// Inserter Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterRecoverableStorageError = new("inserter-recoverable-storage-error");
    /// <summary>
    /// Inserter Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterRemoved = new("inserter-removed");
    /// <summary>
    /// Inserter Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterResourceAdded = new("inserter-resource-added");
    /// <summary>
    /// Inserter Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterResourceRemoved = new("inserter-resource-removed");
    /// <summary>
    /// Inserter Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterThermistorFailure = new("inserter-thermistor-failure");
    /// <summary>
    /// Inserter Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterTimingFailure = new("inserter-timing-failure");
    /// <summary>
    /// Inserter Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterTurnedOff = new("inserter-turned-off");
    /// <summary>
    /// Inserter Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterTurnedOn = new("inserter-turned-on");
    /// <summary>
    /// Inserter Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterUnderTemperature = new("inserter-under-temperature");
    /// <summary>
    /// Inserter Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterUnrecoverableFailure = new("inserter-unrecoverable-failure");
    /// <summary>
    /// Inserter Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InserterWarmingUp = new("inserter-warming-up");
    /// <summary>
    /// Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterlockClosed = new("interlock-closed");
    /// <summary>
    /// Interpreter Cartridge Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterCartridgeAdded = new("interpreter-cartridge-added");
    /// <summary>
    /// Interpreter Cartridge Deleted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterCartridgeDeleted = new("interpreter-cartridge-deleted");
    /// <summary>
    /// Interpreter Complex Page Encountered printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterComplexPageEncountered = new("interpreter-complex-page-encountered");
    /// <summary>
    /// Interpreter Memory Decrease printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterMemoryDecrease = new("interpreter-memory-decrease");
    /// <summary>
    /// Interpreter Memory Increase printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterMemoryIncrease = new("interpreter-memory-increase");
    /// <summary>
    /// Interpreter Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterResourceAdded = new("interpreter-resource-added");
    /// <summary>
    /// Interpreter Resource Deleted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason InterpreterResourceDeleted = new("interpreter-resource-deleted");
    /// <summary>
    /// Make Envelope Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeAdded = new("make-envelope-added");
    /// <summary>
    /// Make Envelope Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeAlmostEmpty = new("make-envelope-almost-empty");
    /// <summary>
    /// Make Envelope Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeAlmostFull = new("make-envelope-almost-full");
    /// <summary>
    /// Make Envelope At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeAtLimit = new("make-envelope-at-limit");
    /// <summary>
    /// Make Envelope Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeClosed = new("make-envelope-closed");
    /// <summary>
    /// Make Envelope Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeConfigurationChange = new("make-envelope-configuration-change");
    /// <summary>
    /// Make Envelope Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeCoverClosed = new("make-envelope-cover-closed");
    /// <summary>
    /// Make Envelope Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeCoverOpen = new("make-envelope-cover-open");
    /// <summary>
    /// Make Envelope Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeEmpty = new("make-envelope-empty");
    /// <summary>
    /// Make Envelope Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeFull = new("make-envelope-full");
    /// <summary>
    /// Make Envelope Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeInterlockClosed = new("make-envelope-interlock-closed");
    /// <summary>
    /// Make Envelope Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeInterlockOpen = new("make-envelope-interlock-open");
    /// <summary>
    /// Make Envelope Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeJam = new("make-envelope-jam");
    /// <summary>
    /// Make Envelope Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeLifeAlmostOver = new("make-envelope-life-almost-over");
    /// <summary>
    /// Make Envelope Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeLifeOver = new("make-envelope-life-over");
    /// <summary>
    /// Make Envelope Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeMemoryExhausted = new("make-envelope-memory-exhausted");
    /// <summary>
    /// Make Envelope Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeMissing = new("make-envelope-missing");
    /// <summary>
    /// Make Envelope Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeMotorFailure = new("make-envelope-motor-failure");
    /// <summary>
    /// Make Envelope Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeNearLimit = new("make-envelope-near-limit");
    /// <summary>
    /// Make Envelope Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeOffline = new("make-envelope-offline");
    /// <summary>
    /// Make Envelope Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeOpened = new("make-envelope-opened");
    /// <summary>
    /// Make Envelope Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeOverTemperature = new("make-envelope-over-temperature");
    /// <summary>
    /// Make Envelope Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopePowerSaver = new("make-envelope-power-saver");
    /// <summary>
    /// Make Envelope Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeRecoverableFailure = new("make-envelope-recoverable-failure");
    /// <summary>
    /// Make Envelope Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeRecoverableStorageError = new("make-envelope-recoverable-storage-error");
    /// <summary>
    /// Make Envelope Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeRemoved = new("make-envelope-removed");
    /// <summary>
    /// Make Envelope Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeResourceRemoved = new("make-envelope-resource-removed");
    /// <summary>
    /// Make Envelope Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeThermistorFailure = new("make-envelope-thermistor-failure");
    /// <summary>
    /// Make Envelope Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeTimingFailure = new("make-envelope-timing-failure");
    /// <summary>
    /// Make Envelope Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeTurnedOff = new("make-envelope-turned-off");
    /// <summary>
    /// Make Envelope Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeTurnedOn = new("make-envelope-turned-on");
    /// <summary>
    /// Make Envelope Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeUnderTemperature = new("make-envelope-under-temperature");
    /// <summary>
    /// Make Envelope Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeUnrecoverableFailure = new("make-envelope-unrecoverable-failure");
    /// <summary>
    /// Make Envelope Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeUnrecoverableStorageError = new("make-envelope-unrecoverable-storage-error");
    /// <summary>
    /// Make Envelope Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MakeEnvelopeWarmingUp = new("make-envelope-warming-up");
    /// <summary>
    /// Marker Adjusting Print Quality printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerAdjustingPrintQuality = new("marker-adjusting-print-quality");
    /// <summary>
    /// Marker Developer Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerDeveloperAlmostEmpty = new("marker-developer-almost-empty");
    /// <summary>
    /// Marker Developer Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerDeveloperEmpty = new("marker-developer-empty");
    /// <summary>
    /// Marker Fuser Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerFuserThermistorFailure = new("marker-fuser-thermistor-failure");
    /// <summary>
    /// Marker Fuser Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerFuserTimingFailure = new("marker-fuser-timing-failure");
    /// <summary>
    /// Marker Ink Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerInkAlmostEmpty = new("marker-ink-almost-empty");
    /// <summary>
    /// Marker Ink Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerInkEmpty = new("marker-ink-empty");
    /// <summary>
    /// Marker Print Ribbon Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerPrintRibbonAlmostEmpty = new("marker-print-ribbon-almost-empty");
    /// <summary>
    /// Marker Print Ribbon Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerPrintRibbonEmpty = new("marker-print-ribbon-empty");
    /// <summary>
    /// Marker Toner Cartridge Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerTonerCartridgeMissing = new("marker-toner-cartridge-missing");
    /// <summary>
    /// Marker Waste Ink Receptacle Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerWasteInkReceptacleAlmostFull = new("marker-waste-ink-receptacle-almost-full");
    /// <summary>
    /// Marker Waste Ink Receptacle Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerWasteInkReceptacleFull = new("marker-waste-ink-receptacle-full");
    /// <summary>
    /// Marker Waste Toner Receptacle Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerWasteTonerReceptacleAlmostFull = new("marker-waste-toner-receptacle-almost-full");
    /// <summary>
    /// Marker Waste Toner Receptacle Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MarkerWasteTonerReceptacleFull = new("marker-waste-toner-receptacle-full");
    /// <summary>
    /// Media Path Cannot Duplex Media Selected printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MediaPathCannotDuplexMediaSelected = new("media-path-cannot-duplex-media-selected");
    /// <summary>
    /// Media Path Media Tray Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MediaPathMediaTrayAlmostFull = new("media-path-media-tray-almost-full");
    /// <summary>
    /// Media Path Media Tray Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MediaPathMediaTrayFull = new("media-path-media-tray-full");
    /// <summary>
    /// Media Path Media Tray Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason MediaPathMediaTrayMissing = new("media-path-media-tray-missing");
    /// <summary>
    /// Output Mailbox Select Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason OutputMailboxSelectFailure = new("output-mailbox-select-failure");
    /// <summary>
    /// Perforater Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterAdded = new("perforater-added");
    /// <summary>
    /// Perforater Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterAlmostEmpty = new("perforater-almost-empty");
    /// <summary>
    /// Perforater Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterAlmostFull = new("perforater-almost-full");
    /// <summary>
    /// Perforater At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterAtLimit = new("perforater-at-limit");
    /// <summary>
    /// Perforater Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterClosed = new("perforater-closed");
    /// <summary>
    /// Perforater Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterConfigurationChange = new("perforater-configuration-change");
    /// <summary>
    /// Perforater Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterCoverClosed = new("perforater-cover-closed");
    /// <summary>
    /// Perforater Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterCoverOpen = new("perforater-cover-open");
    /// <summary>
    /// Perforater Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterEmpty = new("perforater-empty");
    /// <summary>
    /// Perforater Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterFull = new("perforater-full");
    /// <summary>
    /// Perforater Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterInterlockClosed = new("perforater-interlock-closed");
    /// <summary>
    /// Perforater Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterInterlockOpen = new("perforater-interlock-open");
    /// <summary>
    /// Perforater Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterJam = new("perforater-jam");
    /// <summary>
    /// Perforater Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterLifeAlmostOver = new("perforater-life-almost-over");
    /// <summary>
    /// Perforater Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterLifeOver = new("perforater-life-over");
    /// <summary>
    /// Perforater Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterMemoryExhausted = new("perforater-memory-exhausted");
    /// <summary>
    /// Perforater Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterMissing = new("perforater-missing");
    /// <summary>
    /// Perforater Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterMotorFailure = new("perforater-motor-failure");
    /// <summary>
    /// Perforater Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterNearLimit = new("perforater-near-limit");
    /// <summary>
    /// Perforater Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterOffline = new("perforater-offline");
    /// <summary>
    /// Perforater Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterOpened = new("perforater-opened");
    /// <summary>
    /// Perforater Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterOverTemperature = new("perforater-over-temperature");
    /// <summary>
    /// Perforater Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterPowerSaver = new("perforater-power-saver");
    /// <summary>
    /// Perforater Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterRecoverableFailure = new("perforater-recoverable-failure");
    /// <summary>
    /// Perforater Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterRecoverableStorageError = new("perforater-recoverable-storage-error");
    /// <summary>
    /// Perforater Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterRemoved = new("perforater-removed");
    /// <summary>
    /// Perforater Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterResourceAdded = new("perforater-resource-added");
    /// <summary>
    /// Perforater Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterResourceRemoved = new("perforater-resource-removed");
    /// <summary>
    /// Perforater Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterThermistorFailure = new("perforater-thermistor-failure");
    /// <summary>
    /// Perforater Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterTimingFailure = new("perforater-timing-failure");
    /// <summary>
    /// Perforater Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterTurnedOff = new("perforater-turned-off");
    /// <summary>
    /// Perforater Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterTurnedOn = new("perforater-turned-on");
    /// <summary>
    /// Perforater Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterUnrecoverableFailure = new("perforater-unrecoverable-failure");
    /// <summary>
    /// Perforater Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PerforaterWarmingUp = new("perforater-warming-up");
    /// <summary>
    /// Power Down printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PowerDown = new("power-down");
    /// <summary>
    /// Power Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PowerUp = new("power-up");
    /// <summary>
    /// Printer Manual Reset printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PrinterManualReset = new("printer-manual-reset");
    /// <summary>
    /// Printer Nms Reset printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PrinterNmsReset = new("printer-nms-reset");
    /// <summary>
    /// Printer Ready To Print printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PrinterReadyToPrint = new("printer-ready-to-print");
    /// <summary>
    /// Puncher Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherAdded = new("puncher-added");
    /// <summary>
    /// Puncher Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherAlmostEmpty = new("puncher-almost-empty");
    /// <summary>
    /// Puncher Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherAlmostFull = new("puncher-almost-full");
    /// <summary>
    /// Puncher At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherAtLimit = new("puncher-at-limit");
    /// <summary>
    /// Puncher Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherClosed = new("puncher-closed");
    /// <summary>
    /// Puncher Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherConfigurationChange = new("puncher-configuration-change");
    /// <summary>
    /// Puncher Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherCoverClosed = new("puncher-cover-closed");
    /// <summary>
    /// Puncher Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherEmpty = new("puncher-empty");
    /// <summary>
    /// Puncher Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherInterlockClosed = new("puncher-interlock-closed");
    /// <summary>
    /// Puncher Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherInterlockOpen = new("puncher-interlock-open");
    /// <summary>
    /// Puncher Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherJam = new("puncher-jam");
    /// <summary>
    /// Puncher Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherLifeAlmostOver = new("puncher-life-almost-over");
    /// <summary>
    /// Puncher Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherLifeOver = new("puncher-life-over");
    /// <summary>
    /// Puncher Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherMemoryExhausted = new("puncher-memory-exhausted");
    /// <summary>
    /// Puncher Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherMissing = new("puncher-missing");
    /// <summary>
    /// Puncher Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherMotorFailure = new("puncher-motor-failure");
    /// <summary>
    /// Puncher Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherNearLimit = new("puncher-near-limit");
    /// <summary>
    /// Puncher Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherOffline = new("puncher-offline");
    /// <summary>
    /// Puncher Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherOpened = new("puncher-opened");
    /// <summary>
    /// Puncher Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherOverTemperature = new("puncher-over-temperature");
    /// <summary>
    /// Puncher Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherPowerSaver = new("puncher-power-saver");
    /// <summary>
    /// Puncher Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherRecoverableFailure = new("puncher-recoverable-failure");
    /// <summary>
    /// Puncher Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherRecoverableStorageError = new("puncher-recoverable-storage-error");
    /// <summary>
    /// Puncher Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherRemoved = new("puncher-removed");
    /// <summary>
    /// Puncher Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherResourceAdded = new("puncher-resource-added");
    /// <summary>
    /// Puncher Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherResourceRemoved = new("puncher-resource-removed");
    /// <summary>
    /// Puncher Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherThermistorFailure = new("puncher-thermistor-failure");
    /// <summary>
    /// Puncher Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherTimingFailure = new("puncher-timing-failure");
    /// <summary>
    /// Puncher Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherTurnedOff = new("puncher-turned-off");
    /// <summary>
    /// Puncher Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherTurnedOn = new("puncher-turned-on");
    /// <summary>
    /// Puncher Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherUnderTemperature = new("puncher-under-temperature");
    /// <summary>
    /// Puncher Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherUnrecoverableFailure = new("puncher-unrecoverable-failure");
    /// <summary>
    /// Puncher Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherUnrecoverableStorageError = new("puncher-unrecoverable-storage-error");
    /// <summary>
    /// Puncher Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason PuncherWarmingUp = new("puncher-warming-up");
    /// <summary>
    /// Separation Cutter Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterAdded = new("separation-cutter-added");
    /// <summary>
    /// Separation Cutter Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterAlmostEmpty = new("separation-cutter-almost-empty");
    /// <summary>
    /// Separation Cutter Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterAlmostFull = new("separation-cutter-almost-full");
    /// <summary>
    /// Separation Cutter At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterAtLimit = new("separation-cutter-at-limit");
    /// <summary>
    /// Separation Cutter Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterConfigurationChange = new("separation-cutter-configuration-change");
    /// <summary>
    /// Separation Cutter Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterCoverClosed = new("separation-cutter-cover-closed");
    /// <summary>
    /// Separation Cutter Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterCoverOpen = new("separation-cutter-cover-open");
    /// <summary>
    /// Separation Cutter Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterEmpty = new("separation-cutter-empty");
    /// <summary>
    /// Separation Cutter Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterFull = new("separation-cutter-full");
    /// <summary>
    /// Separation Cutter Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterInterlockClosed = new("separation-cutter-interlock-closed");
    /// <summary>
    /// Separation Cutter Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterInterlockOpen = new("separation-cutter-interlock-open");
    /// <summary>
    /// Separation Cutter Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterJam = new("separation-cutter-jam");
    /// <summary>
    /// Separation Cutter Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterLifeAlmostOver = new("separation-cutter-life-almost-over");
    /// <summary>
    /// Separation Cutter Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterLifeOver = new("separation-cutter-life-over");
    /// <summary>
    /// Separation Cutter Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterMemoryExhausted = new("separation-cutter-memory-exhausted");
    /// <summary>
    /// Separation Cutter Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterMissing = new("separation-cutter-missing");
    /// <summary>
    /// Separation Cutter Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterMotorFailure = new("separation-cutter-motor-failure");
    /// <summary>
    /// Separation Cutter Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterNearLimit = new("separation-cutter-near-limit");
    /// <summary>
    /// Separation Cutter Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterOffline = new("separation-cutter-offline");
    /// <summary>
    /// Separation Cutter Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterOpened = new("separation-cutter-opened");
    /// <summary>
    /// Separation Cutter Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterOverTemperature = new("separation-cutter-over-temperature");
    /// <summary>
    /// Separation Cutter Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterPowerSaver = new("separation-cutter-power-saver");
    /// <summary>
    /// Separation Cutter Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterRecoverableFailure = new("separation-cutter-recoverable-failure");
    /// <summary>
    /// Separation Cutter Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterRecoverableStorageError = new("separation-cutter-recoverable-storage-error");
    /// <summary>
    /// Separation Cutter Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterRemoved = new("separation-cutter-removed");
    /// <summary>
    /// Separation Cutter Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterResourceAdded = new("separation-cutter-resource-added");
    /// <summary>
    /// Separation Cutter Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterResourceRemoved = new("separation-cutter-resource-removed");
    /// <summary>
    /// Separation Cutter Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterThermistorFailure = new("separation-cutter-thermistor-failure");
    /// <summary>
    /// Separation Cutter Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterTimingFailure = new("separation-cutter-timing-failure");
    /// <summary>
    /// Separation Cutter Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterTurnedOff = new("separation-cutter-turned-off");
    /// <summary>
    /// Separation Cutter Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterTurnedOn = new("separation-cutter-turned-on");
    /// <summary>
    /// Separation Cutter Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterUnderTemperature = new("separation-cutter-under-temperature");
    /// <summary>
    /// Separation Cutter Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterUnrecoverableFailure = new("separation-cutter-unrecoverable-failure");
    /// <summary>
    /// Separation Cutter Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SeparationCutterUnrecoverableStorageError = new("separation-cutter-unrecoverable-storage-error");
    /// <summary>
    /// Sheet Rotator Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorAdded = new("sheet-rotator-added");
    /// <summary>
    /// Sheet Rotator Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorAlmostEmpty = new("sheet-rotator-almost-empty");
    /// <summary>
    /// Sheet Rotator Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorAlmostFull = new("sheet-rotator-almost-full");
    /// <summary>
    /// Sheet Rotator At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorAtLimit = new("sheet-rotator-at-limit");
    /// <summary>
    /// Sheet Rotator Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorClosed = new("sheet-rotator-closed");
    /// <summary>
    /// Sheet Rotator Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorConfigurationChange = new("sheet-rotator-configuration-change");
    /// <summary>
    /// Sheet Rotator Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorCoverClosed = new("sheet-rotator-cover-closed");
    /// <summary>
    /// Sheet Rotator Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorCoverOpen = new("sheet-rotator-cover-open");
    /// <summary>
    /// Sheet Rotator Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorEmpty = new("sheet-rotator-empty");
    /// <summary>
    /// Sheet Rotator Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorFull = new("sheet-rotator-full");
    /// <summary>
    /// Sheet Rotator Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorInterlockClosed = new("sheet-rotator-interlock-closed");
    /// <summary>
    /// Sheet Rotator Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorInterlockOpen = new("sheet-rotator-interlock-open");
    /// <summary>
    /// Sheet Rotator Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorJam = new("sheet-rotator-jam");
    /// <summary>
    /// Sheet Rotator Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorLifeAlmostOver = new("sheet-rotator-life-almost-over");
    /// <summary>
    /// Sheet Rotator Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorLifeOver = new("sheet-rotator-life-over");
    /// <summary>
    /// Sheet Rotator Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorMemoryExhausted = new("sheet-rotator-memory-exhausted");
    /// <summary>
    /// Sheet Rotator Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorMissing = new("sheet-rotator-missing");
    /// <summary>
    /// Sheet Rotator Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorMotorFailure = new("sheet-rotator-motor-failure");
    /// <summary>
    /// Sheet Rotator Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorNearLimit = new("sheet-rotator-near-limit");
    /// <summary>
    /// Sheet Rotator Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorOffline = new("sheet-rotator-offline");
    /// <summary>
    /// Sheet Rotator Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorOpened = new("sheet-rotator-opened");
    /// <summary>
    /// Sheet Rotator Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorOverTemperature = new("sheet-rotator-over-temperature");
    /// <summary>
    /// Sheet Rotator Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorPowerSaver = new("sheet-rotator-power-saver");
    /// <summary>
    /// Sheet Rotator Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorRecoverableFailure = new("sheet-rotator-recoverable-failure");
    /// <summary>
    /// Sheet Rotator Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorRecoverableStorageError = new("sheet-rotator-recoverable-storage-error");
    /// <summary>
    /// Sheet Rotator Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorResourceAdded = new("sheet-rotator-resource-added");
    /// <summary>
    /// Sheet Rotator Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorResourceRemoved = new("sheet-rotator-resource-removed");
    /// <summary>
    /// Sheet Rotator Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorThermistorFailure = new("sheet-rotator-thermistor-failure");
    /// <summary>
    /// Sheet Rotator Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorTimingFailure = new("sheet-rotator-timing-failure");
    /// <summary>
    /// Sheet Rotator Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorTurnedOff = new("sheet-rotator-turned-off");
    /// <summary>
    /// Sheet Rotator Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorTurnedOn = new("sheet-rotator-turned-on");
    /// <summary>
    /// Sheet Rotator Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorUnderTemperature = new("sheet-rotator-under-temperature");
    /// <summary>
    /// Sheet Rotator Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorUnrecoverableFailure = new("sheet-rotator-unrecoverable-failure");
    /// <summary>
    /// Sheet Rotator Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorUnrecoverableStorageError = new("sheet-rotator-unrecoverable-storage-error");
    /// <summary>
    /// Sheet Rotator Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SheetRotatorWarmingUp = new("sheet-rotator-warming-up");
    /// <summary>
    /// Slitter Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterAdded = new("slitter-added");
    /// <summary>
    /// Slitter Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterAlmostEmpty = new("slitter-almost-empty");
    /// <summary>
    /// Slitter Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterAlmostFull = new("slitter-almost-full");
    /// <summary>
    /// Slitter At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterAtLimit = new("slitter-at-limit");
    /// <summary>
    /// Slitter Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterClosed = new("slitter-closed");
    /// <summary>
    /// Slitter Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterConfigurationChange = new("slitter-configuration-change");
    /// <summary>
    /// Slitter Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterCoverClosed = new("slitter-cover-closed");
    /// <summary>
    /// Slitter Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterCoverOpen = new("slitter-cover-open");
    /// <summary>
    /// Slitter Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterEmpty = new("slitter-empty");
    /// <summary>
    /// Slitter Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterFull = new("slitter-full");
    /// <summary>
    /// Slitter Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterInterlockClosed = new("slitter-interlock-closed");
    /// <summary>
    /// Slitter Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterInterlockOpen = new("slitter-interlock-open");
    /// <summary>
    /// Slitter Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterJam = new("slitter-jam");
    /// <summary>
    /// Slitter Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterLifeAlmostOver = new("slitter-life-almost-over");
    /// <summary>
    /// Slitter Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterLifeOver = new("slitter-life-over");
    /// <summary>
    /// Slitter Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterMemoryExhausted = new("slitter-memory-exhausted");
    /// <summary>
    /// Slitter Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterMissing = new("slitter-missing");
    /// <summary>
    /// Slitter Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterMotorFailure = new("slitter-motor-failure");
    /// <summary>
    /// Slitter Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterNearLimit = new("slitter-near-limit");
    /// <summary>
    /// Slitter Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterOffline = new("slitter-offline");
    /// <summary>
    /// Slitter Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterOverTemperature = new("slitter-over-temperature");
    /// <summary>
    /// Slitter Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterPowerSaver = new("slitter-power-saver");
    /// <summary>
    /// Slitter Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterRecoverableFailure = new("slitter-recoverable-failure");
    /// <summary>
    /// Slitter Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterRecoverableStorageError = new("slitter-recoverable-storage-error");
    /// <summary>
    /// Slitter Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterRemoved = new("slitter-removed");
    /// <summary>
    /// Slitter Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterResourceAdded = new("slitter-resource-added");
    /// <summary>
    /// Slitter Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterResourceRemoved = new("slitter-resource-removed");
    /// <summary>
    /// Slitter Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterThermistorFailure = new("slitter-thermistor-failure");
    /// <summary>
    /// Slitter Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterTimingFailure = new("slitter-timing-failure");
    /// <summary>
    /// Slitter Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterTurnedOff = new("slitter-turned-off");
    /// <summary>
    /// Slitter Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterTurnedOn = new("slitter-turned-on");
    /// <summary>
    /// Slitter Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterUnderTemperature = new("slitter-under-temperature");
    /// <summary>
    /// Slitter Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterUnrecoverableFailure = new("slitter-unrecoverable-failure");
    /// <summary>
    /// Slitter Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterUnrecoverableStorageError = new("slitter-unrecoverable-storage-error");
    /// <summary>
    /// Slitter Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SlitterWarmingUp = new("slitter-warming-up");
    /// <summary>
    /// Stacker Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerAdded = new("stacker-added");
    /// <summary>
    /// Stacker Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerAlmostEmpty = new("stacker-almost-empty");
    /// <summary>
    /// Stacker Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerAlmostFull = new("stacker-almost-full");
    /// <summary>
    /// Stacker At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerAtLimit = new("stacker-at-limit");
    /// <summary>
    /// Stacker Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerClosed = new("stacker-closed");
    /// <summary>
    /// Stacker Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerConfigurationChange = new("stacker-configuration-change");
    /// <summary>
    /// Stacker Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerCoverClosed = new("stacker-cover-closed");
    /// <summary>
    /// Stacker Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerCoverOpen = new("stacker-cover-open");
    /// <summary>
    /// Stacker Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerEmpty = new("stacker-empty");
    /// <summary>
    /// Stacker Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerFull = new("stacker-full");
    /// <summary>
    /// Stacker Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerInterlockOpen = new("stacker-interlock-open");
    /// <summary>
    /// Stacker Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerJam = new("stacker-jam");
    /// <summary>
    /// Stacker Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerLifeAlmostOver = new("stacker-life-almost-over");
    /// <summary>
    /// Stacker Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerLifeOver = new("stacker-life-over");
    /// <summary>
    /// Stacker Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerMemoryExhausted = new("stacker-memory-exhausted");
    /// <summary>
    /// Stacker Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerMissing = new("stacker-missing");
    /// <summary>
    /// Stacker Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerMotorFailure = new("stacker-motor-failure");
    /// <summary>
    /// Stacker Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerNearLimit = new("stacker-near-limit");
    /// <summary>
    /// Stacker Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerOffline = new("stacker-offline");
    /// <summary>
    /// Stacker Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerOpened = new("stacker-opened");
    /// <summary>
    /// Stacker Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerOverTemperature = new("stacker-over-temperature");
    /// <summary>
    /// Stacker Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerPowerSaver = new("stacker-power-saver");
    /// <summary>
    /// Stacker Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerRecoverableFailure = new("stacker-recoverable-failure");
    /// <summary>
    /// Stacker Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerRecoverableStorageError = new("stacker-recoverable-storage-error");
    /// <summary>
    /// Stacker Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerRemoved = new("stacker-removed");
    /// <summary>
    /// Stacker Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerResourceAdded = new("stacker-resource-added");
    /// <summary>
    /// Stacker Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerResourceRemoved = new("stacker-resource-removed");
    /// <summary>
    /// Stacker Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerThermistorFailure = new("stacker-thermistor-failure");
    /// <summary>
    /// Stacker Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerTimingFailure = new("stacker-timing-failure");
    /// <summary>
    /// Stacker Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerTurnedOff = new("stacker-turned-off");
    /// <summary>
    /// Stacker Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerTurnedOn = new("stacker-turned-on");
    /// <summary>
    /// Stacker Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerUnderTemperature = new("stacker-under-temperature");
    /// <summary>
    /// Stacker Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerUnrecoverableFailure = new("stacker-unrecoverable-failure");
    /// <summary>
    /// Stacker Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerUnrecoverableStorageError = new("stacker-unrecoverable-storage-error");
    /// <summary>
    /// Stacker Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StackerWarmingUp = new("stacker-warming-up");
    /// <summary>
    /// Stapler Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerAdded = new("stapler-added");
    /// <summary>
    /// Stapler Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerAlmostEmpty = new("stapler-almost-empty");
    /// <summary>
    /// Stapler Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerAlmostFull = new("stapler-almost-full");
    /// <summary>
    /// Stapler At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerAtLimit = new("stapler-at-limit");
    /// <summary>
    /// Stapler Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerClosed = new("stapler-closed");
    /// <summary>
    /// Stapler Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerConfigurationChange = new("stapler-configuration-change");
    /// <summary>
    /// Stapler Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerCoverClosed = new("stapler-cover-closed");
    /// <summary>
    /// Stapler Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerCoverOpen = new("stapler-cover-open");
    /// <summary>
    /// Stapler Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerEmpty = new("stapler-empty");
    /// <summary>
    /// Stapler Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerFull = new("stapler-full");
    /// <summary>
    /// Stapler Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerInterlockClosed = new("stapler-interlock-closed");
    /// <summary>
    /// Stapler Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerInterlockOpen = new("stapler-interlock-open");
    /// <summary>
    /// Stapler Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerJam = new("stapler-jam");
    /// <summary>
    /// Stapler Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerLifeAlmostOver = new("stapler-life-almost-over");
    /// <summary>
    /// Stapler Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerLifeOver = new("stapler-life-over");
    /// <summary>
    /// Stapler Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerMemoryExhausted = new("stapler-memory-exhausted");
    /// <summary>
    /// Stapler Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerMissing = new("stapler-missing");
    /// <summary>
    /// Stapler Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerMotorFailure = new("stapler-motor-failure");
    /// <summary>
    /// Stapler Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerNearLimit = new("stapler-near-limit");
    /// <summary>
    /// Stapler Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerOffline = new("stapler-offline");
    /// <summary>
    /// Stapler Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerOpened = new("stapler-opened");
    /// <summary>
    /// Stapler Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerOverTemperature = new("stapler-over-temperature");
    /// <summary>
    /// Stapler Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerPowerSaver = new("stapler-power-saver");
    /// <summary>
    /// Stapler Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerRecoverableFailure = new("stapler-recoverable-failure");
    /// <summary>
    /// Stapler Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerRecoverableStorageError = new("stapler-recoverable-storage-error");
    /// <summary>
    /// Stapler Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerRemoved = new("stapler-removed");
    /// <summary>
    /// Stapler Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerResourceAdded = new("stapler-resource-added");
    /// <summary>
    /// Stapler Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerResourceRemoved = new("stapler-resource-removed");
    /// <summary>
    /// Stapler Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerThermistorFailure = new("stapler-thermistor-failure");
    /// <summary>
    /// Stapler Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerTimingFailure = new("stapler-timing-failure");
    /// <summary>
    /// Stapler Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerTurnedOff = new("stapler-turned-off");
    /// <summary>
    /// Stapler Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerTurnedOn = new("stapler-turned-on");
    /// <summary>
    /// Stapler Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerUnderTemperature = new("stapler-under-temperature");
    /// <summary>
    /// Stapler Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerUnrecoverableFailure = new("stapler-unrecoverable-failure");
    /// <summary>
    /// Stapler Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerUnrecoverableStorageError = new("stapler-unrecoverable-storage-error");
    /// <summary>
    /// Stapler Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StaplerWarmingUp = new("stapler-warming-up");
    /// <summary>
    /// Stitcher Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherAdded = new("stitcher-added");
    /// <summary>
    /// Stitcher Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherAlmostEmpty = new("stitcher-almost-empty");
    /// <summary>
    /// Stitcher Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherAlmostFull = new("stitcher-almost-full");
    /// <summary>
    /// Stitcher At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherAtLimit = new("stitcher-at-limit");
    /// <summary>
    /// Stitcher Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherClosed = new("stitcher-closed");
    /// <summary>
    /// Stitcher Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherConfigurationChange = new("stitcher-configuration-change");
    /// <summary>
    /// Stitcher Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherCoverClosed = new("stitcher-cover-closed");
    /// <summary>
    /// Stitcher Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherCoverOpen = new("stitcher-cover-open");
    /// <summary>
    /// Stitcher Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherEmpty = new("stitcher-empty");
    /// <summary>
    /// Stitcher Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherFull = new("stitcher-full");
    /// <summary>
    /// Stitcher Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherInterlockClosed = new("stitcher-interlock-closed");
    /// <summary>
    /// Stitcher Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherInterlockOpen = new("stitcher-interlock-open");
    /// <summary>
    /// Stitcher Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherJam = new("stitcher-jam");
    /// <summary>
    /// Stitcher Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherLifeAlmostOver = new("stitcher-life-almost-over");
    /// <summary>
    /// Stitcher Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherLifeOver = new("stitcher-life-over");
    /// <summary>
    /// Stitcher Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherMemoryExhausted = new("stitcher-memory-exhausted");
    /// <summary>
    /// Stitcher Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherMissing = new("stitcher-missing");
    /// <summary>
    /// Stitcher Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherMotorFailure = new("stitcher-motor-failure");
    /// <summary>
    /// Stitcher Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherNearLimit = new("stitcher-near-limit");
    /// <summary>
    /// Stitcher Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherOffline = new("stitcher-offline");
    /// <summary>
    /// Stitcher Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherOpened = new("stitcher-opened");
    /// <summary>
    /// Stitcher Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherOverTemperature = new("stitcher-over-temperature");
    /// <summary>
    /// Stitcher Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherPowerSaver = new("stitcher-power-saver");
    /// <summary>
    /// Stitcher Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherRecoverableFailure = new("stitcher-recoverable-failure");
    /// <summary>
    /// Stitcher Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherRecoverableStorageError = new("stitcher-recoverable-storage-error");
    /// <summary>
    /// Stitcher Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherRemoved = new("stitcher-removed");
    /// <summary>
    /// Stitcher Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherResourceAdded = new("stitcher-resource-added");
    /// <summary>
    /// Stitcher Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherResourceRemoved = new("stitcher-resource-removed");
    /// <summary>
    /// Stitcher Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherThermistorFailure = new("stitcher-thermistor-failure");
    /// <summary>
    /// Stitcher Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherTimingFailure = new("stitcher-timing-failure");
    /// <summary>
    /// Stitcher Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherTurnedOff = new("stitcher-turned-off");
    /// <summary>
    /// Stitcher Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherTurnedOn = new("stitcher-turned-on");
    /// <summary>
    /// Stitcher Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherUnderTemperature = new("stitcher-under-temperature");
    /// <summary>
    /// Stitcher Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherUnrecoverableFailure = new("stitcher-unrecoverable-failure");
    /// <summary>
    /// Stitcher Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherUnrecoverableStorageError = new("stitcher-unrecoverable-storage-error");
    /// <summary>
    /// Stitcher Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason StitcherWarmingUp = new("stitcher-warming-up");
    /// <summary>
    /// Subunit Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitAdded = new("subunit-added");
    /// <summary>
    /// Subunit Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitAlmostEmpty = new("subunit-almost-empty");
    /// <summary>
    /// Subunit Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitAlmostFull = new("subunit-almost-full");
    /// <summary>
    /// Subunit At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitAtLimit = new("subunit-at-limit");
    /// <summary>
    /// Subunit Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitClosed = new("subunit-closed");
    /// <summary>
    /// Subunit Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitEmpty = new("subunit-empty");
    /// <summary>
    /// Subunit Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitFull = new("subunit-full");
    /// <summary>
    /// Subunit Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitLifeAlmostOver = new("subunit-life-almost-over");
    /// <summary>
    /// Subunit Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitLifeOver = new("subunit-life-over");
    /// <summary>
    /// Subunit Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitMemoryExhausted = new("subunit-memory-exhausted");
    /// <summary>
    /// Subunit Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitMissing = new("subunit-missing");
    /// <summary>
    /// Subunit Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitMotorFailure = new("subunit-motor-failure");
    /// <summary>
    /// Subunit Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitNearLimit = new("subunit-near-limit");
    /// <summary>
    /// Subunit Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitOffline = new("subunit-offline");
    /// <summary>
    /// Subunit Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitOpened = new("subunit-opened");
    /// <summary>
    /// Subunit Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitOverTemperature = new("subunit-over-temperature");
    /// <summary>
    /// Subunit Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitPowerSaver = new("subunit-power-saver");
    /// <summary>
    /// Subunit Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitRecoverableFailure = new("subunit-recoverable-failure");
    /// <summary>
    /// Subunit Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitRecoverableStorageError = new("subunit-recoverable-storage-error");
    /// <summary>
    /// Subunit Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitRemoved = new("subunit-removed");
    /// <summary>
    /// Subunit Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitResourceAdded = new("subunit-resource-added");
    /// <summary>
    /// Subunit Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitThermistorFailure = new("subunit-thermistor-failure");
    /// <summary>
    /// Subunit Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitTimingFailure = new("subunit-timing-failure");
    /// <summary>
    /// Subunit Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitTurnedOff = new("subunit-turned-off");
    /// <summary>
    /// Subunit Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitTurnedOn = new("subunit-turned-on");
    /// <summary>
    /// Subunit Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitUnderTemperature = new("subunit-under-temperature");
    /// <summary>
    /// Subunit Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitUnrecoverableFailure = new("subunit-unrecoverable-failure");
    /// <summary>
    /// Subunit Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitUnrecoverableStorageError = new("subunit-unrecoverable-storage-error");
    /// <summary>
    /// Subunit Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason SubunitWarmingUp = new("subunit-warming-up");
    /// <summary>
    /// Trimmer Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerAdded = new("trimmer-added");
    /// <summary>
    /// Trimmer Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerAlmostEmpty = new("trimmer-almost-empty");
    /// <summary>
    /// Trimmer Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerAlmostFull = new("trimmer-almost-full");
    /// <summary>
    /// Trimmer At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerAtLimit = new("trimmer-at-limit");
    /// <summary>
    /// Trimmer Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerClosed = new("trimmer-closed");
    /// <summary>
    /// Trimmer Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerConfigurationChange = new("trimmer-configuration-change");
    /// <summary>
    /// Trimmer Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerCoverClosed = new("trimmer-cover-closed");
    /// <summary>
    /// Trimmer Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerCoverOpen = new("trimmer-cover-open");
    /// <summary>
    /// Trimmer Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerEmpty = new("trimmer-empty");
    /// <summary>
    /// Trimmer Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerFull = new("trimmer-full");
    /// <summary>
    /// Trimmer Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerInterlockClosed = new("trimmer-interlock-closed");
    /// <summary>
    /// Trimmer Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerInterlockOpen = new("trimmer-interlock-open");
    /// <summary>
    /// Trimmer Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerJam = new("trimmer-jam");
    /// <summary>
    /// Trimmer Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerLifeAlmostOver = new("trimmer-life-almost-over");
    /// <summary>
    /// Trimmer Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerLifeOver = new("trimmer-life-over");
    /// <summary>
    /// Trimmer Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerMemoryExhausted = new("trimmer-memory-exhausted");
    /// <summary>
    /// Trimmer Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerMotorFailure = new("trimmer-motor-failure");
    /// <summary>
    /// Trimmer Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerNearLimit = new("trimmer-near-limit");
    /// <summary>
    /// Trimmer Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerOffline = new("trimmer-offline");
    /// <summary>
    /// Trimmer Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerOpened = new("trimmer-opened");
    /// <summary>
    /// Trimmer Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerOverTemperature = new("trimmer-over-temperature");
    /// <summary>
    /// Trimmer Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerPowerSaver = new("trimmer-power-saver");
    /// <summary>
    /// Trimmer Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerRecoverableFailure = new("trimmer-recoverable-failure");
    /// <summary>
    /// Trimmer Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerRecoverableStorageError = new("trimmer-recoverable-storage-error");
    /// <summary>
    /// Trimmer Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerRemoved = new("trimmer-removed");
    /// <summary>
    /// Trimmer Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerResourceAdded = new("trimmer-resource-added");
    /// <summary>
    /// Trimmer Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerResourceRemoved = new("trimmer-resource-removed");
    /// <summary>
    /// Trimmer Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerThermistorFailure = new("trimmer-thermistor-failure");
    /// <summary>
    /// Trimmer Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerTimingFailure = new("trimmer-timing-failure");
    /// <summary>
    /// Trimmer Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerTurnedOff = new("trimmer-turned-off");
    /// <summary>
    /// Trimmer Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerTurnedOn = new("trimmer-turned-on");
    /// <summary>
    /// Trimmer Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerUnderTemperature = new("trimmer-under-temperature");
    /// <summary>
    /// Trimmer Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerUnrecoverableFailure = new("trimmer-unrecoverable-failure");
    /// <summary>
    /// Trimmer Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerUnrecoverableStorageError = new("trimmer-unrecoverable-storage-error");
    /// <summary>
    /// Trimmer Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason TrimmerWarmingUp = new("trimmer-warming-up");
    /// <summary>
    /// Wrapper Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperAdded = new("wrapper-added");
    /// <summary>
    /// Wrapper Almost Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperAlmostEmpty = new("wrapper-almost-empty");
    /// <summary>
    /// Wrapper Almost Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperAlmostFull = new("wrapper-almost-full");
    /// <summary>
    /// Wrapper At Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperAtLimit = new("wrapper-at-limit");
    /// <summary>
    /// Wrapper Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperClosed = new("wrapper-closed");
    /// <summary>
    /// Wrapper Configuration Change printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperConfigurationChange = new("wrapper-configuration-change");
    /// <summary>
    /// Wrapper Cover Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperCoverClosed = new("wrapper-cover-closed");
    /// <summary>
    /// Wrapper Cover Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperCoverOpen = new("wrapper-cover-open");
    /// <summary>
    /// Wrapper Empty printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperEmpty = new("wrapper-empty");
    /// <summary>
    /// Wrapper Full printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperFull = new("wrapper-full");
    /// <summary>
    /// Wrapper Interlock Closed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperInterlockClosed = new("wrapper-interlock-closed");
    /// <summary>
    /// Wrapper Interlock Open printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperInterlockOpen = new("wrapper-interlock-open");
    /// <summary>
    /// Wrapper Jam printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperJam = new("wrapper-jam");
    /// <summary>
    /// Wrapper Life Almost Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperLifeAlmostOver = new("wrapper-life-almost-over");
    /// <summary>
    /// Wrapper Life Over printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperLifeOver = new("wrapper-life-over");
    /// <summary>
    /// Wrapper Memory Exhausted printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperMemoryExhausted = new("wrapper-memory-exhausted");
    /// <summary>
    /// Wrapper Missing printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperMissing = new("wrapper-missing");
    /// <summary>
    /// Wrapper Motor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperMotorFailure = new("wrapper-motor-failure");
    /// <summary>
    /// Wrapper Near Limit printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperNearLimit = new("wrapper-near-limit");
    /// <summary>
    /// Wrapper Offline printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperOffline = new("wrapper-offline");
    /// <summary>
    /// Wrapper Opened printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperOpened = new("wrapper-opened");
    /// <summary>
    /// Wrapper Over Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperOverTemperature = new("wrapper-over-temperature");
    /// <summary>
    /// Wrapper Power Saver printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperPowerSaver = new("wrapper-power-saver");
    /// <summary>
    /// Wrapper Recoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperRecoverableFailure = new("wrapper-recoverable-failure");
    /// <summary>
    /// Wrapper Recoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperRecoverableStorageError = new("wrapper-recoverable-storage-error");
    /// <summary>
    /// Wrapper Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperRemoved = new("wrapper-removed");
    /// <summary>
    /// Wrapper Resource Added printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperResourceAdded = new("wrapper-resource-added");
    /// <summary>
    /// Wrapper Resource Removed printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperResourceRemoved = new("wrapper-resource-removed");
    /// <summary>
    /// Wrapper Thermistor Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperThermistorFailure = new("wrapper-thermistor-failure");
    /// <summary>
    /// Wrapper Timing Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperTimingFailure = new("wrapper-timing-failure");
    /// <summary>
    /// Wrapper Turned Off printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperTurnedOff = new("wrapper-turned-off");
    /// <summary>
    /// Wrapper Turned On printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperTurnedOn = new("wrapper-turned-on");
    /// <summary>
    /// Wrapper Under Temperature printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperUnderTemperature = new("wrapper-under-temperature");
    /// <summary>
    /// Wrapper Unrecoverable Failure printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperUnrecoverableFailure = new("wrapper-unrecoverable-failure");
    /// <summary>
    /// Wrapper Unrecoverable Storage Error printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperUnrecoverableStorageError = new("wrapper-unrecoverable-storage-error");
    /// <summary>
    /// Wrapper Warming Up printer state reason.
    /// See: PWG 5100.9-2009 Section 8.1
    /// </summary>
    public static readonly PrinterStateReason WrapperWarmingUp = new("wrapper-warming-up");

    // PWG 5100.13-2023 Section 9.3 registered printer-state-reasons keywords.

    /// <summary>
    /// A cleaning component (corresponding to Printer MIB prtMarkerSuppliesType values cleanerUnit(18) and fuserCleaningPad(19)) is nearing the end of its service life.
    /// See: PWG 5100.13-2023 Section 9.3
    /// </summary>
    public static readonly PrinterStateReason CleanerLifeAlmostOver = new("cleaner-life-almost-over");

    /// <summary>
    /// A cleaning component (corresponding to Printer MIB prtMarkerSuppliesType values cleanerUnit(18) and fuserCleaningPad(19)) has reached the end of its service life.
    /// See: PWG 5100.13-2023 Section 9.3
    /// </summary>
    public static readonly PrinterStateReason CleanerLifeOver = new("cleaner-life-over");

    // PWG 5100.22-2025 Section 9.3 registered printer-state-reasons keywords.

    /// <summary>
    /// The Printer has been deleted from the System.
    /// See: PWG 5100.22-2025 Section 9.3
    /// </summary>
    public static readonly PrinterStateReason Deleted = new("deleted");

    /// <summary>
    /// The Printer is processing a Resume-Printer request.
    /// See: PWG 5100.22-2025 Section 9.3
    /// </summary>
    public static readonly PrinterStateReason Resuming = new("resuming");

    /// <summary>
    /// The Printer is being tested.
    /// See: PWG 5100.22-2025 Section 9.3
    /// </summary>
    public static readonly PrinterStateReason Testing = new("testing");

    public override string ToString() => Value;
    public static implicit operator string(PrinterStateReason bin) => bin.Value;
    public static implicit operator PrinterStateReason(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
