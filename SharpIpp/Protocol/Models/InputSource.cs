namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-source</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.19
/// </summary>
public readonly record struct InputSource(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly InputSource Adf = new("adf");
    public static readonly InputSource AdfBack = new("adf-back");
    public static readonly InputSource AdfDuplex = new("adf-duplex");
    public static readonly InputSource FilmReader = new("film-reader");
    public static readonly InputSource Platen = new("platen");

    public override string ToString() => Value;
    public static implicit operator string(InputSource value) => value.Value;
    public static explicit operator InputSource(string value) => new(value);
}
