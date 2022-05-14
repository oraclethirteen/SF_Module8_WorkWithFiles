using System;
using System.IO;

namespace Task2
{
    class Program
    {
        // ВНИМАНИЕ! Перед компиляцией необходимо разместить папку 'WorkWithFiles' с файлами для вычисления объема на рабочем столе
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WorkWithFiles");
            
            Console.WriteLine("Путь выполнения: {0}\n", path);

            double fileFolderSize = 0;

            FileFolderCounter(path, ref fileFolderSize);

            Console.WriteLine("Размер папки: {0} байт", fileFolderSize);
        }

        static void FileFolderCounter(string path, ref double size)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Указанного пути не существует");
                return;
            }

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (var file in dirInfo.GetFiles())
                {
                    size += file.Length;
                }

                foreach (var dir in dirInfo.GetDirectories())
                {
                    FileFolderCounter(dir.FullName, ref size);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }
    }
}
