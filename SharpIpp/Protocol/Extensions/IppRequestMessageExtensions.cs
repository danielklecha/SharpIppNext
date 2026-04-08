using SharpIpp.Exceptions;
using SharpIpp.Protocol.Models;
using System;
using System.Linq;

namespace SharpIpp.Protocol.Extensions;

public static class IppRequestMessageExtensions
{
    public static void Validate(this IIppRequestMessage? request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (request.RequestId <= 0)
            throw new IppRequestException("Bad request-id value", request, IppStatusCode.ClientErrorBadRequest);
        if (!request.OperationAttributes.Any())
            throw new IppRequestException("No Operation Attributes", request, IppStatusCode.ClientErrorBadRequest);
        if (request.OperationAttributes.First().Name != JobAttribute.AttributesCharset)
            throw new IppRequestException("attributes-charset MUST be the first attribute", request, IppStatusCode.ClientErrorBadRequest);
        if (request.OperationAttributes.Skip(1).FirstOrDefault().Name != JobAttribute.AttributesNaturalLanguage)
            throw new IppRequestException("attributes-natural-language MUST be the second attribute", request, IppStatusCode.ClientErrorBadRequest);
        if (request.Version < new IppVersion(1, 0))
            throw new IppRequestException("Unsupported IPP version", request, IppStatusCode.ServerErrorVersionNotSupported);

        var hasPrinterUri = request.OperationAttributes.Any(x => x.Name == JobAttribute.PrinterUri);
        var hasSystemUri = request.OperationAttributes.Any(x => x.Name == SystemAttribute.SystemUri);
        if (!hasPrinterUri && !hasSystemUri)
            throw new IppRequestException("No printer-uri or system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (request.IppOperation.IsSystemServiceOperation() && !hasSystemUri)
            throw new IppRequestException("No system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);
    }

    private static bool IsSystemServiceOperation(this IppOperation operation)
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
                or IppOperation.CancelSubscription
                or IppOperation.GetNotifications
                or IppOperation.GetSubscriptionAttributes
                or IppOperation.GetSubscriptions
                or IppOperation.RenewSubscription
                => true,
            _ => false
        };
    }
}
