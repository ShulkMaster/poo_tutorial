namespace Pokedex;

public partial class Sprite
{
    public int Id { get; set; }

    public string? BackDefault { get; set; }

    public string? BackFemale { get; set; }

    public string? BackShiny { get; set; }

    public string? BackShinyFemale { get; set; }

    public string? FrontDefault { get; set; }

    public string? FrontFemale { get; set; }

    public string? FrontShiny { get; set; }

    public string? FrontShinyFemale { get; set; }

    public string? HomeDefault { get; set; }

    public string? HomeFemale { get; set; }

    public string? HomeShiny { get; set; }

    public string? HomeShinyFemale { get; set; }

    public virtual Pokemon IdNavigation { get; set; } = null!;
}
