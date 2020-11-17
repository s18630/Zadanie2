using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Cwiczenie2
{
    public class Data

    {
        public List<string> data { get; set; }
        public  List<CorrectRecord> correctRecords{ get; set; }
        public List<Student> students { get; set; }
        public ErrorsRegister errorsRegister { get; set; }



        public Data(List<string> lista,ErrorsRegister errorsRegister)
        {
            data = new List<string>();
            students = new List<Student>();
            correctRecords = new List<CorrectRecord>();
            this.errorsRegister = errorsRegister;
            extractData(lista);
        }


        public void extractData(List<string> list)
        {
            int allRecords =list.Count;
            int okRecords = 0;

            foreach (string s in list)
            {
                string[] studentsData = s.Split(',');
                try
                {
                    CorrectRecord correctRecord = new CorrectRecord(studentsData);

                    if (isDuplicate(correctRecord) )
                    {
                        errorsRegister.saveToFile("Znaleziono duplikat: " + correctRecord.getCorrectedRecord());
                    }
                    else
                    {
                        correctRecords.Add(correctRecord);
                        string record = correctRecord.getCorrectedRecord();
                        data.Add(record);
                        correctRecord.showDataSet();
                        students.Add(new Student
                        {
                            indexNumber = correctRecord.indexNumber,
                            fname = correctRecord.fname,
                            lname = correctRecord.lname,
                            birthdate = correctRecord.birthdate,
                            email = correctRecord.email,
                            mothersName = correctRecord.mothersName,
                            fathersName = correctRecord.fathersName,
                            studia = new Studies
                            {
                                name = correctRecord.studiesName,
                                mode = correctRecord.studiesMode
                            }
                        });
                        okRecords++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    errorsRegister.saveToFile(ex.Message);
                }
            }
            Console.WriteLine("All records: " + allRecords + "  Correct records: " + okRecords);
        }


        public void showData()
        {
            if (data.Count == 0)
            {
                Console.WriteLine("No data to display. ");
            }
            else
            {
                Console.WriteLine(" Data:");
                int count = 0;
                foreach (string s in data)
                {
                    count++;
                    Console.WriteLine(" Zestaw Danych nr " + count);
                    Console.WriteLine(s + "\n");
                }
                Console.WriteLine("Zostało zapisane " + count + "zestawów danych");
            }
        }

        public bool isDuplicate(CorrectRecord record)
        {
            foreach (CorrectRecord s in correctRecords)
            {
                int conditions = 0;
                if (Equals(s.indexNumber, record.indexNumber))
                {
                    conditions++;
                }
                if (Equals(s.fname, record.fname))
                {
                    conditions++;
                }
                if (Equals(s.lname, record.lname))
                {
                    conditions++;
                }
                if (conditions == 3)
                {
                    return true;
                }
            }
            return false;
        }


        public class CorrectRecord

        {
            public string[] columns { get; set; }
            public string correctedRecord { get; set; }
           
            public string indexNumber { get; set; }
            public string fname { get; set; }
            public string lname { get; set; }
            public string birthdate { get; set; }
            public string email { get; set; }
            public string mothersName { get; set; }
            public string fathersName { get; set; }
            public string studiesName { get; set; }
            public string studiesMode { get; set; }


            public CorrectRecord(string[] columns)
            {
                string line= String.Join(",", columns);
                if (columns.Length != 9)
                {
                    throw new Exception("Zła ilośc dostarczonych do konstruktora danych: " + line);
                }
                if (isBlankFiled(columns))
                {
                    throw new Exception("Jenda z kolumn nawiera nieprawidłową wartość: " + line);
                }
                else
                {
                    columns=sortColumns(columns);
                    this.columns = columns;
                    correctedRecord= string.Join(",", columns);
                }
            }


            public string[] sortColumns(string [] columns) 
            {
                this.indexNumber = columns[4];
                this.fname = columns[0];
                this.lname = columns[1];
                this.birthdate = columns[5];
                this.email = columns[6];
                this.mothersName = columns[7];
                this.fathersName = columns[8];
                this.studiesName = columns[2];
                this.studiesMode = columns[3];

                columns = new string[] {
                    indexNumber,
                    fname,
                    lname,
                    birthdate,
                    email,
                    mothersName,
                    fathersName,
                    studiesName,
                    studiesMode };
                return columns;
            }


            public string getCorrectedRecord()
            {
                return correctedRecord;
            }


            public bool isBlankFiled(string[] fields)
            {
                int column = 0;
                foreach (string s in fields)
                {
                    column++;
                    if (isItEmpty(s) == true)
                    {
                        Console.WriteLine(" Niepasujące pole w kolumnie " + column);
                        return true;
                    }
                }
                return false;
            }


            public bool isItEmpty(string pole)
            {
                return !(Regex.IsMatch(pole, "[a-z0-9]+", RegexOptions.IgnoreCase));
            }


            public void showDataSet()
            {
                Console.WriteLine();
                int count = 0;
                foreach (string s in columns)
                {
                    count++;
                    Console.WriteLine("Kolumna " + count + ": " + s);
                }
            }
        }



    }
}

