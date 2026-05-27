namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media attribute.
/// See: PWG 5101.1
/// See: RFC 8011 Section 5.2.11
/// </summary>
public readonly record struct Media(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The Printer selects the media from the available choices.
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    public static readonly Media Choice = new("choice");

    /// <summary>
    /// The Printer uses its default media.
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    public static readonly Media Default = new("default");

    /// <summary>
    /// ISO A4 media, 210 mm × 297 mm.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media IsoA4210x297mm = new("iso_a4_210x297mm");

    /// <summary>
    /// North American Letter media, 8.5 in × 11 in.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media NaLetter85x11in = new("na_letter_8.5x11in");

    /// <summary>
    /// North American Legal media, 8.5 in × 14 in.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media NaLegal85x14in = new("na_legal_8.5x14in");

    /// <summary>
    /// ISO A3 media, 297 mm × 420 mm.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media IsoA3297x420mm = new("iso_a3_297x420mm");

    /// <summary>
    /// North American index card media, 3 in × 5 in.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media NaIndex3x5in = new("na_index-3x5_3x5in");

    /// <summary>
    /// North American index card media, 4 in × 6 in.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media NaIndex4x6in = new("na_index-4x6_4x6in");

    /// <summary>
    /// North American index card media, 5 in × 8 in.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media NaIndex5x8in = new("na_index-5x8_5x8in");

    /// <summary>
    /// North American Number 10 envelope media, 4.125 in × 9.5 in.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media NaNumber10Envelope4125x95in = new("na_number-10-envelope_4.125x9.5in");

    /// <summary>
    /// A choice between ISO A4 (210 mm × 297 mm) and North American Letter (8.5 in × 11 in).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly Media ChoiceIsoA4210x297mmNaLetter85x11in = new("choice_iso_a4_210x297mm_na_letter_8.5x11in");

    public override string ToString() => Value;
    public static implicit operator string(Media bin) => bin.Value;
    public static implicit operator Media(string value) => new(value);
}
