using System;

namespace SSAPP.XmlRpc
{
    class XmlRpcException : Exception
    {
        public XmlRpcException(string message) : base(message) { }
        public XmlRpcException(string message, Exception innerException) : base(message, innerException) { }
    }
}
