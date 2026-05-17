using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-source attribute, identifying the input tray or feed source for media.
/// See: PWG 5100.13-2023 Section 6.2.19
/// </summary>
public readonly record struct MediaSource(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer alternates between two input sources.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Alternate = new("alternate");

    /// <summary>
    /// The Printer alternates between two roll input sources.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource AlternateRoll = new("alternate-roll");

    /// <summary>
    /// The Printer automatically selects the media source.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Auto = new("auto");

    /// <summary>
    /// The bottom input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Bottom = new("bottom");

    /// <summary>
    /// The bypass (multi-purpose) tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource ByPassTray = new("by-pass-tray");

    /// <summary>
    /// The center input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Center = new("center");

    /// <summary>
    /// The disc input source.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Disc = new("disc");

    /// <summary>
    /// The envelope input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Envelope = new("envelope");

    /// <summary>
    /// The hagaki (Japanese postcard) input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Hagaki = new("hagaki");

    /// <summary>
    /// The large-capacity input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource LargeCapacity = new("large-capacity");

    /// <summary>
    /// The left input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Left = new("left");

    /// <summary>
    /// The main input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Main = new("main");

    /// <summary>
    /// The main roll input source.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource MainRoll = new("main-roll");

    /// <summary>
    /// Manual feed (single-sheet) input.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Manual = new("manual");

    /// <summary>
    /// The middle input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Middle = new("middle");

    /// <summary>
    /// The photo input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Photo = new("photo");

    /// <summary>
    /// The rear input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Rear = new("rear");

    /// <summary>
    /// The right input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Right = new("right");

    /// <summary>
    /// Roll input source number 1.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll1 = new("roll-1");

    /// <summary>
    /// Roll input source number 10.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll10 = new("roll-10");

    /// <summary>
    /// Roll input source number 2.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll2 = new("roll-2");

    /// <summary>
    /// Roll input source number 3.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll3 = new("roll-3");

    /// <summary>
    /// Roll input source number 4.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll4 = new("roll-4");

    /// <summary>
    /// Roll input source number 5.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll5 = new("roll-5");

    /// <summary>
    /// Roll input source number 6.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll6 = new("roll-6");

    /// <summary>
    /// Roll input source number 7.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll7 = new("roll-7");

    /// <summary>
    /// Roll input source number 8.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll8 = new("roll-8");

    /// <summary>
    /// Roll input source number 9.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Roll9 = new("roll-9");

    /// <summary>
    /// The side input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Side = new("side");

    /// <summary>
    /// The top input tray.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Top = new("top");

    /// <summary>
    /// Input tray number 1.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray1 = new("tray-1");

    /// <summary>
    /// Input tray number 10.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray10 = new("tray-10");

    /// <summary>
    /// Input tray number 11.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray11 = new("tray-11");

    /// <summary>
    /// Input tray number 12.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray12 = new("tray-12");

    /// <summary>
    /// Input tray number 13.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray13 = new("tray-13");

    /// <summary>
    /// Input tray number 14.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray14 = new("tray-14");

    /// <summary>
    /// Input tray number 15.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray15 = new("tray-15");

    /// <summary>
    /// Input tray number 16.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray16 = new("tray-16");

    /// <summary>
    /// Input tray number 17.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray17 = new("tray-17");

    /// <summary>
    /// Input tray number 18.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray18 = new("tray-18");

    /// <summary>
    /// Input tray number 19.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray19 = new("tray-19");

    /// <summary>
    /// Input tray number 2.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray2 = new("tray-2");

    /// <summary>
    /// Input tray number 20.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray20 = new("tray-20");

    /// <summary>
    /// Input tray number 3.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray3 = new("tray-3");

    /// <summary>
    /// Input tray number 4.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray4 = new("tray-4");

    /// <summary>
    /// Input tray number 5.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray5 = new("tray-5");

    /// <summary>
    /// Input tray number 6.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray6 = new("tray-6");

    /// <summary>
    /// Input tray number 7.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray7 = new("tray-7");

    /// <summary>
    /// Input tray number 8.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray8 = new("tray-8");

    /// <summary>
    /// Input tray number 9.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Tray9 = new("tray-9");

    /// <summary>
    /// A virtual (software-defined) input source.
    /// See: PWG 5100.13-2023 Section 6.2.19
    /// </summary>
    public static readonly MediaSource Virtual = new("virtual");

    public override string ToString() => Value;
    public static implicit operator string(MediaSource bin) => bin.Value;
    public static explicit operator MediaSource(string value) => new(value);
}
