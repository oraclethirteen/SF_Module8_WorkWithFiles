using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        // ВНИМАНИЕ! Перед компиляцией необходимо разместить файл 'Students.dat' на рабочем столе
        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Console.WriteLine("Путь выполнения: {0}\n", path);

            var pathDir = Path.Combine(path, "Students");
            var pathFile = Path.Combine(path, "Students.dat");

            try
            {
                DirectoryCreate(pathDir);
                DirectorySort(ref pathDir, ref pathFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }

        // Метод создающий папку 'Students' на рабочем столе
        static void DirectoryCreate(string pathDir)
        {
            if (Directory.Exists(pathDir))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(pathDir);
                dirInfo.Delete(true);
            }

            Directory.CreateDirectory(pathDir);

            Console.WriteLine("Папка 'Students' создана\n");
        }

        // Метод десериализующий данные из файла 'Students.dat'
        static Student[] FileSerializer(string pathFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream(pathFile, FileMode.Open))
            {
                var students = (Student[])formatter.Deserialize(fs);

                Console.WriteLine("Файл 'Students.dat' десериализован - данные получены\n");

                return students;
            }
        }

        // Метод сортирующий десериализованные данные в папке 'Students'
        static void DirectorySort(ref string pathDir, ref string pathFile)
        {
            var students = FileSerializer(pathFile);

            foreach (var student in students)
            {
                string path = Path.Combine(pathDir, student.Group + ".txt");

                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        foreach (var stud in students)
                        {
                            if (stud.Group == student.Group)
                            {
                                sw.WriteLine("{0} [ {1} ]", stud.Name, stud.DateOfBirth.ToShortDateString());
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Данные отсортированы\n");

            string[] files = Directory.GetFiles(pathDir);

            foreach (var data in files)
            {
                Console.WriteLine(data);
            }
        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string Name, string Group, DateTime DateOfBirth)
        {
            this.Name = Name;
            this.Group = Group;
            this.DateOfBirth = DateOfBirth;
        }
    }
}