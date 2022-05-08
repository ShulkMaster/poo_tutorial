namespace Pokedex.Api.Reponses;

public class ApiPokemon
{
    public int BaseExperience { get; set; }
    public int Height { get; set; }
    public int Id { get; set; }
    public bool IsDefault { get; set; }
    public string LocationAreaEncounters { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public int Weight { get; set; }
    public ApiSprite Sprites { get; set; } = null!;
}
