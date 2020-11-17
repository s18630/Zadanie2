using System;

using System.Xml.Serialization;

namespace Cwiczenie2
{

   [Serializable]
    
    public  class Student
    {
     [XmlAttribute(AttributeName = "indexNumber")]
        public string indexNumber { get; set; }
     [XmlElement(ElementName = "fname")]
        public string fname { get; set; }

     [XmlElement(ElementName = "lname")]
        public string lname { get; set; }

     [XmlElement(ElementName = "birthdate")]
        public string birthdate { get; set; }

     [XmlElement(ElementName = "email")]
        public string email { get; set; }

     [XmlElement(ElementName = "mothersName")]
        public string mothersName { get; set; }


      [XmlElement(ElementName = "fathersName")]
        public string fathersName { get; set; }

      [XmlElement(ElementName = "studies")]
        public Studies studia { get; set; }
    }


   


}
