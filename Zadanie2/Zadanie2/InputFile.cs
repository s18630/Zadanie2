using System;
using System.Collections.Generic;
using System.IO;


namespace Cwiczenie2
{
    public class InputFile
    {
        public string path { get; set; }
        public List<string> content { get; }
        

        public InputFile(string path)
        {
            setPath(path);
            content = new List<string>();
            readContent();
        }


        public InputFile()
        { 
            setPath(@"dane.csv");
            content = new List<string>();
            readContent();
        }

  
        private void readContent(){

            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    content.Add(s);
                }
            }
        }


        private void setPath( string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
            }
            else
            {
                throw new FileNotFoundException("Plik " + path + " nie istnieje");
            }
        }


        public void showContent()
        {
         foreach(string s in content)
         {
           Console.WriteLine(s);
         }

        }


     


    }
}




    

    
    


  


    

