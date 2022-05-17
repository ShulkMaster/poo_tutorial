using Pokedex.Repositories;
namespace Pokedex.Views
{
    public partial class DetailForm : Form
    {
        private readonly Pokemon pokemon;
        private readonly SpriteRepository imgRepo;

        public DetailForm(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            imgRepo = new SpriteRepository();
        }

        private async void DetailForm_Load(object sender, EventArgs e)
        {
            var tokenSource = new CancellationTokenSource();
            var imgList = await imgRepo.GetAllSprites(pokemon, tokenSource.Token);
            pictureBox1.Image = imgList[0];
            pictureBox2.Image = imgList[1];
            pictureBox3.Image = imgList[2];
        }
    }
}
