namespace Pokedex.Views;

public partial class UserControl1 : UserControl
{
    public UserControl1(string name, Bitmap b)
    {
        InitializeComponent();
        label1.Text = name;
        pictureBox1.Image = b;
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}
