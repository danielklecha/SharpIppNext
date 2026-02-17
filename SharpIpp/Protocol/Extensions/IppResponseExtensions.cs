using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions
{
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
    }
}
