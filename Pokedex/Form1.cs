using Pokedex.ViewModels;
using System.ComponentModel;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        private const int pageSize = 10;
        private int page = 1;
        private readonly MainViewModel vm = new MainViewModel();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnableControls(false);
            for (int i = 0; i < pageSize; i++)
            {
                dataGridView1.Rows.Add();
            }
            vm.PropertyChanged += SetView;
            vm.LoadPokemon(1, pageSize);
            FormClosing += (_, _) =>
            {
                vm.PropertyChanged -= SetView;
            };
        }

        private void SetView(object? _, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(vm.Source)) { return; }
            page = vm.Source.Page;
            label2.Text = $"Pagina {page} of / {vm.Source.TotalPages}";
            EnableControls(true);
            for (int i = 0; i < pageSize; i++)
            {
                var pok = vm.Source[i];
                var cells = dataGridView1.Rows[i].Cells;
                if (pok is null)
                {
                    cells[0].Value = null;
                    cells[1].Value = null;
                    cells[2].Value = null;
                    continue;
                }

                cells[0].Value = pok.Id;
                cells[1].Value = pok.Name;
                cells[2].Value = vm.Source.GetImage(pok);
            }
        }

        private void EnableControls(bool enable = true)
        {
            BtnPrevious.Enabled = enable;
            BtnNext.Enabled = enable;
            BtnCancel.Enabled = !enable;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (vm.Source.TotalPages < page + 1) { return; }
            EnableControls(false);
            vm.LoadPokemon(page + 1, pageSize);
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (page - 1 < 1) { return; }
            EnableControls(false);
            vm.LoadPokemon(page - 1, pageSize);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            vm.CancellPokemonLoad();
            EnableControls(true);
        }
    }
}