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
                    isDone(row.Done),
                };
                var dataRow = new ListViewItem(rowData);
                listView1.Items.Add(dataRow);
            }
        }

        private static string isDone(bool b) => b ? "Si" : "No";
 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InputPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}