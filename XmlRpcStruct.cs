using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SSAPP.XmlRpc
{
    class XmlRpcStruct : XmlRpcData, IEnumerable<KeyValuePair<string, XmlRpcData>>
    {
        public override string TypeName => "struct";
        public override string Value => null;
        private Dictionary<string, XmlRpcData> _Members = new Dictionary<string, XmlRpcData>();

        public void Add(string key, XmlRpcData value)
        {
            _Members.Add(key, value);
        }

        public override void WriteTo(XmlDocument doc, XmlNode parent)
        {
            var node = doc.CreateElement(TypeName);
            foreach (var key in _Members.Keys)
            {
                var member = doc.CreateElement("member");
                var name = doc.CreateElement("name");
                var value = doc.CreateElement("value");
                name.InnerText = key;
                _Members[key].WriteTo(doc, value);
                node.AppendChild(member);
            }
            parent.AppendChild(node);
        }

        public override void ReadFrom(XmlNode node)
        {
            foreach (XmlNode m in node.SelectNodes("member"))
            {
                Add(m["name"].InnerText, ReadValue(m["value"]));
            }
        }

        public XmlRpcData this[string key]
        {
            get
            {
                return _Members[key];
            }
            set
            {
                _Members[key] = value;
            }
        }

        public bool ContainsKey(string key)
        {
            return _Members.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, XmlRpcData>> GetEnumerator()
        {
            return _Members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Members.GetEnumerator();
        }
    }
}
