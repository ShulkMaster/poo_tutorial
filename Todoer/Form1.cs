using Task2 = FileReader.Task;
using FileReader;

namespace Todoer
{
    public partial class Form1 : Form
    {
        private List<Task2> list = new List<Task2>();
        public Form1()
        {

            InitializeComponent();
            var fm = new FileManager();
            IEnumerable<string> data = fm.ReadLines("data.csv");
            list = TaskParser.Parse(data);
            foreach (var row in list)
            {
                string[] rowData = {
                    row.Name,
                    row.Date.ToString("dd/MM/yyyy"),
                    IsDone(row.Done),
                };
                var dataRow = new ListViewItem(rowData);
                listView1.Items.Add(dataRow);
            }
            listView1.Columns[0].Width = -1;
            FormClosing += new FormClosingEventHandler(Autosave);
        }

        private static string IsDone(bool b) => b ? "Si" : "No";

        private void Autosave(object? sender, FormClosingEventArgs e)
        {
            SaveToolStripMenuItem_Click(sender, e);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
            {
                textBox1.Text = string.Empty;
                monthCalendar1.SelectionRange.Start = DateTime.Now;
                checkBox1.Checked = false;
                SaveBtn.Text = "Agregar";
                return;
            };
            var index = listView1.SelectedIndices[0];
            var t = list[index];
            SaveBtn.Text = "Guardar";
            textBox1.Text = t.Name;
            monthCalendar1.SelectionStart = t.Date;
            checkBox1.Checked = t.Done;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 1)
            {
                var selected = listView1.SelectedIndices[0];
                var newTask = list[selected];
                newTask.Name = textBox1.Text.Trim();
                newTask.Date = monthCalendar1.SelectionStart.Date;
                newTask.Done = checkBox1.Checked;
                listView1.Items[selected] = new ListViewItem(new[]{
                    newTask.Name,
                    newTask.Date.ToString("dd/MM/yyyy"),
                    IsDone(newTask.Done),
                });
                return;
            }

            var taskName = textBox1.Text.Trim();
            var date = monthCalendar1.SelectionStart.Date;
            var check = checkBox1.Checked;
            var t = new Task2 { Date = date, Name = taskName, Done = check };
            list.Add(t);
            string[] rowData = {
                    taskName,
                    date.ToString("dd/MM/yyyy"),
                    IsDone(check),
                };
            var dataRow = new ListViewItem(rowData);
            listView1.Items.Add(dataRow);
        }

        private void SaveToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var data = TaskParser.Parse(list);
            new FileManager().SaveLines(data, "data.csv");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToolStripMenuItem_Click(sender, e);
            Close();
        }
    }
}