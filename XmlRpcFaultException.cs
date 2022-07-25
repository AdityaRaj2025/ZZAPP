using System;
using System.Collections.Generic;
using System.Text;

namespace SSAPP.XmlRpc
{
    class XmlRpcFaultException : XmlRpcException
    {
        public int FaultCode { get; private set; }
        public string FaultString { get; private set; }

        public XmlRpcFaultException(int code, string str) : 
            base($"XML-RPCフォールトが発生しました。(code={code}, string={str})")
        {
            FaultCode = code;
            FaultString = str;
        }
    }
}
