using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Extension methods for <see cref="IIppResponseMessage"/>.
/// </summary>
public static class IppResponseMessageExtensions
{
    /// <summary>
    /// Gets a value indicating whether the IPP status code indicates success.
    /// </summary>
    /// <param name="message">The IPP response message.</param>
    /// <returns><c>true</c> if the status code is between <see cref="IppStatusCode.SuccessfulOk"/> and <see cref="IppStatusCode.SuccessfulOkEventsComplete"/>; otherwise, <c>false</c>.</returns>
    public static bool IsSuccessfulStatusCode(this IIppResponseMessage message)
    {
        return (short)message.StatusCode >= (short)IppStatusCode.SuccessfulOk &&
               (short)message.StatusCode <= (short)IppStatusCode.SuccessfulOkEventsComplete;
    }

    public static List<List<IppAttribute>> GetSectionList(this IIppResponseMessage res, SectionTag sectionTag)
    {
        return sectionTag switch
        {
            SectionTag.OperationAttributesTag => res.OperationAttributes,
            SectionTag.JobAttributesTag => res.JobAttributes,
            SectionTag.PrinterAttributesTag => res.PrinterAttributes,
            SectionTag.UnsupportedAttributesTag => res.UnsupportedAttributes,
            SectionTag.SubscriptionAttributesTag => res.SubscriptionAttributes,
            SectionTag.EventNotificationAttributesTag => res.EventNotificationAttributes,
            SectionTag.ResourceAttributesTag => res.ResourceAttributes,
            SectionTag.DocumentAttributesTag => res.DocumentAttributes,
            SectionTag.SystemAttributesTag => res.SystemAttributes,
            _ => throw new ArgumentException($"Unknown section tag: {sectionTag}")
        };
    }

    /// <summary>
    /// Enumerates all attributes across all attribute groups in the response message.
    /// </summary>
    /// <param name="message">The IPP response message.</param>
    /// <returns>An enumerable sequence of all <see cref="IppAttribute"/> instances contained in the message.</returns>
    public static IEnumerable<IppAttribute> GetAllAttributes(this IIppResponseMessage message)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        var groups = new[]
        {
            message.OperationAttributes,
            message.JobAttributes,
            message.PrinterAttributes,
            message.UnsupportedAttributes,
            message.SubscriptionAttributes,
            message.EventNotificationAttributes,
            message.ResourceAttributes,
            message.DocumentAttributes,
            message.SystemAttributes
        };

        foreach (var group in groups)
        {
            if (group is null)
                continue;

            foreach (var attributeList in group)
            {
                if (attributeList is null)
                    continue;

                foreach (var attr in attributeList)
                {
                    yield return attr;
                }
            }
        }
    }
}
