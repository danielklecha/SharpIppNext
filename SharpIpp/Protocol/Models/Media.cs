namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media attribute.
/// See: PWG 5101.1
/// </summary>
public readonly record struct Media(string Value)
{
    public static readonly Media Choice = new("choice");
    public static readonly Media Default = new("default");
    public static readonly Media IsoA4210x297mm = new("iso_a4_210x297mm");
    public static readonly Media NaLetter85x11in = new("na_letter_8.5x11in");
    public static readonly Media NaLegal85x14in = new("na_legal_8.5x14in");
    public static readonly Media IsoA3297x420mm = new("iso_a3_297x420mm");
    public static readonly Media NaIndex3x5in = new("na_index-3x5_3x5in");
    public static readonly Media NaIndex4x6in = new("na_index-4x6_4x6in");
    public static readonly Media NaIndex5x8in = new("na_index-5x8_5x8in");
    public static readonly Media NaNumber10Envelope4125x95in = new("na_number-10-envelope_4.125x9.5in");
    public static readonly Media ChoiceIsoA4210x297mmNaLetter85x11in = new("choice_iso_a4_210x297mm_na_letter_8.5x11in");

    public override string ToString() => Value;
    public static implicit operator string(Media bin) => bin.Value;
    public static explicit operator Media(string value) => new(value);
}
