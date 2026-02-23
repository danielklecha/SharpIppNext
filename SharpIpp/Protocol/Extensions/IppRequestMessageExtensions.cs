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
        if (!request.OperationAttributes.Any(x => x.Name == JobAttribute.PrinterUri))
            throw new IppRequestException("No printer-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);
    }
}
