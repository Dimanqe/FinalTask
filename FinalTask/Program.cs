using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceFilePath = @"C:\Users\dsank\Desktop\Students.dat";
            string targetDirectory = @"C:\Users\dsank\Desktop\Students";
            Directory.CreateDirectory(targetDirectory);
            ReadAndSaveStudents(sourceFilePath, targetDirectory);
        }
        static void ReadAndSaveStudents(string sourceFilePath, string targetDirectory)
        {
            if (File.Exists(sourceFilePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(sourceFilePath, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Student[] students = (Student[])formatter.Deserialize(fs);
                        foreach (var student in students)
                        {
                            string groupFilePath = Path.Combine(targetDirectory, $"{student.Group}.txt");
                            using (StreamWriter sw = File.AppendText(groupFilePath))
                            {
                                sw.WriteLine($"{student.Name}, {student.DateOfBirth.ToShortDateString()}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при десериализации и сохранении: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Исходный бинарный файл не существует.");
            }
        }
    }
}
