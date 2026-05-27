namespace SharpIpp.Protocol.Models;

/// <summary>
/// Identifies the format of the supplied document data.
/// </summary>
public readonly record struct DocumentFormat(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>
    /// application/octet-stream
    /// </summary>
    public static readonly DocumentFormat ApplicationOctetStream = new("application/octet-stream");

    /// <summary>
    /// application/pdf
    /// </summary>
    public static readonly DocumentFormat ApplicationPdf = new("application/pdf");

    /// <summary>
    /// application/postscript
    /// </summary>
    public static readonly DocumentFormat ApplicationPostscript = new("application/postscript");

    /// <summary>
    /// image/jpeg
    /// </summary>
    public static readonly DocumentFormat ImageJpeg = new("image/jpeg");

    /// <summary>
    /// image/pwg-raster
    /// </summary>
    public static readonly DocumentFormat ImagePwgRaster = new("image/pwg-raster");

    /// <summary>
    /// image/urf
    /// </summary>
    public static readonly DocumentFormat ImageUrf = new("image/urf");

    /// <summary>
    /// text/plain
    /// </summary>
    public static readonly DocumentFormat TextPlain = new("text/plain");

    public override string ToString() => Value;
    public static implicit operator string(DocumentFormat bin) => bin.Value;
    public static implicit operator DocumentFormat(string value) => new(value);
}
