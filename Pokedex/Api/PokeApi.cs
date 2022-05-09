using System.Text.Json;
using Pokedex.Api.Reponses;
using Pokedex.Api.Request;
using Microsoft.AspNetCore.WebUtilities;

namespace Pokedex.Api;
public class PokeApi
{
    private readonly HttpClient _client = new HttpClient();
    private readonly JsonSerializerOptions _opt;

    public PokeApi()
    {
        _opt = new JsonSerializerOptions();
        _opt.PropertyNameCaseInsensitive = true;
        _opt.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
    }

    public async Task<PokemonResponse> GetEntriesAsync(QueryParams query, CancellationToken token = default)
    {
        string? url = QueryHelpers.AddQueryString(Endpoints.Pokemon.Index, query.AsDictionary());
        var response = await _client.GetStreamAsync(url, token);
        var pokemons = await JsonSerializer.DeserializeAsync<PokemonResponse>(response, _opt, token);
        return pokemons ?? new PokemonResponse();
    }

    public async Task<ApiPokemon?> GetPokemonAsync(int id, CancellationToken token = default)
    {
        string url = Endpoints.Pokemon.ById(id);
        var response = await _client.GetStreamAsync(url, token);
        var pokemon = await JsonSerializer.DeserializeAsync<ApiPokemon>(response, _opt, token);
        return pokemon;
    }
}

