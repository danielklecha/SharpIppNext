namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>presentation-direction-number-up</c> values.
/// See: PWG 5100.3-2023 Section 5.2.15
/// </summary>
public readonly record struct PresentationDirectionNumberUp(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Pages are arranged left-to-right, then top-to-bottom.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp TorightTobottom = new("toright-tobottom");

    /// <summary>
    /// Pages are arranged top-to-bottom, then left-to-right.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp TobottomToright = new("tobottom-toright");

    /// <summary>
    /// Pages are arranged right-to-left, then top-to-bottom.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp ToleftTobottom = new("toleft-tobottom");

    /// <summary>
    /// Pages are arranged top-to-bottom, then right-to-left.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp TobottomToleft = new("tobottom-toleft");

    /// <summary>
    /// Pages are arranged left-to-right, then bottom-to-top.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp TorightTotop = new("toright-totop");

    /// <summary>
    /// Pages are arranged bottom-to-top, then left-to-right.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp TotopToright = new("totop-toright");

    /// <summary>
    /// Pages are arranged right-to-left, then bottom-to-top.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp ToleftTotop = new("toleft-totop");

    /// <summary>
    /// Pages are arranged bottom-to-top, then right-to-left.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public static readonly PresentationDirectionNumberUp TotopToleft = new("totop-toleft");

    public override string ToString() => Value;
    public static implicit operator string(PresentationDirectionNumberUp bin) => bin.Value;
    public static implicit operator PresentationDirectionNumberUp(string value) => new(value);
}
