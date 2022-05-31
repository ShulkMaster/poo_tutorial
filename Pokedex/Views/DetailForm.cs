using Pokedex.Resources;
using Pokedex.ViewModels;
using System.ComponentModel;
using Pokedex.Views;

namespace Pokedex.Views
{
    public partial class DetailForm : Form
    {
        private readonly Pokemon pokemon;
        private int indexdisplay = 0;
        private readonly DetailViewModel vm = new DetailViewModel();
        private CancellationTokenSource? _source = null;

        public DetailForm(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            vm.PropertyChanged += SetView;
            lblName.Text = $"{DetailForm_r.PokeName} {pokemon.Name}";
            lblWeight.Text = $"{DetailForm_r.PokeWeight} {pokemon.Weight}";
            lblXp.Text = $"{DetailForm_r.PokeXp} {pokemon.BaseExperience}";
        }

        private void DetailForm_Load(object sender, EventArgs e)
        {
            _source = new CancellationTokenSource();
            vm.LoadImages(pokemon, _source.Token);
        }
        /*
          -> onClosing()
          -> Destrutions
          -> onClosed()
         */

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetView(object? _, PropertyChangedEventArgs e) {
            _source?.Dispose();
            _source = null;
            var errorImg = Images.NoticeError;
            var list = vm.Pics;
            pictureBox1.Image = list[0].Pic ?? errorImg;
            pictureBox2.Image = list[1].Pic ?? errorImg;
            pictureBox3.Image = list[2].Pic ?? errorImg;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var errorImg = Images.NoticeError;
            indexdisplay++;
            var list = vm.Pics;
            pictureBox1.Image = list[indexdisplay % list.Count].Pic ?? errorImg;
            pictureBox2.Image = list[(indexdisplay + 1) % list.Count].Pic ?? errorImg;
            pictureBox3.Image = list[(indexdisplay + 2) % list.Count].Pic ?? errorImg;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _source?.Cancel();
            _source?.Dispose();
            _source = null;
        }
    }
}
