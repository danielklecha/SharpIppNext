using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// A decorator for <see cref="IIppRequestMessage"/> that allows clearing specific properties (returning their default/null value).
/// </summary>
public class IppRequestMessageFilter : IIppRequestMessage
{
    private readonly IIppRequestMessage _request;
    private readonly HashSet<string> _clearedProperties = new();

    public IppRequestMessageFilter(IIppRequestMessage request)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
    }

    /// <summary>
    /// Adds a filter to clear a specific property value (it will return default/null).
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="propertyExpression">An expression specifying the property to clear (e.g., x => x.Document).</param>
    public void ClearProperty<T>(Expression<Func<IIppRequestMessage, T>> propertyExpression)
    {
        if (propertyExpression.Body is MemberExpression memberExpression)
        {
            _clearedProperties.Add(memberExpression.Member.Name);
        }
        else
        {
            throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
        }
    }

    public IppVersion Version
    {
        get => _clearedProperties.Contains(nameof(Version)) ? default : _request.Version;
        set => _request.Version = value;
    }

    public IppOperation IppOperation
    {
        get => _clearedProperties.Contains(nameof(IppOperation)) ? default : _request.IppOperation;
        set => _request.IppOperation = value;
    }

    public int RequestId
    {
        get => _clearedProperties.Contains(nameof(RequestId)) ? default : _request.RequestId;
        set => _request.RequestId = value;
    }

    public List<IppAttribute> OperationAttributes => 
        _clearedProperties.Contains(nameof(OperationAttributes)) ? null! : _request.OperationAttributes;

    public List<IppAttribute> JobAttributes => 
        _clearedProperties.Contains(nameof(JobAttributes)) ? null! : _request.JobAttributes;

    public List<IppAttribute> PrinterAttributes => 
        _clearedProperties.Contains(nameof(PrinterAttributes)) ? null! : _request.PrinterAttributes;

    public List<IppAttribute> UnsupportedAttributes => 
        _clearedProperties.Contains(nameof(UnsupportedAttributes)) ? null! : _request.UnsupportedAttributes;

    public List<IppAttribute> SubscriptionAttributes => 
        _clearedProperties.Contains(nameof(SubscriptionAttributes)) ? null! : _request.SubscriptionAttributes;

    public List<IppAttribute> EventNotificationAttributes => 
        _clearedProperties.Contains(nameof(EventNotificationAttributes)) ? null! : _request.EventNotificationAttributes;

    public List<IppAttribute> ResourceAttributes => 
        _clearedProperties.Contains(nameof(ResourceAttributes)) ? null! : _request.ResourceAttributes;

    public List<IppAttribute> DocumentAttributes => 
        _clearedProperties.Contains(nameof(DocumentAttributes)) ? null! : _request.DocumentAttributes;

    public List<IppAttribute> SystemAttributes => 
        _clearedProperties.Contains(nameof(SystemAttributes)) ? null! : _request.SystemAttributes;

    public Stream? Document
    {
        get => _clearedProperties.Contains(nameof(Document)) ? null : _request.Document;
        set => _request.Document = value;
    }
}
