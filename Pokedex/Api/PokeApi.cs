using System.Text.Json;
using Pokedex.Api.Reponses;
using Pokedex.Api.Request;
using Microsoft.AspNetCore.WebUtilities;

namespace Pokedex.Api;
public class PokeApi
{
    private readonly HttpClient _client = new HttpClient();
    private readonly JsonSerializerOptions _opt = new JsonSerializerOptions();

    public PokeApi()
    {
        _opt.PropertyNameCaseInsensitive = true;
    }
    public async Task<PokemonResponse> GetEntriesAsync(QueryParams query)
    {
        string? url = QueryHelpers.AddQueryString(Endpoints.Pokemon.Index, query.AsDictionary());
        var response = await _client.GetStreamAsync(url);
        var pokemons = await JsonSerializer.DeserializeAsync<PokemonResponse>(response, _opt);
        return pokemons ?? new PokemonResponse();
    }

}

