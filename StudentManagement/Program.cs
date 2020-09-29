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
            //ComplexTest.Start();
            CollectionsOverview.Start();

            GetNames(out var surnames, out var names, out var patronymics);

            var students = GetStudents(surnames, names, patronymics, 100);

            const string students_data_file = "students.csv";
            SaveToFile(students, students_data_file);

            var students2 = ReadFromFile(students_data_file);

            var student = students2[0];

            //double rating = student;
            //double rating2 = (double)student;

            SortStudentsByAverageRating(students2);

            //for (var i = 0; i < students2.Length; i++)
            //    Console.WriteLine(students2[i]);

            Console.WriteLine("10% лучших студентов:");
            var best_count = students2.Length / 10;

            for (var i = 0; i < best_count; i++)
            {
                Console.WriteLine("{0} - {1}", i, students2[i]);
            }

            Console.WriteLine();
            Console.WriteLine("10% худших студентов:");
            var last_count = students2.Length / 10;

            for (var i = students2.Length - last_count; i < students2.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i, students2[i]);
            }

            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }

        private static void SaveToFile(Student[] Students, string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                for (var i = 0; i < Students.Length; i++)
                {
                    var student = Students[i];
                    writer.WriteLine("{0};{1};{2};{3}",
                        student.Surname, student.Name, student.Patronymic,
                        string.Join(";", student.Ratings));
                }
            }
        }

        private static void SortStudentsByAverageRating(Student[] Students)
        {
            Array.Sort(Students, (s1, s2) =>
            {
                var rating1 = s1.RatingsAverage;
                var rating2 = s2.RatingsAverage;
                if (rating1 > rating2) return -1;
                else if (rating1 < rating2) return +1;
                else return 0;
            });
        }

        private static Student[] ReadFromFile(string FileName)
        {
            using (var reader = new StreamReader(FileName))
            {
                var students = new List<Student>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if(string.IsNullOrWhiteSpace(line)) 
                        continue;

                    var components = line.Split(";");
                    var student = new Student();
                    student.Surname = components[0];
                    student.Name = components[1];
                    student.Patronymic = components[2];

                    var ratings_count = components.Length - 3;

                    var ratings = new List<int>(ratings_count);
                    for(var i = 0; i < ratings_count; i++)
                        ratings.Add(int.Parse(components[i + 3]));

                    student.Ratings = ratings;

                    students.Add(student);
                }

                return students.ToArray();
            }
        }

        private static Student[] GetStudents(List<string> Surnames, List<string> Names, List<string> Patronymics, int Count)
        {
            var students = new Student[Count];

            var rnd = new Random();
            for (var i = 0; i < students.Length; i++)
            {
                var student = new Student();
                student.Surname = Surnames[rnd.Next(Surnames.Count)];
                student.Name = Names[rnd.Next(Names.Count)];
                student.Patronymic = Patronymics[rnd.Next(Patronymics.Count)];

                student.Ratings = GetRandomRatings(rnd, rnd.Next(5, 20));

                students[i] = student;
            }

            return students;
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
