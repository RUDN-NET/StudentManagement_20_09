using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StudentManagement
{
    class Program
    {
        private const string __NamesFile = "Names.txt";

        static void Main(string[] args)
        {
            char[] cc = new[] { 'H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd', '!' };
            var s1 = "Hello";
            var s2 = "World!";

            var c0 = s1[0];
            //s1[0] = 'A'; // Нельзя!

            var str = s1 + " " + s2;

            //var s0 = "";
            //for (var i = 0; i < 10000; i++)
            //    s0 += "value=" + i + ";";

            StringBuilder builder = new StringBuilder(10000);
            for (var i = 0; i < 100; i++)
                //builder.Append($"value={i};");
                builder.AppendFormat("value={0}", i);

            if (builder.Length > 0)
                builder.Length--;
            var str_result = builder.ToString();
            //str_result = str_result.Substring(0, str_result.Length - 1);

            var compare_result = string.Compare("ABC", "ABC");

            var format_result = string.Format("{0}={1}={0}", 5, "x");

            var values = new int[10];
            for (var i = 0; i < 10; i++)
                values[i] = i;

            var str_values = string.Join("; ", values);

            var str_values_builder = new StringBuilder();
            for (var i = 0; i < values.Length; i++)
                str_values_builder.AppendFormat("{0}; ", values[i]);
            if (str_values_builder.Length > 0)
                str_values_builder.Length -= 2;
            var str_values2 = str_values_builder.ToString();

            //if(str_values != null && str_values != "")
            //if(str_values != null && str_values.Length > 0)
            if (!string.IsNullOrEmpty(str_values))
                Console.WriteLine("Строка не пуста");

            if (!string.IsNullOrWhiteSpace(str_values))
                Console.WriteLine("Строка не пуста и в ней есть что-то помимо пробелов");

            var builder2 = new StringBuilder();
            using (var writer = new StringWriter(builder2))
                writer.WriteLine();

            using (var reader = new StringReader(str_values + "\r\n" + "123"))
            {
                var str1 = reader.ReadLine();
                var str2 = reader.ReadLine();
            }

            var data_file_text = File.ReadAllText("DataFile.txt");

            //[A-Za-z][A-Za-z0-9_-]*@[A-Za-z0-9._]+\.[A-Za-z0-9]+
            // \w+@\w+\.\w+
            // \w - любая буква слова
            // \W - не (любая буква слова)
            // \d - любая цифра
            // \D - всё кроме цифр
            // \b - граница слова
            // \b\w+@\w+\.\w+\b
            
            var email_regex = new Regex(@"\w+@\w+\.\w+");

            var email_matches = email_regex.Matches(data_file_text);
            foreach (Match match in email_matches)
                Console.WriteLine("{0} - {1}", match.Value, match.Index);

            var times = Regex.Matches(data_file_text, @"(\d{2}\.\d{2}\.(\d{4}|\d{2})) (\d{2}:\d{2}:\d{2})");
            foreach (Match match in times)
            {
                var date_str = match.Groups[0];
                var time_str = match.Groups[3];

                var time = DateTime.Parse(match.Value);
                Console.WriteLine(time);
            }

            var correct_phone = "+7(916)123-45-67";
            var incorrect_phone = "Hello(916)asd123-45-67";

            var contains_correct_phone = "телефон +7(916)123-45-67 старосты";
            var not_contains_correct_phone = "телефон старосты";

            // (\+\d+|8)\(\d{3,}\)\d{2,3}-\d{2}-\d{2}

            var phone_pattern = @"(\+\d+|8)\(\d{3,}\)\d{2,3}-\d{2}-\d{2}";

            var check_phone_regex = new Regex(@"^(\+\d+|8)\(\d{3,}\)\d{2,3}-\d{2}-\d{2}$", RegexOptions.Compiled);

            if(check_phone_regex.IsMatch(correct_phone))
                Console.WriteLine("Строка {0} является телефоном", correct_phone);

            if (!check_phone_regex.IsMatch(incorrect_phone))
                Console.WriteLine("Строка {0} не является телефоном", incorrect_phone);

            var phone_number = Regex.Match(contains_correct_phone, phone_pattern);
            if(phone_number.Success)
                Console.WriteLine("Строка содержит телефонный номер {0}", phone_number.Value);

            var phone_number_no_match = Regex.Match(not_contains_correct_phone, phone_pattern);
            if(!phone_number_no_match.Success)
                Console.WriteLine("Строка не содержит номера телефона");

            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}
