using Task2 = FileReader.Task;

namespace Todoer
{
    internal static class TaskParser
    {
        public static List<Task2> Parse(IEnumerable<string> data)
        {
            var size = data.Count();
            if (size < 2) return new List<Task2>();

            var list = new List<Task2>(size - 1);
            var enumerator = data.GetEnumerator();
            enumerator.MoveNext();
            for (int i = 1; i < size; i++)
            {
                enumerator.MoveNext();
                var item = enumerator.Current;
                var task = new Task2();
                string[] columns = item.Split(',');
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
            enumerator.Dispose();
            return list;
        }

        static bool ParseBool(string? boolText)
        {
            if (string.IsNullOrWhiteSpace(boolText))
            {
                return false;
            }

            var trimmed = boolText.Trim();
            return trimmed.Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        static IEnumerable<string> Parse(List<Task2> tasks)
        {
            return tasks.Select(t => $"{t.Name},{t.Done},{t.Date:dd/MM/yyyy}"); ;
        }
    }
}
