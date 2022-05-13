namespace Pokedex.Views;

public partial class SettingsForm : Form
{

    public SettingsForm(Pokemon p, Bitmap img)
    {
        InitializeComponent();
        P = p;
        label1.Text = p.Name;
        pictureBox1.Image = img;
    }

    public Pokemon P { get; }

    private void label1_Click(object sender, EventArgs e)
    {
        
    }
}
