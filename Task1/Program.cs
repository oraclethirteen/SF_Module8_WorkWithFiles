using System;
using System.IO;

namespace Task1
{
    class Program
    {
        // ВНИМАНИЕ! Перед компиляцией необходимо разместить папку 'WorkWithFiles' с файлами для очистки на рабочем столе
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WorkWithFiles");
            
            Console.WriteLine("Путь выполнения: {0}\n", path);

            FileFolderCleaner(path);
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
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        dir.Delete(true);
                        Console.WriteLine("- Папка удалена ('{0}')", dir.Name);
                    }
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();
                        Console.WriteLine("- Файл удалён ('{0}')", file.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }

    }
}
