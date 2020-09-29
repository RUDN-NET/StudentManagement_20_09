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
            GetNames(out var surnames, out var names, out var patronymics);

            var students = new Student[1000];

            var rnd = new Random();
            for (var i = 0; i < students.Length; i++)
            {
                var student = new Student();
                student.Surname = surnames[rnd.Next(surnames.Count)];
                student.Name = names[rnd.Next(names.Count)];
                student.Patronymic = patronymics[rnd.Next(patronymics.Count)];

                student.Ratings = GetRandomRatings(rnd, rnd.Next(5, 20));

                students[i] = student;

                Console.WriteLine(student);
            }

            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }

        private static void GetNames(out List<string> Surnames, out List<string> Names, out List<string> Patronymics)
        {
            Surnames = new List<string>();
            Names = new List<string>();
            Patronymics = new List<string>();

            using (var names_reader = new StreamReader(__NamesFile))
            {
                while (!names_reader.EndOfStream)
                {
                    var line = names_reader.ReadLine();

                    var components = line.Split(' ');

                    var surname = components[0];
                    var name = components[1];
                    var patronymic = components[2];

                    Surnames.Add(surname);
                    Names.Add(name);
                    Patronymics.Add(patronymic);
                }
            }
        }

        private static List<int> GetRandomRatings(Random rnd, int Count)
        {
            var ratings = new List<int>(Count);
            for(var i = 0; i < Count; i++)
                ratings.Add(rnd.Next(0, 101)); // [0, 101) = [0, 100]

            return ratings;
        }
    }
}
