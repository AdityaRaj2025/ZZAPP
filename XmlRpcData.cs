using System.Xml;

namespace SSAPP.XmlRpc
{
    internal abstract class XmlRpcData
    {
        public XmlRpcData() { }

        public abstract string TypeName { get; }
        public abstract string Value { get; }

        /// <summary>
        /// Writes to the parent value node
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="parent">valueノード</param>
        /// <remarks></remarks>
        public virtual void WriteTo(XmlDocument doc, XmlNode parent)
        {
            var node = doc.CreateElement(TypeName);
            node.InnerText = Value;
            parent.AppendChild(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node">Value Node（valueの子）</param>
        /// <remarks></remarks>
        public virtual void ReadFrom(XmlNode node)
        {
        }

        public static XmlRpcData Create(string type)
        {
            switch (type)
            {
                case "string":
                    return new XmlRpcString();

                case "int":
                case "i4":
                    return new XmlRpcInt();

                case "struct":
                    return new XmlRpcStruct();

                case "array":
                    return new XmlRpcArray();

                default:
                    return null;
            }
        }

        // Return the data contained in the <value> node as XmlRpcData
        public static XmlRpcData ReadValue(XmlNode node)
        {
            var data = node.FirstChild;

            var ret = Create(data.Name);
            ret.ReadFrom(data);

            return ret;
        }
    }
}
