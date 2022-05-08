using Pokedex.Repositories;
using Pokedex.Api.Request;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        private int page =  1;
        private readonly EntryRepo repo = new EntryRepo();
        public Form1()
        {
            InitializeComponent();
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            var q = new QueryParams();
            q.SetPage(page);
            var response = await repo.GetEntriesAsync(q);
            var text = "";
            foreach (var entry in response.Entries)
            {
                text += $"\n{entry.Name}";
            }
            label2.Text = $"Pagina {page} of {Math.Ceiling(response.Total / (float) q.Limit)}";
            label1.Text = text;
            page++;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var r = new PokeRepo();
            var pokemon = r.FindAsync(new Entry {  Id = 25 });
            pokemon.GetAwaiter().OnCompleted(() => {
                label3.Text = pokemon?.Result?.Name;
            });
        }
    }
}