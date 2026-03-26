using System;

namespace SharpIpp.Protocol.Models;

public struct NoValue : IEquatable<NoValue>
{
    public const string NoValueString = "###NOVALUE###";

    public override string ToString()
    {
        return "no value";
    }

    public bool Equals(NoValue other)
    {
        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is NoValue other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public static NoValue Instance = new();

    public static bool IsNoValue(object value, Tag tag = Tag.Unknown)
    {
        return value switch
        {
            int integer when integer == int.MinValue => true,
            Enum enumValue when Enum.GetUnderlyingType(enumValue.GetType()) == typeof(short) && Convert.ToInt16(enumValue) == short.MinValue => true,
            Enum enumValue when Enum.GetUnderlyingType(enumValue.GetType()) != typeof(short) && Convert.ToInt32(enumValue) == int.MinValue => true,
            DateTimeOffset dateTimeOffset when dateTimeOffset == default => true,
            DateTime dateTime when dateTime == default => true,
            Range range when range.Equals(default) => true,
            Resolution resolution when resolution.Equals(default) => true,
            StringWithLanguage stringWithLanguage when stringWithLanguage.Equals(default) => true,
            string stringValue when stringValue == string.Empty && tag == Tag.Keyword => true,
            string stringValue when stringValue == NoValueString => true,
            IIppCollection collection when collection.IsNoValue => true,
            OutputBin outputBin when outputBin.Value == string.Empty => true,
            ImpositionTemplate impositionTemplate when impositionTemplate.Value == string.Empty => true,
            JobPhoneNumberScheme jobPhoneNumberScheme when jobPhoneNumberScheme.Value == string.Empty => true,
            FinishingTemplate finishingTemplate when finishingTemplate.Value == string.Empty => true,
            Media media when media.Value == string.Empty => true,
            MediaKey mediaKey when mediaKey.Value == string.Empty => true,
            MediaColor mediaColor when mediaColor.Value == string.Empty => true,
            MediaType mediaType when mediaType.Value == string.Empty => true,
            JobHoldUntil jobHoldUntil when jobHoldUntil.Value == string.Empty => true,
            Compression compression when compression.Value == string.Empty => true,
            Sides sides when sides.Value == string.Empty => true,
            JobSheets jobSheets when jobSheets.Value == string.Empty => true,
            BalingType balingType when balingType.Value == string.Empty => true,
            BindingType bindingType when bindingType.Value == string.Empty => true,
            CoatingType coatingType when coatingType.Value == string.Empty => true,
            CoveringName coveringName when coveringName.Value == string.Empty => true,
            LaminatingType laminatingType when laminatingType.Value == string.Empty => true,
            MediaSourceFeedDirection mediaSourceFeedDirection when mediaSourceFeedDirection.Value == string.Empty => true,
            BalingWhen balingWhen when balingWhen.Value == string.Empty => true,
            FinishingReferenceEdge finishingReferenceEdge when finishingReferenceEdge.Value == string.Empty => true,
            FoldingDirection foldingDirection when foldingDirection.Value == string.Empty => true,
            CoatingSides coatingSides when coatingSides.Value == string.Empty => true,
            StitchingMethod stitchingMethod when stitchingMethod.Value == string.Empty => true,
            TrimmingType trimmingType when trimmingType.Value == string.Empty => true,
            TrimmingWhen trimmingWhen when trimmingWhen.Value == string.Empty => true,
            JobErrorSheetWhen jobErrorSheetWhen when jobErrorSheetWhen.Value == string.Empty => true,
            SeparatorSheetsType separatorSheetsType when separatorSheetsType.Value == string.Empty => true,
            MediaInputTrayCheck mediaInputTrayCheck when mediaInputTrayCheck.Value == string.Empty => true,
            PageDelivery pageDelivery when pageDelivery.Value == string.Empty => true,
            PresentationDirectionNumberUp presentationDirectionNumberUp when presentationDirectionNumberUp.Value == string.Empty => true,
            XImagePosition xImagePosition when xImagePosition.Value == string.Empty => true,
            YImagePosition yImagePosition when yImagePosition.Value == string.Empty => true,
            CoverType coverType when coverType.Value == string.Empty => true,
            JobSpooling jobSpooling when jobSpooling.Value == string.Empty => true,
            PrintContentOptimize printContentOptimize when printContentOptimize.Value == string.Empty => true,
            JobSheetsType jobSheetsType when jobSheetsType.Value == string.Empty => true,
            MultipleDocumentHandling multipleDocumentHandling when multipleDocumentHandling.Value == string.Empty => true,
            PrintScaling printScaling when printScaling.Value == string.Empty => true,
            PrintRenderingIntent printRenderingIntent when printRenderingIntent.Value == string.Empty => true,
            JobErrorAction jobErrorAction when jobErrorAction.Value == string.Empty => true,
            IdentifyAction identifyAction when identifyAction.Value == string.Empty => true,
            JobAccountType jobAccountType when jobAccountType.Value == string.Empty => true,
            JobPasswordEncryption jobPasswordEncryption when jobPasswordEncryption.Value == string.Empty => true,
            JobReleaseAction jobReleaseAction when jobReleaseAction.Value == string.Empty => true,
            WhichJobs whichJobs when whichJobs.Value == string.Empty => true,
            JobStateReason jobStateReason when jobStateReason.Value == string.Empty => true,
            UriScheme uriScheme when uriScheme.Value == string.Empty => true,
            UriAuthentication uriAuthentication when uriAuthentication.Value == string.Empty => true,
            UriSecurity uriSecurity when uriSecurity.Value == string.Empty => true,
            MediaSource mediaSource when mediaSource.Value == string.Empty => true,
            MediaCoating mediaCoating when mediaCoating.Value == string.Empty => true,
            MediaGrain mediaGrain when mediaGrain.Value == string.Empty => true,
            MediaPrePrinted mediaPrePrinted when mediaPrePrinted.Value == string.Empty => true,
            MediaRecycled mediaRecycled when mediaRecycled.Value == string.Empty => true,
            MediaTooth mediaTooth when mediaTooth.Value == string.Empty => true,
            PrintColorMode printColorMode when printColorMode.Value == string.Empty => true,
            DocumentStateReason documentStateReason when documentStateReason.Value == string.Empty => true,
            PrinterStateReason printerStateReason when printerStateReason.Value == string.Empty => true,
            PdlOverride pdlOverride when pdlOverride.Value == string.Empty => true,
            NoValue => true,
            _ => false
        };
    }

    public static T GetCollectionNoValue<T>() where T : IIppCollection, new()
    {
        return new T { IsNoValue = true };
    }

    public static T GetNoValue<T>(Tag tag = Tag.Unknown)
    {
        return (T)GetNoValue(typeof(T), tag);
    }

    public static object GetNoValue(Type type, Tag tag = Tag.Unknown)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        if (underlyingType.IsEnum)
        {
            var enumUnderlyingType = Enum.GetUnderlyingType(underlyingType);
            return enumUnderlyingType == typeof(short)
                ? Enum.ToObject(underlyingType, short.MinValue)
                : Enum.ToObject(underlyingType, int.MinValue);
        }

        return underlyingType switch
        {
            _ when underlyingType == typeof(int) => int.MinValue,
            _ when underlyingType == typeof(string) => tag == Tag.Keyword ? string.Empty : NoValueString,
            _ when underlyingType == typeof(DateTimeOffset) => DateTimeOffset.MinValue,
            _ when underlyingType == typeof(DateTime) => DateTime.MinValue,
            _ when underlyingType == typeof(bool) => false,
            _ when underlyingType == typeof(Range) => new Range(),
            _ when underlyingType == typeof(Resolution) => new Resolution(),
            _ when underlyingType == typeof(StringWithLanguage) => new StringWithLanguage(),
            _ when underlyingType == typeof(ClientInfo) => GetCollectionNoValue<ClientInfo>(),
            _ when underlyingType == typeof(DocumentFormatDetails) => GetCollectionNoValue<DocumentFormatDetails>(),
            _ when underlyingType == typeof(DocumentTemplateAttributes) => GetCollectionNoValue<DocumentTemplateAttributes>(),
            _ when underlyingType == typeof(MediaCol) => GetCollectionNoValue<MediaCol>(),
            _ when underlyingType == typeof(JobCounter) => GetCollectionNoValue<JobCounter>(),
            _ when underlyingType == typeof(JobSheetsCol) => GetCollectionNoValue<JobSheetsCol>(),
            _ when underlyingType == typeof(MediaSize) => GetCollectionNoValue<MediaSize>(),
            _ when underlyingType == typeof(MediaSizeSupported) => GetCollectionNoValue<MediaSizeSupported>(),
            _ when underlyingType == typeof(MediaSourceProperties) => GetCollectionNoValue<MediaSourceProperties>(),
            _ when underlyingType == typeof(FinishingsCol) => GetCollectionNoValue<FinishingsCol>(),
            _ when underlyingType == typeof(Baling) => GetCollectionNoValue<Baling>(),
            _ when underlyingType == typeof(Binding) => GetCollectionNoValue<Binding>(),
            _ when underlyingType == typeof(Coating) => GetCollectionNoValue<Coating>(),
            _ when underlyingType == typeof(Covering) => GetCollectionNoValue<Covering>(),
            _ when underlyingType == typeof(Folding) => GetCollectionNoValue<Folding>(),
            _ when underlyingType == typeof(Laminating) => GetCollectionNoValue<Laminating>(),
            _ when underlyingType == typeof(Punching) => GetCollectionNoValue<Punching>(),
            _ when underlyingType == typeof(Stitching) => GetCollectionNoValue<Stitching>(),
            _ when underlyingType == typeof(Trimming) => GetCollectionNoValue<Trimming>(),
            _ when underlyingType == typeof(Cover) => GetCollectionNoValue<Cover>(),
            _ when underlyingType == typeof(InsertSheet) => GetCollectionNoValue<InsertSheet>(),
            _ when underlyingType == typeof(JobAccountingSheets) => GetCollectionNoValue<JobAccountingSheets>(),
            _ when underlyingType == typeof(JobErrorSheet) => GetCollectionNoValue<JobErrorSheet>(),
            _ when underlyingType == typeof(SeparatorSheets) => GetCollectionNoValue<SeparatorSheets>(),
            _ when underlyingType == typeof(ProofPrint) => GetCollectionNoValue<ProofPrint>(),
            _ when underlyingType == typeof(JobStorage) => GetCollectionNoValue<JobStorage>(),
            _ when underlyingType == typeof(DocumentAccess) => GetCollectionNoValue<DocumentAccess>(),
            _ when underlyingType == typeof(CoverSheetInfo) => GetCollectionNoValue<CoverSheetInfo>(),
            _ when underlyingType == typeof(DestinationUri) => GetCollectionNoValue<DestinationUri>(),
            _ when underlyingType == typeof(OutputAttributes) => GetCollectionNoValue<OutputAttributes>(),
            _ when underlyingType == typeof(Material) => GetCollectionNoValue<Material>(),
            _ when underlyingType == typeof(PrintAccuracy) => GetCollectionNoValue<PrintAccuracy>(),
            _ when underlyingType == typeof(PrintObject) => GetCollectionNoValue<PrintObject>(),
            _ when underlyingType == typeof(OverrideInstruction) => GetCollectionNoValue<OverrideInstruction>(),
            _ when underlyingType == typeof(OutputBin) => new OutputBin(string.Empty),
            _ when underlyingType == typeof(ImpositionTemplate) => new ImpositionTemplate(string.Empty),
            _ when underlyingType == typeof(JobPhoneNumberScheme) => new JobPhoneNumberScheme(string.Empty),
            _ when underlyingType == typeof(FinishingTemplate) => new FinishingTemplate(string.Empty),
            _ when underlyingType == typeof(Media) => new Media(string.Empty),
            _ when underlyingType == typeof(MediaKey) => new MediaKey(string.Empty),
            _ when underlyingType == typeof(MediaColor) => new MediaColor(string.Empty),
            _ when underlyingType == typeof(MediaType) => new MediaType(string.Empty),
            _ when underlyingType == typeof(JobHoldUntil) => new JobHoldUntil(string.Empty),
            _ when underlyingType == typeof(Compression) => new Compression(string.Empty),
            _ when underlyingType == typeof(Sides) => new Sides(string.Empty),
            _ when underlyingType == typeof(JobSheets) => new JobSheets(string.Empty),
            _ when underlyingType == typeof(BalingType) => new BalingType(string.Empty),
            _ when underlyingType == typeof(BindingType) => new BindingType(string.Empty),
            _ when underlyingType == typeof(CoatingType) => new CoatingType(string.Empty),
            _ when underlyingType == typeof(CoveringName) => new CoveringName(string.Empty),
            _ when underlyingType == typeof(LaminatingType) => new LaminatingType(string.Empty),
            _ when underlyingType == typeof(MediaSourceFeedDirection) => new MediaSourceFeedDirection(string.Empty),
            _ when underlyingType == typeof(BalingWhen) => new BalingWhen(string.Empty),
            _ when underlyingType == typeof(FinishingReferenceEdge) => new FinishingReferenceEdge(string.Empty),
            _ when underlyingType == typeof(FoldingDirection) => new FoldingDirection(string.Empty),
            _ when underlyingType == typeof(CoatingSides) => new CoatingSides(string.Empty),
            _ when underlyingType == typeof(StitchingMethod) => new StitchingMethod(string.Empty),
            _ when underlyingType == typeof(TrimmingType) => new TrimmingType(string.Empty),
            _ when underlyingType == typeof(TrimmingWhen) => new TrimmingWhen(string.Empty),
            _ when underlyingType == typeof(JobErrorSheetWhen) => new JobErrorSheetWhen(string.Empty),
            _ when underlyingType == typeof(SeparatorSheetsType) => new SeparatorSheetsType(string.Empty),
            _ when underlyingType == typeof(MediaInputTrayCheck) => new MediaInputTrayCheck(string.Empty),
            _ when underlyingType == typeof(PageDelivery) => new PageDelivery(string.Empty),
            _ when underlyingType == typeof(PresentationDirectionNumberUp) => new PresentationDirectionNumberUp(string.Empty),
            _ when underlyingType == typeof(XImagePosition) => new XImagePosition(string.Empty),
            _ when underlyingType == typeof(YImagePosition) => new YImagePosition(string.Empty),
            _ when underlyingType == typeof(CoverType) => new CoverType(string.Empty),
            _ when underlyingType == typeof(JobSpooling) => new JobSpooling(string.Empty),
            _ when underlyingType == typeof(PrintContentOptimize) => new PrintContentOptimize(string.Empty),
            _ when underlyingType == typeof(JobSheetsType) => new JobSheetsType(string.Empty),
            _ when underlyingType == typeof(MultipleDocumentHandling) => new MultipleDocumentHandling(string.Empty),
            _ when underlyingType == typeof(PrintScaling) => new PrintScaling(string.Empty),
            _ when underlyingType == typeof(PrintRenderingIntent) => new PrintRenderingIntent(string.Empty),
            _ when underlyingType == typeof(JobErrorAction) => new JobErrorAction(string.Empty),
            _ when underlyingType == typeof(IdentifyAction) => new IdentifyAction(string.Empty),
            _ when underlyingType == typeof(JobAccountType) => new JobAccountType(string.Empty),
            _ when underlyingType == typeof(JobPasswordEncryption) => new JobPasswordEncryption(string.Empty),
            _ when underlyingType == typeof(JobReleaseAction) => new JobReleaseAction(string.Empty),
            _ when underlyingType == typeof(WhichJobs) => new WhichJobs(string.Empty),
            _ when underlyingType == typeof(JobStateReason) => new JobStateReason(string.Empty),
            _ when underlyingType == typeof(UriScheme) => new UriScheme(string.Empty),
            _ when underlyingType == typeof(UriAuthentication) => new UriAuthentication(string.Empty),
            _ when underlyingType == typeof(UriSecurity) => new UriSecurity(string.Empty),
            _ when underlyingType == typeof(MediaSource) => new MediaSource(string.Empty),
            _ when underlyingType == typeof(MediaCoating) => new MediaCoating(string.Empty),
            _ when underlyingType == typeof(MediaGrain) => new MediaGrain(string.Empty),
            _ when underlyingType == typeof(MediaPrePrinted) => new MediaPrePrinted(string.Empty),
            _ when underlyingType == typeof(MediaRecycled) => new MediaRecycled(string.Empty),
            _ when underlyingType == typeof(MediaTooth) => new MediaTooth(string.Empty),
            _ when underlyingType == typeof(PrintColorMode) => new PrintColorMode(string.Empty),
            _ when underlyingType == typeof(DocumentStateReason) => new DocumentStateReason(string.Empty),
            _ when underlyingType == typeof(PrinterStateReason) => new PrinterStateReason(string.Empty),
            _ when underlyingType == typeof(PdlOverride) => new PdlOverride(string.Empty),
            _ => throw new ArgumentException($"Type {type} is not supported for NoValue mapping and has no non-null default value")
        };
    }
}

