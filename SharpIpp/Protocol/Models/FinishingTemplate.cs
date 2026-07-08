namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the finishing-template attribute.
/// See: PWG 5100.1
/// </summary>
public readonly record struct FinishingTemplate(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum 
{
    /// <summary>No finishing is applied. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate None = new("none");
    /// <summary>Bind the document with one or more staples. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Staple = new("staple");
    /// <summary>Punch holes in the document. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Punch = new("punch");
    /// <summary>Add a cover to the document. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Cover = new("cover");
    /// <summary>Bind the document. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Bind = new("bind");
    /// <summary>Bind the document with saddle stitching along the middle fold. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate SaddleStitch = new("saddle-stitch");
    /// <summary>Bind the document with edge stitching along one edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate EdgeStitch = new("edge-stitch");
    /// <summary>Staple in the top-left corner. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleTopLeft = new("staple-top-left");
    /// <summary>Staple in the bottom-left corner. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleBottomLeft = new("staple-bottom-left");
    /// <summary>Staple in the top-right corner. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleTopRight = new("staple-top-right");
    /// <summary>Staple in the bottom-right corner. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleBottomRight = new("staple-bottom-right");
    /// <summary>Edge stitch along the left edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate EdgeStitchLeft = new("edge-stitch-left");
    /// <summary>Edge stitch along the top edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate EdgeStitchTop = new("edge-stitch-top");
    /// <summary>Edge stitch along the right edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate EdgeStitchRight = new("edge-stitch-right");
    /// <summary>Edge stitch along the bottom edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate EdgeStitchBottom = new("edge-stitch-bottom");
    /// <summary>Two staples along the left edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleDualLeft = new("staple-dual-left");
    /// <summary>Two staples along the top edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleDualTop = new("staple-dual-top");
    /// <summary>Two staples along the right edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleDualRight = new("staple-dual-right");
    /// <summary>Two staples along the bottom edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleDualBottom = new("staple-dual-bottom");
    /// <summary>Three staples along the left edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleTripleLeft = new("staple-triple-left");
    /// <summary>Three staples along the top edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleTripleTop = new("staple-triple-top");
    /// <summary>Three staples along the right edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleTripleRight = new("staple-triple-right");
    /// <summary>Three staples along the bottom edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate StapleTripleBottom = new("staple-triple-bottom");
    /// <summary>Trim the hardcopy output on one or more edges. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Trim = new("trim");
    /// <summary>Fold the hardcopy output. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Fold = new("fold");
    /// <summary>Bale the document. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Bale = new("bale");
    /// <summary>Deliver the document to the signature booklet maker. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate BookletMaker = new("booklet-maker");
    /// <summary>Apply a protective solid material to each sheet. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Laminate = new("laminate");
    /// <summary>Apply a protective liquid or powdered coating to each sheet. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate Coat = new("coat");
    /// <summary>Shift each set from the previous one by a small amount. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JogOffset = new("jog-offset");
    /// <summary>Bind the document along the left edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate BindLeft = new("bind-left");
    /// <summary>Bind the document along the top edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate BindTop = new("bind-top");
    /// <summary>Bind the document along the right edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate BindRight = new("bind-right");
    /// <summary>Bind the document along the bottom edge. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate BindBottom = new("bind-bottom");
    /// <summary>Trim output after each page. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate TrimAfterPages = new("trim-after-pages");
    /// <summary>Trim output after each document. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate TrimAfterDocuments = new("trim-after-documents");
    /// <summary>Trim output after each set of copies. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate TrimAfterCopies = new("trim-after-copies");
    /// <summary>Trim output after the job. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate TrimAfterJob = new("trim-after-job");
    /// <summary>Punch a single hole in the top-left. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchTopLeft = new("punch-top-left");
    /// <summary>Punch a single hole in the bottom-left. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchBottomLeft = new("punch-bottom-left");
    /// <summary>Punch a single hole in the top-right. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchTopRight = new("punch-top-right");
    /// <summary>Punch a single hole in the bottom-right. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchBottomRight = new("punch-bottom-right");
    /// <summary>Punch two holes on the left side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchDualLeft = new("punch-dual-left");
    /// <summary>Punch two holes at the top. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchDualTop = new("punch-dual-top");
    /// <summary>Punch two holes on the right side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchDualRight = new("punch-dual-right");
    /// <summary>Punch two holes at the bottom. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchDualBottom = new("punch-dual-bottom");
    /// <summary>Punch three holes on the left side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchTripleLeft = new("punch-triple-left");
    /// <summary>Punch three holes at the top. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchTripleTop = new("punch-triple-top");
    /// <summary>Punch three holes on the right side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchTripleRight = new("punch-triple-right");
    /// <summary>Punch three holes at the bottom. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchTripleBottom = new("punch-triple-bottom");
    /// <summary>Punch four holes on the left side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchQuadLeft = new("punch-quad-left");
    /// <summary>Punch four holes at the top. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchQuadTop = new("punch-quad-top");
    /// <summary>Punch four holes on the right side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchQuadRight = new("punch-quad-right");
    /// <summary>Punch four holes at the bottom. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchQuadBottom = new("punch-quad-bottom");
    /// <summary>Punch more than four holes on the left side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchMultipleLeft = new("punch-multiple-left");
    /// <summary>Punch more than four holes at the top. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchMultipleTop = new("punch-multiple-top");
    /// <summary>Punch more than four holes on the right side. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchMultipleRight = new("punch-multiple-right");
    /// <summary>Punch more than four holes at the bottom. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate PunchMultipleBottom = new("punch-multiple-bottom");
    /// <summary>Accordion-fold the output vertically into four sections. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldAccordion = new("fold-accordion");
    /// <summary>Fold the top and bottom quarters toward the midline, then fold in half vertically. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldDoubleGate = new("fold-double-gate");
    /// <summary>Fold the top and bottom quarters toward the midline. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldGate = new("fold-gate");
    /// <summary>Fold the output in half vertically. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldHalf = new("fold-half");
    /// <summary>Fold the output in half horizontally, then Z-fold vertically into three sections. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldHalfZ = new("fold-half-z");
    /// <summary>Fold the top quarter toward the midline. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldLeftGate = new("fold-left-gate");
    /// <summary>Fold the output into three sections vertically (C fold). See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldLetter = new("fold-letter");
    /// <summary>Fold the output in half vertically two times, yielding four sections. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldParallel = new("fold-parallel");
    /// <summary>Fold the output in half horizontally and vertically (cross fold). See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldPoster = new("fold-poster");
    /// <summary>Fold the bottom quarter toward the midline. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldRightGate = new("fold-right-gate");
    /// <summary>Fold the output vertically into three sections forming a Z. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldZ = new("fold-z");
    /// <summary>Fold the output vertically into three sections forming a Z with room for binding. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate FoldEngineeringZ = new("fold-engineering-z");
    /// <summary>JDF fold template F2-1 (2-panel fold). See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF2_1 = new("jdf-f2-1");
    /// <summary>JDF fold template jdf-f4-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF4_1 = new("jdf-f4-1");
    /// <summary>JDF fold template jdf-f4-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF4_2 = new("jdf-f4-2");
    /// <summary>JDF fold template jdf-f6-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_1 = new("jdf-f6-1");
    /// <summary>JDF fold template jdf-f6-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_2 = new("jdf-f6-2");
    /// <summary>JDF fold template jdf-f6-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_3 = new("jdf-f6-3");
    /// <summary>JDF fold template jdf-f6-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_4 = new("jdf-f6-4");
    /// <summary>JDF fold template jdf-f6-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_5 = new("jdf-f6-5");
    /// <summary>JDF fold template jdf-f6-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_6 = new("jdf-f6-6");
    /// <summary>JDF fold template jdf-f6-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_7 = new("jdf-f6-7");
    /// <summary>JDF fold template jdf-f6-8. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF6_8 = new("jdf-f6-8");
    /// <summary>JDF fold template jdf-f8-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_1 = new("jdf-f8-1");
    /// <summary>JDF fold template jdf-f8-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_2 = new("jdf-f8-2");
    /// <summary>JDF fold template jdf-f8-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_3 = new("jdf-f8-3");
    /// <summary>JDF fold template jdf-f8-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_4 = new("jdf-f8-4");
    /// <summary>JDF fold template jdf-f8-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_5 = new("jdf-f8-5");
    /// <summary>JDF fold template jdf-f8-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_6 = new("jdf-f8-6");
    /// <summary>JDF fold template jdf-f8-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF8_7 = new("jdf-f8-7");
    /// <summary>JDF fold template jdf-f10-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF10_1 = new("jdf-f10-1");
    /// <summary>JDF fold template jdf-f10-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF10_2 = new("jdf-f10-2");
    /// <summary>JDF fold template jdf-f10-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF10_3 = new("jdf-f10-3");
    /// <summary>JDF fold template jdf-f12-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_1 = new("jdf-f12-1");
    /// <summary>JDF fold template jdf-f12-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_2 = new("jdf-f12-2");
    /// <summary>JDF fold template jdf-f12-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_3 = new("jdf-f12-3");
    /// <summary>JDF fold template jdf-f12-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_4 = new("jdf-f12-4");
    /// <summary>JDF fold template jdf-f12-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_5 = new("jdf-f12-5");
    /// <summary>JDF fold template jdf-f12-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_6 = new("jdf-f12-6");
    /// <summary>JDF fold template jdf-f12-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_7 = new("jdf-f12-7");
    /// <summary>JDF fold template jdf-f12-8. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_8 = new("jdf-f12-8");
    /// <summary>JDF fold template jdf-f12-9. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_9 = new("jdf-f12-9");
    /// <summary>JDF fold template jdf-f12-10. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_10 = new("jdf-f12-10");
    /// <summary>JDF fold template jdf-f12-11. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_11 = new("jdf-f12-11");
    /// <summary>JDF fold template jdf-f12-12. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_12 = new("jdf-f12-12");
    /// <summary>JDF fold template jdf-f12-13. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_13 = new("jdf-f12-13");
    /// <summary>JDF fold template jdf-f12-14. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF12_14 = new("jdf-f12-14");
    /// <summary>JDF fold template jdf-f14-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF14_1 = new("jdf-f14-1");
    /// <summary>JDF fold template jdf-f16-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_1 = new("jdf-f16-1");
    /// <summary>JDF fold template jdf-f16-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_2 = new("jdf-f16-2");
    /// <summary>JDF fold template jdf-f16-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_3 = new("jdf-f16-3");
    /// <summary>JDF fold template jdf-f16-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_4 = new("jdf-f16-4");
    /// <summary>JDF fold template jdf-f16-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_5 = new("jdf-f16-5");
    /// <summary>JDF fold template jdf-f16-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_6 = new("jdf-f16-6");
    /// <summary>JDF fold template jdf-f16-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_7 = new("jdf-f16-7");
    /// <summary>JDF fold template jdf-f16-8. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_8 = new("jdf-f16-8");
    /// <summary>JDF fold template jdf-f16-9. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_9 = new("jdf-f16-9");
    /// <summary>JDF fold template jdf-f16-10. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_10 = new("jdf-f16-10");
    /// <summary>JDF fold template jdf-f16-11. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_11 = new("jdf-f16-11");
    /// <summary>JDF fold template jdf-f16-12. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_12 = new("jdf-f16-12");
    /// <summary>JDF fold template jdf-f16-13. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_13 = new("jdf-f16-13");
    /// <summary>JDF fold template jdf-f16-14. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF16_14 = new("jdf-f16-14");
    /// <summary>JDF fold template jdf-f18-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_1 = new("jdf-f18-1");
    /// <summary>JDF fold template jdf-f18-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_2 = new("jdf-f18-2");
    /// <summary>JDF fold template jdf-f18-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_3 = new("jdf-f18-3");
    /// <summary>JDF fold template jdf-f18-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_4 = new("jdf-f18-4");
    /// <summary>JDF fold template jdf-f18-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_5 = new("jdf-f18-5");
    /// <summary>JDF fold template jdf-f18-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_6 = new("jdf-f18-6");
    /// <summary>JDF fold template jdf-f18-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_7 = new("jdf-f18-7");
    /// <summary>JDF fold template jdf-f18-8. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_8 = new("jdf-f18-8");
    /// <summary>JDF fold template jdf-f18-9. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF18_9 = new("jdf-f18-9");
    /// <summary>JDF fold template jdf-f20-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF20_1 = new("jdf-f20-1");
    /// <summary>JDF fold template jdf-f20-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF20_2 = new("jdf-f20-2");
    /// <summary>JDF fold template jdf-f24-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_1 = new("jdf-f24-1");
    /// <summary>JDF fold template jdf-f24-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_2 = new("jdf-f24-2");
    /// <summary>JDF fold template jdf-f24-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_3 = new("jdf-f24-3");
    /// <summary>JDF fold template jdf-f24-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_4 = new("jdf-f24-4");
    /// <summary>JDF fold template jdf-f24-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_5 = new("jdf-f24-5");
    /// <summary>JDF fold template jdf-f24-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_6 = new("jdf-f24-6");
    /// <summary>JDF fold template jdf-f24-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_7 = new("jdf-f24-7");
    /// <summary>JDF fold template jdf-f24-8. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_8 = new("jdf-f24-8");
    /// <summary>JDF fold template jdf-f24-9. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_9 = new("jdf-f24-9");
    /// <summary>JDF fold template jdf-f24-10. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_10 = new("jdf-f24-10");
    /// <summary>JDF fold template jdf-f24-11. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF24_11 = new("jdf-f24-11");
    /// <summary>JDF fold template jdf-f28-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF28_1 = new("jdf-f28-1");
    /// <summary>JDF fold template jdf-f32-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_1 = new("jdf-f32-1");
    /// <summary>JDF fold template jdf-f32-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_2 = new("jdf-f32-2");
    /// <summary>JDF fold template jdf-f32-3. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_3 = new("jdf-f32-3");
    /// <summary>JDF fold template jdf-f32-4. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_4 = new("jdf-f32-4");
    /// <summary>JDF fold template jdf-f32-5. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_5 = new("jdf-f32-5");
    /// <summary>JDF fold template jdf-f32-6. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_6 = new("jdf-f32-6");
    /// <summary>JDF fold template jdf-f32-7. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_7 = new("jdf-f32-7");
    /// <summary>JDF fold template jdf-f32-8. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_8 = new("jdf-f32-8");
    /// <summary>JDF fold template jdf-f32-9. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF32_9 = new("jdf-f32-9");
    /// <summary>JDF fold template jdf-f36-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF36_1 = new("jdf-f36-1");
    /// <summary>JDF fold template jdf-f36-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF36_2 = new("jdf-f36-2");
    /// <summary>JDF fold template jdf-f40-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF40_1 = new("jdf-f40-1");
    /// <summary>JDF fold template jdf-f48-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF48_1 = new("jdf-f48-1");
    /// <summary>JDF fold template jdf-f48-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF48_2 = new("jdf-f48-2");
    /// <summary>JDF fold template jdf-f64-1. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF64_1 = new("jdf-f64-1");
    /// <summary>JDF fold template jdf-f64-2. See: PWG 5100.1</summary>
    public static readonly FinishingTemplate JdfF64_2 = new("jdf-f64-2");

    public override string ToString() => Value;
    public static implicit operator string(FinishingTemplate bin) => bin.Value;
    public static implicit operator FinishingTemplate(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
