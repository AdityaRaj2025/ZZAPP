using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SSAPP.XmlRpc
{
    class XmlRpcArray : XmlRpcData, IEnumerable<XmlRpcData>
    {
        public override string TypeName => "array";
        public override string Value => null;
        private List<XmlRpcData> _InternalList = new List<XmlRpcData>();

        public XmlRpcArray() { }

        public void Add(XmlRpcData data)
        {
            _InternalList.Add(data);
        }

        public override void WriteTo(XmlDocument doc, XmlNode parent)
        {
            var node = doc.CreateElement(TypeName);
            var datanode = doc.CreateElement("data");
            node.AppendChild(datanode);
            foreach (var item in _InternalList)
            {
                var valuenode = doc.CreateElement("value");
                item.WriteTo(doc, valuenode);
                datanode.AppendChild(valuenode);
            }
            parent.AppendChild(node);
        }

        public override void ReadFrom(XmlNode node)
        {
            foreach (XmlNode n in node.SelectNodes("data/value"))
            {
                Add(ReadValue(n));
            }
        }

        public IEnumerator<XmlRpcData> GetEnumerator()
        {
            return _InternalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _InternalList.GetEnumerator();
        }
    }
}
