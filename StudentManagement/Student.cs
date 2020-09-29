using System;
using System.Collections.Generic;

namespace StudentManagement
{
    class Student
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public List<int> Ratings { get; set; } = new List<int>();

        public DateTime Birthday { get; set; }

        public int RatingsCount
        {
            get
            {
                return Ratings.Count;
            }
        }

        public int RatingsSum
        {
            get
            {
                var sum = 0;
                for (var i = 0; i < RatingsCount; i++)
                    sum += Ratings[i];
                return sum;
            }
        }

        public double RatingsAverage
        {
            get
            {
                return (double)RatingsSum / RatingsCount;
            }
        }

        public int Age
        {
            get
            {
                var delta = DateTime.Today - Birthday;
                var days_count = (int)delta.TotalDays;
                var years_count = days_count / ((365 * 3 + 366) / (double)4);
                var age = (int)Math.Ceiling(years_count); // Округление вверх
                //Math.Floor() // Округление вниз
                //Math.Round() // Математическое округление
                return age;
            }
        }

        public override string ToString()
        {
            return $"Студент {Surname} {Name} {Patronymic} [{Birthday:dd.MM.yyyy} - {Age} лет] ({RatingsAverage:f3}): {string.Join(", ", Ratings)}";
        }

        //public static implicit operator double(Student student) // Оператор неявного преобразования типа данных "Студент" в тип данных "вещественное число двойной точности"
        //{
        //    return student.RatingsAverage;
        //}

        public static explicit operator double(Student student) // Оператор явного преобразования типа данных "Студент" в тип данных "вещественное число двойной точности"
        {
            return student.RatingsAverage;
        }
    }
}
