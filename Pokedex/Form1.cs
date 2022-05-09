using Pokedex.Api.Request;
using Pokedex.Repositories;
using Pokedex.Repositories.Dto;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        private const int pageSize = 5;
        private int page = 1;
        private readonly EntryRepo repo = new EntryRepo();
        private CancellationTokenSource? cancelable;
        public Form1()
        {
            InitializeComponent();
        }

        private void SetView(PokeList list, QueryParams q)
        {

            page = q.Offset / q.Limit;
            dataGridView1.DataSource = list.Pokemons;
            label2.Text = $"Pagina {page} of {Math.Ceiling(list.Total / (float)q.Limit)}";
        }

        private void EnableControls(bool enable = true)
        {
            BtnPrevious.Enabled = enable;
            BtnNext.Enabled = enable;
            BtnCancel.Enabled = !enable;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var r = new PokeRepo();
            var pokemon = r.FindAsync(new Entry { Id = 25 });
            pokemon.GetAwaiter().OnCompleted(() =>
            {
                //label3.Text = pokemon?.Result?.Name;
            });
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            await LoadPokemon(page + 1);
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            await LoadPokemon(page - 1);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            cancelable?.Cancel();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadPokemon(1);
        }

        private async Task LoadPokemon(int newPage)
        {
            if (page < 1 || cancelable is not null) return;
            EnableControls(false);
            cancelable = new CancellationTokenSource();
            var r = new PokeRepo();
            var q = new QueryParams(pageSize);
            try
            {
                await Task.Delay(5000, cancellationToken: cancelable.Token);
                var data = await r.FindRangeAsync(q);
                SetView(data, q);
                page = newPage;
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message, nameof(TaskCanceledException));
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message, nameof(OperationCanceledException));
            }
            finally
            {
                cancelable.Dispose();
                cancelable = null;
                EnableControls();
            }
        }
    }
}