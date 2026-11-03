using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Extensions;

public static class IppOperationExtensions
{
    public static bool IsSystemServiceOperation(this IppOperation operation) =>
        operation.IsStrictSystemServiceOperation() || operation.IsSubscriptionOperation();

    public static bool IsStrictSystemServiceOperation(this IppOperation operation)
    {
        return operation switch
        {
            IppOperation.AllocatePrinterResources
                or IppOperation.CreatePrinter
                or IppOperation.DeallocatePrinterResources
                or IppOperation.DeletePrinter
                or IppOperation.GetPrinters
                or IppOperation.GetPrinterResources
                or IppOperation.ShutdownOnePrinter
                or IppOperation.StartupOnePrinter
                or IppOperation.RestartOnePrinter
                or IppOperation.CancelResource
                or IppOperation.CreateResource
                or IppOperation.InstallResource
                or IppOperation.SendResourceData
                or IppOperation.SetResourceAttributes
                or IppOperation.CreateResourceSubscriptions
                or IppOperation.CreateSystemSubscriptions
                or IppOperation.DisableAllPrinters
                or IppOperation.EnableAllPrinters
                or IppOperation.GetResources
                or IppOperation.GetSystemAttributes
                or IppOperation.GetSystemSupportedValues
                or IppOperation.PauseAllPrinters
                or IppOperation.PauseAllPrintersAfterCurrentJob
                or IppOperation.RegisterOutputDevice
                or IppOperation.RestartSystem
                or IppOperation.ResumeAllPrinters
                or IppOperation.SetSystemAttributes
                or IppOperation.ShutdownAllPrinters
                or IppOperation.StartupAllPrinters
                => true,
            _ => false
        };
    }

    public static bool IsSubscriptionOperation(this IppOperation operation)
    {
        return operation switch
        {
            IppOperation.CancelSubscription
                or IppOperation.GetNotifications
                or IppOperation.GetSubscriptionAttributes
                or IppOperation.GetSubscriptions
                or IppOperation.RenewSubscription
                => true,
            _ => false
        };
    }

    public static bool SupportsJobUriTarget(this IppOperation operation) =>
        operation.IsJobTargetOperation()
        || operation.IsPwg51005DocumentTargetOperation()
        || operation is IppOperation.CreateJobSubscriptions or IppOperation.ValidateDocument;

    public static bool IsJobTargetOperation(this IppOperation operation)
    {
        return operation switch
        {
            IppOperation.CancelJob
                or IppOperation.GetJobAttributes
                or IppOperation.HoldJob
                or IppOperation.ReleaseJob
                or IppOperation.RestartJob
                or IppOperation.SetJobAttributes
                or IppOperation.SendDocument
                or IppOperation.SendUri
                or IppOperation.CloseJob
                or IppOperation.ResubmitJob
                or IppOperation.ReprocessJob
                or IppOperation.ResumeJob
                or IppOperation.PromoteJob
                or IppOperation.ScheduleJobAfter
                or IppOperation.GetDocuments
                => true,
            _ => false
        };
    }

    public static bool IsPwg51005DocumentTargetOperation(this IppOperation operation)
    {
        return operation switch
        {
            IppOperation.CancelDocument
                or IppOperation.GetDocumentAttributes
                or IppOperation.GetDocuments
                or IppOperation.SetDocumentAttributes
                or IppOperation.DeleteDocument
                or IppOperation.AddDocumentImages
                or IppOperation.AcknowledgeDocument
                or IppOperation.FetchDocument
                or IppOperation.UpdateDocumentStatus
                => true,
            _ => false
        };
    }
}
