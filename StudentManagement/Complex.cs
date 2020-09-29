namespace StudentManagement
{
    static class ComplexTest
    {
        public static void Start()
        {
            ComplexStruct struct_copmlex = new ComplexStruct(); // значимый тип данных - располагается на стеке вызова внутри процедуры Start
            ComplexClass class_complex = new ComplexClass(); // ссылочный тип данных - объекты располагаются в "куче"

            // Копирование значения значимого типа данных из одной переменной в другую - длительный процесс если много полей и свойств надо скопировать
            // Создание новых объектов значимого типа быстрее, чем для ссылочного типа
            // Удаление из памяти для значимого типа гораздо быстрее
            // Но! Объём памяти стека ооочень маленький!
            var a = new ComplexStruct();
            a.Re = 5;
            a.Im = 7;

            var b = new ComplexStruct();
            b.Re = 10;
            b.Im = -7;

            var a1 = a;
            a.Re = 0;

            // Копирование ссылочного значения из одной переменной в другую - вопрос комирования 4 байт ссылки. Очень быстро.

            var x = new ComplexClass();
            x.Re = 5;
            x.Im = 7;

            var x1 = x;
            x.Re = 0;

            var y = new ComplexClass();
            y.Re = 10;
            y.Im = -7;
        }
    }

    struct ComplexStruct
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