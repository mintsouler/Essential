using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Dynamic;
using System.Linq;

namespace Chapter18
{
    public class DynamicXml:DynamicObject
    {
        private XElement Element { get; set; }
        public DynamicXml(XElement element)
        {
            Element = element;
        }

        public static DynamicXml Parse(string text)
        {
            return new DynamicXml(XElement.Parse(text));
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool success = false;
            result = null;
            XElement firstDecendant = Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDecendant != null)
            {
                if (firstDecendant.Descendants().Count() > 0)
                {
                    result = new DynamicXml(firstDecendant);
                }
                else
                {
                    result = firstDecendant.Value;
                }
                success = true;
            }
            return success;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            bool success = false;
            XElement firstDecendant = Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDecendant != null)
            {
                if (value.GetType() == typeof(XElement))
                {
                    firstDecendant.ReplaceWith(value);
                }
                else
                {
                    firstDecendant.Value = value.ToString();
                }
                success = true;
            }
            return success;
        }
    }
}
