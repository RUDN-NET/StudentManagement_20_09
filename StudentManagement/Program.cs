using System;
using System.IO;

namespace StudentManagement
{
    class Program
    {
        private const string __NamesFile = "Names.txt";

        static void Main(string[] args)
        {
            using (StreamReader names_reader = new StreamReader(__NamesFile))
            {
                while (!names_reader.EndOfStream)
                {
                    var line = names_reader.ReadLine();

                    Console.WriteLine(line);
                }

                //names_reader.Close();
                //names_reader.Dispose();
            }


            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
