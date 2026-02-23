using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;

namespace SharpIpp
{
    public partial class SharpIppClient
    {
        /// <inheritdoc />
        public Task<CUPSGetPrintersResponse> GetCUPSPrintersAsync(CUPSGetPrintersRequest request, CancellationToken cancellationToken = default) => 
            SendAsync<CUPSGetPrintersRequest, CUPSGetPrintersResponse>(request, cancellationToken);
    }
}
