using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppRequestMessageValidator
{
    private const string NotifyEventsAttributeName = "notify-events";
    private const string AllRequestedAttributesGroup = "all";

    private static readonly HashSet<string> Pwg51005DocumentRequestedAttributeGroupKeywords =
    [
        AllRequestedAttributesGroup,
        IppAttributeNames.DocumentDescription,
        IppAttributeNames.DocumentTemplate,
    ];

    private static readonly HashSet<string> Pwg51008JobRequestedAttributeGroupKeywords =
    [
        AllRequestedAttributesGroup,
        "job-description",
        "job-template",
        "job-actual",
    ];

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationUriSchemes = new(StringComparer.OrdinalIgnoreCase)
    {
        "tel",
        "fax",
        "sip",
        "sips",
    };

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationUriMembers =
    [
        "post-dial-string",
        "t33-subaddress",
    ];

    /// <summary>
    /// Executes operation-specific validation rules for all supported IPP/PWG operations.
    /// Spec: RFC 8011, PWG 5100.5, PWG 5100.22.
    /// </summary>
    public virtual void ValidateOperationRules(IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        var ctx = context ?? Context;
        var operationAttributes = request.OperationAttributes;
        var hasDocumentNumber = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, IppAttributeNames.DocumentNumber);
        var hasLastDocument = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, IppAttributeNames.LastDocument);
        var hasDocumentUri = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, IppAttributeNames.DocumentUri);
        var hasOutputDeviceUuid = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, IppAttributeNames.OutputDeviceUuid);
        var hasJobUri = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, IppAttributeNames.JobUri);

        var documentNumber = ReadOperationAttributeAs<int>(operationAttributes, IppAttributeNames.DocumentNumber);
        var lastDocument = ReadOperationAttributeAs<bool>(operationAttributes, IppAttributeNames.LastDocument);

        switch (request.IppOperation)
        {
            case IppOperation.CancelDocument:
            case IppOperation.GetDocumentAttributes:
            case IppOperation.DeleteDocument:
            case IppOperation.SetDocumentAttributes:
            case IppOperation.FetchDocument:
            case IppOperation.UpdateDocumentStatus:
            case IppOperation.AcknowledgeDocument:
                ValidateDocumentOperationRules(
                    request,
                    operationAttributes,
                    hasJobUri,
                    hasDocumentNumber,
                    documentNumber,
                    hasOutputDeviceUuid,
                    ctx);
                break;

            case IppOperation.AddDocumentImages:
            case IppOperation.CancelJob:
            case IppOperation.GetJobAttributes:
            case IppOperation.HoldJob:
            case IppOperation.ReleaseJob:
            case IppOperation.RestartJob:
            case IppOperation.SetJobAttributes:
            case IppOperation.CloseJob:
            case IppOperation.ResubmitJob:
            case IppOperation.ReprocessJob:
            case IppOperation.ResumeJob:
            case IppOperation.PromoteJob:
            case IppOperation.ScheduleJobAfter:
            case IppOperation.GetDocuments:
                if (!hasJobUri)
                    ValidateRequiredJobId(operationAttributes, request);
                break;

            case IppOperation.PrintJob:
                if (request.Document == null)
                    throw new IppRequestException("document stream required", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.PrintUri:
                if (ValidateOperationAttributesGroup && !hasDocumentUri)
                    throw new IppRequestException("missing document-uri", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.SendDocument:
                ValidateRequiredLastDocument(hasLastDocument, lastDocument, request);
                if (!hasJobUri)
                    ValidateRequiredJobId(operationAttributes, request);
                if (ValidateOperationAttributesGroup && lastDocument == false && request.Document == null)
                    throw new IppRequestException("document stream required when last-document=false", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.SendUri:
                ValidateRequiredLastDocument(hasLastDocument, lastDocument, request);
                if (!hasJobUri)
                    ValidateRequiredJobId(operationAttributes, request);
                if (ValidateOperationAttributesGroup && lastDocument == false && !hasDocumentUri)
                    throw new IppRequestException("missing document-uri", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.AcknowledgeIdentifyPrinter:
            case IppOperation.DeregisterOutputDevice:
            case IppOperation.FetchJob:
            case IppOperation.GetOutputDeviceAttributes:
            case IppOperation.UpdateJobStatus:
            case IppOperation.UpdateOutputDeviceAttributes:
            case IppOperation.RegisterOutputDevice:
            case IppOperation.UpdateActiveJobs:
            case IppOperation.AcknowledgeJob:
                ValidateOutputDeviceOperationRules(
                    request,
                    operationAttributes,
                    hasJobUri,
                    hasOutputDeviceUuid);
                break;

            case IppOperation.ValidateJob:
            case IppOperation.ValidateDocument:
                if (ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, IppAttributeNames.DocumentPassword))
                    throw new IppRequestException("document-password is not allowed for validate operations", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.CreateJob:
                ValidateCreateJobDestinationRules(operationAttributes, request);
                break;

            case IppOperation.GetNotifications:
                if (ValidateOperationAttributesGroup)
                {
                    var notifyPullMethodAttr = GetNamedAttribute(operationAttributes, IppAttributeNames.NotifyPullMethod);
                    var notifyPullMethodValue = notifyPullMethodAttr?.Value.ToString();
                    if (!string.Equals(notifyPullMethodValue, "ippget", StringComparison.OrdinalIgnoreCase))
                        throw new IppRequestException("'notify-pull-method' must be 'ippget'", request, IppStatusCode.ClientErrorBadRequest);
                }
                break;

            case IppOperation.CancelSubscription:
            case IppOperation.GetSubscriptionAttributes:
            case IppOperation.RenewSubscription:
                if (ValidateOperationAttributesGroup)
                    RequireAttribute(operationAttributes, IppAttributeNames.NotifySubscriptionId, request);
                break;

            case IppOperation.CancelResource:
            case IppOperation.GetResourceAttributes:
            case IppOperation.InstallResource:
            case IppOperation.SendResourceData:
            case IppOperation.SetResourceAttributes:
                if (ValidateOperationAttributesGroup)
                    RequireAttribute(operationAttributes, IppAttributeNames.ResourceId, request);
                break;

            case IppOperation.AllocatePrinterResources:
            case IppOperation.DeallocatePrinterResources:
            case IppOperation.DeletePrinter:
            case IppOperation.GetPrinterResources:
            case IppOperation.ShutdownOnePrinter:
            case IppOperation.StartupOnePrinter:
            case IppOperation.RestartOnePrinter:
                if (ValidateOperationAttributesGroup)
                    RequireAttribute(operationAttributes, IppAttributeNames.PrinterId, request);
                break;

            case IppOperation.CreatePrinterSubscriptions:
            case IppOperation.CreateSystemSubscriptions:
                if (!request.SubscriptionAttributes.Any())
                    throw new IppRequestException("subscription-attributes-group is required and must be non-empty", request, IppStatusCode.ClientErrorBadRequest);
                break;
        }

        ValidateJobRequestedAttributesGroupKeywords(request, ctx);
        ValidateDocumentRequestedAttributesGroupKeywords(request, ctx);
        ValidateNotifyEventsValues(request, ctx);
        ValidateInputAttributesRules(request);
    }

    /// <summary>
    /// Validates document-targeted operation rules (Cancel-Document, Get-Document-Attributes, etc.).
    /// </summary>
    protected virtual void ValidateDocumentOperationRules(
        IIppRequestMessage request,
        List<IppAttribute> operationAttributes,
        bool hasJobUri,
        bool hasDocumentNumber,
        int? documentNumber,
        bool hasOutputDeviceUuid,
        IppRequestValidationContext ctx)
    {
        ValidateRequiredDocumentNumber(hasDocumentNumber, documentNumber, request);

        if (request.IppOperation is IppOperation.AcknowledgeDocument or IppOperation.FetchDocument or IppOperation.UpdateDocumentStatus)
            ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);

        if (request.IppOperation == IppOperation.AcknowledgeDocument)
            ValidateFetchStatusCodeNotSuccessful(operationAttributes, request);

        if (!hasJobUri)
            ValidateRequiredJobId(operationAttributes, request);

        if (request.IppOperation == IppOperation.CancelDocument && documentNumber is int cancelDocumentNumber)
            ValidateCancelDocumentStateRules(cancelDocumentNumber, request, ctx);

        if (request.IppOperation == IppOperation.SetDocumentAttributes)
        {
            if (ValidateDocumentAttributesGroup && !request.DocumentAttributes.Any())
                throw new IppRequestException("missing document attributes", request, IppStatusCode.ClientErrorBadRequest);

            if (documentNumber is int setDocumentNumber)
                ValidateSetDocumentAttributesStateRules(setDocumentNumber, request, ctx);
        }
    }

    /// <summary>
    /// Validates output-device-targeted operation rules (Acknowledge-Identify-Printer, Register-Output-Device, etc.).
    /// </summary>
    protected virtual void ValidateOutputDeviceOperationRules(
        IIppRequestMessage request,
        List<IppAttribute> operationAttributes,
        bool hasJobUri,
        bool hasOutputDeviceUuid)
    {
        ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);

        if (request.IppOperation == IppOperation.UpdateJobStatus && !hasJobUri)
            ValidateRequiredJobId(operationAttributes, request);

        if (request.IppOperation == IppOperation.AcknowledgeJob)
        {
            ValidateFetchStatusCodeNotSuccessful(operationAttributes, request);
            if (!hasJobUri)
                ValidateRequiredJobId(operationAttributes, request);
        }

        if (request.IppOperation == IppOperation.RegisterOutputDevice && ValidateOperationAttributesGroup)
        {
            ValidateMutuallyExclusive(
                operationAttributes,
                IppAttributeNames.OutputDeviceX509Certificate,
                IppAttributeNames.OutputDeviceX509Request,
                request);
        }

        if (request.IppOperation == IppOperation.UpdateActiveJobs && ValidateOperationAttributesGroup)
        {
            ValidateCoDependent(
                operationAttributes,
                IppAttributeNames.JobIds,
                IppAttributeNames.OutputDeviceJobStates,
                request);

            if (HasNamedAttribute(operationAttributes, IppAttributeNames.JobIds))
            {
                var jobIdsCount = CountNamedAttributes(operationAttributes, IppAttributeNames.JobIds);
                var jobStatesCount = CountNamedAttributes(operationAttributes, IppAttributeNames.OutputDeviceJobStates);
                if (jobIdsCount != jobStatesCount)
                    throw new IppRequestException("'job-ids' and 'output-device-job-states' must have the same number of values", request, IppStatusCode.ClientErrorBadRequest);
            }
        }
    }

    /// <summary>
    /// Validates destination-uri rules for create-job operations, rejecting forbidden fax attributes/schemes for Scan.
    /// Spec: PWG 5100.17 / PWG 5100.22.
    /// </summary>
    public virtual void ValidateCreateJobDestinationRules(IReadOnlyCollection<IppAttribute> operationAttributes, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        var destinationUriCollections = EnumerateNamedCollections(operationAttributes, IppAttributeNames.DestinationUris).ToArray();
        if (!destinationUriCollections.Any())
            return;

        foreach (var destinationUriCollection in destinationUriCollections)
        {
            if (destinationUriCollection.Count == 1 && destinationUriCollection[0].Tag.IsOutOfBand())
                continue;

            var destinationUriMembers = destinationUriCollection
                .FromBegCollection()
                .Where(x => x.Tag != Tag.EndCollection)
                .ToArray();

            if (destinationUriMembers.Any(x => Pwg51017ForbiddenDestinationUriMembers.Contains(x.Name)))
                throw new IppRequestException("invalid destination-uris: reserved fax member attributes are not allowed for Scan", request, IppStatusCode.ClientErrorBadRequest);

            foreach (var destinationUriMember in destinationUriMembers.Where(x => x.Name == "destination-uri"))
            {
                var destinationUri = destinationUriMember.Value.ToString();
                if (string.IsNullOrEmpty(destinationUri))
                    continue;

                string? scheme = null;
                if (Uri.TryCreate(destinationUri, UriKind.Absolute, out var parsedUri))
                    scheme = parsedUri.Scheme;
                else if (destinationUri.IndexOf(':') is int i and > 0)
                    scheme = destinationUri.Substring(0, i);

                if (scheme != null && Pwg51017ForbiddenDestinationUriSchemes.Contains(scheme))
                    throw new IppRequestException("invalid destination-uri scheme for Scan", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
            }
        }

        var destinationAccessesCollections = EnumerateNamedCollections(operationAttributes, IppAttributeNames.DestinationAccesses).ToArray();
        if (destinationAccessesCollections.Length > 0 && destinationAccessesCollections.Length != destinationUriCollections.Length)
            throw new IppRequestException("destination-accesses cardinality MUST match destination-uris", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Validates that requested attribute group keywords for Job query operations are supported by the target.
    /// Spec: RFC 8011 Section 3.2.5.1 / PWG 5100.8.
    /// </summary>
    public virtual void ValidateJobRequestedAttributesGroupKeywords(IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (request.IppOperation is not (IppOperation.GetJobAttributes or IppOperation.GetJobs))
            return;

        var ctx = context ?? Context;
        var supported = ctx.JobRequestedAttributeGroupKeywordsSupported;
        if (supported is null || supported.Count == 0)
            return;

        var requestedAttributes = request.OperationAttributes
            .Where(x => x.Name == IppAttributeNames.RequestedAttributes)
            .Select(x => x.Value.ToString())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        if (requestedAttributes.Length == 0)
            return;

        var supportedSet = new HashSet<string>(supported, StringComparer.Ordinal);
        var unsupportedGroupKeywords = requestedAttributes
            .Where(Pwg51008JobRequestedAttributeGroupKeywords.Contains)
            .Where(x => !supportedSet.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedGroupKeywords.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"requested-attributes group value(s) not supported: {string.Join(", ", unsupportedGroupKeywords)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    /// <summary>
    /// Ensures that a required document-number is present and greater than zero.
    /// Spec: PWG 5100.5 Section 5 (Document Object).
    /// </summary>
    private void ValidateRequiredDocumentNumber(bool hasDocumentNumber, int? documentNumber, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasDocumentNumber)
            throw new IppRequestException("missing document-number", request, IppStatusCode.ClientErrorBadRequest);
        if (documentNumber is null || documentNumber <= 0)
            throw new IppRequestException("invalid document-number", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a required job-id parameter is present and greater than zero for job operations targeting printer-uri.
    /// Spec: RFC 8011 Section 3.1.5, Section 3.3.
    /// </summary>
    public virtual void ValidateRequiredJobId(IEnumerable<IppAttribute> operationAttributes, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        var hasJobId = false;
        foreach (var attr in operationAttributes)
        {
            if (attr.Name == IppAttributeNames.JobId)
            {
                hasJobId = true;
                if (attr.Value is int jobId && jobId > 0)
                    return;

                throw new IppRequestException("invalid job-id", request, IppStatusCode.ClientErrorBadRequest);
            }
        }

        if (!hasJobId)
            throw new IppRequestException("missing job-id", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a last-document parameter is present for multi-document operations.
    /// Spec: RFC 8011 Section 3.3.1 (Send-Document).
    /// </summary>
    public virtual void ValidateRequiredLastDocument(bool hasLastDocument, bool? lastDocument, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasLastDocument)
            throw new IppRequestException("missing last-document", request, IppStatusCode.ClientErrorBadRequest);
        if (lastDocument is null)
            throw new IppRequestException("invalid last-document", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a target output-device-uuid is present in coordination operations.
    /// Spec: PWG 5100.22 (IPP System Service).
    /// </summary>
    public virtual void ValidateRequiredOutputDeviceUuid(bool hasOutputDeviceUuid, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasOutputDeviceUuid)
            throw new IppRequestException("missing output-device-uuid", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a proxy fetch-status-code parameter is not successful-ok (must be a warning or error code).
    /// Spec: PWG 5100.22 (IPP System Service).
    /// </summary>
    public static void ValidateFetchStatusCodeNotSuccessful(IEnumerable<IppAttribute> operationAttributes, IIppRequestMessage request)
    {
        var fetchStatusAttributes = operationAttributes
            .Where(x => x.Name == IppAttributeNames.FetchStatusCode)
            .ToArray();
        if (fetchStatusAttributes.Length == 0)
            return;

        var fetchStatusAttribute = fetchStatusAttributes[0];

        if (fetchStatusAttribute.Value is IppStatusCode fetchStatusCode && fetchStatusCode == IppStatusCode.SuccessfulOk)
            throw new IppRequestException("fetch-status-code MUST NOT be successful-ok", request, IppStatusCode.ClientErrorBadRequest);

        if (fetchStatusAttribute.Value is int fetchStatusCodeInt && fetchStatusCodeInt == (int)IppStatusCode.SuccessfulOk)
            throw new IppRequestException("fetch-status-code MUST NOT be successful-ok", request, IppStatusCode.ClientErrorBadRequest);

        if (fetchStatusAttribute.Value is short fetchStatusCodeShort && fetchStatusCodeShort == (short)IppStatusCode.SuccessfulOk)
            throw new IppRequestException("fetch-status-code MUST NOT be successful-ok", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Reads and casts an operation attribute's value to the specified type if present.
    /// </summary>
    private static T? ReadOperationAttributeAs<T>(IEnumerable<IppAttribute> operationAttributes, string name) where T : struct
    {
        var attribute = GetNamedAttribute(operationAttributes, name);
        if (attribute?.Value is T typed)
            return typed;

        return null;
    }

    /// <summary>
    /// Validates that requested attribute group keywords for Document query operations are supported by the target.
    /// Spec: PWG 5100.5 (Document Object).
    /// </summary>
    public virtual void ValidateDocumentRequestedAttributesGroupKeywords(IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (request.IppOperation is not (IppOperation.GetDocumentAttributes or IppOperation.GetDocuments))
            return;

        var ctx = context ?? Context;
        var supported = ctx.DocumentRequestedAttributeGroupKeywordsSupported;
        if (supported is null || supported.Count == 0)
            return;

        var requestedAttributes = request.OperationAttributes
            .Where(x => x.Name == IppAttributeNames.RequestedAttributes)
            .Select(x => x.Value.ToString())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        if (requestedAttributes.Length == 0)
            return;

        var supportedSet = new HashSet<string>(supported, StringComparer.Ordinal);
        var unsupportedGroupKeywords = requestedAttributes
            .Where(Pwg51005DocumentRequestedAttributeGroupKeywords.Contains)
            .Where(x => !supportedSet.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedGroupKeywords.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"requested-attributes group value(s) not supported: {string.Join(", ", unsupportedGroupKeywords)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    /// <summary>
    /// Validates that notify-events keywords are supported by the target printer/system.
    /// Spec: RFC 3995 Section 5.3.3 (notify-events).
    /// </summary>
    public virtual void ValidateNotifyEventsValues(IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        if (!ValidateSubscriptionAttributesGroup)
            return;

        var ctx = context ?? Context;
        var supported = ctx.NotifyEventsSupported;
        if (supported is null || supported.Count == 0)
            return;

        var supportedSet = new HashSet<string>(supported, StringComparer.Ordinal);
        var notifyEvents = request.OperationAttributes
            .Concat(request.SubscriptionAttributes)
            .Where(x => x.Name == NotifyEventsAttributeName)
            .Select(x => x.Value.ToString())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        if (notifyEvents.Length == 0)
            return;

        var unsupportedNotifyEvents = notifyEvents
            .Where(x => !supportedSet.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedNotifyEvents.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"notify-events value(s) not supported: {string.Join(", ", unsupportedNotifyEvents)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    /// <summary>
    /// Validates that a Document is in a state that permits cancellation.
    /// Spec: PWG 5100.5 Section 5 (Cancel-Document state rules).
    /// </summary>
    public virtual void ValidateCancelDocumentStateRules(int documentNumber, IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        var ctx = context ?? Context;
        if (ctx.DocumentStatesByNumber == null)
            return;

        if (!ctx.DocumentStatesByNumber.TryGetValue(documentNumber, out var documentState))
            return;

        if (documentState is DocumentState.Completed or DocumentState.Canceled or DocumentState.Aborted)
            throw new IppRequestException("invalid document-state for Cancel-Document", request, IppStatusCode.ClientErrorNotPossible);

        if (documentState != DocumentState.Processing)
            return;

        if (ctx.DocumentStateReasonsByNumber == null)
            return;

        if (!ctx.DocumentStateReasonsByNumber.TryGetValue(documentNumber, out var stateReasons) || stateReasons == null)
            return;

        if (stateReasons.Contains(DocumentStateReason.ProcessingToStopPoint))
            throw new IppRequestException("invalid document-state-reasons for Cancel-Document", request, IppStatusCode.ClientErrorNotPossible);
    }

    /// <summary>
    /// Validates that a Document is in a state that permits setting of attributes.
    /// Spec: PWG 5100.5 Section 5 (Set-Document-Attributes state rules).
    /// </summary>
    public virtual void ValidateSetDocumentAttributesStateRules(int documentNumber, IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        var ctx = context ?? Context;
        if (ctx.DocumentStatesByNumber == null)
            return;

        if (!ctx.DocumentStatesByNumber.TryGetValue(documentNumber, out var documentState))
            return;

        if (documentState is DocumentState.Completed or DocumentState.Canceled or DocumentState.Aborted)
            throw new IppRequestException("invalid document-state for Set-Document-Attributes", request, IppStatusCode.ClientErrorNotPossible);

        if (documentState == DocumentState.Processing && !ctx.AllowSetDocumentAttributesWhenProcessing)
            throw new IppRequestException("invalid document-state for Set-Document-Attributes", request, IppStatusCode.ClientErrorNotPossible);
    }
}

