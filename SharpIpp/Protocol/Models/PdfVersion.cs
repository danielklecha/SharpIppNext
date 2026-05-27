namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <c>pdf-versions-supported</c>.
/// See: PWG 5100.13-2023 Section 6.5.5
/// </summary>
public readonly record struct PdfVersion(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>No specific PDF version is required. See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion None = new("none");
    /// <summary>Adobe PDF 1.3. See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Adobe13 = new("adobe-1.3");
    /// <summary>Adobe PDF 1.4. See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Adobe14 = new("adobe-1.4");
    /// <summary>Adobe PDF 1.5. See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Adobe15 = new("adobe-1.5");
    /// <summary>Adobe PDF 1.6. See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Adobe16 = new("adobe-1.6");
    /// <summary>Adobe PDF 1.7. See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Adobe17 = new("adobe-1.7");
    /// <summary>ISO 15930-1:2001 (PDF/X-1a). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1593012001 = new("iso-15930-1_2001");
    /// <summary>ISO 15930-3:2002 (PDF/X-3). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1593032002 = new("iso-15930-3_2002");
    /// <summary>ISO 15930-4:2003 (PDF/X-1a:2003). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1593042003 = new("iso-15930-4_2003");
    /// <summary>ISO 15930-6:2003 (PDF/X-3:2003). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1593062003 = new("iso-15930-6_2003");
    /// <summary>ISO 15930-7:2010 (PDF/X-4). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1593072010 = new("iso-15930-7_2010");
    /// <summary>ISO 15930-8:2010 (PDF/X-5). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1593082010 = new("iso-15930-8_2010");
    /// <summary>ISO 16612-2:2010 (PDF/VT). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1661222010 = new("iso-16612-2_2010");
    /// <summary>ISO 19005-1:2005 (PDF/A-1). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1900512005 = new("iso-19005-1_2005");
    /// <summary>ISO 19005-2:2011 (PDF/A-2). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1900522011 = new("iso-19005-2_2011");
    /// <summary>ISO 19005-3:2012 (PDF/A-3). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso1900532012 = new("iso-19005-3_2012");
    /// <summary>ISO 23504-1:2020 (PDF/R). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso2350412020 = new("iso-23504-1_2020");
    /// <summary>ISO 32000-1:2008 (PDF 1.7). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso3200012008 = new("iso-32000-1_2008");
    /// <summary>ISO 32000-2:2017 (PDF 2.0). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Iso3200022017 = new("iso-32000-2_2017");
    /// <summary>PWG 5102.3 (PWG Raster Format). See: PWG 5100.13-2023 Section 6.5.5</summary>
    public static readonly PdfVersion Pwg51023 = new("pwg-5102.3");

    public override string ToString() => Value;
    public static implicit operator string(PdfVersion value) => value.Value;
    public static implicit operator PdfVersion(string value) => new(value);
}
