using System;
using System.Collections.Generic;
using System.Linq;
using Pokedex.Api;
using Pokedex.Api.Reponses;
using System.Threading.Tasks;

namespace Pokedex.Repositories;

public class PokeRepo
{
    PokedexContext context = new PokedexContext();

    public async Task<ApiPokemon?> FindAsync(Entry entry, CancellationToken token = default)
    {
        var api = new PokeApi();
        var result = await api.GetPokemonAsync(entry.Id, token);
        return result;
    }

    public async Task<List<>> FetchRange(IEnumerable<Entry> entries, CancellationToken token = default)
    {

    }

}
