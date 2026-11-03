using System.Collections.Generic;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppRequestMessageValidator
{
    /// <summary>
    /// Validates the strict ordering of operation target attributes in the Operation Attributes group.
    /// Spec: RFC 8011 Section 4.1.5, PWG 5100.5 Section 5.1, PWG 5100.22 Section 7.1.26.
    /// </summary>
    private void ValidateTargetAttributeOrderRule(IIppRequestMessage request, List<IppAttribute> operationAttributes)
    {
        if (operationAttributes.Count < 3)
            throw new IppRequestException("Target URI attribute MUST be the third attribute", request, IppStatusCode.ClientErrorBadRequest);

        int FindAttributeIndex(List<IppAttribute> list, string targetName)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == targetName)
                    return i;
            }
            return -1;
        }

        void EnsurePositionIfPresent(string name, int index, string ordinal, string? message = null)
        {
            var actualIndex = FindAttributeIndex(operationAttributes, name);
            if (actualIndex >= 0 && actualIndex != index)
            {
                throw new IppRequestException(message ?? $"'{name}' MUST be the {ordinal} attribute", request, IppStatusCode.ClientErrorBadRequest);
            }
        }

        var thirdAttr = operationAttributes[2];
        var isStrictSystemService = request.IppOperation.IsStrictSystemServiceOperation();

        if (isStrictSystemService && thirdAttr.Name != IppAttributeNames.SystemUri)
            throw new IppRequestException($"'{IppAttributeNames.SystemUri}' MUST be the third attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (thirdAttr.Name == IppAttributeNames.SystemUri)
        {
            EnsurePositionIfPresent(IppAttributeNames.ResourceId, 3, "fourth");
            EnsurePositionIfPresent(IppAttributeNames.PrinterId, 3, "fourth");
            EnsurePositionIfPresent(IppAttributeNames.NotifySubscriptionId, 3, "fourth");
            if (HasNamedAttribute(operationAttributes, IppAttributeNames.PrinterId))
            {
                EnsurePositionIfPresent(IppAttributeNames.ResourceIds, 4, "fifth");
            }
            return;
        }

        var isPrinterUriTarget = thirdAttr.Name == IppAttributeNames.PrinterUri;
        var isJobUriTarget = thirdAttr.Name == IppAttributeNames.JobUri;

        if (!isPrinterUriTarget && !isJobUriTarget)
        {
            throw new IppRequestException($"Target URI attribute ('{IppAttributeNames.PrinterUri}' or '{IppAttributeNames.JobUri}') MUST be the third attribute", request, IppStatusCode.ClientErrorBadRequest);
        }

        if (HasNamedAttribute(operationAttributes, IppAttributeNames.PrinterUri) && HasNamedAttribute(operationAttributes, IppAttributeNames.JobUri))
            throw new IppRequestException($"'{IppAttributeNames.PrinterUri}' and '{IppAttributeNames.JobUri}' MUST NOT both be supplied as targets", request, IppStatusCode.ClientErrorBadRequest);

        if (isPrinterUriTarget)
        {
            if (HasNamedAttribute(operationAttributes, IppAttributeNames.JobId))
            {
                EnsurePositionIfPresent(IppAttributeNames.JobId, 3, "fourth");
                EnsurePositionIfPresent(IppAttributeNames.DocumentNumber, 4, "fifth");
            }
            else
            {
                if (HasNamedAttribute(operationAttributes, IppAttributeNames.DocumentNumber))
                    throw new IppRequestException($"'{IppAttributeNames.JobId}' MUST be supplied before '{IppAttributeNames.DocumentNumber}' when '{IppAttributeNames.PrinterUri}' is used as target", request, IppStatusCode.ClientErrorBadRequest);

                EnsurePositionIfPresent(IppAttributeNames.JobIds, 3, "fourth");
                EnsurePositionIfPresent(IppAttributeNames.NotifySubscriptionId, 3, "fourth");
            }
        }
        else // isJobUriTarget
        {
            if (HasNamedAttribute(operationAttributes, IppAttributeNames.JobId))
                throw new IppRequestException($"'{IppAttributeNames.JobId}' MUST NOT be supplied when '{IppAttributeNames.JobUri}' is used as target", request, IppStatusCode.ClientErrorBadRequest);

            EnsurePositionIfPresent(
                IppAttributeNames.DocumentNumber,
                3,
                "fourth",
                $"'{IppAttributeNames.DocumentNumber}' MUST be the fourth attribute following '{IppAttributeNames.JobUri}'");
        }
    }
}

