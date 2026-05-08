namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies which printers should be returned.
/// See: PWG 5100.22-2025 Section 7.1.30
/// </summary>
public readonly record struct WhichPrinters(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly WhichPrinters All = new("all");
    public static readonly WhichPrinters NotCompleted = new("not-completed");
    public static readonly WhichPrinters Completed = new("completed");

    public override string ToString() => Value;
    public static implicit operator string(WhichPrinters value) => value.Value;
    public static explicit operator WhichPrinters(string value) => new(value);
}
