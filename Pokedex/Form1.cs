using Pokedex.ViewModels;
using System.ComponentModel;
using Pokedex.Views;

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
                    foreach (DataGridViewCell cell in cells)
                    {
                        cell.Value = null;
                    }
                    continue;
                }

                cells[nameof(Pokemon.Id)].Value = pok.Id;
                cells["NameColumn"].Value = pok.Name;
                cells["Picture"].Value = vm.Source.GetImage(pok);
                cells[nameof(Pokemon.BaseExperience)].Value = pok.BaseExperience;
                cells[nameof(Pokemon.Weight)].Value = pok.Weight;
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

        private void pokemonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var poke = vm.Source[0];
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var p = vm.Source[e.RowIndex] ?? new Pokemon();
            var form = new DetailForm(p);
            form.ShowDialog();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                var rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var p = vm.Source[rowIndex] ?? new Pokemon();
                var form = new DetailForm(p);
                form.ShowDialog();
            }
        }
    }
}