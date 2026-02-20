using System.Collections.Generic;
using System.IO;

namespace SharpIpp.Protocol.Models;

public class IppRequestMessage : IIppRequestMessage
{
    public Stream? Document { get; set; }

    public IppVersion Version { get; set; } = IppVersion.V1_1;

    public IppOperation IppOperation { get; set; }

    public int RequestId { get; set; }

    public List<IppAttribute> OperationAttributes { get; } = [];

    public List<IppAttribute> JobAttributes { get; } = [];

    public List<IppAttribute> PrinterAttributes { get; } = [];

    public List<IppAttribute> UnsupportedAttributes { get; } = [];

    public List<IppAttribute> SubscriptionAttributes { get; } = [];

    public List<IppAttribute> EventNotificationAttributes { get; } = [];

    public List<IppAttribute> ResourceAttributes { get; } = [];

    public List<IppAttribute> DocumentAttributes { get; } = [];

    public List<IppAttribute> SystemAttributes { get; } = [];
}
