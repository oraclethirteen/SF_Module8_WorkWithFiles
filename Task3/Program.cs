using System;
using System.IO;

namespace Task3
{
    class Program
    {
        // ВНИМАНИЕ! Перед компиляцией необходимо разместить папку 'WorkWithFiles' с файлами для вычисления объема и очистки на рабочем столе
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WorkWithFiles");

            Console.WriteLine("Путь выполнения: {0}\n", path);

            double fileFolderSizeOld = 0;
            double fileFolderSizeNew = 0;

            FileFolderCounter(path, ref fileFolderSizeOld);

            Console.WriteLine("Исходынй размер папки: {0} байт\n", fileFolderSizeOld);

            FileFolderCleaner(path);
            FileFolderCounter(path, ref fileFolderSizeNew);

            Console.WriteLine("\nОсвобождено: {0} байт\n", fileFolderSizeOld);
            Console.WriteLine("Текущий размер папки: {0} байт", fileFolderSizeNew);
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

        static void FileFolderCleaner(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Указанного пути не существует");
                return;
            }

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    dir.Delete(true);
                    Console.WriteLine("- Папка удалена ('{0}')", dir.Name);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    file.Delete();
                    Console.WriteLine("- Файл удалён ('{0}')", file.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }
    }
}

