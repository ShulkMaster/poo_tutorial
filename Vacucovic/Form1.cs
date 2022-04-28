namespace Vacucovic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MouseMove += new MouseEventHandler(onMouseMoved);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
        }

        private void onMouseMoved(object? sender, MouseEventArgs e)
        {
            var x = e.Location.X;
            var y = e.Location.Y;
            int indexOf = label1.Text.IndexOf('(');
            string subMessage;
            if (indexOf == -1) {
                subMessage = textBox1.Text;
            } else {
                subMessage = "";
            }
            label1.Text = $"{subMessage} ({x}, {y})";
        }

        private void label1_Click(object sender, EventArgs e)
        { }



      

         private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}