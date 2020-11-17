
using System.IO;
using System.Xml.Serialization;

namespace Cwiczenie2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Xml.Linq;
    public class OutputFile
    {

        public List<string> data;
        public List<Student> students;
        public string path;
        public string format;

        public OutputFile(List<Student> students)
        {
            this.students = students;
            path = @"żesult.xml";
            format = "xml";
            XmlSerializer(students, path);
        }


        public OutputFile(List<Student>students,string path, string format)
        {
            this.students = students;

            if (!isFormatXML(format)  && !isFormatJSON(format))
            {
                throw new Exception("Nieprawidłowy format danych : " + format);
            }
            
            if (isPathCorrectJSON(path) && isFormatJSON(format))
            {
                this.path = path;
                this.format = format;
                JsonCreateFile(students, path);
            }

            if (isPathCorrectXML(path) && isFormatXML(format))
            {
                this.path = path;
                this.format = format;
                XmlSerializer(students, path);
            }

            else
            {
              throw new ArgumentException("Podana ścieżka jest niepoprawna, parametr: " + path + ", " + format);
            }
        }




        public OutputFile(List<Student> students, string parameter)
        {
            this.students = students;

            if (isFormatXML(parameter) || isPathCorrectXML(parameter))
            {
                this.format = "xml";
                if (isPathCorrectXML(parameter))
                {
                    this.path = parameter;
                }
                else
                {
                    this.path = @"żesult.xml";
                }
                XmlSerializer(students, path);
            }else if (isFormatJSON(parameter) || isPathCorrectJSON(parameter))
                {
                    this.format= "JSON";
                    if (isPathCorrectJSON(parameter))
                    {
                        this.path = parameter;
                    }
                    else
                    {
                        this.path = @"żesult.json";
                    }
                    JsonCreateFile(students, path);
                }
                else
                {
                    throw new ArgumentException("Podana ścieżka jest niepoprawna\nparametr: " + parameter);
                }
            
        }





        public OutputFile(List<string> data,string path, string format)
        {
            this.data = data;
            if (!isFormatXML(format) || !isPathCorrectXML(path))
            {
                throw new Exception("Nieprawidłowe argumenty : " + path + ", " + format);
            }
            else
            {
                this.path = path;
                this.format = format;
            }
            UtorzeniePlikuXML();
        }



        public OutputFile(List<string> data, string parameter)
        {
            this.data = data;
            if (isPathCorrectXML(path))
            {
                this.path = parameter;
                this.format = "xml";
            } else if (isFormatXML(parameter))
            {
                this.path = @"żesult.xml";
                this.format = parameter;
            }
            else
            {
               throw new ArgumentException("Podany argument jest niepoprawny:"+ path);
            }
            UtorzeniePlikuXML();
        }


        public OutputFile(List<string> data )
        {
            this.data = data;
            path = @"żesult.xml";
            format = "xml";
            UtorzeniePlikuXML();
        }

        public static void XmlSerializer(List<Student> students, string path)
        {
            
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var stream = File.Open(path, FileMode.Create))
            {
                serializer.Serialize(stream, students);
            }
        }

        public static void JsonCreateFile(List<Student> students, string path)
        {
            string jsonString = JsonSerializer.Serialize(students);
            File.WriteAllText(path, jsonString);
        }


        public bool isPathCorrectXML(string path)
        {
            if (path.EndsWith(".xml"))
            {
                return true;
            }
            return false;
        }


        public bool isPathCorrectJSON(string path)
        {
            if (path.EndsWith(".json"))
            {
                return true;
            }
            return false;
        }


        public bool isFormatXML(string format)
        {
            if (format.Equals("XML", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public bool isFormatJSON(string format)
        {
            if (format.Equals("json", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }






        public void UtorzeniePlikuXML()
           {
               XElement cust = new XElement("studenci",
                   from str in data
                   let fields = str.Split(',')
                   select new XElement("Student",
               new XAttribute("indexNumber", fields[0]),
               new XElement("fname", fields[1]),
               new XElement("lname", fields[2]),
               new XElement("birthdate", fields[3]),
               new XElement("Phone", fields[4]),
               new XElement("mothersname", fields[5]),
                new XElement("fathersname", fields[6]),
               new XElement("studies",
                   new XElement("name", fields[7]),
                   new XElement("mode", fields[8])
               )));

               var studentXml = new XDocument();
               DateTime dt = DateTime.Now;
               string time = dt.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR"));

               var rootElement = new XElement("uczelnia",
                   new XAttribute("createdAt", time),
                   new XAttribute("author", "Weronika Jaworek")
                );

               studentXml.Add(rootElement); 
               var e2 = new XElement(cust);
               rootElement.Add(cust);
    //         Console.WriteLine(studentXml.ToString());
               studentXml.Save(path);
           }
        


    }
}
