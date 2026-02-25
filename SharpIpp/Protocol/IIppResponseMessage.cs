using System.Collections.Generic;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public interface IIppResponseMessage : IIppResponse
{
    new List<List<IppAttribute>> OperationAttributes { get; }

    List<List<IppAttribute>> JobAttributes { get; }

    List<List<IppAttribute>> PrinterAttributes { get; }

    List<List<IppAttribute>> UnsupportedAttributes { get; }

    List<List<IppAttribute>> SubscriptionAttributes { get; }

    List<List<IppAttribute>> EventNotificationAttributes { get; }

    List<List<IppAttribute>> ResourceAttributes { get; }

    List<List<IppAttribute>> DocumentAttributes { get; }

    List<List<IppAttribute>> SystemAttributes { get; }
}
