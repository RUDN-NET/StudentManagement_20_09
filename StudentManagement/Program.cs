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

            var students = GetStudents(surnames, names, patronymics, 1000);

            const string students_data_file = "students.csv";
            SaveToFile(students, students_data_file);

            var students2 = ReadFromFile(students_data_file);

            SortStudentsByAverageRating(students2);

            var homonyms = GetHomonyms(students2);

            var birthdays = new Dictionary<string, List<Student>>();

            foreach (var student in students2)
            {
                var date = student.Birthday.ToString("dd.MM");
                if(birthdays.ContainsKey(date))
                    birthdays[date].Add(student);
                else
                    birthdays.Add(date, new List<Student> { student });
            }

            foreach (var birthday in birthdays)
            {
                if (birthday.Value.Count > 1)
                {
                    Console.WriteLine(birthday.Key);
                    foreach (var student in birthday.Value)
                        Console.WriteLine("\t{0} {1} {2} ({3:dd.MM.yyyy} - возраст {4})",
                            student.Surname, student.Name, student.Patronymic,
                            student.Birthday, student.Age);
                }
            }

            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }

        private static Dictionary<string, List<Student>> GetHomonyms(Student[] Students)
        {
            var homonyms = new Dictionary<string, List<Student>>();

            foreach (var student in Students)
            {
                if(homonyms.ContainsKey(student.Surname))
                    homonyms[student.Surname].Add(student);
                else
                    homonyms.Add(student.Surname, new List<Student> {student});
            }

            return homonyms;
        }

        private static void SaveToFile(Student[] Students, string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                for (var i = 0; i < Students.Length; i++)
                {
                    var student = Students[i];
                    writer.WriteLine("{0};{1};{2};{3};{4}",
                        student.Surname, student.Name, student.Patronymic,
                        student.Birthday.ToShortDateString(),
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

        private static void PrintBestLastStudents(Student[] Students)
        {
            Console.WriteLine("10% лучших студентов:");
            var best_count = Students.Length / 10;

            for (var i = 0; i < best_count; i++)
            {
                Console.WriteLine("{0} - {1}", i, Students[i]);
            }

            Console.WriteLine();
            Console.WriteLine("10% худших студентов:");
            var last_count = Students.Length / 10;

            for (var i = Students.Length - last_count; i < Students.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i, Students[i]);
            }
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
                    student.Birthday = DateTime.Parse(components[3]);

                    var ratings_count = components.Length - 4;

                    var ratings = new List<int>(ratings_count);
                    for(var i = 0; i < ratings_count; i++)
                        ratings.Add(int.Parse(components[i + 4]));

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

                //DateTime.DaysInMonth()

                var year = rnd.Next(1995, 2003);
                var month = rnd.Next(1, 13);
                var day = rnd.Next(1, DateTime.DaysInMonth(year, month) + 1);

                student.Birthday = new DateTime(year, month, day);

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
