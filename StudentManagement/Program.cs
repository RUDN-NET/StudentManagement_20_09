using System;
using System.Collections.Generic;
using System.IO;

namespace StudentManagement
{
    class Program
    {
        private const string __NamesFile = "Names.txt";

        static void Main(string[] args)
        {
            var surnames = new List<string>();
            var names = new List<string>();
            var patronymics = new List<string>();

            using (StreamReader names_reader = new StreamReader(__NamesFile))
            {
                while (!names_reader.EndOfStream)
                {
                    var line = names_reader.ReadLine();

                    var components = line.Split(' ');

                    var surname = components[0];
                    var name = components[1];
                    var patronymic = components[2];

                    surnames.Add(surname);
                    names.Add(name);
                    patronymics.Add(patronymic);

                    //Console.WriteLine(line);
                }

                //names_reader.Close();
                //names_reader.Dispose();
            }


            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
