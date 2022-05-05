﻿namespace Pokedex.Api.Reponses;
public class PokemonResponse
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }

    public List<ApiEntry> Results { get; set; } = new List<ApiEntry>();
}
