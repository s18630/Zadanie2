
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Cwiczenie2
{
    public class Program
    {
        static void Main(string[] args)
        {

            ErrorsRegister errorsRecord = new ErrorsRegister();

            string pathInput = null;
            string arg2 = null;
            string arg3 = null;


            if (args.Length == 0)
            {
                errorsRecord.saveToFile("Nie wprowadzono argumentów");
            }

            if (args.Length > 3)
            {
                errorsRecord.saveToFile("Wprowadzono zbyt dużo argumentów");
            }

            if( args.Length == 3)
            {
                pathInput = args[0];
                arg2 = args[1];
                arg3 = args[2];
            }

            if (args.Length == 2)
            {
                errorsRecord.saveToFile("Niepodano jednego z argumentów");
                if (args[0].EndsWith(".csv"))
                {
                    pathInput = args[0];
                    arg2 = args[1];
                }else
                {
                    arg2 = args[0];
                    arg3 = args[1];
                }
            }
            if (args.Length == 1)
            {
                errorsRecord.saveToFile("Niepodanono dwóch z argumentów");
                if (args[0].EndsWith(".csv"))
                {
                    pathInput = args[0];
                }
                else
                {
                    arg2 = args[0];
                }
            }


            try
            {
                InputFile inputFile;

                if (pathInput == null)
                {
                    inputFile = new InputFile();
                }
                else
                {
                    inputFile = new InputFile(pathInput);
                }

                inputFile.showContent();

                Data studentsData  = new Data(inputFile.content, errorsRecord);
                studentsData.showData();
                List<Student> students = studentsData.students;
                OutputFile outputFile;

                if (arg2!= null & arg3 != null)
                {
                    try
                    {
                      outputFile = new OutputFile(students, arg2 , arg3);
                    }catch(Exception ex)
                    {
                       errorsRecord.saveToFile(ex.Message);
                    }
                }
                else if(arg2 != null)
                {
                    try
                    {
                        outputFile = new OutputFile(students, arg2);
                    }catch (ArgumentException ex)
                    {
                        errorsRecord.saveToFile(ex.Message);
                    }
                }
                else
                {
                    outputFile = new OutputFile(students);
                }


            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                errorsRecord.saveToFile(ex.Message);
            }





        }
    }
}
