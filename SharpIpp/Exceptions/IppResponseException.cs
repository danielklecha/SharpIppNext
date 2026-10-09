using System;
using System.Runtime.Serialization;

using SharpIpp.Protocol;

namespace SharpIpp.Exceptions
{
    [Serializable]
    public class IppResponseException : Exception
    {
        public IppResponseException(string message) : base(message)
        {
        }

        public IppResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IppResponseException(IIppResponseMessage responseMessage)
        {
            ResponseMessage = responseMessage;
        }

        public IppResponseException(string message, IIppResponseMessage responseMessage) : base(message)
        {
            ResponseMessage = responseMessage;
        }

        public IppResponseException(string message, Exception innerException, IIppResponseMessage responseMessage) :
            base(message, innerException)
        {
            ResponseMessage = responseMessage;
        }

        public IIppResponseMessage? ResponseMessage { get; set; }

        public override string ToString()
        {
            return ResponseMessage != null
                ? $"{base.ToString()}\n{nameof(ResponseMessage)}: {ResponseMessage}"
                : base.ToString();
        }
    }
}
