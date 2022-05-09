namespace Pokedex.Repositories.Dto;

public class PokeList
{
    public List<Pokemon?> Pokemons { get; set; } = new List<Pokemon?>();

    public int Total { get; set; } = 1126;
}
