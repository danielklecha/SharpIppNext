using System.Collections.Generic;
using SharpIpp.Models.Responses;

namespace SharpIpp.Protocol.Models
{
    public class IppResponseMessage : IppResponse<OperationAttributes>, IIppResponseMessage
    {
        public new List<List<IppAttribute>> OperationAttributes { get; } = [];

        public List<List<IppAttribute>> JobAttributes { get; } = [];

        public List<List<IppAttribute>> PrinterAttributes { get; } = [];

        public List<List<IppAttribute>> UnsupportedAttributes { get; } = [];

        public List<List<IppAttribute>> SubscriptionAttributes { get; } = [];

        public List<List<IppAttribute>> EventNotificationAttributes { get; } = [];

        public List<List<IppAttribute>> ResourceAttributes { get; } = [];

        public List<List<IppAttribute>> DocumentAttributes { get; } = [];

        public List<List<IppAttribute>> SystemAttributes { get; } = [];
    }
}
