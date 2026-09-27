using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-calendar-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.20
/// </summary>
[IppAttribute(IppAttributeNames.PowerCalendarPolicyCol)]
public class PowerCalendarPolicy : IIppCollection
{

    public IppValue<int>? CalendarId { get; set; }
    public IppValue<int>? DayOfMonth { get; set; }
    public IppValue<int>? DayOfWeek { get; set; }
    public IppValue<int>? Hour { get; set; }
    public IppValue<int>? Minute { get; set; }
    public IppValue<int>? Month { get; set; }
    public IppValue<PowerState>? RequestPowerState { get; set; }
    public IppValue<bool>? RunOnce { get; set; }
}
