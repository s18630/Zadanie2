using System;
using System.Xml.Serialization;

namespace Cwiczenie2
{
    [Serializable]
    public class Studies
    {
       
        [XmlElement(ElementName = "name")]
        public string name { get; set; }

        [XmlElement(ElementName = "mode")]
        public string mode { get; set; }
    }
}
