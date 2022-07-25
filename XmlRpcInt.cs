using System.Xml;

namespace SSAPP.XmlRpc
{
    class XmlRpcInt : XmlRpcData
    {
        public override string TypeName => "int";
        public override string Value => IntValue.ToString();
        public int IntValue { get; set; }

        public XmlRpcInt() { }
        public XmlRpcInt(int value)
        {
            IntValue = value;
        }

        public override void ReadFrom(XmlNode node)
        {
            IntValue = int.Parse(node.InnerText);
        }

        public static implicit operator int(XmlRpcInt v) => v.IntValue;
        public static explicit operator XmlRpcInt(int v) => new XmlRpcInt(v);
    }
}
