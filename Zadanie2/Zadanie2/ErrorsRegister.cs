using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cwiczenie2
{
public  class ErrorsRegister
    {
        public string fileName;
        public string path;

        public ErrorsRegister()
        {
            fileName= "łog.txt";
            path = @"łog.txt";
            createFile();
            string time = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
            saveToFile(" ZAPIS: " + time);
        }


        public void createFile()
        {
            Console.WriteLine("Path to my file: {0}\n", path);
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(path))
                {
                    Console.WriteLine("File \"{0}\" created.", fileName);
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }
        }


        public void saveToFile(string record)
        {
            try {
                
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(record);
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("File \"{0}\" do not exist", fileName);
            }
        }

         
         public void openFile()
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
              }
      



    }
}
