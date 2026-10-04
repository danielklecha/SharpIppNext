using System;
using System.Collections.Generic;
using SharpIpp.Mapping;
using SharpIpp.Protocol;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Attributes that describe the processing to be applied to a Job or Document.
/// See: RFC 8011
/// See: PWG 5100.1-2022
/// See: PWG 5100.3-2023
/// See: PWG 5100.6-2003
/// See: PWG 5100.7-2023
/// See: PWG 5100.8-2003
/// See: PWG 5100.11-2024
/// See: PWG 5100.13-2023
/// See: PWG 5100.15-2013
/// See: PWG 5100.21-2019
/// </summary>
[IppAttribute]
[IppSection(SectionTag.JobAttributesTag)]
public class JobTemplateAttributes
{
    /// <summary>
    /// This attribute specifies a priority for scheduling the Job. A higher
    /// value specifies a higher priority. The value 1 indicates the lowest
    /// possible priority. The value 100 indicates the highest possible
    /// priority.  Among those jobs that are ready to print, a Printer MUST
    /// print all jobs with a priority value of n before printing those with
    /// a priority value of n-1 for all n.
    /// If the Printer object supports this attribute, it MUST always support
    /// the full range from 1 to 100.  No administrative restrictions are
    /// permitted.  This way an end-user can always make full use of the
    /// entire range with any Printer object.  If privileged jobs are
    /// implemented outside IPP/1.1, they MUST have priorities higher than
    /// 100, rather than restricting the range available to end-users.
    /// If the client does not supply this attribute and this attribute is
    /// supported by the Printer object, the Printer object MUST use the
    /// value of the Printer object's "job-priority-default" at job
    /// submission time (unlike most Job Template attributes that are used if
    /// necessary at job processing time).
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    [Range(1, 100)]
    [IppAttribute(IppAttributeNames.JobPriority, Tag.Integer)]
    public IppValue<int>? JobPriority { get; set; }

    /// <summary>
    /// This attribute specifies the named time period during which the Job
    /// MUST become a candidate for printing.
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntil)]
    public IppValue<JobHoldUntil>? JobHoldUntil { get; set; }

    /// <summary>
    /// This attribute specifies how the Printer handles multiple documents
    /// within a Job.
    /// See: RFC 8011 Section 5.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentHandling)]
    public IppValue<MultipleDocumentHandling>? MultipleDocumentHandling { get; set; }

    /// <summary>
    /// This attribute specifies which job start/end sheet(s) the Printer
    /// uses for the Job.
    /// See: RFC 8011 Section 5.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheets)]
    public IppValue<JobSheets>? JobSheets { get; set; }

    /// <summary>
    /// This attribute augments the "job-sheets" Job Template attribute and allows a Client to specify distinct media.
    /// See: PWG 5100.7-2023 Section 6.8.11
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsCol)]
    public IppValue<JobSheetsCol>? JobSheetsCol { get; set; }

    /// <summary>
    /// This attribute specifies the number of copies to be printed.
    /// On many devices the supported number of collated copies will be
    /// limited by the number of physical output bins on the device, and may
    /// be different from the number of uncollated copies which can be
    /// supported.
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.Copies, Tag.Integer)]
    public IppValue<int>? Copies { get; set; }

    /// <summary>
    /// This attribute identifies the finishing operations that the Printer
    /// uses for each copy of each printed document in the Job. For Jobs with
    /// multiple documents, the "multiple-document-handling" attribute
    /// determines what constitutes a "copy" for purposes of finishing.
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.Finishings, Tag.Enum)]
    public IppValue<Finishings[]>? Finishings { get; set; }

    /// <summary>
    /// This attribute specifies detailed finishing instructions that cannot
    /// be expressed by the "finishings" Job Template attribute.
    /// See: PWG 5100.1-2022 Section 5.2
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsCol)]
    public IppValue<FinishingsCol[]>? FinishingsCol { get; set; }

    /// <summary>
    /// This attribute identifies the range(s) of print-stream pages that the
    /// Printer object uses for each copy of each document which are to be
    /// printed.  Nothing is printed for any pages identified that do not
    /// exist in the document(s).  Ranges MUST be in ascending order, for
    /// example: 1-3, 5-7, 15-19 and MUST NOT overlap, so that a non-spooling
    /// Printer object can process the job in a single pass.  If the ranges
    /// are not ascending or are overlapping, the IPP object MUST reject the
    /// request and return the 'client-error-bad-request' status code.  The
    /// attribute is associated with print-stream pages not application-
    /// numbered pages (for example, the page numbers found in the headers
    /// and or footers for certain word processing applications).
    /// For Jobs with multiple documents, the "multiple-document-handling"
    /// attribute determines what constitutes a "copy" for purposes of the
    /// specified page range(s).  When "multiple-document-handling" is
    /// 'single-document', the Printer object MUST apply each supplied page
    /// range once to the concatenation of the print-stream pages.  For
    /// example, if there are 8 documents of 10 pages each, the page-range
    /// '41:60' prints the pages in the 5th and 6th documents as a single
    /// document and none of the pages of the other documents are printed.
    /// When "multiple-document- handling" is 'separate-documents-
    /// uncollated-copies' or 'separate-documents-collated-copies', the
    /// Printer object MUST apply each supplied page range repeatedly to each
    /// document copy.  For the same job, the page-range '1:3, 10:10' would
    /// print the first 3 pages and the 10th page of each of the 8 documents
    /// in the Job, as 8 separate documents.
    /// In most cases, the exact pages to be printed will be generated by a
    /// device driver and this attribute would not be required.  However,
    /// when printing an archived document which has already been formatted,
    /// the end user may elect to print just a subset of the pages contained
    /// in the document.  In this case, if page-range = n.m is specified, the
    /// first page to be printed will be page n. All subsequent pages of the
    /// document will be printed through and including page m.
    /// "page-ranges-supported" is a boolean value indicating whether or not
    /// the printer is capable of supporting the printing of page ranges.
    /// This capability may differ from one PDL to another. There is no
    /// "page-ranges-default" attribute.  If the "page-ranges" attribute is
    /// not supplied by the client, all pages of the document will be
    /// printed.
    /// See: RFC 8011 Section 5.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PageRanges, Tag.RangeOfInteger)]
    public IppValue<Range[]>? PageRanges { get; set; }

    /// <summary>
    /// This attribute specifies how print-stream pages are to be imposed
    /// upon the sides of an instance of a selected medium, i.e., an
    /// impression.
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    [IppAttribute(IppAttributeNames.Sides)]
    public IppValue<Sides>? Sides { get; set; }

    /// <summary>
    /// This attribute specifies the number of print-stream pages to impose
    /// upon a single side of an instance of a selected medium.  For example,
    /// if the value is:
    /// '1'    the Printer MUST place one print-stream page on a single side
    /// of an instance of the selected medium (MAY add some sort
    /// of translation, scaling, or rotation).
    /// '2'    the Printer MUST place two print-stream pages on a single side
    /// of an instance of the selected medium (MAY add some sort
    /// of translation, scaling, or rotation).
    /// '4'    the Printer MUST place four print-stream pages on a single
    /// side of an instance of the selected medium (MAY add some
    /// sort of translation, scaling, or rotation).
    /// This attribute primarily controls the translation, scaling and
    /// rotation of print-stream pages.
    /// See: RFC 8011 Section 5.2.9
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.NumberUp, Tag.Integer)]
    public IppValue<int>? NumberUp { get; set; }

    /// <summary>
    /// This attribute indicates the desired orientation for printed print-
    /// stream pages; it does not describe the orientation of the client-
    /// supplied print-stream pages.
    /// For some document formats (such as 'application/postscript'), the
    /// desired orientation of the print-stream pages is specified within the
    /// document data.  This information is generated by a device driver
    /// prior to the submission of the print job.  Other document formats
    /// (such as 'text/plain') do not include the notion of desired
    /// orientation within the document data.  In the latter case it is
    /// possible for the Printer object to bind the desired orientation to
    /// the document data after it has been submitted.  It is expected that a
    /// Printer object would only support "orientations-requested" for some
    /// document formats (e.g., 'text/plain' or 'text/html') but not others
    /// (e.g., 'application/postscript').  This is no different than any
    /// other Job Template attribute since section 4.2, item 1, points out
    /// that a Printer object may support or not support any Job Template
    /// attribute based on the document format supplied by the client.
    /// However, a special mention is made here since it is very likely that
    /// a Printer object will support "orientation-requested" for only a
    /// subset of the supported document formats.
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    [IppAttribute(IppAttributeNames.OrientationRequested, Tag.Enum)]
    public IppValue<Orientation>? OrientationRequested { get; set; }

    /// <summary>
    /// This attribute identifies the medium that the Printer uses for all
    /// impressions of the Job.
    /// The values for "media" include medium-names, medium-sizes, input-
    /// trays and electronic forms so that one attribute specifies the media.
    /// If a Printer object supports a medium name as a value of this
    /// attribute, such a medium name implicitly selects an input-tray that
    /// contains the specified medium.  If a Printer object supports a medium
    /// size as a value of this attribute, such a medium size implicitly
    /// selects a medium name that in turn implicitly selects an input-tray
    /// that contains the medium with the specified size.  If a Printer
    /// object supports an input-tray as the value of this attribute, such an
    /// input-tray implicitly selects the medium that is in that input-tray
    /// at the time the job prints.  This case includes manual-feed input-
    /// trays.  If a Printer object supports an electronic form as the value
    /// of this attribute, such an electronic form implicitly selects a
    /// medium-name that in turn implicitly selects an input-tray that
    /// contains the medium specified by the electronic form.  The electronic
    /// form also implicitly selects an image that the Printer MUST merge
    /// with the document data as its prints each page.
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    [IppAttribute(IppAttributeNames.Media)]
    public IppValue<Media>? Media { get; set; }

    /// <summary>
    /// This attribute identifies the resolution that Printer uses for the
    /// Job.
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterResolution, Tag.Resolution)]
    public IppValue<Resolution>? PrinterResolution { get; set; }

    /// <summary>
    /// This attribute specifies the print quality that the Printer uses for
    /// the Job.
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintQuality, Tag.Enum)]
    public IppValue<PrintQuality>? PrintQuality { get; set; }

    /// <summary>
    /// This attribute specifies how the Printer scales the content.
    /// See: PWG 5100.13-2023 Section 6.2.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintScaling)]
    public IppValue<PrintScaling>? PrintScaling { get; set; }

    /// <summary>
    /// This attribute specifies the color mode for the Job.
    /// See: PWG 5100.13-2023 Section 6.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorMode)]
    public IppValue<PrintColorMode>? PrintColorMode { get; set; }

    /// <summary>
    /// This attribute specifies the rendering intent for color conversion.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintRenderingIntent)]
    public IppValue<PrintRenderingIntent>? PrintRenderingIntent { get; set; }

    /// <summary>
    /// This attribute specifies the action to take when the Printer encounters a Job processing error.
    /// See: PWG 5100.13-2023 Section 6.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorAction)]
    public IppValue<JobErrorAction>? JobErrorAction { get; set; }

    /// <summary>
    /// This attribute specifies the media and media-related attributes
    /// for the Job using a collection.
    /// See: PWG 5100.7-2023
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaCol)]
    public IppValue<MediaCol>? MediaCol { get; set; }

    /// <summary>
    /// This attribute identifies the device output bin to which the job
    /// is to be delivered.  There are standard values whose attribute
    /// syntax is 'keyword', but there are no standard values whose
    /// attribute syntax is 'name'.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputBin)]
    public IppValue<OutputBin>? OutputBin { get; set; }

    /// <summary>
    /// This attribute specifies the account associated with the Job.
    /// See: PWG 5100.7-2023 Section 6.8.1
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountId, Tag.NameWithoutLanguage)]
    public StringWithLanguage? JobAccountId { get; set; }

    /// <summary>
    /// This attribute specifies the type of value in <c>job-account-id</c>.
    /// See: PWG 5100.11-2024 Section 5.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountType)]
    public IppValue<JobAccountType>? JobAccountType { get; set; }

    /// <summary>
    /// This attribute specifies the user ID associated with the account
    /// specified by the "job-account-id" attribute.
    /// See: PWG 5100.7-2023 Section 6.8.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingUserId, Tag.NameWithoutLanguage)]
    public StringWithLanguage? JobAccountingUserId { get; set; }

    /// <summary>
    /// This attribute specifies the maximum number of seconds allowed
    /// for processing a Job.
    /// See: PWG 5100.7-2023 Section 6.8.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCancelAfter, Tag.Integer)]
    public IppValue<int>? JobCancelAfter { get; set; }

    /// <summary>
    /// This attribute specifies a time period in the future when the
    /// Printer will produce the output for the Job.
    /// See: PWG 5100.7-2023 Section 6.8.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntil)]
    public IppValue<JobHoldUntil>? JobDelayOutputUntil { get; set; }

    /// <summary>
    /// This attribute specifies a date and time when the Printer will
    /// produce the output for the Job.
    /// See: PWG 5100.7-2023 Section 6.8.5
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilTime)]
    public IppValue<DateTimeOffset>? JobDelayOutputUntilTime { get; set; }

    /// <summary>
    /// This attribute specifies the date and time after which the Job
    /// MUST become a candidate for processing.
    /// See: PWG 5100.7-2023 Section 6.8.6
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilTime)]
    public IppValue<DateTimeOffset>? JobHoldUntilTime { get; set; }

    /// <summary>
    /// This attribute specifies how long the Job remains in the Job
    /// Retention phase of a Job's life cycle.
    /// See: PWG 5100.7-2023 Section 6.8.7
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntil)]
    public IppValue<JobHoldUntil>? JobRetainUntil { get; set; }

    /// <summary>
    /// This attribute specifies the number of seconds the Job remains
    /// in the Job Retention phase of a Job's life cycle.
    /// See: PWG 5100.7-2023 Section 6.8.8
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilInterval, Tag.Integer)]
    public IppValue<int>? JobRetainUntilInterval { get; set; }

    /// <summary>
    /// This attribute specifies the date and time when the Job can
    /// leave the Job Retention phase of a Job's life cycle.
    /// See: PWG 5100.7-2023 Section 6.8.9
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilTime)]
    public IppValue<DateTimeOffset>? JobRetainUntilTime { get; set; }

    /// <summary>
    /// This attribute specifies a message that is printed on the Job Sheet.
    /// See: PWG 5100.7-2023 Section 6.8.10
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetMessage, Tag.TextWithoutLanguage)]
    public StringWithLanguage? JobSheetMessage { get; set; }

    /// <summary>
    /// This attribute specifies the output device requested for the Job.
    /// See: PWG 5100.7-2023 Section 6.3.2
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDevice, Tag.NameWithoutLanguage)]
    public StringWithLanguage? OutputDevice { get; set; }

    /// <summary>
    /// This attribute specifies how the Printer should optimize the
    /// content of the document for the output device.
    /// See: PWG 5100.7-2023 Section 6.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintContentOptimize)]
    public IppValue<PrintContentOptimize>? PrintContentOptimize { get; set; }

    /// <summary>
    /// This attribute specifies the number of pages per set
    /// for finishing operations.
    /// See: PWG 5100.1-2022 Section 5.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPagesPerSet, Tag.Integer)]
    public IppValue<int>? JobPagesPerSet { get; set; }

    /// <summary>
    /// The <c>cover-back</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.1
    /// Deprecated in: PWG 5100.3-2023 Section 5.2.1
    /// </summary>
    [Obsolete("The 'cover-back' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.1.")]
    [IppAttribute(IppAttributeNames.CoverBack)]
    public IppValue<Cover>? CoverBack { get; set; }

    /// <summary>
    /// The <c>cover-front</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.1
    /// Deprecated in: PWG 5100.3-2023 Section 5.2.1
    /// </summary>
    [Obsolete("The 'cover-front' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.1.")]
    [IppAttribute(IppAttributeNames.CoverFront)]
    public IppValue<Cover>? CoverFront { get; set; }

    /// <summary>
    /// The <c>force-front-side</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.2
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.ForceFrontSide, Tag.Integer)]
    public IppValue<int[]>? ForceFrontSide { get; set; }

    /// <summary>
    /// The <c>image-orientation</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.ImageOrientation, Tag.Enum)]
    public IppValue<Orientation>? ImageOrientation { get; set; }

    /// <summary>
    /// The <c>imposition-template</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.ImpositionTemplate)]
    public IppValue<ImpositionTemplate>? ImpositionTemplate { get; set; }

    /// <summary>
    /// The <c>insert-sheet</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.5
    /// Deprecated in: PWG 5100.3-2023 Section 5.2.5
    /// </summary>
    [Obsolete("The 'insert-sheet' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.5.")]
    [IppAttribute(IppAttributeNames.InsertSheet)]
    public IppValue<InsertSheet[]>? InsertSheet { get; set; }

    /// <summary>
    /// The <c>job-accounting-sheets</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.6
    /// Deprecated in: PWG 5100.3-2023 Section 5.2.6
    /// </summary>
    [Obsolete("The 'job-accounting-sheets' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.6.")]
    [IppAttribute(IppAttributeNames.JobAccountingSheets)]
    public IppValue<JobAccountingSheets>? JobAccountingSheets { get; set; }

    /// <summary>
    /// The <c>job-complete-before</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCompleteBefore)]
    public IppValue<JobCompleteBefore>? JobCompleteBefore { get; set; }

    /// <summary>
    /// The <c>job-complete-before-time</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.8
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCompleteBeforeTime)]
    public IppValue<DateTimeOffset>? JobCompleteBeforeTime { get; set; }

    /// <summary>
    /// The <c>job-error-sheet</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.9
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheet)]
    public IppValue<JobErrorSheet>? JobErrorSheet { get; set; }

    /// <summary>
    /// The <c>job-message-to-operator</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.10
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMessageToOperator, Tag.TextWithoutLanguage)]
    public StringWithLanguage? JobMessageToOperator { get; set; }

    /// <summary>
    /// The <c>job-phone-number</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.11
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumber, Tag.Uri)]
    public IppValue<string>? JobPhoneNumber { get; set; }

    /// <summary>
    /// The <c>job-recipient-name</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.12
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRecipientName, Tag.NameWithoutLanguage)]
    public StringWithLanguage? JobRecipientName { get; set; }

    /// <summary>
    /// The <c>media-input-tray-check</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.13
    /// Deprecated in: PWG 5100.3-2023 Section 5.2.13
    /// </summary>
    [Obsolete("The 'media-input-tray-check' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.13.")]
    [IppAttribute(IppAttributeNames.MediaInputTrayCheck)]
    public IppValue<MediaInputTrayCheck>? MediaInputTrayCheck { get; set; }

    /// <summary>
    /// The <c>page-delivery</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    [IppAttribute(IppAttributeNames.PageDelivery)]
    public IppValue<PageDelivery>? PageDelivery { get; set; }

    /// <summary>
    /// The <c>presentation-direction-number-up</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// Deprecated in: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    [Obsolete("The 'presentation-direction-number-up' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.15.")]
    [IppAttribute(IppAttributeNames.PresentationDirectionNumberUp)]
    public IppValue<PresentationDirectionNumberUp>? PresentationDirectionNumberUp { get; set; }

    /// <summary>
    /// The <c>separator-sheets</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.16
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheets)]
    public IppValue<SeparatorSheets>? SeparatorSheets { get; set; }

    /// <summary>
    /// The <c>x-image-position</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.17
    /// </summary>
    [IppAttribute(IppAttributeNames.XImagePosition)]
    public IppValue<XImagePosition>? XImagePosition { get; set; }

    /// <summary>
    /// The <c>x-image-shift</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.18
    /// </summary>
    [IppAttribute(IppAttributeNames.XImageShift, Tag.Integer)]
    public IppValue<int>? XImageShift { get; set; }

    /// <summary>
    /// The <c>x-side1-image-shift</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.19
    /// </summary>
    [IppAttribute(IppAttributeNames.XSide1ImageShift, Tag.Integer)]
    public IppValue<int>? XSide1ImageShift { get; set; }

    /// <summary>
    /// The <c>x-side2-image-shift</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.20
    /// </summary>
    [IppAttribute(IppAttributeNames.XSide2ImageShift, Tag.Integer)]
    public IppValue<int>? XSide2ImageShift { get; set; }

    /// <summary>
    /// The <c>y-image-position</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.21
    /// </summary>
    [IppAttribute(IppAttributeNames.YImagePosition)]
    public IppValue<YImagePosition>? YImagePosition { get; set; }

    /// <summary>
    /// The <c>y-image-shift</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.22
    /// </summary>
    [IppAttribute(IppAttributeNames.YImageShift, Tag.Integer)]
    public IppValue<int>? YImageShift { get; set; }

    /// <summary>
    /// The <c>y-side1-image-shift</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.23
    /// </summary>
    [IppAttribute(IppAttributeNames.YSide1ImageShift, Tag.Integer)]
    public IppValue<int>? YSide1ImageShift { get; set; }

    /// <summary>
    /// The <c>y-side2-image-shift</c> Job Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.24
    /// </summary>
    [IppAttribute(IppAttributeNames.YSide2ImageShift, Tag.Integer)]
    public IppValue<int>? YSide2ImageShift { get; set; }

    /// <summary>
    /// The <c>confirmation-sheet-print</c> Job Template attribute.
    /// See: PWG 5100.15-2013 Section 7.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.ConfirmationSheetPrint, Tag.Boolean)]
    public IppValue<bool>? ConfirmationSheetPrint { get; set; }

    /// <summary>
    /// The <c>number-of-retries</c> Job Template attribute.
    /// See: PWG 5100.15-2013 Section 7.4.20
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberOfRetries, Tag.Integer)]
    public IppValue<int>? NumberOfRetries { get; set; }

    /// <summary>
    /// The <c>retry-interval</c> Job Template attribute.
    /// See: PWG 5100.15-2013 Section 7.2.5
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.RetryInterval, Tag.Integer)]
    public IppValue<int>? RetryInterval { get; set; }

    /// <summary>
    /// The <c>retry-time-out</c> Job Template attribute.
    /// See: PWG 5100.15-2013 Section 7.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.RetryTimeOut, Tag.Integer)]
    public IppValue<int>? RetryTimeOut { get; set; }

    /// <summary>
    /// The <c>chamber-humidity</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.1
    /// </summary>
    [Range(0, 100)]
    [IppAttribute(IppAttributeNames.ChamberHumidity, Tag.Integer)]
    public IppValue<int>? ChamberHumidity { get; set; }

    /// <summary>
    /// The <c>chamber-temperature</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.2
    /// </summary>
    [Range(-273, int.MaxValue)]
    [IppAttribute(IppAttributeNames.ChamberTemperature, Tag.Integer)]
    public IppValue<int>? ChamberTemperature { get; set; }

    /// <summary>
    /// The <c>materials-col</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.3
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsCol)]
    public IppValue<Material[]>? MaterialsCol { get; set; }

    /// <summary>
    /// The <c>multiple-object-handling</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.4
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleObjectHandling)]
    public IppValue<MultipleObjectHandling>? MultipleObjectHandling { get; set; }

    /// <summary>
    /// The <c>platform-temperature</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.5
    /// </summary>
    [Range(-273, int.MaxValue)]
    [IppAttribute(IppAttributeNames.PlatformTemperature, Tag.Integer)]
    public IppValue<int>? PlatformTemperature { get; set; }

    /// <summary>
    /// The <c>print-accuracy</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintAccuracy)]
    public IppValue<PrintAccuracy>? PrintAccuracy { get; set; }

    /// <summary>
    /// The <c>print-base</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintBase)]
    public IppValue<PrintBase>? PrintBase { get; set; }

    /// <summary>
    /// The <c>print-objects</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.8
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintObjects)]
    public IppValue<PrintObject[]>? PrintObjects { get; set; }

    /// <summary>
    /// The <c>print-supports</c> Job Template attribute.
    /// See: PWG 5100.21-2019 Section 8.1.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintSupports)]
    public IppValue<PrintSupports>? PrintSupports { get; set; }

    /// <summary>
    /// The <c>overrides</c> Job Template attribute.
    /// See: PWG 5100.6-2003 Section 4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.Overrides)]
    public IppValue<OverrideInstruction[]>? Overrides { get; set; }

    /// <summary>
    /// This attribute specifies the number of copies to be printed for the entire Job,
    /// overriding the document-level "copies" attribute.
    /// See: PWG 5100.7-2023 Section 10.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCopies, Tag.Integer)]
    public IppValue<int>? JobCopies { get; set; }

    /// <summary>
    /// This attribute specifies the finishing operations to apply to the back cover sheet
    /// of the Job, overriding the document-level "cover-back" attribute.
    /// See: PWG 5100.7-2023 Section 10.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCoverBack)]
    public IppValue<Cover>? JobCoverBack { get; set; }

    /// <summary>
    /// This attribute specifies the finishing operations to apply to the front cover sheet
    /// of the Job, overriding the document-level "cover-front" attribute.
    /// See: PWG 5100.7-2023 Section 10.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCoverFront)]
    public IppValue<Cover>? JobCoverFront { get; set; }

    /// <summary>
    /// This attribute identifies the finishing operations that the Printer uses for each
    /// copy of each printed document in the Job, overriding the document-level "finishings"
    /// attribute.
    /// See: PWG 5100.7-2023 Section 10.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobFinishings, Tag.Enum)]
    public IppValue<Finishings[]>? JobFinishings { get; set; }

    /// <summary>
    /// This attribute specifies detailed finishing instructions for the Job, overriding
    /// the document-level "finishings-col" attribute.
    /// See: PWG 5100.7-2023 Section 10.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobFinishingsCol)]
    public IppValue<FinishingsCol[]>? JobFinishingsCol { get; set; }

    /// <summary>
    /// This attribute specifies a password string that the Printer uses to authenticate
    /// the Job. The value is an octetString encoded according to the encryption method
    /// specified by "job-password-encryption".
    /// See: PWG 5100.11-2024 Section 5.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPassword, Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString? JobPassword { get; set; }

    /// <summary>
    /// This attribute specifies the encryption method used to encode the "job-password"
    /// attribute value.
    /// See: PWG 5100.11-2024 Section 5.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPasswordEncryption)]
    public IppValue<JobPasswordEncryption>? JobPasswordEncryption { get; set; }

    /// <summary>
    /// This attribute specifies whether the Printer collates output sheets when producing
    /// multiple copies of a document.
    /// See: PWG 5100.8-2003 Section 3
    /// </summary>
    [IppAttribute(IppAttributeNames.SheetCollate, Tag.Keyword)]
    public IppValue<SheetCollate>? SheetCollate { get; set; }

    /// <summary>
    /// This attribute specifies overrides to Job Template attributes for specific pages
    /// within the Job.
    /// See: PWG 5100.8-2003 Section 3
    /// </summary>
    [IppAttribute(IppAttributeNames.PageOverrides)]
    public IppValue<OverrideInstruction[]>? PageOverrides { get; set; }

    /// <summary>
    /// This attribute specifies the number of pages in each subset when the Job is
    /// divided into subsets for finishing.
    /// See: PWG 5100.8-2003 Section 3
    /// </summary>
    [Obsolete("The 'pages-per-subset' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.PagesPerSubset, Tag.Integer)]
    public IppValue<int[]>? PagesPerSubset { get; set; }

    /// <summary>
    /// This attribute specifies overrides to Job Template attributes for specific
    /// documents within the Job.
    /// See: PWG 5100.8-2003 Section 3
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentOverrides)]
    public IppValue<OverrideInstruction[]>? DocumentOverrides { get; set; }

    /// <summary>
    /// This attribute specifies the input tray from which the Printer selects media
    /// for the Job.
    /// See: PWG 5100.7-2023 Section 15.2
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSource)]
    public IppValue<MediaSource>? MediaSource { get; set; }

    /// <summary>
    /// This attribute specifies the direction in which media is fed from the media source
    /// into the Printer.
    /// See: PWG 5100.7-2023 Section 15.2
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSourceFeedDirection)]
    public IppValue<MediaSourceFeedDirection>? MediaSourceFeedDirection { get; set; }

    /// <summary>
    /// This attribute specifies the orientation of the media as it is fed from the
    /// media source into the Printer.
    /// See: PWG 5100.7-2023 Section 15.3
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSourceFeedOrientation, Tag.Enum)]
    public IppValue<Orientation>? MediaSourceFeedOrientation { get; set; }

    /// <summary>
    /// This attribute specifies the URI of the requesting user, which the Printer
    /// uses for authentication and authorization purposes.
    /// See: PWG 5100.7-2023 Section 5.1.1
    /// </summary>
    [IppAttribute(IppAttributeNames.RequestingUserUri, Tag.Uri)]
    public IppValue<Uri>? RequestingUserUri { get; set; }

    /// <summary>
    /// This attribute specifies the names of Job Template attributes that the Printer
    /// MUST honor when processing the Job. If the Printer cannot honor any of the
    /// listed attributes, it MUST reject the Job.
    /// See: PWG 5100.7-2023 Section 6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMandatoryAttributes, Tag.Keyword)]
    public IppValue<string[]>? JobMandatoryAttributes { get; set; }

    /// <summary>
    /// This attribute specifies the Job IDs of the Jobs that are associated with
    /// this Job (e.g., for job chaining or dependency tracking).
    /// See: PWG 5100.7-2023 Section 6.1
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.JobIds, Tag.Integer)]
    public IppValue<int[]>? JobIds { get; set; }

    /// <summary>
    /// The job-save-disposition Job Template attribute.
    /// See: PWG 5100.11 (obsolete)
    /// </summary>
    [Obsolete("The 'job-save-disposition' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.JobSaveDisposition)]
    public IppValue<JobSaveDisposition>? JobSaveDisposition { get; set; }

    /// <summary>
    /// The pdl-init-file Job Template attribute.
    /// See: PWG 5100.11 (obsolete)
    /// </summary>
    [Obsolete("The 'pdl-init-file' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.PdlInitFile)]
    public IppValue<PdlInitFile>? PdlInitFile { get; set; }

}
