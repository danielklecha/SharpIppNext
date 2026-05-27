namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies which printers should be returned.
/// See: PWG 5100.22-2025 Section 7.1.27
/// </summary>
public readonly record struct WhichPrinters(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// Return all printers regardless of state.
    /// See: PWG 5100.22-2025 Section 7.1.27
    /// </summary>
    public static readonly WhichPrinters All = new("all");

    /// <summary>
    /// Return printers that are not in a completed state.
    /// See: PWG 5100.22-2025 Section 7.1.27
    /// </summary>
    public static readonly WhichPrinters NotCompleted = new("not-completed");

    /// <summary>
    /// Return printers that are in a completed state.
    /// See: PWG 5100.22-2025 Section 7.1.27
    /// </summary>
    public static readonly WhichPrinters Completed = new("completed");

    public override string ToString() => Value;
    public static implicit operator string(WhichPrinters value) => value.Value;
    public static implicit operator WhichPrinters(string value) => new(value);
}
