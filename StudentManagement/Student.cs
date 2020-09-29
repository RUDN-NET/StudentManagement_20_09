using System.Collections.Generic;

namespace StudentManagement
{
    class Student
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public List<int> Ratings { get; set; } = new List<int>();

        public override string ToString()
        {
            return $"Студент {Surname} {Name} {Patronymic}: {string.Join(", ", Ratings)}";
        }
    }
}
