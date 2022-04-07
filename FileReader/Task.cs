using System;

namespace FileReader
{
    public class Task
    {
        public string Name { get; set; } = String.Empty;

        public bool Done { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}