using System.IO;
using System.Collections.Generic;

namespace FileReader
{
    public sealed class FileManager
    {
        public string[] ReadLines(string path)
        {
            string[] data = File.ReadAllLines(path);
            return data;
        }

        public bool SaveLines(IEnumerable<string> lines, string path)
        {
            var f = File.CreateText(path);
            using (f)
            {
                foreach (string line in lines)
                {
                    f.WriteLine(line);
                }
                try
                {
                    f.Close();
                }
                catch (IOException)
                {
                    return false;
                }

            }
            return true;
        }

    }
}
