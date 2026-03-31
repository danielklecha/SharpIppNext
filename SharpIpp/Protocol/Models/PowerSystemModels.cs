using System;

namespace SharpIpp.Protocol.Models;

public class PowerCalendarPolicy : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? CalendarId { get; set; }
    public int? DayOfMonth { get; set; }
    public int? DayOfWeek { get; set; }
    public int? Hour { get; set; }
    public int? Minute { get; set; }
    public int? Month { get; set; }
    public PowerState? RequestPowerState { get; set; }
    public bool? RunOnce { get; set; }
}

public class PowerEventPolicy : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? EventId { get; set; }
    public string? EventName { get; set; }
    public PowerState? RequestPowerState { get; set; }
}

public class PowerTimeoutPolicy : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public PowerState? RequestPowerState { get; set; }
    public PowerState? StartPowerState { get; set; }
    public int? TimeoutId { get; set; }
    public string? TimeoutPredicate { get; set; }
    public int? TimeoutSeconds { get; set; }
}

public class PowerStateCapability : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public bool? CanAcceptJobs { get; set; }
    public bool? CanProcessJobs { get; set; }
    public int? PowerActiveWatts { get; set; }
    public int? PowerInactiveWatts { get; set; }
    public PowerState? PowerState { get; set; }
}

public class PowerStateCounter : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? HibernateTransitions { get; set; }
    public int? OnTransitions { get; set; }
    public int? StandbyTransitions { get; set; }
    public int? SuspendTransitions { get; set; }
}

public class PowerStateMonitor : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? CurrentMonthKwh { get; set; }
    public int? CurrentWatts { get; set; }
    public int? LifetimeKwh { get; set; }
    public bool? MetersAreActual { get; set; }
    public PowerState? PowerState { get; set; }
    public string? PowerStateMessage { get; set; }
    public bool? PowerUsageIsRmsWatts { get; set; }
    public IppOperation[]? ValidRequestPowerStates { get; set; }
}

public class PowerStateTransition : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public PowerState? EndPowerState { get; set; }
    public PowerState? StartPowerState { get; set; }
    public int? StateTransitionSeconds { get; set; }
}

public class SystemConfiguredPrinter : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? PrinterId { get; set; }
    public string? PrinterInfo { get; set; }
    public bool? PrinterIsAcceptingJobs { get; set; }
    public string? PrinterName { get; set; }
    public PrinterServiceType? PrinterServiceType { get; set; }
    public PrinterState? PrinterState { get; set; }
    public string[]? PrinterStateReasons { get; set; }
    public SystemXri[]? PrinterXriSupported { get; set; }
}

public class PowerLogEntry : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? LogId { get; set; }
    public PowerState? PowerState { get; set; }
    public DateTimeOffset? DateTimeAt { get; set; }
    public string? Message { get; set; }
}

public class SystemConfiguredResource : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public string? ResourceFormat { get; set; }
    public int? ResourceId { get; set; }
    public string? ResourceInfo { get; set; }
    public string? ResourceName { get; set; }
    public ResourceState? ResourceState { get; set; }
    public ResourceStateReason[]? ResourceStateReasons { get; set; }
    public string? ResourceType { get; set; }
}
