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
            tasks.Sort((task1, task2) =>
            {
                if (task1.Date == task2.Date)
                {
                    return 0;
                }

                bool isAfter = task1.Date < task2.Date;
                if (isAfter)
                {
                    return -1;
                }

                return 1;
            });

            bool run = true;
            int selectedIndex = 0;
            const int printSize = 5;
            while (run)
            {
                Console.WriteLine("__________________ Tareas ________________________");
                var end = Math.Min(tasks.Count, selectedIndex + printSize);
                var start = Math.Max(0, end - printSize);
                for (int i = start; i < end; i++)
                {
                    var task = tasks[i];
                    var done = task.Done ? "Si" : "No";
                    if (selectedIndex == i)
                    {
                        Console.WriteLine($"=> Nombre: {task.Name} | Hecho: {done} | Fecha: {task.Date:dddd dd MMMM yyyy}");
                        continue;
                    }

                    Console.WriteLine($"   Nombre: {task.Name} | Hecho: {done} | Fecha: {task.Date:dddd dd MMMM yyyy}");
                }

                Console.WriteLine("Flechas [Up] [Down]  ENTER para completar     Q salir\n\n\n");
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (selectedIndex - 1 < 0)
                        {
                            Console.Beep();
                            break;
                        }

                        selectedIndex--;
                        break;
                    }
                    case ConsoleKey.DownArrow:
                    {
                        if (selectedIndex + 1 >= tasks.Count)
                        {
                            Console.Beep();
                            break;
                        }

                        selectedIndex++;
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        var task = tasks[selectedIndex];
                        task.Done = !task.Done;
                        break;
                    }
                    case ConsoleKey.Q:
                    {
                        run = false;
                        SaveChanges(tasks);
                        break;
                    }
                }
            }
        }

        private static void SaveChanges(List<Task> tasks)
        {
            string[] data = Parse(tasks);
            var f = File.CreateText("./data.csv");
            foreach (var s in data)
            {
                f.WriteLine(s);
            }

            f.Close();
            f.Dispose();
        }

        private static string[] Parse(List<Task> tasks)
        {
            string[] stringArray = new string[tasks.Count];
            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks[i];
                stringArray[i] = $"{t.Name},{t.Done},{t.Date.ToString("dd/MM/yyyy")}";
            }

            return stringArray;
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