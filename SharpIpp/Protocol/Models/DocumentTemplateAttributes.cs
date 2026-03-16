using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// PWG 5100.5-2024 Section 6.3
/// Document Template attributes that override Job Template attributes for a specific Document.
/// </summary>
public class DocumentTemplateAttributes : IIppCollection
{
    public bool IsNoValue { get; set; }
    public int? Copies { get; set; }
    public Cover? CoverBack { get; set; }
    public Cover? CoverFront { get; set; }
    public Finishings? Finishings { get; set; }
    public FinishingsCol[]? FinishingsCol { get; set; }
    public int[]? ForceFrontSide { get; set; }
    public ImpositionTemplate? ImpositionTemplate { get; set; }
    public Media? Media { get; set; }
    public MediaCol? MediaCol { get; set; }
    public MediaInputTrayCheck? MediaInputTrayCheck { get; set; }
    public int? NumberUp { get; set; }
    public Orientation? OrientationRequested { get; set; }
    public OutputBin? OutputBin { get; set; }
    public PageDelivery? PageDelivery { get; set; }
    public PageOrderReceived? PageOrderReceived { get; set; }
    public Range[]? PageRanges { get; set; }
    public PresentationDirectionNumberUp? PresentationDirectionNumberUp { get; set; }
    public PrintQuality? PrintQuality { get; set; }
    public Resolution? PrinterResolution { get; set; }
    public Sides? Sides { get; set; }
    public XImagePosition? XImagePosition { get; set; }
    public int? XImageShift { get; set; }
    public int? XSide1ImageShift { get; set; }
    public int? XSide2ImageShift { get; set; }
    public YImagePosition? YImagePosition { get; set; }
    public int? YImageShift { get; set; }
    public int? YSide1ImageShift { get; set; }
    public int? YSide2ImageShift { get; set; }
}
