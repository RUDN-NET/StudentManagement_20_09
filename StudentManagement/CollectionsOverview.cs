using System.Collections;
using System.Collections.Generic;

namespace StudentManagement
{
    static class CollectionsOverview
    {
        public static void Start()
        {
            var complex_array = new ComplexStruct[100000];

            var complex_list = new List<ComplexStruct>();

            //System.Collections.ArrayList list = new ArrayList();

            //list.Add("String value");
            //list.Add(5);
            //list.Add(3.14);

            //var string_value = (string) list[0];
            //var int_value = (int) list[1];
            //var double_value = (double) list[2];

            //object object_value = list[0];
            //object_value = list[1];
            //object_value = list[2];

            //System.Collections.Generic.List<object> list = new List<object>();
            var string_array_list = new ArrayList();
            for (var i = 0; i < 10; i++)
            {
                string_array_list.Add($"Str value = {i + 1}");
            }
            string str = (string)string_array_list[0]; // требуется приведение типов из (object) в (string)

            var string_list = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                string_list.Add($"Str value = {i + 1}");
            }
            string str2 = string_list[0]; // в приведении типов нет необходимости.

            var strings_queue = new Queue<string>();
            var strings_stack = new Stack<string>();

            // 0. Массив // коллекция элементов фиксированной длины - с произвольным доступом к элементам по их индексу
            // 1. Список // коллекция элементов переменной длины - с произвольным доступом к элементам по их индексу
            // 2. Стек // Доступ к элементу осуществляется с одного конца как на чтение так и на запись - по индексу элементы не доступны 
            //    LIFO - Last In First Out
            // 3. Очередь // Добавление элементов с одного конца, извлечение - с другого.
            //    FIFO - First In First Out
            // 4. Словарь (хеш-таблица) - набор пар ключ-значение. Все ключи должны быть уникальны
            //    доступ к элементу словаря осуществляется за константное время

            var studetns_queue = new Queue<Student>(); // Очередь из студентов
            var students_stack = new Stack<Student>(); // Стек из студентов

            var student_homonyms = new Dictionary<string, List<Student>>(); // Словарь студентов-однофамильцев
        }
    }
}
