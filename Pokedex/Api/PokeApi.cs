using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Pokedex.Api.Reponses;

namespace Pokedex.Api;
public class PokeApi
{
    private readonly HttpClient _client = new HttpClient();
    private readonly JsonSerializerOptions _opt = new JsonSerializerOptions();

    public PokeApi()
    {
        _opt.PropertyNameCaseInsensitive = true;
    }
    public async Task<PokemonResponse> GetEntriesAsync()
    {
        string url = "https://pokeapi.co/api/v2/pokemon";
        var response = await _client.GetStreamAsync(url);
        var pokemons = await JsonSerializer.DeserializeAsync<PokemonResponse>(response, _opt);
        return pokemons ?? new PokemonResponse();
    }

}

