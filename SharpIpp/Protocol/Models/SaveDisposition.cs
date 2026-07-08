namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>save-disposition</c> member attribute.
/// See: PWG 5100.11 (obsolete Job Save and Reprint)
/// </summary>
public readonly record struct SaveDisposition(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly SaveDisposition None = new("none");
    public static readonly SaveDisposition SaveOnly = new("save-only");
    public static readonly SaveDisposition PrintSave = new("print-save");

    public override string ToString() => Value;
    public static implicit operator string(SaveDisposition value) => value.Value;
    public static implicit operator SaveDisposition(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
