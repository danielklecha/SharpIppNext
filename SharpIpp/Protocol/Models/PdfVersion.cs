namespace SharpIpp.Protocol.Models;
public readonly record struct PdfVersion(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly PdfVersion None = new("none");
    public static readonly PdfVersion Adobe13 = new("adobe-1.3");
    public static readonly PdfVersion Adobe14 = new("adobe-1.4");
    public static readonly PdfVersion Adobe15 = new("adobe-1.5");
    public static readonly PdfVersion Adobe16 = new("adobe-1.6");
    public static readonly PdfVersion Adobe17 = new("adobe-1.7");
    public static readonly PdfVersion Iso1593012001 = new("iso-15930-1_2001");
    public static readonly PdfVersion Iso1593032002 = new("iso-15930-3_2002");
    public static readonly PdfVersion Iso1593042003 = new("iso-15930-4_2003");
    public static readonly PdfVersion Iso1593062003 = new("iso-15930-6_2003");
    public static readonly PdfVersion Iso1593072010 = new("iso-15930-7_2010");
    public static readonly PdfVersion Iso1593082010 = new("iso-15930-8_2010");
    public static readonly PdfVersion Iso1661222010 = new("iso-16612-2_2010");
    public static readonly PdfVersion Iso1900512005 = new("iso-19005-1_2005");
    public static readonly PdfVersion Iso1900522011 = new("iso-19005-2_2011");
    public static readonly PdfVersion Iso1900532012 = new("iso-19005-3_2012");
    public static readonly PdfVersion Iso2350412020 = new("iso-23504-1_2020");
    public static readonly PdfVersion Iso3200012008 = new("iso-32000-1_2008");
    public static readonly PdfVersion Iso3200022017 = new("iso-32000-2_2017");
    public static readonly PdfVersion Pwg51023 = new("pwg-5102.3");

    public override string ToString() => Value;
    public static implicit operator string(PdfVersion value) => value.Value;
    public static explicit operator PdfVersion(string value) => new(value);
}
