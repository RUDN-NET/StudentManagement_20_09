using System;
using System.IO;

namespace StudentManagement
{
    class Program
    {
        private const string __FileName = "Names.txt";

        private static void TestFileInfo()
        {
            var names_file_info = new FileInfo(__FileName);

            if (!names_file_info.Exists)
            {
                Console.WriteLine("Файл {0} не существует", names_file_info);
                return;
            }

            names_file_info.CreationTime = new DateTime(1990, 05, 09, 15, 06, 18);
            //names_file_info.Attributes = FileAttributes.ReadOnly | FileAttributes.Compressed;

            Console.WriteLine("Дата создания {0}", names_file_info.CreationTime);
            Console.WriteLine("Дата доступа {0}", names_file_info.LastAccessTime);
            Console.WriteLine("Дата записи {0}", names_file_info.LastWriteTime);

            Console.WriteLine("Длина файла {0:f2}кБ", names_file_info.Length / 1024.0);
            //Console.WriteLine("Директория {0}", names_file_info.Directory);
            Console.WriteLine("Директория {0}", names_file_info.DirectoryName);
            Console.WriteLine("Только для чтения: {0}", names_file_info.IsReadOnly);
            Console.WriteLine("Атрибуты {0}", names_file_info.Attributes);

            var names2_file_info = names_file_info.CopyTo("names2.txt", true);

            names_file_info.Delete();

            names_file_info.Refresh();

            if (!names_file_info.Exists)
            {
                Console.WriteLine("Файл удалён");
            }
            else
            {
                Console.WriteLine("Файл выжил");
            }
        }

        static void Main(string[] args)
        {
            TestFileInfo();

            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
