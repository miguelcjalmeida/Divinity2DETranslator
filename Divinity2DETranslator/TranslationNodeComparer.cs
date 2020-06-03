using System;
using System.Collections.Generic;
using System.Xml;

namespace Divinity2DETranslator
{
    public class TranslationNodeComparer : IEqualityComparer<XmlNode>   
    {
        bool IEqualityComparer<XmlNode>.Equals(XmlNode x, XmlNode y)
        {
            return x?.Attributes["contentuid"].Value == y?.Attributes["contentuid"].Value;
        }

        int IEqualityComparer<XmlNode>.GetHashCode(XmlNode node)
        {
            return node.Attributes["contentuid"].Value.GetHashCode();       
        }
    }
}