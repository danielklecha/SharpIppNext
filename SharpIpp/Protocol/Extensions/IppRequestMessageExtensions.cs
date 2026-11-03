using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Extension methods for <see cref="IIppRequestMessage"/>.
/// </summary>
public static class IppRequestMessageExtensions
{
    /// <summary>
    /// Enumerates all attributes across all attribute groups in the request message.
    /// </summary>
    /// <param name="message">The IPP request message.</param>
    /// <returns>An enumerable sequence of all <see cref="IppAttribute"/> instances contained in the message.</returns>
    public static IEnumerable<IppAttribute> GetAllAttributes(this IIppRequestMessage message)
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

            foreach (var attr in group)
            {
                yield return attr;
            }
        }
    }
}

