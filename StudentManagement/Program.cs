using System;
using System.IO;
using System.Text;

namespace StudentManagement
{
    class Program
    {
        private const string __FileName = "Names.txt";
        private const string __TestDir = "TestDir";

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

        private static void TestDirectoryInfo()
        {
            var current_dir = new DirectoryInfo(".");
            var current_dir2 = new DirectoryInfo(Environment.CurrentDirectory);

            //File.Exists()
            //Directory.Move();

            //foreach (var json_file in current_dir.GetFiles("*.json"))
            //{
            //    Console.WriteLine(json_file);
            //}

            //Console.ReadLine();

            var test_dir = new DirectoryInfo(__TestDir);

            //test_dir.Delete(true);

            //foreach (var file in test_dir.GetFiles())
            //    Console.WriteLine("{0} - {1:f2}кБ", file.Name, file.Length / 1024.0);

            PrintFileOfDir(test_dir);

            Console.WriteLine("Полная длина файлов во вложенных директориях {0:f3}Б",
                GetTotalFilesLength(test_dir));

            RenameFilesAndDirs(test_dir);
        }

        private static void PrintFileOfDir(DirectoryInfo dir)
        {
            Console.WriteLine(dir);
            foreach (var file in dir.GetFiles())
                Console.WriteLine("{0} - {1:f2}кБ", file.Name, file.Length / 1024.0);
            Console.WriteLine();

            foreach (var sub_dir in dir.GetDirectories())
                PrintFileOfDir(sub_dir);
        }

        private static long GetTotalFilesLength(DirectoryInfo dir)
        {
            var length = 0l;

            foreach (var file in dir.GetFiles())
                length += file.Length;

            foreach (var sub_dir in dir.GetDirectories())
                length += GetTotalFilesLength(sub_dir);

            return length;
        }

        private static void RenameFilesAndDirs(DirectoryInfo dir)
        {
            var file_template = $"File{{0}}[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].txt";
            var index = 0;
            foreach (var file in dir.GetFiles())
            {
                var new_file_name = dir.FullName + "\\" + string.Format(file_template, index++);
                Console.WriteLine("Переношу содержимое файла");
                Console.WriteLine("\tиз {0}", file.FullName);
                Console.WriteLine("\tв {0}", new_file_name);
                file.MoveTo(new_file_name);
            }

            var dir_template = $"Dir{{0}}[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}]";
            index = 0;
            foreach (var sub_dir in dir.GetDirectories())
            {
                RenameFilesAndDirs(sub_dir);
                var new_dir_name = dir.FullName + "\\" + string.Format(dir_template, index++);
                Console.WriteLine("Переношу содержимое директории");
                Console.WriteLine("\tиз {0}", sub_dir.FullName);
                Console.WriteLine("\tв {0}", new_dir_name);
                sub_dir.MoveTo(new_dir_name);
            }
        }

        private static void TestDriveInfo()
        {
            var c_drive = new DriveInfo("c:\\");

            var system_drives = DriveInfo.GetDrives();

            foreach (var drive in system_drives)
            {
                var root = drive.RootDirectory;
                Console.WriteLine("Диск: {0}", drive.Name);

                PrintDirectory(root, 0, 3);
            }
        }

        private static void PrintDirectory(DirectoryInfo dir, int Level, int MaxLevel)
        {
            if (Level >= MaxLevel) return;

            Console.WriteLine(dir);

            try
            {
                var sub_dirs = dir.GetDirectories();
                var files = dir.GetFiles();

                var offset = new string(' ', Level * 3);

                foreach (var sub_dir in sub_dirs)
                    Console.WriteLine("{0}[d]{1}", offset, sub_dir.Name);

                foreach (var file in files)
                    Console.WriteLine("{0}[f]{1} - {2}b", offset, file.Name, file.Length);

                foreach (var sub_dir in sub_dirs)
                    PrintDirectory(sub_dir, Level + 1, MaxLevel);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Доступ к каталогу {0} запрещён", dir);
            }
        }

        private static void ReadingFile()
        {
            var data_file = new FileInfo(__FileName);

            //var new_file = new FileInfo("new_file.txt");

            //Stream data_stream = data_file.Open(FileMode.Open);

            //var buffer = new byte[1024];
            //var readed = data_stream.Read(buffer, 0, buffer.Length);

            //var new_file_stream = new_file.Create();
            //new_file_stream.Write(buffer, 0, readed);

            //StreamReader reader = new StreamReader(data_stream, Encoding.ASCII);
            //StreamWriter writer = new StreamWriter(new_file_stream, Encoding.UTF8);

            //var str = reader.ReadLine();
            //var all_text = reader.ReadToEnd();

            //writer.Write("Hello World");
            //writer.WriteLine("123");
            //writer.Flush();

            //writer.Close();
            //reader.Close();

            //data_stream.Close();
            //new_file_stream.Close();

            //FileStream data_stream1 = null;
            //try
            //{
            //    data_stream1 = data_file.OpenRead();

            //    // Содержимое контракции using(...) { ... }
            //}
            //finally
            //{
            //    if (data_stream1 != null)
            //        data_stream1.Dispose();
            //}

            using (var data_stream = data_file.OpenRead())
            using (var new_file_stream = new FileStream("new_file.txt", FileMode.Create, FileAccess.Write))
            //using (var reader = new StreamReader(data_stream, Encoding.ASCII))
            //using (var writer = new StreamWriter(new_file_stream, Encoding.UTF8))
            {
                const int buffer_length = 10; // 1024 * 10; 1024 * 1024
                var buffer = new byte[buffer_length];
                long total_readed = 0;
                var stream_length = data_stream.Length;
                while (true)
                {
                    var readed = data_stream.Read(buffer, 0, buffer_length);
                    if (readed == 0) break;

                    new_file_stream.Write(buffer, 0, readed);

                    total_readed += readed;
                    Console.WriteLine("Скопировано {0:p}", total_readed / (double)stream_length);
                }
            }

            using (var bin_reader = new BinaryReader(data_file.OpenRead()))
            using (var bin_writer = new BinaryWriter(new FileStream("new_file.bin", FileMode.Create, FileAccess.Write)))
            {
                bin_reader.ReadInt32();
            }

        }

        static void Main(string[] args)
        {
            var file_path = Path.Combine(@"c:\", @"123\TestDir\", "TestFile.txt");
            var dir_name = Path.GetDirectoryName(file_path);
            var file_name = Path.GetFileName(file_path);
            var file_extension = Path.GetExtension(file_path);

            var file_with_new_ext = Path.ChangeExtension(file_path, ".exe");

            var path_components = file_path.Split('\\', '/');

            var file_name1 = path_components[path_components.Length - 1];
            var dot_index = file_name1.IndexOf('.');
            var extension = file_name1.Substring(dot_index);

            //TestFileInfo();
            //TestDirectoryInfo();
            //TestDriveInfo();
            ReadingFile();

            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }

    }
}
