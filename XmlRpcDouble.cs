using System.Xml;

namespace SSAPP.XmlRpc
{
    class XmlRpcDouble : XmlRpcData
    {
        public override string TypeName => "double";
        public override string Value => DoubleValue.ToString();
        public double DoubleValue { get; set; }

        public XmlRpcDouble() { }
        public XmlRpcDouble(double value)
        {
            DoubleValue = value;
        }

        public override void ReadFrom(XmlNode node)
        {
            DoubleValue = double.Parse(node.InnerText);
        }

        public static implicit operator double(XmlRpcDouble v) => v.DoubleValue;
        public static explicit operator XmlRpcDouble(double v) => new XmlRpcDouble(v);
    }
}
