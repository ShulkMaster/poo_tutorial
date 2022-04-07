using System;
using System.Collections.Generic;
using System.IO;

namespace FileReader
{
    public static class Program
    {
        static void Main()
        {
            string[] data = File.ReadAllLines("./data.csv");
            var tasks = Parse(data);
            foreach (var task in tasks)
            {
                var done = task.Done ? "Si" : "No";
                Console.WriteLine($"Nombre: {task.Name} | Hecho: {done} | Fecha: {task.Date}");
            }
        }

        private static List<Task> Parse(string[] data)
        {
            if (data.Length < 2)
            {
                return new List<Task>();
            }
            
            var list = new List<Task>(data.Length - 1);
            for (var index = 1; index < data.Length; index++)
            {
                var s = data[index];
                var task = new Task();
                string[] columns = s.Split(',');
                task.Name = columns[0].Trim();
                task.Done = ParseBool(columns[1]);
                DateTime dateValue;
                
                var wasSuccess = DateTime.TryParse(columns[2], out dateValue);
                if (wasSuccess)
                {
                    task.Date = dateValue;
                }

                list.Add(task);
            }

            return list;
        }

        private static bool ParseBool(string? boolText)
        {
            if (string.IsNullOrWhiteSpace(boolText))
            {
                return false;
            }

            var trimmed = boolText.Trim();
            return trimmed.Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}