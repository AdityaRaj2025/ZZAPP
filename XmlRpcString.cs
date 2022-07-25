using System;
using System.Xml;

namespace SSAPP.XmlRpc
{
    class XmlRpcString : XmlRpcData
    {
        public override string TypeName => "string";
        public override string Value => StringValue;
        public string StringValue { get; set; }

        public XmlRpcString() { }
        public XmlRpcString(string value)
        {
            StringValue = value;
        }

        public override void ReadFrom(XmlNode node)
        {
            StringValue = node.InnerText;
        }

        public static implicit operator string(XmlRpcString v) => v.StringValue;
        public static explicit operator XmlRpcString(string v) => new XmlRpcString(v);

        public static explicit operator bool(XmlRpcString v)
        {
            throw new NotImplementedException();
        }
    }
}
