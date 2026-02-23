using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharpIpp.Exceptions
{
    [Serializable]
    public class IppRequestException : Exception
    {
        public IppRequestException( IIppRequestMessage requestMessage )
        {
            RequestMessage = requestMessage;
        }

        public IppRequestException( string message, IIppRequestMessage requestMessage, IppStatusCode statusCode ) : base( message )
        {
            RequestMessage = requestMessage;
            StatusCode = statusCode;
        }

        public IppRequestException( string message, Exception innerException, IIppRequestMessage requestMessage, IppStatusCode statusCode ) :
            base( message, innerException )
        {
            RequestMessage = requestMessage;
            StatusCode = statusCode;
        }

        public IIppRequestMessage RequestMessage { get; set; }

        public IppStatusCode StatusCode { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}\n{nameof( RequestMessage )}: {RequestMessage}";
        }
    }
}
