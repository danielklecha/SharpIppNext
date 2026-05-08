namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-calendar-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.20
/// </summary>
public class PowerCalendarPolicy : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public int? CalendarId { get; set; }
    public int? DayOfMonth { get; set; }
    public int? DayOfWeek { get; set; }
    public int? Hour { get; set; }
    public int? Minute { get; set; }
    public int? Month { get; set; }
    public PowerState? RequestPowerState { get; set; }
    public bool? RunOnce { get; set; }
}
