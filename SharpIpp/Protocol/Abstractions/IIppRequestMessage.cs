using System.Collections.Generic;
using System.IO;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol
{
    public interface IIppRequestMessage
    {
        IppVersion Version { get; set; }

        IppOperation IppOperation { get; set; }

        int RequestId { get; set; }

        List<IppAttribute> OperationAttributes { get; }

        List<IppAttribute> JobAttributes { get; }

        List<IppAttribute> PrinterAttributes { get; }

        List<IppAttribute> UnsupportedAttributes { get; }

        List<IppAttribute> SubscriptionAttributes { get; }

        List<IppAttribute> EventNotificationAttributes { get; }

        List<IppAttribute> ResourceAttributes { get; }

        List<IppAttribute> DocumentAttributes { get; }

        List<IppAttribute> SystemAttributes { get; }

        Stream? Document { get; set; }
    }
}
