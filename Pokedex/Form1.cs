using Pokedex.Api;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            var api = new PokeApi();
            var xd = await api.GetEntriesAsync();
            var text = "";
            foreach (var entry in xd.Results)
            {
                text += $"\n{entry.Name}";
            }
            label1.Text = text;
        }
    }
}