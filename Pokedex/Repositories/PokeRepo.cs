using Microsoft.EntityFrameworkCore;
using Pokedex.Api;
using Pokedex.Api.Reponses;
using Pokedex.Api.Request;
using Pokedex.Repositories.Dto;

namespace Pokedex.Repositories;

public class PokeRepo
{
    private readonly PokedexContext context;
    private readonly PokeApi api;

    public PokeRepo()
    {
        api = new PokeApi();
        context = new PokedexContext();
    }

    private static Sprite ToSpitre(int id, ApiSprite source)
    {
        var target = new Sprite
        {
            Id = id,
            BackDefault = source.BackDefault,
            BackFemale = source.BackFemale,
            BackShiny = source.BackShiny,
            BackShinyFemale = source.BackShinyFemale,
            FrontDefault = source.FrontDefault,
            FrontFemale = source.FrontFemale,
            FrontShiny = source.FrontShiny,
            FrontShinyFemale = source.FrontShinyFemale,
            HomeDefault = source.Other.Home.FrontDefault,
            HomeFemale = source.Other.Home.FrontFemale,
            HomeShiny = source.Other.Home.FrontShiny,
            HomeShinyFemale = source.Other.Home.FrontShinyFemale
        };
        return target;
    }

    private static Pokemon ToPokemon(ApiPokemon pokemon)
    {
        var poke = new Pokemon
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Weight = pokemon.Weight,
            BaseExperience = pokemon.BaseExperience,
            Sprite = ToSpitre(pokemon.Id, pokemon.Sprites)
        };
        return poke;
    }

    public async Task<Pokemon?> FindAsync(Entry entry, CancellationToken token = default)
    {
        var fromDb = await context.Pokemons.FindAsync(entry.Id, token);
        if (fromDb != null) return fromDb;
        var result = await api.GetPokemonAsync(entry.Id, token);
        if (result == null) return null;
        var pokemon = ToPokemon(result);
        context.Pokemons.Add(pokemon);
        await context.SaveChangesAsync(token);
        return pokemon;
    }

    public async Task<PokeList> FindRangeAsync(QueryParams q, CancellationToken token = default)
    {
        var result = new PokeList();
        var range = q.Offset + q.Limit;
        if (range > result.Total)
        {
            return result;
        }
        var cached = await context.Pokemons
            .OrderBy(p => p.Id)
            .Skip(q.Offset)
            .Take(q.Limit)
            .ToListAsync(token);

        if (cached.Count >= q.Limit)
        {
            result.Pokemons = cached!;
            return result;
        }

        var chuckSize = q.Limit;

        if (cached.Any())
        {
            chuckSize = q.Limit - cached.Count;
        }

        var missingPokemons = new Task<ApiPokemon?>[chuckSize];
        var start = q.Offset + cached.Count + 1;
        for (var i = 0; i < chuckSize; i++)
        {
            missingPokemons[i] = api.GetPokemonAsync(i + start, token);
        }

        await Task.WhenAll(missingPokemons);
        var fetched = missingPokemons
            .Select(t =>
            {
                if (t.Result is null) return null;
                return ToPokemon(t.Result);
            }).ToList();
        IEnumerable<Pokemon> success = fetched.Where(p => p is not null)!;
        context.Pokemons.AddRange(success);
        await context.SaveChangesAsync(token);
        var newLit = new List<Pokemon?>();
        newLit.AddRange(cached);
        newLit.AddRange(fetched);
        result.Pokemons = newLit;
        return result;
    }

}
