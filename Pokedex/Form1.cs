using Pokedex.Api.Request;
using Pokedex.Repositories;
using Pokedex.Repositories.Dto;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        private const int pageSize = 10;
        private int page = 1;
        private readonly EntryRepo repo = new EntryRepo();
        private CancellationTokenSource? cancelable;
        public Form1()
        {
            InitializeComponent();
        }

        private void SetView(PokeList list, QueryParams q, Dictionary<int, Bitmap> pics)
        {

            page = 1 + q.Offset / q.Limit;
            dataGridView1.DataSource = list.Pokemons;
            label2.Text = $"Pagina {page} of {Math.Ceiling(list.Total / (float)q.Limit)}";
            var count = dataGridView1.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                var pokemon = list.Pokemons[i];
                if (pokemon is null) { continue; }
                dataGridView1.Rows[i].Cells["picture"].Value = pics[pokemon.Id];
                dataGridView1.Rows[i].Height = 200;
            }
        }

        private void EnableControls(bool enable = true)
        {
            BtnPrevious.Enabled = enable;
            BtnNext.Enabled = enable;
            BtnCancel.Enabled = !enable;
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
            if (newPage < 1 || cancelable is not null) return;
            EnableControls(false);
            cancelable = new CancellationTokenSource();
            var r = new PokeRepo();
            var sRepo = new SpriteRepository();
            var q = new QueryParams(pageSize);
            q.SetPage(newPage);
            try
            {
                var data = await r.FindRangeAsync(q, cancelable.Token);
                var pics = await sRepo.GetAllDefaultSprites(data.Pokemons, cancelable.Token);
                SetView(data, q, pics);
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