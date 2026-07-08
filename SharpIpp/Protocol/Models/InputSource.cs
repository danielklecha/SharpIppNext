namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-source</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.19
/// </summary>
public readonly record struct InputSource(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The automatic document feeder (ADF) input source.
    /// See: PWG 5100.15-2013 Section 7.1.1.19
    /// </summary>
    public static readonly InputSource Adf = new("adf");

    /// <summary>
    /// The back side of the automatic document feeder (ADF) input source.
    /// See: PWG 5100.15-2013 Section 7.1.1.19
    /// </summary>
    public static readonly InputSource AdfBack = new("adf-back");

    /// <summary>
    /// The duplex automatic document feeder (ADF) input source.
    /// See: PWG 5100.15-2013 Section 7.1.1.19
    /// </summary>
    public static readonly InputSource AdfDuplex = new("adf-duplex");

    /// <summary>
    /// The film reader input source.
    /// See: PWG 5100.15-2013 Section 7.1.1.19
    /// </summary>
    public static readonly InputSource FilmReader = new("film-reader");

    /// <summary>
    /// The flatbed platen input source.
    /// See: PWG 5100.15-2013 Section 7.1.1.19
    /// </summary>
    public static readonly InputSource Platen = new("platen");

    public override string ToString() => Value;
    public static implicit operator string(InputSource value) => value.Value;
    public static implicit operator InputSource(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
