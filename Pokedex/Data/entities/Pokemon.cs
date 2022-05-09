namespace Pokedex;

public partial class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Weight { get; set; }
    public int BaseExperience { get; set; }

    public virtual Entry IdNavigation { get; set; } = null!;
    public virtual Sprite Sprite { get; set; } = null!;
}
