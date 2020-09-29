namespace StudentManagement
{
    static class ComplexTest
    {
        public static void Start()
        {
            Complex struct_copmlex = new Complex(); // значимый тип данных - располагается на стеке вызова внутри процедуры Start
            ComplexClass class_complex = new ComplexClass(); // ссылочный тип данных - объекты располагаются в "куче"
        }
    }

    struct Complex
    {
        public double Re { get; set; }

        public double Im { get; set; }
    }

    class ComplexClass
    {
        public double Re { get; set; }

        public double Im { get; set; }
    }
}