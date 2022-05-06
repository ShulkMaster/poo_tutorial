namespace Pokedex.Repositories.Dto;

public class PokemonList
{
    public List<Entry> Entries { get; set; } = new List<Entry>();

    public int Total { get; set; } = 1126;

}
